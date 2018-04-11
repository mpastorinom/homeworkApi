using System;
using System.Collections.Generic;
using Homeworks.Entities;

namespace Homeworks.Logic
{
    public interface IHomeworksLogic
    {
        Homework Add(Homework homework);
        Exercise AddExercise(Guid homeworkId, Exercise exercise);
        bool DeleteById(Guid id);
        IEnumerable<Homework> GetAll();
        Homework GetById(Guid id);
        Exercise GetExercise(Guid homeworkId, Guid exerciseId);
        bool Update(Guid id, Homework updatedHomework);
    }
}