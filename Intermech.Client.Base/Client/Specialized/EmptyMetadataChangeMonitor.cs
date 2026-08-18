
// Type: Intermech.Client.Specialized.EmptyMetadataChangeMonitor
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Interfaces.Data.Metadata;
using Intermech.Memoization;


namespace Intermech.Client.Specialized
{
    internal sealed class EmptyMetadataChangeMonitor : IMetadataChangeMonitor, IStateMonitor
    {
      private static readonly object zeroSeqNum = (object) 0;

      public object WriterSeqNum => EmptyMetadataChangeMonitor.zeroSeqNum;

      public bool AnyWritersSince(object seqNum) => seqNum == null;
    }
}
