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
        public float speed;

        public Homer(Texture2D texture, Vector2 position) : base(texture, position)
        {
             this.texture = texture;
            this.position = position;
            height = 80;
            width = 80;
            speed = 30;
        }

        public void Update(double deltaTime)
        {
            velocity.X = -speed;
            position += velocity * (float)deltaTime;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,HitBox,Color.White);
        }

    }
}
