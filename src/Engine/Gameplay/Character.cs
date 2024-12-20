using Ergine.Level;

namespace Ergine.Gameplay
{
    public class Character : Entity
    {
        public MovementComponent movementComponent;

        public Character()
        {
            Console.WriteLine("Initializing Character");
            movementComponent = RegisterComponent<MovementComponent>();

            movementComponent.Move();
            movementComponent.Look();
        }
    }
}
