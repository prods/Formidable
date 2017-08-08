using Formidable.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formidable
{
    public abstract class FormViewBase<TFormViewModel> : IFormView<TFormViewModel>, INotifyPropertyChanged
        where TFormViewModel : IFormViewModel, new()
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, byte[]> _snapshots = new Dictionary<string, byte[]>();


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
        }

        #endregion

        #region View Model State Snapshots


        /// <summary>
        /// Takes a Snapshot from the View Model State
        /// </summary>
        public string TakeSnapshot(bool toDisk = false)
        {
            string _snapshotId = "";

            using (MemoryStream _snapshotStream = new MemoryStream())
            {
                BinaryFormatter _formatter = new BinaryFormatter();
                try
                {
                    _formatter.Serialize(_snapshotStream, this.ViewModel);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error while taking a snapshot of the view model.", ex);
                }

                this._snapshots.Add(_snapshotId, _snapshotStream.ToArray());
            }

            return _snapshotId;
        }

        /// <summary>
        /// Restores a View Model State Snapshot
        /// </summary>
        public void RestoreSnapShot(string snapshotId)
        {
            if (this._snapshots.ContainsKey(snapshotId))
            {
                using (MemoryStream _snapshotStream = new MemoryStream(this._snapshots[snapshotId]))
                {
                    BinaryFormatter _formatter = new BinaryFormatter();
                    try
                    {
                        this.ViewModel = (TFormViewModel)_formatter.Deserialize(_snapshotStream);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while restoring a snapshot of the view model.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Restores last View Model State Snapshot
        /// </summary>
        public void RestoreLatestSnapShot()
        {
            if (this._snapshots.Any())
            {
                using (MemoryStream _snapshotStream = new MemoryStream(this._snapshots.Last().Value))
                {
                    BinaryFormatter _formatter = new BinaryFormatter();
                    try
                    {
                        this.ViewModel = (TFormViewModel)_formatter.Deserialize(_snapshotStream);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while restoring the latest snapshot of the view model.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Restores last View Model State Snapshot
        /// </summary>
        public void RestoreFirstSnapShot()
        {
            if (this._snapshots.Any())
            {
                using (MemoryStream _snapshotStream = new MemoryStream(this._snapshots.First().Value))
                {
                    BinaryFormatter _formatter = new BinaryFormatter();
                    try
                    {
                        this.ViewModel = (TFormViewModel)_formatter.Deserialize(_snapshotStream);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while restoring the latest snapshot of the view model.", ex);
                    }
                }
            }
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
