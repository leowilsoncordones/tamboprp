using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
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
    }
}
