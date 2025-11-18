#version 330
struct Material {
    sampler2D diffuse;
    sampler2D specular;

    float shininess;
};

struct Light {
    vec3 position;
  
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constAtt;
    float linearAtt;
    float quadAtt;
};


uniform Material material;
uniform Light light;
uniform vec3 objectColor;
uniform vec3 viewPos;

in vec3 Normal;
in vec3 FragPos;
in vec2 texCoord;

out vec4 FragColor;

void main()
{
    /* MAIN LIGHT CALCULATIONS */
    //ambient
    vec3 ambient = light.ambient* vec3(texture(material.diffuse, texCoord));

    //diffuse
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diffuseAmount = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diffuseAmount * vec3(texture(material.diffuse, texCoord));

    //specular
    float shininess = 32;
    vec3 viewDirection = normalize(viewPos - FragPos);
    vec3 reflectionVector = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDirection, reflectionVector), 0.0), material.shininess);
    vec3 specular = light.specular * spec * vec3(texture(material.specular, texCoord));

    //Attenuation
    float distance = length(light.position - FragPos);
    float attenuation = 1.0 / (light.constAtt + light.linearAtt * distance + light.quadAtt * (distance * distance));


    vec3 result = (ambient + diffuse + specular) * attenuation;
    FragColor = vec4(result, 1.0);
}