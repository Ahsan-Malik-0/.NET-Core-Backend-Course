using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExample
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    internal class Program 
    { 
        public static void Main()
        {
            // Deserialize JSON to Person object
            string json = "{\"Name\": \"John Doe\", \"Age\": 30}";
            Person person = JsonConvert.DeserializeObject<Person>(json);
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

            // Serialize Person object to JSON
            Person newPerson = new Person { Name = "Ping Jeong", Age = 25 };
            string newJson = JsonConvert.SerializeObject(newPerson);
            Console.WriteLine($"Serialized JSON: {newJson}");
        }
    }
}
