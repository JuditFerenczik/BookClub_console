using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub_konzol
{
    class Tag
    {
        List<Befizetes> befizetes;
        DateTime belepett;
        string csaladnev;
        int id;
        string nem;
        DateTime szuletett;
        string utonev;
        public Tag(int id, string csaladnev, string utonev, List<Befizetes> befizetes, string nem, DateTime szuletett, DateTime belepett)
        {
            this.id = id;
            this.csaladnev = csaladnev;
            this.utonev = utonev;
            this.Befizetes = befizetes;
            this.nem = nem;
            this.szuletett = szuletett;
            this.belepett = belepett;


        }
        public List<Befizetes> Befizetes { get => befizetes; set => befizetes = value; }
        public string Nev { get=> csaladnev + " " + utonev ; }
    }
}
