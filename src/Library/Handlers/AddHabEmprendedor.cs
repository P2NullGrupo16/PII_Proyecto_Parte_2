using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsability que implementa el comando "hola".
    /// </summary>
    public class AddHabEmprendedorHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// Esta clase procesa el mensaje ingresado por el usuario.
        /// </summary>
        /// <param name="next"></param>
        public AddHabEmprendedorHandler(BaseHandler next):base(next)
        {
            this.Keywords = new string[] {"/agregarhabilitacionemprendedor"};
        }

        /// <summary>
        /// Este método procesa el mensaje "Agregar habilitación" y retorna true.
        /// En caso contrario retorna false.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns></returns>
        protected override bool InternalHandle(IMensaje message, out string response)
        {
            if (Logica.HistorialDeChats.ContainsKey(message.Id))
            {
                if (this.CanHandle(message))
                {
                    Logica.HistorialDeChats[message.Id].MensajesDelUser.Add(message.Text); 
                }
                else
                {
                    if ((message.Text.StartsWith("/") == false) && (Logica.HistorialDeChats[message.Id].ComprobarUltimoComandoIngresado("/agregarhabilitacionemprendedor") == true))
                    {
                        Logica.HistorialDeChats[message.Id].MensajesDelUser.Add(message.Text); 
                    }
                    else
                    {
                        response = string.Empty;
                        return false;
                    }
                }
            }

            if (Logica.HistorialDeChats[message.Id].ComprobarUltimoComandoIngresado("/agregarhabilitacionemprendedor") == true)
            {
                List<string> listaConParam = Logica.HistorialDeChats[message.Id].BuscarUltimoComando("/agregarhabilitacionemprendedor");
                
                if (listaConParam.Count == 0)
                {
                    response = "ingrese la habilitacion que desea agregar";
                    return true;
                }
                if (listaConParam.Count == 1)
                {
                    string nuevaHab = listaConParam[0];
                    if (Logica.Emprendedores.ContainsKey(message.Id))
                    {
                        Emprendedor value = Logica.Emprendedores[message.Id];
                        LogicaEmprendedor.AddHabilitacion(value,nuevaHab);
                        response = $"Se ha agregado '{nuevaHab}' a la lista de habilitaciones.";
                        return true;
                    }
                }
            }
            response = string.Empty;
            return false;
        }
    }
}