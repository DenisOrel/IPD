
// Type: Intermech.Interfaces.CommandResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура, предназначенная для возврата результата выполнения пакетных операций
    /// </summary>
    [Serializable]
    public struct CommandResult(long[] processedObjects)
    {
      /// <summary>Идентификаторы успешно обработанных объектов</summary>
      public long[] ProcessedObjects = processedObjects;
      /// <summary>
      /// Ид. последнего объекта, при обработке которого произошла ошибка
      /// </summary>
      public long ErrorObjectID = -1;
      /// <summary>
      /// Сообщение об ошибке обработки объекта номер ErrorObjectID
      /// </summary>
      public string ErrorMessage = "";
    }
}
