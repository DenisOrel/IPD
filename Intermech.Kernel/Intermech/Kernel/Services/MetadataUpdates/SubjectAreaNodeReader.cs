// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.SubjectAreaNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class SubjectAreaNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid areaGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, areaGuid, (IPropertyFactory) new PropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    IDBSubjectAreaType subjectAreaType = this.session.GetSubjectAreaType(this.GUID, false);
    if (subjectAreaType == null)
    {
      subjectAreaType = this.session.GetSubjectAreaType(this.session.GetSubjectAreaCollection().Create(this.propertyFactory.GetPropertyValue<string>("F_AREA_NAME", string.Empty), this.propertyFactory.GetPropertyValue<string>("F_AREA_NOTE", string.Empty), this.GUID));
    }
    else
    {
      subjectAreaType.AreaName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_NAME", subjectAreaType.AreaName);
      subjectAreaType.Note = this.propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_NOTE", subjectAreaType.Note);
    }
    categoryID = 11;
    id = (object) subjectAreaType.AreaID;
  }
}
