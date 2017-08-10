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
            this.bindControls();
        }

        public void Initialize()
        {  
            // After View and View models are created, but before controls are initialized.
            this.tbText.KeyPress += (sender, args) =>
            {
                if (!char.IsDigit(args.KeyChar))
                {
                    args.Handled = false;
                }
            };
        }

        private void bindControls()
        {
            this.View.Bind(tbText, "Text", "MembersNumber");
            this.View.Bind(label2, "Text", "MembersNumber");
        }

        private void setupGridView()
        {
            this.WithControl(this.dgMembers, (grid) =>
            {
                grid.DataSource = null;
                clmFirstName.DataPropertyName = "FirstName";
                clmLastName.DataPropertyName = "LastName";
                clmAge.DataPropertyName = "Age";
                clmRegistrationDate.DataPropertyName = "RegistrationDate";
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WithNewTask(() =>
            {
                tsOngoingOperation.Text = $"Loading {this.View.ViewModel.GetMemberNumberInt()} Members...Please wait...";

                this.setupGridView();
                // Load Content
                this.View.LoadMembers();
            }, () =>
            {
                this.WithControl(this.dgMembers, (gridView) =>
                {
                    gridView.DataSource = this.View.ViewModel.Members;
                    tsOngoingOperation.Text = $"{this.View.ViewModel.GetMemberNumberInt()} were loaded.";
                });
            }, (ex) =>
            {
                // Exception 
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
        }
    }
}
