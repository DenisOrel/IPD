// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.IDLinkTranslateService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services;

public sealed class IDLinkTranslateService : LongLifeObject, IIDLinkTranslate
{
  private string[] _systemIDLinks = new string[3]
  {
    "cad001c2-306c-11d8-b4e9-00304f19f545".ToLower(),
    "cad00623-306c-11d8-b4e9-00304f19f545".ToLower(),
    "cad00622-306c-11d8-b4e9-00304f19f545".ToLower()
  };

  public event IsIDLinkEventHandler IsIDLinkEvent;

  public bool IsIDLink(Guid attributeGuid)
  {
    if (Array.IndexOf<string>(this._systemIDLinks, Convert.ToString((object) attributeGuid).ToLower()) >= 0)
      return true;
    IDLinkEventArgs e = new IDLinkEventArgs(attributeGuid);
    if (this.IsIDLinkEvent != null)
      this.IsIDLinkEvent((object) this, e);
    return e.Handled && e.IsIDLink;
  }

  public bool IsIDLink(int attributeID)
  {
    return this.IsIDLink(MetaDataHelper.GetAttributeTypeGuid(attributeID));
  }
}
