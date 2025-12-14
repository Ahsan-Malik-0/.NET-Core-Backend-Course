using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeserializationConsoleBase
{
    public class Person
    {
        public string UserName { get; set; }
        public int UserAge { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Serialize example for testing
            var samplePerson = new Person { UserName = "Alice", UserAge = 30 };

            // Binary serialization
            using (var fs = new FileStream("person.dat", FileMode.Create))
            using (var writer = new BinaryWriter(fs))
            {
                writer.Write(samplePerson.UserName);
                writer.Write(samplePerson.UserAge);
            }
            Console.WriteLine("Binary serialization complete.");

            // Binary deserialization
            Person deserializedPerson;
            using (var fs = new FileStream("person.dat", FileMode.Open))
            using (var reader = new BinaryReader(fs))
            {
                deserializedPerson = new Person
                {
                    UserName = reader.ReadString(),
                    UserAge = reader.ReadInt32()
                };
            }

            Console.WriteLine($"Binary Deserialization - UserName: {deserializedPerson.UserName}, UserAge: {deserializedPerson.UserAge}");

            // Using XML serialization/deserialization for demonstration
            var xmlData = "<Person><UserName>Bob</UserName><UserAge>30</UserAge></Person>";
            var serializer = new XmlSerializer(typeof(Person));

            using (var reader = new StringReader(xmlData))
            {
                var xmlDeserializedPerson = (Person)serializer.Deserialize(reader);
                Console.WriteLine($"XML Deserialization - UserName: {xmlDeserializedPerson.UserName}, UserAge: {xmlDeserializedPerson.UserAge}");
            }

            // Using System.Text.Json for JSON serialization/deserialization
            var jsonData = "{\"Name\":\"John Doe\",\"Age\":30}";
            var person = JsonSerializer.Deserialize<Person>(jsonData);

            Console.WriteLine($"Name: {person.UserName}, Age: {person.UserAge}");


            // Data integrity check
            try
            {
                var jsonDataa = "{\"UserName\": \"Dana\"}";
                var deserializedPersonn = JsonSerializer.Deserialize<Person>(jsonDataa);

                if (string.IsNullOrEmpty(deserializedPersonn.UserName))
                    throw new Exception("UserName is required");

                Console.WriteLine("Data Integrity Verified");
                Console.WriteLine($"UserName: {deserializedPersonn.UserName}, UserAge: {deserializedPersonn.UserAge}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
            }
        }
    }
}
