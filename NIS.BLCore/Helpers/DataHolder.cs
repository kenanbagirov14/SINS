using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIS.BLCore.Helpers
{
    public class DataHolder
    {
        private static Dictionary<string, DateTime> _dailyInsertedPhones = new Dictionary<string, DateTime>();
        public static bool DailyInsertedPhones(string phone, bool add = false)
        {
            if (add)
            {
                _dailyInsertedPhones.Add(phone, DateTime.Now);
                return true;
            }
            DateTime thisDate1 = DateTime.Now;
            var ad = (DateTime.Now - (DateTime)thisDate1).TotalHours;

            var expired = _dailyInsertedPhones.Where(w => (DateTime.Now - (DateTime)w.Value).TotalHours > 4).ToList();

            for (int i = 0; i < expired.Count; i++)
            {
                _dailyInsertedPhones.Remove(expired[i].Key);
            }
            return _dailyInsertedPhones.Any(a => a.Key == phone); 
        }
    }
}
