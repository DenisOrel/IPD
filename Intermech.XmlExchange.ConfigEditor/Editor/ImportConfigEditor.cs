// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Editor.ImportConfigEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.XmlExchange;
using Intermech.Navigator;
using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.List;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Editor;

internal class ImportConfigEditor : IConfigEditor
{
  private readonly TreeView _treeView;
  private TreeNodeCollection _rootNodeCollection;
  private XmlExchangeImportSettings _importSettings;
  private ExportConfigEditor _exportConfigEditor;
  private XmlImportBase _xmlImportSettings;
  private long _idObjectImportConfig;
  private string _pathFile = string.Empty;
  private ConfigEditorHelper _helper;
  private ConfigEditorMoveMenu _moveMenu;
  private ContextMenuStrip _contextMenu;
  private ToolStripMenuItem _addRuleMenuItem;
  private ToolStripMenuItem _addItemMenuItem;
  private ToolStripMenuItem _deleteItemMenuItem;
  private ToolStripMenuItem _changeObjTypeMenuItem;
  private ToolStripMenuItem _createExpCongItemMenuItem;
  private ToolStripMenuItem _addExpConfigMenuItem;

  public ImportConfigEditor(
    TreeView treeView,
    TreeNodeCollection rootNodeCollection,
    ContextMenuStrip contextMenu)
  {
    this._treeView = treeView;
    this._rootNodeCollection = rootNodeCollection;
    this._helper = ConfigEditorHelper.GetHelper();
    this._contextMenu = contextMenu;
    this._moveMenu = new ConfigEditorMoveMenu(this._treeView);
    this.InitializeContextMenu();
  }

  private void LoadConfigData()
  {
    this._importSettings = new XmlExchangeImportSettings();
    if (!this._importSettings.LoadData(this._xmlImportSettings))
    {
      if (MessageBox.Show("Файл конфигурации не содержит данных. Создать новую конфигурацию?", "Конфигурация XML-экспорта", MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;
      this._importSettings = new XmlExchangeImportSettings("XMLImportSettings", out this._xmlImportSettings);
    }
    this.ConfigLoadTreeView();
  }

  private void ConfigLoadTreeView()
  {
    this._rootNodeCollection.Clear();
    if (this._importSettings == null)
      return;
    TreeNode treeNode1 = new TreeNode("Правила поиска объектов")
    {
      Tag = (object) this._importSettings.RulesSearch
    };
    treeNode1.ImageIndex = treeNode1.SelectedImageIndex = this._helper.IconsIndexObjType("cad00156-306c-11d8-b4e9-00304f19f545");
    this._rootNodeCollection.Add(treeNode1);
    this.LoadRuleList(treeNode1, this._importSettings.RulesSearch.Select<XmlExchangeImportRuleSearch, XmlExchangeImportObjectType>((Func<XmlExchangeImportRuleSearch, XmlExchangeImportObjectType>) (a => (XmlExchangeImportObjectType) a)).ToList<XmlExchangeImportObjectType>());
    TreeNode treeNode2 = new TreeNode("Правила импорта объектов")
    {
      Tag = (object) this._importSettings.RulesImport
    };
    treeNode2.ImageIndex = treeNode2.SelectedImageIndex = this._helper.IconsIndexObjType("cadd959b-306c-11d8-b4e9-00304f19f545");
    this._rootNodeCollection.Add(treeNode2);
    this.LoadRuleList(treeNode2, this._importSettings.RulesImport.Select<XmlExchangeImportRuleImport, XmlExchangeImportObjectType>((Func<XmlExchangeImportRuleImport, XmlExchangeImportObjectType>) (a => (XmlExchangeImportObjectType) a)).ToList<XmlExchangeImportObjectType>());
    TreeNode treeNode3 = new TreeNode("Правила создания объектов")
    {
      Tag = (object) this._importSettings.RulesCreate
    };
    treeNode3.ImageIndex = treeNode3.SelectedImageIndex = this._helper.IconsIndexObjType("cad001b4-306c-11d8-b4e9-00304f19f545");
    this._rootNodeCollection.Add(treeNode3);
    this.LoadRuleList(treeNode3, this._importSettings.RulesCreate.Select<XmlExchangeImportRuleCreate, XmlExchangeImportObjectType>((Func<XmlExchangeImportRuleCreate, XmlExchangeImportObjectType>) (a => (XmlExchangeImportObjectType) a)).ToList<XmlExchangeImportObjectType>());
    TreeNode node1 = new TreeNode("Настройки импорта объектов Imbase")
    {
      Tag = (object) this._importSettings.ImbaseImportSettings
    };
    node1.ImageKey = node1.SelectedImageKey = "importImbaseSettings";
    this._rootNodeCollection.Add(node1);
    TreeNode node2 = new TreeNode("Скрипты импорта");
    node2.Tag = (object) this._importSettings.ImportScripts;
    this._rootNodeCollection.Add(node2);
    node2.ImageIndex = node2.SelectedImageIndex = this._helper.IconsIndexObjType("cad0036a-306c-11d8-b4e9-00304f19f545");
    foreach (XmlExchangeImportScript importScript in (System.Collections.Generic.List<XmlExchangeImportScript>) this._importSettings.ImportScripts)
    {
      TreeNode node3 = new TreeNode(importScript.ScriptName)
      {
        Tag = (object) importScript
      };
      node3.ImageIndex = node3.SelectedImageIndex = node2.ImageIndex;
      node2.Nodes.Add(node3);
    }
    TreeNode node4 = new TreeNode("Скрипты событий импорта");
    node4.Tag = (object) this._importSettings.ImportActionsScripts;
    this._rootNodeCollection.Add(node4);
    node4.ImageIndex = node4.SelectedImageIndex = this._helper.IconsIndexObjType("cad0036a-306c-11d8-b4e9-00304f19f545");
    foreach (XmlExchangeImportScript importActionsScript in (System.Collections.Generic.List<XmlExchangeImportScript>) this._importSettings.ImportActionsScripts)
    {
      TreeNode node5 = new TreeNode(importActionsScript.ScriptName)
      {
        Tag = (object) importActionsScript
      };
      node5.ImageIndex = node5.SelectedImageIndex = node4.ImageIndex;
      node4.Nodes.Add(node5);
    }
    TreeNode node6 = new TreeNode("Модули расширения импорта");
    node6.Tag = (object) this._importSettings.ImportExtensions;
    this._rootNodeCollection.Add(node6);
    node6.ImageIndex = node6.SelectedImageIndex = this._helper.IconsIndexObjType("cad0005b-306c-11d8-b4e9-00304f19f545");
    foreach (XmlExchangeImportExtension importExtension in (System.Collections.Generic.List<XmlExchangeImportExtension>) this._importSettings.ImportExtensions)
    {
      TreeNode node7 = new TreeNode(string.IsNullOrEmpty(importExtension.Name) ? importExtension.Guid.ToString() : importExtension.Name)
      {
        Tag = (object) importExtension
      };
      node7.ImageIndex = node7.SelectedImageIndex = node6.ImageIndex;
      node6.Nodes.Add(node7);
    }
    TreeNode treeNode4 = new TreeNode("Сопоставление типов");
    treeNode4.Tag = (object) this._importSettings.ExportSettings;
    this._rootNodeCollection.Add(treeNode4);
    treeNode4.ImageKey = treeNode4.SelectedImageKey = "importMatchingTypes";
    this.InitializeExportConfigEditor(this._importSettings.ExportSettings.ExportSettings, treeNode4);
    this._treeView.SelectedNode = this._treeView.TopNode;
  }

  private void LoadRuleList(TreeNode nodeList, System.Collections.Generic.List<XmlExchangeImportObjectType> ruleList)
  {
    nodeList.Nodes.Clear();
    foreach (XmlExchangeImportObjectType rule in ruleList)
    {
      TreeNode treeNode = new TreeNode(rule.Name);
      treeNode.Tag = (object) rule;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(rule.Guid);
      if (objectType != null)
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(4, objectType.ObjectTypeID);
      }
      else
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(4, 0);
        FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode, FontStyle.Italic, "Тип объекта отсутствует в базе");
      }
      nodeList.Nodes.Add(treeNode);
    }
  }

  public void LoadConfigInObject(long idObjectConfig)
  {
    if (this._importSettings != null && MessageBox.Show("Все внесенные изменения будут удалены! Продолжить?", "Конфигурация XML-импорта", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
      return;
    this._idObjectImportConfig = idObjectConfig;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._idObjectImportConfig);
      if (dbObject == null)
        return;
      this._xmlImportSettings = XmlImportBase.Load(dbObject, MetaDataHelper.GetAttributeTypeID("cad001b2-306c-11d8-b4e9-00304f19f545"), (ILogger) null);
      if (this._xmlImportSettings == null)
      {
        if (MessageBox.Show("Файл конфигурации не содержит данных. Создать новую конфигурацию?", "Конфигурация XML-импорта", MessageBoxButtons.OKCancel) != DialogResult.OK)
          return;
        this._xmlImportSettings = new XmlImportBase();
      }
    }
    this.LoadConfigData();
  }

  public string LoadConfigInFile(string pathFile)
  {
    this._pathFile = pathFile;
    FileInfo fileInfo = new FileInfo(this._pathFile);
    if (!fileInfo.Exists)
      return string.Empty;
    try
    {
      using (FileStream fileStream = fileInfo.OpenRead())
      {
        this._xmlImportSettings = new XmlImportBase();
        if (this._xmlImportSettings.Load(XDocument.Load((Stream) fileStream).Root))
        {
          this.LoadConfigData();
        }
        else
        {
          int num = (int) MessageBox.Show("Данные файла не соответствует формату конфигурации.", "Конфигурация XML-импорта");
          return string.Empty;
        }
      }
    }
    catch (IOException ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
    return fileInfo.Name;
  }

  public void SaveConfigInObject()
  {
    this._importSettings.SaveData();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._idObjectImportConfig, true);
      if (this._xmlImportSettings == null || !dbObject.isParentType(XmlExchangeConsts.Common.ImportSettObjTypeGuid) || dbObject.ReadOnly || !(dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(XmlExchangeConsts.Common.DataAttrTypeGuid), false) is IBlobWriter blobWriter))
        return;
      using (MemoryStream inStream = new MemoryStream())
      {
        try
        {
          XDocument doc = new XDocument();
          XmlConfigEditorExtension.SaveXmlDocument(doc, this._xmlImportSettings, (XElement) null);
          doc.Save((Stream) inStream);
          inStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream())
          {
            ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
            byte[] array = outStream.ToArray();
            BlobInformation blobInfo = new BlobInformation(inStream.Length, outStream.Length, DateTime.Now, "ImportXmlSettings.xml", ArcMethods.ZLibPacked, string.Empty);
            blobWriter.OpenBlob(blobInfo, false);
            blobWriter.WriteDataBlock(array);
          }
        }
        finally
        {
          inStream.Close();
        }
      }
    }
  }

  public bool SaveConfigInFile(string pathFile)
  {
    string path = !string.IsNullOrEmpty(pathFile) ? pathFile : this._pathFile;
    this._importSettings.SaveData();
    XDocument doc = new XDocument();
    XmlConfigEditorExtension.SaveXmlDocument(doc, this._xmlImportSettings, (XElement) null);
    try
    {
      using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
      {
        doc.Save((Stream) fileStream);
        if (this._pathFile != pathFile)
          this._pathFile = path;
      }
    }
    catch (IOException ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
      return false;
    }
    return true;
  }

  public void UpdateTreeView(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null)
      return;
    if (selectedNode.Tag is XmlExchangeImportObjectType tag3)
      selectedNode.Text = tag3.Name;
    else if (selectedNode.Tag is XmlExchangeImportScript tag2)
      selectedNode.Text = tag2.ScriptName;
    else if (selectedNode.Tag is XmlExchangeImportExtension tag1)
    {
      selectedNode.Text = string.IsNullOrEmpty(tag1.Name) ? tag1.Guid.ToString() : tag1.Name;
    }
    else
    {
      if (this._exportConfigEditor == null)
        return;
      this._exportConfigEditor.UpdateTreeView(sender, e);
    }
  }

  public void EnterEditorWindow(object sender, EventArgs e)
  {
    if (this._exportConfigEditor == null)
      return;
    this._exportConfigEditor.EnterEditorWindow(sender, e);
    this._exportConfigEditor.UpdateTreeView(sender, e);
  }

  public void Menu_Opening(object sender, CancelEventArgs e, TreeNode selectedNode)
  {
    if (selectedNode.Tag is XmlExchangeImportXmlExportSettings)
    {
      this._contextMenu.Items.Clear();
      this._contextMenu.Items.Add((ToolStripItem) this._createExpCongItemMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this._addExpConfigMenuItem);
    }
    else if (!selectedNode.FullPath.Contains("Сопоставление типов"))
    {
      this._contextMenu.Items.Clear();
      if (selectedNode.Tag is XmlExchangeImportObjectType)
      {
        this._contextMenu.Items.Add((ToolStripItem) this._addRuleMenuItem);
        this._contextMenu.Items.Add((ToolStripItem) this._deleteItemMenuItem);
        this._contextMenu.Items.Add((ToolStripItem) this._changeObjTypeMenuItem);
      }
      else if (selectedNode.Tag is XmlExchangeImportRulesImport || selectedNode.Tag is XmlExchangeImportRulesSearch || selectedNode.Tag is XmlExchangeImportRulesCreate)
        this._contextMenu.Items.Add((ToolStripItem) this._addRuleMenuItem);
      else if (selectedNode.Tag is XmlExchangeImportScript || selectedNode.Tag is XmlExchangeImportExtension)
      {
        this._contextMenu.Items.Add((ToolStripItem) this._addItemMenuItem);
        this._contextMenu.Items.Add((ToolStripItem) this._deleteItemMenuItem);
        this._contextMenu.Items.Add((ToolStripItem) this._moveMenu.MoveMenuItem);
      }
      else if (selectedNode.Tag is XmlExchangeImportScriptsBase || selectedNode.Tag is XmlExchangeImportExtensions)
        this._contextMenu.Items.Add((ToolStripItem) this._addItemMenuItem);
    }
    else if (selectedNode.Tag != this._importSettings.ExportSettings && this._exportConfigEditor != null)
      this._exportConfigEditor.Menu_Opening(sender, e, selectedNode);
    if (this._contextMenu.Items.Count > 0)
      e.Cancel = false;
    else
      e.Cancel = true;
  }

  public Image GetTabImage()
  {
    return this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexObjType("cadd9458-306c-11d8-b4e9-00304f19f545")];
  }

  private void AddRuleMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode = !(this._treeView.SelectedNode.Tag is XmlExchangeImportObjectType) ? this._treeView.SelectedNode : this._treeView.SelectedNode.Parent;
    if (!(treeNode.Tag is IList tag1))
      return;
    System.Collections.Generic.List<int> source = new System.Collections.Generic.List<int>();
    foreach (object obj in (IEnumerable) tag1)
      source.Add(MetaDataHelper.GetObjectTypeID(((XmlExchangeImportTypeItem) obj).Guid));
    IMSObjectType objType = this._helper.DiagSelectObjectType(source.Distinct<int>().ToList<int>());
    if (objType == null)
      return;
    XmlExchangeImportObjectType importObjectType = (XmlExchangeImportObjectType) null;
    if (treeNode.Tag is XmlExchangeImportRulesSearch tag4)
      importObjectType = tag4.CreateRule(objType);
    else if (treeNode.Tag is XmlExchangeImportRulesCreate tag3)
      importObjectType = tag3.CreateRule(objType);
    else if (treeNode.Tag is XmlExchangeImportRulesImport tag2)
      importObjectType = tag2.CreateRule(objType);
    if (importObjectType == null)
      return;
    TreeNode node = new TreeNode(importObjectType.Name)
    {
      Tag = (object) importObjectType
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(4, MetaDataHelper.GetObjectTypeID(importObjectType.Guid));
    treeNode.Nodes.Add(node);
    this._treeView.SelectedNode = node;
  }

  private void ChangeObjTypeMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is XmlExchangeImportObjectType tag1))
      return;
    TreeNode parent = selectedNode.Parent;
    if (parent == null || !(parent.Tag is IList tag2))
      return;
    System.Collections.Generic.List<int> objtypes = new System.Collections.Generic.List<int>();
    foreach (XmlExchangeImportObjectType importObjectType in (IEnumerable) tag2)
    {
      if (importObjectType != null)
        objtypes.Add(MetaDataHelper.GetObjectTypeID(importObjectType.Guid));
    }
    IMSObjectType imsObjectType = this._helper.DiagSelectObjectType(objtypes);
    if (imsObjectType == null)
      return;
    tag1.Guid = imsObjectType.Guid;
    tag1.Name = imsObjectType.ObjectTypeName;
    selectedNode.ImageIndex = selectedNode.SelectedImageIndex = this._helper.IconsIndexOf(4, imsObjectType.ObjectTypeID);
    selectedNode.Text = tag1.Name;
    FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, selectedNode, FontStyle.Regular);
  }

  private void DeleteItemMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (!(selectedNode.Tag is XmlExchangeImportItem tag1))
      return;
    TreeNode parent = selectedNode.Parent;
    if (!(parent.Tag is IList tag2) || !tag1.RemoveItemSetting())
      return;
    tag2.Remove((object) tag1);
    parent.Nodes.Remove(selectedNode);
  }

  private void AddItemMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode = !(this._treeView.SelectedNode.Tag is XmlExchangeImportItem) ? this._treeView.SelectedNode : this._treeView.SelectedNode.Parent;
    if (treeNode == null)
      return;
    TreeNode node = (TreeNode) null;
    if (treeNode.Tag is XmlExchangeImportScriptsBase tag2)
    {
      XmlExchangeImportScript script = tag2.CreateScript();
      node = new TreeNode(script.ScriptName);
      node.Tag = (object) script;
    }
    else if (treeNode.Tag is XmlExchangeImportExtensions tag1)
    {
      XmlExchangeImportExtension exchangeImportExtension = tag1.CreateExtension();
      node = new TreeNode(exchangeImportExtension.Name);
      node.Tag = (object) exchangeImportExtension;
    }
    if (node == null)
      return;
    node.ImageIndex = node.SelectedImageIndex = treeNode.ImageIndex;
    treeNode.Nodes.Add(node);
    this._treeView.SelectedNode = node;
  }

  private void CreateExpCongItemMenuItem_Click(object sender, EventArgs e)
  {
    if (!(this._treeView.SelectedNode.Tag is XmlExchangeImportXmlExportSettings tag) || !this.DialogResetExpConfig(tag))
      return;
    tag.ExportSettings = new XmlExchangeExportSettings();
    this.InitializeExportConfigEditor(tag.ExportSettings, this._treeView.SelectedNode);
  }

  private void AddExpConfigMenuItemClick(object sender, EventArgs e)
  {
    if (!(this._treeView.SelectedNode.Tag is XmlExchangeImportXmlExportSettings tag) || !this.DialogResetExpConfig(tag))
      return;
    long options = 0L + 16777216L /*0x01000000*/ + 256L /*0x0100*/;
    long[] numArray = SelectionWindow.SelectObjects(MetaDataHelper.GetObjectName(XmlExchangeConsts.Common.ExportSettObjTypeGuid), "", MetaDataHelper.GetObjectTypeID(XmlExchangeConsts.Common.ExportSettObjTypeGuid), (SelectionOptions) options);
    if (numArray == null || numArray.Length != 1)
      return;
    long objectID = numArray[0];
    if (objectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session?.GetObject(objectID);
      XmlExchangeExportSettings settings;
      if (dbObject == null || !XmlExchangeExportHelper.LoadSettings(dbObject, out settings, false))
        return;
      tag.ExportSettings = settings;
      this.InitializeExportConfigEditor(tag.ExportSettings, this._treeView.SelectedNode);
    }
  }

  private void InitializeContextMenu()
  {
    this._addRuleMenuItem = new ToolStripMenuItem();
    this._addItemMenuItem = new ToolStripMenuItem();
    this._deleteItemMenuItem = new ToolStripMenuItem();
    this._changeObjTypeMenuItem = new ToolStripMenuItem();
    this._createExpCongItemMenuItem = new ToolStripMenuItem();
    this._addExpConfigMenuItem = new ToolStripMenuItem();
    this._contextMenu.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this._addRuleMenuItem,
      (ToolStripItem) this._addItemMenuItem,
      (ToolStripItem) this._deleteItemMenuItem,
      (ToolStripItem) this._changeObjTypeMenuItem,
      (ToolStripItem) this._createExpCongItemMenuItem,
      (ToolStripItem) this._addExpConfigMenuItem
    });
    this._addRuleMenuItem.Name = "addRuleMenuItem";
    this._addRuleMenuItem.AutoSize = true;
    this._addRuleMenuItem.Text = "Добавить правило";
    this._addRuleMenuItem.Click += new EventHandler(this.AddRuleMenuItem_Click);
    this._addItemMenuItem.Name = "addRuleMenuItem";
    this._addItemMenuItem.AutoSize = true;
    this._addItemMenuItem.Text = "Создать элемент";
    this._addItemMenuItem.Click += new EventHandler(this.AddItemMenuItem_Click);
    this._deleteItemMenuItem.Name = "deleteItemMenuItem";
    this._deleteItemMenuItem.AutoSize = true;
    this._deleteItemMenuItem.Text = "Удалить элемент";
    this._deleteItemMenuItem.Click += new EventHandler(this.DeleteItemMenuItem_Click);
    this._changeObjTypeMenuItem.Name = "changeObjTypeMenuItem";
    this._changeObjTypeMenuItem.AutoSize = true;
    this._changeObjTypeMenuItem.Text = "Изменить тип";
    this._changeObjTypeMenuItem.Click += new EventHandler(this.ChangeObjTypeMenuItem_Click);
    this._createExpCongItemMenuItem.Name = "createExpCongItemMenuItem";
    this._createExpCongItemMenuItem.AutoSize = true;
    this._createExpCongItemMenuItem.Text = "Создать";
    this._createExpCongItemMenuItem.Click += new EventHandler(this.CreateExpCongItemMenuItem_Click);
    this._addExpConfigMenuItem.Name = "addExpConfigMenuItem";
    this._addExpConfigMenuItem.AutoSize = true;
    this._addExpConfigMenuItem.Text = "Добавить из объекта";
    this._addExpConfigMenuItem.Click += new EventHandler(this.AddExpConfigMenuItemClick);
  }

  private bool DialogResetExpConfig(
    XmlExchangeImportXmlExportSettings importXmlExportSettings)
  {
    return importXmlExportSettings == null || MessageBox.Show("Все внесенные изменения будут удалены! Продолжить?", "Конфигурация XML-экспорта", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel;
  }

  private void InitializeExportConfigEditor(
    XmlExchangeExportSettings exportSettings,
    TreeNode nodeExportSettings)
  {
    if (exportSettings == null)
      return;
    this._exportConfigEditor = new ExportConfigEditor(this._treeView, nodeExportSettings.Nodes, this._contextMenu);
    this._exportConfigEditor.LoadConfigData(this._importSettings.ExportSettings.ExportSettings);
  }
}
