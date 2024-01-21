using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIS.ServiceCore.Helper
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

            var expired = _dailyInsertedPhones.Where(w => (w.Value - DateTime.Now).TotalHours > 4).ToList();
            for (int i = 0; i < expired.Count; i++)
            {
                _dailyInsertedPhones.Remove(expired[i].Key);
            }
            return _dailyInsertedPhones.Any(a => a.Key == phone);
        }
    }
}
