// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpertExport
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

public class ExpertExport : Form
{
  private SortedList<string, ExpObjInfo> expObjList = new SortedList<string, ExpObjInfo>();
  private string ExportPath = "";
  public SortedDictionary<int, GuidAndName> attrTypes = new SortedDictionary<int, GuidAndName>();
  public SortedDictionary<int, GuidAndName> objTypes = new SortedDictionary<int, GuidAndName>();
  public Dictionary<int, GuidAndName> relTypes = new Dictionary<int, GuidAndName>();
  public Dictionary<long, GuidAndName> objIdents = new Dictionary<long, GuidAndName>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button btnClose;
  private Panel panel2;
  private ListBox lb;
  private Label label1;
  private Button btnSelDirectory;
  private TextBox tbExportFolder;
  private Button btnAddObjects;
  private Button btnDelObjects;
  private TreeView tv;
  private FolderBrowserDialog fbd;
  private Button btnStart;
  private ImageList IL;

  public ExpertExport() => this.InitializeComponent();

  public void Execute()
  {
    int num = (int) this.ShowDialog();
  }

  private void btnAddObjects_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_629"), LocalizationHolder.rm.GetString("Expert.Editor_630"), ExpertConsts.Consts.objDocScript, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long num in numArray)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num);
        if (!objectInfo.Empty)
        {
          ExpObjInfo expObjInfo = new ExpObjInfo(num);
          if (!this.expObjList.ContainsKey(objectInfo.Caption))
            this.expObjList[objectInfo.Caption] = expObjInfo;
        }
      }
    }
    this.lb.BeginUpdate();
    try
    {
      this.lb.Items.Clear();
      foreach (string key in this.expObjList.Keys.ToList<string>())
        this.lb.Items.Add((object) $"[{Convert.ToString(this.expObjList[key].objID)}] {key}");
    }
    finally
    {
      this.lb.EndUpdate();
    }
    this.FillExpObjInfos();
    this.UpdateTree();
  }

  private void btnSelDirectory_Click(object sender, EventArgs e)
  {
    if (this.fbd.ShowDialog() != DialogResult.OK)
      return;
    this.ExportPath = this.fbd.SelectedPath;
    if (!this.ExportPath.EndsWith("\\"))
      this.ExportPath += "\\";
    this.tbExportFolder.Text = this.ExportPath;
  }

  private void FillExpObjInfos()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
      List<string> list = this.expObjList.Keys.ToList<string>();
      for (int index = 0; index < list.Count; ++index)
      {
        ExpObjInfo expObj = this.expObjList[list[index]];
        customService.FillExpObjInfo(ref expObj, sessionKeeper.Session.SessionGUID);
        this.expObjList[list[index]] = expObj;
      }
    }
  }

  private void btnDelObjects_Click(object sender, EventArgs e)
  {
    this.lb.BeginUpdate();
    try
    {
      this.expObjList.Keys.ToList<string>();
      for (int index = this.lb.SelectedIndices.Count - 1; index >= 0; --index)
      {
        int selectedIndex = this.lb.SelectedIndices[index];
        string key = Convert.ToString(this.lb.Items[selectedIndex]);
        this.lb.Items.RemoveAt(selectedIndex);
        this.expObjList.Remove(key);
      }
    }
    finally
    {
      this.lb.EndUpdate();
    }
    this.UpdateTree();
  }

  private void UpdateTree()
  {
    this.CollectEverything();
    this.tv.Nodes.Clear();
    TreeNode node1 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_636"));
    node1.ImageIndex = 0;
    this.tv.Nodes.Add(node1);
    foreach (int key in this.attrTypes.Keys.ToList<int>())
    {
      GuidAndName attrType = this.attrTypes[key];
      TreeNode node2 = new TreeNode($"[{Convert.ToString(key)}] {attrType.Name}")
      {
        ImageIndex = 1
      };
      node2.SelectedImageIndex = node2.ImageIndex;
      node1.Nodes.Add(node2);
    }
    TreeNode node3 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_637"));
    node3.ImageIndex = 0;
    this.tv.Nodes.Add(node3);
    foreach (int key in this.objTypes.Keys.ToList<int>())
    {
      GuidAndName objType = this.objTypes[key];
      TreeNode node4 = new TreeNode($"[{Convert.ToString(key)}] {objType.Name}")
      {
        ImageIndex = 2
      };
      node4.SelectedImageIndex = node4.ImageIndex;
      node3.Nodes.Add(node4);
    }
    TreeNode node5 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_638"));
    node5.ImageIndex = 0;
    this.tv.Nodes.Add(node5);
    foreach (int key in this.relTypes.Keys.ToList<int>())
    {
      GuidAndName relType = this.relTypes[key];
      TreeNode node6 = new TreeNode($"[{Convert.ToString(key)}] {relType.Name}")
      {
        ImageIndex = 3
      };
      node6.SelectedImageIndex = node6.ImageIndex;
      node5.Nodes.Add(node6);
    }
    TreeNode node7 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_639"));
    node7.ImageIndex = 0;
    this.tv.Nodes.Add(node7);
    foreach (long key in this.objIdents.Keys.ToList<long>())
    {
      GuidAndName objIdent = this.objIdents[key];
      TreeNode node8 = new TreeNode($"[{Convert.ToString(key)}] {objIdent.Name}")
      {
        ImageIndex = 4
      };
      node8.SelectedImageIndex = node8.ImageIndex;
      node7.Nodes.Add(node8);
    }
    this.tv.ExpandAll();
  }

  private void CollectEverything()
  {
    this.attrTypes.Clear();
    this.objTypes.Clear();
    this.relTypes.Clear();
    this.objIdents.Clear();
    foreach (string key1 in this.expObjList.Keys.ToList<string>())
    {
      ExpObjInfo expObj = this.expObjList[key1];
      foreach (int key2 in expObj.attrTypes.Keys.ToList<int>())
      {
        if (!this.attrTypes.ContainsKey(key2))
        {
          GuidAndName attrType = expObj.attrTypes[key2];
          this.attrTypes.Add(key2, attrType);
        }
      }
      foreach (int key3 in expObj.objTypes.Keys.ToList<int>())
      {
        if (!this.objTypes.ContainsKey(key3))
        {
          GuidAndName objType = expObj.objTypes[key3];
          this.objTypes.Add(key3, objType);
        }
      }
      foreach (int key4 in expObj.relTypes.Keys.ToList<int>())
      {
        if (!this.relTypes.ContainsKey(key4))
        {
          GuidAndName relType = expObj.relTypes[key4];
          this.relTypes.Add(key4, relType);
        }
      }
      foreach (long key5 in expObj.objIdents.Keys.ToList<long>())
      {
        if (!this.objIdents.ContainsKey(key5))
        {
          GuidAndName objIdent = expObj.objIdents[key5];
          this.objIdents.Add(key5, objIdent);
        }
      }
    }
  }

  private void ExportTemplates()
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ExpObjInfo expObjInfo in (IEnumerable<ExpObjInfo>) this.expObjList.Values)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(expObjInfo.templateID, false);
        if (dbObject == null)
        {
          int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_633") + LocalizationHolder.rm.GetString("Expert.Editor_634"), (object) expObjInfo.templateID), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK);
        }
        else
        {
          string caption = dbObject.Caption;
          expObjInfo.templateName = caption;
          if (!longList.Contains(expObjInfo.templateID))
          {
            longList.Add(expObjInfo.templateID);
            try
            {
              if (dbObject.GetAttributeByID(ExpertConsts.Consts.attrAttrFile) is IBlobReader attributeById)
              {
                BlobInformation blobInformation = attributeById.OpenBlob(0);
                try
                {
                  Stream stream = ZlibHelper.UnpackBuffer(attributeById.ReadDataBlock((int) blobInformation.RealFileSize));
                  FileStream destination = new FileStream($"{this.ExportPath}{caption}.imdx", FileMode.Create);
                  stream.CopyTo((Stream) destination, 4096 /*0x1000*/);
                  stream.Flush();
                  destination.Flush();
                  destination.Close();
                }
                finally
                {
                  attributeById.CloseBlob();
                }
              }
            }
            catch (Exception ex)
            {
              int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_633") + ex.Message, (object) caption), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK);
            }
          }
        }
      }
    }
  }

  private void btnStart_Click(object sender, EventArgs e)
  {
    if (this.ExportPath == "")
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_635"), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK);
    }
    else
    {
      this.ExportTemplates();
      this.ExportScripts();
      this.ExportAttrTables();
      this.ExportPossibleVals();
      this.ExportObjTables();
      this.ExportMasterTable();
      this.ExportScriptsTable();
      this.ExportRelsTable();
      this.ExportObjLinks();
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_646"), LocalizationHolder.rm.GetString("Expert.Editor_10"), MessageBoxButtons.OK);
    }
  }

  private void ExportScripts()
  {
    foreach (ExpObjInfo expObjInfo in (IEnumerable<ExpObjInfo>) this.expObjList.Values)
    {
      if (expObjInfo.zippedScript != null)
      {
        XmlDocument xmlDocument = this.Unpack(expObjInfo.zippedScript);
        using (FileStream w1 = new FileStream($"{this.ExportPath}{expObjInfo.scriptName}.script", FileMode.Create))
        {
          using (XmlTextWriter w2 = new XmlTextWriter((Stream) w1, Encoding.UTF8))
          {
            w2.Formatting = Formatting.Indented;
            xmlDocument.WriteTo((XmlWriter) w2);
          }
        }
      }
    }
  }

  private XmlDocument Unpack(byte[] zipScr)
  {
    InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream) new MemoryStream(zipScr));
    byte[] buffer = new byte[4096 /*0x1000*/];
    MemoryStream inStream = new MemoryStream();
    while (true)
    {
      int count = inflaterInputStream.Read(buffer, 0, 4096 /*0x1000*/);
      if (count > 0)
        inStream.Write(buffer, 0, count);
      else
        break;
    }
    inStream.Position = 0L;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load((Stream) inStream);
    return xmlDocument;
  }

  private void ExportAttrTables()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
      DataTable dataTable = (DataTable) null;
      SortedDictionary<int, GuidAndName> attrTypes = this.attrTypes;
      ref DataTable local = ref dataTable;
      customService.GetAttrTypesTable(attrTypes, out local).WriteXml(this.ExportPath + "ATTR_TYPES.XML", XmlWriteMode.WriteSchema);
      dataTable?.WriteXml(this.ExportPath + "GROUPS.XML", XmlWriteMode.WriteSchema);
    }
  }

  private void ExportPossibleVals()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int anAttributeType in this.attrTypes.Keys.ToList<int>())
      {
        DataTable possibleValues = sessionKeeper.Session.GetAttributeType(anAttributeType, false).GetPossibleValues();
        if (possibleValues != null && possibleValues.Rows.Count > 0)
          possibleValues.WriteXml($"{this.ExportPath}{Convert.ToString(anAttributeType)}.VALS", XmlWriteMode.WriteSchema);
      }
    }
  }

  private void ExportObjTables()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).GetObjTypesTable(this.objTypes).WriteXml(this.ExportPath + "OBJ_TYPES.XML", XmlWriteMode.WriteSchema);
  }

  private void ExportMasterTable()
  {
    DataTable dataTable = new DataTable("_MASTER");
    dataTable.Columns.Add("F_SCRIPT_NAME", typeof (string));
    dataTable.Columns.Add("F_SCRIPT_ID", typeof (long));
    dataTable.Columns.Add("F_KIND", typeof (int));
    dataTable.Columns.Add("F_ID", typeof (long));
    dataTable.Columns.Add("F_NAME", typeof (string));
    foreach (string key in this.expObjList.Keys.ToList<string>())
    {
      ExpObjInfo expObj = this.expObjList[key];
      List<int> list1 = expObj.attrTypes.Keys.ToList<int>();
      for (int index = 0; index < list1.Count; ++index)
      {
        GuidAndName attrType = expObj.attrTypes[list1[index]];
        dataTable.Rows.Add((object) key, (object) expObj.objID, (object) 0, (object) list1[index], (object) attrType.Name);
      }
      List<int> list2 = expObj.objTypes.Keys.ToList<int>();
      for (int index = 0; index < list2.Count; ++index)
      {
        GuidAndName objType = expObj.objTypes[list2[index]];
        dataTable.Rows.Add((object) key, (object) expObj.objID, (object) 1, (object) list2[index], (object) objType.Name);
      }
      List<int> list3 = expObj.relTypes.Keys.ToList<int>();
      for (int index = 0; index < list3.Count; ++index)
      {
        GuidAndName relType = expObj.relTypes[list3[index]];
        dataTable.Rows.Add((object) key, (object) expObj.objID, (object) 2, (object) list3[index], (object) relType.Name);
      }
      List<long> list4 = expObj.objIdents.Keys.ToList<long>();
      for (int index = 0; index < list4.Count; ++index)
      {
        GuidAndName objIdent = expObj.objIdents[list4[index]];
        dataTable.Rows.Add((object) key, (object) expObj.objID, (object) 3, (object) list4[index], (object) objIdent.Name);
      }
    }
    dataTable.DefaultView.Sort = "F_NAME ASC";
    dataTable.DefaultView.ToTable().WriteXml(this.ExportPath + "_MASTER.XML", XmlWriteMode.WriteSchema);
  }

  private void ExportScriptsTable()
  {
    DataTable dataTable = new DataTable("_SCRIPTS");
    dataTable.Columns.Add("F_SCRIPT_NAME", typeof (string));
    dataTable.Columns.Add("F_SCRIPT_ID", typeof (long));
    dataTable.Columns.Add("F_TEMPLATE_NAME", typeof (string));
    dataTable.Columns.Add("F_TEMPLATE_TYPEGUID", typeof (string));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ExpObjInfo expObjInfo in (IEnumerable<ExpObjInfo>) this.expObjList.Values)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(expObjInfo.templateID, false);
        if (dbObject == null)
        {
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_633") + LocalizationHolder.rm.GetString("Expert.Editor_634"), (object) expObjInfo.templateID), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK);
        }
        else
        {
          Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(dbObject.ObjectType);
          dataTable.Rows.Add((object) expObjInfo.scriptName, (object) expObjInfo.objID, (object) dbObject.Caption, (object) objectTypeGuid.ToString());
        }
      }
    }
    dataTable.DefaultView.Sort = "F_SCRIPT_NAME ASC";
    dataTable.DefaultView.ToTable().WriteXml(this.ExportPath + "_SCRIPTS.XML", XmlWriteMode.WriteSchema);
  }

  private void ExportRelsTable()
  {
    DataTable dataTable = new DataTable("REL_TYPES");
    dataTable.Columns.Add("F_ID", typeof (int));
    dataTable.Columns.Add("F_GUID", typeof (string));
    dataTable.Columns.Add("F_NAME", typeof (string));
    foreach (int key in this.relTypes.Keys.ToList<int>())
    {
      GuidAndName relType = this.relTypes[key];
      dataTable.Rows.Add((object) key, (object) relType.g.ToString(), (object) relType.Name);
    }
    dataTable.WriteXml(this.ExportPath + "REL_TYPES.XML", XmlWriteMode.WriteSchema);
  }

  private void ExportObjLinks()
  {
    DataTable dataTable = new DataTable("OBJ_LINKS");
    dataTable.Columns.Add("F_ID", typeof (int));
    dataTable.Columns.Add("F_GUID", typeof (string));
    dataTable.Columns.Add("F_NAME", typeof (string));
    foreach (long key in this.objIdents.Keys.ToList<long>())
    {
      GuidAndName objIdent = this.objIdents[key];
      dataTable.Rows.Add((object) key, (object) objIdent.g.ToString(), (object) objIdent.Name);
    }
    dataTable.WriteXml(this.ExportPath + "OBJ_LINKS.XML", XmlWriteMode.WriteSchema);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExpertExport));
    this.panel1 = new Panel();
    this.btnStart = new Button();
    this.btnClose = new Button();
    this.panel2 = new Panel();
    this.btnSelDirectory = new Button();
    this.tbExportFolder = new TextBox();
    this.btnAddObjects = new Button();
    this.btnDelObjects = new Button();
    this.lb = new ListBox();
    this.label1 = new Label();
    this.tv = new TreeView();
    this.IL = new ImageList(this.components);
    this.fbd = new FolderBrowserDialog();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnStart);
    this.panel1.Controls.Add((Control) this.btnClose);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 389);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(700, 30);
    this.panel1.TabIndex = 0;
    this.btnStart.Location = new Point(9, 3);
    this.btnStart.Name = "btnStart";
    this.btnStart.Size = new Size(75, 23);
    this.btnStart.TabIndex = 1;
    this.btnStart.Text = "Экспорт!";
    this.btnStart.UseVisualStyleBackColor = true;
    this.btnStart.Click += new EventHandler(this.btnStart_Click);
    this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClose.DialogResult = DialogResult.Cancel;
    this.btnClose.Location = new Point(617, 4);
    this.btnClose.Name = "btnClose";
    this.btnClose.Size = new Size(75, 23);
    this.btnClose.TabIndex = 0;
    this.btnClose.Text = "Закрыть";
    this.btnClose.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.btnSelDirectory);
    this.panel2.Controls.Add((Control) this.tbExportFolder);
    this.panel2.Controls.Add((Control) this.btnAddObjects);
    this.panel2.Controls.Add((Control) this.btnDelObjects);
    this.panel2.Controls.Add((Control) this.lb);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Dock = DockStyle.Top;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(700, 184);
    this.panel2.TabIndex = 1;
    this.btnSelDirectory.Location = new Point(8, 152);
    this.btnSelDirectory.Name = "btnSelDirectory";
    this.btnSelDirectory.Size = new Size(122, 23);
    this.btnSelDirectory.TabIndex = 2;
    this.btnSelDirectory.Text = "Папка экспорта...";
    this.btnSelDirectory.UseVisualStyleBackColor = true;
    this.btnSelDirectory.Click += new EventHandler(this.btnSelDirectory_Click);
    this.tbExportFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbExportFolder.Location = new Point(136, 154);
    this.tbExportFolder.Name = "tbExportFolder";
    this.tbExportFolder.ReadOnly = true;
    this.tbExportFolder.Size = new Size(556, 20);
    this.tbExportFolder.TabIndex = 4;
    this.btnAddObjects.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnAddObjects.Location = new Point(562, 24);
    this.btnAddObjects.Name = "btnAddObjects";
    this.btnAddObjects.Size = new Size(130, 23);
    this.btnAddObjects.TabIndex = 3;
    this.btnAddObjects.Text = "Добавить объекты...";
    this.btnAddObjects.UseVisualStyleBackColor = true;
    this.btnAddObjects.Click += new EventHandler(this.btnAddObjects_Click);
    this.btnDelObjects.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnDelObjects.Location = new Point(562, 53);
    this.btnDelObjects.Name = "btnDelObjects";
    this.btnDelObjects.Size = new Size(130, 23);
    this.btnDelObjects.TabIndex = 2;
    this.btnDelObjects.Text = "Удалить объекты";
    this.btnDelObjects.UseVisualStyleBackColor = true;
    this.btnDelObjects.Click += new EventHandler(this.btnDelObjects_Click);
    this.lb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lb.FormattingEnabled = true;
    this.lb.Location = new Point(8, 24);
    this.lb.Name = "lb";
    this.lb.SelectionMode = SelectionMode.MultiExtended;
    this.lb.Size = new Size(548, 121);
    this.lb.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 6);
    this.label1.Name = "label1";
    this.label1.Size = new Size(179, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Выбор экспортируемых объектов";
    this.tv.Dock = DockStyle.Fill;
    this.tv.ImageIndex = 0;
    this.tv.ImageList = this.IL;
    this.tv.Location = new Point(0, 184);
    this.tv.Name = "tv";
    this.tv.SelectedImageIndex = 0;
    this.tv.ShowRootLines = false;
    this.tv.Size = new Size(700, 205);
    this.tv.TabIndex = 2;
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Transparent;
    this.IL.Images.SetKeyName(0, "Folder.bmp");
    this.IL.Images.SetKeyName(1, "attr.bmp");
    this.IL.Images.SetKeyName(2, "_objType.bmp");
    this.IL.Images.SetKeyName(3, "link.ico");
    this.IL.Images.SetKeyName(4, "document.bmp");
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnClose;
    this.ClientSize = new Size(700, 419);
    this.Controls.Add((Control) this.tv);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExpertExport);
    this.Text = "Экспорт объектов экспертной системы";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
