// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Helper.VisibleAttHelper
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Helper;

internal class VisibleAttHelper
{
  private static bool _initialized;
  private static string _value;

  internal static void Init(IUserSession session)
  {
    long allUsersGroupId = session.IdentHelper.AllUsersGroupID;
    VisibleAttHelper._value = new ObjectsVisibility()
    {
      Rights = {
        {
          allUsersGroupId,
          ObjectsVisibilityFlags.Hidden
        }
      }
    }.ToString();
    VisibleAttHelper._initialized = true;
  }

  internal static string AllUsersHidden
  {
    get
    {
      if (!VisibleAttHelper._initialized)
        throw new Exception("VisibleAttHelper не инициализирован!");
      return VisibleAttHelper._value;
    }
  }
}
