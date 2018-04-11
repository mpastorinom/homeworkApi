using System;

namespace Homeworks.WebApi.Models
{
    public class Exercise : Model<Entities.Exercise, Exercise>
    {
        public Guid Id { get; set; }
        public string Problem { get; set; }
        public int Score { get; set; }

        public Exercise() { }

        public Exercise(Entities.Exercise entity)
        {
            SetModel(entity);
        }

        public override Entities.Exercise ToEntity() => new Entities.Exercise()
        {
            Id = this.Id,
            Problem = this.Problem,
            Score = this.Score
        };

        protected override Exercise SetModel(Entities.Exercise entity)
        {
            Id = entity.Id;
            Problem = entity.Problem;
            Score = entity.Score;
            return this;
        }

        public override bool Equals(object obj)
        {
            var aExercise = obj as Exercise;
            if (aExercise == null)
                return false;
            return this.Id == aExercise.Id;
        }
    }
}