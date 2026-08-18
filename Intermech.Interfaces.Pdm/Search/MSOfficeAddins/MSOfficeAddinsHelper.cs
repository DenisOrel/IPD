// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeAddins.MSOfficeAddinsHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Search.MSOfficeAddins;

public static class MSOfficeAddinsHelper
{
  private static readonly Regex ObjectUrlRegex = new Regex("ips://object/(?<objectVersionID>-?[0-9]+)(/(?<action>.*))?", RegexOptions.Compiled);

  public static bool IsObjectUrl(string url)
  {
    return !string.IsNullOrEmpty(url) ? MSOfficeAddinsHelper.ObjectUrlRegex.IsMatch(url) : throw new ArgumentException();
  }

  public static long GetObjectVersionIDFromObjectUrl(string objectUrl)
  {
    if (string.IsNullOrEmpty(objectUrl))
      throw new ArgumentException();
    Group group = MSOfficeAddinsHelper.ObjectUrlRegex.Match(objectUrl).Groups["objectVersionID"];
    return group == null ? 0L : Convert.ToInt64(group.Value);
  }

  public static string CreateObjectUrl(long objectVersionID, string oldObjectUrl = null)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    return $"ips://object/{objectVersionID}/{MSOfficeAddinsHelper.GetActionFromObjectUrl(oldObjectUrl) ?? "card"}";
  }

  private static string GetActionFromObjectUrl(string objectUrl)
  {
    return (!string.IsNullOrEmpty(objectUrl) ? MSOfficeAddinsHelper.ObjectUrlRegex.Match(objectUrl).Groups["action"] : (Group) null)?.Value;
  }
}
