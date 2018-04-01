using Homeworks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworks.DataAccess
{
    public class ExercisesRepository
    {
        public Exercise GetById(Guid id)
        {
            using (var context = new HomeworksContext())
            {
                return context.Exercises.FirstOrDefault(p => p.Id == id);
            }
        }
        public Exercise Add(Guid homeworkId, Exercise exercise)
        {
            using (var context = new HomeworksContext())
            {
                Homework homework = context.Homeworks.Include("Exercises").FirstOrDefault(h => h.Id == homeworkId);
                if(homework != null)
                {
                    homework.Exercises.Add(exercise);
                    context.SaveChanges();
                    return exercise;
                } else
                {
                    return null;
                }
             }
        }

       
    }
}
