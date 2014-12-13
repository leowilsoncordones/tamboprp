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
    public class TestMuerte
    {
        [Test]
        public void MuerteInsert_should_insert()
        {
            var muerte = new Muerte
            {
                Registro = "3110",
                Id_evento = 12,
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp",
                Enfermedad = 1
            };
            var muertemap = new MuerteMapper(muerte);
            var result = muertemap.Insert();
            Assert.IsTrue(result > 1);
        }
    }
}
