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
    public class TurmaController : ApiController
    {
        private DatabaseEntities _bd = new DatabaseEntities();

        // GET: api/turmas
        [HttpGet]
        public IEnumerable<turmas> GetAll()
        {
            return _bd.turmas.Include(x => x.cursos).Include(x => x.matriculas).Include(x => x.notas).Include(x => x.professores).AsEnumerable();
        }

        // GET: api/turmas/5
        [ResponseType(typeof(turmas))]
        public turmas GetId(int id)
        {
            return _bd.turmas.Include(x => x.cursos).Include(x => x.matriculas).Include(x => x.notas).Include(x => x.professores).FirstOrDefault(a => a.id.Equals(id));
        }

        // POST: api/turmas
        [ResponseType(typeof(turmas))]
        public HttpResponseMessage Post(int id, turmas turmas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bd.turmas.Add(turmas);
                    _bd.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, turmas);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = turmas.id }));
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

        // PUT: api/turmas/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put(int id, turmas turmas)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != turmas.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bd.Entry(turmas).State = EntityState.Modified;

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

        // DELETE: api/turmas/5
        [ResponseType(typeof(turmas))]
        public HttpResponseMessage Delete(int id)
        {
            turmas turmas = _bd.turmas.Find(id);
            if (turmas == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _bd.turmas.Remove(turmas);

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, turmas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bd.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TurmasExists(int id)
        {
            return _bd.turmas.Count(e => e.id == id) > 0;
        }
    }
}
