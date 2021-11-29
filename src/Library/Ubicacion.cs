using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class Ubicacion
    {
        /// <summary>
        /// Constructor sin parametros de la clase Ubicacion, ya que es esencial el atributo JsonConstructor
        /// para la serialización de datos en la clase.
        /// </summary>
        /// <returns></returns>
        /// 
        [JsonConstructor]
        public Ubicacion()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Ubicacion"/>.
        /// </summary>
        public Ubicacion(string nombre)
        {
            this.NombreCalle = nombre;
        }

        /// <summary>
        /// .
        /// </summary>
        /// <value>Valor es un string del nombre.</value>
        public string NombreCalle { get; set; }

        /// <summary>
        /// Metodo que utiliza gracias a la interfaz IJsonConvertible para convertir a formato Json y aplicar en persistencia. 
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