using System.ComponentModel;

namespace Monopoly.ViewModel
{

    public abstract class BasicViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Базовый класс MVVM для определения PropertyChanged
        /// </summary>
        /// <param name="prop"></param>
        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
