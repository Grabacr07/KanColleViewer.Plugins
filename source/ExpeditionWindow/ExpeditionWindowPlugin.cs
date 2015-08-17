using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Composition;

namespace Grabacr07.ExpeditionWindow
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [ExportMetadata("Guid", "A515138A-7ED6-4FB8-9F76-F1CFE6AF964A")]
    [ExportMetadata("Title", "ExpeditionWindow")]
    [ExportMetadata("Description", "遠征出し忘れが激しいので、専用ウィンドウ作った。")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "@Grabacr07")]
    public class ExpeditionWindowPlugin : IPlugin, ITool
    {
        public void Initialize()
        {
        }

        string ITool.Name
        {
            get { throw new NotImplementedException(); }
        }

        object ITool.View
        {
            get { throw new NotImplementedException(); }
        }
    }
}
