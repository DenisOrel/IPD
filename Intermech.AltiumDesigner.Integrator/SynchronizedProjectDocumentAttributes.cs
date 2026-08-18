// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SynchronizedProjectDocumentAttributes
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class SynchronizedProjectDocumentAttributes : SynchronizedDocumentAttributes
{
  private readonly AttributeTable attrTable;

  public SynchronizedProjectDocumentAttributes(SettingsService settingsService)
    : base((IIntegratorSettingsService) settingsService)
  {
    this.attrTable = new AttributeTable(settingsService, AttributesTableKind.ProjectAttributes);
  }

  protected override ICollection<StringKey> GetUserDefinedAttributes()
  {
    ICollection<StringKey> definedAttributes = base.GetUserDefinedAttributes();
    foreach (Tuple<StringKey, StringKey, bool> row in this.attrTable.Rows)
      definedAttributes.Add(row.Item1);
    return definedAttributes;
  }

  protected override ICollection<StringKey> GetVirtualAttributes()
  {
    ICollection<StringKey> virtualAttributes = base.GetVirtualAttributes();
    virtualAttributes.Add((StringKey) "Document type");
    return virtualAttributes;
  }
}
