using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iosif_3133A_tema2
{
    class Paralelipiped : GameWindow
    {
        private Matrix4 camera;
        private Vector3 pozitieCamera;
        private Vector3 directieCamera;
        private Vector3 directieRotatieCamera;

        private MouseState pozitiaAnterioaraACursorului;

        public Paralelipiped() : base(800, 600)
        {
            VSync = VSyncMode.On; //sincronizeaza rata de refresh
            pozitieCamera = new Vector3(10, 10, 10);
            directieCamera = new Vector3(0, 0, 0);
            directieRotatieCamera = new Vector3(0, 1, 0);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.White);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.Width, this.Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 250);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            camera = Matrix4.LookAt(pozitieCamera, directieCamera, directieRotatieCamera);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }


        /// <summary>
        /// Prin intermediul tastelor Right,Up,Down,Left se roteste obiectul, iar click stanga apasat urmat de deplasarea mouse-ului inainte/inapoi are ca efect apropierea, respectiv indepartarea de obiect.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetCursorState();

            if (keyboardState[Key.Escape])
            {
                Exit();
                return;
            }

            if (keyboardState[Key.Right])
            {
                GL.Rotate(MathHelper.Pi, 0, 1, 0);
            }

            if (keyboardState[Key.Up])
            {
                GL.Rotate(MathHelper.Pi, -1, 0, 0);
            }

            if (keyboardState[Key.Left])
            {
                GL.Rotate(MathHelper.Pi, 0, -1, 0);
            }

            if (keyboardState[Key.Down])
            {
                GL.Rotate(MathHelper.Pi, 1, 0, 0);
            }

            if (mouseState[MouseButton.Left])
            {
                if (!mouseState.Equals(pozitiaAnterioaraACursorului))
                {
                    int value = (pozitiaAnterioaraACursorului.Y < mouseState.Y) ? -1 : 1;
                    pozitieCamera.X += value;
                    pozitieCamera.Y += value;
                    pozitieCamera.Z += value;
                    camera = Matrix4.LookAt(pozitieCamera, directieCamera, directieRotatieCamera);
                    GL.MatrixMode(MatrixMode.Modelview);
                    GL.LoadMatrix(ref camera);
                }
            }
            pozitiaAnterioaraACursorului = mouseState;
        }


        /// <summary>
        /// Deseneaza paralelipipedul.
        /// </summary>
        private void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);

            GL.Vertex3(2, -1, -1);
            GL.Vertex3(2, 1, -1);
            GL.Vertex3(-2, 1, -1);
            GL.Vertex3(-2, -1, -1);

            GL.Vertex3(2, -1, 1);
            GL.Vertex3(2, 1, 1);
            GL.Vertex3(-2, 1, 1);
            GL.Vertex3(-2, -1, 1);

            GL.Color3(Color.Red);

            GL.Vertex3(2, 1, -1);
            GL.Vertex3(2, 1, 1);
            GL.Vertex3(2, -1, 1);
            GL.Vertex3(2, -1, -1);

            GL.Vertex3(-2, 1, -1);
            GL.Vertex3(-2, 1, 1);
            GL.Vertex3(-2, -1, 1);
            GL.Vertex3(-2, -1, -1);

            GL.Color3(Color.Green);

            GL.Vertex3(2, -1, -1);
            GL.Vertex3(-2, -1, -1);
            GL.Vertex3(-2, -1, 1);
            GL.Vertex3(2, -1, 1);

            GL.Vertex3(2, 1, -1);
            GL.Vertex3(-2, 1, -1);
            GL.Vertex3(-2, 1, 1);
            GL.Vertex3(2, 1, 1);

            GL.End();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Draw();

            SwapBuffers();
        }
    }
}
