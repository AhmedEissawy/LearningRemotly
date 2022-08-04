using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningRemotly.Models
{
    public class Course
    {

        public Course()
        {
            Lessons = new HashSet<Lesson>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        [Required]
        public DateTime Creation_Date { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }


        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}
