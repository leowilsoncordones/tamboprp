using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests_Entidades
{
    [TestFixture]
    public class TestAnimal
    {
        [Test]
        public void date_should_generate()
        {
            var fecha = DateTime.Now;
            var resultado = fecha.ToString("yyyy-MM-dd");
            Assert.AreEqual("2014-11-03",resultado);
        }

    }
}
