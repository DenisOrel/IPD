
// Type: IMClient.AutoUpdater.AutoUpdaterServerConnection




using IPSAutoUpdater.Interfaces;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;


namespace IMClient.AutoUpdater
{
    internal sealed class AutoUpdaterServerConnection
    {
      private object syncRoot;
      private string serverUrl;
      private bool isConnected;
      private AutoUpdaterServerProxy serverProxy;

      public AutoUpdaterServerConnection(string serverObjectUrl)
      {
        if (string.IsNullOrEmpty(serverObjectUrl))
          throw new ArgumentException("Адрес объекта службы не должен быть пуст или равен null.", nameof (serverObjectUrl));
        this.syncRoot = new object();
        this.serverUrl = serverObjectUrl;
      }

      public bool IsConnected
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.isConnected;
        }
      }

      public AutoUpdaterServerProxy ServerObject
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
          {
            this.ConnectLazily();
            return this.serverProxy;
          }
        }
      }

      public bool TestConnection()
      {
        lock (this.syncRoot)
          return this.isConnected && this.TestConnectionCore(this.serverProxy.RawServerObject);
      }

      private bool TestConnectionCore(IAutoUpdaterServer serverObject)
      {
        try
        {
          return serverObject.ID != Guid.Empty;
        }
        catch
        {
          return false;
        }
      }

      public void ValidateConnection()
      {
        lock (this.syncRoot)
        {
          if (this.isConnected && !this.TestConnection())
            this.ResetConnectionCore();
          this.ConnectLazily();
        }
      }

      private void ConnectLazily()
      {
        if (this.isConnected)
          return;
        this.ConnectCore();
      }

      private void ConnectCore()
      {
        this.serverProxy = new AutoUpdaterServerProxy(this.TryConnectServerObject() ?? throw new Exception("Служба обновления ПО ИНТЕРМЕХ не отвечает."));
        this.isConnected = true;
        this.RaiseConnected();
      }

      private IAutoUpdaterServer TryConnectServerObject()
      {
        IAutoUpdaterServer serverObject = (IAutoUpdaterServer) RemotingServices.Connect(typeof (IAutoUpdaterServer), this.serverUrl);
        return this.TestConnectionCore(serverObject) ? serverObject : (IAutoUpdaterServer) null;
      }

      private void ResetConnectionCore()
      {
        this.serverProxy = (AutoUpdaterServerProxy) null;
        this.isConnected = false;
        this.RaiseConnectionLost();
      }

      private void RaiseConnected()
      {
        EventHandler connected = this.Connected;
        if (connected == null)
          return;
        connected((object) this, EventArgs.Empty);
      }

      private void RaiseConnectionLost()
      {
        EventHandler connectionLost = this.ConnectionLost;
        if (connectionLost == null)
          return;
        connectionLost((object) this, EventArgs.Empty);
      }

      public event EventHandler Connected;

      public event EventHandler ConnectionLost;
    }
}
