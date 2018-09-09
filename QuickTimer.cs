namespace Game.Shared.Utility
{
    public class QuickTimer
    {
        private int counter;

        public void Tick()
        {
            counter++;
            if (counter >= 60) counter = 0;
        }

        public bool ShouldFire(int i)
        {
            var count = 60 / i;
            return counter >= count;
        }
    }
}