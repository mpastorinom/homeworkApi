using System;

namespace Homeworks.WebApi.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Problem { get; set; }
        public int Score { get; set; }

        public Exercise()
        {

        }

        public Exercise(Entities.Exercise entity)
        {
            Id = entity.Id;
            Problem = entity.Problem;
            Score = entity.Score;
        }

        public Entities.Exercise toEntity()
        {
            return new Entities.Exercise()
            {
                Id = this.Id,
                Problem = this.Problem,
                Score = this.Score
            };
        }

 
    }
}