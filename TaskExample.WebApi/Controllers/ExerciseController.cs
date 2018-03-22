using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TaskExample.WebApi.Controllers
{
    public class ExerciseController : ApiController
    {
        // GET: api/Exercise
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        public HttpResponseMessage Get([FromUri] Guid Id)
        {
            HttpRequestHeaders headers = this.Request.Headers;

            //Token en header
            string token = string.Empty;
            string pwd = string.Empty;

            if (headers.Contains("token"))
            {
                token = headers.GetValues("token").First();
            }

            if(token != "secreto")
            {
                string sessionId = "";
                string sessionToken = "";
                string theme = "";

                CookieHeaderValue requestCookie = Request.Headers.GetCookies("session").FirstOrDefault();
                if (requestCookie != null)
                {
                    CookieState cookieState = requestCookie["session"];

                    sessionId = cookieState["sid"];
                    sessionToken = cookieState["token"];
                    theme = cookieState["theme"];

                    if(sessionId != "12345")
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

            //Respuesta seteando cache control
            var response = Request.CreateResponse(HttpStatusCode.OK, "value: " + Id);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = new TimeSpan(1, 0, 0, 0)
            };

            //Creo una cookie con un diccionario
            var cookieData = new NameValueCollection();
            cookieData["sid"] = "12345";
            cookieData["token"] = "abcdef";
            cookieData["theme"] = "dark blue";
            var cookie = new CookieHeaderValue("session", cookieData);
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            return response;
        }



    }
}
