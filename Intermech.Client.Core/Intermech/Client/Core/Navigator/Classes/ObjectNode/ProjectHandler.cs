
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.ProjectHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>Текущий проект</summary>
internal class ProjectHandler : IAfterObjectCreatorDialogHandler
{
  public bool Handle(
    IDBObject newObject,
    int itemIndex,
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (viewServices == null || !(viewServices.GetService(typeof (ProjectObjectID)) is ProjectObjectID service))
      return false;
    if (!(newObject is IDBProjectObject))
      newObject.ProjectID = service.ProjectID;
    return true;
  }
}
