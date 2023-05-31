using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.Sprites
{
    internal class Donuts
    {

        private Vector2 Position;
        private Texture2D texture;
        private float Speed;
        public bool isVisible = true;
        public Donuts(Texture2D sprite, GameTime gameTime)
        {
            texture = sprite;
            Random rand = new Random(gameTime.TotalGameTime.Seconds);
            Position = new Vector2(800, rand.Next(50, 250));
            Speed = (float)rand.NextDouble() + (float)0.1;
            isVisible = true;
        }

        public void Update(GameTime gameTime)
        {

            if (Position.X < -texture.Width)
            {
                isVisible = false;
            }
            Position.X -= Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.LightGoldenrodYellow);
        }

    }
}
