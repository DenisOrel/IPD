
// Type: Intermech.Interfaces.IMSAttributeGroup
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Описание группы атрибутов</summary>
    [Serializable]
    public sealed class IMSAttributeGroup : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSAttributeGroup>,
      IDisplayable
    {
      /// <summary>Идентификатор группы атрибутов</summary>
      private int iD;
      /// <summary>Название группы</summary>
      private string name;
      /// <summary>Примечание</summary>
      private string note;
      /// <summary>Идентификатор предметной области</summary>
      private string areaID;
      /// <summary>Идентификатор языка</summary>
      private string languageID;
      /// <summary>Глобальный идентификатор группы атрибутов</summary>
      private Guid guid;

      /// <summary>Идентификатор группы атрибутов</summary>
      public int ID
      {
        get => this.iD;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ID));
          this.iD = value;
        }
      }

      /// <summary>Название группы</summary>
      public string Name
      {
        get => this.name;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Name));
          this.name = value;
        }
      }

      /// <summary>Примечание</summary>
      public string Note
      {
        get => this.note;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Note));
          this.note = value;
        }
      }

      /// <summary>Идентификатор предметной области</summary>
      public string AreaID
      {
        get => this.areaID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AreaID));
          this.areaID = value;
        }
      }

      /// <summary>Идентификатор языка</summary>
      public string LanguageID
      {
        get => this.languageID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LanguageID));
          this.languageID = value;
        }
      }

      /// <summary>Глобальный идентификатор группы атрибутов</summary>
      public Guid Guid
      {
        get => this.guid;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Guid));
          this.guid = value;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSAttributeGroup imsAttributeGroup) ? base.Equals(obj) : this.ID == imsAttributeGroup.ID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
      /// <returns>32-битный хэш-код экземпляра объекта</returns>
      public override int GetHashCode() => this.ID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"Attribute group: [{this.ID}] {this.Name}";

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.ID = DataSetProcessor.GetInt32Value(row, "F_GROUP_ID", 0);
        this.Name = DataSetProcessor.GetStringValue(row, "F_GROUP_NAME", string.Empty);
        this.Note = DataSetProcessor.GetStringValue(row, "F_NOTE", string.Empty);
        this.AreaID = DataSetProcessor.GetStringValue(row, "F_AREA_ID", string.Empty);
        this.LanguageID = DataSetProcessor.GetStringValue(row, "F_LANGUAGE_ID", string.Empty);
        this.Guid = DataSetProcessor.GetGuidValue(row, "F_GUID", Guid.Empty);
      }

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.ID = 0;
        this.Name = string.Empty;
        this.Note = string.Empty;
        this.AreaID = string.Empty;
        this.LanguageID = string.Empty;
        this.Guid = Guid.Empty;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSAttributeGroup imsAttributeGroup))
          return;
        this.ID = imsAttributeGroup.ID;
        this.Name = imsAttributeGroup.Name;
        this.Note = imsAttributeGroup.Note;
        this.AreaID = imsAttributeGroup.AreaID;
        this.LanguageID = imsAttributeGroup.LanguageID;
        this.Guid = imsAttributeGroup.Guid;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSAttributeGroup Clone() => (IMSAttributeGroup) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSAttributeGroup);

      /// <summary>Сравнить с другим объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSAttributeGroup other)
      {
        return other == null ? 1 : string.Compare(this.Name, other.Name, StringComparison.CurrentCultureIgnoreCase);
      }

      /// <summary>Строка для отображения на экране</summary>
      public string Text => this.Name;
    }
}
