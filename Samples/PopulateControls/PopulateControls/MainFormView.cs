using Formidable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulateControls
{
    /// <summary>
    /// Main FormView Plug
    /// </summary>
    public abstract class MainFormViewPlug : FormBase<MainFormView,MainFormViewModel>
    {
        public MainFormViewPlug() : base() {
            this.Initialize();
        }

        protected override void initializeForm()
        {
            this.Initialize();
        }

        public abstract void Initialize();
    }

    /// <summary>
    /// Main Form View
    /// </summary>
    public class MainFormView : FormViewBase<MainFormViewModel>
    {
        public MainFormView() : base()
        {

        }

        protected override void Initialize()
        {
            
        }

        protected override void InitializeOnDesignMode()
        {
            
        }
    }
}
