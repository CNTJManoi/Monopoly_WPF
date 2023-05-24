using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Logic
{
    public class PropertyNotificator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Базовый класс MVVM для определения PropertyChanged
        /// </summary>
        /// <param name="prop"></param>
        public void RaisePropertyChanged([CallerMemberName] string prop=null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
