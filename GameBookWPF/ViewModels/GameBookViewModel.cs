using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GameBook.PresentationModel;
using GameBookWPF.Commands;
using GameBookWPF.Files;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameBookWPF.ViewModels
{
    class GameBookViewModel:INotifyPropertyChanged
    {
        private string _title;

        private string _numPara;

        private string _paraContent;

        private string _message;

        private string _visite;

        private string _image;

        private GameBookPresentationModel _pm = new GameBookPresentationModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ChoixViewModel> ListChoix { get; set; } = new ObservableCollection<ChoixViewModel>(); //list des choix possible pour le para en court.

        public GameBookPresentationModel PM
        {
            get => _pm;
            set
            {
                if (value != _pm)
                {
                    _pm = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PM"));
                }
            }
        } //creation du model de présentation(chargement/création du livre).

        public GameBookViewModel() //initialisation des propriéte
        {

            //OpenBook();
            _title = PM.GetTitre; //initialization titre
            if (PM.GetParaCourant() != -1)
            {
                _numPara = "Paragraph " + PM.GetParaCourant(); // initialization num paragraph en court de lecture.
            }
            else
            {
                _numPara = "";
            }

            _paraContent = PM.GetTxtPara(PM.GetParaCourant()); // initialization texte du para en court de lecture.
            _message = ""; // initialization du message.
            _image = "";
            ////Commandes////
            ChoixCommand = ParameterizedRelayCommand<int>.From(ChangeParaCourent); //assignation de la commande a sa méthode.
            GoBack = ParameterlessRelayCommand.From(this.Retour);//assignation de la commande a sa méthode.
            GoPara = ParameterlessRelayCommand.From(this.GoParagraph);//assignation de la commande a sa méthode.
            Open = ParameterlessRelayCommand.From(this.OpenBook);
            Save = ParameterlessRelayCommand.From(this.SaveProgress);


            LoadChoix(); //charger la list des choix.
            LoadBackGround(); //charger l'image de fond.
        }


        public ICommand ChoixCommand { get; } //command pour les choix.
        public ICommand GoBack { get; } //command pour le retour arrière.
        public ICommand GoPara { get; } //command pour aller directement a un paragraph.
        public ICommand Open { get; } //Command pour ouvrir un livre.
        public ICommand Save { get; } //Command pour sauvegarder l'état d'avancement d'un livre.
       
        
        private void SaveProgress()
        {
            if (!PM.IsEmpty())
            {
                JsonSerializer serializer = new JsonSerializer();

                

                using (StreamWriter sw =
                    new StreamWriter(Titre + ".json") //chemin a changer...
                ) //chemin a changer... 
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, PM.Livre);
                }


                //////////parcourt///////

                string text = File.ReadAllText("Parcourt.txt"); //chemin a changer. vers Parcourt.txt dans le fichier du projet.

                string[] subs = text.Split(';');

                bool exist = false;

                for (int i = 0; i < subs.Length; i++)
                {
                    if (subs[i].Equals(PM.GetTitre))
                    { 
                        exist = true;
                        subs[i+1] = string.Join(",", PM.Parcourt);
                    }
                }

                StreamWriter sWriter = new StreamWriter("Parcourt.txt"); //chemin a changer. vers Parcourt.txt dans le fichier du projet.

                for (int i = 0; i < subs.Length; i++)
                {
                    if(!subs[i].Equals("")) sWriter.WriteLine(subs[i] + ";");
                }

                if (exist == false)
                {
                    sWriter.WriteLine(PM.GetTitre + ";");
                    sWriter.WriteLine(string.Join(",", PM.Parcourt) + ";");
                }

                sWriter.Close();


                Message = "Sauvegarde réussie";
            }

        }

        private void OpenBook()
        {
            SaveProgress(); //l'ors de l'ouverture d'une lecture, j'enregistre la lecture actuel.


            FileResourceChooser fileChooser = new FileResourceChooser(); //crée un fileChooser.
            string path = fileChooser.ResourceIdentifier; //ouvre une fenêtre et récupère le chemin vers l'élément sélectioner.
            if (path != "" && path != null)
            {
                try
                {
                    using (StreamReader file = File.OpenText(path))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                        PM.OpenBookTitre((string)o2["Titre"]);
                        PM.OpenBookParagraphs((JArray)o2["Paragraphs"]);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Message = "Fichier non trouver";
                }

                //parcourt

                StreamReader sr = new StreamReader(@"C:\Users\Ghost\Documents\UE11_POO C#\Poo\Projet\GameBook\Parcourt.txt"); //chemin a changer. vers Parcourt.txt dans le fichier du projet.

                string line = sr.ReadLine();
                bool exist = false;

                while (line != null)
                {
                    if (line.Equals(PM.GetTitre))
                    {
                        line = sr.ReadLine();
                        List<string> parcourtString = line.Split(',').ToList();
                        List<int> parcourt = new List<int>();
                        for (int i = 0; i < parcourtString.Count; i++)
                        {
                            parcourt.Add(Int32.Parse(parcourtString[i]));
                        }

                        PM.Parcourt = parcourt;
                        PM.ParaVisite = parcourt;
                        exist = true;
                    }

                    line = sr.ReadLine();
                }

                if (!exist)
                {
                    PM.Parcourt = new List<int> { 1 };
                    PM.ParaVisite = new List<int> { 1 };
                }

                sr.Close();


                //////////////////////////////////////////

                ReloadPm(); //recharge les éléments par rapport a la nouvelle vue.
            }
        }

        private void ReloadPm()
        {
            Titre = PM.GetTitre;
            ParaCourent = "Paragraph " + PM.GetParaCourant(); 
            ParaContent = PM.GetTxtPara(PM.GetParaCourant());
            Message = "Chargement du livre réussit"; 
            BackGround = PM.GetImgPara(PM.GetParaCourant());
            LoadChoix(); //charger la list des choix.
        }

        private void GoParagraph()
        {
            try
            {
                if (Int32.Parse(AllezPara) <= PM.GetLastPara() && Int32.Parse(AllezPara) > 0)
                {
                    PM.AddParcourt(Int32.Parse(AllezPara)); // ajout du paragraph de redirection dans le parcourt.
                    ParaCourent = "Paragraph " + PM.GetParaCourant(); //changement de l'affichage du paragraph courent.
                    ParaContent = PM.GetTxtPara(PM.GetParaCourant()); //changement de l'affichage du contenu du paragraph courent.
                    LoadChoix(); //chargement de la list des choix du paragraph de redirection.
                    ChangeMessage(); //met a jour le message.
                    PM.AddParaVisite(PM.GetParaCourant()); //ajoute le paragraph courent a la list des paragraph visiter.
                    LoadBackGround();
                }
            }catch (Exception) { }
        }

        private void ChangeParaCourent(int choix) //methode executer l'ors de la selection d'un choix.
        {
            
            PM.AddParcourt(PM.GetRedirChoixPara(PM.GetParaCourant(), choix)); // ajout du paragraph de redirection dans le parcourt.
            ParaCourent = "Paragraph " + PM.GetParaCourant(); //changement de láffichage du paragraph courent.
            ParaContent = PM.GetTxtPara(PM.GetParaCourant()); //changement de l'affichage du contenu du paragraph courent.
            LoadChoix(); //chargement de la list des choix du paragraph de redirection.
            ChangeMessage(); //met a jour le message.
            PM.AddParaVisite(PM.GetParaCourant()); //ajoute le paragraph courent a la list des paragraph visiter.
            LoadBackGround();
        }

        private void Retour()
        {
            
            if (!PM.ParcourtIsStart()) //vérification, si on peut faire un retour arrière.
            {
                PM.SuppLastInParcourt(); //supprime le paragraph actuel du parcourt
                ParaCourent = "Paragraph " + PM.GetParaCourant(); //changement deláffichage du paragraph courent.
                ParaContent = PM.GetTxtPara(PM.GetParaCourant()); //changement de l'affichage du contenu du paragraph courent.
                LoadChoix(); //chargement de la list des choix du paragraph de redirection.
                LoadBackGround();
            }
            ChangeMessage();
        }

        private void ChangeMessage()
        {
            if (PM.ParaVisited(PM.GetParaCourant()))
            {
                Message = "Paragraph déjà visité";
            }
            else
            {
                Message = "";
            }
        }

        private void LoadChoix()
        {
            ListChoix.Clear();//remise a 0 de la list.

            int numParaCourent = PM.GetParaCourant(); //donne le numéros du para courent.
            if (!PM.IsEmpty())
            {
                for (int i = 1;
                    i <= PM.GetNbChoixPara(numParaCourent);
                    i++) //crée un bouton pour chaque choix du paragraph courent
                {
                    ChoixViewModel choix = new ChoixViewModel(i + ". " + PM.GetTxtChoixPara(numParaCourent, i), i,
                        ChoixCommand); // création du choix.
                    ListChoix.Add(choix); //ajout du choix a l'OservableList
                }
            }

        }

        private void LoadBackGround()
        {
            if (!PM.IsEmpty())
            {
                if (PM.ParaImaged(PM.GetParaCourant())) //changer le fond si le paragraph possaide une illustration.
                {
                    BackGround = PM.GetImgPara(PM.GetParaCourant()); //changer le fond par l'illustration du paragraph.
                }
                else
                {
                    BackGround = ""; // si le paragraph n'a pas d'illustration alors l'aisser le fond vide.
                }
            }
        }


        public string Titre
        {
            get => _title;

            set
            {
                if (value != _title)
                {
                    _title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Titre"));
                }
            }
        }

        public string BackGround
        {
            get => _image;

            set
            {
                if (value != _image)
                {
                    _image = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackGround"));
                }
            }
        }

        public string ParaCourent
        {
            get => _numPara;

            set
            {
                if (value != _numPara)
                {
                    _numPara = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ParaCourent"));
                }
            }
        }

        public string ParaContent
        {
            get => _paraContent;

            set
            {
                if (value != _paraContent)
                {
                    _paraContent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ParaContent"));
                }
            }
        }

        public string Message
        {
            get => _message;

            set
            {
                if (value != _message)
                {
                    _message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
        }

        public string AllezPara
        {
            get => _visite;

            set
            {
                if (value != _visite)
                {
                    _visite = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllezPara"));
                }
            }
        }

    }


}
