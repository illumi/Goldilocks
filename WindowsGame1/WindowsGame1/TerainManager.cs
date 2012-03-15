using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CGProj
{
    class TerainManager :Sprite
    {
        public List<platForm> terrains = new List<platForm>();
        MouseState mPreviousMouseState;

        public TerainManager()
        {
            // initalise the map terains here
 
            terrains.Add(new platForm(600, 380, 1, true));
            terrains.Add(new platForm(200, 380, 1, true));
            terrains.Add(new platForm(0, 570, 6, false));
        }

        public void registerSprites(Engine.Pyhsics physicsEngine)
        {
            foreach (platForm p in terrains)
                physicsEngine.registerFixedSolid(p);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch theSpriteBatch)
        {
            foreach (platForm p in terrains)
                p.Draw(theSpriteBatch);
        }

        public void loadTerainContents(ContentManager cm)
        {
            foreach (platForm p in terrains)
                p.LoadContent(cm);
        }

        public void Update(GameTime theGameTime)
        {
            MouseState aCurrentMouseState = Mouse.GetState();
            UpdateMovingBlock(aCurrentMouseState);
            mPreviousMouseState = aCurrentMouseState;

        }


        private void UpdateMovingBlock(MouseState aCurrentMouseState)
        {
            if (aCurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (platForm p in terrains)
                {
                    if (occupiesSameYSpace(aCurrentMouseState.Y, p.CenterPoint, (p.height / 2))
          && occupiesSameXSpace(aCurrentMouseState.X, p.CenterPoint, (p.width/2)))
                    {
                        if (p.movable == true)
                        {
                            p.CenterPointX = aCurrentMouseState.X;
                            p.CenterPointY = aCurrentMouseState.Y;
                        }
                    }
                }

            }


        }



        public Boolean occupiesSameXSpace(float mouseX, Vector2 platformCenter, float platformWidth)
        {
            float platformRightEdge = platformCenter.X + platformWidth;
            float platformLeftEdge = platformCenter.X - platformWidth;

            if ((mouseX >= platformLeftEdge) && (mouseX <= platformRightEdge))
                return true;

            return false;
        }
        public Boolean occupiesSameYSpace(float mouseY, Vector2 platformCenter, float platformHeight)
        {

            float platformBottomEdge = platformCenter.Y + platformHeight;
            float platformTopEdge = platformCenter.Y - platformHeight;

            if ((mouseY >= platformTopEdge) && (mouseY <= platformBottomEdge))
                return true;

            return false;
        }







    }
}
