namespace ThreadingConsoleDemo
{
    using System;
    using System.Threading;

    public class ManualResetEventDemo : DemoCommand
    {
        private readonly ManualResetEvent _mre;

        public ManualResetEventDemo() :
            base("Manual reset event Demo",
                "\n---->Press e to exit. Press any to set event")

        {
            _mre = new ManualResetEvent(false);
        }

        private void DoDemo(EventWaitHandle eventWaitHandle, ThreadStart start)
        {
            for (var i = 0; i < 10; i++)
            {
                var t = new Thread(start) {Name = $"Thread+{i}"};
                t.Start();
            }

            var condition = true;

            while (condition)
            {
                PrintHelpMessage();
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

        private static void SimulateWorkInThread(EventWaitHandle eventWaitHandle)
        {
            eventWaitHandle.WaitOne();
            Console.WriteLine($"\nHello from {Thread.CurrentThread.Name}");
        }

        protected override void DoDemo()
        {
            DoDemo(_mre, ThreadWorkWithManualReset);
        }

        private void ThreadWorkWithManualReset()
        {
            SimulateWorkInThread(_mre);
        }
    }
}