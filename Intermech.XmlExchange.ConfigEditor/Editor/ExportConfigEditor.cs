// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Editor.ExportConfigEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.Interfaces.XmlExchange.Settings.Export.Extensions;
using Intermech.XmlExchange.ConfigEditor.ExportApplSetting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Editor;

internal class ExportConfigEditor : IConfigEditor
{
  private TreeView _treeView;
  private TreeNodeCollection _rootNodeCollection;
  private long _idObjectExportConfig;
  private string _pathFile = string.Empty;
  private bool _modeUserDataOnly;
  private bool _rootExportConfig = true;
  private ConfigEditorHelper _helper;
  private ConfigEditorModeView _modeView;
  private ConfigEditorMoveMenu _moveMenu;
  private XmlExchangeExportSettings _settings;
  private XmlExchangeExportAttrList _attrSettings;
  private XmlExchangeExportObjList _objSettings;
  private XmlExchangeExportRelList _relSettings;
  private List<XmlExchangeExportAppl> _applSettings;
  private XmlExchangeExportScripts _exportScripts;
  private XmlExchangeExportExtensions _exportExtensions;
  private ContextMenuStrip _contextMenu;
  private ToolStripMenuItem createMenuItem;
  private ToolStripMenuItem deleteMenuItem;
  private ToolStripMenuItem addAtrMenuItem;
  private ToolStripMenuItem removeMenuItem;
  private ToolStripMenuItem addObjMenuItem;
  private ToolStripMenuItem addRelMenuItem;
  private ToolStripMenuItem addCustomAtrMenuItem;
  private ToolStripMenuItem addCustomObjMenuItem;
  private ToolStripMenuItem addCustomRelMenuItem;
  private ToolStripMenuItem changeMenuItem;

  public ExportConfigEditor(
    TreeView treeView,
    TreeNodeCollection rootNodeCollection,
    ContextMenuStrip contextMenu)
  {
    this._treeView = treeView;
    this._rootNodeCollection = rootNodeCollection;
    this._helper = ConfigEditorHelper.GetHelper();
    this._modeView = ConfigEditorModeView.GetModeView();
    this._contextMenu = contextMenu;
    this._moveMenu = new ConfigEditorMoveMenu(this._treeView);
    this.InitializeContextMenu();
  }

  public Image GetTabImage()
  {
    return this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexObjType("cadd9444-306c-11d8-b4e9-00304f19f545")];
  }

  public void LoadConfigInObject(long idObjectConfig)
  {
    if (this._settings != null && MessageBox.Show("Все внесенные изменения будут удалены! Продолжить?", "Конфигурация XML-экспорта", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
      return;
    this._idObjectExportConfig = idObjectConfig;
    this._rootNodeCollection.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._idObjectExportConfig);
      if (dbObject == null)
        return;
      XmlExchangeExportHelper.LoadSettings(dbObject, out this._settings, false);
      if (this._settings == null)
      {
        if (MessageBox.Show("Файл конфигурации не содержит данных. Создать новую конфигурацию?", "Конфигурация XML-экспорта", MessageBoxButtons.OKCancel) != DialogResult.OK)
          return;
        this._settings = new XmlExchangeExportSettings();
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
        XmlExchangeExportHelper.LoadSettings((Stream) fileStream, out this._settings);
        if (this._settings == null)
        {
          int num = (int) MessageBox.Show("Данные файла не соответствует формату конфигурации.", "Конфигурация XML-экспорта");
          return string.Empty;
        }
      }
      this.LoadConfigData();
    }
    catch (IOException ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
    return fileInfo.Name;
  }

  internal void LoadConfigData(XmlExchangeExportSettings exportSettings)
  {
    this._settings = exportSettings;
    this.LoadConfigData();
  }

  private void LoadConfigData()
  {
    this._rootNodeCollection.Clear();
    if (this._treeView.Nodes != this._rootNodeCollection)
      this._rootExportConfig = false;
    this._attrSettings = this._settings.AttrSettings;
    this._objSettings = this._settings.ObjSettings;
    this._applSettings = (List<XmlExchangeExportAppl>) this._settings.ApplSettings;
    this._relSettings = this._settings.RelSettings;
    this._exportScripts = this._settings.ExportScripts;
    this._exportExtensions = this._settings.ExportExtensions;
    this._modeView = ConfigEditorModeView.GetModeView();
    if (this._modeView != null)
      this._modeView.GetConfig(this._settings, this._rootExportConfig);
    if (this._rootExportConfig)
    {
      TreeNode node = new TreeNode("Базовые настройки выгрузки")
      {
        Tag = (object) this._settings
      };
      node.SelectedImageIndex = node.ImageIndex = this._helper.IconsIndexObjType("cadd9444-306c-11d8-b4e9-00304f19f545");
      this._rootNodeCollection.Add(node);
    }
    this.LoadDataAttrSettings();
    this.LoadDataObjSettings();
    this.LoadDataRelSettings();
    if (this._rootExportConfig)
    {
      this.LoadDataApplSettings();
      this.LoadDataScriptSettings();
      this.LoadDataExtensionSettings();
    }
    this._treeView.SelectedNode = this._treeView.TopNode;
  }

  private void LoadDataAttrSettings()
  {
    TreeNode node = new TreeNode("Общие атрибуты")
    {
      Tag = (object) this._attrSettings
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(3, 0);
    this._rootNodeCollection.Add(node);
    foreach (XmlExchangeExportAttr type in (IEnumerable<XmlExchangeExportAttr>) this._attrSettings.OrderBy<XmlExchangeExportAttr, string>((Func<XmlExchangeExportAttr, string>) (a => a.CastToType<XmlExchangeExportTypedItem>().TypeName)))
    {
      TreeNode treeNode = new TreeNode(this._helper.ExportTypedName((XmlExchangeExportTypedItem) type));
      treeNode.Tag = (object) type;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(type.TypeGuid);
      if (attributeType != null && type.TypeID == attributeType.AttributeID)
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(3, -1, (object) attributeType.FieldType);
      }
      else
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(3, 0);
        FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode, FontStyle.Italic, "Тип атрибута отсутствует в базе");
      }
      node.Nodes.Add(treeNode);
    }
  }

  private void LoadDataObjSettings()
  {
    TreeNode node = new TreeNode("Типы объектов")
    {
      Tag = (object) this._objSettings
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(4, 0);
    this._rootNodeCollection.Add(node);
    foreach (XmlExchangeExportObj type in (IEnumerable<XmlExchangeExportObj>) this._objSettings.OrderBy<XmlExchangeExportObj, string>((Func<XmlExchangeExportObj, string>) (a => a.CastToType<XmlExchangeExportTypedItem>().TypeName)))
    {
      TreeNode treeNode = new TreeNode(this._helper.ExportTypedName((XmlExchangeExportTypedItem) type));
      treeNode.Tag = (object) type;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(type.TypeGuid);
      if (objectType != null && type.TypeID == objectType.ObjectTypeID)
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(4, type.TypeID);
      }
      else
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(4, 0);
        FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode, FontStyle.Italic, "Тип объекта отсутствует в базе");
      }
      node.Nodes.Add(treeNode);
    }
  }

  private void LoadDataRelSettings()
  {
    TreeNode node = new TreeNode("Типы связей")
    {
      Tag = (object) this._relSettings
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(6, 0);
    this._rootNodeCollection.Add(node);
    foreach (XmlExchangeExportRel type in (IEnumerable<XmlExchangeExportRel>) this._relSettings.OrderBy<XmlExchangeExportRel, string>((Func<XmlExchangeExportRel, string>) (a => a.CastToType<XmlExchangeExportTypedItem>().TypeName)))
    {
      TreeNode treeNode = new TreeNode(this._helper.ExportTypedName((XmlExchangeExportTypedItem) type));
      treeNode.Tag = (object) type;
      IMSRelationType relationType = MetaDataHelper.GetRelationType(type.TypeGuid);
      if (relationType != null && type.TypeID == relationType.RelationTypeID)
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(6, type.TypeID);
      }
      else
      {
        treeNode.ImageIndex = treeNode.SelectedImageIndex = this._helper.IconsIndexOf(6, 0);
        FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode, FontStyle.Italic, "Тип связи отсутствует в базе");
      }
      node.Nodes.Add(treeNode);
    }
  }

  private void LoadDataApplSettings()
  {
    TreeNode node1 = this._rootNodeCollection.OfType<TreeNode>().FirstOrDefault<TreeNode>((Func<TreeNode, bool>) (a => a.Tag == this._applSettings));
    if (node1 == null)
    {
      node1 = new TreeNode("Настройки выгрузки составов")
      {
        Tag = (object) this._applSettings
      };
      node1.ImageKey = node1.SelectedImageKey = "exportApplSettings";
      this._rootNodeCollection.Add(node1);
    }
    else
      node1.Nodes.Clear();
    if (this._applSettings.Count <= 0)
      return;
    foreach (IGrouping<Guid, XmlExchangeExportAppl> source1 in this._applSettings.GroupBy<XmlExchangeExportAppl, Guid>((Func<XmlExchangeExportAppl, Guid>) (a => a.ProjTypeGuid)).ToList<IGrouping<Guid, XmlExchangeExportAppl>>())
    {
      ExportApplObjectType projType;
      int num1;
      if (source1.Key == Guid.Empty && source1.First<XmlExchangeExportAppl>().ProjTypeID == -1)
      {
        projType = new ExportApplObjectType(this._applSettings, new IMSObjectType()
        {
          Guid = Guid.Empty,
          ObjectTypeID = -1,
          ObjectTypeName = "Любой тип объекта"
        });
        num1 = this._helper.IconsIndexOf(4, 0);
      }
      else
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(source1.Key);
        if (objectType != null && objectType.ObjectTypeID == source1.First<XmlExchangeExportAppl>().ProjTypeID)
        {
          projType = new ExportApplObjectType(this._applSettings, objectType);
          num1 = this._helper.IconsIndexOf(4, objectType.ObjectTypeID);
        }
        else
        {
          projType = new ExportApplObjectType(this._applSettings, source1.First<XmlExchangeExportAppl>().ProjTypeID, source1.Key, source1.Key.ToString());
          num1 = this._helper.IconsIndexOf(4, 0);
        }
      }
      TreeNode node2 = new TreeNode(projType.TypeName);
      node2.ImageIndex = node2.SelectedImageIndex = num1;
      node2.Tag = (object) projType;
      node1.Nodes.Add(node2);
      foreach (IGrouping<Guid, XmlExchangeExportAppl> source2 in source1.GroupBy<XmlExchangeExportAppl, Guid>((Func<XmlExchangeExportAppl, Guid>) (a => a.RelTypeGuid)).ToList<IGrouping<Guid, XmlExchangeExportAppl>>())
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(source2.Key);
        ExportApplRelationType applRelationType;
        int num2;
        if (relationType != null)
        {
          applRelationType = new ExportApplRelationType(this._applSettings, projType, relationType);
          num2 = this._helper.IconsIndexOf(6, applRelationType.TypeId);
        }
        else
        {
          applRelationType = new ExportApplRelationType(this._applSettings, projType, source2.First<XmlExchangeExportAppl>().RelTypeID, source2.Key, source2.Key.ToString());
          num2 = this._helper.IconsIndexOf(6, 0);
        }
        TreeNode node3 = new TreeNode(applRelationType.TypeName);
        node3.ImageIndex = node3.SelectedImageIndex = num2;
        node3.Tag = (object) applRelationType;
        node2.Nodes.Add(node3);
      }
    }
  }

  private void LoadDataScriptSettings()
  {
    TreeNode node1 = new TreeNode("Скрипты задачи выгрузки")
    {
      Tag = (object) this._exportScripts
    };
    node1.ImageIndex = node1.SelectedImageIndex = this._helper.IconsIndexObjType("cad0036a-306c-11d8-b4e9-00304f19f545");
    this._rootNodeCollection.Add(node1);
    foreach (XmlExchangeExportScript exportScript in (List<XmlExchangeExportScript>) this._exportScripts)
    {
      TreeNode node2 = new TreeNode(exportScript.ScriptName)
      {
        Tag = (object) exportScript,
        ImageIndex = node1.ImageIndex
      };
      node2.SelectedImageIndex = node2.ImageIndex;
      node1.Nodes.Add(node2);
    }
  }

  private void LoadDataExtensionSettings()
  {
    TreeNode node1 = new TreeNode("Расширения задачи выгрузки")
    {
      Tag = (object) this._exportExtensions
    };
    node1.ImageIndex = node1.SelectedImageIndex = this._helper.IconsIndexObjType("cad0005b-306c-11d8-b4e9-00304f19f545");
    this._rootNodeCollection.Add(node1);
    foreach (XmlExchangeExportExtension exportExtension in (List<XmlExchangeExportExtension>) this._exportExtensions)
    {
      TreeNode node2 = new TreeNode(exportExtension.Name)
      {
        Tag = (object) exportExtension,
        ImageIndex = node1.ImageIndex
      };
      node2.SelectedImageIndex = node2.ImageIndex;
      node1.Nodes.Add(node2);
    }
  }

  public void UpdateTreeView(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null)
      return;
    switch (selectedNode.Tag)
    {
      case XmlExchangeExportSettings _:
        if (this._modeUserDataOnly == this._modeView.UserDataOnly)
          break;
        this._modeUserDataOnly = this._modeView.UserDataOnly;
        this.UpdateNodeCaption();
        break;
      case XmlExchangeExportTypedItem type:
        selectedNode.Text = this._helper.ExportTypedName(type);
        break;
      case IExportApplType _:
        if (this.UpdateNodeExportApplType(selectedNode))
          break;
        this.LoadDataApplSettings();
        break;
      case XmlExchangeExportScript exchangeExportScript:
        selectedNode.Text = exchangeExportScript.ScriptName;
        break;
      case XmlExchangeExportExtension exchangeExportExtension:
        selectedNode.Text = exchangeExportExtension.Name;
        break;
    }
  }

  private void UpdateNodeCaption()
  {
    foreach (TreeNode rootNode in this._rootNodeCollection)
    {
      if (rootNode.Tag is List<XmlExchangeExportTypedItem>)
      {
        foreach (TreeNode node in rootNode.Nodes)
        {
          if (node.Tag is XmlExchangeExportTypedItem tag)
            node.Text = this._helper.ExportTypedName(tag);
        }
      }
    }
  }

  private bool UpdateNodeExportApplType(TreeNode selectedNode)
  {
    if (!(selectedNode.Tag is IExportApplType tag) || tag.GetCurrentApplList().Count != 0)
      return false;
    selectedNode.Text = tag.TypeName;
    return true;
  }

  private void CreateMenuItem_Click(object sender, EventArgs e)
  {
    if (this._treeView.SelectedNode.Tag is XmlExchangeExportScripts || this._treeView.SelectedNode.Tag is XmlExchangeExportScript)
    {
      TreeNode treeNode = (TreeNode) null;
      if (this._treeView.SelectedNode.Tag is XmlExchangeExportScripts)
        treeNode = this._treeView.SelectedNode;
      else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is XmlExchangeExportScripts)
        treeNode = this._treeView.SelectedNode.Parent;
      if (treeNode == null)
        return;
      XmlExchangeExportScript exchangeExportScript = new XmlExchangeExportScript();
      exchangeExportScript.ScriptName = "New Export Script";
      exchangeExportScript.ScriptCode = XmlConfigEmptyScript.xmlExportEmptyScript;
      this._exportScripts.Add(exchangeExportScript);
      TreeNode node = new TreeNode(exchangeExportScript.ScriptName)
      {
        Tag = (object) exchangeExportScript
      };
      node.SelectedImageIndex = node.ImageIndex = treeNode.ImageIndex;
      treeNode.Nodes.Add(node);
      this._treeView.SelectedNode = node;
    }
    if (!(this._treeView.SelectedNode.Tag is XmlExchangeExportExtensions) && !(this._treeView.SelectedNode.Tag is XmlExchangeExportExtension))
      return;
    TreeNode treeNode1 = (TreeNode) null;
    if (this._treeView.SelectedNode.Tag is XmlExchangeExportExtensions)
      treeNode1 = this._treeView.SelectedNode;
    else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is XmlExchangeExportExtensions)
      treeNode1 = this._treeView.SelectedNode.Parent;
    if (treeNode1 == null)
      return;
    XmlExchangeExportExtension exchangeExportExtension = new XmlExchangeExportExtension();
    exchangeExportExtension.Name = "New Export Extension";
    this._exportExtensions.Add(exchangeExportExtension);
    TreeNode node1 = new TreeNode(exchangeExportExtension.Name)
    {
      Tag = (object) exchangeExportExtension
    };
    node1.SelectedImageIndex = node1.ImageIndex = treeNode1.ImageIndex;
    treeNode1.Nodes.Add(node1);
    this._treeView.SelectedNode = node1;
  }

  private void DeleteMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null)
      return;
    TreeNode parent = selectedNode.Parent;
    if (parent == null)
      return;
    this.RemoveExportBase(selectedNode, parent);
  }

  private void AddAtrMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode = (TreeNode) null;
    if (this._treeView.SelectedNode.Tag is XmlExchangeExportAttrList)
      treeNode = this._treeView.SelectedNode;
    else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is XmlExchangeExportAttrList)
      treeNode = this._treeView.SelectedNode.Parent;
    if (treeNode == null)
      return;
    IMSAttributeType imsAttributeType = this._helper.DiagSelectAttributeType(this._attrSettings.Select<XmlExchangeExportAttr, int>((Func<XmlExchangeExportAttr, int>) (a => a.ID)).ToList<int>());
    if (imsAttributeType == null)
      return;
    XmlExchangeExportAttr exchangeExportAttr = new XmlExchangeExportAttr(imsAttributeType.AttributeID, imsAttributeType.AttributeGuid, imsAttributeType.Name);
    this._attrSettings.Add(exchangeExportAttr);
    TreeNode node = new TreeNode(exchangeExportAttr.TypeName)
    {
      Tag = (object) exchangeExportAttr
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(3, -1, (object) imsAttributeType.FieldType);
    treeNode.Nodes.Add(node);
    this._treeView.SelectedNode = node;
  }

  private void addCustomAtrMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode1 = this.SelectedNodeSettingList();
    if (treeNode1 == null || !(treeNode1.Tag is XmlExchangeExportAttrList tag))
      return;
    XmlExchangeExportAttr exchangeExportAttr = new XmlExchangeExportAttr();
    exchangeExportAttr.TypeName = "Новый тип атрибута";
    exchangeExportAttr.TypeID = -1;
    tag.Add(exchangeExportAttr);
    TreeNode treeNode2 = new TreeNode(exchangeExportAttr.TypeName)
    {
      Tag = (object) exchangeExportAttr
    };
    treeNode2.ImageIndex = treeNode2.SelectedImageIndex = this._helper.IconsIndexOf(3, 0);
    treeNode1.Nodes.Add(treeNode2);
    FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode2, FontStyle.Italic, "Тип атрибута отсутствует в базе");
    this._treeView.SelectedNode = treeNode2;
  }

  private void AddObjMenuItem_Click(object sender, EventArgs e)
  {
    if (this._treeView.SelectedNode.Tag is XmlExchangeExportObjList || this._treeView.SelectedNode.Tag is XmlExchangeExportObj)
    {
      TreeNode treeNode = (TreeNode) null;
      if (this._treeView.SelectedNode.Tag is XmlExchangeExportObjList)
        treeNode = this._treeView.SelectedNode;
      else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is XmlExchangeExportObjList)
        treeNode = this._treeView.SelectedNode.Parent;
      if (treeNode == null)
        return;
      IMSObjectType imsObjectType = this._helper.DiagSelectObjectType(this._objSettings.Select<XmlExchangeExportObj, int>((Func<XmlExchangeExportObj, int>) (a => a.ID)).ToList<int>());
      if (imsObjectType == null)
        return;
      XmlExchangeExportObj exchangeExportObj = new XmlExchangeExportObj(imsObjectType.ObjectTypeID, imsObjectType.Guid, imsObjectType.ObjectTypeName);
      this._objSettings.Add(exchangeExportObj);
      TreeNode node = new TreeNode(exchangeExportObj.TypeName)
      {
        Tag = (object) exchangeExportObj
      };
      node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(4, exchangeExportObj.TypeID);
      treeNode.Nodes.Add(node);
      this._treeView.SelectedNode = node;
    }
    else
    {
      if (this._treeView.SelectedNode.Tag != this._applSettings && !(this._treeView.SelectedNode.Tag is ExportApplObjectType))
        return;
      TreeNode treeNode = (TreeNode) null;
      if (this._treeView.SelectedNode.Tag == this._applSettings)
        treeNode = this._treeView.SelectedNode;
      else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag == this._applSettings)
        treeNode = this._treeView.SelectedNode.Parent;
      if (treeNode == null)
        return;
      List<int> objtypes = new List<int>();
      foreach (TreeNode node in treeNode.Nodes)
        objtypes.Add(node.Tag.CastToType<IExportApplType>().TypeId);
      IMSObjectType objType = this._helper.DiagSelectObjectType(objtypes);
      if (objType == null)
        return;
      ExportApplObjectType exportApplObjectType = new ExportApplObjectType(this._applSettings, objType);
      TreeNode node1 = new TreeNode(exportApplObjectType.TypeName)
      {
        Tag = (object) exportApplObjectType
      };
      node1.ImageIndex = node1.SelectedImageIndex = this._helper.IconsIndexOf(4, exportApplObjectType.TypeId);
      treeNode.Nodes.Add(node1);
      this._treeView.SelectedNode = node1;
    }
  }

  private void AddRelMenuItem_Click(object sender, EventArgs e)
  {
    if (this._treeView.SelectedNode.Tag is XmlExchangeExportRelList || this._treeView.SelectedNode.Tag is XmlExchangeExportRel)
    {
      TreeNode treeNode = (TreeNode) null;
      if (this._treeView.SelectedNode.Tag is XmlExchangeExportRelList)
        treeNode = this._treeView.SelectedNode;
      else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is XmlExchangeExportRelList)
        treeNode = this._treeView.SelectedNode.Parent;
      if (treeNode == null)
        return;
      IMSRelationType imsRelationType = this._helper.DiagSelectRelationType(this._relSettings.Select<XmlExchangeExportRel, int>((Func<XmlExchangeExportRel, int>) (a => a.ID)).ToList<int>());
      if (imsRelationType == null)
        return;
      XmlExchangeExportRel exchangeExportRel = new XmlExchangeExportRel(imsRelationType.RelationTypeID, imsRelationType.Guid, imsRelationType.Description);
      this._relSettings.Add(exchangeExportRel);
      TreeNode node = new TreeNode(exchangeExportRel.TypeName)
      {
        Tag = (object) exchangeExportRel
      };
      node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(6, imsRelationType.RelationTypeID);
      treeNode.Nodes.Add(node);
      this._treeView.SelectedNode = node;
    }
    else
    {
      if (!(this._treeView.SelectedNode.Tag is IExportApplType))
        return;
      TreeNode treeNode = (TreeNode) null;
      if (this._treeView.SelectedNode.Tag is ExportApplObjectType)
        treeNode = this._treeView.SelectedNode;
      else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is ExportApplObjectType)
        treeNode = this._treeView.SelectedNode.Parent;
      if (treeNode == null)
        return;
      List<int> typeList = new List<int>();
      foreach (TreeNode node in treeNode.Nodes)
        typeList.Add(node.Tag.CastToType<IExportApplType>().TypeId);
      int typeId = treeNode.Tag.CastToType<IExportApplType>().TypeId;
      IMSRelationType relationType = this._helper.DiagSelectRelationType(typeList, typeId);
      if (relationType == null)
        return;
      ExportApplRelationType applRelationType = new ExportApplRelationType(this._applSettings, treeNode.Tag as ExportApplObjectType, relationType);
      TreeNode node1 = new TreeNode(applRelationType.TypeName)
      {
        Tag = (object) applRelationType
      };
      node1.ImageIndex = node1.SelectedImageIndex = this._helper.IconsIndexOf(6, applRelationType.TypeId);
      treeNode.Nodes.Add(node1);
      this._treeView.SelectedNode = node1;
    }
  }

  private void addCustomRelMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode1 = this.SelectedNodeSettingList();
    if (this.SelectedNodeApplSettings() == null && treeNode1 != null)
    {
      if (!(treeNode1.Tag is XmlExchangeExportRelList tag))
        return;
      XmlExchangeExportRel exchangeExportRel = new XmlExchangeExportRel();
      int num = this._helper.IconsIndexOf(6, 0);
      exchangeExportRel.TypeName = "Новый тип связи";
      exchangeExportRel.TypeID = -1;
      tag.Add(exchangeExportRel);
      TreeNode treeNode2 = new TreeNode(exchangeExportRel.TypeName)
      {
        Tag = (object) exchangeExportRel
      };
      treeNode2.ImageIndex = treeNode2.SelectedImageIndex = num;
      FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode2, FontStyle.Italic, "Тип связи отсутствует в базе");
      treeNode1.Nodes.Add(treeNode2);
      this._treeView.SelectedNode = treeNode2;
    }
    if (!(this._treeView.SelectedNode.Tag is IExportApplType))
      return;
    TreeNode treeNode3 = (TreeNode) null;
    if (this._treeView.SelectedNode.Tag is ExportApplObjectType)
      treeNode3 = this._treeView.SelectedNode;
    else if (this._treeView.SelectedNode.Parent != null && this._treeView.SelectedNode.Parent.Tag is ExportApplObjectType)
      treeNode3 = this._treeView.SelectedNode.Parent;
    if (treeNode3 == null)
      return;
    ExportApplRelationType applRelationType = new ExportApplRelationType(this._applSettings, treeNode3.Tag as ExportApplObjectType, 0, Guid.Empty, "Новый тип связи");
    TreeNode node = new TreeNode(applRelationType.TypeName)
    {
      Tag = (object) applRelationType
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(6, 0);
    treeNode3.Nodes.Add(node);
    this._treeView.SelectedNode = node;
  }

  private void addCustomObjMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode1 = this.SelectedNodeSettingList();
    TreeNode treeNode2 = this.SelectedNodeApplSettings();
    if (treeNode1 == null && treeNode2 == null)
      return;
    if (treeNode2 == null)
    {
      if (!(treeNode1.Tag is XmlExchangeExportObjList tag))
        return;
      XmlExchangeExportObj exchangeExportObj = new XmlExchangeExportObj();
      exchangeExportObj.TypeName = "Новый тип объекта";
      exchangeExportObj.TypeID = -1;
      exchangeExportObj.TypeGuid = Guid.NewGuid();
      tag.Add(exchangeExportObj);
      TreeNode treeNode3 = new TreeNode(exchangeExportObj.TypeName)
      {
        Tag = (object) exchangeExportObj
      };
      treeNode3.ImageIndex = treeNode3.SelectedImageIndex = this._helper.IconsIndexOf(4, 0);
      FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, treeNode3, FontStyle.Italic, "Тип объекта отсутствует в базе");
      treeNode1.Nodes.Add(treeNode3);
      this._treeView.SelectedNode = treeNode3;
    }
    if (treeNode2 == null)
      return;
    ExportApplObjectType exportApplObjectType = new ExportApplObjectType(this._applSettings, 0, Guid.Empty, "Новый тип объекта");
    TreeNode node = new TreeNode(exportApplObjectType.TypeName)
    {
      Tag = (object) exportApplObjectType
    };
    node.ImageIndex = node.SelectedImageIndex = this._helper.IconsIndexOf(4, 0);
    treeNode2.Nodes.Add(node);
    this._treeView.SelectedNode = node;
  }

  private void changeMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null)
      return;
    if (selectedNode.Tag is XmlExchangeExportTypedItem tag1)
    {
      switch (tag1)
      {
        case XmlExchangeExportAttr _:
          IMSAttributeType imsAttributeType = this._helper.DiagSelectAttributeType(this._attrSettings.Select<XmlExchangeExportAttr, int>((Func<XmlExchangeExportAttr, int>) (a => a.ID)).ToList<int>());
          if (imsAttributeType == null)
            return;
          tag1.TypeID = imsAttributeType.AttributeID;
          tag1.TypeGuid = imsAttributeType.AttributeGuid;
          tag1.TypeName = imsAttributeType.Name;
          selectedNode.ImageIndex = selectedNode.SelectedImageIndex = this._helper.IconsIndexOf(3, -1, (object) imsAttributeType.FieldType);
          break;
        case XmlExchangeExportObj _:
          IMSObjectType imsObjectType = this._helper.DiagSelectObjectType(this._objSettings.Select<XmlExchangeExportObj, int>((Func<XmlExchangeExportObj, int>) (a => a.ID)).ToList<int>());
          if (imsObjectType == null)
            return;
          tag1.TypeID = imsObjectType.ObjectTypeID;
          tag1.TypeGuid = imsObjectType.Guid;
          tag1.TypeName = imsObjectType.ObjectTypeName;
          selectedNode.ImageIndex = selectedNode.SelectedImageIndex = this._helper.IconsIndexOf(4, imsObjectType.ObjectTypeID);
          break;
        case XmlExchangeExportRel _:
          IMSRelationType imsRelationType = this._helper.DiagSelectRelationType(this._relSettings.Select<XmlExchangeExportRel, int>((Func<XmlExchangeExportRel, int>) (a => a.ID)).ToList<int>());
          if (imsRelationType == null)
            return;
          tag1.TypeID = imsRelationType.RelationTypeID;
          tag1.TypeGuid = imsRelationType.Guid;
          tag1.TypeName = imsRelationType.Description;
          selectedNode.ImageIndex = selectedNode.SelectedImageIndex = this._helper.IconsIndexOf(6, imsRelationType.RelationTypeID);
          break;
      }
      selectedNode.Text = this._helper.ExportTypedName(tag1);
      FactoryConfigEditor.SetFontTreeNode(this._treeView.Font, selectedNode, FontStyle.Regular);
    }
    if (selectedNode.Tag is ExportApplObjectType tag3)
    {
      IMSObjectType newObjectType = this._helper.DiagSelectObjectType(this._applSettings.Select<XmlExchangeExportAppl, int>((Func<XmlExchangeExportAppl, int>) (a => a.ProjTypeID)).Distinct<int>().ToList<int>());
      if (newObjectType == null)
        return;
      tag3.UpdateExportAppl(newObjectType);
      TreeNode treeNode1 = selectedNode;
      TreeNode treeNode2 = selectedNode;
      ConfigEditorHelper helper = this._helper;
      int typeId = tag3.TypeId > 0 ? tag3.TypeId : 0;
      int num1;
      int num2 = num1 = helper.IconsIndexOf(4, typeId);
      treeNode2.SelectedImageIndex = num1;
      int num3 = num2;
      treeNode1.ImageIndex = num3;
      selectedNode.Text = tag3.TypeName;
    }
    else if (selectedNode.Tag is ExportApplRelationType tag2)
    {
      List<int> typeList = new List<int>();
      foreach (TreeNode node in selectedNode.Parent.Nodes)
        typeList.Add(node.Tag.CastToType<IExportApplType>().TypeId);
      IMSRelationType newRelationType = this._helper.DiagSelectRelationType(typeList);
      if (newRelationType == null)
        return;
      tag2.UpdateExportAppl(newRelationType);
      selectedNode.ImageIndex = selectedNode.SelectedImageIndex = this._helper.IconsIndexOf(6, tag2.TypeId);
      selectedNode.Text = tag2.TypeName;
    }
    this._treeView.SelectedNode = (TreeNode) null;
    this._treeView.SelectedNode = selectedNode;
  }

  private void RemoveMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null)
      return;
    TreeNode parent = selectedNode.Parent;
    if (selectedNode.Tag is XmlExchangeExportTypedItem)
    {
      this.RemoveExportBase(selectedNode, parent);
    }
    else
    {
      ExportApplObjectType applProjType;
      if ((applProjType = selectedNode.Tag as ExportApplObjectType) != null)
      {
        if (parent.Tag != this._applSettings)
          return;
        this._applSettings.RemoveAll((Predicate<XmlExchangeExportAppl>) (appl => appl.ProjTypeGuid == applProjType.TypeGuid));
        parent.Nodes.Remove(selectedNode);
      }
      else
      {
        ExportApplRelationType applRelationType;
        if ((applRelationType = selectedNode.Tag as ExportApplRelationType) == null)
          return;
        ExportApplObjectType projType;
        if ((projType = parent.Tag as ExportApplObjectType) == null)
          return;
        this._applSettings.RemoveAll((Predicate<XmlExchangeExportAppl>) (appl => appl.ProjTypeGuid == projType.TypeGuid && appl.RelTypeGuid == applRelationType.TypeGuid));
        parent.Nodes.Remove(selectedNode);
      }
    }
  }

  private TreeNode SelectedNodeSettingList()
  {
    TreeNode treeNode = (TreeNode) null;
    if (this._treeView.SelectedNode.Tag is IList)
      treeNode = this._treeView.SelectedNode;
    else if (this._treeView.SelectedNode.Parent?.Tag is IList)
      treeNode = this._treeView.SelectedNode.Parent;
    return treeNode;
  }

  private TreeNode SelectedNodeApplSettings()
  {
    TreeNode treeNode = (TreeNode) null;
    if (this._treeView.SelectedNode.Tag is List<XmlExchangeExportAppl>)
      treeNode = this._treeView.SelectedNode;
    else if (this._treeView.SelectedNode.Tag is ExportApplObjectType)
      treeNode = this._treeView.SelectedNode.Parent;
    return treeNode;
  }

  public void SaveConfigInObject()
  {
    if (this._idObjectExportConfig == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session == null)
        return;
      XmlExchangeExportHelper.SaveSettings(this._idObjectExportConfig, session, this._settings);
    }
  }

  public bool SaveConfigInFile(string pathFile)
  {
    string path = !string.IsNullOrEmpty(pathFile) ? pathFile : this._pathFile;
    try
    {
      using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
      {
        XmlExchangeExportHelper.SaveSettings((Stream) fileStream, this._settings);
        if (this._pathFile != pathFile)
          this._pathFile = path;
      }
      return true;
    }
    catch (IOException ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
      return false;
    }
  }

  private void RemoveExportBase(TreeNode selectedNode, TreeNode parentNode)
  {
    if (!(selectedNode.Tag is XmlExchangeExportItem tag1) || !(parentNode.Tag is IList tag2))
      return;
    tag2.Remove((object) tag1);
    parentNode.Nodes.Remove(selectedNode);
  }

  public void EnterEditorWindow(object sender, EventArgs e)
  {
    if (this._settings == null)
      return;
    ConfigEditorModeView.GetModeView()?.GetConfig(this._settings, this._rootExportConfig);
  }

  private void InitializeContextMenu()
  {
    this.createMenuItem = new ToolStripMenuItem();
    this.deleteMenuItem = new ToolStripMenuItem();
    this.addAtrMenuItem = new ToolStripMenuItem();
    this.addCustomAtrMenuItem = new ToolStripMenuItem();
    this.addObjMenuItem = new ToolStripMenuItem();
    this.addCustomObjMenuItem = new ToolStripMenuItem();
    this.addRelMenuItem = new ToolStripMenuItem();
    this.addCustomRelMenuItem = new ToolStripMenuItem();
    this.changeMenuItem = new ToolStripMenuItem();
    this.removeMenuItem = new ToolStripMenuItem();
    this.createMenuItem.Name = "createMenuItem";
    this.createMenuItem.AutoSize = true;
    this.createMenuItem.Text = "Создать элемент";
    this.createMenuItem.Click += new EventHandler(this.CreateMenuItem_Click);
    this.deleteMenuItem.Name = "deleteMenuItem";
    this.deleteMenuItem.AutoSize = true;
    this.deleteMenuItem.Text = "Удалить элемент";
    this.deleteMenuItem.Click += new EventHandler(this.DeleteMenuItem_Click);
    this.addAtrMenuItem.Name = "addAtrMenuItem";
    this.addAtrMenuItem.AutoSize = true;
    this.addAtrMenuItem.Text = "Добавить тип атрибута";
    this.addAtrMenuItem.Click += new EventHandler(this.AddAtrMenuItem_Click);
    this.addCustomAtrMenuItem.Name = "addCustomAtrMenuItem";
    this.addCustomAtrMenuItem.AutoSize = true;
    this.addCustomAtrMenuItem.Text = "Пользовательский тип атрибута";
    this.addCustomAtrMenuItem.Click += new EventHandler(this.addCustomAtrMenuItem_Click);
    this.addObjMenuItem.Name = "addObjMenuItem";
    this.addObjMenuItem.AutoSize = true;
    this.addObjMenuItem.Text = "Добавить тип объекта";
    this.addObjMenuItem.Click += new EventHandler(this.AddObjMenuItem_Click);
    this.addCustomObjMenuItem.Name = "addCustomObjMenuItem";
    this.addCustomObjMenuItem.AutoSize = true;
    this.addCustomObjMenuItem.Text = "Пользовательский тип объекта";
    this.addCustomObjMenuItem.Click += new EventHandler(this.addCustomObjMenuItem_Click);
    this.addRelMenuItem.Name = "addRelMenuItem";
    this.addRelMenuItem.AutoSize = true;
    this.addRelMenuItem.Text = "Добавить тип связи";
    this.addRelMenuItem.Click += new EventHandler(this.AddRelMenuItem_Click);
    this.addCustomRelMenuItem.Name = "addCustomRelMenuItem";
    this.addCustomRelMenuItem.AutoSize = true;
    this.addCustomRelMenuItem.Text = "Пользовательский тип связи";
    this.addCustomRelMenuItem.Click += new EventHandler(this.addCustomRelMenuItem_Click);
    this.changeMenuItem.Name = "changeMenuItem";
    this.changeMenuItem.AutoSize = true;
    this.changeMenuItem.Text = "Изменить тип";
    this.changeMenuItem.Click += new EventHandler(this.changeMenuItem_Click);
    this.removeMenuItem.Name = "removeMenuItem";
    this.removeMenuItem.AutoSize = true;
    this.removeMenuItem.Text = "Исключить тип";
    this.removeMenuItem.Click += new EventHandler(this.RemoveMenuItem_Click);
    this._contextMenu.Items.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this.createMenuItem,
      (ToolStripItem) this.deleteMenuItem,
      (ToolStripItem) this.addAtrMenuItem,
      (ToolStripItem) this.addCustomAtrMenuItem,
      (ToolStripItem) this.addObjMenuItem,
      (ToolStripItem) this.addCustomObjMenuItem,
      (ToolStripItem) this.addRelMenuItem,
      (ToolStripItem) this.addCustomRelMenuItem,
      (ToolStripItem) this.changeMenuItem,
      (ToolStripItem) this.removeMenuItem
    });
    this.addAtrMenuItem.ImageIndex = this._helper.IconsIndexOf(3, 0);
    this.addObjMenuItem.ImageIndex = this._helper.IconsIndexOf(4, 0);
    this.addRelMenuItem.ImageIndex = this._helper.IconsIndexOf(6, 0);
    this.addCustomAtrMenuItem.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(3, 0)], this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 1)]);
    this.addCustomObjMenuItem.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 0)], this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 1)]);
    this.addCustomRelMenuItem.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(6, 0)], this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 1)]);
    this.removeMenuItem.Image = this._helper.ImageIcon("imgDelete");
  }

  public void Menu_Opening(object sender, CancelEventArgs e, TreeNode selectedNode)
  {
    this._contextMenu.Items.Clear();
    this.removeMenuItem.Image = (Image) null;
    if (selectedNode.Tag is XmlExchangeExportAttrList || selectedNode.Tag is XmlExchangeExportAttr)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.addAtrMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.addCustomAtrMenuItem);
    }
    if (selectedNode.Tag is XmlExchangeExportObjList || selectedNode.Tag is XmlExchangeExportObj)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.addObjMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.addCustomObjMenuItem);
    }
    if (selectedNode.Tag is XmlExchangeExportRelList || selectedNode.Tag is XmlExchangeExportRel)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.addRelMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.addCustomRelMenuItem);
    }
    if (selectedNode.Tag is XmlExchangeExportAttr || selectedNode.Tag is XmlExchangeExportObj || selectedNode.Tag is XmlExchangeExportRel || selectedNode.Tag is IExportApplType)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.removeMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.changeMenuItem);
    }
    if (selectedNode.Tag == this._applSettings || selectedNode.Tag is ExportApplObjectType)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.addObjMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.addCustomObjMenuItem);
    }
    if (selectedNode.Tag is ExportApplObjectType || selectedNode.Tag is ExportApplRelationType)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.addRelMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.addCustomRelMenuItem);
    }
    if (selectedNode.Tag is XmlExchangeExportScripts || selectedNode.Tag is XmlExchangeExportExtensions)
      this._contextMenu.Items.Add((ToolStripItem) this.createMenuItem);
    if (selectedNode.Tag is XmlExchangeExportScript || selectedNode.Tag is XmlExchangeExportExtension)
    {
      this._contextMenu.Items.Add((ToolStripItem) this.createMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this.deleteMenuItem);
      this._contextMenu.Items.Add((ToolStripItem) this._moveMenu.MoveMenuItem);
    }
    if (selectedNode.Tag is XmlExchangeExportObj || selectedNode.Tag is ExportApplObjectType)
      this.removeMenuItem.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.ImageIcon("imgDelete"), this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 0)]);
    if (selectedNode.Tag is XmlExchangeExportAttr)
      this.removeMenuItem.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.ImageIcon("imgDelete"), this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(3, 0)]);
    if (!(selectedNode.Tag is XmlExchangeExportRel) && !(selectedNode.Tag is ExportApplRelationType))
      return;
    this.removeMenuItem.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.ImageIcon("imgDelete"), this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(6, 0)]);
  }
}
