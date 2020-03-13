using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileName = "dane.csv";
                var path = @"C:\" + fileName;
                var wynikName = "wynik.xml";
                var wynik = @"C:\" + wynikName;
                string expectedFormat = "xml";

                var lines = File.ReadAllLines(path);

                HashSet<Student> students = new HashSet<Student>(new Comparator());

                var logFileName = "log.txt";
                StreamWriter streamWriter = new StreamWriter(@"C:\" + logFileName);

                var todaysDate = DateTime.Today;

                foreach (var line in lines)
                {
                    string[] student = line.Split(",");

                    if (student.Length == 9)
                    {
                        var stud = new Student
                        {
                            name = student[0], surname = student[1], degreeName = student[2], studyMode = student[3], index = student[4], date = student[5], email = student[6], mName = student[7], fName = student[8]
                        };
                        students.Add(stud);
                    }

                    else if (student.Length != 9)
                    {
                        Console.WriteLine("zapisano niepelne dane do pliku: " + logFileName);
                        streamWriter.WriteLine("Dane sa niepelne: " + student[0] + " " + student[1]);
                    }

                }

                if (expectedFormat.Equals("xml"))
                {
                    XDocument xmlFile = new XDocument(new XElement("uczelnia",
                        new XAttribute("createdAt", todaysDate),
                         new XAttribute("author", "Dominik Kabala"),

                        new XElement("studenci",
                            from student in students
                            select new XElement("student",
                                new XAttribute("indexNumber", student.index),
                                new XElement("fname", student.name),
                                new XElement("lname", student.surname),
                                new XElement("birthdate", student.date),
                                new XElement("email", student.email),
                                new XElement("mothersName", student.mName),
                                new XElement("fathersName", student.fName),
                                new XElement("studies",
                                    new XElement("name", student.degreeName),
                                    new XElement("mode", student.studyMode)
                                )
                            )
                        )));

                    xmlFile.Save(wynik + ".xml");
                    
                }
                else if (expectedFormat.Equals("JSON"))
                {
                    Console.WriteLine("Implementacja wkrotce");
                }
                else
                {
                    Console.WriteLine("Podany format jest bledny");
                }

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex + " Bledna sciezka");
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex + " Plik nie istnieje");
            }
            
        }
    }
}
