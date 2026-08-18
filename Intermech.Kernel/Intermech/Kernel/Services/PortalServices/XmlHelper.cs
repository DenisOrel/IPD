// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.XmlHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.IO;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal static class XmlHelper
{
  public static XmlNode ReadMainFile(ITransferedObject unit, string path)
  {
    string str = Path.Combine(path, PortalConsts.AttributesXmlFileName);
    FileInfo fileInfo = new FileInfo(str);
    if (!fileInfo.Exists)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1120"), (object) PortalConsts.AttributesXmlFileName, unit.Category == TransferedObjectCategory.Relation ? (object) LocalizationHolder.rm.GetString("Kernel_1159") : (object) LocalizationHolder.rm.GetString("Kernel_1160")));
    XmlDocument xmlDocument = new XmlDocument();
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      using (FileStream inStream = new FileStream(str, FileMode.Open))
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) imChunkedStream, (Stream) inStream);
      imChunkedStream.Position = 0L;
      xmlDocument.Load((Stream) imChunkedStream);
    }
    XmlNode xmlNode = (XmlNode) null;
    for (int i = 0; i < xmlDocument.ChildNodes.Count; ++i)
    {
      if (xmlDocument.ChildNodes[i].Name == PortalConsts.XmlRootNodeAttributes)
      {
        xmlNode = xmlDocument.ChildNodes[i];
        break;
      }
    }
    return xmlNode != null ? xmlNode : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1121"), (object) fileInfo.FullName));
  }
}
