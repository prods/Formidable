using Formidable.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Formidable
{
    public abstract class ControlViewBase<TControlViewModel> : IControlView<TControlViewModel>, INotifyPropertyChanged
        where TControlViewModel : IControlViewModel, new()
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        #region Constructors

        public ControlViewBase()
        {
            this.ViewModel = new TControlViewModel();

            if (this.IsDesignMode)
            {
                this.InitializeOnDesignMode();
            }
            else
            {
                this.Initialize();
            }

            this.internalInitialization();
        }

        public ControlViewBase(TControlViewModel model)
        {
            this.ViewModel = model;

            if (this.IsDesignMode)
            {
                this.InitializeOnDesignMode();
            }
            else
            {
                this.Initialize();
            }

            this.internalInitialization();
        }
      
        #endregion

        #region Public Methods and Functions

        /// <summary>
        /// Binds Control to View Model Property
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="Control"></param>
        /// <param name="controlPropertyName"></param>
        /// <param name="viewPropertyName"></param>
        public virtual void Bind<TControl>(TControl Control, string controlPropertyName, string viewPropertyName)
            where TControl : Control
        {
            try
            {
                var bind = new Binding(controlPropertyName, this.ViewModel, viewPropertyName);
                Control.DataBindings.Add(bind);
            }
            catch(Exception ex)
            {
                throw new Exception("Exception while binding control to control view property.", ex);
            }
        }

        /// <summary>
        /// Refreshes Form view model state
        /// </summary>
        public virtual void Refresh()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets form View model state
        /// </summary>
        public virtual void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves Form View Model State
        /// </summary>
        public virtual void Save()
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region Initializers

        /// <summary>
        /// Initialize Form View during constructor execution
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Initializes Form View while on Design Mode during constructor execution
        /// </summary>
        protected abstract void InitializeOnDesignMode();

        /// <summary>
        /// Common Initialization
        /// </summary>
        private void internalInitialization()
        {
            // Wires ViewModel Property Changes Event Upstream
            this.ViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(sender, e);
                }
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Form View Model
        /// </summary>
        public TControlViewModel ViewModel { get; set; }

        /// <summary>
        /// Determines if the current instance is being run in design mode
        /// </summary>
        public bool IsDesignMode
        {
            get
            {
                return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            }
        }
        
        #endregion
    }
}
