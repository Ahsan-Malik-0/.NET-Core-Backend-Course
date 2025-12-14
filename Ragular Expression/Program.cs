// ^ - Starts with
// $ - Ends with
// [] - Range
// () - Group
// . - Single character once
// + - one or more characters in a row
// ? - optional preceding character match
// \ - escape character
// \n - New line
// \d - Digit
// \D - Non-digit
// \s - White space
// \S - non-white space
// \w - alphanumeric/underscore character (word chars)
// \W - non-word characters
// {x,y} - Repeat low (x) to high (y) (no "y" means at least x, no ",y" means that many)
// (x|y) - Alternative - x or y
// [^x] - Anything but x (where x is whatever character you want)




using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Ragular_Expression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string tostring = File.ReadAllText("txt.txt");

            string _pattern = @"\d{4}-?\d{7}";

            MatchCollection matchCollection = Regex.Matches(tostring, _pattern);

            foreach (Match match in matchCollection)
            {
                Console.WriteLine(match.Value);
            }

            //string pattern = @"(\s|^)Tim(\s|$)"; // Example pattern for SSN

            //Regex regex = new Regex(pattern, RegexOptions.Compiled);

            //Console.WriteLine("Tim Corey:" + Regex.IsMatch("Tim Corey", pattern));
            //Console.WriteLine("Timthy Corey:" + Regex.IsMatch("Tiothy Corey", pattern));
            //Console.WriteLine("Always Tim:" + Regex.IsMatch("Always Tim", pattern));
            //Console.WriteLine("I am Tim Corey:" + Regex.IsMatch("I am Tim Corey", pattern));

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            //for (int i = 0; i < 100000; i++)
            //{
            //    Regex.IsMatch("I am Tim Corey", pattern);
            //    //regex.IsMatch("I am Tim Corey");
            //}

            //stopwatch.Stop();

            //Console.WriteLine($"Elapsed Time: ms");
        }
    }
}
