﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiDataWin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<alunos> alunos { get; set; }
        public virtual DbSet<cursos> cursos { get; set; }
        public virtual DbSet<matriculas> matriculas { get; set; }
        public virtual DbSet<notas> notas { get; set; }
        public virtual DbSet<professores> professores { get; set; }
        public virtual DbSet<turmas> turmas { get; set; }
    }
}
