using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CGProj
{
    class platForm :Sprite
    {
        public float length;
        public bool movable;
        public float positionX 
        {
            get 
            {
                return positionX; 
            } 
            set 
            {
                //positionX = value;
                Position.X = value; 
            } 
        }
        public float positionY 
        {
            get 
            {
                return positionY; 
            }

            set 
            {
                //positionY = value;
                Position.Y = value; 
            } 
        }

        //public string asset;
        public int sourcex;
        public int sourcey;
        public int sourcewidth;
        public int sourceheight;
        
        public ContentManager m_ContentManager;
        public String PLATFORM_ASSETNAME = "platform";

        public platForm(float posX, float posY, float length, bool movable, String PLATFORM_ASSETNAME, int sourcex, int sourcey, int sourcewidth, int sourceheight)
        {
            Position = new Vector2(posX, posY);
            this.length = length;
            this.positionX = Position.X;
            this.positionY = Position.Y;
            this.movable = movable;
            this.PLATFORM_ASSETNAME = PLATFORM_ASSETNAME;
            this.sourcex = sourcex;
            this.sourcey = sourcey;
            this.sourcewidth = sourcewidth;
            this.sourceheight = sourceheight;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            WidthScale = length;
            HeightScale = 1f;
            m_ContentManager = theContentManager;
            base.LoadContent(theContentManager, PLATFORM_ASSETNAME);
            Source = new Rectangle(sourcex, sourcey, sourcewidth, sourceheight);
        }
    }
}
