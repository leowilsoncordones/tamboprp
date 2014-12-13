using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using NUnit.Framework;

namespace Tests_Entidades
{
    [TestFixture]
    public class TestConcurso
    {

        [Test]
        public void LugConcById_should_return()
        {
            var lug = new LugarConcurso {Id = 1};
            var lumap = new LugarConcursoMapper(lug);
            var result = lumap.GetLugarConcursoById();

            Assert.IsNotNull(result);
        }

        [Test]
        public void LugConcGetAll_should_return()
        {
            var lumap = new LugarConcursoMapper();
            var result = lumap.GetAll();

            Assert.IsNotEmpty(result);
        }
    }
}
