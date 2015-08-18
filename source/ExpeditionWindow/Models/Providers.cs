using System;
using System.IO;
using MetroTrilithon.Serialization;

namespace Grabacr07.KanColleViewer.Plugins.ExpeditionMonitor.Models
{
	public static class Providers
	{
		public static string LocalFilePath { get; } = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"grabacr.net", "KanColleViewer", "Settings.ExpeditionWindow.xaml");

		public static ISerializationProvider Local { get; } = new FileSettingsProvider(LocalFilePath);
	}
}