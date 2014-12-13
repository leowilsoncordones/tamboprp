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
    public class TestDiagPrenez
    {
        [Test]
        public void DiagPrenezInsert_should_insert()
        {
            var prenez = new Diag_Prenez
            {
                Registro = "3110",
                Id_evento = 7,
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp",
                Diagnostico = 'V'
            };
            var diagmap = new Diag_PrenezMapper(prenez);
            var result = diagmap.Insert();
            Assert.IsTrue(result > 1);
        }
    }
}
