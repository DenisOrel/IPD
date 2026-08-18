
// Type: Intermech.Interfaces.IChildObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий дочерний тип объектов для сервиса автоматической сортировки составов
    /// по типам объектов и связей
    /// </summary>
    public interface IChildObjectType : IXMLStoredClass
    {
      /// <summary>ID дочернего типа объекта</summary>
      int ObjectTypeID { get; set; }

      /// <summary>
      /// Стартовое значение атрибута "Сортировка" для группы объектов состава указанного типа
      /// </summary>
      long StartSortingValue { get; set; }
    }
}
