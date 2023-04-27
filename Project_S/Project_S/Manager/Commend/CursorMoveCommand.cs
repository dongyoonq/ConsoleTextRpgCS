using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class MoveUpCommand : ICommand
    {
        public void Execute()
        {
            if(Console.CursorTop > 0)
                Console.CursorTop--;
        }
    }

    public class MoveDownCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorTop < Console.WindowHeight - 1)
                Console.CursorTop++;
        }
    }

    public class MoveLeftCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorLeft > 0)
                Console.CursorLeft--;
        }
    }

    public class MoveRightCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorLeft < Console.WindowWidth - 1)
                Console.CursorLeft++;
        }
    }
}
