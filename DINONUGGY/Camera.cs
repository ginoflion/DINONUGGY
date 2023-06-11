using DINONUGGY.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY
{
    internal class Camera
    {
        public Matrix Transform { get; private set; }
        public void Follow(Player target)
        {
            var position = Matrix.CreateTranslation(-target.position.X - (target.HitBox.Width / 2),-360  /*-target.position.Y - (target.HitBox.Height / 2)*/, 0);
            var offset = Matrix.CreateTranslation(Game1.screenWidth / 2, Game1.screenHeight / 2, 0);
            Transform = position * offset;
        }
    }
}
