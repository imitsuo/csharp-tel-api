using System.Collections.Generic;

namespace tel_api.Domain.Models
{
    public class Plano
    {
        public static string TARIFA_FIXA = "TARIFA_FIXA";
        public static string FALE_MAIS_30 = "FALE_MAIS_30";
        public static string FALE_MAIS_60 = "FALE_MAIS_60";
        public static string FALE_MAIS_120 = "FALE_MAIS_120";
        public static HashSet<string> Planos 
        { 
            get { return new HashSet<string> { TARIFA_FIXA, FALE_MAIS_30, FALE_MAIS_60, FALE_MAIS_120 }; }
        }

        public static HashSet<string> PlanosFaleMais
        { 
            get { return new HashSet<string> { FALE_MAIS_30, FALE_MAIS_60, FALE_MAIS_120 }; }
        }
    }
}