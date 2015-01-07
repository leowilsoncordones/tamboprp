namespace Entidades
{
    public class Empresa
    {
        public Empresa()
        {
        }

        public Empresa(int id)
        {
            Id = id;
        }

        public Empresa( int id, string nombre, string razonSocial, string rut, 
                        string letraSistema, string direccion, string ciudad, string telefono,
                        string celular, string web, string cpostal, char actual)
        {
            Id = id;
            Nombre = nombre;
            RazonSocial = razonSocial;
            Rut = rut;
            LetraSistema = letraSistema;
            Direccion = direccion;
            Ciudad = ciudad;
            Telefono = telefono;
            Celular = celular;
            Web = web;
            Cpostal = cpostal;
            Actual = actual;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string RazonSocial { get; set; }

        public string Rut { get; set; }

        public string LetraSistema { get; set; }

        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public string Web { get; set; }

        public string Cpostal { get; set; }

        public string Logo { get; set; }

        public string LogoCh { get; set; }

        public char Actual { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
