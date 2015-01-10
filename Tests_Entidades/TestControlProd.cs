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
    public class TestControlProd
    {
        [Test]
        public void ControlProdInsert_Should_Insert()
        {
            var control = new Control_Producc
            {
                Registro = "3110",
                Id_evento = 8,
                Fecha = DateTime.Now,
                Comentarios = "test_tamboprp",
                Dias_para_control = 25,
                Grasa = 2.50,
                Leche = 22.50               
            };
            var contmap = new Control_ProduccMapper(control);
            var result = contmap.Insert();
            Assert.IsTrue(result > 1);
        }

        [Test]

        public void GetShouldGet()
        {
            var conmap = new Controles_totalesMapper();
            var list = conmap.GetControlesTotalesEntreDosFechas("2010-03-25","2015-01-03");
            Assert.IsNotEmpty(list);
        }
    }
}
