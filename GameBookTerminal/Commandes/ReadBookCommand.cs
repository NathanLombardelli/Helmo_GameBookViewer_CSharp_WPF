using GameBook.PresentationModel;
using GameBookTerminal.Interact;

namespace GameBook.Commandes
{
    class ReadBookCommand : Command
    {

        readonly GameBookPresentationModel GameBook;

        public ReadBookCommand(int num,string nom) : base(num, nom)
        {
            this.GameBook = new GameBookPresentationModel();
        }

        override
        public void Execute()
        {
            GameBook.ResetParcourt();
            int numParaEnCours = 0;
            User.Print("                " + GameBook.GetTitre); // affichage du titre.

            User.Print("1.Suivant   2.Quitter");
            int reponse = User.ReadInt();
            if (reponse == 1)
            {
                numParaEnCours += 1;
                GameBook.AddParcourt(numParaEnCours); //on ajoute le para 1 au parcourt.
                int choix = 0;
                do
                {

                    User.Print(numParaEnCours + ". " + GameBook.GetTxtPara(numParaEnCours));
                    if (GameBook.GetChoixPara(numParaEnCours).Count > 0)
                    {
                        for (int i = 0; i < GameBook.GetChoixPara(numParaEnCours).Count; i++)
                        {
                            User.Print((i + 1) + "." + GameBook.GetTxtChoixPara(numParaEnCours, i + 1)+"=>"+GameBook.GetRedirChoixPara(numParaEnCours,i+1)); //afficher les choix du paragraph.
                        }
                        User.Print("5.Retour");
                        User.Print("6.Quitter la lecture");

                        choix = User.ReadInt();
                        if (choix <= GameBook.GetChoixPara(numParaEnCours).Count && choix > 0)
                        {
                            numParaEnCours = GameBook.GetRedirChoixPara(numParaEnCours, choix); //le paragraph en cours de lecture devient celui du choix choisis.
                            GameBook.AddParcourt(numParaEnCours);// on ajoute le para du choix choisi au parcourt.
                        }
                        else if (choix == 5 && GameBook.GetParaCourant() != 1) // retour arriere pas autoriser au para1.
                        {
                            GameBook.SuppLastInParcourt();
                            numParaEnCours = GameBook.GetParaCourant();
                        }
                        else if (choix != 6 && numParaEnCours != 1) //les autres choix n'existent pas.
                        {
                            User.Print("Choix inconnu");
                        }
                    }
                    else //cas chapitre de fin
                    {
                        User.Print("vous êtes arriver à la fin du livre!"); //si pas de choix possible => dernier paragraph du livre.
                        User.Print("voulez-vous recommencer une lecture ?");
                        User.Print("1.Oui   2.non   3.Retourner au paragraph précédent");
                        int continuer = User.ReadInt();
                        if (continuer == 2)
                        {
                            choix = 6; //permet de quitter la lecture.
                        }
                        else if (continuer == 3)
                        {
                            GameBook.SuppLastInParcourt();
                            numParaEnCours = GameBook.GetParaCourant();
                        }
                        else
                        {
                            choix = 6; //résous prob réaffiche le dernier para s demande de relecture.
                            Execute(); //relancer une lecture.
                        }
                    }
                } while (choix != 6);

            }
            else { }//autre chose que 1 => quitter

        }

    }
}
