using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class VOEmpresa
    {
        public VOEmpresa() { }

        public VOEmpresa(Empresa emp)
        {
            Id = emp.Id;
            Nombre = emp.Nombre;
            RazonSocial = emp.RazonSocial;
            Rut = emp.Rut;
            Direccion = emp.Direccion;
            Telefono = emp.Telefono;
            Celular = emp.Celular;
            Cpostal = emp.Cpostal;
            Logo = emp.Logo;
            LogoCh = emp.LogoCh;
            Actual = emp.Actual=='S';
        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string RazonSocial { get; set; }

        public string Rut { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public string Cpostal { get; set; }

        public string Logo { get; set; }

        public string LogoCh { get; set; }

        public bool Actual { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
