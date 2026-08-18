
// Type: Intermech.ControlFlow.MethodAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ControlFlow
{
    public sealed class MethodAction : IAction
    {
      private readonly Action method;

      public MethodAction(Action method)
      {
        this.method = method != null ? method : throw new ArgumentNullException(nameof (method));
      }

      public void Perform() => this.method();
    }
}
