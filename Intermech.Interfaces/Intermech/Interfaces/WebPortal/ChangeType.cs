
// Type: Intermech.Interfaces.WebPortal.ChangeType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Тип изменения публикуемого (передаваемого/принимаемого) объекта
    /// </summary>
    [Serializable]
    public enum ChangeType
    {
      /// <summary>Удаление</summary>
      ctDelete,
      /// <summary>Изменение</summary>
      ctUpdate,
      /// <summary>Добавление</summary>
      ctCreate,
    }
}
