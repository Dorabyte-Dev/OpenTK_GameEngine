#version 330

out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform sampler2D texture1;

void main()
{
    FragColor = mix(texture(texture0, texCoord), texture(texture1, texCoord), 0.2);
    //FragColor = vec4(1.0, 0.0, 1.0, 1.0);
}