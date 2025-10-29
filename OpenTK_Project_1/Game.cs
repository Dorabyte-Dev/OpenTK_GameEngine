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


        /*float[] vertices =
{
            //Position          Texture coordinates
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };*/

        float[] vertices = {
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };

       Vector3[] cubePositions = {
            ( 0.0f,  0.0f,  0.0f),
            ( 2.0f,  5.0f, -15.0f),
            (-1.5f, -2.2f, -2.5f),
            (-3.8f, -2.0f, -12.3f),
            ( 2.4f, -0.4f, -3.5f),
            (-1.7f,  3.0f, -7.5f),
            ( 1.3f, -2.0f, -2.5f),
            ( 1.5f,  2.0f, -2.5f),
            ( 1.5f,  0.2f, -1.5f),
            (-1.3f,  1.0f, -1.5f)
        };

        uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        int VertexBufferObject;
        int ElementBufferObject;
        int VertexArrayObject;

        private Texture _texture;
        private Texture _texture2;

        Shader shader;

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

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) 
        {

        }
        protected override void OnLoad() //Se ejecuta cuando carga el programa por primera vez
        {
            base.OnLoad();

            _timer = Stopwatch.StartNew();
            //Transform


            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            

            shader = new Shader("shader.vert", "shader.frag");
            shader.Use();

            var vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture = Texture.LoadFromFile("wall.jpg");
            _texture.Use(TextureUnit.Texture0);

            _texture2 = Texture.LoadFromFile("awesomeface.png");
            _texture2.Use(TextureUnit.Texture1);

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);






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

            Matrix4 rotY = Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(45));
            Matrix4 rotX = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time)); //Transforms of the model
            Matrix4 model = rotY * rotX;

            GL.BindVertexArray(VertexArrayObject);


            _texture.Use(TextureUnit.Texture0);
            _texture2.Use(TextureUnit.Texture1);
            shader.Use();

            _view = _camera.GetViewMatrix();
            _projection = _camera.GetProjectionMatrix();

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", _view);
            shader.SetMatrix4("projection", _projection);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            foreach(var pos in cubePositions)
            {
                Matrix4 model2 = model * Matrix4.CreateTranslation(pos);
                shader.SetMatrix4("model", model2);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }
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
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
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

            shader.Dispose();
        }
    }
}
