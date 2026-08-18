
// Type: Intermech.Interfaces.MessageDialogs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>Диалоги и константы для общих запросов</summary>
    public class MessageDialogs
    {
      public static readonly string msgQuery = LocalizationHolder.rm.GetString("Client.Core_695");
      public static readonly string msgError = LocalizationHolder.rm.GetString("Client.Core_82");
      public static readonly string msgInformation = LocalizationHolder.rm.GetString("Client.Core_50");
      public static readonly string msgWarning = LocalizationHolder.rm.GetString("Client.Core_132");
      public static readonly string msgConfirmDelete = LocalizationHolder.rm.GetString("Client.Core_87");
      public static readonly string msgConfirmAction = LocalizationHolder.rm.GetString("Client.Core_1071");
      public static readonly string msgReallyDelete = LocalizationHolder.rm.GetString("Client.Core_1072");
      public static readonly string msgReallyDelete0 = LocalizationHolder.rm.GetString("Client.Core_1073");
      public static readonly string msgReallyDeleteObjTypeWithChildren = $"{MessageDialogs.msgReallyDelete0}\n{LocalizationHolder.rm.GetString("Client.Core_1074")}";
      public static readonly string msgReallyDeleteAttribute = LocalizationHolder.rm.GetString("Client.Core_1075");
      public static readonly string msgReallyClearAttribute = LocalizationHolder.rm.GetString("Client.Core_1075c");
      public static readonly string msgReallyDeleteFile = LocalizationHolder.rm.GetString("Client.Core_1076");
      public static readonly string msgReallyDeleteValue = LocalizationHolder.rm.GetString("Client.Core_1077");
      public static readonly string msgReallyExclude0 = LocalizationHolder.rm.GetString("Client.Core_1078");
      public static readonly string msgConfirmInheritance = LocalizationHolder.rm.GetString("Client.Core_1079");
      public static readonly string msgReallySave = LocalizationHolder.rm.GetString("Client.Core_1080");
      public static readonly string msgNeedSave = LocalizationHolder.rm.GetString("Client.Core_1081");
      public static readonly string msgConfirmSave = LocalizationHolder.rm.GetString("Client.Core_1082");
      public static readonly string msgSuccess = LocalizationHolder.rm.GetString("Client.Core_1083");
      public static readonly string msgProcessTerminated = LocalizationHolder.rm.GetString("Client.Core_1084");
      public static readonly string msgErrorWhileAccess = LocalizationHolder.rm.GetString("Client.Core_1085");
    }
}
