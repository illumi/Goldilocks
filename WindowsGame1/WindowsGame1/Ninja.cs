﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CGProj
{
    class Ninja : Sprite
    {
        const string ASSETNAME = "ninja";
        const int START_POSITION_X = 0;
        const int START_POSITION_Y = 450;
        const int SPEED = 260;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        //int gifpos = 1;
        //bool back = false;
        //static int flightDuation = 300;
        //public int stamina = flightDuation;



        private Timer animationTimer = new Timer();
        private Timer animationTimerAttack = new Timer();
        private int tickWalk = 1;
        private int tickAttackThrow = 4;

        const int spriteOffset = 675; //Ninja offset left = 0, right = 675
        
        /*public int getStamina
        {
            get { return stamina; }
        }*/

        public enum State
        {
            Idle,
            Walking,
            Jumping,
            Attack,
            Throw,
        }

        public enum Direction
        {
            Left,
            Right,
        }


        public State mCurrentState = State.Idle;
        public Direction mCurrentDirection = Direction.Right;
        
        Vector2 mSpeed = Vector2.Zero;
        KeyboardState mPreviousKeyboardState;
        Vector2 mStartingPosition = Vector2.Zero;
        //List<Fireball> mFireballs = new List<Fireball>();

        ContentManager mContentManager;


        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;

            /*foreach (Fireball aFireball in mFireballs)
            {
                aFireball.LoadContent(theContentManager);
            }*/

            Position = new Vector2(START_POSITION_X, START_POSITION_Y);

            base.LoadContent(theContentManager, ASSETNAME);
            Source = new Rectangle(0, 0, 135, 195);

            animationTimer.Interval = (1000) * (0.25); //step every 1/4 sec looks more realistic
            animationTimer.Enabled = true;
            animationTimer.Elapsed += new ElapsedEventHandler(OnTimedEventWalk);

            animationTimerAttack.Interval = (1000) * (0.25); //step every 1/4 sec looks more realistic
            animationTimerAttack.Enabled = true;
            animationTimerAttack.Elapsed += new ElapsedEventHandler(OnTimedEventAttackThrow);
        }

        private void OnTimedEventWalk(object source, ElapsedEventArgs e) //do something more useful here!
        {
            if (tickWalk >= 2)
            {
                tickWalk = 1;
            }
            else
            {
                tickWalk++;
            }
        }

        private void OnTimedEventAttackThrow(object source, ElapsedEventArgs e) //do something more useful here!
        {
            if (mCurrentState == State.Attack || mCurrentState == State.Throw)
            {
                if (tickAttackThrow > 3)
                {
                    mCurrentState = State.Idle;
                }
                else
                {
                    tickAttackThrow++;
                }
            }

        }


        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
           // Below:  resets position of char if it reaches the width of the screen. 

            /** 
             * Catch all keyboard buttons here, alter state, and call UpdateAnimation after it. 
             * UpdateAnimation should update the animation based on the global sprite state.
             * The keyboard state shouldn't go further than this method unless we need to abstract it out for some reason?
             **/

            UpdateAnimation(aCurrentKeyboardState);
            UpdateJump(aCurrentKeyboardState);
            //UpdateFireball(theGameTime, aCurrentKeyboardState);
            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, mSpeed, mDirection);
        }


    /*    private void UpdateFireball(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
        {

            foreach (Fireball aFireball in mFireballs)
            {

                aFireball.Update(theGameTime);

            }



            if (aCurrentKeyboardState.IsKeyDown(Keys.RightControl) == true && mPreviousKeyboardState.IsKeyDown(Keys.RightControl) == false)
            {

                ShootFireball();

            }

        }




        private void ShootFireball()
        {

            if (mCurrentState == State.Walking || mCurrentState == State.Jumping || mCurrentState == State.Flying)
            {

                bool aCreateNew = true;

                foreach (Fireball aFireball in mFireballs)
                {

                    if (aFireball.Visible == false)
                    {

                        aCreateNew = false;

                        aFireball.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),

                            new Vector2(200, 0), new Vector2(1, 0));

                        break;

                    }

                }



                if (aCreateNew == true)
                {

                    Fireball aFireball = new Fireball();

                    aFireball.LoadContent(mContentManager);

                    aFireball.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),

                        new Vector2(200, 200), new Vector2(1, 0));

                    mFireballs.Add(aFireball);

                }

            }

        }*/

        private void UpdateAnimation(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking || mCurrentState == State.Idle)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true || aCurrentKeyboardState.IsKeyDown(Keys.A) == true) //left
                {
                    mCurrentState = State.Walking;
                    mCurrentDirection = Direction.Left;

                    MovementAnimation();

                    mSpeed.X = SPEED;
                    mDirection.X = MOVE_LEFT;

                }

                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true || aCurrentKeyboardState.IsKeyDown(Keys.D) == true) //right
                {
                    mCurrentState = State.Walking;
                    mCurrentDirection = Direction.Right;

                    MovementAnimation();

                    mSpeed.X = SPEED;
                    mDirection.X = MOVE_RIGHT;

                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.E) == true) //attack
                {
                    mCurrentState = State.Attack;
                    tickAttackThrow = 1;
                    AttackAnimation();
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Q) == true) //throw
                {
                    mCurrentState = State.Throw;
                    tickAttackThrow = 1;
                    ThrowAnimation();
                }
                else //idle
                {
                    mCurrentState = State.Idle;
                    if (mPreviousKeyboardState.IsKeyDown(Keys.Left) == true || mPreviousKeyboardState.IsKeyDown(Keys.A) == true)//left idle
                    {
                        Source = new Rectangle(0, 0, 135, 195);
                    }
                    else if (mPreviousKeyboardState.IsKeyDown(Keys.Right) == true || mPreviousKeyboardState.IsKeyDown(Keys.D) == true)//right idle
                    {
                        Source = new Rectangle(0 + spriteOffset, 0, 135, 195);
                    }
                    /* else
                     {
                         Source = new Rectangle(0 + spriteOffset, 0, 135, 195);
                     }*/
                }

            }

        }

        private void AttackAnimation()
        {
            if (mCurrentDirection == Direction.Left)
                Source = new Rectangle((185 * tickWalk), 0, 185, 195);
            else
                Source = new Rectangle((185 * tickWalk) + spriteOffset, 0, 185, 195);
        }

        private void ThrowAnimation()
        {
            if (mCurrentDirection == Direction.Left)
                Source = new Rectangle((175 * tickWalk), 0, 175, 195);
            else
                Source = new Rectangle((175 * tickWalk) + spriteOffset, 0, 175, 195);
        }


        private void MovementAnimation() 
        { 
            if (mCurrentDirection == Direction.Left)
            {
                Source = new Rectangle((135 * tickWalk), 0, 135, 195);
            }
            else
            {
                Source = new Rectangle((135 * tickWalk) + spriteOffset, 0, 135, 195);
            }            
        }


        private void UpdateJump(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                    JumpAnimation();
                }
            }

            if (mCurrentState == State.Jumping)
            {

                if (mStartingPosition.Y - Position.Y > 150)
                {
                    JumpAnimation();
                    mDirection.Y = MOVE_DOWN;
                }



                if (Position.Y > mStartingPosition.Y)
                {
                    Position.Y = mStartingPosition.Y;
                    mCurrentState = State.Idle;
                    mDirection = Vector2.Zero;
                    JumpAnimation();
                }


                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    JumpAnimation();
                    Jump();
                }
            }
        }


        private void Jump()
        {
            if (mCurrentState != State.Jumping)
            {

                mCurrentState = State.Jumping;
                mStartingPosition = Position;
                mDirection.Y = MOVE_UP;
                mSpeed = new Vector2(SPEED, SPEED);
                onfloor = false;
            }
        }


        private void JumpAnimation() 
        {
            if(mCurrentDirection == Direction.Left) //left
            {
                if (mStartingPosition.Y - Position.Y > 150) //coming down
                {
                    Source = new Rectangle(135, 585, 135, 195);
                }
                else if (mCurrentState == State.Idle)
                {
                    Source = new Rectangle(0, 0, 135, 195);
                }
                else //coming down going up
                {
                    Source = new Rectangle(0, 585, 135, 195);
                }
            }
            else //right
            {
                if (mStartingPosition.Y - Position.Y > 150) //coming down
                {
                    Source = new Rectangle(135 + spriteOffset, 585, 135, 195);
                }
                else if (mCurrentState == State.Idle)
                {
                    Source = new Rectangle(0 + spriteOffset, 0, 135, 195);
                }
                else //coming down going up
                {
                    Source = new Rectangle(0 + spriteOffset, 585, 135, 195);
                }
            }
        
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            /*foreach (Fireball aFireball in mFireballs)
            {
                aFireball.Draw(theSpriteBatch);
            }*/
            base.Draw(theSpriteBatch);
        }


        public override void collidedFloor(Vector2 colcp, float colH)
        {
            if (onfloor) return;


            Position.Y = colcp.Y + (colH/2);
             mSpeed = new Vector2(0, 0);
            onfloor = true;
            mCurrentState = State.Idle;
        }

    }
}
