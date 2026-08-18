
// Type: Intermech.Navigator.EventLog.Helper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.EventLog;

public sealed class Helper
{
  public static void CollectColumns(NodeColumnCollection columns)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_AUDIT_TYPE));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_BEGIN_DATE));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_EVENT_ID));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_EVENT_TYPE));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_NAME));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_USER_ID));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_COMPUTER_NAME));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_RELATION_ID));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_NOTE));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CATEGORY_TYPE));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CATEGORY_ID));
    columns.Add(service.CreateColumn(Consts.ColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_END_DATE));
  }
}
