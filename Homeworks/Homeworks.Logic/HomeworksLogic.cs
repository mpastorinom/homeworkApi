using Homeworks.DataAccess;
using Homeworks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworks.Logic
{
    public class HomeworksLogic
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

        public void Add(Homework homework)
        {
            homework.Id = Guid.NewGuid();
            homeworks.Add(homework);
        }

        public bool DeleteById(Guid id)
        {
            return homeworks.DeleteById(id);
        }

        public bool Update(Guid id, Homework updatedHomework)
        {
            return homeworks.Update(id, updatedHomework);
        }

        public bool AddExercise(Guid homeworkId, Exercise exercise)
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
