
// Type: Intermech.Interfaces.IParentObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий родительский тип объектов, а также содержащий список допустимых типов связей
    /// </summary>
    public interface IParentObjectType : IXMLStoredClass
    {
      /// <summary>Int32 родительского типа объектов</summary>
      int ObjectTypeID { get; set; }

      /// <summary>Список допустимых типов связей</summary>
      List<ChildRelationType> ChildRelationTypes { get; }

      /// <summary>
      /// Разрешено ли отображать выборки и классификаторы внутри узлов объектов данных типов
      /// </summary>
      bool EnableSelectionsAndClassifiers { get; set; }

      /// <summary>Вернуть описание допустимого типа связи по его ID</summary>
      /// <param name="relTypeID">ID допустимого типа связи</param>
      /// <returns>Описание допустимого типа связи или null</returns>
      ChildRelationType this[int relTypeID] { get; }

      /// <summary>
      /// Перегенерировать стартовые значения атрибута "Сортировка" у всей коллекции дочерних типов объектов
      /// </summary>
      void GenerateStartSortingValues();

      /// <summary>
      /// Выполнить синхронизацию списка допустимых типов связей с кэшем метаданных
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
      void SyncMetadata(IUserSession session);
    }
}
