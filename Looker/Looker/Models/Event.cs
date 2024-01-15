using System;

namespace Looker.Models
{
    public class Event
    {
        public DateTime EV_DATE { get; set; }

        public EVENT_TYPE EV_CODE { get; set; }

        public int EV_OBJID { get; set; }

        public int EV_CARDID { get; set; }

        public int EV_OW_ID { get; set; }
    }

    public enum EVENT_TYPE
    {
        CART = 24,
        OPERATOR = 25,
        UNKNOWN = 42,
        BREAK = 49,
        BREAK_ALT = 50
    }
}
