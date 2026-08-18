
// Type: Intermech.PropertyEditors.ProjectsChildrenView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Закладка для отображения проектов.</summary>
[ToolboxItem(false)]
public class ProjectsChildrenView : ChildrenView
{
  /// <summary>Наименование закладки.</summary>
  public override string Caption
  {
    get => LocalizationHolder.rm.GetString("Client_Core_ObjectsType_Projects");
  }

  /// <summary>Идентификатор иконки.</summary>
  public override int ImageIndex
  {
    get
    {
      return !(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service) ? -1 : service.ImageIndex("imgProjects");
    }
  }

  public override ContentType ViewContentType
  {
    get => ContentType.NonFolders;
    set
    {
    }
  }
}
