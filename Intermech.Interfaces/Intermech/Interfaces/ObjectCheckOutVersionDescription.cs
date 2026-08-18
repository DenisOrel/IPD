
// Type: Intermech.Interfaces.ObjectCheckOutVersionDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс-контейнер, в котором хранится описание версии объекта для взятия на изменение
    /// </summary>
    [Serializable]
    public class ObjectCheckOutVersionDescription : ObjectVersionDescription
    {
      /// <summary>Каким образом была получена версия для редактирования</summary>
      public ObjectCheckedOutVersionMode Mode;

      /// <summary>Создать пустой экземпляр класса</summary>
      public ObjectCheckOutVersionDescription()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="_ID">Идентификатор объекта</param>
      /// <param name="_OBJECT_ID">Идентификатор версии объекта</param>
      /// <param name="_OBJECT_TYPE">Идентификатор типа объекта</param>
      /// <param name="_LCSTEP_ID">Шаг ЖЦ</param>
      /// <param name="_OWNER_ID">Владелец объекта</param>
      /// <param name="_CHKOUT_BY">Владелец объекта</param>
      /// <param name="_CAPTION">Заголовок</param>
      /// <param name="_F_VERSION_ID">Номер версии</param>
      /// <param name="_F_MODIFICATION_ID">Номер группы изменений</param>
      /// <param name="_F_BASE_VERSION">Признак базовой версии</param>
      /// <param name="_Options">Опции</param>
      /// <param name="_Mode">Каким образом была получена версия для редактирования</param>
      public ObjectCheckOutVersionDescription(
        long _ID,
        long _OBJECT_ID,
        int _OBJECT_TYPE,
        int _LCSTEP_ID,
        long _OWNER_ID,
        long _CHKOUT_BY,
        string _CAPTION,
        long _F_VERSION_ID,
        long _F_MODIFICATION_ID,
        long _F_BASE_VERSION,
        ObjectVersionDescriptionOptions _Options,
        ObjectCheckedOutVersionMode _Mode)
        : base(_ID, _OBJECT_ID, _OBJECT_TYPE, _LCSTEP_ID, _OWNER_ID, _CHKOUT_BY, _CAPTION, _F_VERSION_ID, _F_MODIFICATION_ID, _F_BASE_VERSION, _Options)
      {
        this.Mode = _Mode;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из строки таблицы
      /// </summary>
      /// <param name="row">Строка таблицы с данными</param>
      public ObjectCheckOutVersionDescription(DataRow row) => this.Assign((object) row);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Источник информации</param>
      public ObjectCheckOutVersionDescription(object source) => this.Assign(source);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Объект-описатель</param>
      public ObjectCheckOutVersionDescription(IDBObject source) => this.Assign((object) source);

      /// <summary>Очистить экземпляр класса</summary>
      public override void Clear()
      {
        base.Clear();
        this.Mode = ObjectCheckedOutVersionMode.None;
      }

      /// <summary>Скопировать информацию из указанного объекта</summary>
      /// <param name="source">Объект-источник</param>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is ObjectCheckOutVersionDescription versionDescription))
          return;
        this.Mode = versionDescription.Mode;
      }
    }
}
