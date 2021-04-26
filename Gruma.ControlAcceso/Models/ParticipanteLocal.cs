using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.ControlAcceso.Models
{
    class ParticipanteLocal
    {
        public string CURP { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public string nombreDocumento { get; set; }
        public string nombreFoto { get; set; }
        public string razonSocial { get; set; }
        public bool? credencial { get; set; }
        public string estatus { get; set; }

        public string PagoSUA { get; set; }
        public string ExamenMedico { get; set; }
        public string CartaResponsiva { get; set; }
        public string ConstanciaAutoevaluacion { get; set; }
        public string ProgramaHidratacion { get; set; }
        public string PlanAccion { get; set; }
        public string CuestionarioSeguridad { get; set; }
        public string Certificacion1 { get; set; }
        public string Certificacion2 { get; set; }
        //Vencimiento
        public bool PagoSUAVencido { get; set; }
        public bool ExamenMedicoVencido { get; set; }
        public bool CartaResponsivaVencido { get; set; }
        public bool ConstanciaAutoevaluacionVencido { get; set; }
        public bool ProgramaHidratacionVencido { get; set; }
        public bool PlanAccionVencido { get; set; }
        public bool CuestionarioSeguridadVencido { get; set; }
    }

}
