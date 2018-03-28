using Homeworks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworks.DataAccess
{
    public class HomeworksRepository
    {

        public IEnumerable<Homework> GetAll()
        {
            using (var context = new HomeworksContext())
            {
                return context.Homeworks.Include("Exercises");
            }
        }

        public Homework GetById(Guid id)
        {
            using (var context = new HomeworksContext())
            {
                return context.Homeworks.Include("Exercises").FirstOrDefault(p => p.Id == id);
            }
        }

        public void Add(Homework homework)
        {
            using (var context = new HomeworksContext())
            {
                context.Homeworks.Add(homework);
                context.SaveChanges();
            }
        }

        public bool DeleteById(Guid id)
        {
            using (var context = new HomeworksContext())
            {
                Homework homework = context.Homeworks.FirstOrDefault(p => p.Id == id);
                if (homework == null)
                {
                    return false;
                }
                context.Homeworks.Remove(homework);
                context.SaveChanges();
                return true;
            }
        }

        public bool Update(Guid id, Homework updatedHomework)
        {
            using (var context = new HomeworksContext())
            {
                Homework originalHomework = context.Homeworks.FirstOrDefault(p => p.Id == id);
                if (originalHomework == null)
                {
                    return false;
                }
                originalHomework.Description = updatedHomework.Description;
                originalHomework.DueDate = updatedHomework.DueDate;

                context.SaveChanges();
                return true;
            }
        }

    }
}
