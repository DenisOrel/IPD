
// Type: Intermech.Navigator.DBObjects.SiteNameTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Класс для преобразователя, который работает c узлами</summary>
public class SiteNameTransform : INodeColumnTransform
{
  public Type DataType => typeof (string);

  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    string caption = Convert.ToString(sourceValue);
    if (caption != string.Empty)
      caption = SiteIDHelper.GetCaption((ISitesCacheService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISitesCacheService)), caption);
    return CellValue.GetValue(sourceValue, column, (object) caption);
  }
}
