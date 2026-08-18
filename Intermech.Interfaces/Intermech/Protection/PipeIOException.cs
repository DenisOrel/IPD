
// Type: Intermech.Protection.PipeIOException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Protection
{
    internal class PipeIOException(string message) : Exception(message)
    {
      private int _code;

      public PipeIOException(string message, int code)
        : this(message)
      {
        this._code = code;
      }

      public int Code => this._code;
    }
}
