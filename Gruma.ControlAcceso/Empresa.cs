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
    
    public partial class Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresa()
        {
            this.Participante = new HashSet<Participante>();
            this.EmpresaDocumentos = new HashSet<EmpresaDocumentos>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string usuarioCanvas { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string RFC { get; set; }
        public string nombreFactura { get; set; }
        public string direccionFactura { get; set; }
        public Nullable<int> numeroProveedor { get; set; }
        public string nombreResponsable { get; set; }
        public string apPatResponsable { get; set; }
        public string apMatResponsable { get; set; }
        public string puestoResponsable { get; set; }
        public string telOficinaResponsable { get; set; }
        public string telCelularResponsable { get; set; }
        public string correoResponsable { get; set; }
        public string correoResponsable2 { get; set; }
        public string telefonoFactura { get; set; }
        public string correoFactura { get; set; }
        public bool activa { get; set; }
        public string contrasenaCanvas { get; set; }
        public string observaciones { get; set; }
        public string Domicilio { get; set; }
        public string RazonSocial { get; set; }
        public string docSAT { get; set; }
        public string docCedula { get; set; }
        public string docPagoIMSS { get; set; }
        public Nullable<System.DateTime> vigenciaPagoIMSS { get; set; }
        public string docIdentificacion { get; set; }
        public Nullable<int> EmpresaMaestra { get; set; }
        public Nullable<int> RepresentanteLegal { get; set; }
        public string Calle { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string codigoPostal { get; set; }
        public string CalleFac { get; set; }
        public string NoExtFac { get; set; }
        public string NoIntFac { get; set; }
        public string ColoniaFac { get; set; }
        public string CiudadFac { get; set; }
        public string EstadoFac { get; set; }
        public string PaisFac { get; set; }
        public string codigoPostalFac { get; set; }
        public Nullable<int> empresaMasterId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Participante> Participante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpresaDocumentos> EmpresaDocumentos { get; set; }
    }
}
