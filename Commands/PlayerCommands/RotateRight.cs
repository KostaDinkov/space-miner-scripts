namespace Assets.Scripts.Commands.PlayerCommands
{
    public class RotateRight : Command
    {
        public RotateRight(Playercontroller playerController) : base(playerController)
        {
        }

        public override void Execute()
        {
            ((Playercontroller) this.receiver).RotateRight();
        }
    }
}