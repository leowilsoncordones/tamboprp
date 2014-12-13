using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using NUnit.Framework;

namespace Tests_Entidades
{
    [TestFixture]
    public class TestVenta
    {
        [Test]
        public void InsertVenta_should_insert()
        {
            var venta = new Venta
            {
                Registro = "3110",
                Id_evento = 11,
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp"
            };

            var ventamap = new VentaMapper(venta);
            var result = ventamap.Insert();
            Assert.IsTrue(result > 1);
        }
    }
}
