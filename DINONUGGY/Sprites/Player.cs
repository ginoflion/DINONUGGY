﻿using Microsoft.Xna.Framework;
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
    public class Player : Objetos
    {
        float speed, gravity;
        public bool  isDead, isMidair, isCollided;
        public int hp;
        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height+10, width);
        }

        //Contructor
        public Player(Texture2D texture, Vector2 position ) : base(texture, position,true)
        {
            speed = 150;
            gravity = 190;
            isDead  = false;
            isCollided = false;
            hp = 100;
        }


        public void Update(double deltaTime, List<Objetos> gameObjects,List<Homer>homers,List<Marge>marges)
        {
            isMidair = true;
            KeyboardState kState = Keyboard.GetState();
            bool isKeyPressed=false;
            Gravity(deltaTime);
            HandleCollision(gameObjects,homers,marges);

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left)) 
            {
                velocity.X = -speed;
                isKeyPressed = true;
            }
            if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right))
            {
                velocity.X = speed;

                isKeyPressed = true;
            }
            if (kState.IsKeyDown(Keys.Space))
            {
                Jump();

                isKeyPressed = true;

            }
            if(!isKeyPressed)
            {
                velocity.X = 0;
            }
            if (hp <=0 &&  !isDead)
            {
                Die();
                
            }
           
            position += velocity* (float)deltaTime;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, HitBox, Color.White);
        }



        public void HandleCollision(List<Objetos> gameObjects, List<Homer> homers,List<Marge> marges)
        {
            if (isDead == true) return;
            foreach (Objetos gameObject in gameObjects)
            {
                if (CheckCollision(gameObject))
                {
                    if (gameObject is Ground)
                    {
                        if (velocity.Y > 0)
                        {

                            velocity.Y = 0;
                            isMidair = false;
                        }
                        else if (velocity.Y < 0)
                        {

                            velocity.Y = 0;
                        }
                        else if (velocity.X > 0)
                        {
                            float overlap = this.HitBox.Right - gameObject.HitBox.Left;
                            position.X -= overlap;
                            velocity.X = 0;
                        }
                        else if (velocity.X < 0)
                        {
                            float overlap = gameObject.HitBox.Right - this.HitBox.Left;
                            position.X += overlap;
                            velocity.X = 0;
                        }
                    }


                }

            }
            List<Homer> collidedHomers = new List<Homer>();
            List<Marge> collidedMarge = new List<Marge>();

            foreach (Homer homer in homers)
            {
                if (CheckCollision(homer) && !homer.isCollided)
                {
                    hp -= 25;
                    Sounds.damage.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    collidedHomers.Add(homer);
                    homer.isCollided = true;
                    homer.active = false;
                }
            }
            foreach (Marge marge in marges)
            {
                if (CheckCollision(marge) && !marge.isCollided)
                {
                    hp -= 50;
                    Sounds.damage.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    collidedMarge.Add(marge);
                    marge.isCollided = true;
                    marge.active = false;
                }
            }
            

            foreach (Marge marge in collidedMarge)
            {
                marges.Remove(marge);
            }
            foreach (Homer collidedHomer in collidedHomers)
            {
                homers.Remove(collidedHomer);
            }
            if (marges.All(marge => marge.isCollided))
            {
                isCollided = false;
            }
            if (homers.All(homer => homer.isCollided))
            {
                isCollided = false;
            }
            

        }

       


        public void Die()
        {
        
                speed = 0;
                isDead = true;
                Sounds.death.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                Jump();
                
        }

        public void Gravity(double deltaTime) {
            if (isMidair) {
                velocity.Y += (float)(gravity * deltaTime);
            }
        }

        private void Jump()
        {
            if (!isMidair)
            {
                velocity.Y -= 200;
                isMidair = true;
            }

        }



    }
}
