using System;
using System.Collections.Generic;
using System.Globalization;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa un Emprendedor, que se encarga de buscar ofertas, y de manifestar su interés en las que sean de su agrado.
    /// Esta clase que contiene habilitaciones requiere, que se implemente la interfaz IHabilitaciones.
    /// La implementación de la interfaz es necesaria para unificar el nombre de su método con otras clases que tiene similares caracteristicas.
    /// </summary>
    public class Emprendedor : Usuario, IHabilitaciones
    {
        /// <summary>
        /// Este diccionario contiene las ofertas compradas y la fecha correspondiente.
        /// </summary>
        /// <returns></returns>
        public Dictionary<DateTime, Oferta> FechaDeOfertasCompradas = new Dictionary<DateTime, Oferta>();

        /// <summary>
        /// Ofertas en las que se interesa el emprendedor.
        /// </summary>
        public List<Oferta> OfertasInteresado = new List<Oferta>();

        /// <summary>
        /// Lista de habilitaciones del emprendedor.
        /// </summary>
        public List<string> HabilitacionesEmprendedor = new List<string>();

        private List<Oferta> ofertasCompradas = new List<Oferta>();

        private string especializaciones;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Emprendedor"/>.
        /// Como la clase hereda de la clase Usuario, recibe por parametros los propios de Usuario y los particulares de Emprendedor.
        /// </summary>
        /// <param name="nombre">Nombre del emprededor.</param>
        /// <param name="ubicacion">Ubicación del emprendedor.</param>
        /// <param name="rubro">Rubro del emprendedor.</param>
        /// <param name="habilitacion">Habilitaciones del emprendedor.</param>
        /// <param name="especializaciones">Especializaciones del emprendedor.</param>
        public Emprendedor(string nombre, string ubicacion, string rubro, Habilitaciones habilitacion, string especializaciones)
            : base(nombre, ubicacion, rubro)
        {
            this.Especializaciones = especializaciones;
            this.Habilitacion = habilitacion;
        }

        /// <summary>
        /// Habilitaciones del emprendedor.
        /// </summary>
        public Habilitaciones Habilitacion = new Habilitaciones();

        /// <summary>
        /// Obtiene una lista de las habilitaciones del emprendedor.
        /// </summary>
        /// <value>HabilitacionesEmprendedor.</value>
        public List<string> HabilitacionesDeEmprendedor { get => this.HabilitacionesEmprendedor; }

        /// <summary>
        /// Obtiene o establece las Especializaciones del emprendedor.
        /// </summary>
        public string Especializaciones { get; set; }

        /// <summary>
        /// Agrega habilitaciones.
        /// </summary>
        /// <param name="habilitacionBuscada">Nombre de la habilitación a agregar.</param>
        public void AddHabilitacion(string habilitacionBuscada)
        {
            if (this.Habilitacion.ListaHabilitaciones.Contains(habilitacionBuscada))
            {
                this.HabilitacionesEmprendedor.Add(habilitacionBuscada);
            }
        }

        /// <summary>
        /// Quita habilitaciones.
        /// </summary>
        /// <param name="habilitacion">Nombre de la habilitaciones a remover.</param>
        public void RemoveHabilitacion(string habilitacion)
        {
            this.HabilitacionesEmprendedor.Remove(habilitacion);
        }

        /// <summary>
        /// Muestra todas las habilitaciones posibles para agregar.
        /// </summary>
        public string GetListaHabilitaciones()
        {
          return this.Habilitacion.HabilitacionesDisponibles();
        }

        /// <summary>
        /// Calcula cuantas ofertas se han comprado desde diferentes fechas, y cuanto dinero se gastó en ellas.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio.</param>
        /// <param name="fechaFinal">Fecha de final.</param>
        /// <returns>Retorna las ofertas compradas dentro del período de tiempo especificado.</returns>
        public int CalcularOfertasCompradas(string fechaInicio, string fechaFinal)
        {
            int ofertasCompradas = 0;
            DateTime fInicio = DateTime.Parse(fechaInicio, CultureInfo.InvariantCulture);
            DateTime fFinal = DateTime.Parse(fechaFinal, CultureInfo.InvariantCulture);

            foreach (KeyValuePair<DateTime,Oferta> par in this.FechaDeOfertasCompradas)
            {
                if (par.Key >= fInicio && par.Key <= fFinal)
                {
                ofertasCompradas++;
                }
            }
            string texto = $"Se han comprado {ofertasCompradas} ofertas en el tiempo indicado";
            ConsolePrinter.DatoPrinter(texto);
            return ofertasCompradas;
        }
    }
}