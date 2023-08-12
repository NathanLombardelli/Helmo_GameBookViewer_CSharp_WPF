using System.Windows.Input;

namespace GameBookWPF.ViewModels
{
    class ChoixViewModel
    {
        public string Text { get; }
        public int Index { get; }
        public ICommand ChangeParaCourent { get; }


        public ChoixViewModel(string text,int pos,ICommand commande)
        {
            this.Text = text;
            this.Index = pos;
            this.ChangeParaCourent = commande;

        }

    }
}
