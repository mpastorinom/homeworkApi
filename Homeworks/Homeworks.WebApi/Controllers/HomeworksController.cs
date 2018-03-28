using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homeworks.WebApi.Models;

namespace Homeworks.WebApi.Controllers
{
    public class HomeworksController : ApiController
    {
        //Lista temporal en memoria estática a modo ejemplo, se reemplazaría por una llamada a la lógica de negocio
        private static List<Homework> homeworks = new List<Homework>();

        // GET: api/Homeworks
        public IHttpActionResult Get()
        {
            return Ok(homeworks);
        }

        // GET: api/Homeworks/5
        public IHttpActionResult Get([FromUri] Guid id)
        {
            var homework = homeworks.Find(h => h.Id == id);
            if (homework == null)
            {
                return NotFound();
            }
            return Ok(homework);
        }

        // POST: api/Homeworks
        public IHttpActionResult Post([FromBody] Homework homework)
        {
            homework.Id = Guid.NewGuid();
            homeworks.Add(homework);
            //DefaultApi hace referencia a la configuracion de App_Start/WebApiConfig.cs
            return CreatedAtRoute("DefaultApi", new { homework.Id }, homework);
        }

        // PUT: api/Homeworks/5
        public IHttpActionResult Put([FromUri] Guid id, [FromBody] Homework updatedHomework)
        {
            var homework = homeworks.Find(h => h.Id == id);
            if (homework == null)
            {
                return NotFound();
            }
            updatedHomework.Id = id;
            homeworks.Remove(homework);
            homeworks.Add(updatedHomework);

            return Ok();
        }

        // DELETE: api/Homeworks/5
        public IHttpActionResult Delete([FromUri] Guid id)
        {
            var homework = homeworks.Find(h => h.Id == id);
            if (homework == null)
            {
                return NotFound();
            }
            homeworks.Remove(homework);

            return Ok();
        }

        [Route("api/homeworks/{homeworkId:guid}/exercises/{exerciseId:guid}", Name = "GetExerciseById")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] Guid homeworkId, [FromUri] Guid exerciseId)
        {
            var homework = homeworks.Find(h => h.Id == homeworkId);
            if (homework == null)
            {
                return NotFound();
            }
            var exercise = homework.Exercises.Find(e => e.Id == exerciseId);
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
            exercise.Id = Guid.NewGuid();
            var homework = homeworks.Find(h => h.Id == id);
            if (homework == null)
            {
                return NotFound();
            }
            homework.Exercises.Add(exercise);
            return CreatedAtRoute("GetExerciseById", new { homeworkId = id, exerciseId = exercise.Id }, exercise);
            //Otra forma es devolver el Get del controlador por defecto de exercises si es que existe
            //return CreatedAtRoute("DefaultApi", new { controller = "exercises", id = exercise.Id }, exercise);
        }
    }
}

