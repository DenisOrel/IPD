// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.ProcessXMLFileFormer
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Services.PortalServices;
using System.Xml;

#nullable disable
namespace Intermech.Workflow.Server;

internal sealed class ProcessXMLFileFormer(
  IUserSession session,
  ExtendedTransferedObject unit,
  IBackupWriter writer,
  Attributes4ProcessTag tag) : XMLFileFormer(session, unit, writer, (Attributes4Tag) tag)
{
  public static ExtendedTransferedObject[] Pack(
    CustomPublishDataInfo processInfo,
    IUserSession session,
    IBackupWriter writer)
  {
    ExtendedTransferedObject unit = new ExtendedTransferedObject(ChangeType.ctCreate, TransferedObjectCategory.AutoTransfer);
    new ProcessXMLFileFormer(session, unit, writer, new Attributes4ProcessTag(processInfo.Data)).SaveAttributes();
    return new ExtendedTransferedObject[1]{ unit };
  }

  protected override void WriteRootNode(XmlDocument xmlDocument, XmlNode xmlRootNode)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement(PortalConsts.XmlNodeSysAttribute);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_PARAMS", ((Attributes4ProcessTag) this.tag).Data);
    xmlRootNode.AppendChild(element);
  }
}
