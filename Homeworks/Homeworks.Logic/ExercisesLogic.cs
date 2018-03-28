using Homeworks.DataAccess;
using Homeworks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworks.Logic
{
    public class ExercisesLogic
    {
        private ExercisesRepository exercises = new ExercisesRepository();

        public Exercise GetById(Guid id)
        {
            return exercises.GetById(id);
        } 
    }
}
