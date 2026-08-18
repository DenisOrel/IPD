// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.HierarchyDescriptor
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.GTC.Server.Processors;

public class HierarchyDescriptor
{
  public string Caption { get; set; }

  public string GtcId { get; set; }

  public Tuple<string, string>[] FileUrls { get; private set; }

  public List<HierarchyDescriptor> Children { get; internal set; }

  public HierarchyDescriptor()
    : this(string.Empty, string.Empty)
  {
  }

  public HierarchyDescriptor(string aCaption, string aGtcId, Tuple<string, string>[] aFileUrls = null)
  {
    this.Children = new List<HierarchyDescriptor>();
    this.Caption = aCaption;
    this.GtcId = aGtcId;
    this.FileUrls = aFileUrls;
  }
}
