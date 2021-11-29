using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsability que implementa el comando "hola".
    /// </summary>
    public class AgregarHabEmpresaHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// Esta clase procesa el mensaje ingresado por el usuario.
        /// </summary>
        /// <param name="next"></param>
        public AgregarHabEmpresaHandler(BaseHandler next):base(next)
        {
            this.Keywords = new string[] {"/agregarhabilitacionempresa"};
        }

        /// <summary>
        /// Este método procesa el mensaje "Agregar habilitación" y retorna true.
        /// En caso contrario retorna false.
        /// </summary>
        /// <param name="mensaje">Recibe por parametro el mensaje a procesar.</param>
        /// <param name="respuesta">Recibe por paramtro la respuesta al mensaje procesado.</param>
        /// <returns>Retorna true si se ha podido realizar la operación, o false en caso contrario.</returns>
        protected override bool InternalHandle(IMensaje mensaje, out string respuesta)
        {
            if (!this.ChequearHandler(mensaje, "/agregarhabilitacionempresa"))
            {
                respuesta = string.Empty;
                return false;
            }
            else if (Singleton<ContenedorPrincipal>.Instancia.Empresas.ContainsKey(mensaje.Id))
            {
                List<string> listaConParametros = Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].BuscarUltimoComando("/agregarhabilitacionempresa");
                
                if (listaConParametros.Count == 0)
                {

                    respuesta = $"Ingrese la habilitación que desea agregar.\n{Singleton<ContenedorRubroHabilitaciones>.Instancia.textoListaHabilitaciones()}";
                    return true;
                }
                else if (listaConParametros.Count == 1)
                {
                    string nuevaHab = listaConParametros[0];
                    
                    Empresa value = Singleton<ContenedorPrincipal>.Instancia.Empresas[mensaje.Id];
                    try
                    {
                        LogicaEmpresa.AddHabilitacion(value,nuevaHab);
                    }
                    catch (System.ArgumentException e)
                    {
                        respuesta = e.Message;
                        return true;
                    }
                    
                    Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].HistorialClear();
                    respuesta = $"Se ha agregado '{nuevaHab}' a la lista de habilitaciones. {OpcionesUso.AccionesEmpresas()}";
                    return true;   
                }
            }
            else
            {
                respuesta = $"Usted no es una empresa, no puede utilizar este comando.";
                return true;
            }
            
            respuesta = string.Empty;
            return false;
        }
    }
}