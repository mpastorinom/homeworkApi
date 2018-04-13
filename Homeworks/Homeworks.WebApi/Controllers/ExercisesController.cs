using Homeworks.Logic;
using Homeworks.WebApi.Models;
using System;
using System.Web.Http;

namespace Homeworks.WebApi.Controllers
{
    public class ExercisesController : ApiController
    {
        private ExercisesLogic exercises = new ExercisesLogic();

        public IHttpActionResult Get([FromUri] Guid id)
        {
            return Ok(Exercise.ToModel(exercises.GetById(id)));

            /*HttpRequestHeaders headers = this.Request.Headers;

            string token = string.Empty;

            if (headers.Contains("token"))
            {
                token = headers.GetValues("token").First();
            }

            if (token != "secreto")
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized to access: " + Id);
            
            //Ejemplo de como setear un header http, en este caso el cache control
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = new TimeSpan(1, 0, 0, 0)
            };

            return response;*/
        }
    }
}
