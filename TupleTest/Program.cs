using System;

namespace TupleTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var actionCommands = (attack: "attack", skip: "skip");

            Console.WriteLine(actionCommands.attack);

        }
    }
}
