using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningRemotly.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Register_Date { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

    }
}
