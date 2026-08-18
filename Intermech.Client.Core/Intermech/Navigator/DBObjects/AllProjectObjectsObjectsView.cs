
// Type: Intermech.Navigator.DBObjects.AllProjectObjectsObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка, отображающая список всех объектов проекта</summary>
public class AllProjectObjectsObjectsView : ObjectsViewBase
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;

  /// <summary>Название закладки</summary>
  public override string Caption
  {
    [DebuggerStepThrough] get => AllProjectObjectsNode.AllProjectObjectsNodeName;
  }

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (AllProjectObjectsObjectsView._imageIndex >= 0)
        return AllProjectObjectsObjectsView._imageIndex;
      AllProjectObjectsObjectsView._imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgAllProjectObjects");
      return AllProjectObjectsObjectsView._imageIndex;
    }
  }
}
