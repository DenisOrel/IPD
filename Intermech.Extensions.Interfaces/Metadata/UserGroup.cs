// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.UserGroup
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

[InitFieldsWithSession]
public abstract class UserGroup
{
  [NotNull]
  public const long UnknownID = 0;
  [NotNull]
  public static readonly SystemUserGroup AllUsers = UserGroup.Create("cad00017-306c-11d8-b4e9-00304f19f545", nameof (AllUsers));
  [NotNull]
  public static readonly SystemUserGroup ObjectCreator = UserGroup.Create("cadd96b1-306c-11d8-b4e9-00304f19f545", nameof (ObjectCreator));
  [NotNull]
  public static readonly SystemUserGroup RelationCreator = UserGroup.Create("cadd96b3-306c-11d8-b4e9-00304f19f545", nameof (RelationCreator));
  public const string EntityName = "Группы пользователей";
  public const string EntityInstanceName = "Группа пользователей";
  public const string EntityInstanceNameInGenitiveCase = "группы пользователей";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemUserGroup Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return UserGroup.Create<UserGroup>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemUserGroup Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [NotNull, NotWhitespace] string idName) where THolder : UserGroup
  {
    return new SystemUserGroup(new Guid(guid), typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string AllUsersGuid = "cad00017-306c-11d8-b4e9-00304f19f545";
    public const string ObjectCreatorGuid = "cadd96b1-306c-11d8-b4e9-00304f19f545";
    public const string RelationCreatorGuid = "cadd96b3-306c-11d8-b4e9-00304f19f545";
  }
}
