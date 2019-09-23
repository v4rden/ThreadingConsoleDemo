namespace ThreadingConsoleDemo
{
    using System;

    public abstract class DemoCommand : IDemoCommand
    {
        private readonly string _demoName;
        private readonly string _helpMessage;

        protected DemoCommand(string name, string helpMessage)
        {
            _demoName = name;
            _helpMessage = helpMessage;
        }

        public void Start()
        {
            PrintStartMessage();
            DoDemo();
            PrintFinishMessage();
        }

        protected abstract void DoDemo();

        protected void PrintHelpMessage()
        {
            Console.WriteLine(_helpMessage);
        }

        private void PrintStartMessage()
        {
            Console.WriteLine($"\n_____ Starting {_demoName} _____");
        }

        private void PrintFinishMessage()
        {
            Console.WriteLine($"\n_____ Finishing {_demoName} _____");
        }
    }
}