﻿using System.Diagnostics;
using System.Numerics;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = System.Drawing.Color;

namespace Game.Core;

internal sealed class Quad : IDrawableGameObject
{
    private readonly float[] vertices =
    {
        -0.5f, -0.5f, 0,
        0.4f,
        0, 0,

        0.5f, -0.5f, 0,
        0.3f,
        1, 0,

        -0.5f, 0.5f, 0,
        0.1f,
        0, 1,

        0.5f, 0.5f, 0,
        0.9f,
        1, 1
    };

    private readonly uint[] indices =
    {
        0, 1, 2,
        1, 3, 2
    };

    private readonly GL context;

    private bool isInitialized;
    private uint vbo;
    private uint ebo;
    private uint vao;
    private uint shaderProgram;
    private uint texture1;
    private uint texture2;

    public Quad(GL context)
    {
        this.context = context;
    }

    public unsafe void Initialize()
    {
        vbo = context.GenBuffer();
        ebo = context.GenBuffer();
        vao = context.GenVertexArray();

        context.BindVertexArray(vao);
        context.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
        context.BufferData(BufferTargetARB.ArrayBuffer, new ReadOnlySpan<float>(vertices), BufferUsageARB.StaticDraw);

        context.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);
        context.BufferData(BufferTargetARB.ElementArrayBuffer, new ReadOnlySpan<uint>(indices), BufferUsageARB.StaticDraw);

        context.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), null);
        context.EnableVertexAttribArray(0);

        context.VertexAttribPointer(1, 1, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)(3 * sizeof(float)));
        context.EnableVertexAttribArray(1);

        context.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)(4 * sizeof(float)));
        context.EnableVertexAttribArray(2);

        context.BindVertexArray(0);

        var vs = @"
            #version 330 core
            layout (location = 0) in vec3 pos;
            layout (location = 1) in float red;
            layout (location = 2) in vec2 tex;

            out float redColor;
            out vec2 texCoord;

            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;

            void main() {
                gl_Position = projection * view * model * vec4(pos, 1);
                redColor = red;
                texCoord = tex;
            }";

        var vertexShader = context.CreateShader(ShaderType.VertexShader);
        context.ShaderSource(vertexShader, vs);
        context.CompileShader(vertexShader);
        Debug.Write(context.GetShaderInfoLog(vertexShader));

        var fs = @"
            #version 330 core
            in float redColor;
            in vec2 texCoord;
            
            out vec4 color;

            uniform sampler2D image1; 
            uniform sampler2D image2; 

            void main()
            {
                color = mix(texture(image1, texCoord), texture(image2, texCoord), 0.7f);
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

        texture1 = context.GenTexture();
        context.BindTexture(TextureTarget.Texture2D, texture1);

        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);
        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);

        LoadImage(context, "wood.jpg");
        context.GenerateMipmap(TextureTarget.Texture2D);

        texture2 = context.GenTexture();
        context.BindTexture(TextureTarget.Texture2D, texture2);

        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);
        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
        context.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);

        LoadImage(context, "smile.jpg");
        context.GenerateMipmap(TextureTarget.Texture2D);
    }

    public unsafe void Draw(double delta)
    {
        if (!isInitialized)
        {
            Initialize();
            isInitialized = true;
        }

        context.Clear(ClearBufferMask.ColorBufferBit);
        context.ClearColor(Color.CadetBlue);
        context.UseProgram(shaderProgram);

        context.ActiveTexture(TextureUnit.Texture0);
        context.BindTexture(TextureTarget.Texture2D, texture1);
        context.ActiveTexture(TextureUnit.Texture1);
        context.BindTexture(TextureTarget.Texture2D, texture2);

        context.Uniform1(0, 0);
        context.Uniform1(1, 1);

        var ms = Environment.TickCount / 1000f;
        var z = (float)Math.Cos(ms) * 15 + 30;
        var model = Matrix4x4.CreateScale(10);
        var view = Matrix4x4.CreateLookAt(new Vector3(0, 0, z), Vector3.Zero, Vector3.UnitY);
        var projection = Matrix4x4.CreatePerspectiveFieldOfView(3.14f / 2f, 1, 0.1f, 2000f);
        context.UniformMatrix4(0, 1, false, (float*)&model);
        context.UniformMatrix4(2, 1, false, (float*)&view);
        context.UniformMatrix4(1, 1, false, (float*)&projection);

        context.BindVertexArray(vao);
        context.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, null);
        context.BindVertexArray(0);
        context.UseProgram(0);
    }

    private static unsafe void LoadImage(GL context, string filename)
    {
        using var image = Image.Load<Rgb24>(filename);
        image.Mutate(x => x.Flip(FlipMode.Vertical));

        var pixelData = new Rgb24[image.Width * image.Height];
        image.CopyPixelDataTo(pixelData);
        fixed (void* dataPointer = pixelData)
        {
            context.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgb8, (uint)image.Width, (uint)image.Height, 0,
                PixelFormat.Rgb, PixelType.UnsignedByte, dataPointer);
        }
    }
}