using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;
using Grabacr07.KanColleWrapper;
using MetroTrilithon.Lifetime;
using MetroTrilithon.Mvvm;

namespace Grabacr07.ExpeditionWindow.ViewModels
{
	public class MainWindowViewModel : WindowViewModel
	{
		#region Expeditions 変更通知プロパティ

		private IReadOnlyList<ExpeditionViewModel> _Expeditions = new List<ExpeditionViewModel>();

		public IReadOnlyList<ExpeditionViewModel> Expeditions
		{
			get { return this._Expeditions; }
			set
			{
				if (this._Expeditions != value)
				{
					this._Expeditions = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		public MainWindowViewModel()
		{
			this.Title = "遠征モニター";
		}

		protected override void InitializeCore()
		{
			base.InitializeCore();
			this.CanClose = true;

			KanColleClient.Current.Homeport.Organization
				.Subscribe(nameof(Organization.Fleets), this.UpdateExpeditions)
				.AddTo(this);

			// タスク バーを 1 分おきに更新する
			var timer = new DispatcherTimer(DispatcherPriority.Normal) { Interval = TimeSpan.FromMilliseconds(60000.0), };
			timer.Tick += this.TimerOnTick;
			timer.Start();
		    this.TimerOnTick(null, null); // これはひどぅい

		    Application.Current.MainWindow.Closed += this.MainWindowOnClosed;
		    Disposable.Create(() => Application.Current.MainWindow.Closed -= this.MainWindowOnClosed);
		}

	    public void UpdateExpeditions()
		{
			this.Expeditions = KanColleClient.Current.Homeport.Organization.Fleets
				.Skip(1)
				.Select(x => new { x.Value.Id, x.Value.Expedition, })
				.Where(a => a.Expedition != null)
				.Select(a => new ExpeditionViewModel(a.Id, a.Expedition).AddTo(this))
				.ToList();
		    this.TimerOnTick(null, null);
		}

		private void TimerOnTick(object sender, EventArgs args)
		{
			if (this.Expeditions.Count == 0) return;

			if (this.Expeditions.Any(x => x.State == ExpeditionState.Returned))
			{
				this.UpdateTaskbar(TaskbarItemProgressState.Indeterminate, 1.0);
			}
			else
			{
				var target = this.Expeditions.Where(x => x.Source.IsInExecution).Aggregate(Early);
				if (target.Source.Remaining.HasValue && target.Source.ReturnTime.HasValue)
				{
					var state = this.Expeditions.Any(x => x.State == ExpeditionState.Waiting)
						? TaskbarItemProgressState.Paused
						: TaskbarItemProgressState.Normal;
					var start = target.Source.ReturnTime.Value.Subtract(TimeSpan.FromMinutes(target.Source.Mission.RawData.api_time)); // 開始時間
					var value = DateTimeOffset.Now.Subtract(start).TotalMinutes / target.Source.Mission.RawData.api_time;

					this.UpdateTaskbar(state, value);
				}
				else
				{
					this.UpdateTaskbar(TaskbarItemProgressState.Error, .0);
				}
			}
		}

		private void MainWindowOnClosed(object sender, EventArgs args)
		{
			this.Close();
		}

		private static ExpeditionViewModel Early(ExpeditionViewModel vm1, ExpeditionViewModel vm2)
		{
			// 2 つの遠征を比較して早く帰ってくるほうを返すやつ

			if (vm1.Source.IsInExecution)
			{
				if (vm2.Source.IsInExecution)
				{
					return vm1.Source.ReturnTime < vm2.Source.ReturnTime ? vm1 : vm2;
				}

				return vm1;
			}

			return vm2;
		}
	}
}
