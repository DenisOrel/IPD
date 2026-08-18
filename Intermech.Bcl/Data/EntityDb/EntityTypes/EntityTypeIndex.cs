
// Type: Intermech.Data.EntityDb.EntityTypes.EntityTypeIndex
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Data.EntityDb.Common;
using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb.EntityTypes
{
    internal sealed class EntityTypeIndex : UniversalIndex<Type, Type>
    {
      public EntityTypeIndex()
        : base((IIndexKeyProvider<Type, Type>) new EmptyIndexKeyProvider<Type>(), (IDirectIndex<Type>) new NonUniqueEqualityDirectIndex<Type>((IEqualityComparer<Type>) EqualityComparer<Type>.Default), (IInverseIndex<Type>) new UniqueInverseIndex<Type>())
      {
      }
    }
}
