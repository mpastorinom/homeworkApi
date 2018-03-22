using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskExample.WebApi.Models
{
    public class Homework
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Score { get; set; }
        public List<Exercise> Exercises {get; set;}

        public Homework()
        {
            Exercises = new List<Exercise>();
        }
    }
}