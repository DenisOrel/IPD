// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportingImbaseEntity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportingImbaseEntity
{
  public string Caption { get; private set; }

  public long Code { get; private set; }

  public long Link { get; private set; }

  public string Key { get; private set; }

  private void Init(string caption, long code, long link, string key)
  {
    this.Caption = caption;
    this.Code = code;
    this.Link = link;
    this.Key = key;
  }

  public bool IsImbaseEntity
  {
    get => this.Code != -1L && this.Link != 0L || !string.IsNullOrEmpty(this.Key);
  }

  public ImportingImbaseEntity(ImportingObject briefObject)
  {
    AttributeRecord attributeRecord1 = briefObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == MetaDataHelper.GetAttributeTypeID("cad0020f-306c-11d8-b4e9-00304f19f545")));
    AttributeRecord attributeRecord2 = briefObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == MetaDataHelper.GetAttributeTypeID("cad00209-306c-11d8-b4e9-00304f19f545")));
    AttributeRecord attributeRecord3 = briefObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == MetaDataHelper.GetAttributeTypeID("cad00162-306c-11d8-b4e9-00304f19f545")));
    this.Init(briefObject.Object.Caption, attributeRecord1 == null || attributeRecord1.IntegerValue == null ? -1L : (long) attributeRecord1.IntegerValue, attributeRecord2 == null || attributeRecord2.IntegerValue == null ? 0L : (long) attributeRecord2.IntegerValue, attributeRecord3 == null || string.IsNullOrEmpty(Convert.ToString(attributeRecord3.StringValue)) ? (string) null : (string) attributeRecord3.StringValue);
  }

  public ImportingImbaseEntity(IDBObject dbObject)
  {
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"), false);
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad00209-306c-11d8-b4e9-00304f19f545"), false);
    IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(new Guid("cad00162-306c-11d8-b4e9-00304f19f545"), false);
    this.Init(dbObject.NameInMessages, attributeByGuid1 == null || attributeByGuid1.AsInteger < 0L ? -1L : attributeByGuid1.AsInteger, attributeByGuid2 != null ? attributeByGuid2.AsInteger : 0L, attributeByGuid3?.AsString);
  }
}
