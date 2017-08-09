using Formidable.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Formidable
{
    public abstract class FormViewBase<TFormViewModel> : IFormView<TFormViewModel>, INotifyPropertyChanged
        where TFormViewModel : IFormViewModel, new()
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFormViewModelSnapshotProvider<TFormViewModel> _snapshotProvider;
        
        #region Constructors

        public FormViewBase()
        {
            this.ViewModel = new TFormViewModel();

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

        public FormViewBase(TFormViewModel model)
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

        public FormViewBase(IFormViewModelSnapshotProvider<TFormViewModel> snapshotProvider)
        {
            this.ViewModel = new TFormViewModel();

            if (this.IsDesignMode)
            {
                this.InitializeOnDesignMode();
            }
            else
            {
                this.Initialize();
            }

            this._snapshotProvider = snapshotProvider;

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
                throw new Exception("Exception while binding control to form view property.", ex);
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

            // Set default Snapshots Provider
            if (this._snapshotProvider == null)
            {
                this._snapshotProvider = new SnapshotProvider.DefaultSnapshotProvider<TFormViewModel>();
            }
        }

        #endregion

        #region View Model State Snapshots


        /// <summary>
        /// Takes a Snapshot from the View Model State
        /// </summary>
        public string TakeViewModelSnapshot(bool toDisk = false)
        {
            return this._snapshotProvider.TakeSnapshot(this.ViewModel, toDisk);
        }

        /// <summary>
        /// Restores a View Model State Snapshot
        /// </summary>
        public void RestoreViewModelSnapshot(string snapshotId)
        {
            this.ViewModel = this._snapshotProvider.GetSnapshot(snapshotId);
        }

        /// <summary>
        /// Restores last View Model State Snapshot
        /// </summary>
        public void RestoreLatestViewModelSnapshot()
        {
            this.ViewModel = this._snapshotProvider.GetLatestSnapshot();
        }

        /// <summary>
        /// Restores last View Model State Snapshot
        /// </summary>
        public void RestoreFirstViewModelSnapShot()
        {
            this.ViewModel = this._snapshotProvider.GetFirstSnapshot();
        }

        /// <summary>
        /// Clear all snapshots
        /// </summary>
        public void CleanViewModelSnapshots()
        {
            this._snapshotProvider.Clean();
        }

        /// <summary>
        /// Get Snapshots count
        /// </summary>
        /// <returns></returns>
        public int SnapshotsCount()
        {
            return this._snapshotProvider.Count();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Form View Model
        /// </summary>
        public TFormViewModel ViewModel { get; set; }

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
