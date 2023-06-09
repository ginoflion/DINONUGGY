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
        float speed, gravity;
        public bool  isDead, isMidair;
        public int score;
        
       
        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y , width, height );
        }

        //Contructor
        public Player(Texture2D texture, Vector2 position ) : base(texture, position)
        {
            speed = 10;
            gravity = 100;
            isDead  = false;
        }


        public void Update(double deltaTime, List<Objetos> gameObjects)
        {

            Gravity(deltaTime);
            HandleCollision(gameObjects);
            KeyboardState kState = Keyboard.GetState();

            if ((kState.IsKeyDown(Keys.A)) || (kState.IsKeyDown(Keys.Left)))
            {
                velocity.X -= speed;        
            }
            if ((kState.IsKeyDown(Keys.D)) || (kState.IsKeyDown(Keys.Right)))
            {
                velocity.X += speed;
            }
            if ((kState.IsKeyDown(Keys.Space)))
            {
                Jump();
            }
            position += velocity * (float)deltaTime;
        }
          
        

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, HitBox, Color.White);
        }

        public void HandleCollision(List<Objetos> gameObjects)
        {
            foreach (Objetos gameObject in gameObjects)
            {
                if ((velocity.Y > 0 && IsTouchingTop(gameObject)) ||
                    (velocity.Y < 0 && IsTouchingBottom(gameObject)))
                {
                    if (gameObject is Ground)
                    {
                        velocity.Y = 0;
                        isMidair = false;
                       
                    }
                    if (gameObject is Homer )
                    {
                        velocity.Y = 0;
                        if (!isDead) Die();
                    }
                  
                }
                // Checar Colisões da esquerda e direita
                if ((velocity.X < 0 && IsTouchingRight(gameObject)) ||
                    (velocity.X > 0 && IsTouchingLeft(gameObject)))
                {
                    if (gameObject is Ground)
                    {
                        velocity.X = 0;
                        if (!isDead)
                        {
                            //Score();
                        }
                    }
                    if (gameObject is Homer )
                    {
                        velocity.X = 0;
                        if (!isDead) Die();
                    }
                   
                }

            }
        }


        public void Die()
        {
            speed = 0;
            isDead = true;
        }

        public void Gravity(double deltaTime) {
            if (isMidair) {
                velocity.Y += (float)(gravity * deltaTime);
            }
                }



        private void Jump()
        {

            velocity.Y -= 10;
            isMidair = true;

        }



    }
}
