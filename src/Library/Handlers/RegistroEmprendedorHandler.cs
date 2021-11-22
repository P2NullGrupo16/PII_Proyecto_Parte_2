using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
    /// </summary>
    public class RegistroEmprendedorHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Esta clase procesa el mensaje "hola".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RegistroEmprendedorHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/registrarse"};
        }

        /// <summary>
        /// Procesa el mensaje "Registrarse" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="mensaje">El mensaje a procesar.</param>
        /// <param name="respuesta">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMensaje mensaje, out string respuesta)
        {
            if (Logica.HistorialDeChats.ContainsKey(mensaje.Id))
            {
                if (this.CanHandle(mensaje))
                {
                    Logica.HistorialDeChats[mensaje.Id].MensajesDelUser.Add(mensaje.Text); 
                }
                else
                {
                    if ((mensaje.Text.StartsWith("/") == false) && (Logica.HistorialDeChats[mensaje.Id].ComprobarUltimoComandoIngresado("/registrarse") == true))
                    {
                        Logica.HistorialDeChats[mensaje.Id].MensajesDelUser.Add(mensaje.Text); 
                    }
                    else
                    {
                        respuesta = string.Empty;
                        return false;
                    }
                }
            }
            
            // cambiar este canhandle por algo tipo, si en el historial, el ultimo comando es /Registrarse, entra al if.
            if (Logica.HistorialDeChats[mensaje.Id].ComprobarUltimoComandoIngresado("/registrarse") == true)
            {
                List<string> listaConParametros = Logica.HistorialDeChats[mensaje.Id].BuscarUltimoComando("/registrarse");
                if (listaConParametros.Count == 0)
                {
                    respuesta = "Ingrese el nombre";
                    return true;
                    //Console.WriteLine("Entro aca");
                }
                if (listaConParametros.Count == 1)
                {
                    respuesta = "Ingrese la ubicacion";
                    return true;
                }
                if (listaConParametros.Count == 2)
                {
                    respuesta = "Ingrese rubro";
                    return true;
                }
                if (listaConParametros.Count == 3)
                {
                    respuesta = "Ingrese especializaciones";
                    return true;
                }
                if (listaConParametros.Count == 4)
                {
                    string nombreEmprendedor = listaConParametros[3];
                    string ubicacionEmprendedor = listaConParametros[2];
                    string rubroEmprendedor = listaConParametros[1];
                    string especializacionesEmprendedor = listaConParametros[0];
                    LogicaEmprendedor.RegistroEmprendedor(nombreEmprendedor, ubicacionEmprendedor, rubroEmprendedor, especializacionesEmprendedor, mensaje.Id);
                    respuesta = $"Usted se ha registrado como un Emprendedor con el nombre {nombreEmprendedor}, la ubicacion {ubicacionEmprendedor}, el rubro {rubroEmprendedor}, y la especializacion {especializacionesEmprendedor}. ";
                    return true;
                }
            }

            respuesta = string.Empty;
            return false;   
        }
    }
}