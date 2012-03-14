using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;


namespace CGProj
{
    public class Sprite
    {
        //The current position of the Sprite

        public Vector2 Position = new Vector2(0, 0);
        protected Vector2 mDirection = Vector2.Zero;
        protected Vector2 mSpeed = Vector2.Zero;
        //The texture object used when drawing the sprite


        public const int GRAVITY = 250;


        public string AssetName;


        protected Texture2D mSpriteTexture;
        

        public Rectangle Size;

        private float mWidthScale = 0.5f;
        private float mHeightScale = 0.5f;

        public Vector2 CenterPoint
        {
            get { Vector2 centP = new Vector2();
            centP.X = Position.X + (width / 2);
            centP.Y = Position.Y - (height / 2);
            return centP;
            }
        }
        public float height
        {
            get { return Size.Height; }
        }
        public float width
        {
            get { return Size.Width; }
        }

        public bool onfloor = true;

        

        //Load the texture for the sprite using the Content Pipeline



        //The Rectangular area from the original image that 

        //defines the Sprite. 

        Rectangle mSource;

        public Rectangle Source
        {

            get { return mSource; }

            set
            {

                mSource = value;

                Size = new Rectangle(0, 0, (int)(mSource.Width * Scale), (int)(mSource.Height * Scale));

            }

        }




        //When the scale is modified throught he property, the Size of the 

        //sprite is recalculated with the new scale applied.

        public float Scale
        {
            get { return mWidthScale; }
            set
            {
                mWidthScale = value;
                mHeightScale = value;
                //Recalculate the Size of the Sprite with the new scale

                Size = new Rectangle(0, 0, (int)(Source.Width * mWidthScale), (int)(Source.Height * mHeightScale));
            }
        }


        public float WidthScale
        {
            get { return mWidthScale; }
            set
            {
                mWidthScale = value;

                //Recalculate the Size of the Sprite with the new scale

                Size = new Rectangle(0, 0, (int)(Source.Width * WidthScale), (int)(Source.Height * HeightScale));
            }
        }

        public float HeightScale
        {
            get { return mHeightScale; }
            set
            {
                mHeightScale = value;

                //Recalculate the Size of the Sprite with the new scale

                Size = new Rectangle(0, 0, (int)(Source.Width * WidthScale), (int)(Source.Height * HeightScale));
            }
        }





        public void decrementYSpeed()
        {
            mSpeed.Y -= GRAVITY;
            if(mSpeed.Y <= 0)
            {
                mSpeed.Y *= -1;
                mDirection.Y = 1;
            }
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.

        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {

            Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;

        }



        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {

            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Source = new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * WidthScale), (int)(mSpriteTexture.Height * HeightScale));
        }

        public virtual void Draw(SpriteBatch theSpriteBatch)
        {

            theSpriteBatch.Draw(mSpriteTexture, Position, Source,
                Color.White, 0.0f, Vector2.Zero, new Vector2(WidthScale, HeightScale), SpriteEffects.None, 0);

        }


        #region collision_Detection_Virtuals
        public virtual void collidedFloor(Vector2 colcp, float colH)
        {
            int i = 0;
            i++;
        }
        #endregion




    }

}
