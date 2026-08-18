
// Type: Intermech.Interfaces.FileExtensionsInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    /// <summary>Класс для описания  настроек просмотра файлов</summary>
    [Serializable]
    public class FileExtensionsInfo
    {
      /// <summary>
      /// 
      /// </summary>
      public StyleView Style { get; set; }

      /// <summary>использовать для просмотра</summary>
      public bool Enabled { get; set; }

      /// <summary>наименование</summary>
      public string Name { get; }

      /// <summary>Программный идентификатор ProgID</summary>
      public string ProgId { get; }

      /// <summary>Маски для расширений файлов</summary>
      public string Extensions { get; }

      /// <summary>использовать для всех пользователей</summary>
      public bool IsAllUser { get; set; }

      /// <summary>строка комманды просмотра</summary>
      public string CommandLine { get; }

      /// <summary>неизвестен</summary>
      public bool IsUnknown => this.Style == StyleView.Unknown;

      /// <summary>
      /// 
      /// </summary>
      public string ShellCommandLine { get; private set; }

      /// <summary>
      /// 
      /// </summary>
      public Guid ID { get; private set; }

      /// <summary>
      /// Признак указывающий, что данная настройка не пишется в базу
      /// </summary>
      public bool NotPersist { get; set; }

      /// <summary>Default constructor</summary>
      private FileExtensionsInfo()
      {
        this.IsAllUser = false;
        this.CommandLine = string.Empty;
        this.Enabled = false;
        this.Name = string.Empty;
        this.ProgId = string.Empty;
        this.Extensions = string.Empty;
        this.NotPersist = false;
        this.Style = StyleView.Unknown;
      }

      /// <summary>Заполнить поля класса</summary>
      /// <param name="used">использовать для просмотра</param>
      /// <param name="name">наименование</param>
      /// <param name="progID">Программный идентификатор ProgID</param>
      /// <param name="extensions">Маски для расширений файлов</param>
      /// <param name="id"></param>
      public FileExtensionsInfo(bool used, string name, string progID, string extensions, Guid id = default (Guid))
        : this()
      {
        this.Enabled = used;
        this.Name = name;
        this.ProgId = progID;
        this.Extensions = extensions;
        this.ID = id;
        this.CheckStyle();
      }

      /// <summary>Заполнить поля класса</summary>
      /// <param name="used">использовать для просмотра</param>
      /// <param name="name">наименование</param>
      /// <param name="progID">Программный идентификатор ProgID</param>
      /// <param name="extensions">Маски для расширений файлов</param>
      /// <param name="commandline"></param>
      public FileExtensionsInfo(
        bool used,
        string name,
        string progID,
        string extensions,
        string commandline)
        : this()
      {
        this.Enabled = used;
        this.Name = name;
        this.ProgId = progID;
        this.Extensions = extensions;
        this.CommandLine = commandline;
        this.CheckStyle();
      }

      /// <summary>Заполнить поля класса на основе строки из настроек</summary>
      /// <param name="settingsString">строка из настроек</param>
      public FileExtensionsInfo(string settingsString)
        : this()
      {
        string[] strArray = settingsString.Split('¦');
        if (strArray.Length < 4)
          return;
        this.Enabled = strArray[0] == "1";
        this.Name = strArray[1];
        this.ProgId = strArray[2];
        this.Extensions = strArray[3];
        this.IsAllUser = strArray.Length > 4 && strArray[4] == "1";
        this.CommandLine = strArray.Length <= 5 ? string.Empty : strArray[5];
        this.CheckStyle();
      }

      /// <summary>Проверка стиля</summary>
      private void CheckStyle()
      {
        this.Style = StyleView.Unknown;
        if (this.IsUnknown)
          this.CheckStyleHandler();
        if (this.IsUnknown)
          this.CheckStyleCommandLine();
        if (this.IsUnknown)
          this.CheckStyleActiveX();
        if (!this.IsUnknown)
          return;
        using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey(this.ProgId + "\\Shell"))
        {
          if (registryKey1 == null)
            return;
          string[] subKeyNames = registryKey1.GetSubKeyNames();
          if (subKeyNames.Length == 0)
            return;
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(subKeyNames[0] + "\\Command"))
          {
            if (registryKey2 == null)
              return;
            this.ShellCommandLine = registryKey2.GetValue("") as string;
            if (!File.Exists(Environment.ExpandEnvironmentVariables(FileExtensionsInfo.ParseArguments(this.ShellCommandLine)[0]).Replace("\"", "")))
              return;
            if (this.ShellCommandLine != null && this.ShellCommandLine.IndexOf("%1") != -1)
            {
              this.ShellCommandLine = this.ShellCommandLine.Replace("\"%1\"", "\"%x\"").Replace("%1", "\"%x\"").Replace("\"%x\"", "\"%1\"");
              this.Style = StyleView.Shell;
            }
            else
            {
              this.Style = StyleView.Default;
              this.ShellCommandLine = $"{this.ShellCommandLine}¦{subKeyNames[0]}";
            }
          }
        }
      }

      /// <summary>проверка стиля на предпросмотр</summary>
      private void CheckStyleHandler()
      {
        switch (this.ProgId)
        {
          case "NativeHandler":
            this.Style = StyleView.Native;
            this.NotPersist = true;
            break;
          case "InternalHandler":
            this.Style = StyleView.Internal;
            this.NotPersist = true;
            break;
          case "InternalExtractView":
            this.Style = StyleView.InternalExtractView;
            this.NotPersist = true;
            break;
          case "PreviewHandler":
            this.Style = StyleView.PreView;
            this.NotPersist = true;
            break;
          case "ExtractImage":
            this.Style = StyleView.ExtractImage;
            this.NotPersist = true;
            break;
          case "PrevThumbnail":
            this.Style = StyleView.PrevThumbnail;
            this.NotPersist = true;
            break;
        }
      }

      /// <summary>проверка стиля </summary>
      private void CheckStyleCommandLine()
      {
        if (this.CommandLine.IndexOf("%1") == -1)
          return;
        this.Style = StyleView.CommandLine;
      }

      /// <summary>проверка стиля на ActiveX</summary>
      private void CheckStyleActiveX()
      {
        if (this.ProgId.Contains("imAxViewers"))
          this.NotPersist = true;
        Type typeFromProgId = Type.GetTypeFromProgID(this.ProgId, false);
        if (typeFromProgId != (Type) null)
        {
          this.ID = typeFromProgId.GUID;
        }
        else
        {
          using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(this.ProgId + "\\CLSID", false))
          {
            if (registryKey != null)
            {
              Guid result;
              if (Guid.TryParse(Convert.ToString(registryKey.GetValue((string) null)), out result))
              {
                Type typeFromClsid = Type.GetTypeFromCLSID(result, false);
                if (typeFromClsid != (Type) null)
                  this.ID = typeFromClsid.GUID;
              }
            }
          }
        }
        if (!(this.ID != Guid.Empty))
          return;
        this.Style = StyleView.ActiveX;
      }

      /// <summary>Converts a wildcard to a regex.</summary>
      /// <param name="pattern">The wildcard pattern to convert.</param>
      /// <returns>A regex equivalent of the given wildcard.</returns>
      private string WildcardToRegex(string pattern)
      {
        pattern = (pattern ?? string.Empty).Trim();
        if (pattern.Length == 0 || pattern == "*.*")
          return "^*$";
        if (pattern.StartsWith("."))
          pattern = pattern.Substring(1);
        pattern = $"(?<mask>{string.Join("|", Regex.Escape(pattern).Split(';', ',', '|'))})";
        return $"^{pattern.Replace("\\|", "|").Replace("|\\.", "|").Replace("\\*", ".*").Replace("\\?", ".")}$";
      }

      public static string[] ParseArguments(string commandLine)
      {
        char[] charArray = commandLine.ToCharArray();
        bool flag1 = false;
        bool flag2 = true;
        for (int index = 0; index < charArray.Length; ++index)
        {
          if (charArray[index] == '"')
            flag1 = !flag1;
          if (flag2 && !flag1 && charArray[index] == ' ')
          {
            charArray[index] = '\n';
            flag2 = false;
          }
        }
        return new string(charArray).Split('\n');
      }

      /// <summary>найти соответствие маске расширений</summary>
      /// <param name="input">расширение файла</param>
      /// <returns>true - расширение файла соответствует маске</returns>
      public bool IsMatch(string input)
      {
        if (input.StartsWith("."))
          input = input.Substring(1);
        return new Regex(this.WildcardToRegex(this.Extensions), RegexOptions.IgnoreCase).IsMatch(input);
      }

      public override string ToString()
      {
        return $"{(this.Enabled ? "1" : "0")}¦{this.Name}¦{this.ProgId}¦{this.Extensions}¦{(this.IsAllUser ? "1" : "0")}¦{this.CommandLine}";
      }

      public override bool Equals(object obj)
      {
        return obj is FileExtensionsInfo fileExtensionsInfo && this.ProgId == fileExtensionsInfo.ProgId && this.Extensions == fileExtensionsInfo.Extensions && this.CommandLine == fileExtensionsInfo.CommandLine && this.Name == fileExtensionsInfo.Name;
      }

      public override int GetHashCode()
      {
        return this.ProgId.GetHashCode() ^ this.Extensions.GetHashCode() ^ this.CommandLine.GetHashCode() ^ this.Name.GetHashCode();
      }
    }
}
