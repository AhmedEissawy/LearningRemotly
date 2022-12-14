using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningRemotly.Models
{
    public class Teacher
    {

        public Teacher()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
