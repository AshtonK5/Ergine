
namespace Ergine.Level
{
    public class Component
    {
        public Entity entity
        {
            get;
            private set;
        }

        public virtual void OnInit(Entity entity)
        {
            this.entity = entity;

        }
    }
}
