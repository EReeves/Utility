namespace Game.Shared.Utility
{
    public static class RenderingConstants
    {
        //Floor objects are rendered lower than everything else, will be set less than this value.
        public const int FloorRenderLayerStart = 10;
        //The layer depth floor tiles start at.
        public const float FloorLayerDepthStart = 0.9f;
        //Used for dividing world position to get a layerDepth value.
        public const float WorldPositionDivision = 10000;
        //Used for dividing layer count to get a layerDepth value.
        public const float LayerCountDivision = 1000000;
        //The layer tiles, players and objects are usually rendered to.
        public const int MainRenderLayer = 100;
        //The render depth, players and objects usually start at.
        public const float MainLayerDepthStart = 0.5f;
        
    }
    
}