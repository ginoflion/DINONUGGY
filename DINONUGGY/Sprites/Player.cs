using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DINONUGGY.Sprites
{
    public class Player : Objetos
    {
        Vector2 velocity;
        float speed, gravity;
        public bool  isDead, isJumping, isInvincible;
        public int score;


        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y + 8, width, height - 16);
        }

        //Contructor
        public Player(Texture2D texture, Vector2 position ) : base(texture, position)
        {
            speed = 6;
            gravity = 25;
            isDead = isInvincible = false;
        }

        //Update method (is executed every tick)
        public void Update(double deltaTime, List<Objetos> gameObjects)
        {
            Movement(deltaTime);
            Gravity(deltaTime);
            
            position += (velocity * (float)deltaTime) * 60;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, HitBox, Color.White);
        }

        public void Movement(double deltaTime)
        {
            if (!isDead)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isJumping)
                {
                    Jump();
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Space)) isJumping = false;
            }

            velocity.X = speed;
        }

    

     

        public void Gravity(double deltaTime) => velocity.Y += (float)(gravity * deltaTime);


        private void Jump()
        {
            velocity.Y = 0;
            isJumping = true;
        }

       
       
    }
}
