// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.UserObjectType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public class UserObjectType : SystemObjectType
{
  public const string TypeGuid = "cad00002-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static UserObjectType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad00002-306c-11d8-b4e9-00304f19f545");
    return new UserObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid), guid, idName);
  }

  private UserObjectType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (ObjectTypes), true, idPropertyName)
  {
  }

  protected internal UserObjectType(
    [NotEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
  }

  public new abstract class Attributes : SystemObjectType.Attributes
  {
    [NotNull]
    public static readonly SystemAttribute4ObjectType HireDate = UserObjectType.Attributes.Create(Intermech.Metadata.Attributes.UserHireDate);
    [NotNull]
    public static readonly SystemAttribute4ObjectType FireDate = UserObjectType.Attributes.Create(Intermech.Metadata.Attributes.UserFireDate);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4ObjectType Create([NotNull] SystemAttribute attribute)
    {
      return SystemObjectType.Attributes.Create("cad00002-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }
}
