
// Type: Intermech.Data.EntityDb.Common.PropertyCompareInfo`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class PropertyCompareInfo<T> : IPropertyCompareFactory<T>, IPropertyCompareInfo
    {
      private readonly PropertyDescriptor pd;
      private readonly IndexType indexType;
      private bool customComparer;
      private PropertyCompareKind kind;

      public PropertyCompareInfo(PropertyDescriptor pd, IndexType indexType)
      {
        this.pd = pd != null ? pd : throw new ArgumentNullException(nameof (pd));
        this.indexType = indexType;
        this.DetectCompareKind();
      }

      private void DetectCompareKind()
      {
        ComparerAttribute attribute = (ComparerAttribute) this.pd.Attributes[typeof (ComparerAttribute)];
        if (attribute != null)
        {
          Type objectType = attribute.Creator.GetObjectType();
          this.customComparer = true;
          if ((this.indexType == IndexType.Auto || this.indexType == IndexType.Ordered) && typeof (IComparer<T>).IsAssignableFrom(objectType))
          {
            this.kind = PropertyCompareKind.Ordered;
            return;
          }
          if ((this.indexType == IndexType.Auto || this.indexType == IndexType.Equality) && typeof (IEqualityComparer<T>).IsAssignableFrom(objectType))
          {
            this.kind = PropertyCompareKind.Equality;
            return;
          }
        }
        else
        {
          this.customComparer = false;
          if ((this.indexType == IndexType.Auto || this.indexType == IndexType.Ordered) && typeof (IComparable<T>).IsAssignableFrom(typeof (T)))
          {
            this.kind = PropertyCompareKind.Ordered;
            return;
          }
          if ((this.indexType == IndexType.Auto || this.indexType == IndexType.Equality) && typeof (IEquatable<T>).IsAssignableFrom(typeof (T)))
          {
            this.kind = PropertyCompareKind.Equality;
            return;
          }
        }
        this.kind = PropertyCompareKind.None;
      }

      public IComparer<T> CreateFullComparer()
      {
        if (this.kind != PropertyCompareKind.Ordered)
          throw new NotSupportedException();
        return this.customComparer ? (IComparer<T>) ((ServiceObjectAttribute) this.pd.Attributes[typeof (ComparerAttribute)]).Creator.CreateInstance() : (IComparer<T>) Comparer<T>.Default;
      }

      public IEqualityComparer<T> CreateEqualityComparer()
      {
        if (this.kind != PropertyCompareKind.Equality)
          throw new NotSupportedException();
        return this.customComparer ? (IEqualityComparer<T>) ((ServiceObjectAttribute) this.pd.Attributes[typeof (ComparerAttribute)]).Creator.CreateInstance() : (IEqualityComparer<T>) EqualityComparer<T>.Default;
      }

      public PropertyCompareKind CompareKind => this.kind;

      public bool CustomComparer => this.customComparer;
    }
}
