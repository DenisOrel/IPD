
// Type: Intermech.UI.NullMasterSlaveProgressSink
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Индикатор-заглушка, используемый в тех случаях, когда отображения хода выполнения процесса не требуется.
    /// </summary>
    internal sealed class NullMasterSlaveProgressSink : IMasterSlaveProgressSink
    {
      private static readonly NullMasterSlaveProgressSink defaultInstance = new NullMasterSlaveProgressSink();
      private IPercentageProgressSink nullSink;

      public NullMasterSlaveProgressSink()
      {
        this.nullSink = (IPercentageProgressSink) NullPercentageProgressSink.Default;
      }

      public IPercentageProgressSink MasterSink
      {
        [DebuggerStepThrough] get => this.nullSink;
      }

      public IPercentageProgressSink CreateSlaveSink() => this.nullSink;

      /// <summary>
      /// Возвращает экземпляр индикатора, который может использоваться по умолчанию.
      /// </summary>
      public static NullMasterSlaveProgressSink Default
      {
        [DebuggerStepThrough] get => NullMasterSlaveProgressSink.defaultInstance;
      }
    }
}
