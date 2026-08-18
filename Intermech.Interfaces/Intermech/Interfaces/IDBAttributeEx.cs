
// Type: Intermech.Interfaces.IDBAttributeEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public interface IDBAttributeEx
    {
      /// <summary>
      /// Количество значений в списке значений атрибута. Возвращает 0 если осталось одно пустое значение.
      /// </summary>
      int ValuesCount { get; }

      /// <summary>
      /// Добавляет значение в список значений атрибута (для miltivalued атрибутов) и
      /// возвращает номер добавленного значения в списке. Если newValue != null, то оно
      /// записывается в качестве значения атрибута. Если у атрибута есть одно пустое значение, то вместо добавления нового значение метод возвращает индекс пустого значение.
      /// </summary>
      /// <param name="newValue">Номер добавленного значения</param>
      int AddValue(object newValue);

      /// <summary>
      /// Удаляет текущее значение из списка значений атрибута. Метод можно применить и к последнему значению - оно будет обнулено вместо удаления.
      /// </summary>
      int DeleteValue();

      /// <summary>
      /// Возвращает true, если атрибут содержит одно пустое значение.
      /// </summary>
      bool IsNull { get; }

      /// <summary>
      /// Индекс текущего значения в списке значений. Отличается от обычного индекса тем, что если первое (с индексом 0) значение атрибута пустое, то возвращает -1. И не даёт на него спозиционироваться.
      /// </summary>
      int Index { get; set; }
    }
}
