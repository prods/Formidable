using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formidable;
using Formidable.Interfaces;
using PopulateControls.Models;
using PopulateControls.Repositories;

namespace PopulateControls.Controls
{
    public partial class SampleGridControl : SampleGridViewPlug
    {
        public SampleGridControl() : base()
        {
            InitializeComponent();
            this.initialize();
        }

        private void initialize()
        {
            this.bindControls();
        }

        private void bindControls()
        {
            this.View.Bind(this.lblCaption, "Text", "Caption");
        }

        private void setupGrid()
        {
            this.WithControl(this.dgvMain, (grid) =>
            {
                grid.DataSource = null;
                this.clmFirstName.DataPropertyName = "FirstName";
                this.clmLastName.DataPropertyName = "LastName";
                this.clmAge.DataPropertyName = "Age";
                this.clmRegistrationDate.DataPropertyName = "RegistrationDate";
            });
        }

        public void RefreshData()
        {
            this.WithNewTask(() =>
            {
                // Not Perfect, but does the work. Binding changes from a different thread causes exceptions.
                // But adding the control here defeats the purpose of the binding...
                this.WithControl(lblCaption, (lbl) =>
                {
                    this.View.SetCaption($"Loading {this.View.GetMemberNumber()} Members...");
                });
                this.setupGrid();
                this.View.Refresh();
            }, () =>
            {
                this.WithControl(this.dgvMain, (grid) =>
                {
                    this.dgvMain.DataSource = this.View.ViewModel.Members;
                    this.View.SetCaption($"{this.View.GetMemberNumber()} Members");
                });
            });
        }

        public string Caption
        {
            get
            {
                return this.View.GetCaption();
            }
            set
            {
                this.View.SetCaption(value);
            }
        }

        public int MembersNumber
        {
            get
            {
                return this.View.GetMemberNumber();
            }
            set
            {
                this.View.SetMemberNumber(value);
            }
        }

    }

    // Create and do not touch
    // Do not add any logic in this class
    public class SampleGridViewPlug : ControlBase<SampleGridView, SampleGridViewModel>
    {
        public SampleGridViewPlug() : base()
        {
            
        }
    }


    public class SampleGridViewModel : ControlViewModelBase, IControlViewModel
    {
        private string _caption;
        private int _memberNumber;
        private IEnumerable<Member> _members;

        public SampleGridViewModel() : base()
        {
            this.Caption = "Members";
        }

        public string Caption
        {
            get
            {
                return this._caption;
            }
            set
            {
                if (this._caption != value)
                {
                    this._caption = value;
                    NotifyPropertyChanged("Caption");
                }
            }
        }

        public int MemberNumber
        {
            get
            {
                return this._memberNumber;
            }
            set
            {
                if (this._memberNumber != value)
                {
                    this._memberNumber = value;
                    NotifyPropertyChanged("MemberNumber");
                }
            }
        }

        public IEnumerable<Member> Members
        {
            get
            {
                return this._members;
            }
            set
            {
                if (this._members != value)
                {
                    this._members = value;
                    NotifyPropertyChanged("Members");
                }
            }
        }

    }

    public class SampleGridView : ControlViewBase<SampleGridViewModel>, IControlView<SampleGridViewModel>
    {
        private MemberRepository _repository;

        public SampleGridView() : base()
        {
            this._repository = new MemberRepository();
        }

        protected override void Initialize()
        {
            
        }

        protected override void InitializeOnDesignMode()
        {
            
        }

        public void Refresh()
        {
            this.ViewModel.Members = this._repository.GetMembers(this.ViewModel.MemberNumber);
        }

        public int GetMemberNumber()
        {
            return this.ViewModel.MemberNumber;
        }

        public void SetMemberNumber(int value)
        {
            this.ViewModel.MemberNumber = value;
        }

        public string GetCaption()
        {
            return this.ViewModel.Caption;
        }

        public void SetCaption(string value)
        {
            this.ViewModel.Caption = value;
        }
    }
}
