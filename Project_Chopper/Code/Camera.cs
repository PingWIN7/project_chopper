using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1Test.Code;
using Game1Test.Code.UI;

namespace Chopper.Code
{
    class Camera
    {
        /*private const float zoomUpperLimit = 1.5f;
       private const float zoomLowerLimit = 0.5f;*/

        private float zoom;
        private Matrix transform;
        private Vector2 position;
        private float rotation;

        /*private int wierPortWidth;
        private int viewPortHeight;
        private int worldWidth;
        private int worldHeight;*/

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = (value < 0.1f ? 0.1f : value);
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }


        public Camera()
        {
            //zoom = 1.0f;
            zoom = 0.5f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public void CameraFollow(Vector2 position)
        {
            this.position = Vector2.Lerp( this.position, position, 0.075f); //.075
            //this.position = Vector2.Lerp(new Vector3(this.position.X,this.position.Y,0), new Vector3(position.X,position.Y,0), 0.05f);
        }

        public void move(Vector2 amount)
        {
            position += amount;
        }

        public void update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();
            //if (key.IsKeyDown(Keys.PageUp))
            if (UI.zoomInButton.IsItPressed())
            {
                if (zoom <= 1.5)
                {
                    zoom += 0.01f;
                }
            }
            if (UI.zoomOutButton.IsItPressed())
            {
                if (zoom >= 0.4)
                {
                    zoom -= 0.01f;
                }
            }


        }

        public Matrix transformation(GraphicsDevice graphicsDevice)
        {
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) * Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }
    }
}
