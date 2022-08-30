using Game.Contracts;
using Silk.NET.Input;

namespace Game.Objects.Components;

internal sealed class KeyboardInput : IObjectComponent
{
    public Action<Key, int>? OnKeyDown { get; set; }
    public Action<Key, int>? OnKeyUp { get; set; }
    public Action<char>? OnKeyChar { get; set; }
}