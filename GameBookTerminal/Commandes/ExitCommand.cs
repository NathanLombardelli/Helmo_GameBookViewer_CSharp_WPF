using System;

namespace GameBook.Commandes
{
    class ExitCommand : Command
    {

        public ExitCommand(int num, string nom) : base(num, nom) // constructeur avec base = au super en java.
        {
        }

        override
        public void Execute()
        {
            Console.WriteLine("Au revoir");
            Environment.Exit(0);
        }


    }
}
