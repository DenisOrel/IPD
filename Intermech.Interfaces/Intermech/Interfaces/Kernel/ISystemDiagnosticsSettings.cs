
// Type: Intermech.Interfaces.Kernel.ISystemDiagnosticsSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Kernel
{
    /// <summary>Интерфейс настроек службы диагностики ядра системы</summary>
    public interface ISystemDiagnosticsSettings
    {
      /// <summary>Максимальный размер лог-файла (в мегабайтах)</summary>
      int MaxLogFileSize { get; }

      /// <summary>Максимальный размер лог-файла (в байтах)</summary>
      int MaxLogFileSizeInBytes { get; }

      /// <summary>Количество предыдущих копий лог-файла</summary>
      int MaxLogFileCopies { get; }

      /// <summary>Каталог с лог-файлами сервера приложений</summary>
      string ServerLogPath { get; }

      /// <summary>Получить все настройки диагностики</summary>
      SDSettings Settings { get; }

      /// <summary>Установить новые настройки диагностики</summary>
      /// <param name="sessionGuid">Гуид админской сессии</param>
      /// <param name="settings">Настройки</param>
      void SetSettings(Guid sessionGuid, SDSettings settings);
    }
}
