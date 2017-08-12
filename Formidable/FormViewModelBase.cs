using Formidable.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Formidable
{
    public class FormViewModelBase : IFormViewModel
    {
        private bool _hasChanges;
        public event PropertyChangedEventHandler PropertyChanged;

        public FormViewModelBase()
        {
            this._hasChanges = false;
        }

        /// <summary>
        /// Raises Property Changed event
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                this._hasChanges = true;
            }
        }

        /// <summary>
        /// Resets the Form View Model State
        /// </summary>
        public void ResetState()
        {
            this._hasChanges = false;
        }

        /// <summary>
        /// Determines if the current view model was changed
        /// </summary>
        /// <returns></returns>
        public virtual bool HasChanges()
        {
            return this._hasChanges;
        }
    }
}
