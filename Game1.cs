using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Idle_Game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        DebugHelper.Initialize(_spriteBatch,
                                Content.Load<SpriteFont>("Fonts\\debugfont"),
                                Content.Load<SpriteFont>("Fonts\\debugfontBig"));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        DebugHelper.Begin();

        BigNumber b = new BigNumber(232);
        DebugHelper.DrawText($"Gold: {b.ToString()}", 10, 10, Color.Black, true);
        DebugHelper.DrawText($"FPS: {1f / gameTime.ElapsedGameTime.TotalSeconds:0}", 10, 30, Color.DarkRed, true);
        DebugHelper.End();

        base.Draw(gameTime);
    }
}
