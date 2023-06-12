﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DINONUGGY.Sprites
{
    public class Objetos
    {
        public Texture2D texture; 
        public Vector2 velocity;
        public Vector2 position;
        public int height = 70, width = 70;
        public bool active;


        public virtual Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height, width);
        }
        public Objetos(Texture2D texture, Vector2 position, bool active)
        {
            this.texture = texture;
            this.position = position;
            this.active = active;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, HitBox,Color.Red);

            
        }


        protected bool CheckCollision( Objetos obj2)
        {
            return this.HitBox.Intersects(obj2.HitBox);
        }


    }
}
