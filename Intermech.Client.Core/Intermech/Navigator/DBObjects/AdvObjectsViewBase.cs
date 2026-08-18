
// Type: Intermech.Navigator.DBObjects.AdvObjectsViewBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка, отображающая список определённых объектов</summary>
public class AdvObjectsViewBase : ObjectsViewBase
{
  /// <summary>Заголовок закладки</summary>
  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_275");

  /// <summary>
  /// Возвращает тип элементов навигации, которые зачитываются и отображаются в гриде.
  /// </summary>
  public override ContentType ViewContentType => ContentType.NonFolders;
}
