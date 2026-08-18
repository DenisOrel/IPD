// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributeGroupNodeReader
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

internal sealed class AttributeGroupNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid groupGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, groupGuid, (IPropertyFactory) new PropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    IDBAttributesGroup attributesGroup = this.session.GetAttributesGroup(this.GUID, false);
    if (attributesGroup == null)
    {
      attributesGroup = this.session.GetAttributesGroup(this.session.GetAttributesGroupCollection().Create(this.propertyFactory.GetPropertyValue<string>("F_GROUP_NAME"), this.propertyFactory.GetPropertyValue<string>("F_NOTE", string.Empty), this.propertyFactory.GetPropertyValue<string>("F_LANGUAGE_ID", string.Empty), this.propertyFactory.GetPropertyValue<string>("F_AREA_ID", string.Empty), this.GUID));
      this.SetAccess(attributesGroup as IDBSecurity, this.propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", new List<UpdateScriptAccessRight>(0)), 12, Convert.ToInt64(attributesGroup.GroupID));
    }
    else
    {
      attributesGroup.GroupName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_GROUP_NAME", attributesGroup.GroupName);
      attributesGroup.Note = this.propertyFactory.GetObligatoryPropertyValue<string>("F_NOTE", attributesGroup.Note);
      (attributesGroup as IDBLanguage).LanguageID = this.propertyFactory.GetObligatoryPropertyValue<string>("F_LANGUAGE_ID", (attributesGroup as IDBLanguage).LanguageID);
      (attributesGroup as IDBSubjectArea).SubjectAreas = this.propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_ID", (attributesGroup as IDBSubjectArea).SubjectAreas);
    }
    categoryID = 12;
    id = (object) attributesGroup.GroupID;
  }
}
