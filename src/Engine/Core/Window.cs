using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Ergine.Core
{
    public abstract class Window : IDisposable
    {
        public Sdl2Window WindowContext
        {
            get;
            private set;
        }

        public GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        public Window()
        {
            WindowCreateInfo windowCreateInfo = new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowWidth = 960,
                WindowHeight = 540,
                WindowTitle = "Ergine Application"
            };

            WindowContext = VeldridStartup.CreateWindow(ref windowCreateInfo);

            GraphicsDeviceOptions graphicsDeviceOptions = new GraphicsDeviceOptions()
            {
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true,
            };

            GraphicsDevice = VeldridStartup.CreateGraphicsDevice(WindowContext, graphicsDeviceOptions);

            WindowContext.KeyDown += OnKeyDown;
            WindowContext.KeyUp += OnKeyUp;
            WindowContext.Moved += OnMoved;
            WindowContext.Closed += OnClosed;
            WindowContext.Closing += OnClosing;
            WindowContext.DragDrop += OnDragDrop;
            WindowContext.Exposed += OnExposed;
            WindowContext.FocusGained += OnFocusGained;
            WindowContext.FocusLost += OnFocusLost;
            WindowContext.Hidden += OnHidden;
            WindowContext.MouseDown += OnMouseDown;
            WindowContext.MouseEntered += OnMouseEntered;
            WindowContext.MouseLeft += OnMouseLeft;
            WindowContext.MouseMove += OnMouseMove;
            WindowContext.MouseUp += OnMouseUp;
            WindowContext.MouseWheel += OnMouseWheel;
            WindowContext.Resized += OnResized;
            WindowContext.Shown += OnShown;

            while (WindowContext.Exists) OnRender();
            Dispose();
        }

        protected virtual void OnKeyDown(KeyEvent Key) { }
        protected virtual void OnKeyUp(KeyEvent Key) { }
        protected virtual void OnMoved(Point point) { }
        protected virtual void OnClosed() { }
        protected virtual void OnClosing() { }
        protected virtual void OnDragDrop(DragDropEvent draggedEvent) { }
        protected virtual void OnExposed() { }
        protected virtual void OnFocusGained() { }
        protected virtual void OnFocusLost() { }
        protected virtual void OnHidden() { }
        protected virtual void OnMouseDown(MouseEvent mouseEvent) { }
        protected virtual void OnMouseEntered() { }
        protected virtual void OnMouseLeft() { }
        protected virtual void OnMouseMove(MouseMoveEventArgs mouseMoveArgs) { }
        protected virtual void OnMouseUp(MouseEvent mouseEvent) { }
        protected virtual void OnMouseWheel(MouseWheelEventArgs mouseWheelArgs) { }
        protected virtual void OnResized() { }
        protected virtual void OnShown() { }

        protected virtual void OnRender()
        {
            WindowContext.PumpEvents();

        }

        public virtual void Dispose()
        {

        }
    }
}
