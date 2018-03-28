using Homeworks.DataAccess;
using Homeworks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworksLogic
{
    public class HomeworksLogic
    {
        private HomeworksRepository homeworks; 

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
    }
}
