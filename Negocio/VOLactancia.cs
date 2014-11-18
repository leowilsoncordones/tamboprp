using System;

namespace Negocio
{
    public class VOLactancia
    {
        public VOLactancia()
        {
            
        }

        public VOLactancia(string registro, int numLact, int dias, double leche305, double grasa305, double leche365, double grasa365, double prodLeche, double prodGrasa)
        {
            Registro = registro;
            Numero = numLact;
            Dias = dias;
            Leche305 = leche305;
            Grasa305 = grasa305;
            Leche365 = leche365;
            Grasa365 = grasa365;
            ProdLeche = prodLeche;
            ProdGrasa = prodGrasa;
            if (ProdLeche > 0)
            {
                PorcentajeGrasa = (ProdGrasa/ProdLeche)*100;
                PorcentajeGrasa = Math.Round(PorcentajeGrasa, 2);
            }
            else PorcentajeGrasa = 0;
        }

        public double Grasa365 { get; set; }

        public double Leche365 { get; set; }

        public double Grasa305 { get; set; }

        public double Leche305 { get; set; }

        public double PorcentajeGrasa { get; set; }

        public string Registro { get; set; }

        public string Nombre { get; set; }

        public double ProdGrasa { get; set; }

        public double ProdLeche { get; set; }

        public int Dias { get; set; }

        public int Numero { get; set; }
    }
}