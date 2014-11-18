using System;

namespace Negocio
{
    public class VOControlProdUltimo
    {
        public VOControlProdUltimo()
        {
            
        }

        public VOControlProdUltimo  (string registro, int numLact, int dias, 
                                    double leche, double grasa, DateTime fechaServicio, int numServicio, 
                                    char diag, DateTime fechaProbParto, DateTime fechaControl)
        {
            Registro = registro;
            NumLact = numLact;
            Dias = dias;
            Leche = leche;
            Grasa = grasa;
            ProdLeche = Dias * Leche;
            ProdGrasa = Dias * Grasa;
            FechaServicio = fechaServicio;
            NumServicio = numServicio;
            Diag = diag;
            FechaProbParto = fechaProbParto;

            FechaControl = fechaControl;
        }

        public VOControlProdUltimo(string registro, int numLact, int dias, double leche, double grasa, DateTime fechaControl)
        {
            Registro = registro;
            NumLact = numLact;
            Dias = dias;
            Leche = leche;
            Grasa = grasa;
            ProdLeche = Dias * Leche;
            ProdGrasa = Dias * Grasa;

            FechaControl = fechaControl;
        }

        public DateTime FechaControl { get; set; }

        public char Diag { get; set; }

        public DateTime FechaProbParto { get; set; }

        public int NumServicio { get; set; }

        public DateTime FechaServicio { get; set; }

        public double Grasa { get; set; }

        public double Leche { get; set; }

        public string Registro { get; set; }

        public string Nombre { get; set; }

        public double ProdGrasa { get; set; }

        public double ProdLeche { get; set; }

        public int Dias { get; set; }

        public int NumLact { get; set; }
    }
}