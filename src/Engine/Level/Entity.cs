
namespace Ergine.Level
{
    public abstract class Entity
    {
        private List<Component> m_Components = new List<Component>();

        public T RegisterComponent<T>()
            where T : Component, new()
        {
            T registeredComponent = new T();
            registeredComponent.OnInit(this);
            m_Components.Add(registeredComponent);
            Console.WriteLine($"Componen Registered {registeredComponent.GetType().Name}");
            return registeredComponent;
        }

        public void GetComponent<T>()
            where T : Component
        {

        }

        public void RemoveComponent<T>()
            where T : Component
        {

        }
    }
}
