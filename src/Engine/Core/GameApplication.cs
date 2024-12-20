
using System.Numerics;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;


namespace Ergine.Core;


public class GameApplication : Application<GameApplication>
{
    private static CommandList?  m_CL;
    private static DeviceBuffer? m_VertexBuffer;
    private static DeviceBuffer? m_IndexBuffer;
    private static Shader[]?     m_Shaders;
    private static Pipeline?     m_Pipeline;


    private const string VertexCode = @"
        #version 450

        layout(location = 0) in vec2 vsPosition;
        layout(location = 1) in vec4 vsColor;
        
        layout(location = 0) out vec4 fsin_Color;
        
        void main()
        {
            gl_Position = vec4(vsPosition, 0, 1);
            fsin_Color = vsColor;
        }
    ";

    private const string FragmentCode = @"
        #version 450
        
        layout(location = 0) in vec4 fsin_Color;
        layout(location = 0) out vec4 fsout_Color;

        void main()
        {
            fsout_Color = fsin_Color;
        }
    ";


    protected override void OnShown()
    {
        CreateResources();

    }

    public void CreateResources()
    {
        ResourceFactory resourceFactory = GraphicsDevice.ResourceFactory;

        VertexPositionColor[] quadVertices =
        {
            new VertexPositionColor(new Vector2(-.75f,  .75f), RgbaFloat.Red),
            new VertexPositionColor(new Vector2( .75f,  .75f), RgbaFloat.Green),
            new VertexPositionColor(new Vector2(-.75f, -.75f), RgbaFloat.Blue),
            new VertexPositionColor(new Vector2( .75f, -.75f), RgbaFloat.Yellow),
        };

        ushort[] quadIndices = { 0, 1, 2, 3 };

        m_VertexBuffer = resourceFactory.CreateBuffer(new BufferDescription(4 * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
        m_IndexBuffer = resourceFactory.CreateBuffer(new BufferDescription(4 * sizeof(ushort), BufferUsage.IndexBuffer));
        GraphicsDevice.UpdateBuffer(m_VertexBuffer, 0, quadVertices);
        GraphicsDevice.UpdateBuffer(m_IndexBuffer, 0, quadIndices);
        VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
            new VertexElementDescription("vsPosition", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
            new VertexElementDescription("vsColor", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4)
        );

        ShaderDescription vertexShaderDescription = new ShaderDescription(
            ShaderStages.Vertex,
            Encoding.UTF8.GetBytes(VertexCode),
            "main"
        );

        ShaderDescription fragmentShaderDescription = new ShaderDescription(
            ShaderStages.Fragment,
            Encoding.UTF8.GetBytes(FragmentCode),
            "main"
        );

        m_Shaders = resourceFactory.CreateFromSpirv(
            vertexShaderDescription,
            fragmentShaderDescription
        );

        GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
        pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;
        pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
            depthTestEnabled: true,
            depthWriteEnabled: true,
            comparisonKind: ComparisonKind.LessEqual
        );
        pipelineDescription.RasterizerState = new RasterizerStateDescription(
            cullMode: FaceCullMode.Back,
            fillMode: PolygonFillMode.Solid,
            frontFace: FrontFace.Clockwise,
            depthClipEnabled: true,
            scissorTestEnabled: false
        );
        pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
        pipelineDescription.ResourceLayouts = System.Array.Empty<ResourceLayout>();
        pipelineDescription.ShaderSet = new ShaderSetDescription(
            vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
            shaders: m_Shaders
        );
        pipelineDescription.Outputs = GraphicsDevice.SwapchainFramebuffer.OutputDescription;
        m_Pipeline = resourceFactory.CreateGraphicsPipeline(pipelineDescription);

        m_CL = resourceFactory.CreateCommandList();
    }

    protected override void OnRender()
    {
        base.OnRender();

        if (m_CL != null)
        {
            m_CL.Begin();

            /* Setup rendering output */
            m_CL.SetFramebuffer(GraphicsDevice.SwapchainFramebuffer);

            /* Clear screen */
            m_CL.ClearColorTarget(0, RgbaFloat.Black);

            m_CL.SetVertexBuffer(0, m_VertexBuffer);
            m_CL.SetIndexBuffer(m_IndexBuffer, IndexFormat.UInt16);
            m_CL.SetPipeline(m_Pipeline);

            m_CL.DrawIndexed(
                    indexCount: 4,
                    instanceCount: 1,
                    indexStart: 0,
                    vertexOffset: 0,
                    instanceStart: 0
                );

            GraphicsDevice.SwapBuffers();

            m_CL.End();
            GraphicsDevice.SubmitCommands(m_CL);
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        m_Pipeline?.Dispose();
        m_CL?.Dispose();
        m_VertexBuffer?.Dispose();
        m_IndexBuffer?.Dispose();
        GraphicsDevice?.Dispose();
    }

    protected override void OnKeyDown(KeyEvent Key)
    {
        base.OnKeyDown(Key);

        if (Key.Key == Veldrid.Key.Escape)
        {
            WindowContext.Close();

        }
    }
}

public struct VertexPositionColor
{
    public Vector2 Position;
    public RgbaFloat Color;
    public VertexPositionColor(Vector2 position, RgbaFloat color)
    {
        Position = position;
        Color = color;
    }
    public const uint SizeInBytes = 24;
}
