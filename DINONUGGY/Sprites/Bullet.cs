using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DINONUGGY.Sprites
{
    public class Bullet:Objetos
    {
        int speed;
        public bool isActive { get; set; }

        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height , width);
        }


        public Bullet( Texture2D texture,Vector2 position,int width,int heigh):base(texture,position,true)
        {
            this.position = position;
            this.velocity = Vector2.Zero;
            this.texture = texture;
            this.speed = 9;
            this.width = width;
            this.height = heigh;
            this.isActive = true;

        }

        
        public void Update(GameTime gameTime, List<Marge> margeList,List<Homer> homersList)
        {
            velocity.X = speed;
            position += velocity;


            foreach (Marge marge in margeList)
            {
                if (HitBox.Intersects(marge.HitBox))
                {
                    margeList.Remove(marge);
                    isActive = false;
                    break;
                }
            }
            foreach(Homer homer in homersList)
            {
                if (HitBox.Intersects(homer.HitBox))
                {
                    Sounds.homerHit.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    isActive = false;
                    break;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);
        }

        

       
    }

}
