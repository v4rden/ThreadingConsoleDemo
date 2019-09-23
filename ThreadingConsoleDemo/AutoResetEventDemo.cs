namespace ThreadingConsoleDemo
{
    using System;
    using System.Threading;

    public class AutoResetEventDemo : DemoCommand
    {
        private AutoResetEvent _are;

        public AutoResetEventDemo()
            : base("Auto reset event demo",
                "\n----> Press e key to exit. Press any key to set event")
        {
            _are = new AutoResetEvent(false);
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

        private void ThreadWork(EventWaitHandle eventWaitHandle)
        {
            eventWaitHandle.WaitOne();
            Console.WriteLine($"\nHello from {Thread.CurrentThread.Name}");
        }

        protected override void DoDemo()
        {
            DoDemo(_are, ThreadWorkWithAutoResetEvent);
        }

        private void ThreadWorkWithAutoResetEvent()
        {
            ThreadWork(_are);
        }
    }
}