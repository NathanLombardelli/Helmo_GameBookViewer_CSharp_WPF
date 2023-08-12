using System;
using System.Collections.Generic;


namespace GameBook.Domain
{
    public class Paragraph
    {

        private readonly int _numP;
        private readonly string _text;
        private readonly List<Choix> _choix;
        private readonly string _illustration;

        public Paragraph(int numPara, String txt, List<Choix> choix, string imagePath)
        {
            this._numP = numPara;
            this._text = txt;
            this._choix = choix;
            this._illustration = imagePath;
        }


        public int NumPara => _numP;

        public string Text => _text;

        public List<Choix> Choix => _choix;

        public string Image => _illustration;


    }
}
