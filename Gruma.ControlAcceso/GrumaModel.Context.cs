﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Graftech.ControlAcceso
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class admin_grumaEntities : DbContext
    {
        public admin_grumaEntities()
            : base("name=admin_grumaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<CursosExternos> CursosExternos { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<estatus> estatus { get; set; }
        public virtual DbSet<Participante> Participante { get; set; }
        public virtual DbSet<Participante_Certif> Participante_Certif { get; set; }
        public virtual DbSet<Participante_Curso> Participante_Curso { get; set; }
        public virtual DbSet<ParticipanteDocumentos> ParticipanteDocumentos { get; set; }
        public virtual DbSet<EmpresaDocumentos> EmpresaDocumentos { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<DocumentosEmpresa> DocumentosEmpresa { get; set; }
    }
}