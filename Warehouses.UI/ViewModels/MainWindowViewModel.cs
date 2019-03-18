using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.UI.ViewModels
{
    public class MainWindowViewModel
    {        
        public MainWindowViewModel(IMainMenuViewModel mainMenuViewModel)
        {
            this.MainMenuViewModel = mainMenuViewModel;
        }
        public void Load()
        {

        }
        public IMainMenuViewModel MainMenuViewModel { get; }
    }
}
