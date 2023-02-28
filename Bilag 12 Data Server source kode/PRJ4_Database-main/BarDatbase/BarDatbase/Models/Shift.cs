using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #region Relationships

        public int BartenderId { get; set; }
        public Bartender Bartender { get; set; }

        #endregion
    }
}
