using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de imprimir por consola los atributos de oferta.
    /// </summary>
    /// <remarks>
    /// En este caso se aplicó SRP para poder imprimir las ofertas sin tener que modificarlas a ellas.
    /// </remarks>
    public class ConsolePrinter : IPrinter
    {
        /// <summary>
        /// Este método imprime por consola los atributos de oferta.
        /// </summary>
        /// <param name="oferta">Una oferta.</param>
        public string OfertaPrinter(Oferta oferta)
        {
            string texto = $"Nombre: {oferta.Nombre}, ID: {oferta.Id}, Material: {oferta.Material.Nombre}, Precio {oferta.Material.Precio}, Unidad: {oferta.Material.Unidad}, Ubicación {oferta.Ubicacion.NombreCalle}, Fecha de Publicación {Oferta.FechaDePublicacion}";
            Console.WriteLine(texto);
            return texto;
        }

        /// <summary>
        /// Este método imprime una string con información.
        /// </summary>
        /// <param name="dato"></param>
        public static void DatoPrinter(String dato)
        {
            Console.WriteLine(dato);
        }
    }
}