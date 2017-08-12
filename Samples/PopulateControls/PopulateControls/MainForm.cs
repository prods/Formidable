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
            this.initialize();
        }

        #region Events 

        private void button1_Click(object sender, EventArgs e)
        {
            this.refreshMembersData();
        }

        #endregion

        #region Public Methods and Functions

        #endregion

        #region Private Methods and Functions

        private void initialize()
        {
            // After View and View models are created, but before controls are initialized.
            this.tbText.KeyPress += (sender, args) =>
            {
                if (!char.IsDigit(args.KeyChar))
                {
                    args.Handled = true;
                }
            };

            this.bindControls();
        }

        private void bindControls()
        {
            this.View.Bind(tbText, "Text", "MembersNumberString");
            this.View.Bind(label2, "Text", "MembersNumberString");
            this.View.Bind(this.gdvCustomGrid, "MembersNumber", "MemberNumber");
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

        private void refreshMembersData()
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            });

            // Refresh Custom Grid
            gdvCustomGrid.RefreshData();
        }

        #endregion
    }
}
