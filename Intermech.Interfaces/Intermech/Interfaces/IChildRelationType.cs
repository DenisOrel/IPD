
// Type: Intermech.Interfaces.IChildRelationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий допустимый тип связи, а также содержащий список дочерних типов объектов
    /// </summary>
    public interface IChildRelationType : IXMLStoredClass
    {
      /// <summary>ID допустимого типа связи</summary>
      int RelationTypeID { get; set; }

      /// <summary>Список дочерних типов объектов</summary>
      List<ChildObjectType> ChildObjectTypes { get; }

      /// <summary>Вернуть описание дочернего типа по его Guid</summary>
      /// <param name="objTypeID">ID дочернего типа</param>
      /// <returns>Описание дочернего типа или null</returns>
      ChildObjectType this[int objTypeID] { get; }

      /// <summary>
      /// Поиск в текущем описании типа связи наиболее подходящий родительский тип объекта для данного типа
      /// </summary>
      /// <param name="childObjType">Дочерний тип объекта</param>
      /// <returns>Наиболее подходящий родительский тип объекта или дочерний тип</returns>
      int GetNearestBaseParentObjectType(int childObjType);

      /// <summary>
      /// Перегенерировать стартовые значения атрибута "Сортировка" у дочерних типов объектов
      /// </summary>
      void GenerateStartSortingValues();

      /// <summary>Выполнить синхронизацию  с кэшем метаданных</summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
      void SyncMetadata(IUserSession session);
    }
}
