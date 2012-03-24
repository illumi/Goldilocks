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
    class AI : Sprite
    {

        const string ASSETNAME = "WizardSquare";
        const int START_POSITION_X = 500;
        const int START_POSITION_Y = 300;
        const int SPEED = 100;
        //const int JUMPFORCE = 1500;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        //const int ATTACKRANGE = 20;


        public enum State
        {
            Alive,
            Dead,
        }

        public enum Direction
        {
            Left,
            Right,
        }


        public State mCurrentState = State.Alive;
        public Direction mCurrentDirection = Direction.Left;

        Vector2 mStartingPosition = new Vector2(START_POSITION_X, START_POSITION_Y);


        ContentManager mContentManager;

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;


            Position = new Vector2(START_POSITION_X, START_POSITION_Y);

            base.LoadContent(theContentManager, ASSETNAME);
            Source = new Rectangle(0, 0, 200, 200);

        }


        public void Update(GameTime theGameTime)
        {

            UpdateBehaviour();

            base.Update(theGameTime, mSpeed, mDirection);
        }


        private void UpdateBehaviour()
        {
            if(mCurrentState == State.Alive)
            {
                if (mCurrentDirection == Direction.Left)
                {
                    mSpeed.X = SPEED;
                    mDirection.X = MOVE_LEFT;
                }

                else if (mCurrentDirection == Direction.Right)
                {
                    mSpeed.X = SPEED;
                    mDirection.X = MOVE_RIGHT;
                }
            }
            else if (mCurrentState == State.Dead)
            {
                mSpeed.X = 0;
                mDirection.X = 0;

                Source = new Rectangle(200, 0, 200, 200);
            }
        }


        public override void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch);
        }



        public override void collidedFloor(Vector2 colcp, float colH)
        {
            if (CenterPoint.Y <= colcp.Y)
            {
                mSpeed.Y = 0;
                falling = false;
            }
            else
            {
                //mSpeed.Y = GRAVITY;
                //onfloor = false;

            }

            if (onfloor) return;

            if (CenterPoint.Y <= colcp.Y)
            {
                falling = false;
                mSpeed = new Vector2(0, 0);
                onfloor = true;
               // mCurrentState = State.Idle;
            }
            else
            {
                return;

            }

            //Position.Y = colcp.Y + (colH/2);

        }


        public override void collidedWall(Vector2 colcp, float colW)
        {
            if ((Position.X <= (colcp.X + (colW * 2))) && ((Position.X >= ((colcp.X + ((colW - 10))))) && (CenterPoint.Y > colcp.Y))) //left collision
            {
                mSpeed.X = 0;
                Position.X = colcp.X + (colW);
            }
            else if (((Position.X + width) >= colcp.X) && ((Position.X + width) <= (colcp.X + 2)) && (CenterPoint.Y > colcp.Y)) //right collision
            {
                mSpeed.X = 0;
                Position.X = (colcp.X - 70);
            }
            else
            {
                return;
            }
        }





    }
}
