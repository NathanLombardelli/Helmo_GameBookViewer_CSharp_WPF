using GameBook.Commandes;
using GameBookTerminal.Interact;
using System.Collections.Generic;

namespace GameBook
{
    public class Terminal
    {
        public static void Main() {
            

            //création des commandes.
            ReadBookCommand readBook = new ReadBookCommand(1, "Lancer la lecture");
            ExitCommand exit = new ExitCommand(2, "Sortir du programme");

            List<Command> listCommand = new List<Command> {readBook,exit};

            //Menu
            int reponse;

            do
            {
                for (int i = 0;i < listCommand.Count ; i++) 
                {
                    User.Print((i+1)+". "+ listCommand[i].GetNom());
                }

                reponse = User.ReadInt();

                switch (reponse)
                {
                    case 1:
                        listCommand[reponse - 1].Execute();
                        break;
                    case 2:
                        listCommand[reponse - 1].Execute();
                        break;
                }
            } while (reponse != 2);


        }
    }
}
