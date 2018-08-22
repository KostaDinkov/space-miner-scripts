using System.Collections.Generic;


namespace Assets.Scripts.Commands
{
    class CommandQueue
    {
        private Queue<Command> commands;

        public CommandQueue()
        {
            this.commands = new Queue<Command>();
        }

        public void Enqueue(Command command)
        {
            this.commands.Enqueue(command);
        }

        public void Execute()
        {
            
            var command = commands.Dequeue();
            command.Execute();
        }

        public bool IsEmpty()
        {
            return this.commands.Count == 0;
        }

        public void Clear()
        {
            this.commands.Clear();
        }
    }
}
