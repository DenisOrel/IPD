
// Type: Intermech.ApplicationModel.ApplicationEventLogWriters
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Configuration;
using Intermech.Diagnostics;
using Intermech.IO;
using System;
using System.IO;


namespace Intermech.ApplicationModel
{
    /// <summary>Фабрика для журналов событий приложений IPS.</summary>
    public static class ApplicationEventLogWriters
    {
      private const string vshostSuffix = ".vshost";

      /// <summary>
      /// Создает объект для записи в текстовый журнал событий приложения, используя для размещения файла журнала
      /// либо каталого временных файлов + имя процесса приложения, либо каталог, указанный в параметре "LogPath" из файла app.config.
      /// </summary>
      /// <param name="fileName">Имя файла журнала</param>
      /// <returns></returns>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="fileName" /> не должен быть пуст или равен null</exception>
      public static TextFileEventLogWriter CreateTextFileWriter(string fileName)
      {
        if (string.IsNullOrEmpty(fileName))
          throw new ArgumentException("Не задано имя файла журнала событий.", nameof (fileName));
        if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
          fileName = Path.ChangeExtension(fileName, ".log");
        string str1 = Path.Combine(Path.GetTempPath(), ApplicationEventLogWriters.GetProcessExecutableName());
        string name = AppSettingsHelper.GetString("LogPath", (string) null);
        if (!string.IsNullOrEmpty(name))
        {
          string str2 = Environment.ExpandEnvironmentVariables(name);
          if (!Path.IsPathRooted(str2))
            str2 = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, str2));
          str1 = str2;
        }
        if (!Directory.Exists(str1))
          Directory.CreateDirectory(str1);
        string str3 = Path.Combine(str1, fileName);
        if (File.Exists(str3))
          FileUtils.SetReadOnlyAttribute(str3, false);
        return EventLogWriters.CreateTextFileWriter(str3);
      }

      private static string GetProcessExecutableName()
      {
        string path = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        if (path.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase) || path.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
          path = Path.GetFileNameWithoutExtension(path);
        if (path.EndsWith(".vshost"))
          path = path.Remove(path.Length - ".vshost".Length);
        return path;
      }
    }
}
