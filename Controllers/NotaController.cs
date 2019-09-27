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
    public class NotaController : ApiController
    {
        private DatabaseEntities _bd = new DatabaseEntities();

        // GET: api/notas
        [HttpGet]
        public IEnumerable<notas> GetAll()
        {
            return _bd.notas.Include(x => x.turmas).Include(x => x.alunos).AsEnumerable();
        }

        // GET: api/notas/5
        [ResponseType(typeof(notas))]
        public notas GetId(int id)
        {
            return _bd.notas.Include(x => x.turmas).Include(x => x.alunos).FirstOrDefault(a => a.id.Equals(id));
        }

        // POST: api/notas
        [ResponseType(typeof(notas))]
        public HttpResponseMessage Post(int id, notas notas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bd.notas.Add(notas);
                    _bd.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, notas);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = notas.id }));
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

        // PUT: api/notas/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put(int id, notas notas)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != notas.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bd.Entry(notas).State = EntityState.Modified;

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

        // DELETE: api/notas/5
        [ResponseType(typeof(notas))]
        public HttpResponseMessage Delete(int id)
        {
            notas notas = _bd.notas.Find(id);
            if (notas == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _bd.notas.Remove(notas);

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, notas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bd.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotasoExists(int id)
        {
            return _bd.notas.Count(e => e.id == id) > 0;
        }
    }
}
