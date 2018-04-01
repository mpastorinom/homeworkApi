using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homeworks.WebApi.Models;
using Homeworks.Logic;

namespace Homeworks.WebApi.Controllers
{
    public class HomeworksController : ApiController
    {
        private HomeworksLogic homeworks = new HomeworksLogic();

        // GET: api/Homeworks
        public IHttpActionResult Get()
        {
            return Ok(homeworks.GetAll());
        }

        // GET: api/Homeworks/5
        public IHttpActionResult Get([FromUri] Guid id)
        {
            var homework = homeworks.GetById(id);
            if (homework == null)
            {
                return NotFound();
            }
            return Ok(homework);
        }

        // POST: api/Homeworks
        public IHttpActionResult Post([FromBody] Homework homework)
        {
            var newHomework = homeworks.Add(homework.toEntity());
            //DefaultApi hace referencia a la configuracion de App_Start/WebApiConfig.cs
            return CreatedAtRoute("DefaultApi", new { newHomework.Id }, newHomework);
        }

        // PUT: api/Homeworks/5
        public IHttpActionResult Put([FromUri] Guid id, [FromBody] Homework updatedHomework)
        {
            bool result = homeworks.Update(id, updatedHomework.toEntity());
            if(!result)
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE: api/Homeworks/5
        public IHttpActionResult Delete([FromUri] Guid id)
        {
            bool result = homeworks.DeleteById(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("api/homeworks/{homeworkId:guid}/exercises/{exerciseId:guid}", Name = "GetExerciseById")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] Guid homeworkId, [FromUri] Guid exerciseId)
        {
            var exercise = homeworks.GetExercise(homeworkId, exerciseId);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [Route("api/homeworks/{id:guid}/exercises")]
        [HttpPost]
        public IHttpActionResult Post([FromUri] Guid id, [FromBody] Exercise exercise)
        {
            var new_exercise = homeworks.AddExercise(id, exercise.toEntity());
            if (new_exercise == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("GetExerciseById", new { homeworkId = id, exerciseId = new_exercise.Id }, new_exercise);
            //Otra forma es devolver el Get del controlador por defecto de exercises si es que existe
            //return CreatedAtRoute("DefaultApi", new { controller = "exercises", id = exercise.Id }, exercise);
        }
    }
}

