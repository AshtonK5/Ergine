
namespace Ergine.Core
{
    public class Application<T>
        where T : class, new()
    {
        private static T? m_Application;

        public static T? Instance
        {
            get;
            private set;
        } = m_Application;

        internal static void Init()
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
