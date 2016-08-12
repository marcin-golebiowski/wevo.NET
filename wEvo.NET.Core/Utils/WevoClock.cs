namespace wevo.NET.Core.Utils
{
    /**
    * Clock object interface used by wevo2 library.
    * @author Karol Stosiek (karol.stosiek@gmail.com)
    * @author Michal Anglart (anglart.michal@gmail.com)
    */
    public interface wevoClock
    {
        long GetCurrentTimeMillis();
    }
}
