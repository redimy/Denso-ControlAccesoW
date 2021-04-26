using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.Entity.Core;
using System.IO;
using Denso.ControlAcceso;
using Denso.ControlAcceso.Models;

namespace Denso.ControlAcceso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string clave2;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(MainWindow_KeyUp);
            this.PreviewTextInput += MainWindow_PreviewTextInput;
            using (admin_grumaEntities context = new admin_grumaEntities())
            {
                try
                {
                    context.estatus.FirstOrDefault();
                }
                catch (EntityException ee) {
                    this.MensajeInicio.Content = "No conexión a internet";
                }
            }
        }

        private void limpiarIndicador()
        {
            Color colorAmarillo = (Color)ColorConverter.ConvertFromString("#fff200");
            Color colorRojo = Colors.Red;

            List<string> vaciarList = new List<string>();
            this.MensajeInicio.Content = "Obteniendo información...";


            this.PagoSUA.Content = String.Empty;
            this.ExamenMedico.Content = String.Empty;
            this.CartaResponsiva.Content = String.Empty;
            this.ConstanciaAutoevaluacion.Content = String.Empty;
            this.ProgramaHidratacion.Content = String.Empty;
            this.PlanAccion.Content = String.Empty;
            this.CuestionarioSeguridad.Content = String.Empty;

            this.PagoSUA.Foreground = new SolidColorBrush(colorRojo); 
            this.ExamenMedico.Foreground = new SolidColorBrush(colorRojo); 
            this.CartaResponsiva.Foreground = new SolidColorBrush(colorRojo); 
            this.ConstanciaAutoevaluacion.Foreground = new SolidColorBrush(colorRojo); 
            this.ProgramaHidratacion.Foreground = new SolidColorBrush(colorRojo); 
            this.PlanAccion.Foreground = new SolidColorBrush(colorRojo); 
            this.CuestionarioSeguridad.Foreground = new SolidColorBrush(colorRojo); 



            this.SignalPase.Fill = new SolidColorBrush(colorAmarillo);
            this.Resultado.Content = String.Empty;

            dc3List.ItemsSource = vaciarList;
            certificacionesList.ItemsSource = vaciarList;

        }

        private void limpiarNombre()
        {
            this.DatosIdentificador.Content = String.Empty;
            this.DatosNombre.Content = String.Empty;
            this.DatosApellido.Content = String.Empty;
            this.DatosTitulo.Content = String.Empty;
            DatosRazonSocial.Content = String.Empty;
            this.DatosFoto.Source = new BitmapImage(new Uri("pack://application:,,,/NO-USER.png"));
        }

        private void MainWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                limpiarIndicador();
                limpiarNombre();
          
                _barcode.Add(e.Text[0]);
            }
            catch (IndexOutOfRangeException iore)
            {
                this.MensajeInicio.Content = "Formato de info inválido";
            }
        }

        List<char> _barcode = new List<char>(1);

        private AccesoUsuario validaAcceso(int iClave)
        {
            //return GrumaService.verificaAutorizacion(iClave);
            return DensoService.verificaAutorizacionLocal(iClave);
        }

        private String formateoClave(String clave)
        {
            String resultado = clave.Replace("\r", String.Empty).PadLeft(9, '0');
            return resultado;
        }


        private String urlImagenWeb(String clave,String nombreFoto)
        {
            String sSistema = new String(ConfigurationManager.AppSettings["sistema"].Reverse().ToArray());
            String sUrlImagen = String.Format("https://{0}", sSistema);

            String resultado = String.Format("{0}/Uploads/Fotos/"+nombreFoto,
                sUrlImagen
            );

            //String resultado = String.Format("{0}/Uploads/Fotos/" + nombreFoto,
            //    sUrlImagen
            //).Replace("\r", String.Empty);

            return resultado;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            List<dc3Certificados> dc3ListLocal = new List<dc3Certificados>();
            List<Certificaciones> certificacionesLocal = new List<Certificaciones>();


            // Barcode scanner hits Enter/Return after reading barcode
            if (e.Key == Key.Return && _barcode.Count > 0)
            {
                string BarCodeData = new String(_barcode.ToArray());

                String sSistema = new String(ConfigurationManager.AppSettings["sistema"].Reverse().ToArray());

                string clave = BarCodeData.Split('-').LastOrDefault();
                clave = clave.Replace("\r", "");

                if (clave != "")
                {
                    clave2 = clave;
                }

                _barcode.Clear();
                limpiarIndicador();
                limpiarNombre();

                int iClave = 0;
                try
                {
                    if (clave == "")
                    {
                        clave = clave2;
                    }

                    iClave = Convert.ToInt32(clave);

                }
                catch (Exception ex)
                {
                    this.MensajeInicio.Content = "Formato de clave inválido";
                    return;
                }

                AccesoUsuario verifica = validaAcceso(iClave);

                this.MensajeInicio.Content = String.Empty;
                this.DatosFoto.Source = new BitmapImage(new Uri("pack://application:,,,/NO-USER.png"));

                Color colorPintado = Colors.Red;
                Color colorRojo = Colors.Red;
                Color colorVerde = (Color)ColorConverter.ConvertFromString("#02a950");


                String sResolucion = "ALTO";

                foreach(var item in verifica._dc3Certificados)
                {
                    dc3ListLocal.Add(item);
                }
                dc3List.ItemsSource = dc3ListLocal;

                foreach (var item in verifica._certificaciones)
                {
                    certificacionesLocal.Add(item);
                }
                certificacionesList.ItemsSource = certificacionesLocal;

                try
                {
                    string sImagenLocation;
                    if(verifica.persona != null)
                    {
                         sImagenLocation = urlImagenWeb(clave, verifica.persona.nombreFoto);
                    }
                    else
                    {
                        sImagenLocation = "";
                    }
                    if (!File.Exists(sImagenLocation))
                    {
                        this.DatosFoto.Source = new BitmapImage(new Uri(sImagenLocation));
                    }

                    if (verifica.persona.PagoSUA != null && !verifica.persona.PagoSUAVencido)
                    {
                        this.PagoSUA.Content = verifica.persona.PagoSUA;
                        this.PagoSUA.Foreground = new SolidColorBrush(colorVerde);

                    }
                    else if (verifica.persona.PagoSUAVencido)
                    {
                        this.PagoSUA.Content = verifica.persona.PagoSUA;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.PagoSUA.Content = "Pago SUA";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;
                    }

                    if (verifica.persona.ExamenMedico != null && !verifica.persona.ExamenMedicoVencido)
                    {
                        this.ExamenMedico.Content = verifica.persona.ExamenMedico;
                        this.ExamenMedico.Foreground = new SolidColorBrush(colorVerde);
                    }
                    else if (verifica.persona.ExamenMedicoVencido)
                    {
                        this.ExamenMedico.Content = verifica.persona.ExamenMedico;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.ExamenMedico.Content = "Examen Médico";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;
                    }

                    if (verifica.persona.CartaResponsiva != null && !verifica.persona.CartaResponsivaVencido)
                    {
                        this.CartaResponsiva.Content = verifica.persona.CartaResponsiva;
                        this.CartaResponsiva.Foreground = new SolidColorBrush(colorVerde);
                    }
                    else if (verifica.persona.CartaResponsivaVencido)
                    {
                        this.CartaResponsiva.Content = verifica.persona.CartaResponsiva;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.CartaResponsiva.Content = "Carta Responsiva";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }

                    if (verifica.persona.ConstanciaAutoevaluacion != null && !verifica.persona.ConstanciaAutoevaluacionVencido)
                    {
                        this.ConstanciaAutoevaluacion.Content = verifica.persona.ConstanciaAutoevaluacion;
                        this.ConstanciaAutoevaluacion.Foreground = new SolidColorBrush(colorVerde);
                    }
                    else if (verifica.persona.ConstanciaAutoevaluacionVencido)
                    {
                        this.ConstanciaAutoevaluacion.Content = verifica.persona.ConstanciaAutoevaluacion;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.ConstanciaAutoevaluacion.Content = "Constancia Autoevaluación";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }

                    if (verifica.persona.ProgramaHidratacion != null && !verifica.persona.ProgramaHidratacionVencido)
                    {
                        this.ProgramaHidratacion.Content = verifica.persona.ProgramaHidratacion;
                        this.ProgramaHidratacion.Foreground = new SolidColorBrush(colorVerde);
                    }
                    else if (verifica.persona.ProgramaHidratacionVencido)
                    {
                        this.ProgramaHidratacion.Content = verifica.persona.ProgramaHidratacion;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.ProgramaHidratacion.Content = "Programa de Hidratación";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }

                    if (verifica.persona.PlanAccion != null && !verifica.persona.PlanAccionVencido)
                    {
                        this.PlanAccion.Content = verifica.persona.PlanAccion;
                        this.PlanAccion.Foreground = new SolidColorBrush(colorVerde);
                    }
                    else if (verifica.persona.PlanAccionVencido)
                    {
                        this.PlanAccion.Content = verifica.persona.PlanAccion;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.PlanAccion.Content = "Plan de Acción";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }

                    if (verifica.persona.CuestionarioSeguridad != null && !verifica.persona.CuestionarioSeguridadVencido)
                    {
                        this.CuestionarioSeguridad.Content = verifica.persona.CuestionarioSeguridad;
                        this.CuestionarioSeguridad.Foreground = new SolidColorBrush(colorVerde);
                    }
                    else if (verifica.persona.CuestionarioSeguridadVencido)
                    {
                        this.CuestionarioSeguridad.Content = verifica.persona.CuestionarioSeguridad;
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;

                    }
                    else
                    {
                        this.CuestionarioSeguridad.Content = "Cuestionario de Seguridad";
                        sResolucion = "ALTO";
                        colorPintado = colorRojo;
                        verifica.autorizado = false;
                    }

                    if (verifica.autorizado)
                    {
                        colorPintado = (Color)ColorConverter.ConvertFromString("#02a950");
                        sResolucion = "SIGA";
                        this.DatosIdentificador.Content = verifica.persona.CURP;
                        this.DatosNombre.Content = verifica.persona.nombre;
                        this.DatosApellido.Content = String.Format("{0} {1}", verifica.persona.apellidoPaterno, verifica.persona.apellidoMaterno);
                        this.DatosTitulo.Content = "Razón Social:";
                        this.DatosRazonSocial.Content = verifica.persona.razonSocial;

                    }
                    else
                    {
                        this.MensajeInicio.Content = verifica.mensajeInactivo;
                        this.DatosIdentificador.Content = verifica.persona.CURP;
                        this.DatosNombre.Content = verifica.persona.nombre;
                        this.DatosApellido.Content = String.Format("{0} {1}", verifica.persona.apellidoPaterno, verifica.persona.apellidoMaterno);
                        this.DatosTitulo.Content = "Razón Social:";
                        this.DatosRazonSocial.Content =  verifica.persona.razonSocial;
                    }
                    this.SignalPase.Fill = new SolidColorBrush(colorPintado);
                    this.Resultado.Content = sResolucion;
                }
                catch (Exception ex)
                {
                    this.MensajeInicio.Content = verifica.mensaje;
                    this.SignalPase.Fill = new SolidColorBrush(colorPintado);
                    this.Resultado.Content = sResolucion;
                }
            }
        }
    }
}
