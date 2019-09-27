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
    public class MatriculaController : ApiController
    {
        private DatabaseEntities _bd = new DatabaseEntities();

        // GET: api/matriculas
        [HttpGet]
        public IEnumerable<matriculas> GetAll()
        {
            return _bd.matriculas.Include(x => x.turmas).Include(x => x.alunos).AsEnumerable();
        }

        // GET: api/matriculas/5
        [ResponseType(typeof(matriculas))]
        public matriculas GetId(int id)
        {
            return _bd.matriculas.Include(x => x.turmas).Include(x => x.alunos).FirstOrDefault(a => a.id.Equals(id));
        }

        // POST: api/matriculas
        [ResponseType(typeof(matriculas))]
        public HttpResponseMessage Post(int id, matriculas matriculas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bd.matriculas.Add(matriculas);
                    _bd.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, matriculas);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = matriculas.id }));
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

        // PUT: api/matriculas/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put(int id, matriculas matriculas)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != matriculas.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bd.Entry(matriculas).State = EntityState.Modified;

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

        // DELETE: api/matriculas/5
        [ResponseType(typeof(matriculas))]
        public HttpResponseMessage Delete(int id)
        {
            matriculas matriculas = _bd.matriculas.Find(id);
            if (matriculas == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _bd.matriculas.Remove(matriculas);

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, matriculas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bd.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatriculasExists(int id)
        {
            return _bd.matriculas.Count(e => e.id == id) > 0;
        }
    }
}
