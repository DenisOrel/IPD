
// Type: Intermech.Data.SectionEntities.SectionMetadata
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.SectionEntities
{
    internal sealed class SectionMetadata
    {
      private readonly Type sectionType;
      private readonly Dictionary<string, SectionProperty> nameLookupTable;
      private readonly List<SectionProperty> sectionProperties;

      public SectionMetadata(Type sectionType, ICollection<SectionProperty> sectionProperties)
      {
        if (sectionType == (Type) null)
          throw new ArgumentNullException(nameof (sectionType));
        if (sectionProperties == null)
          throw new ArgumentNullException(nameof (sectionProperties));
        this.sectionType = sectionType;
        this.sectionProperties = new List<SectionProperty>((IEnumerable<SectionProperty>) sectionProperties);
        this.nameLookupTable = new Dictionary<string, SectionProperty>(sectionProperties.Count);
        foreach (SectionProperty sectionProperty in (IEnumerable<SectionProperty>) sectionProperties)
          this.nameLookupTable.Add(sectionProperty.Descriptor.Name, sectionProperty);
      }

      public Type SectionType => this.sectionType;

      public SectionProperty PropertyByName(string propertyName)
      {
        if (propertyName == null)
          throw new ArgumentNullException(nameof (propertyName));
        SectionProperty sectionProperty;
        this.nameLookupTable.TryGetValue(propertyName, out sectionProperty);
        return sectionProperty;
      }

      public IEnumerable<SectionProperty> EnumProperties()
      {
        return (IEnumerable<SectionProperty>) this.sectionProperties;
      }
    }
}
