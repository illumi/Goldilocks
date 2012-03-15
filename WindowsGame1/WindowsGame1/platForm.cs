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

        private ContentManager m_ContentManager;
        private String PLATFORM_ASSETNAME = "floor";

        public platForm(float posX, float posY, float length, bool movable)
        {
            Position = new Vector2(posX, posY);
            this.length = length;
            this.positionX = Position.X;
            this.positionY = Position.Y;
            this.movable = movable;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            WidthScale = length;
            HeightScale = 0.2f;
            m_ContentManager = theContentManager;
            base.LoadContent(theContentManager, PLATFORM_ASSETNAME);
        }
    }
}
