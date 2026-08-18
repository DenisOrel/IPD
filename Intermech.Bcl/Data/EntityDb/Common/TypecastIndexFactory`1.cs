
// Type: Intermech.Data.EntityDb.Common.TypecastIndexFactory`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class TypecastIndexFactory<TKey> : IPropertyIndexFactory
    {
      public IIndex<object> CreateIndex(
        PropertyKind propertyKind,
        bool isUnique,
        IPropertyCompareInfo compareInfo)
      {
        return (IIndex<object>) new UniversalIndex<object, TKey>((IIndexKeyProvider<object, TKey>) new TypecastIndexKeyProvider<TKey>(), this.CreateDirectIndex(isUnique, (IPropertyCompareFactory<TKey>) compareInfo ?? throw new ArgumentException("CompareInfo must implement IPropertyCompareFactory<>.", nameof (compareInfo))), this.CreateInverseIndex(propertyKind));
      }

      private IDirectIndex<TKey> CreateDirectIndex(
        bool isUnique,
        IPropertyCompareFactory<TKey> compareFactory)
      {
        switch (compareFactory.CompareKind)
        {
          case PropertyCompareKind.Equality:
            IEqualityComparer<TKey> equalityComparer = compareFactory.CreateEqualityComparer();
            return isUnique ? (IDirectIndex<TKey>) new UniqueEqualityDirectIndex<TKey>(equalityComparer) : (IDirectIndex<TKey>) new NonUniqueEqualityDirectIndex<TKey>(equalityComparer);
          case PropertyCompareKind.Ordered:
            IComparer<TKey> fullComparer = compareFactory.CreateFullComparer();
            return isUnique ? (IDirectIndex<TKey>) new UniqueOrderedDirectIndex<TKey>(fullComparer) : (IDirectIndex<TKey>) new NonUniqueOrderedDirectIndex<TKey>(fullComparer);
          default:
            throw new NotSupportedException();
        }
      }

      private IInverseIndex<TKey> CreateInverseIndex(PropertyKind propertyKind)
      {
        if (propertyKind == PropertyKind.Scalar)
          return (IInverseIndex<TKey>) new UniqueInverseIndex<TKey>();
        if (propertyKind == PropertyKind.Vector)
          return (IInverseIndex<TKey>) new NonUniqueInverseIndex<TKey>();
        throw new NotImplementedException();
      }
    }
}
