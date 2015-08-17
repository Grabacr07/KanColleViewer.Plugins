using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper;
using Livet;
using MetroTrilithon.Mvvm;

namespace Grabacr07.ExpeditionWindow.ViewModels
{
	public class MainWindowViewModel : WindowViewModel
	{
		#region Expeditions 変更通知プロパティ

		private IReadOnlyList<ExpeditionViewModel> _Expeditions;

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

			this.Expeditions = KanColleClient.Current.Homeport.Organization.Fleets
				.Select(x => x.Value.Expedition)
				.Select(x => new ExpeditionViewModel(this, x))
				.ToList();
		}
	}
}
