using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MetroTrilithon.Serialization;

namespace Grabacr07.KanColleViewer.Plugins.ExpeditionMonitor.Models
{
	public static class MainWindowSettings
	{
		public static SerializableProperty<bool> TopMost { get; }
			= new SerializableProperty<bool>(GetKey(), Providers.Local, true) { AutoSave = true, };

		public static SerializableProperty<double> TaskbarUpdateInterval { get; }
			= new SerializableProperty<double>(GetKey(), Providers.Local, 20000.0) { AutoSave = true, };

		private static string GetKey([CallerMemberName] string caller = "")
		{
			return nameof(MainWindowSettings) + "." + caller;
		}
	}
}
