
// Type: Intermech.Data.SectionEntities.SectionPropertyReference
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Data.EntityDb;
using System;
using System.ComponentModel;
using System.Diagnostics;


namespace Intermech.Data.SectionEntities
{
    public sealed class SectionPropertyReference : IEquatable<SectionPropertyReference>
    {
      private readonly Type sectionType;
      private readonly string propertyName;

      public SectionPropertyReference(Type sectionType, string propertyName)
      {
        if (sectionType == (Type) null)
          throw new ArgumentNullException(nameof (sectionType));
        if (propertyName == null)
          throw new ArgumentNullException(nameof (propertyName));
        this.sectionType = sectionType;
        this.propertyName = propertyName;
      }

      [Conditional("DEBUG")]
      private static void CheckPropertyExists(Type sectionType, string propertyName)
      {
        if (TypeDescriptor.GetProperties(sectionType)[propertyName] == null)
          throw new EntityDatabaseException($"No property '{propertyName}' found in type '{sectionType.FullName}'.");
      }

      public Type SectionType => this.sectionType;

      public string PropertyName => this.propertyName;

      public bool Equals(SectionPropertyReference other)
      {
        return other != null && other.sectionType == this.sectionType && other.propertyName == this.propertyName;
      }

      public override bool Equals(object obj)
      {
        return !(obj is SectionPropertyReference other) ? base.Equals(obj) : this.Equals(other);
      }

      public override int GetHashCode()
      {
        return this.sectionType.GetHashCode() ^ this.propertyName.GetHashCode();
      }

      public override string ToString() => string.Format(this.propertyName);
    }
}
