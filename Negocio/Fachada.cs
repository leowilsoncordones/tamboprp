using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class Fachada
    {
        private static Fachada _instance;

        private Fachada()
        {
        }

        public static Fachada Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Fachada();
                }
                return _instance;
            }
        }

    }
}
