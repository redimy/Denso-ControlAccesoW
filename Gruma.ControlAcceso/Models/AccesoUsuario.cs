using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graftech.ControlAcceso.Models
{
    class AccesoUsuario
    {
        public Boolean autorizado { get; set; }
        //public Participante persona { get; set; }
        public ParticipanteLocal persona { get; set; }
        public Boolean documentoVencido { get; set; }
        public String nombreDocumento { get; set; }
        public List<String> documentosVencidos { get; set; }
        public List<dc3Certificados> _dc3Certificados { get; set; }
        public List<Certificaciones> _certificaciones { get; set; }

        public String mensajeInactivo { get; set; }
        public String mensaje { get; set; }
        public String mensajeVencido { get; set; }

        public string nombreFoto { get; set; }


        public AccesoUsuario()
        {
            autorizado = false;
            documentoVencido = false;
            nombreDocumento = "";
            mensaje = "¡El usuario no existe!";
            mensajeVencido = "";
            mensajeInactivo = "";
            documentosVencidos = new List<string>();
            nombreFoto = "NO-USER.png";

            _dc3Certificados = new List<dc3Certificados>();
            _certificaciones = new List<Certificaciones>();

        }
    }

}
