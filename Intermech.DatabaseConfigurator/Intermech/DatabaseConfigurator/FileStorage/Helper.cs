// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.Helper
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal sealed class Helper
{
  public static void CollectColumns(NodeColumnCollection columns)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_FILE_ID));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_FILENAME));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_FILESIZE));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_FILEDATE));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_ZIPSIZE));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_NOTE));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECTLINK_ID));
  }
}
