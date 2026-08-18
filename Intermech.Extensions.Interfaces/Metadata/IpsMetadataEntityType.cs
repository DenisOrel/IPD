// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.IpsMetadataEntityType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Metadata;

public abstract class IpsMetadataEntityType : IpsMetadataEntityBase<int>
{
  protected IpsMetadataEntityType(
    [NotNull, NotWhitespace] string guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(guid, holderType, obligatory, idPropertyName)
  {
  }

  protected IpsMetadataEntityType(
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(guid, holderType, obligatory, idPropertyName)
  {
  }

  protected IpsMetadataEntityType(
    [NotEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
  }

  protected internal IpsMetadataEntityType(
    [NotEmpty] ObligatoryObjectAttributes id,
    [NotNull] Type holderType,
    [NotNull, NotWhitespace] string idPropertyName)
    : base((int) id, holderType, idPropertyName)
  {
  }
}
