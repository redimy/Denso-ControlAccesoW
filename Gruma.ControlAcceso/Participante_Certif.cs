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
    
    public partial class Participante_Certif
    {
        public int id { get; set; }
        public int participante_id { get; set; }
        public Nullable<System.DateTime> fechaExamen { get; set; }
        public string salaExamen { get; set; }
        public string certificacionObtenida { get; set; }
        public Nullable<int> calificacion { get; set; }
        public Nullable<System.DateTime> fechaValidez { get; set; }
    
        public virtual Participante Participante { get; set; }
    }
}
