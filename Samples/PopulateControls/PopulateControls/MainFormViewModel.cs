using System.Collections.Generic;
using Formidable;
using PopulateControls.Models;

namespace PopulateControls
{
    /// <summary>
    /// Main Form View Model
    /// </summary>
    public class MainFormViewModel : FormViewModelBase
    {
        private int _membersNumber;
        private IEnumerable<Member> _members;

        public MainFormViewModel() : base()
        {
            // Initial State
            this._membersNumber = 0;
        }

        public string MembersNumberString
        {
            get { return this._membersNumber.ToString(); }
            set
            {
                int _value = 0;
                if (value == string.Empty)
                {
                    this._membersNumber = _value;
                }
                else
                {
                    int.TryParse(value, out _value);
                    if (this._membersNumber != _value)
                    {
                        this._membersNumber = _value;
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        public int MemberNumber
        {
            get
            {
                return this._membersNumber;
            }
        }

        public IEnumerable<Member> Members
        {
            get { return this._members; }
            set
            {
                if (this._members != value)
                {
                    this._members = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int GetMemberNumberInt()
        {
            return this._membersNumber;
        }
    }
}
