using Microsoft.Xna.Framework;
using Nez;
using Nez.Debug;
using Nez.ECS.Components.Physics.Colliders;
using Nez.Graphics;
using Nez.Graphics.Batcher;
using Nez.Physics.Shapes;

namespace Game.Shared.Utility
{
    /// <inheritdoc />
    /// <summary>
    ///     Just so that it looks different in debug render.
    /// </summary>
    public class PolygonColliderTrigger : PolygonCollider
    {
        public PolygonColliderTrigger(Vector2[] points) : base(points)
        {
            IsTrigger = true;
        }

        public PolygonColliderTrigger(int vertCount, float radius) : base(vertCount, radius)
        {
            IsTrigger = true;
        }

        public float? Depth { get; set; } = null;

        public override void DebugRender(Graphics graphics)
        {
            var poly = Shape as Polygon;
            // ReSharper disable once PossibleNullReferenceException
            graphics.Batcher.DrawPolygon(Shape.Position, poly.Points, Color.AliceBlue, true,
                Debug.Size.LineSizeMultiplier);
        }
    }
}