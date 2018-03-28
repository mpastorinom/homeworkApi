using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homeworks.WebApi.Models
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

        public Homework(Entities.Homework entity)
        {
            Id = entity.Id;
            Description = entity.Description;
            DueDate = entity.DueDate;
            Score = entity.Score;
            Exercises = entity.Exercises.ConvertAll(m => new Exercise(m));
        }

        public Entities.Homework toEntity()
        {
            return new Entities.Homework()
            {
                Id = this.Id,
                Description = this.Description,
                DueDate = this.DueDate,
                Exercises = this.Exercises.ConvertAll(m => m.toEntity()),
            };
        }
    }
}