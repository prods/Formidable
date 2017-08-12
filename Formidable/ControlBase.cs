using Formidable.Interfaces;
using System;
using System.Windows.Forms;

namespace Formidable
{
    public abstract class ControlBase<TControlView, TControlViewModel> : UserControl
        where TControlView: IControlView<TControlViewModel>, new()
        where TControlViewModel : IControlViewModel, new()
    {
        private readonly AsyncPresenter _asyncPresenter;

        protected ControlBase() : base()
        {
            this._asyncPresenter = new AsyncPresenter();
            this.View = new TControlView();
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

        protected TControlView View { get; set; }

    }
}
