using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CaliburnMicroListBoxDoubleClick
{
    public class ShellViewModel : PropertyChangedBase
    {
        private List<Person> persons;

        public ShellViewModel()
        {
            persons = new List<Person>();
            persons.Add(new Person() { FirstName = "Arie", LastName = "Jones", City = "Indianapolis" });
            persons.Add(new Person() { FirstName = "Brett", LastName = "Canova", City = "Indianapolis" });
            persons.Add(new Person() { FirstName = "Justin", LastName = "Bieber", City = "Somewhere in Canada" });
            persons.Add(new Person() { FirstName = "Steve", LastName = "Jones", City = "Denver" });
            persons.Add(new Person() { FirstName = "Dennis", LastName = "Miller", City = "Santa Barbara" });
            persons.Add(new Person() { FirstName = "David", LastName = "Letterman", City = "New York" });

            HereList = new ObservableCollection<Person>(persons);
            ThereList = new ObservableCollection<Person>();
        }

        private ObservableCollection<Person> _HereList;

        public ObservableCollection<Person> HereList
        {
            get
            {
                return _HereList;
            }
            set
            {
                _HereList = value;
                NotifyOfPropertyChange(() => HereList);
            }
        }

        private ObservableCollection<Person> _ThereList;

        public ObservableCollection<Person> ThereList
        {
            get
            {
                return _ThereList;
            }
            set
            {
                _ThereList = value;
                NotifyOfPropertyChange(() => ThereList);
            }
        }

        public void MoveToThere(Person person)
        {
            HereList.Remove(person);
            ThereList.Add(person);
        }

        public void MoveToHere(Person person)
        {
            HereList.Add(person);
            ThereList.Remove(person);
        }
    }
}