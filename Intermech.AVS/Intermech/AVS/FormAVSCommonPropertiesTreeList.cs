// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.FormAVSCommonPropertiesTreeList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraTreeList;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

internal class FormAVSCommonPropertiesTreeList : TreeList
{
  protected override void OnPaint(PaintEventArgs e)
  {
    if (this.StateImageList == null)
      throw new Exception("FormAVSCommonPropertiesTreeList.StateImageList == nuuuuul");
    base.OnPaint(e);
  }
}
