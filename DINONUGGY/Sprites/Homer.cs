using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.Sprites
{
    public class Homer : Objetos
    {
        Vector2 origin;
        public bool isActive;

        float activePosition { get; }

        public Homer(Texture2D texture, Vector2 position) : base(texture, position)
        {
             this.texture = texture;
            this.position = position;
            height = 80;
            width = 80;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            activePosition = position.X;
        }

       
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), null, Color.Gray, 0, origin, SpriteEffects.None, 0f);
        }

    }
}
