// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.Localization
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Office.Server;

internal class Localization
{
  [NotNull]
  private static readonly ResourceManager _resources = new ResourceManager("Intermech.Office.Server.Resources.OfficeServerResources", Assembly.GetExecutingAssembly());
  [NotNull]
  private static readonly ResourceManager _attributes = new ResourceManager("Intermech.Office.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull] string stringName, [NotNull] params object[] args)
  {
    string format = Localization._resources.GetString(stringName);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    string format = Localization._resources.GetString(stringName, cultureInfo);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeString([NotNull] string stringName, [NotNull] params object[] args)
  {
    string format = Localization._attributes.GetString(stringName);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeString(
    [NotNull] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    string format = Localization._attributes.GetString(stringName, cultureInfo);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }
}
