using System;
using System.Collections.Generic;

namespace GameBook.Domain
{
    public class Book
    {
        private string _title;
        private List<Paragraph> _paragraphs;

        public Book(String Titre, List<Paragraph> para)
        {
            this._title = Titre;
            this._paragraphs = para;
        }


        public string Titre
        {
            get => _title;
            set => _title = value;
        }
        public List<Paragraph> Paragraphs
        {
            get => _paragraphs;
            set => _paragraphs = value;
        }

        public string GetImgPara(int numPara)
        {
            for (int i = 0; i < _paragraphs.Count; i++) // le numéros du paragraph ne correspond pas toujour a sa position.
            {
                if (_paragraphs[i].NumPara == numPara)
                {
                    return _paragraphs[i].Image;
                }
            }
            return null;
        }


        public string GetTxtPara(int numPara) 
        {
            for (int i = 0; i<_paragraphs.Count ;i++) // le numéros du paragraph ne correspond pas toujour a sa position.
            {
                if (_paragraphs[i].NumPara == numPara) {
                    return _paragraphs[i].Text;
                }
            }
            return null;
        }


        internal List<Choix> GetChoixPara(int numPara)
        {
            for (int i = 0; i < _paragraphs.Count; i++) // le numéros du paragraph ne correspond pas toujour a sa position.
            {
                if (_paragraphs[i].NumPara == numPara)
                {
                    return this._paragraphs[i].Choix;
                }
            }
            return null;
        }


        internal int GetNumLastPara()
        {
            return this._paragraphs.Count;
        }

        internal void ClearParagraphs()
        {
            Paragraphs = new List<Paragraph>();
        }

        internal void AddParagraph(Paragraph paragraph)
        {
            Paragraphs.Add(paragraph);
        }

        public bool IsEmpty()
        {
            if (Paragraphs.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
