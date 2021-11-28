using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase contiene un método para remover habilitaciones de empresas.
    /// </summary>
    public class RemoveHabEmpresaHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public RemoveHabEmpresaHandler (BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/removerhabempresa"};
        }
        
        /// <summary>
        /// Se encarga de procesar el mensaje para determinar si se removerá una habilitación.
        /// </summary>
        /// <param name="mensaje">El mensaje a procesar.</param>
        /// <param name="respuesta">La respusta al mensaje procesado.</param>
        /// <returns></returns>
        protected override bool InternalHandle(IMensaje mensaje, out string respuesta)
        {
            if (!this.ChequearHandler(mensaje, "/removerhabempresa"))
            {
                respuesta = string.Empty;
                return false;
            }
            
            if (Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].ComprobarUltimoComandoIngresado("/removerhabempresa") == true)
            {
                List<string> listaConParametros = Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].BuscarUltimoComando("/removerhabempresa");
                if (listaConParametros.Count == 0)
                {
                    respuesta = $"Ingrese el nombre de la habilitación a eliminar {listaConParametros.Count}.";
                    return true;
                }
                
                if (listaConParametros.Count == 1)
                {
                    string habilitacion = listaConParametros[0];
                    if (Singleton<ContenedorPrincipal>.Instancia.Empresas.ContainsKey(mensaje.Id))
                    {
                        Empresa value = Singleton<ContenedorPrincipal>.Instancia.Empresas[mensaje.Id];
                        LogicaEmpresa.RemoveHabilitacion(value, habilitacion);
                        
                        respuesta = $"Se ha removido la habilitación {habilitacion} con éxito. {OpcionesUso.AccionesEmpresas()}";
                    }
                    else
                    {
                        respuesta = "No se ha podido remover la habilitación, usted no está registrado como Empresa."+OpcionesUso.AccionesEmpresas();
                    }
                    return true;
                }
                else
                {
                    respuesta = $"Algo fue mal. {OpcionesUso.AccionesEmpresas()}";
                    return true;
                }
            }

            respuesta = string.Empty;
            return false;
        }
    }
}