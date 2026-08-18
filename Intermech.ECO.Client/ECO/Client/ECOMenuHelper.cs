// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOMenuHelper
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class ECOMenuHelper(ICommandManager commandManager) : DocumentMenuHelper(commandManager), IDisposable
{
  public static Guid ECOToolBarGuid = new Guid("6AD697C2-B070-4F4A-A0E5-B75A44BFF729");

  public ECOAncestorForm EcoForm
  {
    get => this.Form as ECOAncestorForm;
    set => this.Form = (ImDocumentEditorFormBase) value;
  }

  public Intermech.Bars.ToolBar CreateECOToolBar(
    ImageList imageList,
    ICommandManager commandManager)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Guid = ECOMenuHelper.ECOToolBarGuid;
    toolBar.ImageList = imageList;
    toolBar.Text = "Извещение";
    toolBar.Flow = ToolBarLayout.Horizontal;
    toolBar.DockLine = 1;
    toolBar.DockOffset = 1;
    this.AddNewButton("ECO.AttachToECO", toolBar, commandManager);
    this.AddNewButton("ECO.AttachToECO_ExternalDoc", toolBar, commandManager);
    this.AddNewButton("ECO.DetachFromECO", toolBar, commandManager);
    this.AddNewButton("ECO.ProcChanges", toolBar, commandManager);
    this.AddNewButton("ECO.InsertList", toolBar, commandManager);
    this.AddNewButton("ECO.DeleteList", toolBar, commandManager);
    this.AddNewButton("ECO.ChangeReason", toolBar, commandManager);
    this.AddNewButton("ECO.Card", toolBar, commandManager);
    this.AddNewButton("ECO.Tree", toolBar, commandManager);
    this.AddNewButton("ECO.SpecSymbol", toolBar, commandManager);
    this.AddNewButton("ECO.CopyAllElems", toolBar, commandManager);
    this.AddNewButton("ECO.CopyTable", toolBar, commandManager);
    this.AddNewButton("ECO.PasteElems", toolBar, commandManager);
    this.AddNewButton("ECO.MoveElemUp", toolBar, commandManager);
    this.AddNewButton("ECO.MoveElemDown", toolBar, commandManager);
    this.AddNewButton("ECO.DeleteElem", toolBar, commandManager);
    this.AddNewButton("ECO.SortByDes", toolBar, commandManager);
    this.AddNewButton("ECO.ChangeGoal", toolBar, commandManager);
    this.AddNewButton("ECO.ImgFromObj", toolBar, commandManager);
    this.AddNewButton("ECO.ImgFromFile", toolBar, commandManager);
    this.AddNewButton("ECO.ImgFromClip", toolBar, commandManager);
    this.AddNewButton("ECO.CreateOLE", toolBar, commandManager);
    this.AddNewButton("ECO.LaunchShooter", toolBar, commandManager);
    return toolBar;
  }

  private void AvsMenuHelper_BeforePopup(object sender, MenuPopupEventArgs e)
  {
  }

  public override bool QueryStatus(
    ICommandState commandState,
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    return base.QueryStatus(commandState, context, docControl);
  }

  public override bool Execute(
    ICommandState commandState,
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    return base.Execute(commandState, context, docControl);
  }

  public override bool QueryStatus_FormatText(DocumentTreeNode context)
  {
    int num = base.QueryStatus_FormatText(context) ? 1 : 0;
    if (num == 0)
      return num != 0;
    ECOAncestorForm activeEcoEditorForm = ECOPlugin.plugin.ActiveECOEditorForm;
    return num != 0;
  }

  private static void item_Click(object sender, EventArgs e)
  {
  }

  public new void Dispose()
  {
  }
}
