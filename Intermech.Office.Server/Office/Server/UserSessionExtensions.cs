// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.UserSessionExtensions
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Office.Server;

public static class UserSessionExtensions
{
  [ContractAnnotation("throwExceptOnError:false => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static DBResolution GetResolution(
    [NotNull] this IUserSession session,
    [NotEmpty] long resolutionID,
    bool throwExceptOnError = true)
  {
    return session.GetObject<DBResolution, ResolutionNotFoundException>(resolutionID, throwExceptOnError);
  }
}
