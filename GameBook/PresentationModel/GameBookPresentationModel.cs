using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameBook.Domain;
using Newtonsoft.Json.Linq;

namespace GameBook.PresentationModel
{
    public class GameBookPresentationModel
    {
        private Book _livre;
        private List<int> _parcourt; //parcourt de l'utilsateur.
        private List<int> _paraVisite; //chapitres déjà lu.

        public  GameBookPresentationModel()
        {
            ///création du livre.
            /*
                //Choix
                 Choix c2 = new Choix(2, "aller para 2 jhbcsajgfffffffffffffffffffffffffff");
                 Choix c3 = new Choix(3, "aller para 3");
                 Choix c4 = new Choix(4, "aller para 4");
                 List<Choix> listChoixPara1 = new List<Choix> {c2,c3};
                 List<Choix> listChoixPara2 = new List<Choix> {c3,c4};
                 List<Choix> listChoixPara3 = new List<Choix> {c4};
                 List<Choix> listChoixVide = new List<Choix>();

                 //paragraphes
                 Paragraph para1 = new Paragraph(1, "paragraphe 1", listChoixPara1,"");
                 Paragraph para2 = new Paragraph(2, "paragraphe 2", listChoixPara2, "C:\\Users\\Ghost\\Pictures\\Fond d'ecran\\anime\\Anime.jpg");
                 Paragraph para3 = new Paragraph(3, "paragraphe 3", listChoixPara3, "https://upload.wikimedia.org/wikipedia/commons/3/30/Googlelogo.png");
                 Paragraph para4 = new Paragraph(4, "paragraphe 4 (Fin)", listChoixVide,"");
                 List<Paragraph> listPara = new List<Paragraph>
                 {
                     para1,para2,para3,para4
                 };
            */
                 //Book
                 this._livre = new Book("Veuillez ouvrir un livre", new List<Paragraph>());

                 //creation parcourt
                 _parcourt = new List<int>();

                 //creation de la list des chapitres visite.
                 _paraVisite = new List<int>();

            

        }


        public Book Livre
        {
            get => _livre;
            set => _livre = value;
        }

        public List<int> Parcourt
        {
            get => _parcourt;
            set => _parcourt = value;
        }

        public List<int> ParaVisite
        {
            get => _paraVisite;
            set => _paraVisite = value;
        }



        public string GetTitre
        {
            get => Livre.Titre;
            set => Livre.Titre = value;
        }

        public int GetLastPara()
        { 
            return _livre.GetNumLastPara();
        }

            public void AddParaVisite(int numPara)
        {
            if (!_paraVisite.Contains(numPara))
            {
                _paraVisite.Add(numPara);
            }
        }

        public string GetImgPara(int numPara)
        {
            return _livre.GetImgPara(numPara);
        }

        public bool ParaImaged(int numPara)
        {
            if (GetImgPara(numPara).Equals("") || GetImgPara(numPara) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ParaVisited(int numPara)
        {
            return _paraVisite.Contains(numPara);
        }

        public int GetNbChoixPara(int numParaCourent)
        {
            return _livre.GetChoixPara(numParaCourent).Count;

        }

        public void ResetParcourt()
        {
            this._parcourt = new List<int>();
        }

        public bool ParcourtIsStart()
        {
            
            if (_parcourt.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void OpenBookTitre(string titre)
        {
            Livre.Titre = titre;
            /* string txtJson = File.ReadAllText(path);
             dynamic json = JValue.Parse(txtJson);
             Livre.Titre = json.Titre; //ok
             List<Paragraph> list = new List<Paragraph>();
            */

        }

        public void OpenBookParagraphs( JArray tab)
        {
            Livre.ClearParagraphs();

            for (int i = 0; i < tab.Count; i++)
            {
                Livre.AddParagraph(new Paragraph((int)tab[i]["NumPara"], (string)tab[i]["Text"], OpenBookChoix((int)tab[i]["NumPara"],(JArray)tab[i]["Choix"]), (string)tab[i]["Image"]));
            }

        }

        private List<Choix> OpenBookChoix(int numPara, JArray tab)
        {

            List<Choix> list = new List<Choix>();

            for (int i = 0; i < tab.Count(); i++)
            {
                Choix c = new Choix((int)tab[i]["Redirection"], (string)tab[i]["ChoixTxt"]);
                list.Add(c);
            }

            return list;
        }

        public string GetTxtPara(int numP) 
        {
            return _livre.GetTxtPara(numP);
        }


        public List<Choix> GetChoixPara(int numPara) 
        {
            return _livre.GetChoixPara(numPara);
        }

        public string GetTxtChoixPara(int numPara,int numChoix) 
        {
           return GetChoixPara(numPara)[numChoix-1].ChoixTxt;
        }

        public int GetRedirChoixPara(int numPara,int numChoix) 
        {
            return GetChoixPara(numPara)[numChoix - 1].Redirection;
        }

        public void AddParcourt(int numChapitre) 
        {
            this._parcourt.Add(numChapitre);
        }

        public void SuppLastInParcourt() 
        {
            this._parcourt.RemoveAt(_parcourt.Count-1);
        }

        public int GetParaCourant() 
        {
            if (!Livre.IsEmpty())
            {
                return this._parcourt[_parcourt.Count - 1];
            }
            else
            {
                return -1;
            }
        }

        public bool IsEmpty()
        {
            return this.Livre.IsEmpty();
        }
    }
}
