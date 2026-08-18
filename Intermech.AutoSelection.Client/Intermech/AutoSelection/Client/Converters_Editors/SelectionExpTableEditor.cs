// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionExpTableEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Forms;
using Intermech.Expert.Table;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionExpTableEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (!(value is eTable[] expTables))
      AutoSelectionExpTableSetup.EditTables(ref expTables);
    if (expTables != null)
      AutoSelectionExpTableSetup.EditTableData(ref expTables);
    return (object) expTables;
  }
}
