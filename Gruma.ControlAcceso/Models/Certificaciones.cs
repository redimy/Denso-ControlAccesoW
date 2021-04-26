using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.ControlAcceso.Models
{
    public class Certificaciones
    {
        public string Name { get; set; }
        public DateTime? Vencimiento { get; set; }
        public string Estatus { get; set; }
        public string pathFoto { get; set; }

    }
}
