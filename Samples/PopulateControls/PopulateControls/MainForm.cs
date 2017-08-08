using Formidable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopulateControls
{
    public partial class MainForm : MainFormViewPlug
    {
        public MainForm() : base()
        {
            InitializeComponent();
            this.BindControls();
        }

        public override void Initialize()
        {
            // After View and View models are created, but before controls are initialized.
            this.View.ViewModel.LabelText = "Enter a text here...";
        }

        private void BindControls()
        {
            this.View.Bind(tbText, "Text", "LabelText");
            this.View.Bind(label2, "Text", "LabelText");
        }
    }
}
