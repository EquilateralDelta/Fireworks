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
using BloomPostprocess;

namespace Fireworks
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static public EntityController control;
        static public Vector2 windowSize;
        BloomComponent bloom;

        Song song;
        Texture2D pineBase, pineLights;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.IsFullScreen = true;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            bloom = new BloomComponent(this);
            Components.Add(bloom);
            bloom.Settings = new BloomSettings(null, 0.01f, 1, 5, .5f, 2, 1);
        }

        protected override void Initialize()
        {
            windowSize.X = graphics.PreferredBackBufferWidth;
            windowSize.Y = graphics.PreferredBackBufferHeight;

            base.Initialize();
            control = new EntityController();
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.Load(Content);
            song = Content.Load<Song>("newYear");
            MediaPlayer.Play(song);
            pineBase = Content.Load<Texture2D>("pineBase");
            pineLights = Content.Load<Texture2D>("pineLights");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        MouseState lastMState;
        bool forPreventInitialiseExit = false;
        //KeyboardState lastKState = new KeyboardState();

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mState = Mouse.GetState();
            if (lastMState == null)
                lastMState = mState; 
            KeyboardState kState = Keyboard.GetState();
            if ((kState.GetPressedKeys().Length != 0) ||
                (mState.X != lastMState.X) || (mState.Y != lastMState.Y))
            {
                if (forPreventInitialiseExit)
                    Exit();
                else
                    forPreventInitialiseExit = true;
            }
                
            lastMState = mState;

            control.Update(gameTime.ElapsedGameTime.Milliseconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            bloom.BeginDraw();
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            control.Draw(spriteBatch);
            spriteBatch.Draw(pineBase, new Vector2(windowSize.X - pineBase.Width + 20,
                windowSize.Y - pineBase.Height + 25), new Color(30, 30, 30));
            spriteBatch.Draw(pineLights, new Vector2(windowSize.X - pineLights.Width + 20,
                windowSize.Y - pineLights.Height + 25), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
