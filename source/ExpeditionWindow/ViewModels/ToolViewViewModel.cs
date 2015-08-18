using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livet;
using Livet.Messaging;

namespace Grabacr07.KanColleViewer.Plugins.ExpeditionMonitor.ViewModels
{
    public class ToolViewViewModel : ViewModel
    {
        public void ShowWindow()
        {
            this.Messenger.Raise(new TransitionMessage(new MainWindowViewModel(), "MainWindow.Show"));
        }
    }
}
