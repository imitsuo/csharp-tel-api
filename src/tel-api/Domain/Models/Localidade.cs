using System.Collections.Generic;

namespace tel_api.Domain.Models
{
    public class Localidade
    {
        public static HashSet<string> Localidades 
        {
            get 
            {
                return new HashSet<string>()
                {
                    "011","012","013","014","015","016","017","018","019","021","022","024","027","028",
                    "031","032","033","034","035","037","038","041","042","043","044","045","046","047",
                    "048","049","051","053","054","055","061","062","063","064","065","066","067","068",
                    "069","071","073","074","075","077","079","081","082","083","084","085","086","087",
                    "088","089","091","092","093","094","095","096","097","098","099"
                };    
            }
        }
    }
}