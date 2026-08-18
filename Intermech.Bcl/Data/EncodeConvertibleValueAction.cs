
// Type: Intermech.Data.EncodeConvertibleValueAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data
{
    public class EncodeConvertibleValueAction : TransferValueRecordAction
    {
      public EncodeConvertibleValueAction(
        ValueBag source,
        StringKey sourceKey,
        ValueBag target,
        StringKey targetKey)
        : base(source, sourceKey, target, targetKey)
      {
        this.IsOpenMetadataTarget = false;
        this.OptimizeEmptyValues = true;
      }

      public bool IsOpenMetadataTarget { get; set; }

      /// <summary>
      /// Возвращает или задает режим оптимизации записи пустых значений.
      /// Если на принимающей стороне нет одноименного параметра, то пустое значение не записывается,
      /// так как считается, что отсутствующее значение эквивалентно пустому.
      /// </summary>
      public bool OptimizeEmptyValues { get; set; }

      public override void Perform()
      {
        ValueRecord sourceItem = this.Source.Find(this.SourceKey);
        if (sourceItem == null)
          return;
        ValueRecord targetItem = this.Target.Find(this.TargetKey);
        this.EncodeValue(sourceItem, targetItem);
      }

      private void EncodeValue(ValueRecord sourceItem, ValueRecord targetItem)
      {
        Tuple<Type, object> tuple;
        try
        {
          tuple = this.EmitTargetValue(sourceItem, targetItem);
        }
        catch (InvalidCastException ex)
        {
          throw new CantUpdateAttributeValueException(sourceItem, (Exception) ex);
        }
        catch (FormatException ex)
        {
          throw new CantUpdateAttributeValueException(sourceItem, (Exception) ex);
        }
        if (targetItem == null)
        {
          if (this.OptimizeEmptyValues && !this.IsSignificantValue(tuple.Item1, tuple.Item2))
            return;
        }
        else if (this.SkipWriteEqualValue(targetItem, tuple.Item2))
          return;
        if (!this.Target.CanUpdate(this.TargetKey, tuple.Item1, this.IsOpenMetadataTarget))
          throw new CantUpdateAttributeValueException(sourceItem);
        this.Target.Update(this.TargetKey, tuple.Item2, this.IsOpenMetadataTarget);
        this.Target.CopyFlag(this.TargetKey, sourceItem.Flags, NamedFlags.ThrowSetException);
      }

      private Tuple<Type, object> EmitTargetValue(ValueRecord sourceItem, ValueRecord targetItem)
      {
        if (sourceItem.IsNull)
          return Tuple.Create(typeof (string), (object) string.Empty);
        Type conversionType = targetItem != null ? targetItem.DataType : typeof (string);
        object obj = sourceItem.DataType == conversionType ? sourceItem.Value : Convert.ChangeType(sourceItem.Value, conversionType);
        return Tuple.Create(conversionType, obj);
      }

      private bool IsSignificantValue(Type dataType, object value)
      {
        return value != null && (!(dataType == typeof (string)) || !object.Equals(value, (object) string.Empty));
      }

      private bool SkipWriteEqualValue(ValueRecord targetItem, object newValue)
      {
        return targetItem.DataType == typeof (string) && (targetItem.IsNull || object.Equals(targetItem.Value, (object) string.Empty)) && (newValue == null || object.Equals(newValue, (object) string.Empty));
      }
    }
}
