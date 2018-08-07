﻿using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
      /// <summary>
      /// Set the client-configurable portions onf an Account
      /// http://developer.oanda.com/rest-live-v20/account-ep/
      /// </summary>
      /// <param name="accountID">Account Identifier</param>
      /// <param name="parameters">The parameters for the request</param>
      /// <returns>an AccountConfigurationResponse object containing the updated values that were applied to the account</returns>
      public static async Task<AccountConfigurationResponse> PatchAccountConfigurationAsync(string accountID, AccountConfigurationParameters parameters)
      {
         string uri = ServerUri(EServer.Account) + "accounts/" + accountID + "/configuration";
 
         var response = await MakeRequestWithJSONBody<AccountConfigurationResponse, AccountConfigurationErrorResponse, AccountConfigurationParameters>("PATCH", parameters, uri);

         return response;
      }

      public class AccountConfigurationParameters
      {
         /// <summary>
         /// Client-defined alias (name) for the Account
         /// </summary>
         [DataMember(EmitDefaultValue = false)]
         public string alias { get; set; }

         /// <summary>
         /// The margin rate the Account should be set to.
         /// </summary>
         [DataMember(EmitDefaultValue = false)]
         [JsonConverter(typeof(StringDecimalConverter))]
         public decimal? marginRate { get; set; }
      }
   }

   public class AccountConfigurationResponse : Response
   {
      /// <summary>
      /// The transaction that configures the Account.
      /// </summary>
      public ClientConfigureTransaction clientConfigureTransaction;
   }

   public class AccountConfigurationErrorResponse : ErrorResponse
   {
      /// <summary>
      /// The transaction that rejects the configuration of the Account.
      /// </summary>
      public ClientConfigureRejectTransaction clientConfigureRejectTransaction { get; set; }
   }
}