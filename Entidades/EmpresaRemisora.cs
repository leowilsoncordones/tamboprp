namespace Entidades
{
    public class EmpresaRemisora
    {
        public EmpresaRemisora()
        {
        }

        public EmpresaRemisora(int id)
        {
            Id = id;
        }

        public EmpresaRemisora( int id, string nombre, string razonSocial, 
                                string rut, string direccion, string telefono, char actual )
        {
            Id = id;
            Nombre = nombre;
            RazonSocial = razonSocial;
            Rut = rut;
            Direccion = direccion;
            Telefono = telefono;
            Actual = actual;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string RazonSocial { get; set; }

        public string Rut { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public char Actual { get; set; }

        public override string ToString()
        {
            return Nombre + " (" + RazonSocial + ")";
        }

    }
}
