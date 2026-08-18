// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportAttributesStore
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportAttributesStore : IDisposable
{
  public Hashtable ObjectLinkToAttributeType { get; set; }

  public Hashtable MasterAttrToAttributeType { get; set; }

  public Hashtable SourceAttrToAttributeType { get; set; }

  public Hashtable AttributeFormules { get; set; }

  public ImportAttributesStore()
  {
    this.ObjectLinkToAttributeType = new Hashtable();
    this.MasterAttrToAttributeType = new Hashtable();
    this.SourceAttrToAttributeType = new Hashtable();
    this.AttributeFormules = new Hashtable();
  }

  public void Dispose()
  {
    this.ObjectLinkToAttributeType = (Hashtable) null;
    this.MasterAttrToAttributeType = (Hashtable) null;
    this.SourceAttrToAttributeType = (Hashtable) null;
    this.AttributeFormules = (Hashtable) null;
  }
}
