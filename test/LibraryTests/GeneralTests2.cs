using ClassLibrary;
using NUnit.Framework;

namespace Test.Library
{ 
    /// <summary>
    /// Clase de pruebas de TestGeneral.
    /// </summary>
    [TestFixture]
    public class GeneralTests2
    {
        /// <summary>
        /// Test historia de usuario2
        /// Tenemos una empresa y un emprendedor, la empresa crea productos, y se quiere comprobar
        /// si el método para calcular ofertas entregadas/consumidas segun tiempo funcionan acorde a lo estipulado.
        /// </summary>
        [Test]
        public void TestGeneral2()
        {
            Empresa  empresaConaprole = new Empresa("Conaprole", "Pakistan", new Rubro("textil"), new Habilitaciones());
            Emprendedor emprendedor1 = new Emprendedor("Lebron James", "Korea del Norte", new Rubro("textil"), new Habilitaciones(), "Decorado de interiores");

            LogicaEmpresa.CrearProducto(empresaConaprole, "Coca-cola", "Nix", 2000, "Litros", "bebidas", "Guyana Francesa", "Constante");
            LogicaEmpresa.CrearProducto(empresaConaprole, "Coca-cola ZERO", "Nix", 2000, "Litros", "bebidas", "Guyana Francesa", "Constante");
            LogicaEmpresa.CrearProducto(empresaConaprole, "Fiat 1", "El mejor de todos", 5500, "Cantidad", "auto", "Carrasco", "Constante");
            LogicaEmpresa.CrearProducto(empresaConaprole, "Coca-cola2", "Nix", 2000, "Litros", "bebidas", "Guyana Francesa", "Constante");
            LogicaEmpresa.CrearProducto(empresaConaprole, "Coca-cola ZERO2", "Nix", 2000, "Litros", "bebidas", "Guyana Francesa", "Constante");
            LogicaEmpresa.CrearProducto(empresaConaprole, "Fiat 12", "El mejor de todos", 5500, "Cantidad", "auto", "Aguas verdes", "Constante");

            // Quiero como empresa calcular las ofertas entregadas segun x tiempo.
            
            LogicaEmpresa.AceptarOferta(empresaConaprole, "Fiat 1");
            LogicaEmpresa.AceptarOferta(empresaConaprole, "Coca-cola");

            LogicaEmpresa.CalcularOfertasVendidasSegunTiempo(empresaConaprole, "2020-10-15", "2028-10-15");

            int expectedCantidadVendidasSegunTiempo = 2;

            Assert.AreEqual(expectedCantidadVendidasSegunTiempo, LogicaEmpresa.CalcularOfertasVendidasSegunTiempo(empresaConaprole, "2020-10-15", "2028-10-15"));

            //Quiero como emprendedor calcular las ofertas que consumí segun x tiempo.

            LogicaEmprendedor.InteresadoEnOferta(emprendedor1, "Fiat 12");
            LogicaEmprendedor.InteresadoEnOferta(emprendedor1, "Coca-cola2");
            LogicaEmprendedor.InteresadoEnOferta(emprendedor1, "Coca-cola ZERO2");

            int expectedCantidadConsumidaSegunTiempo = 3;

            Assert.AreEqual(expectedCantidadConsumidaSegunTiempo, LogicaEmprendedor.CalcularOfertasCompradas(emprendedor1, "2020-10-15", "2028-10-15"));            
        }
    }
}