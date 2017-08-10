using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PopulateControls.Models
{
    public class Member : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private int _age;
        private DateTime _registrationDate;
        
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (this._firstName != value)
                {
                    this._firstName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (this._lastName != value)
                {
                    this._lastName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (this._age != value)
                {
                    this._age = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set
            {
                if (this._registrationDate != value)
                {
                    this._registrationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
