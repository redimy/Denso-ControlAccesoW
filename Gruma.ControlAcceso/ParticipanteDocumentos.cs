//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ParticipanteDocumentos
    {
        public int Id { get; set; }
        public Nullable<int> IdParticipante { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string Documento { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
    
        public virtual Participante Participante { get; set; }
        public virtual Documentos Documentos { get; set; }
    }
}
