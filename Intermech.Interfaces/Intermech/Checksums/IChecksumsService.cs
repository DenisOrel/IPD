
// Type: Intermech.Checksums.IChecksumsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Checksums
{
    /// <summary>
    /// Интерфейс сервиса для вычисления контрольных сумм значений файловых/блобовых/короткоблобовых атрибутов
    /// </summary>
    public interface IChecksumsService
    {
      /// <summary>
      /// Для файлового/блобового атрибута начать процесс вычисления контрольной суммы, проверять статус по GetChecksumTaskStatus,
      /// после получения результата сообщать об этом серверу по ChecksumTaskFree
      /// </summary>
      /// <param name="sessionGuid">пользовательская сессия</param>
      /// <param name="elementId">ид объекта/связи</param>
      /// <param name="kind">объект/связь</param>
      /// <param name="attributeId">ид атрибута</param>
      /// <param name="index">индекс значения внутри атрибута</param>
      /// <param name="algorithm">алгоритм вычисления хэша</param>
      /// <returns>taskGuid</returns>
      Guid CalcChecksum(
        Guid sessionGuid,
        long elementId,
        AttributableElements kind,
        int attributeId,
        int index,
        ChecksumAlgorithm algorithm);

      /// <summary>
      /// Получить статус выполнения задачи вычисления контрольной суммы
      /// </summary>
      /// <param name="taskGuid"></param>
      /// <returns></returns>
      ChecksumTaskProgress GetChecksumTaskProgress(Guid taskGuid);

      /// <summary>
      /// Выдать результат вычисления контрольной суммы по окончании расчета, проверять предварительно GetChecksumTaskStatus, вызывать ChecksumFree после получения значения
      /// </summary>
      /// <param name="taskGuid"></param>
      /// <returns></returns>
      ChecksumClass GetChecksum(Guid taskGuid);

      /// <summary>
      /// Освободить результат вычисления контрольной суммы от удержания сервером
      /// </summary>
      /// <param name="taskGuid"></param>
      void ChecksumFree(Guid taskGuid);
    }
}
