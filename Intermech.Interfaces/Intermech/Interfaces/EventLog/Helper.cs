
// Type: Intermech.Interfaces.EventLog.Helper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.Collections;


namespace Intermech.Interfaces.EventLog
{
    /// <summary>
    /// Вспомогательный статический класс для работы с элементами журнала событий
    /// </summary>
    public static class Helper
    {
      /// <summary>
      /// Список названий действий, выполняемых над объектом
      /// (для которых проверяются права доступа и записываются события)
      /// </summary>
      public static Hashtable ActionNames = new Hashtable();

      /// <summary>Добавление наименований действий/событий в таблицу</summary>
      public static void Init()
      {
        Helper.ActionNames.Clear();
        Helper.AddActionName(12, 0L, ActionType.AddLink, LocalizationHolder.rm.GetString("Server_4"));
        Helper.AddActionName(12, 0L, ActionType.DeleteLink, LocalizationHolder.rm.GetString("Server_5"));
        Helper.AddActionName(12, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_6"));
        Helper.AddActionName(12, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_7"));
        Helper.AddActionName(12, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_8"));
        Helper.AddActionName(12, 0L, ActionType.List, LocalizationHolder.rm.GetString("Server_9"));
        Helper.AddActionName(1, 0L, ActionType.Login, LocalizationHolder.rm.GetString("Server_10"));
        Helper.AddActionName(1, 0L, ActionType.Edit, LocalizationHolder.rm.GetString("Server_11"));
        Helper.AddActionName(1, 0L, ActionType.View, LocalizationHolder.rm.GetString("Server_12"));
        Helper.AddActionName(1, 0L, ActionType.Purge, LocalizationHolder.rm.GetString("Server_13"));
        Helper.AddActionName(1, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_14"));
        Helper.AddActionName(1, 0L, ActionType.NextLCStep, LocalizationHolder.rm.GetString("Server_15"));
        Helper.AddActionName(17, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_16"));
        Helper.AddActionName(17, 0L, ActionType.Edit, LocalizationHolder.rm.GetString("Server_17"));
        Helper.AddActionName(17, 0L, ActionType.View, LocalizationHolder.rm.GetString("Server_18"));
        Helper.AddActionName(17, 0L, ActionType.Print, LocalizationHolder.rm.GetString("Server_19"));
        Helper.AddActionName(17, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_20"));
        Helper.AddActionName(17, 0L, ActionType.Purge, LocalizationHolder.rm.GetString("Server_21"));
        Helper.AddActionName(17, 0L, ActionType.TakeOwnership, LocalizationHolder.rm.GetString("Server_22"));
        Helper.AddActionName(17, 0L, ActionType.ChangeBaseVersion, LocalizationHolder.rm.GetString("ChangeDocumentBaseVersion"));
        Helper.AddActionName(17, 0L, ActionType.ChangeAccessLevel, LocalizationHolder.rm.GetString("ChangeDocumentAccessLevel"));
        Helper.AddActionName(17, 0L, ActionType.Remove, LocalizationHolder.rm.GetString("Server_23"));
        Helper.AddActionName(17, 0L, ActionType.GetAccess, LocalizationHolder.rm.GetString("Server_24"));
        Helper.AddActionName(17, 0L, ActionType.SetAccess, LocalizationHolder.rm.GetString("Server_25"));
        Helper.AddActionName(17, 0L, ActionType.NextLCStep, LocalizationHolder.rm.GetString("Server_26"));
        Helper.AddActionName(17, 0L, ActionType.DocRegistry, LocalizationHolder.rm.GetString("Server_117"));
        Helper.AddActionName(18, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_27"));
        Helper.AddActionName(18, 0L, ActionType.Edit, LocalizationHolder.rm.GetString("Server_28"));
        Helper.AddActionName(18, 0L, ActionType.View, LocalizationHolder.rm.GetString("Server_29"));
        Helper.AddActionName(18, 0L, ActionType.Print, LocalizationHolder.rm.GetString("Server_30"));
        Helper.AddActionName(18, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_31"));
        Helper.AddActionName(18, 0L, ActionType.Purge, LocalizationHolder.rm.GetString("Server_32"));
        Helper.AddActionName(18, 0L, ActionType.TakeOwnership, LocalizationHolder.rm.GetString("Server_33"));
        Helper.AddActionName(18, 0L, ActionType.ChangeBaseVersion, LocalizationHolder.rm.GetString("ChangeProjectBaseVersion"));
        Helper.AddActionName(18, 0L, ActionType.Remove, LocalizationHolder.rm.GetString("Server_34"));
        Helper.AddActionName(18, 0L, ActionType.GetAccess, LocalizationHolder.rm.GetString("Server_35"));
        Helper.AddActionName(18, 0L, ActionType.SetAccess, LocalizationHolder.rm.GetString("Server_36"));
        Helper.AddActionName(18, 0L, ActionType.NextLCStep, LocalizationHolder.rm.GetString("Server_37"));
        Helper.AddActionName(7, 0L, ActionType.Edit, LocalizationHolder.rm.GetString("Server_38"));
        Helper.AddActionName(7, 0L, ActionType.View, LocalizationHolder.rm.GetString("Server_39"));
        Helper.AddActionName(7, 0L, ActionType.Purge, LocalizationHolder.rm.GetString("Server_40"));
        Helper.AddActionName(7, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_41"));
        Helper.AddActionName(7, 0L, ActionType.NextLCStep, LocalizationHolder.rm.GetString("Server_42"));
        Helper.AddActionName(3, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_43"));
        Helper.AddActionName(3, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_44"));
        Helper.AddActionName(3, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_45"));
        Helper.AddActionName(3, 0L, ActionType.List, LocalizationHolder.rm.GetString("Server_46"));
        Helper.AddActionName(3, 0L, ActionType.Write, LocalizationHolder.rm.GetString("Server_47"));
        Helper.AddActionName(9, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_48"));
        Helper.AddActionName(9, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_49"));
        Helper.AddActionName(9, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_50"));
        Helper.AddActionName(11, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_51"));
        Helper.AddActionName(11, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_52"));
        Helper.AddActionName(11, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_53"));
        Helper.AddActionName(4, 0L, ActionType.View, LocalizationHolder.rm.GetString("Server_54"));
        Helper.AddActionName(4, 0L, ActionType.List, LocalizationHolder.rm.GetString("Server_55"));
        Helper.AddActionName(4, 0L, ActionType.EditLink, LocalizationHolder.rm.GetString("Server_56"));
        Helper.AddActionName(4, 0L, ActionType.AddLink, LocalizationHolder.rm.GetString("Server_57"));
        Helper.AddActionName(4, 0L, ActionType.DeleteLink, LocalizationHolder.rm.GetString("Server_58"));
        Helper.AddActionName(4, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_59"));
        Helper.AddActionName(4, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_60"));
        Helper.AddActionName(4, 0L, ActionType.Create, LocalizationHolder.rm.GetString("Server_61"));
        Helper.AddActionName(4, 0L, ActionType.CreateChildItem, LocalizationHolder.rm.GetString("Server_62"));
        Helper.AddActionName(10, 0L, ActionType.List, LocalizationHolder.rm.GetString("Server_63"));
        Helper.AddActionName(10, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_64"));
        Helper.AddActionName(10, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_65"));
        Helper.AddActionName(6, 0L, ActionType.List, LocalizationHolder.rm.GetString("Server_66"));
        Helper.AddActionName(6, 0L, ActionType.EditProperties, LocalizationHolder.rm.GetString("Server_67"));
        Helper.AddActionName(6, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("Server_68"));
        Helper.AddActionName(6, 0L, ActionType.EditLink, LocalizationHolder.rm.GetString("Server_69"));
        Helper.AddActionName(6, 0L, ActionType.DeleteLink, LocalizationHolder.rm.GetString("Server_70"));
        Helper.AddActionName(6, 0L, ActionType.AddLink, LocalizationHolder.rm.GetString("Server_71"));
        Helper.AddActionName(23, 0L, ActionType.Restore, LocalizationHolder.rm.GetString("RestoreFromSnapshot"));
        Helper.AddActionName(23, 0L, ActionType.Create, LocalizationHolder.rm.GetString("CreateSnapshot"));
        Helper.AddActionName(26, 0L, ActionType.Create, LocalizationHolder.rm.GetString("ImbaseAttCreate"));
        Helper.AddActionName(26, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("ImbaseAttDel"));
        Helper.AddActionName(26, 0L, ActionType.Edit, LocalizationHolder.rm.GetString("ImbaseAttEdit"));
        Helper.AddActionName(26, 0L, ActionType.SetAccess, LocalizationHolder.rm.GetString("ImbaseAttAccess"));
        Helper.AddActionName(25, 0L, ActionType.Create, LocalizationHolder.rm.GetString("ImbaseRecCreate"));
        Helper.AddActionName(25, 0L, ActionType.Delete, LocalizationHolder.rm.GetString("ImbaseRecDel"));
        Helper.AddActionName(25, 0L, ActionType.Edit, LocalizationHolder.rm.GetString("ImbaseRecEdit"));
        Helper.AddActionName(25, 0L, ActionType.SetAccess, LocalizationHolder.rm.GetString("ImbaseRecAccess"));
      }

      /// <summary>
      /// Добавить наименование действия actType для категории categoryType
      /// </summary>
      public static void AddActionName(
        int categoryType,
        long categoryID,
        ActionType actType,
        string actionName)
      {
        Helper.ActionNames.Add((object) new CategoryValue(categoryType, categoryID, actType), (object) actionName);
      }
    }
}
