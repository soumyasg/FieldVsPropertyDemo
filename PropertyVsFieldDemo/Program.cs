using System;

namespace PropertyVsFieldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var personWithField = new PersonWithFields();
            personWithField.Name = string.Empty;  // Allowed !!
            personWithField.Age = -10; // Allowed

            var personWithProperties = new PersonWithProperties();

            // Subscribe to the event
            personWithProperties.NameChanged += (sender, e) => Console.WriteLine("Name has changed!");

            personWithProperties.Name = "Sam"; // This will trigger event
            personWithProperties.Age = 10; // This will be OK

            personWithProperties.Name = string.Empty; // Runtime exception
            personWithProperties.Age = -10; // Runtime exception



        }
    }

    class PersonWithFields
    {
        public string Name;
        public int Age;
        
    }

    class PersonWithProperties
    {
        private string name;
        private int age;
        public event EventHandler NameChanged;

        public string Name
        {
            get => this.name;

            set
            {
                // null check
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name can't be blank");

                // assign the value
                this.name = value;

                // notify that the name has changed
                if (NameChanged != null)
                    NameChanged(this, EventArgs.Empty);

            }
        }

        public int Age
        {
            get => this.age;

            set // Make sure age values make sense
            {
                if (value <= 0) 
                    throw new ArgumentException("Age must be positive!");
                if(value > 120)
                    throw new ArgumentException("Please enter a valid age!");

                // assign the value
                this.age = value;

            }
        }
    }
}
