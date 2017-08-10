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
        private readonly AsyncPresenter _asyncPresenter;

        protected FormBase() : base()
        {
            this.View = new TFormView();
            this._asyncPresenter = new AsyncPresenter();
        }

        /// <summary>
        /// Runs Specified Operation and Subsequent Callback in a new Task
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="callback"></param>
        /// <param name="failureCallback"></param>
        protected void WithNewTask(Action operation, Action callback = default(Action), Action<Exception> failureCallback = default(Action<Exception>))
        {
            this._asyncPresenter.WithNewTask(operation, callback, failureCallback);
        }
        
        /// <summary>
        /// Performs Asynchronous operations on the provided controls
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="uiOperation"></param>
        /// <param name="async"></param>
        protected void WithControl<TControl>(TControl control, Action<TControl> uiOperation, bool async = false)
            where TControl : Control
        {
            this._asyncPresenter.WithControl<TControl>(control, uiOperation, async);
        }

        /// <summary>
        /// Form View Model
        /// </summary>
        protected TFormView View { get; set; }
    }
}
