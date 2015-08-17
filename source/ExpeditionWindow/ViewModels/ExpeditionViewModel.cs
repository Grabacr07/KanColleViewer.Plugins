using System;
using System.Collections.Generic;
using System.ComponentModel;
using Grabacr07.KanColleWrapper.Models;
using MetroTrilithon.Lifetime;
using MetroTrilithon.Mvvm;
using StatefulModel;

namespace Grabacr07.ExpeditionWindow.ViewModels
{
	public class ExpeditionViewModel : IDisposableHolder, INotifyPropertyChanged
	{
		private readonly MainWindowViewModel owner;
		private readonly Expedition source;
		private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

		[Notify]
		public ExpeditionState State
		{
			get { return this.state; }
			set { this.SetProperty(ref this.state, value, statePropertyChangedEventArgs); }
		}

		[Notify]
		public string ReturnTime
		{
			get { return this.returnTime; }
			set { this.SetProperty(ref this.returnTime, value, returnTimePropertyChangedEventArgs); }
		}

		[Notify]
		public string Remaining
		{
			get { return this.remaining; }
			set { this.SetProperty(ref this.remaining, value, remainingPropertyChangedEventArgs); }
		}

		public ExpeditionViewModel(MainWindowViewModel owner, Expedition expedition)
		{
			this.owner = owner;
			this.source = expedition;
			this.source.Subscribe(nameof(Expedition.Remaining), this.HandleUpdateRemaining).AddTo(this);
			this.source.Subscribe(nameof(Expedition.ReturnTime), this.HandleUpdateReturnTime).AddTo(this);
		}


		private void HandleUpdateRemaining()
		{
			this.Remaining = this.source.Remaining.HasValue
				? $"{(int)this.source.Remaining.Value.TotalHours:D2}:{this.source.Remaining.Value.ToString(@"mm\:ss")}"
				: "--:--:--";
			this.UpdateState();
		}

		private void HandleUpdateReturnTime()
		{
			this.ReturnTime = this.source.ReturnTime?.LocalDateTime.ToString("MM/dd HH:mm") ?? "--/-- --:--";
			this.UpdateState();
		}

		private void UpdateState()
		{
			this.State = this.source.IsInExecution
				? ExpeditionState.Watinig
				: this.source.Remaining?.TotalSeconds > 0
					? ExpeditionState.InExecution
					: ExpeditionState.Returned;
		}

		public void Dispose()
		{
			this.compositeDisposable.Dispose();
		}

		ICollection<IDisposable> IDisposableHolder.CompositeDisposable => this.compositeDisposable;

		#region NotifyPropertyChangedGenerator

		public event PropertyChangedEventHandler PropertyChanged;

		private ExpeditionState state;
		private static readonly PropertyChangedEventArgs statePropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(State));
		private string returnTime;
		private static readonly PropertyChangedEventArgs returnTimePropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(ReturnTime));
		private string remaining;
		private static readonly PropertyChangedEventArgs remainingPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Remaining));

		private void SetProperty<T>(ref T field, T value, PropertyChangedEventArgs ev)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				this.PropertyChanged?.Invoke(this, ev);
			}
		}

		#endregion
	}
}