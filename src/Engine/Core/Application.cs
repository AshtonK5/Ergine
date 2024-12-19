
namespace Ergine.Core
{
    public abstract class Application<T> : Window
        where T : class, new()
    {
        private static T? m_Application;

        public static T? Instance
        {
            get;
            private set;
        } = m_Application;

        public Application()
        {
            WindowContext.Closed += Shutdown;

        }

        public static void Init()
        {
            if (m_Application == null)
                m_Application = new T();
        }

        public virtual void Shutdown()
        {
            if (m_Application != null)
                m_Application = null;
        }
    }
}
