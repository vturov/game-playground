using System.Diagnostics;
using Silk.NET.OpenGL;

namespace Game.Core;

internal sealed class ShaderProgram
{
    private readonly GL context;
    private bool isInitialized;
    private uint id;

    public ShaderProgram(GL context)
    {
        this.context = context;
    }

    public void Use()
    {
        if (!isInitialized)
        {
            Initialize();
            isInitialized = true;
        }

        context.UseProgram(id);
    }

    private void Initialize()
    {
        var vs = File.ReadAllText("content\\shaders\\quad.vs");
        var vertexShader = context.CreateShader(ShaderType.VertexShader);
        context.ShaderSource(vertexShader, vs);
        context.CompileShader(vertexShader);
        Debug.Write(context.GetShaderInfoLog(vertexShader));

        var fs = File.ReadAllText("content\\shaders\\quad.ps");
        var fragmentShader = context.CreateShader(ShaderType.FragmentShader);
        context.ShaderSource(fragmentShader, fs);
        context.CompileShader(fragmentShader);
        Debug.Write(context.GetShaderInfoLog(fragmentShader));

        id = context.CreateProgram();
        context.AttachShader(id, vertexShader);
        context.AttachShader(id, fragmentShader);

        context.LinkProgram(id);
        Debug.Write(context.GetProgramInfoLog(id));

        context.DetachShader(id, vertexShader);
        context.DetachShader(id, fragmentShader);

        context.DeleteShader(vertexShader);
        context.DeleteShader(fragmentShader);
    }
}