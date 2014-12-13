using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using Negocio;
using NUnit.Framework;

namespace Tests_Entidades
{
    [TestFixture]
    class TestCeloSinServicio
    {

        [Test]
        public void Insert_CelosSinServicio_should_insert()
        {
            var celo = new Celo_Sin_Servicio
            {
                Registro = "5676",
                Id_evento = 2,
                Comentarios = "test_tamboprp",
                Fecha = DateTime.Now

            };
            var celoSinservMpr = new Celo_Sin_ServicioMapper(celo);
            var numero = celoSinservMpr.Insert();
            Assert.IsTrue(numero>0);
        }
    }
}
