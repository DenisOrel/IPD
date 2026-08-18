// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SortedRelType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public sealed class SortedRelType : SystemRelationType
{
  public const string TypeGuid = "cad00151-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static SortedRelType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad00151-306c-11d8-b4e9-00304f19f545");
    return new SortedRelType(MetaDataHelperService.Instance.GetRelationTypeID(guid), guid, idName);
  }

  private SortedRelType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (RelationTypes), true, idPropertyName)
  {
  }

  public new abstract class Attributes : SystemRelationType.Attributes
  {
    [NotNull]
    public static readonly SystemAttribute4RelationType SortIndex = SortedRelType.Attributes.Create(Intermech.Metadata.Attributes.SortIndex);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4RelationType Create([NotNull] SystemAttribute attribute)
    {
      return SystemRelationType.Attributes.Create("cad00151-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }
}
