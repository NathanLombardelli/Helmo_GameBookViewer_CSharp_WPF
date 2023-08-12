using System;

namespace GameBookTerminal.Interact
{
    class User
    {

       public static void Print(string txt) 
        {
            Console.WriteLine(txt);
        }

        public static int ReadInt()
        {
           return int.Parse(Console.ReadLine());
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }


    }
}
