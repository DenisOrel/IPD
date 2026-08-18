
// Type: Intermech.ControlFlow.Cooperative.CooperativeState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.ControlFlow.Cooperative
{
    public abstract class CooperativeState
    {
      internal sealed class Constant : CooperativeState
      {
      }

      internal sealed class Wait : CooperativeState
      {
        public readonly IWaitObject WaitObject;

        public Wait(IWaitObject waitObject)
        {
          this.WaitObject = waitObject != null ? waitObject : throw new ArgumentNullException(nameof (waitObject));
        }
      }

      internal sealed class Call : CooperativeState
      {
        public readonly IEnumerable<CooperativeState> InnerStates;

        public Call(IEnumerable<CooperativeState> innerStates)
        {
          this.InnerStates = innerStates != null ? innerStates : throw new ArgumentNullException(nameof (innerStates));
        }
      }
    }
}
