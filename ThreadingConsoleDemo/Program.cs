namespace ThreadingConsoleDemo
{
    using System;
    using System.Threading;

    class Program
    {
        private const string Greeting =
            "\n ===================== \n Chose m for manual reset event, a for auto reset event, e for exit";

        static void Main()
        {
            var command = AskInput();
            ProcessInput(command);
        }

        private static char AskInput()
        {
            Console.WriteLine(Greeting);
            return Console.ReadKey().KeyChar;
        }

        private static void ProcessInput(char command)
        {
            while (true)
            {
                switch (command)
                {
                    case 'e':
                        Console.WriteLine("Exiting ...");
                        break;
                    case 'a':
                        command = ProcessCommandAndAskNext(new AutoResetEventDemo());
                        continue;
                    case 'm':
                        command = ProcessCommandAndAskNext(new ManualResetEventDemo());
                        continue;
                    default:
                        command = AskInput();
                        continue;
                }

                break;
            }
        }

        private static char ProcessCommandAndAskNext(IDemoCommand command)
        {
            command.Start();
            return AskInput();
        }
    }
}