
// Type: Intermech.Localization.Resources
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Localization;

[LocalizationRequired]
internal class Resources
{
  [NotNull]
  private static readonly ResourceManager _resources = new ResourceManager("Intermech.Controls.Resources.Resources", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    string format = Intermech.Localization.Resources._resources.GetString(stringName);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    string format = Intermech.Localization.Resources._resources.GetString(stringName, cultureInfo);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }
}
