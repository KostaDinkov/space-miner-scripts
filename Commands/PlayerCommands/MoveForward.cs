
namespace Assets.Scripts.Commands.PlayerCommands
{
    public class MoveForward:Command
    {
        public MoveForward(Playercontroller playerController) : base(playerController)
        {
        }

        public override void Execute()
        {
            this.receiver.StartCoroutine(((Playercontroller)this.receiver).MoveForward());
        }
    }
}
