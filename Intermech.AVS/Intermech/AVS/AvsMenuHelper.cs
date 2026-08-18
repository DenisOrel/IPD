// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsMenuHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Common_Dialogs;
using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Snapshots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

internal class AvsMenuHelper(ICommandManager commandManager) : DocumentMenuHelper(commandManager)
{
  /// <summary>Guid панели инструментов "Спецификация"</summary>
  public static Guid SpecificationToolBarGuid = new Guid("5AEC0513-27D1-4cb5-8029-10D37B259B10");
  private bool? existUndo;

  public AVSWindow AvsWindow
  {
    get => this.Form as AVSWindow;
    set => this.Form = (ImDocumentEditorFormBase) value;
  }

  /// <summary>Создать панель инструментов "Спецификация"</summary>
  /// <param name="imageList">Список иконок</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Панель инструментов "Спецификация"</returns>
  public Intermech.Bars.ToolBar CreateSpecificationToolBar(
    ImageList imageList,
    ICommandManager commandManager)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Guid = AvsMenuHelper.SpecificationToolBarGuid;
    toolBar.ImageList = imageList;
    toolBar.Text = "Конструкторские документы";
    toolBar.Flow = ToolBarLayout.Horizontal;
    toolBar.DockLine = 1;
    toolBar.DockOffset = 1;
    this.AddNewButton("AVS.AddNewSpecRow", toolBar, commandManager);
    this.AddNewButton("AVS.AddSpecRow", toolBar, commandManager);
    this.AddNewButton("AVS.AddSpecRowFromImbase", toolBar, commandManager);
    (this.AddNewButton("AVS.AddSpecSection", toolBar, commandManager) as DropDownMenuItem).BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.AvsMenuHelper_BeforePopup);
    this.AddNewButton("AVS.Property", toolBar, commandManager);
    this.AddNewButton("AVS.SelectGridColumns", toolBar, commandManager);
    this.AddNewButton("AVS.OpenInNewWindow", toolBar, commandManager);
    this.AddNewButton("AVS.DeleteRecord", toolBar, commandManager);
    this.AddNewButton("AVS.Sort", toolBar, commandManager);
    this.AddNewButton("AVS.SortRazdel", toolBar, commandManager);
    this.AddNewButton("AVS.RowUp", toolBar, commandManager);
    this.AddNewButton("AVS.RowDown", toolBar, commandManager);
    this.AddNewButton("AVS.NumberPositions", toolBar, commandManager);
    this.AddNewButton("AVS.ClearNumberPositions", toolBar, commandManager);
    this.AddNewButton("AVS.GroupRowsByHeader", toolBar, commandManager);
    this.AddNewButton("AVS.UnGroupRowsByHeader", toolBar, commandManager);
    this.AddNewButton("AVS.RefreshFormatAndSmotri", toolBar, commandManager);
    this.AddNewButton("AVS.ClearSmotri", toolBar, commandManager);
    this.AddNewButton("AVS.RefreshMass", toolBar, commandManager);
    this.AddNewButton("AVS.CheckErrors", toolBar, commandManager);
    this.AddNewButton("AVS.FinishWork", toolBar, commandManager);
    this.AddNewButton("AVS.RowProperties", toolBar, commandManager);
    this.AddNewButton("AVS.SetOccurenceKey", toolBar, commandManager);
    return toolBar;
  }

  private void AvsMenuHelper_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    AvsMenuHelper.CreateAddSpecSectionItems(sender);
  }

  public static void CreateAddSpecSectionItems(object sender)
  {
    if (!(sender is MenuItemBase menuItemBase))
      return;
    menuItemBase.Items.Clear();
    if (AVSPlugin.Instance.ActiveAVSWindow != null)
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      List<SpecificationSectionInfo> documentSections = AVSPlugin.Instance.ActiveAVSWindow.AVSDocument.GetAllowableDocumentSections();
      for (int index = 0; index < documentSections.Count; ++index)
      {
        MenuButtonItem menuButtonItem = new MenuButtonItem(documentSections[index].Caption);
        menuButtonItem.Tag = (object) documentSections[index].SectionID;
        menuButtonItem.CommandName = "AVS.AddSpecSection." + menuButtonItem.Tag.ToString();
        menuItemBase.Items.Add((ToolbarItemBase) menuButtonItem);
        menuButtonItem.Click += new EventHandler(AvsMenuHelper.item_Click);
      }
    }
    if (menuItemBase.Items.Count != 0)
      return;
    MenuButtonItem menuButtonItem1 = new MenuButtonItem("[Нет записей]");
    menuItemBase.Items.Add((ToolbarItemBase) menuButtonItem1);
    menuItemBase.Items[0].Enabled = false;
  }

  public override bool QueryStatus(
    ICommandState commandState,
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    if (!(commandState.CommandName == "Undo"))
      return base.QueryStatus(commandState, context, docControl);
    if (!this.existUndo.HasValue)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.existUndo = new bool?(this.GetSnapshots(this.AvsWindow.AVSDocument.DocumentID, sessionKeeper.Session).Count > 0);
    }
    commandState.Enabled = !this.AvsWindow.ReadOnly && this.existUndo.Value;
    return true;
  }

  public override bool Execute(
    ICommandState commandState,
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    if (!(commandState.CommandName == "Undo"))
      return base.Execute(commandState, context, docControl);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<AvsMenuHelper.SnapshotInfo> snapshots1 = this.GetSnapshots(this.AvsWindow.AVSDocument.DocumentID, sessionKeeper.Session);
      if (snapshots1.Count > 0)
      {
        AvsMenuHelper.SnapshotInfo info1 = SelectUndoSnapshotForm.Execute(snapshots1);
        if (info1 != null)
        {
          if (this.AvsWindow.AVSDocument.IsSpecification)
          {
            foreach (ProductInfo productInfo in this.AvsWindow.AVSDocument.productsInfo)
            {
              List<AvsMenuHelper.SnapshotInfo> snapshots2 = this.GetSnapshots(productInfo.Id, sessionKeeper.Session);
              foreach (AvsMenuHelper.SnapshotInfo info2 in snapshots2)
              {
                if (info2.Note == info1.Note)
                  this.RevertChanges(info2, snapshots2, productInfo.Id, sessionKeeper.Session, false);
              }
            }
          }
          this.RevertChanges(info1, snapshots1, this.AvsWindow.AVSDocument.DocumentID, sessionKeeper.Session, true);
          AVSPlugin.Instance.ReloadSpecification(this.AvsWindow.AVSDocument);
        }
      }
      else
      {
        int num = (int) MessageBox.Show("Нет данных для отмены изменений");
      }
    }
    return true;
  }

  private void RevertChanges(
    AvsMenuHelper.SnapshotInfo info,
    List<AvsMenuHelper.SnapshotInfo> infos,
    long objId,
    IUserSession session,
    bool delete)
  {
    session.GetSnapshot(info.Id)?.SaveToObject(objId);
    if (!delete)
      return;
    this.DeleteSnapshots(info, infos, session);
  }

  private void DeleteSnapshots(
    AvsMenuHelper.SnapshotInfo info,
    List<AvsMenuHelper.SnapshotInfo> infos,
    IUserSession session)
  {
    foreach (AvsMenuHelper.SnapshotInfo info1 in infos)
    {
      session.GetSnapshot(info1.Id)?.Delete(0L);
      if (info1 == info)
        break;
    }
  }

  private List<AvsMenuHelper.SnapshotInfo> GetSnapshots(long verId, IUserSession session)
  {
    List<AvsMenuHelper.SnapshotInfo> snapshots = new List<AvsMenuHelper.SnapshotInfo>();
    try
    {
      IDBSnapshotCollection snapshotCollection = session.GetSnapshotCollection();
      DataTable versionSnapshots = snapshotCollection.GetObjectVersionSnapshots(verId, "F_SNAPSHOT_DATE");
      if (versionSnapshots.Rows.Count == 0)
        versionSnapshots = snapshotCollection.GetObjectVersionSnapshots(-verId, "F_SNAPSHOT_DATE");
      int count = versionSnapshots.Rows.Count;
      foreach (DataRow row in (InternalDataCollectionBase) versionSnapshots.Rows)
      {
        string note = "";
        if (versionSnapshots.Columns.Contains("F_NOTE"))
          note = Convert.ToString(row["F_NOTE"]);
        else if (versionSnapshots.Columns.Contains("F_NAME"))
          note = Convert.ToString(row["F_NAME"]);
        long int64 = Convert.ToInt64(row["F_SNAPSHOT_ID"]);
        DateTime time = Convert.ToDateTime(row["F_SNAPSHOT_DATE"]);
        time = time.ToLocalTime();
        AvsMenuHelper.SnapshotInfo snapshotInfo = new AvsMenuHelper.SnapshotInfo(time, int64, note);
        snapshots.Add(snapshotInfo);
      }
      snapshots.Sort(new Comparison<AvsMenuHelper.SnapshotInfo>(AvsMenuHelper.CompareSnapshotInfo));
    }
    catch
    {
    }
    return snapshots;
  }

  private static int CompareSnapshotInfo(AvsMenuHelper.SnapshotInfo x, AvsMenuHelper.SnapshotInfo y)
  {
    return -x.Time.CompareTo(y.Time);
  }

  public override bool QueryStatus_FormatText(DocumentTreeNode context)
  {
    bool flag = base.QueryStatus_FormatText(context);
    if (flag && AVSPlugin.Instance.ActiveAVSWindow != null)
      flag = AVSPlugin.Instance.ActiveAVSWindow.ViewMode == AVSViewMode.Page;
    return flag;
  }

  private static void item_Click(object sender, EventArgs e)
  {
    if (AVSPlugin.Instance.ActiveAVSWindow == null || sender == null || !(sender is MenuButtonItem) || ((ToolbarItemBase) sender).Tag == null || !(((ToolbarItemBase) sender).Tag is long))
      return;
    AVSPlugin.Instance.ActiveAVSWindow.AddSpecSections(AVSPlugin.Instance.ActiveAVSWindow.GetCommandContext(), new List<long>()
    {
      (long) ((ToolbarItemBase) sender).Tag
    });
  }

  internal class SnapshotInfo
  {
    private DateTime time;
    private string note;
    private long id;

    public SnapshotInfo(DateTime time, long id, string note)
    {
      this.Time = time;
      this.Note = note;
      this.Id = id;
    }

    public DateTime Time
    {
      get => this.time;
      set => this.time = value;
    }

    public string Note
    {
      get => this.note;
      set => this.note = value;
    }

    public long Id
    {
      get => this.id;
      set => this.id = value;
    }

    public override string ToString()
    {
      string str = this.Note;
      int length = str.LastIndexOf('~');
      if (length != -1)
        str = str.Substring(0, length);
      if (str.Length == 0)
        str = this.Time.ToString("dd/MM/yyyy HH:mm");
      return str;
    }
  }
}
