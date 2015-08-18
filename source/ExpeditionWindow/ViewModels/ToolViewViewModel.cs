using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Messaging;

namespace Grabacr07.ExpeditionWindow.ViewModels
{
    public class ToolViewViewModel : ViewModel
    {
        public void ShowWindow()
        {
            this.Messenger.Raise(new TransitionMessage(new MainWindowViewModel(), "MainWindow.Show"));
        }
    }
}
