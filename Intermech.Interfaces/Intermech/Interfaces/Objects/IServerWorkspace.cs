
// Type: Intermech.Interfaces.Objects.IServerWorkspace
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Objects
{
    /// <summary>Интерфейс серверного объекта "Рабочий стол"</summary>
    public interface IServerWorkspace
    {
      /// <summary>
      /// Найти объект номер objectID на рабочем столе. Возвращает ид. папки или
      /// самого рабочего стола, где лежит этот объект. Если объект на столе не
      /// найден, то возвращает -1. Если objectID = -1, то ищет на рабочем столе
      /// рабочие копии объектов.
      /// </summary>
      long FindInWorkspace(long objectID);

      /// <summary>
      /// Создает на рабочем столе обязательные выборки и папки (если их нет) - Корзина, Взятые на изменение и пр.
      /// Возвращает true, если что-либо было создано в ходе работы этой функции.
      /// </summary>
      bool CreateSamples();
    }
}
