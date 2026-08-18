
// Type: Intermech.Runtime.ComInterop.LocalServer.IReferenceCounter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal interface IReferenceCounter
    {
      void Increment();

      void Decrement();

      void Decrement(int value);
    }
}
