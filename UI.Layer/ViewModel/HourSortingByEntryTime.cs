using Business.Layer.Model;
using System.Collections;

namespace UI.Layer.ViewModel
{
    public class HourSortingByEntryTime : IComparer
    {
        public int Compare(object a, object b)
        {
            Hour x = a as Hour;
            Hour y = b as Hour;
            int result = x.EntryTime.CompareTo(y.EntryTime);
            if (result == 0)
                return x.ExitTime.CompareTo(y.ExitTime);
            return result;
        }
    }
}
