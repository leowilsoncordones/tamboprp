using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Entidades;
using Negocio;

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

        [Test]
        public void getEventosAnimal_should_collect() 
        {
            var anim = new Animal();
            var listEv = new List<Evento>();
            listEv = Fachada.Instance.GetEventosAnimal(anim);
            Assert.IsNotNull(anim);
        }


        [Test]

        public void consolidarLactanciaTest()
        {
            var result = Fachada.Instance.ConsolidarLactancia("3451", false);
            Assert.IsTrue(result.Registro == "3451");
        }

    }
}
