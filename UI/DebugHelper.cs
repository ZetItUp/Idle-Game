using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Idle_Game;
public static class DebugHelper
{
    private static SpriteFont _font;
    private static SpriteBatch _spriteBatch;
    private static bool _begun = false;

    public static void Initialize(SpriteBatch spriteBatch, SpriteFont font)
    {
        _spriteBatch = spriteBatch;
        _font = font;
    }

    public static void Begin()
    {
        if (!_begun)
        {
            _spriteBatch.Begin();
            _begun = true;
        }
    }

    public static void DrawText(string text, int x, int y, Color color)
    {
        if (_font == null || _spriteBatch == null)
            return;

        Begin(); // Startar batchen om inte redan igång
        _spriteBatch.DrawString(_font, text, new Vector2(x, y), color);
    }

    public static void End()
    {
        if (_begun)
        {
            _spriteBatch.End();
            _begun = false;
        }
    }
}