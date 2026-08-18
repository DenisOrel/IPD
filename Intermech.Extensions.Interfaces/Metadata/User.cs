// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.User
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

[InitFieldsWithSession]
public abstract class User
{
  public const string EntityInstanceNameInGenitiveCase = "пользователя";
  [NotNull]
  public const long UnknownID = 0;
  [NotNull]
  public static readonly SystemUser System = User.Create("cad0000d-306c-11d8-b4e9-00304f19f545", nameof (System));
  [NotNull]
  public static readonly SystemUser InternalSettingsUpdater = User.Create("cadd96af-306c-11d8-b4e9-00304f19f545", nameof (InternalSettingsUpdater));
  public const string EntityName = "Пользователи";
  public const string EntityInstanceName = "Пользователь";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemUser Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return User.Create<User>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemUser Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [NotNull, NotWhitespace] string idName) where THolder : User
  {
    return new SystemUser(new Guid(guid), typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string SystemGuid = "cad0000d-306c-11d8-b4e9-00304f19f545";
    public const string InternalSettingsUpdaterGuid = "cadd96af-306c-11d8-b4e9-00304f19f545";
  }
}
