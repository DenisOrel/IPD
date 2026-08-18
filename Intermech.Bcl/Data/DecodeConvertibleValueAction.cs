
// Type: Intermech.Data.DecodeConvertibleValueAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data
{
    public sealed class DecodeConvertibleValueAction : TransferValueRecordAction
    {
      private readonly Type targetDataType;

      public DecodeConvertibleValueAction(
        ValueBag source,
        StringKey sourceKey,
        ValueBag target,
        StringKey targetKey,
        Type targetDataType)
        : base(source, sourceKey, target, targetKey)
      {
        this.targetDataType = !(targetDataType == (Type) null) ? targetDataType : throw new ArgumentNullException(nameof (targetDataType));
      }

      public DecodeConvertibleValueAction(
        ValueBag source,
        ValueBag target,
        StringKey key,
        Type dataType)
        : this(source, key, target, key, dataType)
      {
      }

      public override void Perform()
      {
        ValueRecord sourceItem = this.Source.Find(this.SourceKey);
        if (sourceItem == null)
          return;
        if (sourceItem.IsNull)
          this.ReportEmptyValuedItem(sourceItem);
        else
          this.DecodeValue(sourceItem);
      }

      private void DecodeValue(ValueRecord sourceItem)
      {
        object obj = this.ScavengeSourceValue(sourceItem);
        try
        {
          this.Target.Update(this.TargetKey, ValueRecord.IsNullValue(obj) ? (object) TypedNull.Instance(this.targetDataType) : Convert.ChangeType(obj, this.targetDataType));
          this.Target.CopyFlag(this.TargetKey, sourceItem.Flags, NamedFlags.ThrowSetException);
        }
        catch (InvalidCastException ex)
        {
          this.ReportBadTypedItem(sourceItem);
        }
        catch (FormatException ex)
        {
          this.ReportBadValuedItem(sourceItem, (Exception) ex);
        }
      }
    }
}
