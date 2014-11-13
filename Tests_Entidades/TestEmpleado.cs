using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Datos;
using NUnit.Framework;
using Entidades;


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

        [Test]
        public void empleado_should_get_all()
        {
            List<Empleado> listEmp;
            listEmp = new EmpleadoMapper().GetAll();
            Assert.IsNotEmpty(listEmp);
        }

        [Test]
        public void empleado_getEmpleadoById_should_return_empleado()
        {
            Empleado emp = new Empleado();
            emp.Id_empleado = 1;
            var result = new EmpleadoMapper(emp).GetEmpleadoById();
            Assert.AreEqual(result.Id_empleado, emp.Id_empleado);
        }


        [Test]
        public void empleado_insert_should_create_new_empleado()
        {
            var emp = new Empleado {Nombre = "Leonardo", Apellido = "Wilson", Iniciales = "LW", Activo = true};
            var result = new EmpleadoMapper(emp).Insert();
            Assert.IsTrue(result != -1);
        }


        [Test]
        public void empleado_update_should_update()
        {
            var emp = new Empleado { Id_empleado = 25, Nombre = "Leo", Apellido = "Wilson", Iniciales = "LW", Activo = true};
            var result = new EmpleadoMapper(emp).Update();
            Assert.IsTrue(result != -1);
        }

        [Test]

        public void empleado_delete_should_delete()
        {
            Empleado emp = new Empleado();
            emp.Id_empleado = 25;
            var result = new EmpleadoMapper(emp).Delete();
            Assert.IsTrue(result != -1);
        }


        [Test]
        public void empleado_should_serialize_to_json()
        {
            var emp = new Empleado {Activo = true, Apellido = "Wilson", Id_empleado = 1, Iniciales = "LW", Nombre = "Leo"};
            var json = new JavaScriptSerializer().Serialize(emp);
            Assert.IsNotEmpty(json);
        }

        [Test]
        public void empleado_should_serialize_list_to_json()
        {
            var emp = new Empleado { Activo = true, Apellido = "Wilson", Id_empleado = 1, Iniciales = "LW", Nombre = "Leo" };
            var emp2 = new Empleado { Activo = true, Apellido = "Gil", Id_empleado = 2, Iniciales = "FG", Nombre = "Fede" };
            var lista = new List<Empleado> {emp, emp2};
            var json = new JavaScriptSerializer().Serialize(lista);
            Assert.IsNotEmpty(json);
        }

    }
}
