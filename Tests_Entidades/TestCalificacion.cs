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
     public class TestCalificacion
    {
        [Test]
        public void CalificacionInsert_should_insert()
        {
            var calif = new Calificacion
            {
                Registro = "H-DESCONOC",
                Id_evento = 9,
                Letras = "B",
                Puntos = 88,
                Fecha = DateTime.Now
            };
            var califmap = new CalificacionMapper(calif);
            var result = califmap.Insert();
            Assert.IsTrue(result > 1);
        }
    }
}
