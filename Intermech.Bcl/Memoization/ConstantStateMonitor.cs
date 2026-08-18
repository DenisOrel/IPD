
// Type: Intermech.Memoization.ConstantStateMonitor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Memoization
{
    public sealed class ConstantStateMonitor : IStateMonitor
    {
      public static readonly ConstantStateMonitor Value = new ConstantStateMonitor();

      public bool AnyWritersSince(object seqNum) => seqNum == null;

      public object WriterSeqNum => (object) this;
    }
}
