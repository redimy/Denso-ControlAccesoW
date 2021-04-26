using Denso.ControlAcceso;
using Denso.ControlAcceso.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Media;

namespace Denso.ControlAcceso
{
    class DensoService
    {
        public static AccesoUsuario verificaAutorizacion(int clave)
        {
            AccesoUsuario resultado = new AccesoUsuario();
            using (admin_grumaEntities contexto = new admin_grumaEntities())
            {
                Participante persona = null;
                try
                {
                    persona = contexto.Participante.FirstOrDefault(p => p.id.Equals(clave));
                }
                catch (EntityCommandExecutionException ecee)
                {
                    persona = null;
                    resultado.mensaje = "Problema de conexión a internet";
                }
                catch (EntityException ee)
                {
                    persona = null;
                    resultado.mensaje = "Problema de conexión a internet";
                }

                if (persona != null)
                {
                    //resultado.persona = persona;

                    if (persona.activo.HasValue)
                    {
                        resultado.mensaje = "Usuario no autorizado";
                        if (persona.activo.Value)
                        {
                            resultado.autorizado = true;
                            resultado.mensaje = String.Empty;
                        }
                    }
                }
            }
            return resultado;
        }

        public static AccesoUsuario verificaAutorizacionLocal(int clave)
        {
            AccesoUsuario resultado = new AccesoUsuario();
            admin_grumaEntities dbContext = new admin_grumaEntities();
            ParticipanteLocal personaLocal = new ParticipanteLocal();
            Color colorVerde = (Color)ColorConverter.ConvertFromString("#02a950");

            try
            {
                using (var connection = dbContext.Database.Connection)
                {
                    DateTime dateNow = new DateTime();
                    dateNow = DateTime.Now;
                    DateTime tmp;
                    DateTime tmpCert;
                    int dateResult;
                    int dateResultCert;

                    DateTime dc3Tmp;

                    int dc3DateResult;



                    var empresaId = (from part in dbContext.Participante
                                     where part.id == clave
                                     select part.empresa_id).FirstOrDefault();

                    var empresaMasterId =
                              (from part in dbContext.Participante
                               join em in dbContext.Empresa on part.empresa_id equals em.id
                               where part.id == clave
                               select new
                               {
                                   em.empresaMasterId
                               }).FirstOrDefault();

                    var participante =
                              (from part in dbContext.Participante
                               join em in dbContext.Empresa on part.empresa_id equals em.id
                               where part.id == clave
                               select new
                               {
                                   partId = part.id,
                                   part.CURP,
                                   part.nombre,
                                   part.apellidoPaterno,
                                   part.apellidoMaterno,
                                   part.activo,
                                   part.foto,
                                   razonSocial = em.RazonSocial,
                                   part.credencial,
                                   part.estatus,
                                   part.empresa_id
                               }).ToList();

                    if (participante == null || empresaMasterId.empresaMasterId != 810)
                    {
                        resultado.mensaje = "Usuario no existe";

                        return resultado;
                    }

                    var empresaDocumentos =
                           (from docs in dbContext.EmpresaDocumentos
                            where docs.IdEmpresa == empresaId
                            select new
                            {
                                docs.IdDocumento,
                                docs.NombreDocumento,
                                docs.IdEmpresa,
                                docs.FechaVencimiento
                            }).ToList();

                    var participanteDocumentos =
                        (from docs in dbContext.ParticipanteDocumentos
                         where docs.IdParticipante == clave
                         select new
                         {
                             docs.IdDocumento,
                             docs.NombreDocumento,
                             docs.FechaVencimiento
                         }).ToList();




                    var dc3Certificados =
                           (from dc3Cert in dbContext.CursosExternos where dc3Cert.participante_id == clave
                            select dc3Cert
                        ).ToList();

                    var certificaciones = (from cert in dbContext.Participante_Certif
                                           where cert.participante_id == clave
                                           select cert).ToList();


                    connection.Open();
                    //var command = connection.CreateCommand();
                    //command.CommandText = "from part in dbContext.Participante where part.id == clave select part";
                    //var result = command.ExecuteNonQuery();


                    foreach (var user in participante)
                    {

                        personaLocal.CURP = user.CURP;
                        personaLocal.nombre = user.nombre;
                        personaLocal.apellidoPaterno = user.apellidoPaterno;
                        personaLocal.apellidoMaterno = user.apellidoMaterno;
                        personaLocal.nombreFoto = user.foto;
                        personaLocal.razonSocial = user.razonSocial;
                        personaLocal.credencial = user.credencial;
                        personaLocal.estatus = user.estatus;

                        if (personaLocal.fechaVencimiento != null)
                        {
                            tmp = DateTime.Parse(personaLocal.fechaVencimiento.ToString());
                            dateResult = DateTime.Compare(tmp, dateNow);
                        }
                        else
                        {
                            dateResult = DateTime.Compare(dateNow, dateNow);
                        }

                        Boolean bActivo = user.activo.Equals('0');

                        if (user.credencial != null && user.credencial == false)
                        {
                            resultado.autorizado = false;
                            resultado.mensajeInactivo = "Credencial inactiva.";
                        }
                        else if (user.estatus != "Activo")
                        {
                            resultado.autorizado = false;
                            resultado.mensajeInactivo = "Usuario inactivo.";
                        }
                        else
                        {
                            resultado.autorizado = true;
                            resultado.mensaje = "Usuario autorizado";
                        }
                        resultado.persona = personaLocal;
                    }

                    foreach (var dc3Cert in dc3Certificados)
                    {
                        dc3Certificados item = new dc3Certificados();
                        item.Name = dc3Cert.nombre;
                        item.Vencimiento = dc3Cert.fechaVencimiento;


                        if (dc3Cert.fechaVencimiento != null)
                        {
                            dc3Tmp = DateTime.Parse(dc3Cert.fechaVencimiento.ToString());
                            dc3DateResult = DateTime.Compare(dc3Tmp, dateNow);
                        }
                        else
                        {
                            dc3DateResult = DateTime.Compare(dateNow, dateNow);
                        }

                        if (dc3DateResult < 0)
                        {
                            item.Estatus = "Vencido";
                            resultado.autorizado = false;
                            item.pathFoto = "img/certificadoExpirado.png";
                        }
                        else
                        {
                            item.Estatus = "Vigente";
                            item.pathFoto = "img/certificadoVigente.png";
                        }

                        resultado._dc3Certificados.Add(item);
                    }

                    if (dc3Certificados.Count < 2)
                    {
                        resultado.autorizado = false;
                        resultado.mensajeInactivo = "Necesita minimo 1 Certificado DC3";
                    }

                    foreach (var cert in certificaciones)
                    {
                        Certificaciones item = new Certificaciones();
                        item.Name = cert.certificacionObtenida;
                        item.Vencimiento = cert.fechaValidez;


                        if (cert.fechaValidez != null)
                        {
                            tmpCert = DateTime.Parse(cert.fechaValidez.ToString());
                            dateResultCert = DateTime.Compare(tmpCert, dateNow);
                        }
                        else
                        {
                            dateResultCert = DateTime.Compare(dateNow, dateNow);
                        }

                        if (dateResultCert < 0)
                        {
                            item.Estatus = "Vencido";
                            resultado.autorizado = false;
                            item.pathFoto = "img/certificadoExpirado.png";
                        }
                        else
                        {
                            item.Estatus = "Vigente";
                            item.pathFoto = "img/certificadoVigente.png";
                        }

                        resultado._certificaciones.Add(item);
                    }


                    foreach (var doc in participanteDocumentos)
                    {
                        if (doc.FechaVencimiento != null)
                        {
                            tmp = DateTime.Parse(doc.FechaVencimiento.ToString());
                            dateResult = DateTime.Compare(tmp, dateNow);
                        }
                        else
                        {
                            dateResult = DateTime.Compare(dateNow, dateNow);
                        }

                        switch (doc.IdDocumento)
                        {
                            case 2:
                                if (dateResult < 0)
                                {
                                    personaLocal.PagoSUA = "Pago SUA Vencido";
                                    personaLocal.PagoSUAVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.PagoSUA = "Pago SUA";
                                }
                                break;
                            case 13:
                                if (dateResult < 0)
                                {
                                    personaLocal.ExamenMedico = "Examen Médico Vencido";
                                    personaLocal.ExamenMedicoVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.ExamenMedico = "Examen Médico";
                                }
                                break;
                            case 14:
                                if (dateResult < 0)
                                {
                                    personaLocal.CartaResponsiva = "Carta Responsiva vencido";
                                    personaLocal.CartaResponsivaVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.CartaResponsiva = "Carta Responsiva";
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    foreach (var emp in empresaDocumentos)
                    {
                        if (emp.FechaVencimiento != null)
                        {
                            tmp = DateTime.Parse(emp.FechaVencimiento.ToString());
                            dateResult = DateTime.Compare(tmp, dateNow);
                        }
                        else
                        {
                            dateResult = DateTime.Compare(dateNow, dateNow);
                        }

                        switch (emp.IdDocumento)
                        {
                            case 3:
                                if (dateResult < 0)
                                {
                                    personaLocal.PagoSUA = "Pago SUA Vencido";
                                    personaLocal.PagoSUAVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.PagoSUA = "Pago SUA";
                                }
                                break;
                            case 8:
                                if (dateResult < 0)
                                {
                                    personaLocal.ConstanciaAutoevaluacion = "Constancia Autoevaluación Vencido";
                                    personaLocal.ConstanciaAutoevaluacionVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.ConstanciaAutoevaluacion = "Constancia Autoevaluación";
                                }
                                break;
                            case 9:
                                if (dateResult < 0)
                                {
                                    personaLocal.ProgramaHidratacion = "Programa de Hidratación Vencido";
                                    personaLocal.ProgramaHidratacionVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.ProgramaHidratacion = "Programa de Hidratación";
                                }
                                break;
                            case 10:
                                if (dateResult < 0)
                                {
                                    personaLocal.PlanAccion = "Plan de Acción Vencido";
                                    personaLocal.PlanAccionVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.PlanAccion = "Plan de Acción";
                                }
                                break;
                            case 11:
                                if (dateResult < 0)
                                {
                                    personaLocal.CuestionarioSeguridad = "Cuestionario de Seguridad Vencido";
                                    personaLocal.CuestionarioSeguridadVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.CuestionarioSeguridad = "Cuestionario de Seguridad";
                                }
                                break;
                            case 12:
                                if (dateResult < 0)
                                {
                                    personaLocal.CartaResponsiva = "Carta Responsiva Vencido";
                                    personaLocal.CartaResponsivaVencido = true;
                                    resultado.autorizado = false;
                                }
                                else
                                {
                                    personaLocal.CartaResponsiva = "Carta Responsiva";
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                resultado.mensaje = "error: " + ex;
                return resultado;
            }
        }
    }
}
