
// Type: Intermech.Interfaces.CategoryDescriptor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для хранения идентификационной информации Тип категории + Идентификатор объекта данной категории
    /// </summary>
    [Serializable]
    /// <summary>Создать экземпляр структуры</summary>
    /// <param name="categoryType">Идентификатор категории</param>
    /// <param name="categoryID">Идентификатор типа</param>
    public struct CategoryDescriptor(int categoryType, long categoryID)
    {
      /// <summary>Идентификатор категории</summary>
      public int CategoryType = categoryType;
      /// <summary>Идентификатор типа</summary>
      public long CategoryID = categoryID;
    }
}
