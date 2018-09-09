using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Shared.Utility
{
    static public class BatcherExtensions
    {
        private static Texture2D whiteTexture;
        private static Rectangle oneByOne;
        
        public static void Load(string whiteTexture, ContentManager content)
        {
            BatcherExtensions.whiteTexture = content.Load<Texture2D>(whiteTexture);
            oneByOne = new Rectangle(0,0,1,1);
        }
        
        public static void DrawSolidRectangle(this SpriteBatch batcher, Rectangle rect, Color backColor, float rot = 0f, float depth = 1f)
        {
            batcher.Draw(whiteTexture, rect, oneByOne, backColor,rot, Vector2.Zero, SpriteEffects.None,depth );
        }
    }
}