using System;

namespace Project_S
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Game.GetInstance().Init())
                Game.GetInstance().Run();
        }
    }
}
