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

        
        public virtual Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height, width);
        }

        public Objetos(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, HitBox,Color.Red);

            
        }


        protected bool IsTouchingLeft(Objetos gameObject)
        {
            return this.HitBox.Right + this.velocity.X > gameObject.HitBox.Left &&
                   this.HitBox.Left < gameObject.HitBox.Right &&
                   this.HitBox.Bottom > gameObject.HitBox.Top &&
                   this.HitBox.Top < gameObject.HitBox.Bottom;
        }

        protected bool IsTouchingRight(Objetos gameObject)
        {
            return this.HitBox.Left + this.velocity.X < gameObject.HitBox.Right &&
                   this.HitBox.Right > gameObject.HitBox.Left &&
                   this.HitBox.Bottom > gameObject.HitBox.Top &&
                   this.HitBox.Top < gameObject.HitBox.Bottom;
        }

        protected bool IsTouchingTop(Objetos gameObject)
        {
            return this.HitBox.Bottom + this.velocity.Y > gameObject.HitBox.Top &&
                   this.HitBox.Top < gameObject.HitBox.Bottom &&
                   this.HitBox.Right > gameObject.HitBox.Left &&
                   this.HitBox.Left < gameObject.HitBox.Right;
        }

        protected bool IsTouchingBottom(Objetos gameObject)
        {
            return this.HitBox.Top + this.velocity.Y < gameObject.HitBox.Bottom &&
                   this.HitBox.Bottom > gameObject.HitBox.Top &&
                   this.HitBox.Right > gameObject.HitBox.Left &&
                   this.HitBox.Left < gameObject.HitBox.Right;
        }


    }
}
