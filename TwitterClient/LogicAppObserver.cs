//********************************************************* 
// 
//    Copyright (c) Microsoft. All rights reserved. 
//    This code is licensed under the Microsoft Public License. 
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY 
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR 
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT. 
// 
//*********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;

namespace TwitterClient
{
    class LogicAppObserver : IObserver<TwitterPayload>
    {

        private string url;
        public LogicAppObserver(LogicAppConfig config)
        {
            url = config.LogicAppURL;
        }
        public void OnNext(TwitterPayload TwitterPayloadData)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var serializedString = JsonConvert.SerializeObject(TwitterPayloadData);
                    var result = client.PostAsync(url, new StringContent(serializedString, Encoding.UTF8, "application/json")).Result;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Sending" + serializedString + " at: " + TwitterPayloadData.CreatedAt.ToString() + " with response: " + result.StatusCode );
                }
              
                                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
            }

        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            
        }

    }
}
