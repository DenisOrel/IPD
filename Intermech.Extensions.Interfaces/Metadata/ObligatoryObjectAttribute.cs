// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.ObligatoryObjectAttribute
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Metadata;

public class ObligatoryObjectAttribute(
  [CanBeEmpty] ObligatoryObjectAttributes id,
  [NotNull] Type holderType,
  [NotNull, NotWhitespace] string idPropertyName) : SystemAttribute(id, holderType, idPropertyName)
{
  private int Guid => throw new NotImplementedException("ObligatoryObjectAttribute.Guid");
}
