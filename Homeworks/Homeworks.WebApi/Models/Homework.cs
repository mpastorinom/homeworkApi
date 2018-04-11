using System;
using System.Collections.Generic;

namespace Homeworks.WebApi.Models
{
    public class Homework : Model<Entities.Homework, Homework>
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
            SetModel(entity);
        }

        public override Entities.Homework ToEntity() => new Entities.Homework()
        {
            Id = this.Id,
            Description = this.Description,
            DueDate = this.DueDate,
            Exercises = this.Exercises.ConvertAll(m => m.ToEntity()),
        };

        protected override Homework SetModel(Entities.Homework entity)
        {
            Id = entity.Id;
            Description = entity.Description;
            DueDate = entity.DueDate;
            Score = entity.Score;
            Exercises = entity.Exercises.ConvertAll(m => new Exercise(m));
            return this;
        }
    }
}