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
    public class TestSecado
    {
        [Test]
        public void SecadoInsert_should_insert()
        {
            var secado = new Secado
            {
                Registro = "3110",
                Id_evento = 4,
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp",
                Motivos_secado = 1,
            };
            var secmap = new SecadoMapper(secado);
            var result = secmap.Insert();
            Assert.IsTrue(result > 1);
        }
    }
}
