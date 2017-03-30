﻿using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Order
{
   public class TakeProfitOrder : Order
   {
      public string type { get; set; }
      public long tradeId { get; set; }
      public string clientTradeID { get; set; }
      public double price { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string positionFill { get; set; }
      public string triggerCondition { get; set; }
      public long? fillingTransactionID { get; set; }
      public string filledTime { get; set; }
      public long? tradeOpenedID { get; set; }
      public long? tradeReducedID { get; set; }
      public List<long> tradeClosedIDs { get; set; }
      public long? cancellingTransactionID { get; set; }
      public string cancelledTime { get; set; }
      public long? replacesOrderID { get; set; }
      public long? replacedByOrderID { get; set; }
   }
}
