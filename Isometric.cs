using System;
using System.Runtime.CompilerServices;
using Game.Shared.Components.Map;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Nez.Maths;
using Nez.Utils.Extensions;

namespace Game.Shared.Utility
{
    public static class Isometric
    {
        public static int RenderLayerStart { get; set; } = 1000;

        public static Vector2 NE;
        public static Vector2 NW;
        public static Vector2 SE;
        public static Vector2 SW;

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
      
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        
        public static void CalculateDirectionals()
        {
            var dir = IsometricToWorld(new Point(0, -1),IsometricMap.Instance);
            dir.Normalize();
            
            NE = dir;
            NW = dir.FlipX();
            SE = dir.FlipY();
            SW = dir.FlipX().FlipY();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WorldToIsometric(Vector2 pos, IsometricMap map)
        {
            var xd2 = map.TileWidth / 2;
            var yd2 = map.TileHeight / 2;

            return new Vector2
            {
                X = (pos.X / xd2 + pos.Y / yd2) / 2,
                Y = (pos.Y / yd2 - pos.X / xd2) / 2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WorldToIsometricWorld(Vector2 pos, IsometricMap map)
        {
            pos = new Vector2
            {
                X = pos.X / map.TileHeight,
                Y = pos.Y / map.TileHeight
            };
            return new Vector2
            {
                X = (pos.X - pos.Y) * (map.TileWidth / 2),
                Y = (pos.X + pos.Y) * (map.TileHeight / 2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 IsometricToWorld(Vector2 pos, IsometricMap map)
        {
            return new Vector2
            {
                X = (pos.X - pos.Y) * (map.TileWidth / 2),
                Y = (pos.X + pos.Y) * (map.TileHeight / 2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 IsometricToWorld(Point pos, IsometricMap map)
        {
            return IsometricToWorld(new Vector2(pos.X, pos.Y), map);
        }
    }
}