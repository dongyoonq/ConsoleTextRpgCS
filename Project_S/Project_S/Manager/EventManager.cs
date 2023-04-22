using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class EventManager
    {
        private string eventName;
        object? eventData;

        private static EventManager Inst;
        public static EventManager GetInstance()
        {
            return Inst ??= new EventManager();
        }

        public void Run()
        {

        }
    }
}
