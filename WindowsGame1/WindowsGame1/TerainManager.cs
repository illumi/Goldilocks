using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace CGProj
{
    class TerainManager :Sprite
    {
        private List<platForm> terrains = new List<platForm>();

        public TerainManager()
        {
            // initalise the map terains here

            terrains.Add(new platForm(400, 450, 1));
            terrains.Add(new platForm(0, 570, 6));
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

    }
}
