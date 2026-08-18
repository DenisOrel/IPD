
// Type: Intermech.Data.BasicValueBagFormatter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;


namespace Intermech.Data
{
    public abstract class BasicValueBagFormatter : IValueBagFormatter
    {
      public abstract bool IsOpenMetadata { get; }

      public abstract bool IsContainerSupported(IValueBagContainer container);

      public abstract bool IsValueSupported(StringKey valueKey);

      public ContainerValues Read(IValueBagContainer container, ICollection<StringKey> valueKeys)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (valueKeys == null)
          throw new ArgumentNullException(nameof (valueKeys));
        this.ValidateContainer(container);
        List<StringKey> valueKeys1 = this.SelectSupportedKeys(valueKeys);
        ValueBag bag;
        if (valueKeys1.Count > 0)
        {
          bag = this.DoRead(container, (ICollection<StringKey>) valueKeys1);
          bag.AcceptChanges();
        }
        else
          bag = new ValueBag();
        return new ContainerValues(bag, this.IsOpenMetadata);
      }

      public bool Write(IValueBagContainer container, ContainerValues values)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (values == null)
          throw new ArgumentNullException(nameof (values));
        this.ValidateContainer(container);
        ICollection<StringKey> changedValues = (ICollection<StringKey>) this.SelectSupportedKeys((ICollection<StringKey>) values.Bag.GetChangedItemsKeys());
        if (changedValues.Count == 0)
          return false;
        this.DoWrite(container, values, changedValues);
        return true;
      }

      private List<StringKey> SelectSupportedKeys(ICollection<StringKey> valueKeys)
      {
        return CollectionUtils.FindAllAsList(valueKeys, new Predicate<StringKey>(this.IsValueSupported));
      }

      private void ValidateContainer(IValueBagContainer container)
      {
        if (!this.IsContainerSupported(container))
          throw new NotSupportedException("Unsupported container type.");
      }

      protected abstract ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys);

      protected abstract void DoWrite(
        IValueBagContainer container,
        ContainerValues values,
        ICollection<StringKey> changedValues);
    }
}
