using System;
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


        const int spriteOffset = 675; //Ninja offset left = 0, right = 675
        
        /*public int getStamina
        {
            get { return stamina; }
        }*/

        enum State
        {
            Idle,
            Walking,
            Jumping,
            Attack,
            Throw,
        }

        enum Direction
        {
            Left,
            Right,
        }


        State mCurrentState = State.Idle;

        Direction mCurrentDirection = Direction.Right;
        
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

            animationTimer.Interval = (1000) * (1);
            animationTimer.Enabled = true;
            animationTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e) //do something more useful here!
        {
            int i = 0;
            i++;
        }


        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
           // Below:  resets position of char if it reaches the width of the screen. 
            if (Position.X >= 790)
            {
                Position.X = 0;
                //screen = 1;
            }

            UpdateMovement(aCurrentKeyboardState);
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


        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
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
                else //idle
                {
                    mCurrentState = State.Idle;
                    Source = new Rectangle(0, 0, 135, 195);
                }

            }

        }


        private void MovementAnimation() //TODO suport 3 frames
        { 
            if (mCurrentDirection == Direction.Left)
            {
                Source = new Rectangle(0, 0, 135, 195);
            }
            else
            {
                Source = new Rectangle(0+spriteOffset, 0, 135, 195);
            }            
        }


        private void UpdateJump(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (mCurrentState == State.Jumping)
            {
                if (mStartingPosition.Y - Position.Y > 150)
                {
                    mDirection.Y = MOVE_DOWN;
                }



                if (Position.Y > mStartingPosition.Y)
                {
                    Position.Y = mStartingPosition.Y;
                    mCurrentState = State.Walking;
                    mDirection = Vector2.Zero;
                }


                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
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
    }
}
