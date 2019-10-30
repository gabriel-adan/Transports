using Business.Layer.Model;
using System.Data.ORM.Mapping;

namespace Infraestructure.Layer.Map
{
    public class HourMap : ClassMap<Hour>
    {
        public HourMap()
        {
            Key(h => h.EntryTime);
            Key(h => h.ExitTime);
            Key(h => h.DayOfWeek);
            Key(h => h.Customer);

            ForeignKey(h => h.Customer).ColumnName("CustomerId");
            ForeignKey(h => h.EntryDriver).ColumnName("EntryDriverId");
            ForeignKey(h => h.ExitDriver).ColumnName("ExitDriverId");
        }
    }
}
