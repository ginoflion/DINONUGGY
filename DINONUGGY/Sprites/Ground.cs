using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.Sprites
{
    public class Ground : Objetos
    {

        public Ground(Texture2D texture, Vector2 position, int height, int width) : base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            this.height = height;
            this.width = width;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.MintCream);
        }
    }
}
