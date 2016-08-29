using System;
using System.ComponentModel;
using System.ServiceModel;

namespace Resmed.Stock.ClientService.Common
{
    public sealed class WcfClientProxy<TClient, TChannel> : IDisposable
        where TClient : ClientBase<TChannel>, new()
        where TChannel : class
    {
        /// <summary>
        /// The current client in scope.
        /// </summary>
        private TClient _currentClient;

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_currentClient != null)
            {
                if (_currentClient.State != CommunicationState.Closed &&
                    _currentClient.State != CommunicationState.Faulted)
                {
                    _currentClient.Close();
                }

                _currentClient = null;
            }
        }

        #endregion

        private TClient GetClient()
        {
            return _currentClient ?? (_currentClient = new TClient());
        }

        private TClient GetClient(String Uri)
        {
            _currentClient = _currentClient ?? (_currentClient = new TClient());
            _currentClient.Endpoint.Address = new EndpointAddress(Uri);
            return _currentClient;
        }

        public TResult MakeCall<TResult>(Func<TClient, TResult> remoteCall,String Uri)
        {
            try
            {
                TClient client = GetClient(Uri);
                return remoteCall(client);
            }
            catch (Exception)
            {
                // Invalidate the current client object
                Dispose();
                throw;
            }
        }

        public TResult MakeCall<TResult>(Func<TClient, TResult> remoteCall)
        {
            try
            {
                TClient client = GetClient();
                return remoteCall(client);
            }
            catch (Exception)
            {
                // Invalidate the current client object
                Dispose();
                throw;
            }
        }

        public void MakeCall(Action<TClient> remoteCall)
        {
            try
            {
                TClient client = GetClient();
                remoteCall(client);
            }
            catch (Exception)
            {   
                Dispose();
                throw;
            }
        }

        public void MakeCall(Action<TClient> remoteCall,String Uri)
        {
            try
            {
                TClient client = GetClient(Uri);
                remoteCall(client);
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public void MakeCallAsync(Action<TClient> remoteCall)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += BgwDoWork;
            bgw.RunWorkerAsync(remoteCall);
        }

        private void BgwDoWork(object sender, DoWorkEventArgs e)
        {
            var action = e.Argument as Action<TClient>;
            if (action != null)
            {
                action(GetClient());
            }
        }
    }
}