// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.UserSessionExtensions
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class UserSessionExtensions
{
  /// <summary>Получить интерфейс канцелярского поручения.</summary>
  /// <exception cref="T:Intermech.Office.Interfaces.ResolutionNotFoundException">Если поручение не будет найдено в БД</exception>
  /// <exception cref="T:Intermech.Office.Interfaces.ObjectIsNotIDBResolutionException">Если объект с переданным идентификатором найден, но он не поручение</exception>
  /// <param name="session">The session to act on. This cannot be null.</param>
  /// <param name="resolutionID">Identifier for the resolution.</param>
  /// <param name="throwExceptOnError">(Optional) True to throw exception if not found.</param>
  /// <returns>The resolution. This may be null.</returns>
  [ContractAnnotation("throwExceptOnError:false => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBResolution GetResolution(
    [NotNull] this IUserSession session,
    long resolutionID,
    bool throwExceptOnError = true)
  {
    return session.GetObject<IDBResolution, ResolutionNotFoundException>(resolutionID, throwExceptOnError);
  }
}
