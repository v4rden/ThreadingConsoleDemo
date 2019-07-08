namespace ThreadingConsoleDemo
{
    using System;
    using System.Threading;

    class Program
    {
        private static ManualResetEvent _mre = new ManualResetEvent(false);
        private static AutoResetEvent _are = new AutoResetEvent(false);

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
                        break;
                    case 'a':
                        DoAutoResetDemo();
                        command = AskInput();
                        continue;
                    case 'm':
                        DoManualResetDemo();
                        command = AskInput();
                        continue;
                    default:
                        command = AskInput();
                        continue;
                }

                break;
            }
        }

        private static void DoDemo(EventWaitHandle eventWaitHandle, ThreadStart start)
        {
            for (var i = 0; i < 10; i++)
            {
                var t = new Thread(start) {Name = $"Thread+{i}"};
                t.Start();
            }

            var condition = true;

            while (condition)
            {
                Console.WriteLine("\n ===================== \n Press e to exit. Press any to set event");
                var key = Console.ReadKey();
                if (key.KeyChar == 'e')
                {
                    condition = false;
                }
                else
                {
                    eventWaitHandle.Set();
                }

                Thread.Sleep(300);
                eventWaitHandle.Reset();
            }
        }

        private static void ThreadWork(EventWaitHandle eventWaitHandle)
        {
            eventWaitHandle.WaitOne();
            Console.WriteLine($"Hello from {Thread.CurrentThread.Name}");
        }

        private static void DoManualResetDemo()
        {
            DoDemo(_mre, ThreadWorkWithManualReset);
        }

        private static void ThreadWorkWithManualReset()
        {
            ThreadWork(_mre);
        }

        private static void DoAutoResetDemo()
        {
            DoDemo(_are, ThreadWorkWithAutoResetEvent);
        }

        private static void ThreadWorkWithAutoResetEvent()
        {
            ThreadWork(_are);
        }
    }
}