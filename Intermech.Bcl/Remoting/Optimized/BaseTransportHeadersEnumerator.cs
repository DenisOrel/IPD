
// Type: Intermech.Remoting.Optimized.BaseTransportHeadersEnumerator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections;


namespace Intermech.Remoting.Optimized
{
    internal class BaseTransportHeadersEnumerator : IEnumerator
    {
      private BaseTransportHeaders _headers;
      private bool _bStarted;
      private int _currentIndex;
      private IEnumerator _otherHeadersEnumerator;

      public object Current
      {
        get
        {
          if (!this._bStarted)
            return (object) null;
          if (this._currentIndex != -1)
            return (object) new DictionaryEntry((object) this._headers.MapHeaderIndexToName(this._currentIndex), this._headers.GetValueFromHeaderIndex(this._currentIndex));
          return this._otherHeadersEnumerator != null ? this._otherHeadersEnumerator.Current : (object) null;
        }
      }

      public BaseTransportHeadersEnumerator(BaseTransportHeaders headers)
      {
        this._headers = headers;
        this.Reset();
      }

      public bool MoveNext()
      {
        if (this._currentIndex != -1)
        {
          if (this._bStarted)
            ++this._currentIndex;
          else
            this._bStarted = true;
          while (this._currentIndex != -1)
          {
            if (this._currentIndex >= 4)
            {
              this._otherHeadersEnumerator = this._headers.GetOtherHeadersEnumerator();
              this._currentIndex = -1;
            }
            else
            {
              if (this._headers.GetValueFromHeaderIndex(this._currentIndex) != null)
                return true;
              ++this._currentIndex;
            }
          }
        }
        if (this._otherHeadersEnumerator == null)
          return false;
        if (this._otherHeadersEnumerator.MoveNext())
          return true;
        this._otherHeadersEnumerator = (IEnumerator) null;
        return false;
      }

      public void Reset()
      {
        this._bStarted = false;
        this._currentIndex = 0;
        this._otherHeadersEnumerator = (IEnumerator) null;
      }
    }
}
