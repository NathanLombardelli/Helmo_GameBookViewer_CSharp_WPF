
namespace GameBook.Commandes
{
    class Command
    {
        readonly int num;
        readonly string nom;

        public Command(int num,string nom) 
        {
            this.num = num;
            this.nom = nom;
        }

        public string GetNom() 
        {
            return this.nom;
        }

        public int GetNum()
        {
            return this.num;
        }


        virtual public void Execute() //virtual pour pouvoir override
        {
        }

    }
}
