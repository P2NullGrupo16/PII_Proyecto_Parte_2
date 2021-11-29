using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsability que implementa el comando "hola".
    /// </summary>
    public class CalcularOfertasCompradasHandler : BaseHandler 
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// Esta clase procesa el mensaje ingresado por el usuario.
        /// </summary>
        /// <param name="next">El próximo "handler."</param>
        public CalcularOfertasCompradasHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/calcularofertascompradas"};
        }
        
        /// <summary>
        /// Este método procesa el mensaje "Calculas ofertas Vendidas" y retorna true.
        /// En caso contrario retorna false.
        /// </summary>
        /// <param name="mensaje">Recibe por parametro el mensaje a procesar.</param>
        /// <param name="respuesta">Recibe por paramtro la respuesta al mensaje procesado.</param>
        /// <returns>Retorna true si se ha podido realizar la operación, o false en caso contrario.</returns>
        protected override bool InternalHandle(IMensaje mensaje, out string respuesta)
        {
            if (!this.ChequearHandler(mensaje, "/calcularofertascompradas"))
            {
                respuesta = string.Empty;
                return false;
            }
            else if (Singleton<ContenedorPrincipal>.Instancia.Emprendedores.ContainsKey(mensaje.Id))
            {
                List<string> listaConParam = Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].BuscarUltimoComando("/calcularofertascompradas");
                if (listaConParam.Count == 0)
                {
                    respuesta = "Ingrese la fecha de inicio (YYYY-MM-DD).";
                    return true;
                }
                else if (listaConParam.Count == 1)
                {
                    respuesta = "Ingrese la fecha final (YYYY-MM-DD).";
                    return true;
                }
                else if (listaConParam.Count == 2)
                {
                    string fechaInicio = listaConParam[1];
                    string fechaFinal = listaConParam[0];

                    Emprendedor value = Singleton<ContenedorPrincipal>.Instancia.Emprendedores[mensaje.Id];

                    try
                    {
                        LogicaEmprendedor.CalcularOfertasCompradas(value, fechaInicio, fechaFinal);
                    }
                    catch (System.ArgumentException e)
                    {
                        respuesta = e.Message;
                        return true;    
                    }

                    Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].HistorialClear();
                    respuesta = $"En este periodo se han adquirido {LogicaEmprendedor.CalcularOfertasCompradas(value, fechaInicio, fechaFinal)}. {OpcionesUso.AccionesEmprendedor()}";
                    return true;
                }
            }
            else
            {
                respuesta = $"Usted no es un emprendedor, no puede usar este comando.";
                return true;
            }
            
            respuesta = string.Empty;
            return false;
        }
    }
}