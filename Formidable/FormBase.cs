using Formidable.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formidable
{
    public abstract class FormBase<TFormView, TFormViewModel> : Form
        where TFormView : IFormView<TFormViewModel>, new()
        where TFormViewModel : IFormViewModel, new()
    {
        public FormBase() : base()
        {
            this.View = new TFormView();
            this.initializeForm();
            
        }

        /// <summary>
        /// Initializes Form
        /// </summary>
        protected abstract void initializeForm();
        
        /// <summary>
        /// Performs Asynchronous operations on the provided controls
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="Control"></param>
        /// <param name="UIOperation"></param>
        /// <param name="Async"></param>
        protected void WithControl<TControl>(TControl Control, Action<TControl> UIOperation, bool Async = false)
            where TControl : Control
        {
            ResponsiveUI.Instance.WithControl<TControl>(Control, UIOperation, Async);
        }

        /// <summary>
        /// Form View Model
        /// </summary>
        protected TFormView View { get; set; }
    }
}
