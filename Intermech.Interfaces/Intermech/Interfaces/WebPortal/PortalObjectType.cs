
// Type: Intermech.Interfaces.WebPortal.PortalObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Структура, описывающая тип объектов, используемый порталом
    /// </summary>
    [Serializable]
    public class PortalObjectType : ICloneable, IComparable, IComparable<PortalObjectType>
    {
      /// <summary>Идентификатор в базе портала</summary>
      public int ID;
      /// <summary>Идентификатор родительского типа</summary>
      public int ParentID;
      /// <summary>Наименование</summary>
      public string Name;
      /// <summary>Глобальный идентификатор типа</summary>
      public string GUID;
      /// <summary>Иконка</summary>
      public byte[] Icon;
      /// <summary>Атрибуты типа объектов</summary>
      public PortalAttributeType[] Attributes;

      /// <summary>Конструктор</summary>
      public PortalObjectType()
      {
        this.ID = -1;
        this.ParentID = -1;
        this.Name = string.Empty;
        this.GUID = Guid.Empty.ToString();
        this.Icon = (byte[]) null;
      }

      /// <summary>Конструктор</summary>
      /// <param name="id">Идентификатор в базе портала</param>
      /// <param name="parentID">Идентификатор родительского типа</param>
      /// <param name="name">Наименование</param>
      /// <param name="guid">Глобальный идентификатор типа</param>
      /// <param name="icon">Иконка</param>
      public PortalObjectType(int id, int parentID, string name, string guid, byte[] icon)
      {
        this.ID = id;
        this.ParentID = parentID;
        this.Name = name;
        this.GUID = guid;
        this.Icon = icon;
      }

      /// <summary>
      /// Создать вспомогательный класс, загрузить данные из указанной строки (таблица должна быть получена из кэша метаданных)
      /// </summary>
      /// <param name="row">Строка с исходными данными</param>
      public PortalObjectType(DataRow row)
      {
        this.ID = Convert.ToInt32(row["F_OBJECT_TYPE"]);
        this.ParentID = Convert.ToInt32(row["F_PARENT_ID"]);
        this.Name = row["F_OBJ_TYPE_NAME"].ToString();
        this.GUID = row["F_GUID"].ToString();
        this.Icon = (byte[]) row["F_ICON"];
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is PortalObjectType portalObjectType) ? base.Equals(obj) : this.ID == portalObjectType.ID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.ID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => this.Name;

      /// <summary>Создать точную копию</summary>
      /// <returns>Точная копия</returns>
      public object Clone()
      {
        return (object) new PortalObjectType()
        {
          Attributes = this.Attributes,
          GUID = this.GUID,
          Icon = this.Icon,
          ID = this.ID,
          Name = this.Name,
          ParentID = this.ParentID
        };
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as PortalObjectType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(PortalObjectType other)
      {
        return other == null ? 1 : this.Name.CompareTo(other.Name);
      }
    }
}
