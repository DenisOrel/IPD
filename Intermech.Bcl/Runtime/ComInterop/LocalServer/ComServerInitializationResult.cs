
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerInitializationResult
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    public sealed class ComServerInitializationResult
    {
      internal ComServerInitializationResult(Exception exception = null, bool exitRequested = false)
      {
        this.IsSuccessful = exception == null;
        this.Exception = exception;
        this.ExitRequested = exitRequested;
      }

      public bool IsSuccessful { get; private set; }

      public Exception Exception { get; private set; }

      public bool ExitRequested { get; private set; }
    }
}
