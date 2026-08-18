
// Type: Intermech.Data.EntityDb.VirtualPropertyReference
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb
{
    public sealed class VirtualPropertyReference : IEquatable<VirtualPropertyReference>
    {
      private readonly string propertyName;
      private readonly long uniqueId;

      public VirtualPropertyReference(string propertyName)
      {
        this.propertyName = propertyName != null ? propertyName : throw new ArgumentNullException(nameof (propertyName));
        this.uniqueId = (long) RuntimeId.Create();
      }

      public string PropertyName => this.propertyName;

      public long UniqueId => this.uniqueId;

      public bool Equals(VirtualPropertyReference other)
      {
        return other != null && other.uniqueId == this.uniqueId;
      }

      public override bool Equals(object obj)
      {
        return !(obj is VirtualPropertyReference other) ? base.Equals(obj) : this.Equals(other);
      }

      public override int GetHashCode() => this.uniqueId.GetHashCode();

      public override string ToString() => this.propertyName;
    }
}
