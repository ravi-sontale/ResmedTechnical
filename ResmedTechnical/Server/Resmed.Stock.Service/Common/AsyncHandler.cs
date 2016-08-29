using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Practices.Unity;
using Resmed.Stock.Core;
using Resmed.Stock.Model;
using Resmed.Stock.Repositories;

namespace Resmed.Stock.Service.Common
{
    public class StockAsyncHandler : IAsyncHandler
    {
        public static ManualResetEvent AllDone = new ManualResetEvent(false);
        private const int BufferSize = 1024;

        public void CreateReport(IEnumerable<Site> sites, IList<string> symbols)
        {
            foreach (var site in sites)
            {
                foreach (var symbol in symbols)
                {
                    var url = string.Format(site.Url, symbol);
                    var req = WebRequest.Create(url);
                    req.Proxy = WebRequest.GetSystemWebProxy();
                    req.UseDefaultCredentials = true;
                    req.Proxy.Credentials = CredentialCache.DefaultCredentials;

                    var rs = new RequestState
                    {
                        Request = req,
                        SiteId = site.ID,
                        StockSymbol = symbol
                    };
                    req.BeginGetResponse(RespCallback, rs);
                    AllDone.WaitOne();
                }
            }
        }

        private static void RespCallback(IAsyncResult ar)
        {
            // Get the RequestState object from the async result.
            var rs = (RequestState)ar.AsyncState;

            // Get the WebRequest from RequestState.
            var req = rs.Request;

            // Call EndGetResponse, which produces the WebResponse object
            //  that came from the request issued above.
            var resp = req.EndGetResponse(ar);

            //  Start reading data from the response stream.
            var responseStream = resp.GetResponseStream();

            // Store the response stream in RequestState to read 
            // the stream asynchronously.
            rs.ResponseStream = responseStream;

            //  Pass rs.BufferRead to BeginRead. Read data into rs.BufferRead
            if (responseStream != null)
            {
                responseStream.BeginRead(rs.BufferRead, 0,
                    BufferSize, ReadCallBack, rs);
            }
        }

        private static void ReadCallBack(IAsyncResult asyncResult)
        {
            // Get the RequestState object from AsyncResult.
            var rs = (RequestState)asyncResult.AsyncState;

            // Retrieve the ResponseStream that was set in RespCallback. 
            var responseStream = rs.ResponseStream;

            // Read rs.BufferRead to verify that it contains data. 
            var read = responseStream.EndRead(asyncResult);
            if (read > 0)
            {
                // Prepare a Char array buffer for converting to Unicode.
                var charBuffer = new char[BufferSize];

                // Convert byte stream to Char array and then to String.
                // len contains the number of characters converted to Unicode.
                var len =
                   rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);

                var str = new string(charBuffer, 0, len);

                // Append the recently read data to the RequestData stringbuilder
                // object contained in RequestState.
                rs.RequestData.Append(
                   Encoding.ASCII.GetString(rs.BufferRead, 0, read));

                // Continue reading data until 
                // responseStream.EndRead returns –1.
                var ar = responseStream.BeginRead(
                   rs.BufferRead, 0, BufferSize,
                   ReadCallBack, rs);
            }
            else
            {
                if (rs.RequestData.Length > 0)
                {
                    //  Display data to the console.
                    var strContent = rs.RequestData.ToString();
                    var repository = new StockRepository(new UnityContainer());
                    repository.CreateFinanceReport(new StockFileModel()
                    {
                        FileContent = strContent,
                        ImportedDateTime = DateTime.Now,
                        SiteId = rs.SiteId,
                        StockSymbolId = repository.GetSymbolIdBySymbol(rs.StockSymbol)
                    });
                }
                // Close down the response stream.
                responseStream.Close();
                // Set the ManualResetEvent so the main thread can exit.
                AllDone.Set();
            }
        }    
    }
}