using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub_konzol
{
    class Befizetes
    {
         DateTime datum;
        int osszeg;
        public Befizetes(DateTime datum,int osszeg)
        {
            this.datum = datum;
            this.osszeg = osszeg;
           

        }
        public DateTime Datum { get => datum; }
        public int Osszeg { get => osszeg; }
    }
   
}
