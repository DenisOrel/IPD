
// Type: Intermech.Interfaces.Kernel.SDSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Kernel
{
    /// <summary>
    /// Класс для хранения настроек службы диагностики ядра IPS
    /// </summary>
    [Serializable]
    public class SDSettings
    {
      /// <summary>
      /// Размер файлов в шкафу (в гигабайтах), после которого нужно уведомлять админа о его превышении (если в описании шкафа не сказано другое).
      /// 0 - не нужно никаких нотификаций
      /// </summary>
      public int StorageSizeNotification { get; private set; }

      /// <summary>
      /// Нижний порог свободного места на диске сервера приложений (в гигабайтах), после которого нужно уведомлять администратора о проблеме
      /// </summary>
      public int ServerDiskFreeSizeNotification { get; private set; }

      /// <summary>
      /// Максимальный объем физической памяти, использованной сервером приложений (в мегабайтах), после которого нужно уведомлять администратора о проблеме
      /// </summary>
      public int ServerPeakMemoryUsageNotification { get; private set; }

      /// <summary>Максимальный размер лог-файла (в мегабайтах)</summary>
      public int MaxLogFileSize { get; private set; }

      /// <summary>Количество предыдущих копий лог-файла</summary>
      public int MaxLogFileCopies { get; private set; }

      /// <summary>Путь к папке с лог-файлами на сервере приложений</summary>
      public string ServerLogPath { get; private set; }

      public SDSettings(
        int maxStorageSize,
        int serverDiskFreeSize,
        int serverPeakMemoryUsage,
        int maxLogFileSize,
        string serverLogPath,
        int maxLogFileCopies)
      {
        this.ServerDiskFreeSizeNotification = serverDiskFreeSize;
        this.StorageSizeNotification = maxStorageSize;
        this.ServerPeakMemoryUsageNotification = serverPeakMemoryUsage;
        this.MaxLogFileSize = maxLogFileSize;
        this.ServerLogPath = serverLogPath;
        this.MaxLogFileCopies = maxLogFileCopies;
      }
    }
}
