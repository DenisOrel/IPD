// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Controls.CustomTreeList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Handler;
using DevExpress.IM.XtraTreeList.ViewInfo;
using System;

#nullable disable
namespace Intermech.AVS.Controls;

public class CustomTreeList : TreeList
{
  private bool focusLost;
  public static bool stop;
  private int endUnboundLoadCount;

  protected override void OnLostFocus(EventArgs e)
  {
    this.focusLost = true;
    base.OnLostFocus(e);
    this.focusLost = false;
  }

  protected override TreeListViewInfo CreateViewInfo()
  {
    return (TreeListViewInfo) new CustomTreeListViewInfo((TreeList) this);
  }

  protected override TreeListHandler CreateHandler()
  {
    return (TreeListHandler) new CustomTreeListHandler((TreeList) this);
  }

  public override void HideEditor()
  {
    if (this.focusLost)
      this.HideEditorCore(false);
    else
      base.HideEditor();
  }

  public override void EndUnboundLoad()
  {
    if (this.endUnboundLoadCount <= 0)
      return;
    --this.endUnboundLoadCount;
    base.EndUnboundLoad();
  }

  public override void BeginUnboundLoad()
  {
    ++this.endUnboundLoadCount;
    base.BeginUnboundLoad();
  }
}
