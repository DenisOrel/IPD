// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectInfoCache
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Concurrent;

#nullable disable
namespace Intermech.Interfaces;

[Obsolete("Этот кэш вообще не должен использоваться, т.к. не реализовано обновление заголовка после его изменения")]
public class ObjectInfoCache
{
  [NotNull]
  private static readonly ConcurrentDictionary<long, string> _cache = new ConcurrentDictionary<long, string>();

  [NotNull]
  public static string GetCaption([CanBeEmpty] long objectID)
  {
    return ObjectInfoCache._cache.GetOrAdd(objectID, (Func<long, string>) (objID =>
    {
      string str;
      switch (objID)
      {
        case -1:
          str = "No object";
          break;
        case 0:
          str = "Unknown object";
          break;
        default:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            try
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
              str = !objectInfo.Empty ? objectInfo.Caption : "?";
              break;
            }
            catch
            {
              str = "?";
              break;
            }
          }
      }
      return ObjectInfoCache._cache[objID] = str;
    })) ?? "?";
  }

  [NotNull]
  public static string GetCaption([CanBeEmpty] long objectID, [NotNull] IUserSession session)
  {
    return ObjectInfoCache._cache.GetOrAdd(objectID, (Func<long, string>) (objID =>
    {
      string str;
      switch (objID)
      {
        case -1:
          str = "No object";
          break;
        case 0:
          str = "Unknown object";
          break;
        default:
          try
          {
            QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
            str = !objectInfo.Empty ? objectInfo.Caption : "?";
            break;
          }
          catch
          {
            str = "?";
            break;
          }
      }
      return ObjectInfoCache._cache[objID] = str;
    })) ?? "?";
  }
}
