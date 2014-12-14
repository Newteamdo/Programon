using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProgramonEngine
{
    public class Camera
    {
        private Vector2 Position { get; set; }
        private Vector2 DesiredPosition { get; set; }

        private Vector2 CameraOffset { get; set; }
        private Rectangle GraphicsBounds { get; set; }

        public Rectangle CameraWorld { get; private set; }

        public Camera(Vector2 startPos)
        {
            Position = startPos;
            DesiredPosition = startPos;

            CameraOffset = Vector2.Zero;
        }

        public void Initialize(Rectangle graphicalBounds)
        {
            GraphicsBounds = graphicalBounds;
            CameraWorld = graphicalBounds;
        }

        /// <summary> Signal a new position for the camera. (should only be used in free mode) </summary>
        public void Move(Vector2 unitMove)
        {
            DesiredPosition = Position + unitMove;
        }

        /// <summary> Teleport the camera to a new location. (WARNING: May cause problems in chase mode!)</summary>
        public void Teleport(Vector2 newPos)
        {
            Position = new Vector2(newPos.X / Sprite.NodeWidth, newPos.Y / Sprite.NodeHeight);
            DesiredPosition = Position;
        }

        /// <summary> Update the camera matrixes. (Free mode) </summary>
        public void Update()
        {
            if (Position != DesiredPosition)
                Position = Vector2.SmoothStep(Position, DesiredPosition, .1f);

            CameraWorld = new Rectangle((int)Position.X, (int)Position.Y, (int)((GraphicsBounds.Width / Sprite.NodeWidth) + Position.X), (int)((GraphicsBounds.Height / Sprite.NodeHeight) + Position.Y));
            CameraOffset = new Vector2(CameraWorld.X * Sprite.NodeWidth, CameraWorld.Y * Sprite.NodeHeight);
        }

        /// <summary> Update the camera matrixes. (Chase mode) </summary>
        public void Update(Vector2 lookAt, Rectangle mapBounds)
        {
            Vector2 relativeLookAt = new Vector2((lookAt.X - (GraphicsBounds.Width >> 1)) / Sprite.NodeWidth, (lookAt.Y - (GraphicsBounds.Height >> 1)) / Sprite.NodeHeight);

            if ((lookAt.X - (GraphicsBounds.Width >> 1)) >= 0 && (lookAt.X + (GraphicsBounds.Width >> 1)) <= mapBounds.Width * Sprite.NodeWidth)
                Position = new Vector2(relativeLookAt.X, Position.Y);
            if ((lookAt.Y - (GraphicsBounds.Height >> 1)) >= 0 && (lookAt.Y + (GraphicsBounds.Height >> 1)) <= mapBounds.Height * Sprite.NodeHeight)
                Position = new Vector2(Position.X, relativeLookAt.Y);

            CameraWorld = new Rectangle((int)Position.X, (int)Position.Y, (int)(GraphicsBounds.Width + Position.X), (int)(GraphicsBounds.Height + Position.Y));
            CameraOffset = new Vector2(CameraWorld.X * Sprite.NodeWidth, CameraWorld.Y * Sprite.NodeHeight);
        }

        /// <summary> Get the relative position of the object from the camera. </summary>
        /// <param name="position"> Fixedposition. </param>
        public Vector2 GetRelativePosition(Vector2 position)
        {
            return position - CameraOffset;
        }
    }
}
