// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ContextXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal class ContextXMLFileFormer : ObjectXMLFileFormer
{
  private readonly PublishComposition _composition;

  public ContextXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBObject obj,
    Attributes4ObjectTag tag,
    PublishComposition composition)
    : base(session, unit, writer, obj, tag)
  {
    this._composition = composition;
  }

  protected override void GetAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    PreparedPersistentObject prepared)
  {
    this.WriteAdditionalAttributes(session, xmlDocument, xmlRootNode, this.writer);
  }

  protected override void WriteAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    IBackupWriter writer)
  {
    Guid modificationID;
    List<long> objectIDs;
    if (!ContextHelper.GetContextContents(session as UserSession, this.dbObject.ObjectID, out modificationID, out objectIDs))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (long num in objectIDs)
    {
      long objectID = num;
      PublishCompositionObject compositionObject = this._composition.Objects.Find((Predicate<PublishCompositionObject>) (x => x.ObjectID == objectID));
      if (compositionObject != null)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(';');
        stringBuilder.Append(compositionObject.ObjectGuid.ToString());
      }
    }
    XmlNode element = (XmlNode) xmlDocument.CreateElement(PortalConsts.XmlNodeContext);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_MODIFICATION_ID", modificationID.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJECTS", stringBuilder.ToString());
    xmlRootNode.AppendChild(element);
  }
}
