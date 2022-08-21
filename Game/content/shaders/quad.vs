#version 330 core
layout (location = 0) in vec3 pos;
layout (location = 1) in float red;
layout (location = 2) in vec2 tex;

out float redColor;
out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(pos, 1);
    redColor = red;
    texCoord = tex;
}