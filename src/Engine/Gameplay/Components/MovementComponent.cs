using Ergine.Level;

namespace Ergine.Gameplay
{
    public class MovementComponent : Component
    {

        public void Move()
        {
            Console.WriteLine("Move called");
            if ((Character)entity != null)
            {
                Console.WriteLine("Moving..");
            }
        }

        public void Look()
        {

        }
    }
}
