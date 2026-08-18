
// Type: Intermech.Remoting.Ipc.IpcConnectionInfo
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Remoting.Ipc
{
    /// <summary>
    /// Параметры подключения к другому приложению по ipc-каналу .NET Remoting.
    /// </summary>
    [Serializable]
    public class IpcConnectionInfo
    {
      /// <summary>Время ожидания подключения по умолчанию</summary>
      public const int DefaultLaunchTime = 30000;

      /// <summary>Создает объект.</summary>
      /// <param name="applicationName">Имя приложения, к которому выполняется подключение</param>
      /// <param name="applicationObjectUri">Локальный uri головного объекта приложения, к которому выполняется подключение</param>
      /// <param name="executablePath">Путь к исполняемому файлу приложения</param>
      /// <param name="launchWaitTime">Время ожидания подключения</param>
      public IpcConnectionInfo(
        string applicationName,
        string applicationObjectUri,
        string executablePath,
        string commandLineArgs = "",
        int launchWaitTime = 30000)
      {
        if (string.IsNullOrEmpty(applicationName))
          throw new ArgumentException("Не задано имя приложения, к которому выполняется подключение.", nameof (applicationName));
        if (string.IsNullOrEmpty(applicationObjectUri))
          throw new ArgumentException("Не задан uri головного объекта приложения, к которому выполняется подключение", nameof (applicationObjectUri));
        if (string.IsNullOrEmpty(executablePath))
          throw new ArgumentException("Не задан путь к исполняемому файлу приложения.", nameof (executablePath));
        if (commandLineArgs == null)
          throw new ArgumentNullException("Не заданы аргументы командной строки для приложения, к которому выполняется подключение", nameof (commandLineArgs));
        if (launchWaitTime <= 0)
          throw new ArgumentException("Некорректно задано время ожидания загрузки процесса.", nameof (launchWaitTime));
        this.ApplicationName = applicationName;
        this.ApplicationObjectUri = applicationObjectUri;
        this.ExecutablePath = executablePath;
        this.CommandLineArgs = commandLineArgs;
        this.LaunchWaitTime = launchWaitTime;
      }

      /// <summary>
      /// Возвращает имя приложения, к которому выполняется подключение.
      /// </summary>
      public string ApplicationName { get; }

      /// <summary>
      /// Возвращает локальный uri головного объекта приложения, к которому выполняется подключение.
      /// </summary>
      public string ApplicationObjectUri { get; }

      /// <summary>
      /// Возвращает аргументы командной строки, которые передаются приложению при подключении
      /// </summary>
      public string CommandLineArgs { get; }

      /// <summary>Возвращает путь к исполняемому файлу приложения.</summary>
      public string ExecutablePath { get; }

      /// <summary>Возвращает время ожидания подключения</summary>
      public int LaunchWaitTime { get; }
    }
}
