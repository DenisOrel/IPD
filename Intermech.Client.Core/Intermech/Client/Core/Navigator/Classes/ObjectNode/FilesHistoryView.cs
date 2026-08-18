
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.FilesHistoryView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using DevExpress.IM.XtraGrid.Views.Grid.ViewInfo;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Objects;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DiskStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>
/// Закладка для отображения истории изменения файлов
/// 26.04.2010 - по требованию г-на Жукова история выводится
/// для всех файлов объекта (не версии! )
/// </summary>
public class FilesHistoryView : UserControl
{
  private const string stateStreamName = "FilesHistoryView";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GridView gvFilesHistory;
  private GridControl gcFilesHistory;
  private ContextMenuStrip cmsSaveFile;
  private ToolStripMenuItem toolStripMenuItem1;

  public FilesHistoryView() => this.InitializeComponent();

  private void LoadHistoryInfo(long id, long blobID)
  {
    DataTable dataTable1 = new DataTable();
    this.CreateColumns(dataTable1);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      object[] columns = new object[1]{ (object) -2 };
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad00000-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) "Intermech Document Server", LogicalOperators.NONE, 0)
      };
      foreach (DataRow row1 in (InternalDataCollectionBase) session.ObjectsSelect(new Guid("cad00014-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(conditions, columns)).Rows)
      {
        try
        {
          long int64 = Convert.ToInt64(row1[0]);
          IBlobStorageObject blobStorageObject = session.GetObject(int64) as IBlobStorageObject;
          DataTable dataTable2 = blobID == 0L ? blobStorageObject.GetObjectHistory(id) : blobStorageObject.GetFileHistory(blobID, id);
          if (dataTable2 != null)
          {
            if (dataTable2.Rows.Count > 0)
            {
              foreach (DataRow row2 in (InternalDataCollectionBase) dataTable2.Rows)
              {
                List<object> objectList = new List<object>(row2.ItemArray.Length);
                for (int index = 0; index < row2.ItemArray.Length; ++index)
                {
                  object obj = row2.ItemArray[index];
                  objectList.Add(obj);
                  switch (index)
                  {
                    case 8:
                      objectList.Add((object) EnumDescConverter.GetEnumDescription((Enum) (ArcMethods) Convert.ToInt32(obj)));
                      break;
                    case 9:
                    case 10:
                      objectList.Add(Convert.ToInt32(obj) == 0 ? (object) LocalizationHolder.rm.GetString("Client.Core_1321") : (object) LocalizationHolder.rm.GetString("Client.Core_1322"));
                      break;
                  }
                }
                IDBObject dbObject = session.GetObject(int64, false);
                objectList.Add((object) int64);
                if (dbObject != null)
                  objectList.Add((object) dbObject.NameInMessages);
                dataTable1.Rows.Add(objectList.ToArray());
              }
            }
          }
        }
        catch
        {
        }
      }
      this.FillGrid(dataTable1);
    }
  }

  /// <summary>id версии объекта</summary>
  /// <param name="objectID"></param>
  public void LoadObjectHistory(long objectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject == null)
        return;
      this.LoadHistoryInfo(dbObject.ID, 0L);
    }
  }

  /// <summary>заполняем грид</summary>
  /// <param name="table"></param>
  private void FillGrid(DataTable table)
  {
    this.gcFilesHistory.BeginUpdate();
    try
    {
      this.gcFilesHistory.DataSource = (object) table;
      this.gvFilesHistory.Columns[8].VisibleIndex = this.gvFilesHistory.Columns[10].VisibleIndex = this.gvFilesHistory.Columns[12].VisibleIndex = this.gvFilesHistory.Columns[14].VisibleIndex = -1;
    }
    finally
    {
      this.gcFilesHistory.EndUpdate();
    }
  }

  private void CreateColumns(DataTable historyTable)
  {
    if (historyTable.Columns.Count != 0)
      return;
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1323"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1324"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1325"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1326"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1327"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1328"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1329"));
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1330"));
    historyTable.Columns.Add();
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1331"));
    historyTable.Columns.Add();
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1332"));
    historyTable.Columns.Add();
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1333"));
    historyTable.Columns.Add();
    historyTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1334"));
  }

  private void gcFilesHistory_MouseDown(object sender, MouseEventArgs e)
  {
    GridHitInfo gridHitInfo = this.gvFilesHistory.CalcHitInfo(e.Location);
    if (e.Button != MouseButtons.Right || !gridHitInfo.InRow || this.gvFilesHistory.IsGroupRow(gridHitInfo.RowHandle))
      return;
    this.gvFilesHistory.FocusedRowHandle = gridHitInfo.RowHandle;
    this.cmsSaveFile.Show((Control) this.gcFilesHistory, gridHitInfo.HitPoint);
  }

  /// <summary>сохранить выбранный файл</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void toolStripMenuItem1_Click(object sender, EventArgs e)
  {
    DataRow dataRow = this.gvFilesHistory.GetDataRow(this.gvFilesHistory.FocusedRowHandle);
    long int64_1 = Convert.ToInt64(dataRow[0]);
    long int64_2 = Convert.ToInt64(dataRow[1]);
    int int32_1 = Convert.ToInt32(dataRow[2]);
    string fileName = Convert.ToString(dataRow[3]);
    long int64_3 = Convert.ToInt64(dataRow[4]);
    long int64_4 = Convert.ToInt64(dataRow[5]);
    ArcMethods int32_2 = (ArcMethods) Convert.ToInt32(dataRow[8]);
    long int64_5 = Convert.ToInt64(dataRow[14]);
    FileHistoryNodeID fileInfo = new FileHistoryNodeID(int64_2, int32_1, int64_1, int64_4, int64_3, fileName, int32_2, int64_5);
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.RestoreDirectory = true;
    saveFileDialog.FileName = string.IsNullOrEmpty(fileInfo.FileName) ? $"{fileInfo.FileID}_{fileInfo.HistoryID}.blb" : fileInfo.FileName;
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream aDestStream;
    try
    {
      aDestStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
    }
    catch
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_945") + saveFileDialog.FileName, MessageDialogs.msgError);
      return;
    }
    FileProcReader blobProcessor = new FileProcReader(fileInfo, AttributableElements.Object, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, new BlobProcCustomClass.ThreadFinishEventHandler(FilesHistoryView.DownloadFinished));
    ((IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) new BlobProcessorTask(string.Format(LocalizationHolder.rm.GetString("Client.Core_1335"), (object) saveFileDialog.FileName), (BlobProcCustomClass) blobProcessor));
    blobProcessor.ReadDataThread(true);
  }

  public static void DownloadFinished(
    BlobProcCustomClass sender,
    bool result,
    object message,
    Exception exception,
    BlobInformation bi)
  {
    object obj = (object) null;
    if (obj == null)
      return;
    if (!result)
      return;
    try
    {
      File.SetLastWriteTime(obj.ToString(), bi.ModifyDate);
    }
    catch
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_947") + bi.ModifyDate.ToString((IFormatProvider) CultureInfo.InvariantCulture) + LocalizationHolder.rm.GetString("Client.Core_948") + obj, MessageDialogs.msgError);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilesHistoryView));
    this.gvFilesHistory = new GridView();
    this.gcFilesHistory = new GridControl();
    this.cmsSaveFile = new ContextMenuStrip(this.components);
    this.toolStripMenuItem1 = new ToolStripMenuItem();
    this.gvFilesHistory.BeginInit();
    this.gcFilesHistory.BeginInit();
    this.cmsSaveFile.SuspendLayout();
    this.SuspendLayout();
    this.gvFilesHistory.GridControl = this.gcFilesHistory;
    componentResourceManager.ApplyResources((object) this.gvFilesHistory, "gvFilesHistory");
    this.gvFilesHistory.Name = "gvFilesHistory";
    this.gvFilesHistory.OptionsBehavior.Editable = false;
    this.gvFilesHistory.OptionsMenu.EnableColumnMenu = false;
    this.gvFilesHistory.OptionsMenu.EnableFooterMenu = false;
    this.gvFilesHistory.OptionsMenu.EnableGroupPanelMenu = false;
    this.gvFilesHistory.OptionsView.ShowIndicator = false;
    componentResourceManager.ApplyResources((object) this.gcFilesHistory, "gcFilesHistory");
    this.gcFilesHistory.EmbeddedNavigator.Name = "";
    this.gcFilesHistory.MainView = (BaseView) this.gvFilesHistory;
    this.gcFilesHistory.Name = "gcFilesHistory";
    this.gcFilesHistory.MouseDown += new MouseEventHandler(this.gcFilesHistory_MouseDown);
    this.cmsSaveFile.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.toolStripMenuItem1
    });
    this.cmsSaveFile.Name = "cmsSaveFile";
    componentResourceManager.ApplyResources((object) this.cmsSaveFile, "cmsSaveFile");
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gcFilesHistory);
    this.Name = nameof (FilesHistoryView);
    this.gvFilesHistory.EndInit();
    this.gcFilesHistory.EndInit();
    this.cmsSaveFile.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
