using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadBinFile
{
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AverageScore { get; set; }

        public Student(string name, string group, DateTime dateTime, decimal averag)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateTime;
            AverageScore = averag;
        }
    }
}
