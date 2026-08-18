
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Client.Core.Resources.ClientCoreResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Client.Core.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull] string stringName, [NotNull] params object[] args)
  {
    string format = LocalizationHolder.rm.GetString(stringName);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    string format = LocalizationHolder.rm.GetString(stringName, cultureInfo);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeString([NotNull] string stringName, [NotNull] params object[] args)
  {
    string format = LocalizationHolder.rma.GetString(stringName);
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
    string format = LocalizationHolder.rma.GetString(stringName, cultureInfo);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }
}
