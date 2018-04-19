using System;
using System.Web.Http;
using Homeworks.WebApi.Models;
using Homeworks.Logic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Homeworks.WebApi.Controllers
{
    public class HomeworksController : ApiController
    {
        private IHomeworksLogic homeworks;

        public HomeworksController()
        {
            homeworks = new HomeworksLogic();
        }

        public HomeworksController(IHomeworksLogic aHomeworksLogic)
        {
            homeworks = aHomeworksLogic;
        }

        // GET: api/Homeworks
        public IHttpActionResult Get()
        {
            return Ok(Homework.ToModel(homeworks.GetAll()));
        }

        // GET: api/Homeworks/5
        public IHttpActionResult Get([FromUri] Guid id)
        {
            var homework = homeworks.GetById(id);
            if (homework == null)
            {
                return NotFound();
            }
            return Ok(Homework.ToModel(homework));
        }

        // POST: api/Homeworks
        public IHttpActionResult Post([FromBody] Homework homework)
        {
            try
            {
                var newHomework = homeworks.Add(Homework.ToEntity(homework));
                var modelNewHomework = Homework.ToModel(newHomework);
                //DefaultApi hace referencia a la configuracion de App_Start/WebApiConfig.cs
                return CreatedAtRoute("DefaultApi", new { modelNewHomework.Id }, modelNewHomework);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Homeworks/5
        public IHttpActionResult Put([FromUri] Guid id, [FromBody] Homework updatedHomework)
        {
            bool result = homeworks.Update(id, updatedHomework.ToEntity());
            if (!result)
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
            return Ok(Exercise.ToModel(exercise));
        }

        [Route("api/homeworks/{id:guid}/exercises")]
        [HttpPost]
        public IHttpActionResult Post([FromUri] Guid id, [FromBody] Exercise exercise)
        {
            var newExercise = homeworks.AddExercise(id, exercise.ToEntity());
            if (newExercise == null)
            {
                return NotFound();
            }
            var modelNewExercise = Exercise.ToModel(newExercise);
            return CreatedAtRoute("GetExerciseById", new { homeworkId = id, exerciseId = modelNewExercise.Id }, modelNewExercise);
            //Otra forma es devolver el Get del controlador por defecto de exercises si es que existe
            //return CreatedAtRoute("DefaultApi", new { controller = "exercises", id = exercise.Id }, exercise);
        }

        [HttpGet]
        //TAREAS SOLO PARA ADMINISTRADORES
        public IHttpActionResult GetAllRestrictedHomeworks()
        {
            var authorization = Request.Headers.Authorization;
            var key = authorization == null ? string.Empty : authorization.Scheme;
            if (key == "admin")
            {
                return Ok(Homework.ToModel(homeworks.GetAll()));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("api/homeworks/formatted")]
        public HttpResponseMessage GetFormatted([FromUri] FormatModel model)
        {
            var response = new HttpResponseMessage();
            var accept = Request.Headers.Accept;
            var key = accept == null ? string.Empty : accept.ToString();
            if (key.Contains("text/html"))
            {
                response.Content = new StringContent(GetHTML(model));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                response.StatusCode = HttpStatusCode.OK;
          
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotAcceptable;
            }
            return response;
        }

        private string GetHTML(FormatModel model)
        {
            string html = "<html><body>{0}</body></html>";
            string body = "<div style='color:{0}; background-color:{1}; font-size:{2}'>{3}</div>";
            return string.Format(html, string.Format(body, model.Color, model.Background, model.FontSize, model.Text));
        }

    }
}

