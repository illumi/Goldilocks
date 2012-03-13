﻿using System;
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
        private float length;
        private ContentManager m_ContentManager;
        private String PLATFORM_ASSETNAME = "floor";

        public platForm(float posX, float posY, float length)
        {
            Position = new Vector2(posX, posY);
            this.length = length;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            WidthScale = length;
            HeightScale = 1;
            m_ContentManager = theContentManager;
            base.LoadContent(theContentManager, PLATFORM_ASSETNAME);
        }
    }
}
