// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.Role
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

[InitFieldsWithSession]
public abstract class Role
{
  [NotNull]
  public const long UnknownID = 0;
  [NotNull]
  public static readonly SystemRole InternalService = Role.Create("cadd96ad-306c-11d8-b4e9-00304f19f545", nameof (InternalService));
  public const string EntityName = "Роли";
  public const string EntityInstanceName = "Роль";
  public const string EntityInstanceNameInGenitiveCase = "роли";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemRole Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Role.Create<Role>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemRole Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [NotNull, NotWhitespace] string idName) where THolder : Role
  {
    return new SystemRole(new Guid(guid), typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string InternalServiceGuid = "cadd96ad-306c-11d8-b4e9-00304f19f545";
  }
}
