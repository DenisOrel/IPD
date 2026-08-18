
// Type: Intermech.UI.MasterSlaveProgressSinkAdapter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.UI
{
    internal sealed class MasterSlaveProgressSinkAdapter : IMasterSlaveProgressSink
    {
      private IPercentageProgressSink masterProgressSink;
      private IPercentageProgressSink nullSink;

      public MasterSlaveProgressSinkAdapter(IPercentageProgressSink masterProgressSink)
      {
        this.masterProgressSink = masterProgressSink != null ? masterProgressSink : throw new ArgumentNullException(nameof (masterProgressSink));
        this.nullSink = (IPercentageProgressSink) NullPercentageProgressSink.Default;
      }

      public IPercentageProgressSink MasterSink
      {
        [DebuggerStepThrough] get => this.masterProgressSink;
      }

      public IPercentageProgressSink CreateSlaveSink() => this.nullSink;
    }
}
