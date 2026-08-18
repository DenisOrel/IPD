// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.SystemFolder
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.PropertyEditors;

public class SystemFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public override bool AddChildEnabled => false;

  public SystemFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) null)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(14, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetSystemSecurity();
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ConfigurationTabPage);
  }

  public override int ExportCategoryValue => 14;

  public override int ListCategoryValue => 14;
}
