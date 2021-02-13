using Caliburn.Micro;

namespace CaliburnMicroListBoxDoubleClick
{
    public class Person : PropertyChangedBase
    {
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                NotifyOfPropertyChange(() => FirstName);
            }
        }

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                NotifyOfPropertyChange(() => LastName);
            }
        }

        private string _City;

        public string City
        {
            get { return _City; }
            set
            {
                _City = value;
                NotifyOfPropertyChange(() => City);
            }
        }
    }
}