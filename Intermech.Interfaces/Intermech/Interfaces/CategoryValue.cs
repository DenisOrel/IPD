
// Type: Intermech.Interfaces.CategoryValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения значений Тип категории + Идентификатор объекта данной категории + Тип действия.
    /// Используется в кэше прав доступа, журнале регистрации доступа и т.п.
    /// </summary>
    [Serializable]
    public class CategoryValue
    {
      /// <summary>Идентификатор типа категории</summary>
      public int CategoryType;
      /// <summary>Идентификатор категории</summary>
      public long CategoryID;
      /// <summary>Тип действия</summary>
      public ActionType ActionID;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="aCategoryType">Идентификатор типа категории</param>
      /// <param name="aCategoryID">Идентификатор категории</param>
      /// <param name="anActionID">Тип действия</param>
      public CategoryValue(int aCategoryType, long aCategoryID, ActionType anActionID)
      {
        this.CategoryType = aCategoryType;
        this.CategoryID = aCategoryID;
        this.ActionID = anActionID;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return (int) ((ActionType) (this.CategoryType ^ (int) (this.CategoryID >> 32 /*0x20*/) ^ (int) (this.CategoryID << 32 /*0x20*/ >> 32 /*0x20*/)) ^ this.ActionID + 50);
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        CategoryValue categoryValue = (CategoryValue) obj;
        return this.CategoryType == categoryValue.CategoryType && this.CategoryID == categoryValue.CategoryID && this.ActionID == categoryValue.ActionID;
      }
    }
}
