using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using RenderEngine;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Reflection;

namespace GameEngine
{
    public class Game : GameWindow
    {


        float[] lightVertices =
{
            //Position         
            0.5f,  0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        };

        private readonly float[] vertices =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };


        Vector3 cubePosition = new Vector3(0.0f, 0.0f, 0.0f);

        private readonly Vector3[] _cubePositions =
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f, -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3(2.4f, -0.4f, -3.5f),
            new Vector3(-1.7f, 3.0f, -7.5f),
            new Vector3(1.3f, -2.0f, -2.5f),
            new Vector3(1.5f, 2.0f, -2.5f),
            new Vector3(1.5f, 0.2f, -1.5f),
            new Vector3(-1.3f, 1.0f, -1.5f)
        };

        Vector3 lightPosition = new Vector3(2f, 1f, 1f);
        Vector3 lightScale = new Vector3(0.3f, 0.3f, 0.3f);
        int VertexBufferObject;
        int lightVertexArrayObject;
        int cubeVertexArrayObject;

        Shader lightingShader;
        Shader lampShader;

        float speed = 1.5f;
        bool _firstMove = true;
        float sensitivity = 0.2f;
        Vector2 _lastPos;
        float _fov;

        private Stopwatch _timer;

        private Camera _camera;

        // Then, we create two matrices to hold our view and projection. They're initialized at the bottom of OnLoad.
        // The view matrix is what you might consider the "camera". It represents the current viewport in the window.
        private Matrix4 _view;

        // This represents how the vertices will be projected. It's hard to explain through comments,
        // so check out the web version for a good demonstration of what this does.
        private Matrix4 _projection;

        private Texture texture0;
        private Texture texture1;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) 
        {

        }
        protected override void OnLoad() //Se ejecuta cuando carga el programa por primera vez
        {
            base.OnLoad();

            _timer = Stopwatch.StartNew();
            //Transform
            lightingShader = new Shader("shader.vert", "light.frag");
            lampShader = new Shader("shader.vert", "shader.frag");

            texture0 = Texture.LoadFromFile("container2.png");
            texture1 = Texture.LoadFromFile("container2_specular.png");

            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            
            cubeVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(cubeVertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            var vertexLocation = lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            var normalLocation = lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            var texCoordLocation = lightingShader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            lightVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(lightVertexArrayObject);

            var lightVertexLocation = lampShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);


            //Camera
            _camera = new Camera(new Vector3(0, 0, 3), Size.X / (float)Size.Y);




            //Code goes here

            //DrawTriangle
        }

        protected override void OnRenderFrame(FrameEventArgs e) //Se ejecuta al renderizar cada frame visual del juego. Aquí va todo lo que se dibuja.
        {
            base.OnRenderFrame(e);


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            float _time = (float)_timer.Elapsed.TotalSeconds;

            lightPosition = new Vector3(3 * MathF.Sin(_time * 0.25f), 2 * MathF.Sin(.05f*_time), 3 * MathF.Cos(_time * 0.25f));
            Matrix4 rotY = Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(45));
            Matrix4 rotX = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time * 5)); //Transforms of the model
            //Matrix4 model = Matrix4.CreateScale(3) * rotY * rotX * Matrix4.CreateTranslation(cubePosition);

            GL.BindVertexArray(cubeVertexArrayObject);
            
            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);
            lightingShader.Use();
            lightingShader.SetMatrix4("model", Matrix4.Identity);
            lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
            //lightingShader.SetVector3("lightColor", new Vector3(1.0f, 0.3f, 1.0f));
            lightingShader.SetVector3("light.position", lightPosition);
            lightingShader.SetVector3("viewPos", _camera.Position);

            

            lightingShader.SetInt("material.diffuse", 0);
            lightingShader.SetInt("material.specular", 1);
            lightingShader.SetVector3("material.ambient", new Vector3(1.0f, 0.5f, 0.31f));
            //lightingShader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
            //lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            lightingShader.SetFloat("material.shininess", 64.0f);
            lightingShader.SetVector3("light.ambient", new Vector3(0.2f, 0.2f, 0.2f));
            lightingShader.SetVector3("light.diffuse", new Vector3(0.5f, 0.5f, 0.5f)); // darken the light a bit to fit the scene
            lightingShader.SetVector3("light.specular", new Vector3(1.0f, 1.0f, 1.0f));

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            /*for (int i = 0; i < _cubePositions.Length; i++)
            {
                Matrix4 model = Matrix4.Identity;
                model *= Matrix4.CreateTranslation(_cubePositions[i]);
                float angle = 20.0f * i;
                model *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
                lightingShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }*/

            Matrix4 lightModel = Matrix4.CreateScale(lightScale) * Matrix4.CreateTranslation(lightPosition);

            GL.BindVertexArray(lightVertexArrayObject);

            lampShader.Use();

            lampShader.SetMatrix4("model", lightModel);
            lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            

            /*foreach(var pos in cubePositions)
            {
                Matrix4 model2 = model * Matrix4.CreateTranslation(pos);
                shader.SetMatrix4("model", model2);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }*/
            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            //Code goes here.

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) //Se ejecuta cada vez que el FrameBuffer se redimensiona
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnUpdateFrame(FrameEventArgs e) //Se ejecuta al renderizar cada frame lógico del juego. Aquí va todo lo que es jugable.
        {
            base.OnUpdateFrame(e);

            if (!IsFocused) // check to see if the window is focused
            {
                return;
            }
            CursorState = CursorState.Grabbed;

            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            float delta = (float)e.Time;
            if (KeyboardState.IsKeyDown(Keys.W)) _camera.Position += _camera.Front * speed * delta;
            if (KeyboardState.IsKeyDown(Keys.S)) _camera.Position -= _camera.Front * speed * delta;
            if (KeyboardState.IsKeyDown(Keys.A)) _camera.Position -= _camera.Right * speed * delta;
            if (KeyboardState.IsKeyDown(Keys.D)) _camera.Position += _camera.Right * speed * delta;
            if (KeyboardState.IsKeyDown(Keys.LeftShift)) _camera.Position -= _camera.Up * speed * delta;
            if (KeyboardState.IsKeyDown(Keys.Space)) _camera.Position += _camera.Up * speed * delta;
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsFocused) // check to see if the window is focused  
            {
                var mouse = MouseState;
                if (_firstMove)
                {
                    _lastPos = new Vector2(mouse.X, mouse.Y);
                    _firstMove = false;
                }
                else
                {
                    var deltaX = mouse.X - _lastPos.X;
                    var deltaY = mouse.Y - _lastPos.Y;
                    _lastPos = new Vector2(mouse.X, mouse.Y);
                    _camera.Yaw += deltaX * sensitivity;
                    _camera.Pitch -= deltaY * sensitivity;
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            // Ajustar el FOV usando la rueda del ratón
            _camera.Fov -= (float)e.OffsetY;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            lightingShader.Dispose();
        }
    }
}
