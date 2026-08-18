
// Type: Intermech.Remoting.Optimized.BaseTransportHeaders
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Net;
using System.Runtime.Remoting.Channels;


namespace Intermech.Remoting.Optimized
{
    [Serializable]
    internal class BaseTransportHeaders : ITransportHeaders
    {
      internal const int WellknownHeaderCount = 4;
      private object _connectionId;
      private object _ipAddress;
      private string _requestUri;
      private string _contentType;
      private ITransportHeaders _otherHeaders;

      public string RequestUri
      {
        get => this._requestUri;
        set => this._requestUri = value;
      }

      public string ContentType
      {
        get => this._contentType;
        set => this._contentType = value;
      }

      public object ConnectionId
      {
        set => this._connectionId = value;
      }

      public IPAddress IPAddress
      {
        set => this._ipAddress = (object) value;
      }

      public object this[object key]
      {
        get
        {
          if (key is string headerName)
          {
            int index = this.MapHeaderNameToIndex(headerName);
            if (index != -1)
              return this.GetValueFromHeaderIndex(index);
          }
          return this._otherHeaders != null ? this._otherHeaders[key] : (object) null;
        }
        set
        {
          bool flag = false;
          if (key is string headerName)
          {
            int index = this.MapHeaderNameToIndex(headerName);
            if (index != -1)
            {
              this.SetValueFromHeaderIndex(index, value);
              flag = true;
            }
          }
          if (flag)
            return;
          if (this._otherHeaders == null)
            this._otherHeaders = (ITransportHeaders) new TransportHeaders();
          this._otherHeaders[key] = value;
        }
      }

      public BaseTransportHeaders() => this._otherHeaders = (ITransportHeaders) null;

      public IEnumerator GetEnumerator() => (IEnumerator) new BaseTransportHeadersEnumerator(this);

      internal IEnumerator GetOtherHeadersEnumerator()
      {
        return this._otherHeaders == null ? (IEnumerator) null : this._otherHeaders.GetEnumerator();
      }

      internal int MapHeaderNameToIndex(string headerName)
      {
        if (string.Compare(headerName, "__ConnectionId", StringComparison.OrdinalIgnoreCase) == 0)
          return 0;
        if (string.Compare(headerName, "__IPAddress", StringComparison.OrdinalIgnoreCase) == 0)
          return 1;
        if (string.Compare(headerName, "__RequestUri", StringComparison.OrdinalIgnoreCase) == 0)
          return 2;
        return string.Compare(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase) == 0 ? 3 : -1;
      }

      internal string MapHeaderIndexToName(int index)
      {
        switch (index)
        {
          case 0:
            return "__ConnectionId";
          case 1:
            return "__IPAddress";
          case 2:
            return "__RequestUri";
          case 3:
            return "Content-Type";
          default:
            return (string) null;
        }
      }

      internal object GetValueFromHeaderIndex(int index)
      {
        switch (index)
        {
          case 0:
            return this._connectionId;
          case 1:
            return this._ipAddress;
          case 2:
            return (object) this._requestUri;
          case 3:
            return (object) this._contentType;
          default:
            return (object) null;
        }
      }

      internal void SetValueFromHeaderIndex(int index, object value)
      {
        switch (index)
        {
          case 0:
            this._connectionId = value;
            break;
          case 1:
            this._ipAddress = value;
            break;
          case 2:
            this._requestUri = (string) value;
            break;
          case 3:
            this._contentType = (string) value;
            break;
        }
      }
    }
}
