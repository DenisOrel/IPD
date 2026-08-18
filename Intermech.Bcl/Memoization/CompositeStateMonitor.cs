
// Type: Intermech.Memoization.CompositeStateMonitor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Memoization
{
    public sealed class CompositeStateMonitor : IStateMonitor
    {
      private readonly List<IStateMonitor> monitors;

      public CompositeStateMonitor(ICollection<IStateMonitor> monitors)
      {
        if (monitors == null)
          throw new ArgumentNullException(nameof (monitors));
        this.monitors = monitors.Count != 0 ? new List<IStateMonitor>((IEnumerable<IStateMonitor>) monitors) : throw new ArgumentOutOfRangeException();
      }

      public CompositeStateMonitor(params IStateMonitor[] monitors)
        : this((ICollection<IStateMonitor>) monitors)
      {
      }

      public bool AnyWritersSince(object seqNum)
      {
        if (seqNum == null)
          return true;
        object[] objArray = (object[]) seqNum;
        for (int index = 0; index < this.monitors.Count; ++index)
        {
          if (this.monitors[index].AnyWritersSince(objArray[index]))
            return true;
        }
        return false;
      }

      public object WriterSeqNum
      {
        get
        {
          object[] writerSeqNum = new object[this.monitors.Count];
          for (int index = 0; index < this.monitors.Count; ++index)
            writerSeqNum[index] = this.monitors[index].WriterSeqNum;
          return (object) writerSeqNum;
        }
      }
    }
}
