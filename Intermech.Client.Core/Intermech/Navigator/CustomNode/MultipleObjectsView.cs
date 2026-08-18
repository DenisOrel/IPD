
// Type: Intermech.Navigator.CustomNode.MultipleObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.CustomNode;

/// <summary>Закладка, отображающая содержимое кастомного списка объектов</summary>
internal class MultipleObjectsView : ObjectsViewBase
{
  public override ContentType ViewContentType => ContentType.NonFolders;
}
