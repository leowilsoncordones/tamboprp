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

        public Empresa( int id, string nombre, string razonSocial, 
                        string rut, string direccion, string telefono,
                        string celular, string cpostal, char actual)
        {
            Id = id;
            Nombre = nombre;
            RazonSocial = razonSocial;
            Rut = rut;
            Direccion = direccion;
            Telefono = telefono;
            Celular = celular;
            Cpostal = cpostal;
            Actual = actual;
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

        public char Actual { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
