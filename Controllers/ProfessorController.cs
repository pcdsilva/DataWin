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
    public class ProfessorController : ApiController
    {
        private DatabaseEntities _bd = new DatabaseEntities();

        // GET: api/professores
        [HttpGet]
        public IEnumerable<professores> GetAll()
        {
            return _bd.professores.Include(x => x.turmas).AsEnumerable();
        }

        // GET: api/professores/5
        [ResponseType(typeof(professores))]
        public professores GetId(int id)
        {
            return _bd.professores.Include(x => x.turmas).FirstOrDefault(a => a.id.Equals(id));
        }

        // POST: api/professores
        [ResponseType(typeof(professores))]
        public HttpResponseMessage Post(int id, professores professores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bd.professores.Add(professores);
                    _bd.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, professores);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = professores.id }));
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

        // PUT: api/professores/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put(int id, professores professores)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != professores.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bd.Entry(professores).State = EntityState.Modified;

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

        // DELETE: api/professores/5
        [ResponseType(typeof(professores))]
        public HttpResponseMessage Delete(int id)
        {
            professores professores = _bd.professores.Find(id);
            if (professores == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _bd.professores.Remove(professores);

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, professores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bd.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfessoresExists(int id)
        {
            return _bd.professores.Count(e => e.id == id) > 0;
        }
    }
}
