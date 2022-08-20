using Silk.NET.OpenGL;
using System.Diagnostics;
using System.Drawing;

namespace Game
{
    internal sealed class Scene
    {
        private readonly float[] vertices =
        {
            -0.5f, -0.5f, 0,
            0.5f, -0.5f, 0,
            0, 0.5f, 0
        };

        private uint vbo;
        private uint vao;
        private uint shaderProgram;

        public unsafe void Initialize(GL context)
        {
            vbo = context.GenBuffer();
            vao = context.GenVertexArray();

            context.BindVertexArray(vao);

            context.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
            context.BufferData(BufferTargetARB.ArrayBuffer, new ReadOnlySpan<float>(vertices), BufferUsageARB.StaticDraw);

            context.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
            context.EnableVertexAttribArray(0);

            var vs = @"
            #version 330 core
            layout (location = 0) in vec3 pos;

            void main() {
               gl_Position = vec4(pos, 1);
            }";

            var vertexShader = context.CreateShader(ShaderType.VertexShader);
            context.ShaderSource(vertexShader, vs);
            context.CompileShader(vertexShader);
            Debug.Write(context.GetShaderInfoLog(vertexShader));

            var fs = @"
            #version 330 core
            out vec4 color;

            void main()
            {
                color = vec4(1.0f, 0.5f, 0.2f, 1.0f);
            }";

            var fragmentShader = context.CreateShader(ShaderType.FragmentShader);
            context.ShaderSource(fragmentShader, fs);
            context.CompileShader(fragmentShader);
            Debug.Write(context.GetShaderInfoLog(fragmentShader));

            shaderProgram = context.CreateProgram();
            context.AttachShader(shaderProgram, vertexShader);
            context.AttachShader(shaderProgram, fragmentShader);
            context.LinkProgram(shaderProgram);
            Debug.Write(context.GetProgramInfoLog(shaderProgram));

            context.DetachShader(shaderProgram, vertexShader);
            context.DeleteShader(vertexShader);
            context.DetachShader(shaderProgram, fragmentShader);
            context.DeleteShader(fragmentShader);
        }

        public void Draw(GL context)
        {
            context.Clear(ClearBufferMask.ColorBufferBit);
            context.ClearColor(Color.CadetBlue);

            context.UseProgram(shaderProgram);
            context.BindVertexArray(vao);
            context.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}