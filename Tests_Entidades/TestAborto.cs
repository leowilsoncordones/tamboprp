using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using NUnit.Framework;

namespace Tests_Entidades
{
    [TestFixture]
    public class TestAborto
    {

        [Test]

        public void aborto_should_return_count_current_year()
        {
            var abmap = new AbortoMapper();
            var result = abmap.GetCantAbortosEsteAnio();
            Assert.IsTrue(9==result);
        }

        [Test]
        public void aborto_should_insert_and_log()
        {
            var abort = new Aborto
            {
                Id_evento = 0,
                Registro = "3110",
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp",
                Reg_padre = "SENTRY"
            };
            var abmap = new AbortoMapper(abort);
            var result = abmap.Insert();
            Assert.IsTrue(result>1);
        }

        [Test]
        public void aborto_should_not_insert_and_log()
        {
            var abort = new Aborto
            {
                Id_evento = 0,
                Registro = "3110",
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp",
                Reg_padre = "3110"
            };
            var abmap = new AbortoMapper(abort);
            Assert.Throws<System.Data.SqlClient.SqlException>(() => abmap.Insert());
        }


    }
}
