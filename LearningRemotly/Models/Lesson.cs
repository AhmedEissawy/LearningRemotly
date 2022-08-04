using System;
using System.ComponentModel.DataAnnotations;

namespace LearningRemotly.Models
{
    public class Lesson
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime Creation_Date { get; set; }

        public string Url { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

    }
}
