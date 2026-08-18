
// Type: Intermech.Navigator.DBObjects.VersionRulesViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер вьюшки для фильтрации состава объектов</summary>
internal class VersionRulesViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    int objectType1 = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectType;
    IDBObjectTypeInfo objectType2 = service.GetObjectType(new Guid("cad001b3-306c-11d8-b4e9-00304f19f545"), true);
    IDBObjectTypeInfo objectType3 = service.GetObjectType(new Guid("cad001b5-306c-11d8-b4e9-00304f19f545"), true);
    IDBObjectTypeInfo objectType4 = service.GetObjectType(new Guid("cad001b4-306c-11d8-b4e9-00304f19f545"), true);
    int objectType5 = objectType2.ObjectType;
    int objectType6 = objectType3.ObjectType;
    int objectType7 = objectType4.ObjectType;
    if (objectType1 == objectType5 || objectType1 == objectType6 || objectType1 == objectType7)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("VersionRulesView", new ViewInfo(0, 762, typeof (VersionRulesView)));
    return views;
  }
}
