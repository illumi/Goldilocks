using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XNA_Game_UnitTests
{
    /// <summary>
    /// Summary description for ColDetectionXspaceOcupier
    /// </summary>
    [TestClass]
    public class ColDetectionXspaceOcupier
    {

        [TestMethod]
        public void objectsOccupieSameSpace()
        {
           CGProj.Engine.CollisionDetection colDetect = new CGProj.Engine.CollisionDetection();

           bool xSpace = colDetect.occupiesSameXSpace(new Microsoft.Xna.Framework.Vector2(10, 0), 5, new Microsoft.Xna.Framework.Vector2(17), 5);
           Assert.AreEqual(xSpace, true);
        }

        [TestMethod]
        public void objectsDontOccupieSameSpace()
        {
            CGProj.Engine.CollisionDetection colDetect = new CGProj.Engine.CollisionDetection();

            bool xSpace = colDetect.occupiesSameXSpace(new Microsoft.Xna.Framework.Vector2(10, 0), 5, new Microsoft.Xna.Framework.Vector2(21), 5);
            Assert.AreEqual(xSpace, false);
        }
    }
}
