using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.ClientApi.Helpers
{
    public static class OrderStatus
    {
        public static string IN_PROGRESS = "IN_PROGRESS";
        public static string SHIPPED = "SHIPPED";
        public static string IN_BACKORDER = "IN_BACKORDER";
        public static string MANCO = "MANCO";
        public static string IN_COMBI = "IN_COMBI";
        public static string CLOSED = "CLOSED";
        public static string NEW = "NEW";
        public static string RETURNED = "RETURNED";
        public static string REQUIRES_CORRECTION = "REQUIRES_CORRECTION";

        public static List<string> List = new List<string>
        {
            OrderStatus.IN_PROGRESS, OrderStatus.SHIPPED, OrderStatus.IN_BACKORDER,
            OrderStatus.MANCO,OrderStatus.IN_COMBI,OrderStatus.CLOSED,
            OrderStatus.NEW,OrderStatus.RETURNED,OrderStatus.REQUIRES_CORRECTION
        };
    }

    public enum OrderStatusEnum
    {
        InProgres,
        Shipped,
        InBackorder,
        Manco,
        InCombi,
        Closed,
        New,
        Returned,
        RequestCorrection
    }
}
