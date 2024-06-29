using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Common.Command;
using WPF.Common.ViewModel;

namespace BackOffice.UI.Common.ViewModel
{
    public class NavigationViewModel:ViewModelBase
    {
        private ViewModelBase currentView;

        public ViewModelBase CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand HomeCommand { get; private set; }
        public ICommand ClientsCommand { get; private set; }

        public NavigationViewModel()
        {
            this.HomeCommand = new RelayCommand(NavigateToHome);
            this.ClientsCommand = new RelayCommand(NavigateToClients);

        }

        private void NavigateToHome(object obj)
        {
            CurrentView = new HomeViewModel();
        }

        private void NavigateToClients(object obj)
        {
            CurrentView = new ClientsViewModel();
        }
    }
}
