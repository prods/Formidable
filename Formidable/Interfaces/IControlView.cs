using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formidable.Interfaces
{
    /// <summary>
    /// Control View
    /// </summary>
    /// <typeparam name="TFormViewModel"></typeparam>
    public interface IControlView<TControlViewModel> : INotifyPropertyChanged
        where TControlViewModel : IControlViewModel
    {
        TControlViewModel ViewModel { get; set; }

        void Reset();
        void Refresh();
        void Save();

        bool IsDesignMode { get; }
    }
}
