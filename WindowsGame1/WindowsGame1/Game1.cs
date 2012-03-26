using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CGProj
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Engine.Pyhsics m_physicsEngine = new Engine.Pyhsics();
        private TerainManager mTerainManager = new TerainManager();

        public enum GameStates
        {
            Menu,
            InGame,
            Building,
        }

        Menu menu;

        public static GameStates gamestate;
        private SpriteFont arial;

        public bool screenloaded = false;
        public int currscreen = 0;

        Sprite mBackgroundOne;
        Sprite mBackgroundTwo;
        Sprite mBackgroundThree;

        Ninja mNinjaSprite;

        AI mWizardSprite;

       // StaminaBar mStamina;
      //  Sprite bee2;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        String Back1 = "level1_clouds";
        //String Back2 = "Background02";
        String Back3 = "level1";
        //String Back4 = "Background04";
        //String Back5 = "Background05";
        



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            menu = new Menu();
            gamestate = GameStates.Menu;
            
            this.IsMouseVisible = true;

            //Change the resolution to 1024X700

            graphics.PreferredBackBufferWidth = 1024;

            graphics.PreferredBackBufferHeight = 700;

            graphics.ApplyChanges();

            // TODO: Add your initialization logic here


            mNinjaSprite = new Ninja();
            mNinjaSprite.HeightScale = 0.4f;
            mNinjaSprite.WidthScale = 0.4f;



            mWizardSprite = new AI();
            mWizardSprite.HeightScale = 0.4f;
            mWizardSprite.WidthScale = 0.4f;


            //  bee2 = new Sprite();
            // mStamina = new StaminaBar();



            mBackgroundOne = new Sprite();

            mBackgroundOne.Scale = 2.0f;

            //mBackgroundTwo = new Sprite();

            //mBackgroundTwo.Scale = 2.0f;

            mBackgroundThree = new Sprite();

            mBackgroundThree.WidthScale = 1f;

            mBackgroundThree.HeightScale = 1f;

            /*mBackgroundFour = new Sprite();

            mBackgroundFour.Scale = 2.0f;

            mBackgroundFive = new Sprite();

            mBackgroundFive.Scale = 2.0f;*/

 

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mTerainManager.loadTerainContents(this.Content);
            mTerainManager.registerSprites(this.m_physicsEngine);
            // TODO: use this.Content to load your game content here
            arial = Content.Load<SpriteFont>("Arial");
            mNinjaSprite.LoadContent(this.Content);

            mWizardSprite.LoadContent(this.Content);

            this.m_physicsEngine.registerMoveableSolid(mNinjaSprite);

            this.m_physicsEngine.registerMoveableSolid(mWizardSprite);

          //  mStamina.LoadContent(this.Content);

           // bee2.LoadContent(this.Content, "BEE");

           // bee2.Position = new Vector2(50,50);

            mBackgroundOne.LoadContent(this.Content, Back1);

            mBackgroundOne.Position = new Vector2(0, 0);

           // mBackgroundTwo.LoadContent(this.Content, Back2);

           // mBackgroundTwo.Position = new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 0);
           // mBackgroundThree.Source = new Rectangle(0,0,1024, 450);

            mBackgroundThree.LoadContent(this.Content, Back3);

            mBackgroundThree.Position = new Vector2(0,240);//240

            //mBackgroundThree.Position = new Vector2(mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width, 0);

           /* mBackgroundFour.LoadContent(this.Content, Back4);

            mBackgroundFour.Position = new Vector2(mBackgroundThree.Position.X + mBackgroundThree.Size.Width, 0);

            mBackgroundFive.LoadContent(this.Content, Back5);

            mBackgroundFive.Position = new Vector2(mBackgroundFour.Position.X + mBackgroundFour.Size.Width, 0);*/

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();*/

            if (gamestate == GameStates.Menu)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
                {
                    menu.Iterator++;
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
                {
                    menu.Iterator--;
                }

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.E))
                {
                    if (menu.Iterator == 0)
                    {
                        gamestate = GameStates.InGame;
                        SetUpGame();
                    }
                    else if (menu.Iterator == 1)
                    {
                        this.Exit();
                    }
                    menu.Iterator = 0;
                }
            }
            else if (gamestate == GameStates.InGame)
            {

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                {
                    gamestate = GameStates.Menu;
                }

                mTerainManager.Update(gameTime); // ############THIS IS THE METHOD THAT MOVES THE BLOCKS WITH THE MOUSE [PUT IN IT'S OWN STATE]####
                
                mNinjaSprite.Update(gameTime);

                mWizardSprite.Update(gameTime);


                if (mWizardSprite.mCurrentState != AI.State.Dead)
                {

                    if (m_physicsEngine.m_colDetector.occupiesSameXSpace(mNinjaSprite.CenterPoint, mNinjaSprite.width, mWizardSprite.CenterPoint, mWizardSprite.width) && m_physicsEngine.m_colDetector.occupiesSameYSpace(mNinjaSprite.CenterPoint, mNinjaSprite.height, mWizardSprite.CenterPoint, mWizardSprite.height))
                    {
                        if (mNinjaSprite.CenterPoint.X >= mWizardSprite.Position.X && mNinjaSprite.CenterPoint.X <= mWizardSprite.CenterPoint.X)
                        {
                            if (mNinjaSprite.CenterPoint.Y >= mWizardSprite.Position.Y && mNinjaSprite.CenterPoint.Y <= mWizardSprite.CenterPoint.Y)
                            {
                                mNinjaSprite.Position.X = 0;
                                mNinjaSprite.Position.Y = 100;
                            }
                        }
                        
                    }

                    if (mNinjaSprite.Position.X < mWizardSprite.Position.X)
                    {
                        mWizardSprite.mCurrentDirection = AI.Direction.Left;
                    }

                    else if (mNinjaSprite.Position.X > mWizardSprite.Position.X)
                    {
                        mWizardSprite.mCurrentDirection = AI.Direction.Right;
                    }

                    if (mNinjaSprite.CenterPoint.Y < mWizardSprite.Position.Y)
                    {
                        mWizardSprite.up = true;
                    }
                    else mWizardSprite.up = false;


                    if (mNinjaSprite.mCurrentState == Ninja.State.Attack)
                    {
                        if (((mNinjaSprite.Position.X + mNinjaSprite.ATTACKRANGE) > mWizardSprite.Position.X) && mNinjaSprite.mCurrentDirection == Ninja.Direction.Right)
                        {
                            mWizardSprite.mCurrentState = AI.State.Dead;
                        }
                        else if (((mNinjaSprite.Position.X - mNinjaSprite.ATTACKRANGE) > mWizardSprite.Position.X) && mNinjaSprite.mCurrentDirection == Ninja.Direction.Left)
                        {
                            mWizardSprite.mCurrentState = AI.State.Dead;
                        }
                    }

                }
                //mStamina.stamina = mBeeSprite.stamina;


                if (mNinjaSprite.Position.X >= 1024)
                {
                    mNinjaSprite.Position.X = 0;
                    mWizardSprite.Position.Y = 100;
                    mWizardSprite.Position.X = 200;
                    mWizardSprite.mCurrentState = AI.State.Alive;
                    currscreen++;
                    mTerainManager.currscreen = currscreen;
                    mTerainManager.loadLevel(this.m_physicsEngine);
                    mTerainManager.loadTerainContents(this.Content);
                    mTerainManager.registerSprites(this.m_physicsEngine);
                    mBackgroundThree.Position.X -= 1024;
                    screenloaded = false;
                }
                else if (mNinjaSprite.Position.X < 0)
                {
                    mNinjaSprite.Position.X = 0;
                }

                else if (mNinjaSprite.Position.Y > 800)
                {
                    mNinjaSprite.Position.Y = 100;
                    mNinjaSprite.Position.X = 0;
                }


                if (mWizardSprite.Position.X >= 1024)
                {
                    mWizardSprite.Position.X = 0;
                }

                else if (mWizardSprite.Position.X < 0)
                {
                    mWizardSprite.Position.X = 0;
                }

                else if (mWizardSprite.Position.Y > 800)
                {

                    mWizardSprite.mCurrentState = AI.State.Dead;

                    mWizardSprite.Position.Y = 100;
                    mWizardSprite.Position.X = 200;

                    
                }



                if (currscreen == 1 && screenloaded == false)
                {
                   // Back1 = "comeatmebro";
                   /* Back2 = "comeatmebro";
                    Back3 = "comeatmebro";
                    Back4 = "comeatmebro";
                    Back5 = "comeatmebro";*/
                    mBackgroundOne.LoadContent(this.Content, Back1);
                    screenloaded = true;
                }
                else if (currscreen == 2 && screenloaded == false)
                {
                   // Back1 = "BackgroundCloud01";
                   /* Back2 = "BackgroundCloud01";
                    Back3 = "BackgroundCloud01";
                    Back4 = "BackgroundCloud01";
                    Back5 = "BackgroundCloud01";*/
                    mBackgroundOne.LoadContent(this.Content, Back1);
                    /*mBackgroundTwo.LoadContent(this.Content, Back2);
                    mBackgroundThree.LoadContent(this.Content, Back3);
                    mBackgroundFour.LoadContent(this.Content, Back4);
                    mBackgroundFive.LoadContent(this.Content, Back5);*/
                    screenloaded = true;
                }
                /* TODO needs fixed to take into acount actual vector of player sprite. Jumping vertically moves background weirdly too.*/
                if (mNinjaSprite.mCurrentDirection == Ninja.Direction.Right && (mNinjaSprite.mSpeed.X > 0) && (mNinjaSprite.mCurrentState == Ninja.State.Walking || mNinjaSprite.mCurrentState == Ninja.State.Jumping))
                {

                    if (mBackgroundOne.Position.X < -mBackgroundOne.Size.Width)
                    {

                       // mBackgroundOne.Position.X = mBackgroundFive.Position.X + mBackgroundFive.Size.Width;
                    }



                  /*  if (mBackgroundTwo.Position.X < -mBackgroundTwo.Size.Width)
                    {

                        mBackgroundTwo.Position.X = mBackgroundOne.Position.X + mBackgroundOne.Size.Width;
                    }



                    if (mBackgroundThree.Position.X < -mBackgroundThree.Size.Width)
                    {

                        mBackgroundThree.Position.X = mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width;
                    }



                    if (mBackgroundFour.Position.X < -mBackgroundFour.Size.Width)
                    {

                        mBackgroundFour.Position.X = mBackgroundThree.Position.X + mBackgroundThree.Size.Width;
                    }



                    if (mBackgroundFive.Position.X < -mBackgroundFive.Size.Width)
                    {

                        mBackgroundFive.Position.X = mBackgroundFour.Position.X + mBackgroundFour.Size.Width;
                    }
                    */
                    Vector2 aDirection = new Vector2(-1, 0);

                    Vector2 aSpeed = new Vector2(160, 0);


                    mBackgroundOne.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                  /*  mBackgroundTwo.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    mBackgroundThree.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    mBackgroundFour.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    mBackgroundFive.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;*/

                }
                else if (mNinjaSprite.mCurrentDirection == Ninja.Direction.Left && (mNinjaSprite.mSpeed.X > 0) && (mNinjaSprite.mCurrentState == Ninja.State.Walking || mNinjaSprite.mCurrentState == Ninja.State.Jumping) && mNinjaSprite.Position.X > 0)
                {

                    if (mBackgroundOne.Position.X > mBackgroundOne.Size.Width)
                    {

                      //  mBackgroundOne.Position.X = mBackgroundFive.Position.X - mBackgroundFive.Size.Width;
                    }



                   /* if (mBackgroundTwo.Position.X > mBackgroundTwo.Size.Width)
                    {

                        mBackgroundTwo.Position.X = mBackgroundOne.Position.X - mBackgroundOne.Size.Width;
                    }



                    if (mBackgroundThree.Position.X > mBackgroundThree.Size.Width)
                    {

                        mBackgroundThree.Position.X = mBackgroundTwo.Position.X - mBackgroundTwo.Size.Width;
                    }



                    if (mBackgroundFour.Position.X > mBackgroundFour.Size.Width)
                    {

                        mBackgroundFour.Position.X = mBackgroundThree.Position.X - mBackgroundThree.Size.Width;
                    }



                    if (mBackgroundFive.Position.X > mBackgroundFive.Size.Width)
                    {

                        mBackgroundFive.Position.X = mBackgroundFour.Position.X - mBackgroundFour.Size.Width;
                    }
                    */
                    Vector2 aDirection = new Vector2(-1, 0);

                    Vector2 aSpeed = new Vector2(160, 0);


                    mBackgroundOne.Position -= aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                  /*  mBackgroundTwo.Position -= aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    mBackgroundThree.Position -= aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    mBackgroundFour.Position -= aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    mBackgroundFive.Position -= aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;*/

                }

                this.m_physicsEngine.notifyCollisions();
            }
            else { /*new game state additions here, pause, loading, whatever.*/ }

            base.Update(gameTime);
        }

        private void SetUpGame()
        {
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

           

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            if (gamestate == GameStates.InGame)
            {
                mBackgroundOne.Draw(this.spriteBatch);

               // mBackgroundTwo.Draw(this.spriteBatch);

                mBackgroundThree.Draw(this.spriteBatch);

                /*mBackgroundFour.Draw(this.spriteBatch);

                mBackgroundFive.Draw(this.spriteBatch);*/

                this.mTerainManager.Draw(this.spriteBatch);

                mNinjaSprite.Draw(this.spriteBatch);

                mWizardSprite.Draw(this.spriteBatch);
                //mStamina.Draw(this.spriteBatch);

                //bee2.Draw(this.spriteBatch);
            }
            else if (gamestate == GameStates.Menu)
            {
                menu.DrawMenu(spriteBatch, graphics.GraphicsDevice.Viewport.Width, arial);
            }
            else { }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
