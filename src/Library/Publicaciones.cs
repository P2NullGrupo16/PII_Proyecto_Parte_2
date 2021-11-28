using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de las Publicaciones.
    /// </summary>
    /// <remarks>
    /// Para esta clase se utilizó el patron de diseño de Expert, ya que desde nuestro punto de vista,
    /// la clase Publicaciones tiene metodos que sean exclusivos de su clase ya que es la que se encarga de conocer 
    /// todo lo necesario para hacer posible la ejecución de sus métodos, y que no sean necesarios para el resto de clases.
    /// </remarks>

    public class Publicaciones : IJsonConvertible
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonConstructor]
        public Publicaciones()
        {
        }


        /// <summary>
        /// Una lista que contiene las ofertas.
        /// </summary>
        [JsonInclude]
        public List<Oferta> OfertasPublicados {get;} = new List<Oferta>();

        /// <summary>
        /// Este método imprime las ofertas contenidas en OfertasPublicados.
        /// </summary>
        public void GetOfertasPublicados()
        {
            StringBuilder getOfertasPublicados = new StringBuilder("Ofertas: \n");
            foreach (Oferta oferta in this.OfertasPublicados)
            {
                getOfertasPublicados.Append($"- {oferta.Nombre}.");
            }

            ConsolePrinter.DatoPrinter(getOfertasPublicados.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConvertirJson()
        {
            JsonSerializerOptions opciones = new()
            {
                WriteIndented = true,
                ReferenceHandler = MyReferenceHandler.Instance,
            };

            return JsonSerializer.Serialize(this, opciones);
        }
    }
}