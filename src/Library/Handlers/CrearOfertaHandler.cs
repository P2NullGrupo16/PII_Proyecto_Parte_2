using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/crearoferta".
    /// </summary>
    public class CrearOfertaHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Esta clase procesa el mensaje "hola".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CrearOfertaHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/crearoferta"};
        }

        /// <summary>
        /// Este método procesa el mensaje "Crear una oferta" y retorna true.
        /// En caso contrario retorna false.
        /// </summary>
        /// <param name="mensaje">Recibe por parametro el mensaje a procesar.</param>
        /// <param name="respuesta">Recibe por paramtro la respuesta al mensaje procesado.</param>
        /// <returns>Retorna true si se ha podido realizar la operación, o false en caso contrario.</returns>
        protected override bool InternalHandle(IMensaje mensaje, out string respuesta)
        {
            if (!this.ChequearHandler(mensaje, "/crearoferta"))
            {
                respuesta = string.Empty;
                return false;
            }
            else if (Singleton<ContenedorPrincipal>.Instancia.Empresas.ContainsKey(mensaje.Id))
            {
                List<string> listaConParametros = Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].BuscarUltimoComando("/crearoferta");
                if (listaConParametros.Count == 0)
                {
                    respuesta = "Ingrese el nombre de la oferta";
                    return true;
                }
                else if (listaConParametros.Count == 1)
                {
                    respuesta = "Ingrese el nombre del material";
                    return true;
                }
                else if (listaConParametros.Count == 2)
                {
                    respuesta = "Ingrese el precio";
                    return true;
                }
                else if (listaConParametros.Count == 3)
                {
                    respuesta = "Ingrese unidad";
                    return true;
                }
                else if (listaConParametros.Count == 4)
                {
                    respuesta = "Ingrese un tag o palabra clave";
                    return true;
                }
                else if (listaConParametros.Count == 5)
                {
                    respuesta = "Ingrese una ubicación";
                    return true;
                }
                else if (listaConParametros.Count == 6)
                {
                    respuesta = "Elija si es una oferta puntual o constante";
                    return true;
                }
                else if (listaConParametros.Count == 7)
                {
                    respuesta = "Ingrese la cantidad";
                    return true;
                }
                else if (listaConParametros.Count == 8)
                {
                    string cantidadMaterial = listaConParametros[0];
                    string puntualConstante = listaConParametros[1];
                    string ubicacionOferta = listaConParametros[2];
                    string tagOferta = listaConParametros[3];
                    string unidadesOferta = listaConParametros[4];
                    string precioOferta = listaConParametros[5];
                    string nombreMaterialOferta = listaConParametros[6];
                    string nombreOferta = listaConParametros[7];
                    
                    Empresa value = Singleton<ContenedorPrincipal>.Instancia.Empresas[mensaje.Id];

                    try
                    {
                        LogicaEmpresa.CrearOferta(value, nombreOferta, nombreMaterialOferta, cantidadMaterial, precioOferta, unidadesOferta, tagOferta, ubicacionOferta, puntualConstante);
                    }
                    catch (System.ArgumentException e)
                    { 
                        respuesta = $"{e.Message}\nUse /crearoferta de nuevo.";
                        return true;
                    }
                    
                    Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].HistorialClear();
                    respuesta = $"Se ha registrado con nombre {nombreOferta}, de material {nombreMaterialOferta}, del tipo {puntualConstante}, unidades: {unidadesOferta}, al precio de: {precioOferta}, con la ubicación en {ubicacionOferta} y el tag {tagOferta}. {OpcionesUso.AccionesEmpresas()}";
                    return true; 
                }
            }
            else
            {
                respuesta = $"Usted no es una empresa, no puede usar este comando.";
                return true;
            }
            
            respuesta= string.Empty;
            return false;
        }
    }
}