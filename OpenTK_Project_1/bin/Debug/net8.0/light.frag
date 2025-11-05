#version 330
struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininess;
};

struct Light {
    vec3 position;
  
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};


uniform Material material;
uniform Light light;
uniform vec3 objectColor;
uniform vec3 viewPos;

in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

void main()
{
    //ambient
    vec3 ambient = material.ambient * light.ambient;

    //diffuse
    vec3 norm = normalize(Normal);
    vec3 dir = normalize(light.position - FragPos);
    float diffuseAmount = max(dot(norm, dir), 0.0);
    vec3 diffuse = (diffuseAmount * material.diffuse) * light.diffuse;

    //specular
    float shininess = 32;
    vec3 viewDirection = normalize(viewPos - FragPos);
    vec3 reflectionVector = reflect(-dir, norm);
    float spec = pow(max(dot(viewDirection, reflectionVector), 0.0), shininess);
    vec3 specular = (spec * material.specular) * light.specular;

    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
}