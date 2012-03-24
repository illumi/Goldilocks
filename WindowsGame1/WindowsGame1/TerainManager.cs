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
        public int currscreen;



        public TerainManager()
        {
            // initalise the map terains here
            //1024
            
            //terrains.Add(new platForm(600, 380, 1, true, "platform", 0, 0, 200, 30));
            //terrains.Add(new platForm(200, 380, 1, true, "platform", 0, 0, 200, 30));

            //screen 1
            if (currscreen == 0)
            {
                terrains.Add(new platForm(0, 570, 1, false, "level1", 0, 330, 245, 20));
                terrains.Add(new platForm(243, 538, 1, false, "level1", 243, 298, 56, 100));//243 298			56 100
                terrains.Add(new platForm(298, 602, 1, false, "level1", 298, 362, 341, 88));//298 362			341 88
                terrains.Add(new platForm(503, 522, 1, false, "level1", 503, 282, 76, 20));//503 282			76 20 
                terrains.Add(new platForm(703, 602, 1, false, "level1", 703, 362, 616, 80)); // 703 362			616 80	
                terrains.Add(new platForm(843, 555, 1, false, "level1", 843, 315, 96, 63));// 843 315			96 63
                terrains.Add(new platForm(904, 506, 1, false, "level1", 904, 266, 96, 111));// 904 266			96 111
                terrains.Add(new platForm(998, 538, 1, false, "level1", 998, 298, 21, 82));//998 298			21 82
            }


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
           // mPreviousMouseState = aCurrentMouseState;
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
                            mPreviousMouseState = aCurrentMouseState;
                            p.CenterPointX = aCurrentMouseState.X;
                            p.CenterPointY = aCurrentMouseState.Y;
                        }
                    }
                }

            }


        }


        public void loadLevel(Engine.Pyhsics physicsEngine)
        {
            //screen 2
            if (currscreen == 1)
            {
                physicsEngine.deRegisterFixedSolids();
                terrains.Clear();

                terrains.Add(new platForm(0, 602, 1, false, "level1", 1024, 362, 296, 15));

                terrains.Add(new platForm(390, 602, 1, false, "level1", 1414, 362, 96, 49));//1414 362		96 49

                terrains.Add(new platForm(478, 586, 1, false, "level1", 1502, 346, 20, 15));//1502 346		20 15

                terrains.Add(new platForm(419, 523, 1, false, "level1", 1443, 283, 97, 20));//1443 283		97 20

                terrains.Add(new platForm(577, 523, 1, false, "level1", 1601, 283, 20, 15));//1601 283		20 15

                terrains.Add(new platForm(494, 618, 1, false, "level1", 1518, 378, 247, 63));//1518 378		247 63

                terrains.Add(new platForm(738, 582, 1, false, "level1", 1762, 342, 65, 50));//1762 342		65 50

                terrains.Add(new platForm(799, 555, 1, false, "level1", 1823, 315, 136, 79));//1823 315		136 79

                terrains.Add(new platForm(934, 602, 1, false, "level1", 1958, 362, 81, 79));//1958 362		81 79

                terrains.Add(new platForm(673, 445, 1, false, "level1", 1697, 205, 20, 15));//1697 205		20 15

                terrains.Add(new platForm(783, 374, 1, false, "level1", 1807, 134, 20, 15));//1807 134		20 15

                terrains.Add(new platForm(878, 330, 1, false, "level1", 1902, 90, 60, 15));//1902 90 		60 15
            }


            if (currscreen == 2)
            {
                physicsEngine.deRegisterFixedSolids();
                terrains.Clear();

                terrains.Add(new platForm(56, 603, 1, false, "level1", 2104, 363, 55, 80));//2104 363		55 80

                terrains.Add(new platForm(175, 603, 1, false, "level1", 2223, 363, 248, 40));//2223 363		248 40

                terrains.Add(new platForm(415, 571, 1, false, "level1", 2463, 331, 150, 50));//2463 331		150 50

                terrains.Add(new platForm(555, 523, 1, false, "level1", 2603, 283, 141, 56));//2603 283		141 56

                terrains.Add(new platForm(695, 475, 1, false, "level1", 2743, 235, 76, 74));//2743 235		76 74

                terrains.Add(new platForm(735, 427, 1, false, "level1", 2783, 187, 97, 20));//2783 187		97 20

                terrains.Add(new platForm(766, 522, 1, false, "level1", 2814, 282, 362, 161));//2814 282		362 161 
            }

            if (currscreen == 3)
            {
                physicsEngine.deRegisterFixedSolids();
                terrains.Clear();


                terrains.Add(new platForm(0, 522, (0.2f), false, "level1", 2814, 282, 362, 161));
               // terrains.Add(new platForm(0, 603, 1, false, "platform", 0, 0, 200, 30));
                terrains.Add(new platForm(800, 603, 1, false, "platform", 0, 0, 200, 30));

                terrains.Add(new platForm(100, 200, 1, true, "Make", 0, 0, 160, 100));
                terrains.Add(new platForm(260, 200, 1, true, "A", 0, 0, 160, 100));
                terrains.Add(new platForm(420, 200, 1, true, "Bridge", 0, 0, 160, 100));
                terrains.Add(new platForm(580, 200, 1, true, "With", 0, 0, 160, 100));
                terrains.Add(new platForm(740, 200, 1, true, "Words", 0, 0, 160, 100));
                terrains.Add(new platForm(200, 50, 1, false, "Instructions", 0, 0, 600, 150));

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
