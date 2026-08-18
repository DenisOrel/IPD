
// Type: Intermech.Data.SectionEntities.SectionProperty
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Data.EntityDb;
using Intermech.Data.EntityDb.Common;
using System;
using System.Collections;
using System.ComponentModel;


namespace Intermech.Data.SectionEntities
{
    internal sealed class SectionProperty
    {
      private static readonly Type compareInfoGenType = typeof (PropertyCompareInfo<>);
      private static readonly Type indexFactoryGenType = typeof (TypecastIndexFactory<>);
      private static readonly Type enumerableCollectionType = typeof (IEnumerable);
      private readonly long uniqueId;
      private readonly PropertyDescriptor pd;
      private readonly IndexableAttribute indexableAttr;
      private readonly PropertyKind propKind;
      private readonly Type propDataType;
      private readonly bool isReadOnly;
      private IPropertyCompareInfo compareInfo;
      private IPropertyIndexFactory indexFactory;

      public SectionProperty(PropertyDescriptor pd, IndexableAttribute indexableAttr)
      {
        if (pd == null)
          throw new ArgumentNullException(nameof (pd));
        if (indexableAttr == null)
          throw new ArgumentNullException(nameof (indexableAttr));
        this.uniqueId = (long) RuntimeId.Create();
        this.pd = pd;
        this.indexableAttr = indexableAttr;
        this.propKind = SectionProperty.DetectPropertyKind(pd);
        this.propDataType = SectionProperty.DetectPropertyDataType(pd, this.propKind);
        this.isReadOnly = pd.IsReadOnly || !pd.SupportsChangeEvents;
      }

      private static PropertyKind DetectPropertyKind(PropertyDescriptor pd)
      {
        if (pd == null)
          throw new ArgumentNullException(nameof (pd));
        return pd.PropertyType == typeof (string) || !SectionProperty.enumerableCollectionType.IsAssignableFrom(pd.PropertyType) ? PropertyKind.Scalar : PropertyKind.Vector;
      }

      private static Type DetectPropertyDataType(PropertyDescriptor pd, PropertyKind kind)
      {
        if (pd == null)
          throw new ArgumentNullException(nameof (pd));
        if (kind == PropertyKind.Scalar)
          return pd.PropertyType;
        if (kind != PropertyKind.Vector)
          throw new NotSupportedException();
        if (pd.PropertyType.IsArray && pd.PropertyType.HasElementType)
          return pd.PropertyType.GetElementType();
        if (pd.PropertyType.IsGenericType)
        {
          Type[] genericArguments = pd.PropertyType.GetGenericArguments();
          if (genericArguments.Length == 1)
            return genericArguments[0];
        }
        throw new Exception("Can't detect property data type.");
      }

      public IIndex<object> CreateIndex()
      {
        if (this.indexFactory == null)
          this.indexFactory = (IPropertyIndexFactory) Activator.CreateInstance(SectionProperty.indexFactoryGenType.MakeGenericType(this.DataType));
        return this.indexFactory.CreateIndex(this.Kind, this.IsUnique, this.CompareInfo);
      }

      public object GetValue(object sectionObject)
      {
        return sectionObject != null ? this.pd.GetValue(sectionObject) : throw new ArgumentNullException(nameof (sectionObject));
      }

      public long UniqueId => this.uniqueId;

      public PropertyDescriptor Descriptor => this.pd;

      public PropertyKind Kind => this.propKind;

      public Type DataType => this.propDataType;

      public bool IsReadOnly => this.isReadOnly;

      public IndexType IndexType => this.indexableAttr.IndexType;

      public bool IsUnique => this.indexableAttr.IsUnique;

      public IPropertyCompareInfo CompareInfo
      {
        get
        {
          if (this.compareInfo == null)
            this.compareInfo = (IPropertyCompareInfo) Activator.CreateInstance(SectionProperty.compareInfoGenType.MakeGenericType(this.propDataType), (object) this.Descriptor, (object) this.IndexType);
          return this.compareInfo;
        }
      }

      public override int GetHashCode() => this.uniqueId.GetHashCode();

      public override bool Equals(object obj)
      {
        return !(obj is SectionProperty sectionProperty) ? base.Equals(obj) : sectionProperty.uniqueId == this.uniqueId;
      }
    }
}
