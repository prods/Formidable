using Formidable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Formidable;

namespace PopulateControls
{
    /// <summary>
    /// Main Form View Model
    /// </summary>
    public class MainFormViewModel : FormViewModelBase
    {
        private string _labelText;

        public MainFormViewModel() : base()
        {
            this._labelText = string.Empty;
        }

        public string LabelText
        {
            get
            {
                return this._labelText;
            }
            set
            {
                if (this._labelText != value)
                {
                    this._labelText = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
