using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroTrilithon.Serialization;

namespace Grabacr07.ExpeditionWindow.Models
{
	public static class MainWindowSettings
	{
		public static SerializableProperty<bool> TopMost { get; }
			= new SerializableProperty<bool>(nameof(MainWindowSettings) + "." + nameof(TopMost), Providers.Local, true);
	}
}
