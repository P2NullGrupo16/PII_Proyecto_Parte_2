using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa al Administrador, persona que invitara a las empresas a ingresar a la aplicación.
    /// </summary>
    public class Administrador
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Administrador"/>.
        /// </summary>
        /// <param name="nombre">Recibe por parametro un string de nombre.</param>
        public Administrador(string nombre)
        {
            if ( string.IsNullOrEmpty(nombre))
            {
                this.Nombre = "Jhon";
            }
            else 
            {
                this.Nombre = nombre;
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que le da el nombre al administrador.
        /// </summary>
        /// <value>El valor del nombre es de tipo string.</value>
        public string Nombre { get; set; }

        /// <summary>
        /// Esta lista contiene las empresas que el Administrador a invitado a unirse a la aplicación.
        /// </summary>
        /// <returns>Retorna la lista de Empresas que contiene.</returns>
        public List<Empresa> Empresas = new List<Empresa>();

        /// <summary>
        /// Este método permite invitar una empresa a unirse a la aplicación.
        /// </summary>
        /// <param name="empresa">Recibe un objeto de tipo empresa como parametro.</param>
        public void InvitarEmpresa(Empresa empresa)
        {
            this.Empresas.Add(empresa);
        }
    }
}