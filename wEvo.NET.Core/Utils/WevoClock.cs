namespace wEvo.NET.Core.Utils
{
    /**
    * Clock object interface used by WEvo2 library.
    * @author Karol Stosiek (karol.stosiek@gmail.com)
    * @author Michal Anglart (anglart.michal@gmail.com)
    */
    public interface WevoClock
    {
        long GetCurrentTimeMillis();
    }
}
