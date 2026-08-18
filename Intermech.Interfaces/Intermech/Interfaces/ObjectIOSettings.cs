
// Type: Intermech.Interfaces.ObjectIOSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Класс с настройками исполнения обязанностей</summary>
    [Serializable]
    public class ObjectIOSettings
    {
      public long ID = -1;
      public List<MyElement> IoList = new List<MyElement>();
      public string BeginDate = "";
      public string EndDate = "";
      public RoleProperties CurrentRole = new RoleProperties(0L, "");
      /// <summary>Роли, доступные пользователю</summary>
      /// 
      ///             Осторожнее с этим свойством, его надо менять каждый раз, когда меняется IoList
      ///             Возможно здесь нужен еще рефакторинг :/
      public List<RoleProperties> CommonUsersRoles = new List<RoleProperties>();
      public bool Reload = true;

      public ObjectIOSettings()
      {
      }

      public ObjectIOSettings(
        long id,
        List<MyElement> listIO,
        string beginDate,
        string endDate,
        RoleProperties currentrRole,
        List<RoleProperties> commonUsersRoles,
        bool reload)
      {
        this.ID = id;
        this.IoList.AddRange((IEnumerable<MyElement>) listIO);
        this.BeginDate = beginDate;
        this.EndDate = endDate;
        this.CurrentRole = currentrRole;
        this.CommonUsersRoles = commonUsersRoles;
        this.Reload = reload;
      }

      /// <summary>
      /// Преобразование списка идентификаторв в строку с обозначениями
      /// </summary>
      /// <param name="list">Список идентификаторов</param>
      /// <returns>Строка с обозначениями</returns>
      public string IOCaptions()
      {
        string str = "";
        foreach (MyElement io in this.IoList)
          str = str != "" ? $"{str}, {io.Caption}" : io.Caption;
        return str;
      }

      public bool IsValid()
      {
        return this.IoList.Count != 0 && (!(this.BeginDate != "") || !(this.EndDate != "") || !(Convert.ToDateTime(this.BeginDate) > Convert.ToDateTime(this.EndDate)));
      }
    }
}
