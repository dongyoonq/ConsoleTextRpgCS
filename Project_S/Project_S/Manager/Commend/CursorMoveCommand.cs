using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class CursorMoveUpCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorTop > 0)
                Console.CursorTop--;
        }
    }

    public class CursorMoveDownCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorTop < Console.WindowHeight - 1)
                Console.CursorTop++;
        }
    }

    public class CursorMoveLeftCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorLeft > 0)
                Console.CursorLeft--;
        }
    }

    public class CursorMoveRightCommand : ICommand
    {
        public void Execute()
        {
            if (Console.CursorLeft < Console.WindowWidth - 1)
                Console.CursorLeft++;
        }
    }
}
