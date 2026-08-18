
// Type: Intermech.Settings.IniFile
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using Intermech.WindowsDll;


namespace Intermech.Settings
{
    /// <summary>Класс для работы с Ini-файлами</summary>
    public sealed class IniFile
    {
      [NotNull]
      [FileExists]
      private readonly string _filePath;

      public IniFile([NotNull, FileExists] string filePath) => this._filePath = filePath;

      [CanBeNull]
      public string Read([NotNull, NotWhitespace] string section, [NotNull, NotWhitespace] string key, [CanBeNull] string defaultValue = null)
      {
        return Kernel32.GetPrivateProfileString_ThrowWinErrors(section, key, defaultValue, this._filePath).Trim();
      }

      [CanBeNull]
      public static string ReadValue([NotNull, FileExists] string filePath, [NotNull, NotWhitespace] string section, [NotNull, NotWhitespace] string key, [CanBeNull] string defaultValue = null)
      {
        return Kernel32.GetPrivateProfileString_ThrowWinErrors(section, key, defaultValue, filePath).Trim();
      }

      public void Write([CanBeNull, NotWhitespace] string section, [CanBeNull, NotWhitespace] string key, [CanBeNull] string value)
      {
        Kernel32.WritePrivateProfileString_ThrowWinErrors(section, key, value, this._filePath);
      }

      public static void WriteValue([NotNull, FileExists] string filePath, [CanBeNull, NotWhitespace] string section, [CanBeNull, NotWhitespace] string key, [CanBeNull] string value)
      {
        Kernel32.WritePrivateProfileString_ThrowWinErrors(section, key, value, filePath);
      }

      public void DeleteKey([NotNull, NotWhitespace] string section, [NotNull, NotWhitespace] string key)
      {
        this.Write(section, key, (string) null);
      }

      public static void DeleteKey([NotNull, FileExists] string filePath, [NotNull, NotWhitespace] string section, [NotNull, NotWhitespace] string key)
      {
        IniFile.WriteValue(filePath, section, key, (string) null);
      }

      public void DeleteSection([NotNull, NotWhitespace] string section)
      {
        this.Write(section, (string) null, (string) null);
      }

      public static void DeleteSection([NotNull, FileExists] string filePath, [NotNull, NotWhitespace] string section)
      {
        IniFile.WriteValue(filePath, section, (string) null, (string) null);
      }

      public bool KeyExists([NotNull, NotWhitespace] string section, [NotNull, NotWhitespace] string key)
      {
        return (this.Read(section, key) ?? string.Empty).Length > 0;
      }
    }
}
