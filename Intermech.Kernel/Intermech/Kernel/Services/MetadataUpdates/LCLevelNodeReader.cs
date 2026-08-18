// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LCLevelNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LCLevelNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid levelGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, levelGuid, (IPropertyFactory) new PropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    byte[] propertyValue1 = this.propertyFactory.GetPropertyValue<byte[]>("F_ICON", (byte[]) null);
    List<UpdateScriptAccessRight> propertyValue2 = this.propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", (List<UpdateScriptAccessRight>) null);
    IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(this.GUID, false);
    if (lifecycleLevel == null)
    {
      lifecycleLevel = this.session.GetLifecycleLevel(this.session.GetLifecycleLevelCollection().Create(this.propertyFactory.GetPropertyValue<string>("F_LEVEL_NAME"), this.propertyFactory.GetPropertyValue<string>("F_LITERA"), this.propertyFactory.GetPropertyValue<string>("F_AREA_ID", string.Empty), this.GUID, this.propertyFactory.GetPropertyValue<bool>("F_DEFAULT", false)));
      if (propertyValue1 != null)
        lifecycleLevel.LevelIcon = propertyValue1;
      this.SetAccess(lifecycleLevel as IDBSecurity, propertyValue2, 8, Convert.ToInt64(lifecycleLevel.LevelID));
    }
    else
    {
      lifecycleLevel.LevelName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_LEVEL_NAME", lifecycleLevel.LevelName);
      lifecycleLevel.Litera = this.propertyFactory.GetObligatoryPropertyValue<string>("F_LITERA", lifecycleLevel.Litera);
      if (this.propertyFactory.IsPropertyObligatory("F_ICON"))
        lifecycleLevel.LevelIcon = propertyValue1;
      lifecycleLevel.IsDefaultLevel = this.propertyFactory.GetObligatoryPropertyValue<bool>("F_DEFAULT", lifecycleLevel.IsDefaultLevel);
      (lifecycleLevel as IDBSubjectArea).SubjectAreas = this.propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_ID", (lifecycleLevel as IDBSubjectArea).SubjectAreas);
    }
    categoryID = 8;
    id = (object) lifecycleLevel.LevelID;
  }
}
