namespace Dzaba.Utils.Collections
{
    public interface ICacheEntry<out TValue>
    {
        TValue GetValue();
    }
}
