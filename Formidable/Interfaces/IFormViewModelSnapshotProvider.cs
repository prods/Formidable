using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formidable.Interfaces
{
    public interface IFormViewModelSnapshotProvider<TFormViewModel> 
        where TFormViewModel: IFormViewModel
    {
        string TakeSnapshot(TFormViewModel viewModel, bool toDisk = false);
        TFormViewModel GetLatestSnapshot();
        TFormViewModel GetFirstSnapshot();
        TFormViewModel GetSnapshot(string snapshotId);

        void Clean();
        int Count();
    }
}
