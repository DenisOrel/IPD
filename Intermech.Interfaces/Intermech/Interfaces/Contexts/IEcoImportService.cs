
// Type: Intermech.Interfaces.Contexts.IEcoImportService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Серверная служба, позволяющая обрабатывать импортированные извещения
    /// </summary>
    public interface IEcoImportService
    {
      /// <summary>Проверить, выполняется ли задание</summary>
      bool IsRunning { get; }

      /// <summary>
      /// Количество обработанных извещений. Если задание не было запущено, будет выдано исключение
      /// </summary>
      long Progress { get; }

      /// <summary>Попытаться запустить задание</summary>
      /// <returns>true - задание было успешно запущено. Если задание было запущено ранее, будет выдано исключение</returns>
      bool Start();

      /// <summary>Попытаться остановить задание</summary>
      /// <returns>true - задание было успешно остановлено. Если задание не было запущено, будет выдано исключение</returns>
      bool Stop();
    }
}
