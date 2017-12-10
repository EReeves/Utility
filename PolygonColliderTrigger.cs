using Microsoft.Xna.Framework;
using Nez;
using Nez.PhysicsShapes;

namespace Game.Shared.Utility
{
    /// <inheritdoc />
    /// <summary>
    /// Just so that it looks different in debug render.
    /// </summary>
    public class PolygonColliderTrigger : PolygonCollider
    {
        public PolygonColliderTrigger(Vector2[] points) : base(points)
        {
            isTrigger = true;
        }

        public PolygonColliderTrigger(int vertCount, float radius) : base(vertCount, radius)
        {
            isTrigger = true;
        }

        public override void debugRender(Graphics graphics)
        {
            var poly = shape as Polygon;
            // ReSharper disable once PossibleNullReferenceException
            graphics.batcher.drawPolygon( shape.position, poly.points, Color.AliceBlue, true, Debug.Size.lineSizeMultiplier );
            
        }
    }
}