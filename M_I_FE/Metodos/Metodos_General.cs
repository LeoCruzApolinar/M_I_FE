using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_I_FE.Metodos
{
    public class Metodos_General
    {
        public static bool EsNumero(string str)
        {
            return decimal.TryParse(str, out _);
        }
    }
}
