// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PreparedPersistentObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

public sealed class PreparedPersistentObject
{
  public XmlDocument AttributesXML { get; private set; }

  public List<Tuple<int, int, string>> DBBlobs { get; set; }

  public List<Tuple<string, byte[]>> InventedBlobs { get; set; }

  public PreparedPersistentObject(XmlDocument attributesXML, List<Tuple<int, int, string>> dbBlobs)
  {
    this.AttributesXML = attributesXML;
    this.DBBlobs = dbBlobs;
    this.InventedBlobs = new List<Tuple<string, byte[]>>();
  }
}
