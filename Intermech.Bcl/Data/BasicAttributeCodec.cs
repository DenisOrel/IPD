
// Type: Intermech.Data.BasicAttributeCodec
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using System;
using System.Collections.Generic;


namespace Intermech.Data
{
    public class BasicAttributeCodec : IAttributeCodec
    {
      private readonly IValueBagFormatter formatter;
      private Dictionary<StringKey, Tuple<bool, IAttributeLayout>> attributeMap;

      public BasicAttributeCodec(IValueBagFormatter formatter)
      {
        this.formatter = formatter != null ? formatter : throw new ArgumentNullException(nameof (formatter));
        this.attributeMap = new Dictionary<StringKey, Tuple<bool, IAttributeLayout>>();
      }

      public bool IsAttributeSupported(StringKey attributeKey)
      {
        if (attributeKey == (StringKey) null)
          throw new ArgumentNullException(nameof (attributeKey));
        return this.GetAttributeEntry(attributeKey) != null;
      }

      public ICollection<StringKey> GetContainerValueKeys(ICollection<StringKey> attributeKeys)
      {
        if (attributeKeys == null)
          throw new ArgumentNullException(nameof (attributeKeys));
        OrderedList<StringKey> collection = new OrderedList<StringKey>(attributeKeys.Count * 2, (IComparer<StringKey>) Comparer<StringKey>.Default);
        foreach (StringKey attributeKey in (IEnumerable<StringKey>) attributeKeys)
          collection.AddRange<StringKey>((IEnumerable<StringKey>) (this.GetAttributeEntry(attributeKey) ?? throw new NotSupportedException("Attribute is not supported.")).Item2.ContainerKeys);
        return (ICollection<StringKey>) collection;
      }

      public ICollection<StringKey> GetContainerValueKeys(StringKey attributeKey)
      {
        return (ICollection<StringKey>) new OrderedList<StringKey>((IEnumerable<StringKey>) ((!(attributeKey == (StringKey) null) ? this.GetAttributeEntry(attributeKey) : throw new ArgumentNullException(nameof (attributeKey))) ?? throw new NotSupportedException("Attribute is not supported.")).Item2.ContainerKeys, (IComparer<StringKey>) Comparer<StringKey>.Default);
      }

      public ValueBag Decode(DecodeAttributesParams decodeParams)
      {
        if (decodeParams == null)
          throw new ArgumentNullException(nameof (decodeParams));
        ValueBag attributes = new ValueBag();
        ICollection<StringKey> stringKeys = (ICollection<StringKey>) this.SelectSupportedKeys(decodeParams.AttributeKeys);
        if (stringKeys.Count > 0)
        {
          foreach (StringKey attributeKey in (IEnumerable<StringKey>) stringKeys)
            this.EmitDecodeAction(decodeParams.Container, attributeKey, decodeParams.ContainerValues, attributes, decodeParams.Options).Perform();
          attributes.AcceptChanges();
        }
        return attributes;
      }

      public void Encode(EncodeAttributesParams encodeParams)
      {
        ICollection<StringKey> stringKeys = encodeParams != null ? (ICollection<StringKey>) this.SelectSupportedKeys(encodeParams.AttributeKeys) : throw new ArgumentNullException(nameof (encodeParams));
        if (stringKeys.Count == 0)
          return;
        ContainerValues containerValues = encodeParams.ContainerValues.Clone();
        string containerDisplayName = encodeParams.ContainerDisplayName;
        if (string.IsNullOrEmpty(containerDisplayName))
          containerDisplayName = encodeParams.Container.ToString();
        foreach (StringKey attributeKey in (IEnumerable<StringKey>) stringKeys)
          new EncodeAttributeErrorHandler(this.EmitEncodeAction(encodeParams.Container, attributeKey, encodeParams.Attributes, containerValues, encodeParams.Options), containerDisplayName, encodeParams.Options.ReportErrorsOnly).Perform();
        this.TransferChanges(containerValues, encodeParams.ContainerValues);
      }

      private void TransferChanges(ContainerValues containerCopy, ContainerValues containerValues)
      {
        foreach (ValueRecord changedItem in containerCopy.Bag.GetChangedItems())
        {
          ValueRecord valueRecord = containerValues.Bag.Find(changedItem.Key);
          if (valueRecord == null)
          {
            if (!containerValues.IsOpenMetadata)
              throw new NotSupportedException();
            containerValues.Bag.Add(changedItem.Clone());
          }
          else if ((!changedItem.IsNull || !valueRecord.IsNull) && !object.Equals(changedItem.Value, valueRecord.Value))
          {
            valueRecord.Value = changedItem.Value;
            valueRecord.Flags.CopyAll(changedItem.Flags);
          }
        }
      }

      /// <summary>
      /// Определяет правила декодирования значений атрибутов объекта из контейнера значений, прочитанного из файла документа.
      /// </summary>
      /// <param name="container">Сервисный объект, предоставляющий доступ к файлу документа</param>
      /// <param name="attributeKey">Имя атрибута, который требуется декодировать</param>
      /// <param name="containerValues">Закодированные значения, прочитанные из файла документа</param>
      /// <param name="attributes">Декодированные атрибуты объекта</param>
      /// <param name="options">Опции декодирования</param>
      /// <returns>Объект, реализующий декодирование</returns>
      protected virtual IAction EmitDecodeAction(
        IValueBagContainer container,
        StringKey attributeKey,
        ContainerValues containerValues,
        ValueBag attributes,
        DecodeAttributesOptions options)
      {
        return (IAction) new CopySourceValueAction(containerValues.Bag, CollectionUtils.GetFirstItem((IEnumerable<StringKey>) (this.GetAttributeEntry(attributeKey) ?? throw new NotSupportedException("Attribute is not supported.")).Item2.ContainerKeys), attributes, attributeKey);
      }

      /// <summary>
      /// Определяет правила кодирования значений атрибутов объекта в контейнер значений, предназначенный для записи в файл документа.
      /// </summary>
      /// <param name="container">Сервисный объект, предоставляющий доступ к файлу документа</param>
      /// <param name="attributeKey">Имя атрибута, который требуется закодировать</param>
      /// <param name="attributes">Атрибуты объекта</param>
      /// <param name="containerValues">Закодированные значения, которые будут записаны в файл документа</param>
      /// <param name="options">Опции кодирования</param>
      /// <returns>Объект, реализующий кодирование</returns>
      protected virtual IAction EmitEncodeAction(
        IValueBagContainer container,
        StringKey attributeKey,
        ValueBag attributes,
        ContainerValues containerValues,
        EncodeAttributesOptions options)
      {
        return (IAction) new EncodeConvertibleValueAction(attributes, attributeKey, containerValues.Bag, CollectionUtils.GetFirstItem((IEnumerable<StringKey>) (this.GetAttributeEntry(attributeKey) ?? throw new NotSupportedException("Attribute is not supported.")).Item2.ContainerKeys))
        {
          IsOpenMetadataTarget = containerValues.IsOpenMetadata,
          OptimizeEmptyValues = options.OptimizeEmptyValues
        };
      }

      public ContainerValues ReadFileProperties(
        IValueBagContainer container,
        ICollection<StringKey> attributeKeys)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (attributeKeys == null)
          throw new ArgumentNullException(nameof (attributeKeys));
        return this.Formatter.Read(container, this.GetContainerValueKeys((ICollection<StringKey>) this.SelectSupportedKeys(attributeKeys)));
      }

      public ContainerValues ReadAttributes(
        IValueBagContainer container,
        ICollection<StringKey> attributeKeys,
        DecodeAttributesOptions options)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (attributeKeys == null)
          throw new ArgumentNullException(nameof (attributeKeys));
        if (options == null)
          throw new ArgumentNullException(nameof (options));
        List<StringKey> attributeKeys1 = this.SelectSupportedKeys(attributeKeys);
        ContainerValues containerValues = this.Formatter.Read(container, this.GetContainerValueKeys((ICollection<StringKey>) attributeKeys1));
        if (attributeKeys1.Count > 0)
          containerValues = new ContainerValues(this.Decode(new DecodeAttributesParams(container, (ICollection<StringKey>) attributeKeys1, containerValues, options)), containerValues.IsOpenMetadata);
        return containerValues;
      }

      public IValueBagFormatter Formatter => this.formatter;

      private List<StringKey> SelectSupportedKeys(ICollection<StringKey> attributeKeys)
      {
        return CollectionUtils.FindAllAsList(attributeKeys, new Predicate<StringKey>(this.IsAttributeSupported));
      }

      private Tuple<bool, IAttributeLayout> GetAttributeEntry(StringKey attributeKey)
      {
        if (attributeKey == (StringKey) null)
          throw new ArgumentNullException(nameof (attributeKey));
        Tuple<bool, IAttributeLayout> attributeEntry;
        lock (this.attributeMap)
        {
          if (!this.attributeMap.TryGetValue(attributeKey, out attributeEntry))
          {
            attributeEntry = this.CreateAttributeEntry(attributeKey);
            this.attributeMap.Add(attributeKey, attributeEntry);
          }
        }
        return attributeEntry;
      }

      private Tuple<bool, IAttributeLayout> CreateAttributeEntry(StringKey attributeKey)
      {
        IAttributeLayout attributeLayout1 = this.GetContainerAttributeLayout(attributeKey);
        if (attributeLayout1 == null)
          return (Tuple<bool, IAttributeLayout>) null;
        if (!this.Formatter.IsValueSupported(CollectionUtils.GetFirstItem((IEnumerable<StringKey>) attributeLayout1.ContainerKeys)))
          return (Tuple<bool, IAttributeLayout>) null;
        int num = attributeLayout1.ContainerKeys.Count > 1 ? 1 : 0;
        List<StringKey> allAsList = CollectionUtils.FindAllAsList(attributeLayout1.ContainerKeys, new Predicate<StringKey>(this.Formatter.IsValueSupported));
        if (allAsList.Count != attributeLayout1.ContainerKeys.Count)
          attributeLayout1 = (IAttributeLayout) new BasicAttributeLayout(attributeKey, (ICollection<StringKey>) allAsList);
        IAttributeLayout attributeLayout2 = attributeLayout1;
        return Tuple.Create(num != 0, attributeLayout2);
      }

      protected virtual IAttributeLayout GetContainerAttributeLayout(StringKey attributeKey)
      {
        StringKey containerKey = !(attributeKey == (StringKey) null) ? this.GetContainerValueKey(attributeKey) : throw new ArgumentNullException(nameof (attributeKey));
        return containerKey == (StringKey) null ? (IAttributeLayout) null : (IAttributeLayout) new BasicAttributeLayout(attributeKey, containerKey);
      }

      protected virtual StringKey GetContainerValueKey(StringKey attributeKey)
      {
        return !(attributeKey == (StringKey) null) ? attributeKey : throw new ArgumentNullException(nameof (attributeKey));
      }
    }
}
