
// Type: Intermech.Localization.Resources
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Localization
{
    [LocalizationRequired]
    internal class Resources
    {
      [NotNull]
      private static readonly ResourceManager _resources = new ResourceManager("Intermech.Resources.BclResources", Assembly.GetExecutingAssembly());
      [NotNull]
      private static readonly ResourceManager _attributes = new ResourceManager("Intermech.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

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

      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetAttributeString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
      {
        string format = Intermech.Localization.Resources._attributes.GetString(stringName);
        if (format == null)
          return stringName;
        return args.Length != 0 ? string.Format(format, args) : format;
      }

      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetAttributeString(
        [NotNull, NotWhitespace] string stringName,
        [NotNull] CultureInfo cultureInfo,
        [NotNull] params object[] args)
      {
        string format = Intermech.Localization.Resources._attributes.GetString(stringName, cultureInfo);
        if (format == null)
          return stringName;
        return args.Length != 0 ? string.Format(format, args) : format;
      }
    }
}
