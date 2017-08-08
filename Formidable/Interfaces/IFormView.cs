using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formidable.Interfaces
{
    /// <summary>
    /// Form View
    /// </summary>
    /// <typeparam name="TFormViewModel"></typeparam>
    public interface IFormView<TFormViewModel> : INotifyPropertyChanged
        where TFormViewModel : IFormViewModel
    {
        TFormViewModel ViewModel { get; set; }

        void Reset();
        void Refresh();
        void Save();

        bool IsDesignMode { get; }
    }
}
