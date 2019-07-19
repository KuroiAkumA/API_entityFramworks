using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.VewModels;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PruebaController : ApiController
    {
        [HttpGet]
        public REply helloWorld()
        {
            REply or = new REply();
            or.result = 1;
            or.message = "Hola Mundo";
            return or;
        }

        [HttpPost]
        public REply Login([FromBody] AccessModel model)
        {
            REply or = new REply();
            or.result = 0;
            try
            {
                using (DB_ViviLovelyNailsEntities db = new DB_ViviLovelyNailsEntities())
                {
                    var list = db.User.Where(d => d.email == model.email && d.password == model.pass && d.idEstatus == 1);
                    if (list.Count() > 0)
                    {
                        or.result = 1;
                        or.data = Guid.NewGuid().ToString();

                        User oUser = list.First();
                        oUser.token = (string)or.data;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        or.message = "Usuarios Incorrectos";
                    }
                }
            }
            catch (Exception ex)
            {
                or.message = "Ocurrio un error, lo estamos corrigiendo";
            }
            return or;
        }
    }
}
