namespace Dzaba.Utils
{
    public interface IRandom
    {
        int Next();
        int Next(int maxValue);
        bool IsHit(double percentageChance);
    }

    public class Random : IRandom
    {
        private const int HitByPercentageMax = 10000000;

        private System.Random rand = new System.Random();

        public bool IsHit(double percentageChance)
        {
            Require.InRange(percentageChance, 0, 100, nameof(percentageChance));

            var hit = rand.Next(HitByPercentageMax);
            var perc = hit / (double)HitByPercentageMax * 100.0;
            return percentageChance >= perc;
        }

        public int Next()
        {
            return rand.Next();
        }

        public int Next(int maxValue)
        {
            return rand.Next(maxValue);
        }
    }
}
