// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseContentXmlReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal class BriefcaseContentXmlReader
{
  public static List<Guid> ReadObjects(string xmlFileName)
  {
    List<Guid> guidList = new List<Guid>();
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(xmlFileName);
    foreach (XmlNode selectNode in xmlDocument.SelectNodes("EXPORTCONTENTDATASET/EXPORTATTRIBUTE"))
    {
      if (Convert.ToInt32(selectNode.SelectSingleNode("F_CATEGORY_ID").InnerText) == 1)
      {
        string innerText = selectNode.SelectSingleNode("F_OBJECT_ID").InnerText;
        if (GuidHelper.IsGuid(innerText))
          guidList.Add(new Guid(innerText));
      }
    }
    return guidList;
  }
}
