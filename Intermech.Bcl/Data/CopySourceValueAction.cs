
// Type: Intermech.Data.CopySourceValueAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data
{
    public sealed class CopySourceValueAction(
      ValueBag source,
      StringKey sourceKey,
      ValueBag target,
      StringKey targetKey) : TransferValueRecordAction(source, sourceKey, target, targetKey)
    {
      public CopySourceValueAction(ValueBag source, ValueBag target, StringKey key)
        : this(source, key, target, key)
      {
      }

      public override void Perform()
      {
        ValueRecord sourceItem = this.Source.Find(this.SourceKey);
        if (sourceItem == null)
          return;
        this.Target.Update(this.TargetKey, this.ScavengeSourceValue(sourceItem));
        this.Target.CopyFlag(this.TargetKey, sourceItem.Flags, NamedFlags.ThrowSetException);
      }
    }
}
