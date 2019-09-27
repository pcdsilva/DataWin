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
    public class AlunoController : ApiController
    {
        private DatabaseEntities _bd = new DatabaseEntities();
        // GET: api/Aluno
        [HttpGet]
        public IEnumerable<alunos> GetAll()
        {
            //return bd.alunos.AsEnumerable();
            return _bd.alunos.Include(x => x.matriculas).Include(x => x.notas).ToArray();
        }

        // GET: api/Aluno/5
        [ResponseType(typeof(alunos))]
        public alunos GetId(int id)
        {
            return _bd.alunos.Include(x => x.matriculas).Include(x => x.notas).FirstOrDefault(a => a.id.Equals(id));
        }

        // POST: api/Aluno
        [ResponseType(typeof(alunos))]
        public HttpResponseMessage Post(int id, alunos aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bd.alunos.Add(aluno);
                    _bd.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, aluno);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = aluno.id }));
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

        // PUT: api/Aluno/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put(int id, alunos aluno)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != aluno.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bd.Entry(aluno).State = EntityState.Modified;

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

        // DELETE: api/Aluno/5
        [ResponseType(typeof(alunos))]
        public HttpResponseMessage Delete(int id)
        {
            alunos aluno = _bd.alunos.Find(id);
            if (aluno == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _bd.alunos.Remove(aluno);

            try
            {
                _bd.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, aluno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bd.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlunoExists(int id)
        {
             return _bd.alunos.Count(e => e.id == id) > 0;
        }
    }
}
