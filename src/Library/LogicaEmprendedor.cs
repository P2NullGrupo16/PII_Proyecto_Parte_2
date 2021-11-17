using System;
namespace ClassLibrary
{
    /// <summary>
    /// Esta clase contiene la lógica del emprendedor.
    /// </summary>
    /// <remarks>
    /// Contiene un método para llamar a cada método de la clase Emprendedor.
    /// </remarks>
    public static class LogicaEmprendedor
    {
        /// <summary>
        /// Registro de usuario para ser emprendedor.
        /// </summary>
        /// <param name="nombre">Nombre del emprendedor.</param>
        /// <param name="ubicacion">Ubicacion del emprendedor.</param>
        /// <param name="rubro">Rubro del emprendedor.</param>
        /// <param name="especializaciones">Especializaciones del emprendedor.</param>
        /// <param name="id">Id del chat.</param>
        public static void RegistroEmprendedor(string nombre, string ubicacion, string rubro, string especializaciones, string id)
        {
            if (Rubro.CheckRubro(rubro))
            { 
             Emprendedor nuevoEmprendedor = new Emprendedor(nombre, ubicacion, rubro, new Habilitaciones(), especializaciones); 
             Logica.Emprendedores.Add(id, nuevoEmprendedor); // Agrego a la lista de emprendedores registrados.
            }
            else
            {
                Console.WriteLine("El rubro no existe.");
            }      
        }

        /// <summary>
        /// Este método se encarga de llamar a AddHabilitación de Emprendedor.
        /// </summary>
        /// <param name="emprendedor">Un emprendedor.</param>
        /// <param name="habilitacionBuscada">Nombre de la habilitación a agregar.</param>
        public static void AddHabilitacion(Emprendedor emprendedor, string habilitacionBuscada)
        {
            emprendedor.AddHabilitacion(habilitacionBuscada);
        }

        /// <summary>
        /// Este método se encarga de llamar a RemoveHabilitación de Emprendedor.
        /// </summary>
        /// <param name="emprendedor">Un emprendedor.</param>
        /// <param name="habilitacion">Nombre de la habilitación a remover.</param>
        public static void RemoveHabilitacion(Emprendedor emprendedor, string habilitacion)
        {
            emprendedor.RemoveHabilitacion(habilitacion);
        }

        /// <summary>
        /// Este método llama a GetHabilitacionList de Emprendedor.
        /// </summary>
        /// <param name="emprendedor">Un emprendedor.</param>
        public static string GetHabilitacionList(Emprendedor emprendedor)
        {
            return emprendedor.GetHabilitacionList();
        }

        /// <summary>
        /// Este método llama a InteresadoEnOferta de Emprendedor.
        /// </summary>
        /// <param name="emprendedor">Un emprendedor.</param>
        /// <param name="nombreOferta">Una oferta.</param>
        // Se hizo en equipo.
        public static void InteresadoEnOferta(Emprendedor emprendedor, string nombreOferta)
        {
            foreach (Oferta item in Logica.PublicacionesA.OfertasPublicados)
            {
               if (item.Nombre == nombreOferta)
               {
                emprendedor.OfertasInteresado.Add(item);
                item.Interesado.Add(emprendedor.Nombre);
                item.EmpresaCreadora.InteresadosEnOfertas.Add(item); // Agregado para solucionar test
                emprendedor.FechaDeOfertasCompradas.Add(DateTime.Now, item); // La fecha en la que se compró la oferta

               } 
            }
        }

        /// <summary>
        /// Este método llama a CalcularOfertasCompradas de Emprendedor.
        /// </summary>
        /// <param name="emprendedor">Un emprendedor.</param>
        /// <param name="fechaInicio">Fecha de inicio.</param>
        /// <param name="fechaFinal">Fecha de final.</param>
        /// <returns>Retorna las ofertas compradas dentro del período de tiempo especificado.</returns>
        public static int CalcularOfertasCompradas(Emprendedor emprendedor, string fechaInicio, string fechaFinal)
        {
            return emprendedor.CalcularOfertasCompradas(fechaInicio, fechaFinal);
        }
    }
}