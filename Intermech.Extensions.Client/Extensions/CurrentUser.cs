// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CurrentUser
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class CurrentUser
{
  private static long? _id;
  [CanBeNull]
  private static Guid? _userGuid;
  [CanBeNull]
  private static Guid? _roleGuid;
  [CanBeNull]
  private static bool? _isAdmin;

  public static long ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CurrentUser._id.CheckInitializedIn<long>(typeof (Helper));
    }
  }

  public static Guid UserGuid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CurrentUser._userGuid.CheckInitializedIn<Guid>(typeof (Helper));
    }
  }

  public static Guid? RoleGuid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return new Guid?(CurrentUser._roleGuid.CheckInitializedIn<Guid>(typeof (Helper)));
    }
  }

  public static bool IsAdmin
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CurrentUser._isAdmin.CheckInitializedIn<bool>(typeof (Helper));
    }
  }

  internal static void Init([NotNull] IUserSession session)
  {
    using (SessionKeeper sessionKeeper = session.IsSystemSession ? new SessionKeeper() : (SessionKeeper) null)
    {
      if (sessionKeeper != null)
        session = sessionKeeper.Session;
      CurrentUser._isAdmin = new bool?(session.IsAdmin);
      UserAndRoleInfo userAndRoleInfo = session.GetUserAndRoleInfo();
      CurrentUser._id = new long?(userAndRoleInfo.ID);
      CurrentUser._userGuid = new Guid?(userAndRoleInfo.UserGuid);
      CurrentUser._roleGuid = new Guid?(userAndRoleInfo.RoleGuid);
    }
  }
}
