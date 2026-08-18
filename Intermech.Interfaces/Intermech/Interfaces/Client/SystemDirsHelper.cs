
// Type: Intermech.Interfaces.Client.SystemDirsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.IO;


namespace Intermech.Interfaces.Client
{
    /// <summary>
    /// Вспомогательный статический класс для проверки каталогов
    /// </summary>
    public static class SystemDirsHelper
    {
      private static string[] systemFolders = new string[9]
      {
        "SystemDrive",
        "windir",
        "USERPROFILE",
        "ProgramFiles",
        "SystemRoot",
        "CommonProgramFiles",
        "APPDATA",
        "TMP",
        "TEMP"
      };

      /// <summary>Проверить, является ли указанная папка системной</summary>
      /// <param name="folder">Полный путь к проверяемой папке</param>
      /// <returns>true - папка системная</returns>
      public static bool IsSystemFolder(string folder)
      {
        FileInfo fileInfo = new FileInfo(typeof (SystemDirsHelper).Assembly.Location);
        StringComparer cultureIgnoreCase = StringComparer.CurrentCultureIgnoreCase;
        if (cultureIgnoreCase.Compare(folder, fileInfo.DirectoryName) == 0 || cultureIgnoreCase.Compare(folder, fileInfo.DirectoryName + "\\") == 0 || folder == Environment.SystemDirectory)
          return true;
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.SystemDirectory);
        if (cultureIgnoreCase.Compare(folder, directoryInfo.Root.FullName) != 0)
        {
          if (cultureIgnoreCase.Compare(folder + "\\", directoryInfo.Root.FullName) != 0)
          {
            try
            {
              IDictionary environmentVariables = Environment.GetEnvironmentVariables();
              string[] strArray = ((string) environmentVariables[(object) "Path"]).Split(new string[1]
              {
                ";"
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray != null && strArray.Length != 0)
              {
                for (int index = 0; index < strArray.Length; ++index)
                {
                  if (cultureIgnoreCase.Compare(strArray[index], folder) == 0 || cultureIgnoreCase.Compare(strArray[index] + "\\", folder) == 0)
                    return true;
                }
              }
              for (int index = 0; index < SystemDirsHelper.systemFolders.Length; ++index)
              {
                string y = (string) environmentVariables[(object) SystemDirsHelper.systemFolders[index]];
                if (cultureIgnoreCase.Compare(folder, y) == 0 || cultureIgnoreCase.Compare(folder, y + "\\") == 0)
                  return true;
              }
              Array values = Enum.GetValues(typeof (Environment.SpecialFolder));
              for (int index = 0; index < values.Length; ++index)
              {
                string folderPath = Environment.GetFolderPath((Environment.SpecialFolder) values.GetValue(index));
                if (cultureIgnoreCase.Compare(folder, folderPath) == 0 || cultureIgnoreCase.Compare(folder, folderPath + "\\") == 0)
                  return true;
              }
            }
            catch
            {
              return true;
            }
            return false;
          }
        }
        return true;
      }

      /// <summary>Получить путь к папке "TEMP"</summary>
      /// <returns>Путь к папке "TEMP"</returns>
      public static string GetTempDir()
      {
        try
        {
          return (string) Environment.GetEnvironmentVariables()[(object) "TEMP"];
        }
        catch
        {
          return string.Empty;
        }
      }
    }
}
