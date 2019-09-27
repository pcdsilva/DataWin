using ApiDataWin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiDataWin.Controllers
{
    public class CursoController : ApiController
    {
        private DatabaseEntities _bd = new DatabaseEntities();

        // GET: api/cursos
        [HttpGet]
        public IEnumerable<cursos> GetAll()
        {
            return _bd.cursos.Include(x => x.turmas).ToArray(); ;
        }

        // GET: api/cursos/5
        [ResponseType(typeof(cursos))]
        public cursos GetId(int id)
        {
            return _bd.cursos.Include(x => x.turmas).FirstOrDefault(a => a.id.Equals(id));
        }

        // POST: api/cursos
        [ResponseType(typeof(cursos))]
        public HttpResponseMessage Post(int id, cursos cursos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bd.cursos.Add(cursos);
                    _bd.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cursos);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cursos.id }));
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/cursos/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put(int id, cursos cursos)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != cursos.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bd.Entry(cursos).State = EntityState.Modified;

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/cursos/5
        [ResponseType(typeof(cursos))]
        public HttpResponseMessage Delete(int id)
        {
            cursos cursos = _bd.cursos.Find(id);
            if (cursos == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _bd.cursos.Remove(cursos);

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cursos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bd.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursosExists(int id)
        {
            return _bd.cursos.Count(e => e.id == id) > 0;
        }
    }
}
