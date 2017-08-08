using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formidable.Interfaces
{
    /// <summary>
    /// Form View Model Interface
    /// </summary>
    public interface IFormViewModel : INotifyPropertyChanged
    {
        bool HasChanges();

    }
}
