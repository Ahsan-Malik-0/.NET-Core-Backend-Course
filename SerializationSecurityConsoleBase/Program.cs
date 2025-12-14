using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SerializationSecurityConsoleBase
{
    class Person
    {
        public string Name { get; set; }
        public string email { get; set; }
        public string password { get; set; }


        public string GenerateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(ToString()));
                return Convert.ToBase64String(hashBytes);
            }
        }
        public Person EncriptData()
        {
            // Simple encryption logic (for demonstration purposes only)
            password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            return this;
        }
    }
    internal class Program
    {
        public static string SerializreUserData(Person person)
        {
            if(string.IsNullOrWhiteSpace(person.Name) || 
                string.IsNullOrWhiteSpace(person.email) || 
                string.IsNullOrWhiteSpace(person.password)
            )
            {
                Console.WriteLine("Invalid data");
                return string.Empty;
            }

            person = person.EncriptData();
            return JsonSerializer.Serialize(person);
            
        }

        public static Person DecryptData(string encryptedPassword, bool isTrusted)
        {
            if (!isTrusted)
            {
                Console.WriteLine("Untrusted source. Decryption aborted.");
                return null;
            }

            return JsonSerializer.Deserialize<Person>(encryptedPassword);
        }
        static void Main(string[] args)
        {
            Person person = new Person
            {
                Name = "John Doe",
                email = "jhon@gmail.com",
                password = "P@ssw0rd"
            };
            
            string hash = person.GenerateHash();
            string serializedData = SerializreUserData(person);
            Person deserializedPerson = DecryptData(serializedData, true);
            Console.WriteLine($"Hash: {hash}");
            Console.WriteLine($"Serialized Data: {serializedData}");
            Console.WriteLine($"Deserialized Person: Name={deserializedPerson.Name}, Email={deserializedPerson.email}, Password={deserializedPerson.password}");
        }
    }
}
