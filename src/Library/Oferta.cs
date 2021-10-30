using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ClassLibrary
{
    public class Oferta : IHabilitaciones
    {
        private List<Habilitaciones> habilitaciones = new List<Habilitaciones>();
        public Oferta(string nombre, string material, int precio, string unidad, string tags, string ubicacion, Guid id)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Material = material;
            this.PrecioUnitario = precio;
            this.Unidad = unidad;
            this.Tags = tags;
            this.Ubicacion = ubicacion;

        }
        public string Nombre {get; set;}
        public string Material {get; set;}
        public int PrecioUnitario {get; set;}
        public int PrecioTotal;
        public string Unidad {get; set;}
        
        public string Tags {get; set;}
        
        public Guid Id {get; private set;} = Guid.NewGuid();

        public bool Interesado {get; set;}
        

        public void AddHabilitacion(string nombre)
        {
            Habilitaciones habilitacion = new Habilitaciones(nombre);
            this.habilitaciones.Add(habilitacion);
            Console.WriteLine($"Habilitación '{habilitacion.Nombre}' agregada exitosamente.");
        }
        public void RemoveHabilitacion(string nombre)
        {
            Habilitaciones habilitacion = new Habilitaciones(nombre);
            this.habilitaciones.Remove(habilitacion);
            Console.WriteLine( $"Habilitación '{habilitacion.Nombre}' eliminada exitosamente.");
        }
        
        public void GetHabilitacionList()
        {
            StringBuilder getHabilitaciones = new StringBuilder("Habilitaciones: \n");
            foreach (Habilitaciones habilitacion in habilitaciones)
            {
                getHabilitaciones.Append($"- {habilitacion.Nombre}.");   
            }
            Console.WriteLine(getHabilitaciones.ToString());
        }
        public DateTime FechaDePublicacion 
        {
            get
            {
                return DateTime.Now;
            }
        }
        public string Ubicacion {get; set;}
        
    }
    
}