using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Tests_Entidades
{
    [TestFixture]
    public class TestEmpleado
    {

        [Test]
        public void empleado_should_have_id()
        {
            var emp = new Empleado{Id_empleado = 1, Activo = true, Apellido = "Perez", Nombre = "Juan" };
            var result = emp.Id_empleado.ToString();
            Assert.IsNotEmpty(result);
        }

    }
}
