﻿namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class CreateTransaction : Transaction
   {
      public string type { get; set; }
      public int divisionID { get; set; }
      public int siteID { get; set; }
      public int accountUserID { get; set; }
      public int accountNumber { get; set; }
      public string homeCurrency { get; set; }
   }
}