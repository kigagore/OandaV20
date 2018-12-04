using Newtonsoft.Json;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Stream;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
    public abstract class StreamSession<T> where T : IHeartbeat
    {
        protected readonly string AccountId;
        protected WebResponse Response;

        private CancellationTokenSource _cancellationTokenSource;

        public delegate void DataHandler(T data);
        public event DataHandler DataReceived;
        public void OnDataReceived(T data)
        {
            DataReceived?.Invoke(data);
        }
        public delegate void SessionStatusHandler(string accountId, bool started, Exception e);
        public event SessionStatusHandler SessionStatusChanged;
        public void OnSessionStatusChanged(bool started, Exception e)
        {
            SessionStatusChanged?.Invoke(AccountId, started, e);
        }

        protected StreamSession(string accountId)
        {
            AccountId = accountId;
        }

        protected abstract Task<WebResponse> GetSession();

        public virtual async Task StartSession()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var ct = _cancellationTokenSource.Token;
            Response = await GetSession();

            await Task.Run(() =>
            {
                try
                {
                    ct.ThrowIfCancellationRequested();
                    using (Response)
                    {
                        var stream = Response.GetResponseStream();
                        if(stream == null) return;
                        StreamReader reader = new StreamReader(stream);
                        while (!ct.IsCancellationRequested)
                        {
                            string line = reader.ReadLine();
                            var data = JsonConvert.DeserializeObject<T>(line);

                            OnSessionStatusChanged(true, null);

                            OnDataReceived(data);
                        }
                    }
                }
                finally
                {
                    Response = null;
                }
            }, ct);
        }

        public void StopSession()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
