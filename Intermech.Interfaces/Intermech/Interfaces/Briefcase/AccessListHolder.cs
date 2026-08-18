
// Type: Intermech.Interfaces.Briefcase.AccessListHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Кэширует таблицы прав доступа к метаданным,
    /// для объектов и связей кэширование не выполняет из-за больших объемов
    /// </summary>
    public class AccessListHolder
    {
      private HybridDictionary alh = new HybridDictionary();

      public void Clear()
      {
        foreach (DictionaryEntry dictionaryEntry in this.alh)
        {
          if (dictionaryEntry.Value != null && dictionaryEntry.Value is DataTable)
            ((DataTable) dictionaryEntry.Value).Clear();
        }
        this.alh.Clear();
      }

      /// <summary>возвращаются только строки с F_PARENT_KEY=0</summary>
      /// <param name="security"></param>
      /// <param name="category"></param>
      /// <param name="id"></param>
      /// <param name="contextObject"></param>
      /// <returns></returns>
      public DataRow[] GetAccessList(
        IUserSession session,
        IDBSecurity security,
        int category,
        object id,
        object contextObject)
      {
        if (category == 1 || category == 2 || category == 5 || category == 10)
        {
          ActionProperties[] actions = (ActionProperties[]) null;
          QuickObjectInfo[] users = (QuickObjectInfo[]) null;
          return security.GetAccessList(out actions, out users).Select("F_PARENT_KEY=0");
        }
        DataRow[] accessList;
        switch (category)
        {
          case 3:
          case 4:
          case 6:
          case 8:
          case 12:
          case 16 /*0x10*/:
            accessList = this.GetAccessDataTable4Category(session, category).Select("F_CATEGORY_ID=" + Convert.ToInt32(id).ToString());
            break;
          case 7:
            accessList = this.GetAccessDataTable4Category(session, category).Select("F_CATEGORY_ID=" + (contextObject != null ? (Convert.ToInt64(contextObject) << 32 /*0x20*/ | (long) Convert.ToInt32(id)).ToString() : Convert.ToInt32(id).ToString()));
            break;
          case 9:
          case 11:
            accessList = this.GetAccessDataTable4Category(session, category).Select("F_CATEGORY_ID=0");
            break;
          default:
            ActionProperties[] actions1 = (ActionProperties[]) null;
            QuickObjectInfo[] users1 = (QuickObjectInfo[]) null;
            accessList = security.GetAccessList(out actions1, out users1).Select("F_PARENT_KEY=0");
            break;
        }
        return accessList;
      }

      /// <summary>Возвращает Datatable для категории</summary>
      /// <param name="category"></param>
      /// <returns></returns>
      private DataTable GetAccessDataTable4Category(IUserSession session, int category)
      {
        DataTable dataTable4Category;
        if (this.alh.Contains((object) category))
        {
          dataTable4Category = this.alh[(object) category] as DataTable;
        }
        else
        {
          dataTable4Category = this.LoadAccessDataTable(session, category);
          if (dataTable4Category != null)
            this.alh[(object) category] = (object) dataTable4Category;
        }
        return dataTable4Category;
      }

      /// <summary>Загружает Datatable для категории</summary>
      /// <param name="category"></param>
      /// <returns></returns>
      private DataTable LoadAccessDataTable(IUserSession session, int category)
      {
        DataTable dataTable = (DataTable) null;
        IServerBriefcase briefcase = session.GetBriefcase();
        if (briefcase != null)
          dataTable = briefcase.GetDatatable(session.SessionGUID, "IMS_CATEGORY_ACCESS", $"F_CATEGORY_TYPE={category.ToString()} AND F_PARENT_KEY=0", string.Empty);
        return dataTable;
      }
    }
}
