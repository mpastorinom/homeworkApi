using Homeworks.DataAccess;
using Homeworks.Entities;
using System;
using System.Collections.Generic;

namespace Homeworks.Logic
{
    public class HomeworksLogic : IHomeworksLogic
    {
        private HomeworksRepository homeworks = new HomeworksRepository();
        private ExercisesRepository exercises = new ExercisesRepository();

        public IEnumerable<Homework> GetAll()
        {
            return homeworks.GetAll();
        }

        public Homework GetById(Guid id)
        {
            return homeworks.GetById(id);
        }

        public Homework Add(Homework homework)
        {
            homework.Id = Guid.NewGuid();
            return homeworks.Add(homework);
        }

        public bool DeleteById(Guid id)
        {
            return homeworks.DeleteById(id);
        }

        public bool Update(Guid id, Homework updatedHomework)
        {
            return homeworks.Update(id, updatedHomework);
        }

        public Exercise AddExercise(Guid homeworkId, Exercise exercise)
        {
            exercise.Id = Guid.NewGuid();
            return exercises.Add(homeworkId, exercise);
        }

        public Exercise GetExercise(Guid homeworkId, Guid exerciseId)
        {
            var homework = homeworks.GetById(homeworkId);
            if(homework == null)
            {
                return null;
            }
            return homework.Exercises.Find(e => e.Id == exerciseId);
        }
    }
}
