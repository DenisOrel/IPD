
// Type: Intermech.Interfaces.ObjectCheckedOutVersionMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Каким образом была получена версия для редактирования</summary>
    [Serializable]
    public enum ObjectCheckedOutVersionMode
    {
      /// <summary>
      /// Возникла ошибка при получении версии для редактирования
      /// </summary>
      Error = -1, // 0xFFFFFFFF
      /// <summary>Нет информации или исходная версия</summary>
      None = 0,
      /// <summary>Объект можно модифицировать в базе данных</summary>
      InBase = 1,
      /// <summary>Существующая рабочая копия указанной версии объекта</summary>
      ActualCopy = 2,
      /// <summary>Версия объекта была взята на изменение</summary>
      CheckOut = 3,
      /// <summary>Выпущена новая версия для редактирования</summary>
      NewVersion = 4,
      /// <summary>
      /// Родительская версия, на основании которой была выпущена новая версия объекта
      /// </summary>
      Parent = 5,
    }
}
