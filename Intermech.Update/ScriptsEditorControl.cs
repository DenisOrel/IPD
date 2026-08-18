// Decompiled with JetBrains decompiler
// Type: Intermech.Update.ScriptsEditorControl
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Controls;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.LifeCycles;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using Intermech.Update.CodeFormers;
using Intermech.Update.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace Intermech.Update;

public class ScriptsEditorControl : DockControl, IGuid
{
  private Guid _guid = new Guid("F2D7BF25-5658-4709-A157-A2E7418E5FA1");
  private Dictionary<object, Object4Script> _objects;
  private XDocument xModelDoc;
  private bool isUpdated;
  private Dictionary<Guid, string> errors = new Dictionary<Guid, string>();
  private bool suppressErrorDlg;
  private readonly List<string> temporaryFiles = new List<string>();
  private IContainer components;
  private Panel panel1;
  private Panel panel2;
  private ImageList imageList1;
  private ToolStripButton tsbAddObject;
  private ToolStripButton tsbAddAttributeGroup;
  private ToolStripButton tsbAddAttribute;
  private ToolStripButton tsbAddObjectType;
  private ToolStripButton tsbAddLCSchemas;
  private ToolStripButton tsbAddSubjArea;
  private ToolStripButton tsbAddRelationTypes;
  private ToolStripButton tsbAddLCLevel;
  private ToolStripButton tsbAddLanguage;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripLabel toolStripLabel3;
  private ToolStripButton tsbStart;
  private ToolStripLabel toolStripLabel2;
  private ToolStripButton tsbDeleteRecord;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton tsbNewScript;
  private ToolStrip toolStrip1;
  private TreeView treeView1;
  private ToolStripButton tsbLoadAndCompareXml;
  private Label lblStatus;
  private ContextMenuStrip contextMenuTreeList;
  private ToolStripMenuItem miAddToScript;
  private ToolStripMenuItem miSyncWithScript;
  private ToolStripMenuItem miDeleteFromScript;
  private ToolStripMenuItem miCollapseAll;
  private Label lblLegendSame;
  private Label lblLegendDiffs;
  private Label lblLegendMissing;
  private ToolStripButton tsbSaveToXml;
  private Label lblErrors;
  private ToolStripMenuItem miExpandAll;
  private ToolStripMenuItem miMultiSelect;

  public ScriptsEditorControl()
  {
    this.InitializeComponent();
    this._objects = new Dictionary<object, Object4Script>();
    this.treeView1.MouseClick += new MouseEventHandler(this.HandleMouseActions);
    this.miAddToScript.Click += (EventHandler) ((s, e) => this.AddNodeObjectToScript());
    this.miDeleteFromScript.Click += (EventHandler) ((s, e) => this.RemoveNodeObjectFromScript(true));
    this.miSyncWithScript.Click += (EventHandler) ((s, e) => this.UpdateScriptNodeFromTreeNode());
    this.miCollapseAll.Click += (EventHandler) ((s, e) => this.treeView1.CollapseAll());
    this.miExpandAll.Click += (EventHandler) ((s, e) => this.treeView1.ExpandAll());
    this.miMultiSelect.Click += new EventHandler(this.ToggleSelectionMode);
  }

  public Guid GUID => this._guid;

  private int ImageIndex(int category)
  {
    switch (category)
    {
      case -1:
        return 0;
      case 2:
        return 1;
      case 3:
        return 3;
      case 4:
        return 4;
      case 5:
      case 6:
        return 7;
      case 8:
        return 8;
      case 9:
        return 9;
      case 11:
        return 6;
      case 12:
        return 2;
      case 16 /*0x10*/:
        return 5;
      default:
        return 0;
    }
  }

  private void RefreshButtons()
  {
    this.tsbNewScript.Enabled = true;
    this.tsbStart.Enabled = this._objects != null && this._objects.Count > 0;
    this.tsbDeleteRecord.Enabled = this.treeView1.SelectedNode != null;
    this.tsbLoadAndCompareXml.Enabled = true;
    ToolStripMenuItem miAddToScript = this.miAddToScript;
    int num1;
    if (this.treeView1.SelectedNode != null && this.xModelDoc != null && this.treeView1.SelectedNode.Tag is Object4Script)
    {
      Font nodeFont = this.treeView1.SelectedNode.NodeFont;
      num1 = nodeFont != null ? (nodeFont.Bold ? 1 : 0) : 0;
    }
    else
      num1 = 0;
    miAddToScript.Enabled = num1 != 0;
    ToolStripMenuItem deleteFromScript = this.miDeleteFromScript;
    int num2;
    if (this.treeView1.SelectedNode != null && this.xModelDoc != null && this.treeView1.SelectedNode.Tag is Object4Script)
    {
      Font nodeFont = this.treeView1.SelectedNode.NodeFont;
      if ((nodeFont != null ? (nodeFont.Bold ? 1 : 0) : 0) == 0)
      {
        num2 = this.treeView1.SelectedNode.ForeColor != Color.SlateGray ? 1 : 0;
        goto label_7;
      }
    }
    num2 = 0;
label_7:
    deleteFromScript.Enabled = num2 != 0;
    ToolStripMenuItem miSyncWithScript = this.miSyncWithScript;
    int num3;
    if (this.treeView1.SelectedNode != null && this.xModelDoc != null && this.treeView1.SelectedNode.Tag is Object4Script)
    {
      Font nodeFont = this.treeView1.SelectedNode.NodeFont;
      num3 = nodeFont != null ? (nodeFont.Italic ? 1 : 0) : 0;
    }
    else
      num3 = 0;
    miSyncWithScript.Enabled = num3 != 0;
    this.miCollapseAll.Enabled = this.treeView1.SelectedNode != null;
    Label lblLegendDiffs = this.lblLegendDiffs;
    Label lblLegendMissing = this.lblLegendMissing;
    bool flag1;
    this.lblLegendSame.Visible = flag1 = this.xModelDoc != null && this.treeView1.Nodes.Count > 0;
    int num4;
    bool flag2 = (num4 = flag1 ? 1 : 0) != 0;
    lblLegendMissing.Visible = num4 != 0;
    int num5 = flag2 ? 1 : 0;
    lblLegendDiffs.Visible = num5 != 0;
    this.tsbSaveToXml.Enabled = this.xModelDoc != null && this.isUpdated;
    this.lblErrors.Visible = this.errors.Count > 0;
  }

  private void AddObject(int categoryID, string caption, object dbObj, bool addToScript = false)
  {
    TreeNode treeNode = (TreeNode) null;
    if (dbObj is IDBGuid dbGuid)
    {
      Guid guid = dbGuid.GUID;
      if (!CodeFormer.IsGuidAllowableForScript(caption, guid, out string _))
        throw new Exception($"Нельзя добавить {caption} в скрипт автообновления, т.к. в скрипты можно помещать только системные или пользовательские объекты и метаданные.");
      if (this._objects.ContainsKey((object) guid))
      {
        string str = this._objects[(object) guid]?.Caption ?? "нет данных";
        this.errors[guid] = $" ({str}) уже присутствует в списке";
        if (this.suppressErrorDlg)
          return;
        if (IMMessageBox.Show(MessageDialogs.msgError, $"Объект '{str}' уже присутствует в списке !", new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("OK", DialogResult.Yes),
          new IMMessageBoxButton("OK для всех", DialogResult.OK)
        }, IMMessageBoxImage.Error) != DialogResult.OK)
          return;
        this.suppressErrorDlg = true;
        return;
      }
      Object4Script object4Script = new Object4Script(categoryID, (object) guid, caption);
      this._objects.Add((object) guid, object4Script);
      treeNode = this.AddObjectToTreeView(object4Script, dbObj);
    }
    if (this.xModelDoc != null)
    {
      if (addToScript)
      {
        if (treeNode != null)
        {
          this.treeView1.SelectedNode = treeNode;
          this.AddNodeObjectToScript();
        }
      }
      else
      {
        foreach (TreeNode node in this.treeView1.Nodes)
          this.CompareNodeWithXml(node);
      }
    }
    this.RefreshButtons();
  }

  private TreeNode AddObjectToTreeView(Object4Script obj, object dbObj)
  {
    TreeNode treeView = new TreeNode(obj.Caption)
    {
      Tag = (object) obj
    };
    treeView.ImageIndex = treeView.SelectedImageIndex = this.ImageIndex(obj.CategoryID);
    treeView.BackColor = this.treeView1.ForeColor;
    treeView.ForeColor = this.treeView1.BackColor;
    this.treeView1.Nodes.Add(treeView);
    this.AddProperties(obj, dbObj, treeView);
    treeView.Expand();
    return treeView;
  }

  private void AddProperties(List<ScriptNode> props, TreeNode parentNode)
  {
    for (int index = 0; index < props.Count; ++index)
    {
      ScriptNode prop = props[index];
      if (!(prop is ObjectProperty4Script) || ((ObjectProperty4Script) prop).Visible)
      {
        TreeNode treeNode = new TreeNode(prop.Caption)
        {
          Tag = (object) prop
        };
        treeNode.ImageIndex = !(prop is Object4Script) ? (treeNode.SelectedImageIndex = 0) : (treeNode.SelectedImageIndex = this.ImageIndex((prop as Object4Script).CategoryID));
        parentNode.Nodes.Add(treeNode);
        if (prop is Object4Script)
          this.AddProperties((prop as Object4Script).Properties, treeNode);
      }
    }
  }

  private void AddProperties(Object4Script object4, object obj, TreeNode parentNode)
  {
    ICodeFormer codeFormer = this.GetCodeFormer(object4.CategoryID);
    if (codeFormer == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      object4.Properties = codeFormer.GetProperties(sessionKeeper.Session, obj);
    if (object4.Properties == null || object4.Properties.Count <= 0)
      return;
    this.AddProperties(object4.Properties, parentNode);
  }

  private ICodeFormer GetCodeFormer(int categoryID)
  {
    ICodeFormer codeFormer = (ICodeFormer) null;
    switch (categoryID)
    {
      case 2:
        codeFormer = (ICodeFormer) new ObjectCodeFormer();
        break;
      case 3:
        codeFormer = (ICodeFormer) new AttributeCodeFormer();
        break;
      case 4:
        codeFormer = (ICodeFormer) new ObjectTypeCodeFormer();
        break;
      case 6:
        codeFormer = (ICodeFormer) new RelationTypeCodeFormer();
        break;
      case 8:
        codeFormer = (ICodeFormer) new LCLevelCodeFormer();
        break;
      case 9:
        codeFormer = (ICodeFormer) new LanguageCodeFormer();
        break;
      case 11:
        codeFormer = (ICodeFormer) new SubjectAreaCodeFormer();
        break;
      case 12:
        codeFormer = (ICodeFormer) new AttributesGroupCodeFormer();
        break;
      case 16 /*0x10*/:
        codeFormer = (ICodeFormer) new LCSchemaCodeFormer();
        break;
    }
    return codeFormer;
  }

  private void CheckRecursive(TreeNode node, bool value)
  {
    if (node.Tag is ObjectProperty4Script)
    {
      node.Checked = (node.Tag as ObjectProperty4Script).Obligatory = value;
    }
    else
    {
      if (!(node.Tag is Object4Script))
        return;
      node.Checked = value;
      foreach (TreeNode node1 in node.Nodes)
        this.CheckRecursive(node1, value);
    }
  }

  private void UpdateScriptNodeFromTreeNode()
  {
    XContainer baseElement = this.RemoveNodeObjectFromScript();
    if (baseElement == null)
      return;
    this.AddNodeObjectToScript(baseElement);
  }

  private XContainer RemoveNodeObjectFromScript(bool refresh = false)
  {
    if (!(this.treeView1.SelectedNode.Tag is Object4Script tag1))
      return (XContainer) null;
    string objId = tag1.ID.ToString();
    string parentObjId = (this.treeView1.SelectedNode.Parent?.Tag is Object4Script tag2 ? tag2.ID.ToString() : (string) null) ?? (string) null;
    XContainer xcontainer = parentObjId == null ? (XContainer) this.xModelDoc.Descendants((XName) "Objects").FirstOrDefault<XElement>() : (XContainer) this.xModelDoc.Root.Elements((XName) "Object").FirstOrDefault<XElement>((Func<XElement, bool>) (o => o.Attribute((XName) "Guid").Value == parentObjId));
    List<XElement> list = xcontainer != null ? xcontainer.Elements((XName) "Object").Where<XElement>((Func<XElement, bool>) (o => o.Attribute((XName) "Guid").Value == objId)).ToList<XElement>() : (List<XElement>) null;
    if (list != null && list.Count > 0)
    {
      list[0].Remove();
      this.isUpdated = true;
      this.RefreshButtons();
    }
    if (refresh)
      this.CompareNodeWithXml(this.treeView1.SelectedNode, parentObjId == null ? (XContainer) this.xModelDoc : xcontainer);
    return xcontainer;
  }

  private void AddNodeObjectToScript(XContainer baseElement = null)
  {
    if (!(this.treeView1.SelectedNode?.Tag is Object4Script tag1))
      return;
    if (baseElement == null)
    {
      string parentObjId = (this.treeView1.SelectedNode.Parent?.Tag is Object4Script tag2 ? tag2.ID.ToString() : (string) null) ?? (string) null;
      baseElement = parentObjId == null ? (XContainer) this.xModelDoc.Descendants((XName) "Objects").FirstOrDefault<XElement>() : (XContainer) this.xModelDoc.Root.Elements((XName) "Object").FirstOrDefault<XElement>((Func<XElement, bool>) (o => o.Attribute((XName) "Guid").Value == parentObjId));
    }
    if (baseElement == null)
    {
      int num1 = (int) MessageBox.Show("Невозможно добавить объект к скрипту с данной структурой!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      string errmessage;
      XElement xmlElement = this.ConvertObjectToXmlElement(tag1, out errmessage);
      if (xmlElement != null)
      {
        baseElement?.Add((object) xmlElement);
        this.isUpdated = true;
        this.RefreshButtons();
        this.CompareNodeWithXml(this.treeView1.SelectedNode, baseElement);
      }
      if (string.IsNullOrWhiteSpace(errmessage))
        return;
      int num2 = (int) MessageBox.Show(errmessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void ToggleSelectionMode(object sender, EventArgs e)
  {
  }

  private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (e.Node.Tag is Object4Script)
    {
      this.treeView1.AfterCheck -= new TreeViewEventHandler(this.treeView1_AfterCheck);
      foreach (TreeNode node in e.Node.Nodes)
        this.CheckRecursive(node, e.Node.Checked);
      this.treeView1.AfterCheck += new TreeViewEventHandler(this.treeView1_AfterCheck);
    }
    else
    {
      if (!(e.Node.Tag is ObjectProperty4Script))
        return;
      if (e.Node.Checked && !e.Node.Parent.Checked)
      {
        this.treeView1.AfterCheck -= new TreeViewEventHandler(this.treeView1_AfterCheck);
        e.Node.Parent.Checked = true;
        this.treeView1.AfterCheck += new TreeViewEventHandler(this.treeView1_AfterCheck);
      }
      (e.Node.Tag as ObjectProperty4Script).Obligatory = e.Node.Checked;
    }
  }

  public override void Activated()
  {
    if (this.xModelDoc == null)
      this.InitScriptXmlModel();
    this.RefreshButtons();
  }

  private void tsbDeleteRecord_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView1.SelectedNode;
    if (selectedNode == null)
      return;
    if (selectedNode.Tag is Object4Script)
    {
      if (selectedNode.Parent != null && selectedNode.Parent.Tag is Object4Script)
        (selectedNode.Parent.Tag as Object4Script).Properties.Remove((ScriptNode) (selectedNode.Tag as Object4Script));
      else
        this._objects.Remove((selectedNode.Tag as Object4Script).ID);
      this.treeView1.Nodes.Remove(selectedNode);
    }
    else if (selectedNode.Tag is ObjectProperty4Script)
    {
      (selectedNode.Parent.Tag as Object4Script).Properties.Remove((ScriptNode) (selectedNode.Tag as ObjectProperty4Script));
      selectedNode.Parent.Nodes.Remove(selectedNode);
    }
    this.RefreshButtons();
  }

  private void tsbNewScript_Click(object sender, EventArgs e)
  {
    if ((this.xModelDoc == null ? 0 : (this.isUpdated ? 1 : 0)) != 0 && MessageBox.Show("Сохранить существующие данные?", "Новый скрипт", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      if (string.IsNullOrWhiteSpace(this.lblStatus.Text))
        this.tsbStart_Click(sender, e);
      else
        this.tsbSaveToXml_Click(sender, e);
    }
    if (this._objects.Count > 0 || this.treeView1.Nodes.Count > 0)
    {
      this._objects.Clear();
      this.treeView1.Nodes.Clear();
    }
    this.isUpdated = false;
    this.lblStatus.Text = "";
    this.temporaryFiles.Clear();
    this.errors.Clear();
    this.InitScriptXmlModel();
    this.RefreshButtons();
  }

  private void InitScriptXmlModel()
  {
    using (XmlNodeReader reader = new XmlNodeReader((XmlNode) ScriptsEditorControl.CreateBlankScriptXmlDocument()))
    {
      int content = (int) reader.MoveToContent();
      this.xModelDoc = XDocument.Load((XmlReader) reader);
    }
  }

  private void tsbStart_Click(object sender, EventArgs e)
  {
    this.suppressErrorDlg = false;
    this.GenerateScriptXmlFile();
  }

  private string GenerateScriptXmlFile()
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.Filter = "XML files (*.xml)|*.xml";
    saveFileDialog.Title = "Укажите имя xml-файла";
    saveFileDialog.FileName = "plugin";
    saveFileDialog.DefaultExt = "xml";
    saveFileDialog.RestoreDirectory = true;
    if (saveFileDialog.ShowDialog() == DialogResult.OK)
    {
      FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
      XmlDocument scriptXmlDocument = ScriptsEditorControl.CreateBlankScriptXmlDocument();
      XmlNode lastChild = scriptXmlDocument.LastChild;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<XmlNode> xmlNodeList = new List<XmlNode>();
        foreach (KeyValuePair<object, Object4Script> keyValuePair in this._objects)
        {
          ICodeFormer codeFormer = this.GetCodeFormer(keyValuePair.Value.CategoryID);
          if (codeFormer != null)
          {
            XmlNode node = codeFormer.GenerateNode(sessionKeeper.Session, scriptXmlDocument, keyValuePair.Value, fileInfo.Directory.FullName);
            if (node != null)
              lastChild.AppendChild(node);
          }
        }
      }
      scriptXmlDocument.Save(fileInfo.FullName);
      using (XmlNodeReader reader = new XmlNodeReader((XmlNode) scriptXmlDocument))
      {
        int content = (int) reader.MoveToContent();
        this.xModelDoc = XDocument.Load((XmlReader) reader);
      }
    }
    return saveFileDialog.FileName;
  }

  private static XmlDocument CreateBlankScriptXmlDocument()
  {
    XmlDocument scriptXmlDocument = new XmlDocument();
    scriptXmlDocument.AppendChild((XmlNode) scriptXmlDocument.CreateXmlDeclaration("1.0", (string) null, (string) null));
    XmlNode element = (XmlNode) scriptXmlDocument.CreateElement("Objects");
    scriptXmlDocument.AppendChild(element);
    XmlAttribute attribute = scriptXmlDocument.CreateAttribute("PluginVersion");
    attribute.Value = "1.0.0.0";
    element.Attributes.Append(attribute);
    return scriptXmlDocument;
  }

  private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) => this.RefreshButtons();

  private void HandleMouseActions(object sender, MouseEventArgs args)
  {
    if (args.Button != MouseButtons.Right)
      return;
    TreeNode nodeAt = this.treeView1.GetNodeAt(args.X, args.Y);
    this.treeView1.SelectedNode = nodeAt;
    if (nodeAt == null || !(nodeAt.Tag is Object4Script))
      return;
    this.RefreshButtons();
    this.contextMenuTreeList.Show((Control) this.treeView1, new Point(args.X, args.Y));
  }

  private void tsbAddObject_Click(object sender, EventArgs e)
  {
    this.suppressErrorDlg = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] ids = SelectionWindow.SelectObjects("Выбор объекта", "", SelectionOptions.SelectObjects);
      if (ids == null || ids.Length == 0)
        return;
      List<long> longList = new List<long>((IEnumerable<long>) (sessionKeeper.Session.GetCustomService(typeof (IServerBriefcase)) as IServerBriefcase).GetLinkedObjectVersions(sessionKeeper.Session.SessionGUID, 1, ids));
      for (int index = 0; index < ids.Length; ++index)
      {
        if (longList.IndexOf(ids[index]) < 0)
          longList.Add(ids[index]);
      }
      for (int index = 0; index < longList.Count; ++index)
      {
        IDBObject dbObj = sessionKeeper.Session.GetObject(longList[index]);
        if (dbObj.CheckoutBy == 0L)
          this.AddObject(2, dbObj.Caption, (object) dbObj, true);
      }
    }
  }

  private void tsbAddAttributeGroup_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), "Выберите группы атрибутов", typeof (AttributeGroupFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup((int) selectorForm.IDList[index]);
        this.AddObject(12, attributesGroup.GroupName, (object) attributesGroup, true);
      }
    }
  }

  private void tsbAddAttribute_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), "Выберите типы атрибута", typeof (AttributeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((int) selectorForm.IDList[index]);
        this.AddObject(3, attributeType.Name, (object) attributeType, true);
      }
    }
  }

  private void tsbAddObjectType_Click(object sender, EventArgs e)
  {
    this.suppressErrorDlg = false;
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Выберите типы объектов", typeof (ObjectTypeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType((int) selectorForm.IDList[index]);
        if (objectType.Icon == null || objectType.Icon.Length == 0)
          throw new Exception($"Тип объектов \"{objectType.ObjectTypeName}\" не может быть помещен в скрипты автообновления, так как у него отсутствует иконка.");
        this.AddObject(4, objectType.ObjectTypeName, (object) objectType, true);
      }
    }
  }

  private void tsbAddLCSchemas_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (LCSchemasFolder), "Выберите схемы жизненных циклов", typeof (LCSchemaFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBLCSchema lcSchema = sessionKeeper.Session.GetLCSchema((int) selectorForm.IDList[index]);
        this.AddObject(16 /*0x10*/, lcSchema.Name, (object) lcSchema, true);
      }
    }
  }

  private void tsbAddSubjArea_Click(object sender, EventArgs e)
  {
    this.suppressErrorDlg = false;
    SelectorForm selectorForm = new SelectorForm(typeof (AreasFolder), "Выберите предметные области", typeof (AreaFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBSubjectAreaType subjectAreaType = sessionKeeper.Session.GetSubjectAreaType(Convert.ToChar(selectorForm.IDList[index]));
        this.AddObject(11, subjectAreaType.AreaName, (object) subjectAreaType, true);
      }
    }
  }

  private void tsbAddRelationTypes_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), "Выберите тип связей", typeof (RelationTypeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBRelationType relationType = sessionKeeper.Session.GetRelationType((int) selectorForm.IDList[index]);
        if (relationType.Icon == null || relationType.Icon.Length == 0)
          throw new Exception($"Тип связей \"{relationType.Description}\" не может быть помещен в скрипты автообновления, так как у него отсутствует иконка.");
        this.AddObject(6, relationType.Description, (object) relationType, true);
      }
    }
  }

  private void tsbAddLCLevel_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (LevelsFolder), "Выберите уровни продвижения", typeof (LevelFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBLifecycleLevelType lifecycleLevel = sessionKeeper.Session.GetLifecycleLevel((int) selectorForm.IDList[index]);
        this.AddObject(8, lifecycleLevel.LevelName, (object) lifecycleLevel, true);
      }
    }
  }

  private void tsbAddLanguage_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (LanguagesFolder), "Выберите языковой вариант", typeof (LanguageFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        IDBLanguageType language = sessionKeeper.Session.GetLanguage(Convert.ToChar(selectorForm.IDList[index]).ToString());
        this.AddObject(9, language.LanguageName, (object) language, true);
      }
    }
  }

  private void tsbLoadAndCompareXml_Click(object sender, EventArgs e)
  {
    this.suppressErrorDlg = false;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "XML files (*.xml)|*.xml";
    openFileDialog.Title = "Укажите имя xml-файла скрипта обновления";
    openFileDialog.FileName = "Intermech.AVS";
    openFileDialog.DefaultExt = "xml";
    openFileDialog.Multiselect = false;
    openFileDialog.RestoreDirectory = true;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.tsbNewScript_Click(sender, e);
    this.xModelDoc = XDocument.Load(openFileDialog.FileName);
    this.lblStatus.Text = openFileDialog.FileName;
    if (this.treeView1.Nodes.Count == 0)
    {
      this.GenerateTreeNodes();
    }
    else
    {
      foreach (TreeNode node in this.treeView1.Nodes)
        this.CompareNodeWithXml(node);
    }
  }

  private void GenerateTreeNodes()
  {
    if (this.xModelDoc == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.errors.Clear();
      foreach (XElement xelement in this.xModelDoc.Element((XName) "Objects")?.Elements((XName) "Object") ?? Enumerable.Empty<XElement>())
      {
        Guid guid = Guid.Parse(xelement.Attribute((XName) "Guid")?.Value ?? "{00000000-0000-0000-0000-000000000000}");
        int int32 = Convert.ToInt32(xelement.Attribute((XName) "CategoryID")?.Value ?? "0");
        if (!(guid == Guid.Empty))
        {
          int? length;
          switch (int32)
          {
            case 2:
              IDBObject dbObj = sessionKeeper.Session.GetObject(guid, false);
              if (dbObj != null)
              {
                this.AddObject(2, dbObj.Caption, (object) dbObj);
                break;
              }
              this.errors.Add(guid, "не найден в базе");
              break;
            case 3:
              IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(guid, false);
              if (attributeType != null)
              {
                this.AddObject(3, attributeType.Name, (object) attributeType);
                break;
              }
              this.errors.Add(guid, "не найден в базе");
              break;
            case 4:
              IDBObjectType objectType = sessionKeeper.Session.GetObjectType(guid, false);
              length = objectType?.Icon?.Length;
              if ((length ?? 0) > 0)
              {
                this.AddObject(4, objectType.ObjectTypeName, (object) objectType);
                break;
              }
              this.errors.Add(guid, "не найден в базе или содержит неверные данные");
              break;
            case 6:
              IDBRelationType relationType = sessionKeeper.Session.GetRelationType(guid, false);
              length = relationType?.Icon?.Length;
              if ((length ?? 0) > 0)
              {
                this.AddObject(6, relationType.Description, (object) relationType);
                break;
              }
              this.errors.Add(guid, "не найден в базе или содержит неверные данные");
              break;
            case 8:
              IDBLifecycleLevelType lifecycleLevel = sessionKeeper.Session.GetLifecycleLevel(guid, false);
              if (lifecycleLevel != null)
              {
                this.AddObject(8, lifecycleLevel.LevelName, (object) lifecycleLevel);
                break;
              }
              this.errors.Add(guid, "не найден в базе");
              break;
            case 9:
              IDBLanguageType language = sessionKeeper.Session.GetLanguage(guid, false);
              if (language != null)
              {
                this.AddObject(9, language.LanguageName, (object) language);
                break;
              }
              this.errors.Add(guid, "не найден в базе");
              break;
            case 11:
              IDBSubjectAreaType subjectAreaType = sessionKeeper.Session.GetSubjectAreaType(guid, false);
              if (subjectAreaType == null)
              {
                this.errors.Add(guid, "не найден в базе");
                break;
              }
              this.AddObject(11, subjectAreaType.AreaName, (object) subjectAreaType);
              break;
            case 12:
              IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup(guid, false);
              if (attributesGroup != null)
              {
                this.AddObject(12, attributesGroup.GroupName, (object) attributesGroup);
                break;
              }
              this.errors.Add(guid, "не найден в базе");
              break;
            case 16 /*0x10*/:
              IDBLCSchema lcSchema = sessionKeeper.Session.GetLCSchema(guid, false);
              if (lcSchema != null)
              {
                this.AddObject(16 /*0x10*/, lcSchema.Name, (object) lcSchema);
                break;
              }
              this.errors.Add(guid, "не найден в базе");
              break;
          }
          this.CompareNodeWithXml(this.treeView1.Nodes[this.treeView1.Nodes.Count - 1], restoreObligatoryFlag: true);
        }
      }
    }
  }

  private void tsbSaveToXml_Click(object sender, EventArgs e)
  {
    if (string.IsNullOrWhiteSpace(this.lblStatus.Text))
    {
      this.lblStatus.Text = this.GenerateScriptXmlFile();
      this.isUpdated = false;
      this.RefreshButtons();
    }
    else
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      FileInfo fileInfo = new FileInfo(this.lblStatus.Text);
      saveFileDialog.Filter = "XML files (*.xml)|*.xml";
      saveFileDialog.Title = "Укажите имя xml-файла";
      saveFileDialog.InitialDirectory = fileInfo.DirectoryName;
      saveFileDialog.FileName = fileInfo.Name;
      saveFileDialog.DefaultExt = "xml";
      saveFileDialog.RestoreDirectory = true;
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        this.xModelDoc.Save(saveFileDialog.FileName);
        this.SaveTempFiles(Path.GetDirectoryName(saveFileDialog.FileName));
        this.isUpdated = false;
        this.lblStatus.Text = saveFileDialog.FileName;
        this.RefreshButtons();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, ex.Message ?? "", "Ошибка");
      }
    }
  }

  private void SaveTempFiles(string targetDir)
  {
    if (this.temporaryFiles.Count == 0)
      return;
    foreach (string temporaryFile in this.temporaryFiles)
    {
      string str1 = Path.GetFileName(temporaryFile).Equals(temporaryFile, StringComparison.CurrentCultureIgnoreCase) ? Path.Combine(Path.GetTempPath(), temporaryFile) : temporaryFile;
      if (File.Exists(str1))
      {
        string fileName = Path.GetFileName(temporaryFile);
        string str2 = Path.Combine(targetDir, fileName);
        try
        {
          if (File.Exists(str2))
            File.Delete(str2);
          File.Move(str1, str2);
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, $"Невозможно переместить файл {fileName}.", "Ошибка");
        }
      }
    }
  }

  private void lblErrors_Click(object sender, EventArgs e)
  {
    int num = (int) new ErrorListForm()
    {
      Errors = this.errors.Select<KeyValuePair<Guid, string>, string>((Func<KeyValuePair<Guid, string>, string>) (d => $"Объект с id = {d.Key}:\r\n{d.Value}")).ToList<string>()
    }.ShowDialog();
  }

  private int CompareNodeWithXml(TreeNode node, XContainer xnode = null, bool restoreObligatoryFlag = false)
  {
    bool flag = true;
    xnode = xnode ?? (XContainer) this.xModelDoc.Elements((XName) "Objects").FirstOrDefault<XElement>();
    if (xnode == null)
      return 0;
    Object4Script object4;
    if ((object4 = node.Tag as Object4Script) != null)
    {
      XElement xelement = xnode.Elements((XName) "Object").FirstOrDefault<XElement>((Func<XElement, bool>) (oe => oe.Attribute((XName) "Guid").Value == object4.ID.ToString()));
      Font font = node.NodeFont ?? this.treeView1.Font;
      if (xelement == null)
      {
        node.NodeFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
        node.BackColor = Color.White;
        node.ForeColor = Color.Red;
        node.Nodes.OfType<TreeNode>().ToList<TreeNode>().ForEach((Action<TreeNode>) (n => n.ForeColor = Color.Red));
        return -1;
      }
      Dictionary<string, string> diffs = new Dictionary<string, string>();
      flag = flag && this.CompareObjProperties(object4, xelement, out diffs);
      if (!flag && diffs.Count == 0)
      {
        node.BackColor = Color.White;
        node.ForeColor = Color.SlateGray;
        node.Nodes.OfType<TreeNode>().ToList<TreeNode>().ForEach((Action<TreeNode>) (n => n.ForeColor = Color.SlateGray));
        return 0;
      }
      if (!flag)
      {
        if (diffs.Count > 0)
        {
          foreach (TreeNode treeNode in node.Nodes.OfType<TreeNode>().Where<TreeNode>((Func<TreeNode, bool>) (n => n.Tag is ObjectProperty4Script)))
          {
            string key = (treeNode.Tag as ObjectProperty4Script).PropertyID.ToString();
            if (diffs.ContainsKey(key))
            {
              treeNode.ForeColor = !string.IsNullOrEmpty(diffs[key]) ? Color.Blue : Color.Red;
              if (restoreObligatoryFlag)
              {
                if (diffs[key].Contains("O"))
                {
                  treeNode.Checked = true;
                  (treeNode.Tag as ObjectProperty4Script).Obligatory = true;
                }
                if (diffs[key] == "O")
                  treeNode.ForeColor = Color.Black;
              }
            }
            else
              treeNode.ForeColor = Color.Black;
          }
        }
        foreach (TreeNode node1 in node.Nodes.OfType<TreeNode>().Where<TreeNode>((Func<TreeNode, bool>) (n => n.Tag is Object4Script)))
        {
          int num = this.CompareNodeWithXml(node1, (XContainer) xelement);
          flag = flag && num == 0;
          switch (num)
          {
            case -1:
              node1.ForeColor = Color.Red;
              continue;
            case 1:
              node1.ForeColor = Color.Blue;
              continue;
            default:
              node1.ForeColor = Color.Black;
              continue;
          }
        }
      }
      if (!flag)
      {
        node.NodeFont = new Font(font.FontFamily, font.Size, FontStyle.Italic);
        node.ForeColor = Color.Blue;
        node.BackColor = Color.White;
      }
      else
      {
        node.NodeFont = new Font(font.FontFamily, font.Size, FontStyle.Regular);
        node.ForeColor = Color.Black;
        node.BackColor = Color.White;
        node.Nodes.OfType<TreeNode>().ToList<TreeNode>().ForEach((Action<TreeNode>) (n => n.ForeColor = Color.Black));
      }
    }
    return !flag ? 1 : 0;
  }

  private bool CompareObjProperties(
    Object4Script treeNodeObj,
    XElement xmlScriptObjElement,
    out Dictionary<string, string> diffs)
  {
    bool flag1 = true;
    diffs = new Dictionary<string, string>();
    XElement xmlElement = this.ConvertObjectToXmlElement(treeNodeObj, out string _);
    if (xmlElement == null)
      return false;
    if (xmlScriptObjElement.ToString().Equals(xmlElement.ToString(), StringComparison.CurrentCulture))
      return true;
    foreach (XElement descendant in xmlElement.Descendants((XName) "Property"))
    {
      XElement prop = descendant;
      string objGuid = prop.Parent?.Attribute((XName) "Guid").Value ?? "";
      IEnumerable<XElement> source = xmlScriptObjElement.Descendants((XName) "Property").Where<XElement>((Func<XElement, bool>) (p => (p.Parent?.Attribute((XName) "Guid").Value ?? "") == objGuid && p.Attribute((XName) "Id").Value == prop.Attribute((XName) "Id").Value));
      if (source == null || !source.Any<XElement>())
      {
        diffs[prop.Attribute((XName) "Id").Value] = "";
        flag1 = false;
      }
      else
      {
        bool flag2 = true;
        foreach (XElement xelement1 in source)
        {
          flag2 = true;
          if (prop.Attribute((XName) "Obligatory").Value != xelement1.Attribute((XName) "Obligatory").Value)
          {
            diffs[prop.Attribute((XName) "Id").Value] = "O";
            flag2 = false;
          }
          XElement xelement2 = prop.Descendants((XName) "PropValue").FirstOrDefault<XElement>();
          XElement xelement3 = xelement1.Descendants((XName) "PropValue").FirstOrDefault<XElement>();
          if (xelement3 != null && xelement2 != null)
          {
            if (xelement3.Attribute((XName) "Value").Value != xelement2.Attribute((XName) "Value").Value)
            {
              string str = diffs.ContainsKey(prop.Attribute((XName) "Id").Value) ? diffs[prop.Attribute((XName) "Id").Value] + "V" : "V";
              diffs[prop.Attribute((XName) "Id").Value] = str;
              flag2 = false;
            }
          }
          else if (xelement2 != null || xelement3 != null)
          {
            string str = diffs.ContainsKey(prop.Attribute((XName) "Id").Value) ? diffs[prop.Attribute((XName) "Id").Value] + "V" : "V";
            diffs[prop.Attribute((XName) "Id").Value] = str;
            flag2 = false;
          }
          if (flag2)
          {
            if (diffs.ContainsKey(prop.Attribute((XName) "Id").Value))
            {
              diffs.Remove(prop.Attribute((XName) "Id").Value);
              break;
            }
            break;
          }
        }
        if (!flag2)
          flag1 = false;
      }
    }
    return flag1;
  }

  private XElement ConvertObjectToXmlElement(Object4Script treeNodeObj, out string errmessage)
  {
    errmessage = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICodeFormer codeFormer = this.GetCodeFormer(treeNodeObj.CategoryID);
      if (codeFormer == null)
        return (XElement) null;
      codeFormer.FailOnError = false;
      XmlNode xmlNode = (XmlNode) null;
      Guid key = Guid.Parse(treeNodeObj.ID.ToString());
      string str = treeNodeObj?.Caption ?? "нет данных";
      try
      {
        xmlNode = codeFormer.GenerateNode(sessionKeeper.Session, new XmlDocument(), treeNodeObj, Path.GetTempPath());
        this.temporaryFiles.AddRange(codeFormer.TempFilePaths);
        if (codeFormer.Errors.Count > 0)
        {
          errmessage = $"Скрипт для '{str}' создан с ошибками.";
          foreach (string error in codeFormer.Errors)
          {
            if (this.errors.ContainsKey(key))
              this.errors[key] = $"{this.errors[key]}\r\n{error}";
            else
              this.errors.Add(key, error);
          }
        }
      }
      catch (Exception ex)
      {
        errmessage = $" '{str}' не может быть создан в скрипте ({ex.Message})";
        if (!this.errors.ContainsKey(key))
          this.errors.Add(key, errmessage);
      }
      if (xmlNode != null)
        return XElement.Load(xmlNode.CreateNavigator().ReadSubtree());
    }
    return (XElement) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ScriptsEditorControl));
    this.panel1 = new Panel();
    this.lblErrors = new Label();
    this.lblLegendSame = new Label();
    this.lblLegendDiffs = new Label();
    this.lblLegendMissing = new Label();
    this.lblStatus = new Label();
    this.panel2 = new Panel();
    this.treeView1 = new TreeView();
    this.imageList1 = new ImageList(this.components);
    this.tsbAddObject = new ToolStripButton();
    this.tsbAddAttributeGroup = new ToolStripButton();
    this.tsbAddAttribute = new ToolStripButton();
    this.tsbAddObjectType = new ToolStripButton();
    this.tsbAddLCSchemas = new ToolStripButton();
    this.tsbAddSubjArea = new ToolStripButton();
    this.tsbAddRelationTypes = new ToolStripButton();
    this.tsbAddLCLevel = new ToolStripButton();
    this.tsbAddLanguage = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.toolStripLabel3 = new ToolStripLabel();
    this.tsbStart = new ToolStripButton();
    this.toolStripLabel2 = new ToolStripLabel();
    this.tsbDeleteRecord = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.tsbNewScript = new ToolStripButton();
    this.toolStrip1 = new ToolStrip();
    this.tsbLoadAndCompareXml = new ToolStripButton();
    this.tsbSaveToXml = new ToolStripButton();
    this.contextMenuTreeList = new ContextMenuStrip(this.components);
    this.miAddToScript = new ToolStripMenuItem();
    this.miSyncWithScript = new ToolStripMenuItem();
    this.miDeleteFromScript = new ToolStripMenuItem();
    this.miCollapseAll = new ToolStripMenuItem();
    this.miExpandAll = new ToolStripMenuItem();
    this.miMultiSelect = new ToolStripMenuItem();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.contextMenuTreeList.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.lblErrors);
    this.panel1.Controls.Add((Control) this.lblLegendSame);
    this.panel1.Controls.Add((Control) this.lblLegendDiffs);
    this.panel1.Controls.Add((Control) this.lblLegendMissing);
    this.panel1.Controls.Add((Control) this.lblStatus);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.lblErrors, "lblErrors");
    this.lblErrors.Cursor = Cursors.Hand;
    this.lblErrors.ForeColor = Color.Navy;
    this.lblErrors.Name = "lblErrors";
    this.lblErrors.Click += new EventHandler(this.lblErrors_Click);
    componentResourceManager.ApplyResources((object) this.lblLegendSame, "lblLegendSame");
    this.lblLegendSame.ForeColor = Color.Black;
    this.lblLegendSame.Name = "lblLegendSame";
    componentResourceManager.ApplyResources((object) this.lblLegendDiffs, "lblLegendDiffs");
    this.lblLegendDiffs.ForeColor = Color.Blue;
    this.lblLegendDiffs.Name = "lblLegendDiffs";
    componentResourceManager.ApplyResources((object) this.lblLegendMissing, "lblLegendMissing");
    this.lblLegendMissing.ForeColor = Color.Red;
    this.lblLegendMissing.Name = "lblLegendMissing";
    componentResourceManager.ApplyResources((object) this.lblStatus, "lblStatus");
    this.lblStatus.Name = "lblStatus";
    this.panel2.Controls.Add((Control) this.treeView1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.treeView1.CheckBoxes = true;
    componentResourceManager.ApplyResources((object) this.treeView1, "treeView1");
    this.treeView1.ImageList = this.imageList1;
    this.treeView1.Name = "treeView1";
    this.treeView1.AfterCheck += new TreeViewEventHandler(this.treeView1_AfterCheck);
    this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "folder.ico");
    this.imageList1.Images.SetKeyName(1, "Object-Bring Forward.png");
    this.imageList1.Images.SetKeyName(2, "Атрибуты автоподбора.ico");
    this.imageList1.Images.SetKeyName(3, "Атрибут.ico");
    this.imageList1.Images.SetKeyName(4, "Типы объектов.ico");
    this.imageList1.Images.SetKeyName(5, "Схемы жизненного цикла.ico");
    this.imageList1.Images.SetKeyName(6, "Предметные области.ico");
    this.imageList1.Images.SetKeyName(7, "Типы связей.ico");
    this.imageList1.Images.SetKeyName(8, "Уровни продвижения.ico");
    this.imageList1.Images.SetKeyName(9, "Языковые варианты.ico");
    this.tsbAddObject.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddObject, "tsbAddObject");
    this.tsbAddObject.Name = "tsbAddObject";
    this.tsbAddObject.Click += new EventHandler(this.tsbAddObject_Click);
    this.tsbAddAttributeGroup.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddAttributeGroup, "tsbAddAttributeGroup");
    this.tsbAddAttributeGroup.Name = "tsbAddAttributeGroup";
    this.tsbAddAttributeGroup.Click += new EventHandler(this.tsbAddAttributeGroup_Click);
    this.tsbAddAttribute.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddAttribute, "tsbAddAttribute");
    this.tsbAddAttribute.Name = "tsbAddAttribute";
    this.tsbAddAttribute.Click += new EventHandler(this.tsbAddAttribute_Click);
    this.tsbAddObjectType.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddObjectType, "tsbAddObjectType");
    this.tsbAddObjectType.Name = "tsbAddObjectType";
    this.tsbAddObjectType.Click += new EventHandler(this.tsbAddObjectType_Click);
    this.tsbAddLCSchemas.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddLCSchemas, "tsbAddLCSchemas");
    this.tsbAddLCSchemas.Name = "tsbAddLCSchemas";
    this.tsbAddLCSchemas.Click += new EventHandler(this.tsbAddLCSchemas_Click);
    this.tsbAddSubjArea.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddSubjArea, "tsbAddSubjArea");
    this.tsbAddSubjArea.Name = "tsbAddSubjArea";
    this.tsbAddSubjArea.Click += new EventHandler(this.tsbAddSubjArea_Click);
    this.tsbAddRelationTypes.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddRelationTypes, "tsbAddRelationTypes");
    this.tsbAddRelationTypes.Name = "tsbAddRelationTypes";
    this.tsbAddRelationTypes.Click += new EventHandler(this.tsbAddRelationTypes_Click);
    this.tsbAddLCLevel.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddLCLevel, "tsbAddLCLevel");
    this.tsbAddLCLevel.Name = "tsbAddLCLevel";
    this.tsbAddLCLevel.Click += new EventHandler(this.tsbAddLCLevel_Click);
    this.tsbAddLanguage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbAddLanguage, "tsbAddLanguage");
    this.tsbAddLanguage.Name = "tsbAddLanguage";
    this.tsbAddLanguage.Click += new EventHandler(this.tsbAddLanguage_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    componentResourceManager.ApplyResources((object) this.toolStripLabel3, "toolStripLabel3");
    this.toolStripLabel3.Name = "toolStripLabel3";
    this.tsbStart.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbStart, "tsbStart");
    this.tsbStart.Name = "tsbStart";
    this.tsbStart.Click += new EventHandler(this.tsbStart_Click);
    componentResourceManager.ApplyResources((object) this.toolStripLabel2, "toolStripLabel2");
    this.toolStripLabel2.Name = "toolStripLabel2";
    this.tsbDeleteRecord.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbDeleteRecord, "tsbDeleteRecord");
    this.tsbDeleteRecord.Name = "tsbDeleteRecord";
    this.tsbDeleteRecord.Click += new EventHandler(this.tsbDeleteRecord_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this.tsbNewScript.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbNewScript.Image = (Image) Resources.ScriptNew;
    componentResourceManager.ApplyResources((object) this.tsbNewScript, "tsbNewScript");
    this.tsbNewScript.Name = "tsbNewScript";
    this.tsbNewScript.Click += new EventHandler(this.tsbNewScript_Click);
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.Items.AddRange(new ToolStripItem[18]
    {
      (ToolStripItem) this.tsbAddObject,
      (ToolStripItem) this.tsbAddAttributeGroup,
      (ToolStripItem) this.tsbAddAttribute,
      (ToolStripItem) this.tsbAddObjectType,
      (ToolStripItem) this.tsbAddLCSchemas,
      (ToolStripItem) this.tsbAddSubjArea,
      (ToolStripItem) this.tsbAddRelationTypes,
      (ToolStripItem) this.tsbAddLCLevel,
      (ToolStripItem) this.tsbAddLanguage,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.toolStripLabel3,
      (ToolStripItem) this.tsbStart,
      (ToolStripItem) this.toolStripLabel2,
      (ToolStripItem) this.tsbDeleteRecord,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.tsbNewScript,
      (ToolStripItem) this.tsbLoadAndCompareXml,
      (ToolStripItem) this.tsbSaveToXml
    });
    this.toolStrip1.Name = "toolStrip1";
    this.tsbLoadAndCompareXml.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbLoadAndCompareXml, "tsbLoadAndCompareXml");
    this.tsbLoadAndCompareXml.Name = "tsbLoadAndCompareXml";
    this.tsbLoadAndCompareXml.Click += new EventHandler(this.tsbLoadAndCompareXml_Click);
    this.tsbSaveToXml.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbSaveToXml, "tsbSaveToXml");
    this.tsbSaveToXml.Name = "tsbSaveToXml";
    this.tsbSaveToXml.Click += new EventHandler(this.tsbSaveToXml_Click);
    this.contextMenuTreeList.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.miAddToScript,
      (ToolStripItem) this.miSyncWithScript,
      (ToolStripItem) this.miDeleteFromScript,
      (ToolStripItem) this.miCollapseAll,
      (ToolStripItem) this.miExpandAll
    });
    this.contextMenuTreeList.Name = "contextMenuStripOutputMapping";
    componentResourceManager.ApplyResources((object) this.contextMenuTreeList, "contextMenuTreeList");
    this.miAddToScript.Name = "miAddToScript";
    componentResourceManager.ApplyResources((object) this.miAddToScript, "miAddToScript");
    this.miSyncWithScript.Name = "miSyncWithScript";
    componentResourceManager.ApplyResources((object) this.miSyncWithScript, "miSyncWithScript");
    this.miDeleteFromScript.Name = "miDeleteFromScript";
    componentResourceManager.ApplyResources((object) this.miDeleteFromScript, "miDeleteFromScript");
    this.miCollapseAll.Name = "miCollapseAll";
    componentResourceManager.ApplyResources((object) this.miCollapseAll, "miCollapseAll");
    this.miExpandAll.Name = "miExpandAll";
    componentResourceManager.ApplyResources((object) this.miExpandAll, "miExpandAll");
    this.miMultiSelect.CheckOnClick = true;
    this.miMultiSelect.Name = "miMultiSelect";
    componentResourceManager.ApplyResources((object) this.miMultiSelect, "miMultiSelect");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = nameof (ScriptsEditorControl);
    this.TabImage = (Image) Resources.ScriptNew;
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.contextMenuTreeList.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
