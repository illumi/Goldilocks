using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGProj.Engine
{
    public class Pyhsics
    {
        private CollisionDetection m_colDetector;
        private const float GRAVATY = 1;

        private List<Sprite> m_fixedObjects;
        private List<Sprite> m_moveingObjects;

        public Pyhsics()
        {
            m_colDetector = new CollisionDetection();
            m_fixedObjects = new List<Sprite>();
            m_moveingObjects = new List<Sprite>();
        }

        public void notifyCollisions()
        {
            updateVectors(); //Gravity
            m_colDetector.notifyCollisions();
        }

        public void registerMoveableSolid(Sprite mSolid)
        {
            m_moveingObjects.Add(mSolid);
            m_colDetector.attatchMoveing(mSolid);
        }

        public void registerFixedSolid(Sprite fSolid)
        {
            m_fixedObjects.Add(fSolid);
            m_colDetector.attatchFixed(fSolid);
        }



        public void updateVectors()
        {
            foreach (Sprite moveing in m_moveingObjects)
            {
                if (moveing.falling == false)
                {
                    moveing.decrementYSpeed();
                }
                else if (moveing.falling == true)
                {
                    moveing.incrementYSpeed();
                }
            }
        }
    }


}
