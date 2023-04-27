using System;

namespace Project_S
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Core.GetInstance().Init())
                Core.GetInstance().Run();

            //Core.GetInstance().Init();

            //Player player = new Player("newPlayer");
        }
    }
}
