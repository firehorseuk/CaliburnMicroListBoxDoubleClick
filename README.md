# CaliburnMicroListBoxDoubleClick
CaliburnMicro ListBox DoubleClick Example

This solves the perennial problem WPF MVVM ListBox double click problem along with using the CaliburnMicro framework.

There is no code-behind.

Example is taken from https://www.programmersedge.com/caliburnmicro-wpf-double-click-event/

Its a wordpress site and the images and file download do not link to anything so I've re-created the example and put it up on Github in case the original example ever disappears.


Original article below in case the original site disappears.

#CaliburnMicro : WPF Double Click Event

Arie D. Jones

Web Developer

9 JUN 2011 • 6 MIN READ

So a lot of the time you may be wanting to create a scenario that is not quite out of the box…such as a user being able to double-click on an object that doesn’t natively support a double-click event.

Caliburn.Micro does an awesome job handling messages on events but WPF has certain intricacies that  you have to take into account so that the framework can perform the job that it was intended to do.

So let’s start out with a very simple example. I will create a view here with just two listboxes. Nothing sneaky here, just two plain ol’ ListBox objects….one is the HereList and one is the ThereList.

<img src="https://github.com/firehorseuk/CaliburnMicroListBoxDoubleClick/blob/master/CaliburnMicroListBoxDoubleClick/CaliburnMicroListBoxDoubleClickScreen.png">

Now I would like to populate the HereList with a list of objects and allow my user to double click on any given object. Once the double click event occurs then that object is magically transported over to the ThereList. Pretty simple right? Except that the ListBox control doesn’t really have a nice way to allow you to use Caliburn.Micro’s Message.Attach to the objects without totally hosing things up. The reason why is that when you try to attach a message to an object within the ListBox it is done so at the context of the actual object level. So if I have a ObservableCollection<Person> attached to my HereList control…. then it will try to locate the Person.MoveTheStuffOverToTheOtherBoxCommand which more often than not does’t exist.  🙁

Not a problem. We can actually pretty simply apply a style to the listbox in order for Caliburn.Micro to attach a message to each ListBoxItem and also tell the framework to target our parent ViewModel. So let’s get our ViewModel wired up first…we’ll add a simple Person class in as well.

      public class Person: PropertyChangedBase
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
 

Now we’ll add in some properties into our ShellViewModel to support the lists for the ListBoxes and the event for moving a person to the other box.

      public class ShellViewModel : PropertyChangedBase
      {
          private List<Person> persons;
   
          public ShellViewModel()
          {
                persons = new List<Person>();
                persons.Add(new Person(){ FirstName="Arie", LastName="Jones", City="Indianapolis"});
                persons.Add(new Person(){ FirstName="Brett", LastName="Canova", City="Indianapolis"});
                persons.Add(new Person(){ FirstName="Justin", LastName="Bieber", City="Somewhere in Canada"});
                persons.Add(new Person(){ FirstName="Steve", LastName="Jones", City="Denver"});
                persons.Add(new Person(){ FirstName="Dennis", LastName="Miller", City="Santa Barbara"});
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
 

And our ListBoxes basically look like this

        <ListBox x:Name="HereList" Height="233" HorizontalAlignment="Left" 
                 Margin="12,50,0,0"  VerticalAlignment="Top" Width="173">
            <ListBox.ItemTemplate>
                <DataTemplate>
 
                        <Grid Margin="2" >
                            <Grid.RowDefinitions>
                                <RowDefinition/>
                                <RowDefinition/>
                            </Grid.RowDefinitions>
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="{Binding FirstName}"/>
                                <TextBlock Text="{Binding LastName}"/>
                            </StackPanel>
                            <TextBlock Grid.Row="1" Text="{Binding City}" FontWeight="SemiBold" />
                        </Grid>
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>

Now we need to make a couple of small changes to the XAML to wire up the events. First thing is that we need to put an ItemsPanel template on our ListBoxes so that we can point caliburn to link up with the correct context. Nothing fancy just an ordinary StackPanel.

            <ListBox.ItemsPanel>
                <ItemsPanelTemplate>
                    <StackPanel cal:Action.TargetWithoutContext="{Binding}" />
                </ItemsPanelTemplate>
            </ListBox.ItemsPanel>
 

Now we can add in an ItemContainerStyle to take care of setting each of the ListBoxItems up with an appropriate call to our functions

            <ListBox.ItemContainerStyle>
                <Style TargetType="{x:Type ListBoxItem}">
                    <Setter Property="cal:Message.Attach" 
                            Value="[Event MouseDoubleClick] = [Action MoveToThere($dataContext)]"/>
                </Style>
            </ListBox.ItemContainerStyle>
 
Now each of our items will call back to the appropriate ViewModel function and pass along the $dataContext which in this instance is just a Person object. And we now have the intended functionality!

<img src="https://github.com/firehorseuk/CaliburnMicroListBoxDoubleClick/blob/master/CaliburnMicroListBoxDoubleClick/CaliburnMicroListBoxDoubleClickCompleted.png">

And here’s the completed project for you to peruse

WPFDoubleClickExample.zip

Hopefully this helps a few of you out as you being to figure out the framework.

Cheers!

AJ
