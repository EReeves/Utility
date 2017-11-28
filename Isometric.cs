using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using Game.Shared.Components.Map;
using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;

namespace Game.Shared.Utility
{
    public class Isometric
    {    //Uses this map for tile width/height if not specified.
        private static IsometricMap map;
        public static IsometricMap Map
        {
            get => map;
            set
            {
                map = value;
                dimensions = new Vector2()
                {
                    X = (float)value.TileWidth/2,
                    Y = (float)value.TileHeight/2
                };
            }
        }

        private static Vector2 dimensions;
        
        private const string mapNotDefined = "Map must be set if tile demensions aren't specified.";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WorldToIsometric(Vector2 pos)
        {
            Assert.isNotNull(Map, mapNotDefined);

            return new Vector2()
            {
                X = (pos.X / dimensions.X + pos.Y / dimensions.Y) / 2,
                Y = (pos.Y / dimensions.Y - (pos.X / dimensions.X)) / 2
            };
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 IsometricToWorld(Point pos)
        {
            Assert.isNotNull(Map, mapNotDefined);
            return new Vector2()
            {
                X = (pos.X - pos.Y) * dimensions.X,
                Y = (pos.X + pos.Y) * (38/2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WorldToIsometric(Vector2 vec, Vector2 tileDimensions)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 IsometricToWorld(Vector2 vec, Vector2 tileDimensions)
        {
            throw new NotImplementedException();

        }
        
        
    }
}