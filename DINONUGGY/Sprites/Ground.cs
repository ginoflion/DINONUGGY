using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace DINONUGGY.Sprites
{
    public class Ground : Objetos
    {
        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y , width, height );
        }
        public Ground(Texture2D texture, Vector2 position, int height, int width) : base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.MintCream);
        }

    }
}
