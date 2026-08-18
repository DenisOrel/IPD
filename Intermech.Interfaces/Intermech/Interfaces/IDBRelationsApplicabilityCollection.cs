
// Type: Intermech.Interfaces.IDBRelationsApplicabilityCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Коллекция допустимых связей объектов данного типа с объектами различных типов
    /// </summary>
    public interface IDBRelationsApplicabilityCollection
    {
      /// <summary>
      /// Возвращает таблицу со списком допустимых применяемостей связями типа relationType
      /// объектов типа objectType в объектах типа inObjectType.
      /// Если relationType меньше 0, то вернется список типов связей, которыми объекты типа objectType применяются в объектах типа inObjectType.
      /// Если objectType   меньше 0, то вернется спиcок всех применяемости всех типов в типе inObjectType.
      /// Если inObjectType меньше 0, то вернется список входимостей объектов типа objectType.
      /// 
      /// если допустимых применяемостей нет, то возвращается DataTable с количеством записей 0
      /// </summary>
      DataTable GetApplicabilitiesList(int relationType, int objectType, int inObjectType);

      /// <summary>
      /// Возвращает объект-описатель допустимости связи типа relationType между объектами
      /// типов objectType и inObjectType. Если таковая применяемость не настроена, то
      /// метод возвращает null.
      /// </summary>
      IDBRelationsApplicability GetApplicability(int relationType, int objectType, int inObjectType);

      /// <summary>
      /// Возвращает строку таблицы IMS_TYPES_APPLICABILITY допустимости связи типа relationType между объектами
      /// типов objectType и inObjectType. Если таковая применяемость не настроена, то
      /// метод возвращает null.
      /// </summary>
      DataRow GetApplicabilityRow(int relationType, int objectType, int inObjectType);

      /// <summary>
      /// Возвращает объект-описатель допустимости связи номер applicabilityID
      /// </summary>
      IDBRelationsApplicability GetApplicability(int applicabilityID);

      /// <summary>
      /// Создает новый контекст связи на основании структуры applicabilityProperties и
      /// возвращает его идентификатор.
      /// </summary>
      int Create(
        RelationsApplicabilityProperties applicabilityProperties);
    }
}
