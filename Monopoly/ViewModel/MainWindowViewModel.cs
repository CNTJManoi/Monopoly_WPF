using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.ViewModel
{
    public class MainWindowViewModel : BasicViewModel   
    {
        private BasicViewModel _viewModel;
        public BasicViewModel CurrentContentVM
        {
            get
            {
                return _viewModel;
            }
            set
            {
                if (_viewModel != value)
                {
                    _viewModel = value;
                    RaisePropertyChanged("CurrentContentVM");
                }
            }
        }

        public MainWindowViewModel()
        {
            MenuViewModel mw = new MenuViewModel(this);
            CurrentContentVM = mw;
        }

        public void GoToViewModel(BasicViewModel bvm)
        {
            CurrentContentVM = bvm;
        }
    }
}
