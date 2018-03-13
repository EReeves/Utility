using System.Collections.Generic;
using TexturePackerLoader;

namespace Game.Shared.Utility
{
    public class SpriteSheets
    {     
        public readonly Dictionary<string, SpriteSheet> SpriteSheetDictionary = new Dictionary<string, SpriteSheet>();
        
        //Singleton Implementation
        private static SpriteSheets instance;
        public static SpriteSheets Instance
        {
            get
            {
                instance = instance ?? new SpriteSheets();
                return instance;
            }
        }             

        public SpriteSheet this[string key]
        {
            get => SpriteSheetDictionary[key];
            set => SpriteSheetDictionary[key] = value;
        }

        public void AddSpriteSheet(string key, SpriteSheet sheet)
        {
            SpriteSheetDictionary[key] = sheet;
        }
    }
}