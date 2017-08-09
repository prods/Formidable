using Formidable.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Formidable.SnapshotProvider
{
    public class DefaultSnapshotProvider<TFormViewModel> : IFormViewModelSnapshotProvider<TFormViewModel>
        where TFormViewModel : IFormViewModel
    {
        private Dictionary<string, byte[]> _snapshots;

        public DefaultSnapshotProvider()
        {
            this._snapshots = new Dictionary<string, byte[]>();
        }

        /// <summary>
        /// Gets Snapshot
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        public TFormViewModel GetSnapshot(string snapshotId)
        {
            if (this._snapshots.ContainsKey(snapshotId))
            {
                using (MemoryStream _snapshotStream = new MemoryStream(this._snapshots[snapshotId]))
                {
                    BinaryFormatter _formatter = new BinaryFormatter();
                    try
                    {
                        return (TFormViewModel)_formatter.Deserialize(_snapshotStream);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while restoring a snapshot of the view model.", ex);
                    }
                }
            }

            return default(TFormViewModel);
        }

        /// <summary>
        /// Get First Snapshot
        /// </summary>
        /// <returns></returns>
        public TFormViewModel GetFirstSnapshot()
        {
            if (this._snapshots.Any())
            {
                using (MemoryStream _snapshotStream = new MemoryStream(this._snapshots.First().Value))
                {
                    BinaryFormatter _formatter = new BinaryFormatter();
                    try
                    {
                        return (TFormViewModel)_formatter.Deserialize(_snapshotStream);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while restoring the latest snapshot of the view model.", ex);
                    }
                }
            }

            return default(TFormViewModel);
        }

        /// <summary>
        /// Get Latest Snapshot
        /// </summary>
        /// <returns></returns>
        public TFormViewModel GetLatestSnapshot()
        {
            if (this._snapshots.Any())
            {
                using (MemoryStream _snapshotStream = new MemoryStream(this._snapshots.Last().Value))
                {
                    BinaryFormatter _formatter = new BinaryFormatter();
                    try
                    {
                        return (TFormViewModel)_formatter.Deserialize(_snapshotStream);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while restoring the latest snapshot of the view model.", ex);
                    }
                }
            }

            return default(TFormViewModel);
        }

        /// <summary>
        /// Take Snapshot
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="toDisk"></param>
        /// <returns></returns>
        public string TakeSnapshot(TFormViewModel viewModel, bool toDisk = false)
        {
            string _snapshotId = "";

            using (MemoryStream _snapshotStream = new MemoryStream())
            {
                BinaryFormatter _formatter = new BinaryFormatter();
                try
                {
                    _formatter.Serialize(_snapshotStream, viewModel);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error while taking a snapshot of the view model.", ex);
                }

                this._snapshots.Add(_snapshotId, _snapshotStream.ToArray());
            }

            return _snapshotId;
        }

        // Clean Snapshots
        public void Clean()
        {
            this._snapshots.Clear();
        }

        /// <summary>
        /// Get Snapshots Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this._snapshots.Count();
        }
    }
}
