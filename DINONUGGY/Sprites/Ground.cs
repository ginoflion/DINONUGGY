using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.Sprites
{
    public class Ground
    {
        Texture2D Sprite;
        Vector2 Position;

        public Ground(Vector2 position, Texture2D sprite)
        {
            Sprite = sprite;
            Position = position;
        }

        public void Update(GameTime gameTime, float worldSpeed)
        {
            if (Position.X < -Sprite.Width)
            {
                float overflow = Sprite.Width + Position.X;
                Position.X = Sprite.Width + overflow;
            }
            Position.X -= 30 * worldSpeed * (1 / (float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
