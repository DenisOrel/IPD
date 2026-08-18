
// Type: Intermech.Interfaces.IAssignable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс позволяет копировать в поля объекта содержимое полей другого объекта
    /// </summary>
    public interface IAssignable
    {
      /// <summary>Очистить поля класса</summary>
      void Clear();

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      void Assign(object source);
    }
}
