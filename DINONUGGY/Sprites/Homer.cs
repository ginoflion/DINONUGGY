﻿using Microsoft.Xna.Framework.Graphics;
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

        
        public Homer(Texture2D texture, Vector2 position) : base(texture, position)
        {
             this.texture = texture;
            this.position = position;
            height = 80;
            width = 80;
          
        }

       
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,HitBox,Color.White);
        }

    }
}
