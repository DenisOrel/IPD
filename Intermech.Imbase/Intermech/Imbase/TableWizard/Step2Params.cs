// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step2Params
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

internal class Step2Params
{
  private Dictionary<Guid, StructureEditorPropGridDescriptor> _descrs = new Dictionary<Guid, StructureEditorPropGridDescriptor>();
  private Dictionary<Guid, int> _IDs = new Dictionary<Guid, int>();

  internal Step2Params(ListView.ListViewItemCollection items)
  {
    foreach (ListViewItem listViewItem in items)
    {
      if (listViewItem.Tag != null)
      {
        StructureEditorPropGridDescriptor tag = listViewItem.Tag as StructureEditorPropGridDescriptor;
        this._descrs.Add(tag.AttributeGuid, tag);
        this._IDs.Add(tag.AttributeGuid, tag.AttributeID);
      }
    }
  }

  internal List<Guid> GUIDs => new List<Guid>((IEnumerable<Guid>) this._descrs.Keys);

  internal StructureEditorPropGridDescriptor GetProperties(Guid g) => this._descrs[g];

  internal int GetID(Guid g) => !this._IDs.ContainsKey(g) ? 0 : this._IDs[g];
}
