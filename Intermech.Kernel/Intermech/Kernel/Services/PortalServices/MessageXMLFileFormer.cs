// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.MessageXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal class MessageXMLFileFormer(
  IUserSession session,
  ExtendedTransferedObject unit,
  IBackupWriter writer,
  RemoteData data) : CustomXMLFileFormer<RemoteData>(session, unit, writer, data)
{
  protected override void WriteRootNode(XmlDocument xmlDocument, XmlNode xmlRootNode)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement(PortalConsts.XmlNodeSysAttribute);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_PARAMS", this.data.Data);
    xmlRootNode.AppendChild(element);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_MESSAGE", this.data.RemoteMessage.Message);
    xmlRootNode.AppendChild(element);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_ADD_DATA", this.data.RemoteMessage.AdditionalData);
    xmlRootNode.AppendChild(element);
    xmlDocument.AppendChild(xmlRootNode);
  }
}
