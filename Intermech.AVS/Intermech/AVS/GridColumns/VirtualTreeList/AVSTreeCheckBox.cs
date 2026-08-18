// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.AVSTreeCheckBox
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

public class AVSTreeCheckBox : CheckBox
{
  protected override void OnLocationChanged(EventArgs e) => base.OnLocationChanged(e);

  protected override void OnCheckedChanged(EventArgs e) => base.OnCheckedChanged(e);

  protected override void OnCheckStateChanged(EventArgs e) => base.OnCheckStateChanged(e);
}
