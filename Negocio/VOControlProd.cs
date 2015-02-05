using System;

namespace Negocio
{
    public class VOControlProd
    {
        public VOControlProd()
        {
            
        }

        public VOControlProd  (string registro, int numLact, int dias, 
                                    double leche, double grasa, string fechaServicio, int numServicio, 
                                    char diag, string fechaProbParto, string fechaControl)
        {
            Registro = registro;
            NumLact = numLact;
            Dias = dias;
            Leche = leche;
            Grasa = grasa;
            //ProdLeche = Dias * Leche;
            //ProdGrasa = Dias * Grasa;
            FechaServicio = fechaServicio;
            NumServicio = numServicio;
            Diag = diag;
            FechaProbParto = fechaProbParto;

            FechaControl = fechaControl;
        }

        public VOControlProd(string registro, int numLact, int dias, double leche, double grasa, string fechaControl)
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

        public string FechaControl { get; set; }

        public char Diag { get; set; }

        public string FechaProbParto { get; set; }

        public int NumServicio { get; set; }

        public string FechaServicio { get; set; }

        public double Grasa { get; set; }

        public double Leche { get; set; }

        public string Registro { get; set; }

        public string Nombre { get; set; }

        public double ProdGrasa { get; set; }
        // PROD TOTAL GRASA EN ESTA LACTANCIA ACTUAL
        
        public double ProdLeche { get; set; }
        // PROD TOTAL LECHE EN ESTA LACTANCIA ACTUAL

        public int Dias { get; set; }

        public int NumLact { get; set; }
        

    }
}