
// Type: Intermech.Tools.LaunchActions.SimpleFileLaunchHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Files;
using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Tools.LaunchActions;

public abstract class SimpleFileLaunchHandler : ParameterlessLaunchHandler
{
  public SimpleFileLaunchHandler(Guid id, string applicationName)
    : base(id, applicationName)
  {
  }

  public sealed override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    QuickObjectInfo objectInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectInfo = sessionKeeper.Session.GetObjectInfo(launchParams.ObjectId);
    if (objectInfo.Empty)
      return;
    FileAttributeEditMode? attributeEditMode = ServiceUtils.GetService<IFileAttributeEditorService>((object) ApplicationServices.Container, true).GetFileAttributeEditMode(objectInfo.ObjectTypeID);
    if (!attributeEditMode.HasValue || attributeEditMode.Value != FileAttributeEditMode.Normal)
      return;
    this.DoLaunch(launchParams, objectInfo);
  }

  protected abstract void DoLaunch(LaunchParams launchParams, QuickObjectInfo objectInfo);
}
