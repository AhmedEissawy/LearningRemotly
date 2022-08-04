using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningRemotly.Models
{
    public class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Creation_Date { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
