
// Type: Intermech.Interfaces.WebPortal.TaskNotifications
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Список уведомлений на задачу</summary>
    public class TaskNotifications
    {
      /// <summary>Тип задачи для уведомлений</summary>
      private TaskType _type;
      /// <summary>Список уведомлений</summary>
      public List<TaskNotification> Notifications;

      public static TaskNotifications GetNotifications(IUserSession session, TaskType type)
      {
        TaskNotifications notifications = new TaskNotifications(type);
        notifications.Load(session);
        return notifications;
      }

      public TaskNotifications(TaskType type)
      {
        this._type = type;
        this.Notifications = new List<TaskNotification>();
      }

      public static Guid GetAccauntSender(IUserSession session)
      {
        string str = session.Configurations.ReadString(PortalConsts.PortalClientModuleName, "ErrorNotifications", "AccauntSender", string.Empty, DBConfigMode.GlobalOnly);
        return str == string.Empty || !GuidHelper.IsGuid(str) ? Guid.Empty : new Guid(str);
      }

      public static void SetAccauntSender(IUserSession session, Guid accauntGuid)
      {
        session.Configurations.WriteString(PortalConsts.PortalClientModuleName, "ErrorNotifications", "AccauntSender", accauntGuid.ToString(), 0L);
      }

      public void Load(IUserSession session)
      {
        DataTable dataTable = session.Configurations.ReadSection(PortalConsts.PortalClientModuleName, this._type.ToString(), 0L);
        if (this.Notifications.Count > 0)
          this.Notifications.Clear();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          string[] strArray = Convert.ToString(dataTable.Rows[index]["F_VALUE"]).Split('|');
          this.Notifications.Add(new TaskNotification(strArray[0], strArray[1], Convert.ToBoolean(strArray[2])));
        }
      }

      public void Save(IUserSession session)
      {
        if (this.Notifications.Count <= 0)
          return;
        DataTable table = new DataTable();
        table.Columns.Add("F_PARAM_NAME", typeof (string));
        table.Columns.Add("F_VALUE", typeof (string));
        for (int index = 0; index < this.Notifications.Count; ++index)
        {
          DataRow row = table.NewRow();
          row["F_PARAM_NAME"] = (object) index.ToString();
          row["F_VALUE"] = (object) $"{this.Notifications[index].User}|{this.Notifications[index].Email}|{this.Notifications[index].Enable}";
          table.Rows.Add(row);
        }
        table.AcceptChanges();
        session.Configurations.WriteSection(PortalConsts.PortalClientModuleName, this._type.ToString(), table, 0L);
      }
    }
}
