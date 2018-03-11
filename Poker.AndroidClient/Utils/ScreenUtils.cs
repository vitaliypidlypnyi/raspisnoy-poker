namespace Poker.AndroidClient.Utils
{
    public static class ScreenUtils
    {
        public static int ToDp(this int value, float displayDensity)
        {
            return (int)(value / displayDensity);
        }
    }
}