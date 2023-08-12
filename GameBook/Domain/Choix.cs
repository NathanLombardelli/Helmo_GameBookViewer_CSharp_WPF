using System;

namespace GameBook.Domain
{
    public class Choix
    {
        private readonly int _redirectionP;
        private readonly string _choixTxt;

        public Choix(int redirection, String txt)
        {
            this._redirectionP = redirection;
            this._choixTxt = txt;
        }


        public int Redirection => _redirectionP;

        public string ChoixTxt => _choixTxt;



    }
}
