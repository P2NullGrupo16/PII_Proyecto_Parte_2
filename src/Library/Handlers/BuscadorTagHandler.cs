using System;
using Telegram.Bot.Types;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
    /// </summary>
    public class BuscadorTagHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Esta clase procesa el mensaje "hola".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public BuscadorTagHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"!BuscarTag"};
        }

        /// <summary>
        /// 
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
                    if ((mensaje.Text.StartsWith("!") == false) && (Logica.HistorialDeChats[mensaje.Id].ComprobarUltimoComandoIngresado("!BuscarTag") == true))
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
            
            if (Logica.HistorialDeChats[mensaje.Id].ComprobarUltimoComandoIngresado("!BuscarTag") == true)
            {
                List<string> listaComandos = Logica.HistorialDeChats[mensaje.Id].BuscarUltimoComando("!BuscarTag");
                if (listaComandos.Count == 0)
                {
                    respuesta = "Ingrese el Tag por el que sea filtrar en su búsqueda.";
                    return true;
                }
                if (listaComandos.Count == 1)
                {
                    string palabraClave = listaComandos[0];
                    
                    LogicaBuscadores.BuscarPorTags(palabraClave);
                    respuesta = TelegramPrinter.BusquedaPrinter(LogicaBuscadores.BuscarPorTags(palabraClave));
                    return true;
                }          
            }

            respuesta = string.Empty;
            return false;
        }
    }
}