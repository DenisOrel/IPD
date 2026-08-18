
// Type: Intermech.Data.IAttributeCodec
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data
{
    public interface IAttributeCodec
    {
      IValueBagFormatter Formatter { get; }

      bool IsAttributeSupported(StringKey attributeKey);

      ICollection<StringKey> GetContainerValueKeys(ICollection<StringKey> attributeKeys);

      ICollection<StringKey> GetContainerValueKeys(StringKey attributeKey);

      ValueBag Decode(DecodeAttributesParams decodeParams);

      void Encode(EncodeAttributesParams encodeParams);

      ContainerValues ReadFileProperties(
        IValueBagContainer container,
        ICollection<StringKey> attributeKeys);

      ContainerValues ReadAttributes(
        IValueBagContainer container,
        ICollection<StringKey> attributeKeys,
        DecodeAttributesOptions options);
    }
}
