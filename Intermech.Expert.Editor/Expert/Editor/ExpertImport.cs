// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpertImport
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
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

public class ExpertImport : Form
{
  private SortedList<string, ExpObjInfo> expObjList = new SortedList<string, ExpObjInfo>();
  public SortedDictionary<int, GuidAndName> attrTypes = new SortedDictionary<int, GuidAndName>();
  public SortedDictionary<int, GuidAndName> objTypes = new SortedDictionary<int, GuidAndName>();
  public Dictionary<int, GuidAndName> relTypes = new Dictionary<int, GuidAndName>();
  public Dictionary<long, GuidAndName> objIdents = new Dictionary<long, GuidAndName>();
  private SortedList<string, int> templTypes = new SortedList<string, int>();
  public string ImportPath = "";
  public List<ExpertImport.ImportFixup> fixupList = new List<ExpertImport.ImportFixup>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnSelDirectory;
  private TextBox tbExportFolder;
  private Panel panel1;
  private Button btnStart;
  private Button btnClose;
  private FolderBrowserDialog fbd;
  private Panel panel2;
  private Panel panel3;
  private Panel panel4;
  private Label label1;
  private TreeView tv;
  private Label label2;
  private ImageList IL;
  private TreeView uniTV;
  private ProgressBar pBar;
  private Panel panel5;
  private Label label3;
  private ListBox lbLog;
  private Splitter splitter1;
  private Splitter splitter2;

  public ExpertImport() => this.InitializeComponent();

  public void Execute()
  {
    int num = (int) this.ShowDialog();
  }

  private void btnSelDirectory_Click(object sender, EventArgs e)
  {
    if (this.fbd.ShowDialog() != DialogResult.OK)
      return;
    this.ImportPath = this.fbd.SelectedPath;
    if (!this.ImportPath.EndsWith("\\"))
      this.ImportPath += "\\";
    if (this.ValidateFolder())
    {
      this.tbExportFolder.Text = this.ImportPath;
      this.FillExpObjList();
      this.FillScriptTree();
      this.FillUniTV();
      this.lbLog.Items.Clear();
      this.ImportAttrTypes();
      this.ImportObjTypes();
      this.ImportRelTypes();
      this.ImportObjects();
    }
    else
      this.ImportPath = "";
  }

  private bool ValidateFolder()
  {
    if (!File.Exists(this.ImportPath + "_MASTER.XML"))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_640") + LocalizationHolder.rm.GetString("Expert.Editor_641"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
      return false;
    }
    if (!File.Exists(this.ImportPath + "_SCRIPTS.XML"))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_640") + LocalizationHolder.rm.GetString("Expert.Editor_669"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
      return false;
    }
    if (!File.Exists(this.ImportPath + "OBJ_TYPES.XML"))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_640") + LocalizationHolder.rm.GetString("Expert.Editor_642"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
      return false;
    }
    if (!File.Exists(this.ImportPath + "ATTR_TYPES.XML"))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_640") + LocalizationHolder.rm.GetString("Expert.Editor_643"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
      return false;
    }
    if (!File.Exists(this.ImportPath + "REL_TYPES.XML"))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_640") + LocalizationHolder.rm.GetString("Expert.Editor_644"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
      return false;
    }
    if (File.Exists(this.ImportPath + "OBJ_LINKS.XML"))
      return true;
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_640") + LocalizationHolder.rm.GetString("Expert.Editor_645"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
    return false;
  }

  private void FillExpObjList()
  {
    this.expObjList.Clear();
    DataTable dataTable1 = new DataTable();
    int num1 = (int) dataTable1.ReadXml(this.ImportPath + "_SCRIPTS.XML");
    int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00134-306c-11d8-b4e9-00304f19f545"));
    this.templTypes.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      string key1 = Convert.ToString(row[0]);
      long int64 = Convert.ToInt64(row[1]);
      string key2 = Convert.ToString(row[2]);
      string g = Convert.ToString(row[3]);
      ExpObjInfo expObjInfo = this.expObjList.ContainsKey(key1) ? this.expObjList[key1] : new ExpObjInfo(int64);
      if (!this.expObjList.ContainsKey(key1))
      {
        this.expObjList.Add(key1, expObjInfo);
        expObjInfo.scriptName = key1;
        expObjInfo.templateName = key2;
      }
      if (!this.templTypes.ContainsKey(key2))
      {
        int num2 = MetaDataHelper.GetObjectTypeID(new Guid(g));
        switch (num2)
        {
          case -1:
          case 0:
            num2 = objectTypeId;
            break;
        }
        this.templTypes.Add(key2, num2);
      }
    }
    DataTable dataTable2 = new DataTable();
    int num3 = (int) dataTable2.ReadXml(this.ImportPath + "_MASTER.XML");
    DataTable dataTable3 = new DataTable();
    int num4 = (int) dataTable3.ReadXml(this.ImportPath + "ATTR_TYPES.XML");
    DataTable dataTable4 = new DataTable();
    int num5 = (int) dataTable4.ReadXml(this.ImportPath + "OBJ_TYPES.XML");
    DataTable dataTable5 = new DataTable();
    int num6 = (int) dataTable5.ReadXml(this.ImportPath + "REL_TYPES.XML");
    DataTable dataTable6 = new DataTable();
    int num7 = (int) dataTable6.ReadXml(this.ImportPath + "OBJ_LINKS.XML");
    foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
    {
      string key = Convert.ToString(row[0]);
      Convert.ToInt64(row[1]);
      int int32 = Convert.ToInt32(row[2]);
      long int64 = Convert.ToInt64(row[3]);
      ExpObjInfo expObj = this.expObjList[key];
      switch (int32)
      {
        case 0:
          DataRow[] dataRowArray1 = dataTable3.Select("F_ATTRIBUTE_ID = " + Convert.ToString((int) int64));
          if (dataRowArray1.Length != 0 && !expObj.attrTypes.ContainsKey((int) int64))
          {
            GuidAndName guidAndName = new GuidAndName(new Guid(Convert.ToString(dataRowArray1[0]["F_GUID"])), Convert.ToString(dataRowArray1[0]["F_NAME"]));
            expObj.attrTypes.Add((int) int64, guidAndName);
            if (!this.attrTypes.ContainsKey((int) int64))
            {
              this.attrTypes.Add((int) int64, guidAndName);
              continue;
            }
            continue;
          }
          continue;
        case 1:
          DataRow[] dataRowArray2 = dataTable4.Select("F_OBJECT_TYPE = " + Convert.ToString((int) int64));
          if (dataRowArray2.Length != 0 && !expObj.objTypes.ContainsKey((int) int64))
          {
            GuidAndName guidAndName = new GuidAndName(new Guid(Convert.ToString(dataRowArray2[0]["F_GUID"])), Convert.ToString(dataRowArray2[0]["F_OBJ_NAME"]));
            expObj.objTypes.Add((int) int64, guidAndName);
            if (!this.objTypes.ContainsKey((int) int64))
            {
              this.objTypes.Add((int) int64, guidAndName);
              continue;
            }
            continue;
          }
          continue;
        case 2:
          DataRow[] dataRowArray3 = dataTable5.Select("F_ID = " + Convert.ToString((int) int64));
          if (dataRowArray3.Length != 0 && !expObj.relTypes.ContainsKey((int) int64))
          {
            GuidAndName guidAndName = new GuidAndName(new Guid(Convert.ToString(dataRowArray3[0]["F_GUID"])), Convert.ToString(dataRowArray3[0]["F_NAME"]));
            expObj.relTypes.Add((int) int64, guidAndName);
            if (!this.relTypes.ContainsKey((int) int64))
            {
              this.relTypes.Add((int) int64, guidAndName);
              continue;
            }
            continue;
          }
          continue;
        case 3:
          DataRow[] dataRowArray4 = dataTable6.Select("F_ID = " + Convert.ToString(int64));
          if (dataRowArray4.Length != 0 && !expObj.objIdents.ContainsKey(int64))
          {
            GuidAndName guidAndName = new GuidAndName(new Guid(Convert.ToString(dataRowArray4[0]["F_GUID"])), Convert.ToString(dataRowArray4[0]["F_NAME"]));
            expObj.objIdents.Add((long) (int) int64, guidAndName);
            if (!this.objIdents.ContainsKey((long) (int) int64))
            {
              this.objIdents.Add((long) (int) int64, guidAndName);
              continue;
            }
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  private void FillScriptTree()
  {
    this.tv.Nodes.Clear();
    foreach (string key1 in this.expObjList.Keys.ToList<string>())
    {
      ExpObjInfo expObj = this.expObjList[key1];
      TreeNode node1 = new TreeNode($"{expObj.scriptName} [{Convert.ToString(expObj.objID)}]")
      {
        ImageIndex = 5
      };
      node1.SelectedImageIndex = node1.ImageIndex;
      this.tv.Nodes.Add(node1);
      List<int> list1 = expObj.attrTypes.Keys.ToList<int>();
      if (list1.Count > 0)
      {
        TreeNode node2 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_647"))
        {
          ImageIndex = 0
        };
        node2.SelectedImageIndex = node2.ImageIndex;
        node1.Nodes.Add(node2);
        foreach (int key2 in list1)
        {
          GuidAndName attrType = expObj.attrTypes[key2];
          TreeNode node3 = new TreeNode($"{attrType.Name} [{attrType.g.ToString()}] ({Convert.ToString(key2)})")
          {
            ImageIndex = 1
          };
          node3.SelectedImageIndex = node3.ImageIndex;
          node2.Nodes.Add(node3);
        }
      }
      List<int> list2 = expObj.objTypes.Keys.ToList<int>();
      if (list2.Count > 0)
      {
        TreeNode node4 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_648"))
        {
          ImageIndex = 0
        };
        node4.SelectedImageIndex = node4.ImageIndex;
        node1.Nodes.Add(node4);
        foreach (int key3 in list2)
        {
          GuidAndName objType = expObj.objTypes[key3];
          TreeNode node5 = new TreeNode($"{objType.Name} [{objType.g.ToString()}] ({Convert.ToString(key3)})")
          {
            ImageIndex = 2
          };
          node5.SelectedImageIndex = node5.ImageIndex;
          node4.Nodes.Add(node5);
        }
      }
      List<int> list3 = expObj.relTypes.Keys.ToList<int>();
      if (list3.Count > 0)
      {
        TreeNode node6 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_649"))
        {
          ImageIndex = 0
        };
        node6.SelectedImageIndex = node6.ImageIndex;
        node1.Nodes.Add(node6);
        foreach (int key4 in list3)
        {
          GuidAndName relType = expObj.relTypes[key4];
          TreeNode node7 = new TreeNode($"{relType.Name} [{relType.g.ToString()}] ({Convert.ToString(key4)})")
          {
            ImageIndex = 3
          };
          node7.SelectedImageIndex = node7.ImageIndex;
          node6.Nodes.Add(node7);
        }
      }
      List<long> list4 = expObj.objIdents.Keys.ToList<long>();
      if (list4.Count > 0)
      {
        TreeNode node8 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_650"))
        {
          ImageIndex = 0
        };
        node8.SelectedImageIndex = node8.ImageIndex;
        node1.Nodes.Add(node8);
        foreach (long key5 in list4)
        {
          GuidAndName objIdent = expObj.objIdents[key5];
          TreeNode node9 = new TreeNode($"{objIdent.Name} [{objIdent.g.ToString()}] ({Convert.ToString(key5)})")
          {
            ImageIndex = 4
          };
          node9.SelectedImageIndex = node9.ImageIndex;
          node8.Nodes.Add(node9);
        }
      }
    }
  }

  private void FillUniTV()
  {
    this.uniTV.Nodes.Clear();
    TreeNode node1 = new TreeNode(LocalizationHolder.rm.GetString("Expert.Editor_636"));
    node1.ImageIndex = 0;
    this.uniTV.Nodes.Add(node1);
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
    this.uniTV.Nodes.Add(node3);
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
    this.uniTV.Nodes.Add(node5);
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
    this.uniTV.Nodes.Add(node7);
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
    this.uniTV.ExpandAll();
  }

  public void PerformCriticalError(
    ExpertImport.InfoType it,
    long Id,
    string newGuid,
    string errMessage)
  {
    this.fixupList.Add(new ExpertImport.ImportFixup(it, ExpertImport.ImportAction.iaCriticalErr, Id, newGuid));
    StringBuilder stringBuilder = new StringBuilder(LocalizationHolder.rm.GetString("Expert.Editor_651"));
    switch (it)
    {
      case ExpertImport.InfoType.itAttrType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_652"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_653"), (object) Id);
        break;
      case ExpertImport.InfoType.itRelType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_654"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjRef:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_670"), (object) Id);
        break;
    }
    if (errMessage != "")
      stringBuilder.Append(errMessage);
    stringBuilder.Append(LocalizationHolder.rm.GetString("Expert.Editor_656"));
    this.lbLog.Items.Add((object) stringBuilder.ToString());
  }

  public void PerformGUIDChange(ExpertImport.InfoType it, long Id, string newGuid)
  {
    this.fixupList.Add(new ExpertImport.ImportFixup(it, ExpertImport.ImportAction.iaReplaceGuid, Id, newGuid));
    StringBuilder stringBuilder = new StringBuilder(LocalizationHolder.rm.GetString("Expert.Editor_659"));
    switch (it)
    {
      case ExpertImport.InfoType.itAttrType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_652"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_653"), (object) Id);
        break;
      case ExpertImport.InfoType.itRelType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_654"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjRef:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_670"), (object) Id);
        break;
    }
    stringBuilder.Append(LocalizationHolder.rm.GetString("Expert.Editor_658"));
    this.lbLog.Items.Add((object) stringBuilder.ToString());
  }

  public void PerformIdentChange(ExpertImport.InfoType it, long Id, string newGuid)
  {
    this.fixupList.Add(new ExpertImport.ImportFixup(it, ExpertImport.ImportAction.iaReplaceId, Id, newGuid));
    StringBuilder stringBuilder = new StringBuilder(LocalizationHolder.rm.GetString("Expert.Editor_659"));
    switch (it)
    {
      case ExpertImport.InfoType.itAttrType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_652"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_653"), (object) Id);
        break;
      case ExpertImport.InfoType.itRelType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_654"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjRef:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_670"), (object) Id);
        break;
    }
    stringBuilder.Append(LocalizationHolder.rm.GetString("Expert.Editor_657"));
    this.lbLog.Items.Add((object) stringBuilder.ToString());
  }

  public void PerformNameChange(ExpertImport.InfoType it, long Id, string newGuid)
  {
    this.fixupList.Add(new ExpertImport.ImportFixup(it, ExpertImport.ImportAction.iaReplaceName, Id, newGuid));
    StringBuilder stringBuilder = new StringBuilder(LocalizationHolder.rm.GetString("Expert.Editor_659"));
    switch (it)
    {
      case ExpertImport.InfoType.itAttrType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_652"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_653"), (object) Id);
        break;
      case ExpertImport.InfoType.itRelType:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_654"), (object) Id);
        break;
      case ExpertImport.InfoType.itObjRef:
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Expert.Editor_670"), (object) Id);
        break;
    }
    stringBuilder.Append(LocalizationHolder.rm.GetString("Expert.Editor_660"));
    this.lbLog.Items.Add((object) stringBuilder.ToString());
  }

  private bool PerformAttrType(DataRow dr)
  {
    int int32_1 = Convert.ToInt32(dr["F_ATTRIBUTE_ID"]);
    string anAttributeName = Convert.ToString(dr["F_NAME"]);
    string str = Convert.ToString(dr["F_GUID"]);
    FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(dr["F_ATTRIBUTE_TYPE"]);
    Guid attrTypeGuid = new Guid(str);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(attrTypeGuid);
      if (attributeType1 != null)
      {
        if (attributeType1.FieldType != int32_2)
          this.PerformCriticalError(ExpertImport.InfoType.itAttrType, (long) int32_1, str, LocalizationHolder.rm.GetString("Expert.Editor_655"));
        if (attributeType1.AttributeID != int32_1)
          this.PerformIdentChange(ExpertImport.InfoType.itAttrType, (long) attributeType1.AttributeID, str);
        if (attributeType1.Name != anAttributeName)
          this.PerformNameChange(ExpertImport.InfoType.itAttrType, (long) int32_1, attributeType1.Name);
      }
      else
      {
        IDBAttributeType attributeType2 = session.GetAttributeType(anAttributeName, false);
        if (attributeType2 != null)
        {
          if (attributeType2.AttributeType != int32_2)
            this.PerformCriticalError(ExpertImport.InfoType.itAttrType, (long) int32_1, str, LocalizationHolder.rm.GetString("Expert.Editor_655"));
          this.PerformGUIDChange(ExpertImport.InfoType.itAttrType, (long) int32_1, attributeType2.PropertiesStructure.AttributeGuid.ToString());
          if (attributeType2.AttributeID != int32_1)
            this.PerformIdentChange(ExpertImport.InfoType.itAttrType, (long) attributeType2.AttributeID, str);
        }
        else
          this.CreateNewAttr(dr);
      }
    }
    return true;
  }

  private void CreateNewAttr(DataRow dr)
  {
    int int32 = Convert.ToInt32(dr["F_ATTRIBUTE_ID"]);
    string str1 = Convert.ToString(dr["F_NAME"]);
    string str2 = Convert.ToString(dr["F_GUID"]);
    AttributeTypeProperties attrProperties = new AttributeTypeProperties(dr);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBAttributeTypeCollection attributeTypeCollection = session.GetAttributeTypeCollection(-1);
      try
      {
        int num1 = attributeTypeCollection.Create(attrProperties);
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(int32);
        if (attributeType != null)
        {
          this.lbLog.Items.Add((object) string.Format(LocalizationHolder.rm.GetString("Expert.Editor_661"), (object) str1));
          if (attributeType.AttributeID != int32)
            this.fixupList.Add(new ExpertImport.ImportFixup(ExpertImport.InfoType.itAttrType, ExpertImport.ImportAction.iaReplaceId, (long) attributeType.AttributeID, str2));
          if (attributeType.AttributeGuid.ToString() != str2)
            this.fixupList.Add(new ExpertImport.ImportFixup(ExpertImport.InfoType.itAttrType, ExpertImport.ImportAction.iaReplaceGuid, (long) int32, attributeType.AttributeGuid.ToString()));
        }
        string fileName = $"{this.ImportPath}{Convert.ToString(int32)}.vals";
        DataTable valuesTable = new DataTable();
        int num2 = (int) valuesTable.ReadXml(fileName);
        session.GetAttributeType(num1, false)?.SetPossibleValues(valuesTable);
        string str3 = Convert.ToString(dr["F_GROUPLIST"]);
        if (!(str3 != ""))
          return;
        string[] strArray = str3.Split(',');
        DataTable dataTable = new DataTable();
        int num3 = (int) dataTable.ReadXml(this.ImportPath + "GROUPS.XML");
        foreach (string str4 in strArray)
        {
          Convert.ToInt32(str4);
          DataRow[] dataRowArray = dataTable.Select("F_COLUMN_ID = " + str4);
          if (dataRowArray.Length != 0)
          {
            string groupName = Convert.ToString(dataRowArray[0]["F_COLUMN_NAME"]);
            IDBAttributesGroup attributesGroup = session.GetAttributesGroup(groupName, false);
            if (attributesGroup != null)
            {
              attributesGroup.IncludeAttribute(num1);
            }
            else
            {
              int aGroupID = session.GetAttributesGroupCollection().Create(groupName, "", "", "", Guid.Empty);
              session.GetAttributesGroup(aGroupID, false)?.IncludeAttribute(num1);
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.PerformCriticalError(ExpertImport.InfoType.itAttrType, (long) int32, str2, ex.Message);
      }
    }
  }

  private void ImportAttrTypes()
  {
    DataTable dataTable = new DataTable();
    int num = (int) dataTable.ReadXml(this.ImportPath + "ATTR_TYPES.XML");
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      this.PerformAttrType(row);
  }

  private bool PerformObjType(DataRow dr)
  {
    int int32 = Convert.ToInt32(dr["F_OBJECT_TYPE"]);
    string anObjectTypeName = Convert.ToString(dr["F_OBJ_NAME"]);
    string str = Convert.ToString(dr["F_GUID"]);
    Guid objTypeGuid = new Guid(str);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IMSObjectType objectType1 = MetaDataHelper.GetObjectType(objTypeGuid);
      if (objectType1 != null)
      {
        if (objectType1.ObjectName != anObjectTypeName)
          this.PerformNameChange(ExpertImport.InfoType.itObjType, (long) int32, objectType1.ObjectName);
        if (objectType1.ObjectTypeID != int32)
          this.PerformIdentChange(ExpertImport.InfoType.itObjType, (long) objectType1.ObjectTypeID, str);
      }
      else
      {
        IDBObjectType objectType2 = session.GetObjectType(anObjectTypeName, false);
        if (objectType2 != null)
        {
          if (objectType2.PropertiesStructure.ObjectTypeGuid.ToString() != str)
            this.PerformGUIDChange(ExpertImport.InfoType.itObjType, (long) int32, objectType2.PropertiesStructure.ObjectTypeGuid.ToString());
          if (objectType2.ObjectType != int32)
            this.PerformIdentChange(ExpertImport.InfoType.itObjType, (long) objectType2.ObjectType, str);
        }
        else
          this.PerformCriticalError(ExpertImport.InfoType.itObjType, (long) int32, str, LocalizationHolder.rm.GetString("Expert.Editor_662"));
      }
    }
    return true;
  }

  private void ImportObjTypes()
  {
    DataTable dataTable = new DataTable();
    int num = (int) dataTable.ReadXml(this.ImportPath + "OBJ_TYPES.XML");
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      this.PerformObjType(row);
  }

  private void ImportRelTypes()
  {
  }

  private void ImportObjects()
  {
  }

  private bool ImportTemplates()
  {
    foreach (string file in Directory.GetFiles(this.ImportPath, "*.imdx"))
    {
      string withoutExtension = Path.GetFileNameWithoutExtension(file);
      string fileName = Path.GetFileName(file);
      if (this.templTypes.ContainsKey(withoutExtension))
      {
        int templType = this.templTypes[withoutExtension];
        long objectID = 0;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable dataTable = sessionKeeper.Session.GetObjectCollection(templType).Select(new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(ExpertConsts.Consts.attrCaption, RelationalOperators.Equal, (object) withoutExtension, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
          }, new ColumnDescriptor[1]
          {
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
          }));
          if (dataTable.Rows.Count > 0)
            objectID = Convert.ToInt64(dataTable.Rows[0][0]);
        }
        long num = 0;
        string str = withoutExtension;
        if (objectID != 0L)
        {
          ReplaceTemplate.ReplaceAction ra = ReplaceTemplate.ReplaceAction.raReplace;
          ReplaceTemplate replaceTemplate = new ReplaceTemplate();
          if (!replaceTemplate.Execute((IWin32Window) this, true, withoutExtension, out ra))
            return false;
          switch (ra)
          {
            case ReplaceTemplate.ReplaceAction.raReplace:
              using (SessionKeeper sessionKeeper = new SessionKeeper())
                this.SetObjectFile(sessionKeeper.Session.GetObject(objectID, false), file, fileName);
              num = objectID;
              break;
            case ReplaceTemplate.ReplaceAction.raUseCurrent:
              num = objectID;
              break;
            case ReplaceTemplate.ReplaceAction.raCreateNew:
              str = replaceTemplate.newName;
              break;
          }
        }
        if (num == 0L)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject idbO = sessionKeeper.Session.GetObjectCollection(templType).Create();
            IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(ExpertConsts.Consts._attrObjName, false);
            if (dbAttribute != null)
              dbAttribute.AsString = str;
            this.SetObjectFile(idbO, file, str + ".imdx");
            idbO.CommitCreation(false);
          }
        }
        foreach (string key in this.expObjList.Keys.ToList<string>())
        {
          ExpObjInfo expObj = this.expObjList[key];
          if (expObj.templateName == withoutExtension)
            expObj.templateID = num;
        }
      }
    }
    return true;
  }

  private void SetObjectFile(IDBObject idbO, string tPath, string fileName)
  {
    if (idbO == null || !(idbO.Attributes.AddAttribute(ExpertConsts.Consts.attrAttrFile, false) is IBlobWriter blobWriter))
      return;
    byte[] data = (byte[]) null;
    using (Stream stream = (Stream) new MemoryStream())
    {
      FileStream fileStream = new FileStream(tPath, FileMode.Open);
      fileStream.CopyTo(stream);
      fileStream.Flush();
      fileStream.Close();
      data = ZlibHelper.PackBuffer(stream);
    }
    string fileName1 = Path.GetFileName(tPath);
    BlobInformation blobInfo = new BlobInformation((long) data.Length, (long) data.Length, DateTime.Now, fileName1, ArcMethods.ZLibPacked, "");
    if (!blobWriter.OpenBlob(blobInfo, false))
      return;
    blobWriter.WriteDataBlock(data);
  }

  private void ImportScripts()
  {
    List<string> list = this.expObjList.Keys.ToList<string>();
    foreach (string str1 in list)
    {
      long num1 = 0;
      ExpObjInfo expObj = this.expObjList[str1];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(ExpertConsts.Consts.attrCaption, RelationalOperators.Equal, (object) str1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
        }, new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
        }));
        if (dataTable.Rows.Count > 0)
          num1 = Convert.ToInt64(dataTable.Rows[0][0]);
      }
      long num2 = 0;
      string str2 = str1;
      if (num1 != 0L)
      {
        ReplaceTemplate.ReplaceAction ra = ReplaceTemplate.ReplaceAction.raReplace;
        ReplaceTemplate replaceTemplate = new ReplaceTemplate();
        if (replaceTemplate.Execute((IWin32Window) this, false, str1, out ra))
        {
          switch (ra)
          {
            case ReplaceTemplate.ReplaceAction.raReplace:
              num2 = num1;
              break;
            case ReplaceTemplate.ReplaceAction.raUseCurrent:
              expObj.objID = 0L;
              continue;
            case ReplaceTemplate.ReplaceAction.raCreateNew:
              str2 = replaceTemplate.newName;
              break;
          }
        }
      }
      if (num2 == 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript).Create();
          IDBAttribute dbAttribute1 = dbObject.Attributes.AddAttribute(ExpertConsts.Consts._attrObjName, false);
          if (dbAttribute1 != null)
            dbAttribute1.AsString = str2;
          IDBAttribute dbAttribute2 = dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrTemplateLink, false);
          if (dbAttribute2 != null)
            dbAttribute2.AsInteger = expObj.templateID;
          dbObject.CommitCreation(false);
          num2 = dbObject.ObjectID;
        }
      }
      expObj.objID = num2;
      expObj.scriptName = str2;
    }
    DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
    foreach (string key in list)
    {
      if (this.expObjList[key].objID != 0L)
      {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load($"{this.ImportPath}{key}.script");
        this.LoadScriptTree(xDoc);
      }
    }
  }

  private void LoadNodeFromXML(XmlNode xmlRoot, ScriptTreeNode rootNode)
  {
    string str = "";
    int modTag = -1;
    int opTag = -1;
    if (xmlRoot.Attributes != null)
    {
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlRoot.Attributes)
      {
        if (attribute.Name == "label")
          str = attribute.Value;
        else if (attribute.Name == "modTag")
          modTag = Convert.ToInt32(attribute.Value);
        else if (attribute.Name == "opTag")
          opTag = Convert.ToInt32(attribute.Value);
      }
    }
    ScriptTreeNode rootNode1 = new ScriptTreeNode();
    rootNode1.LoadXML(xmlRoot, modTag, opTag);
    rootNode1.label = str;
    rootNode.Items.Add((object) rootNode1);
    rootNode1.parent = rootNode;
    if (!xmlRoot.HasChildNodes)
      return;
    foreach (XmlNode childNode in xmlRoot.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "node")
        this.LoadNodeFromXML(childNode, rootNode1);
    }
  }

  private ScriptTreeNode LoadScriptTree(XmlDocument xDoc)
  {
    ScriptTreeNode rootNode = new ScriptTreeNode();
    XmlElement documentElement = xDoc.DocumentElement;
    if (documentElement.HasChildNodes)
    {
      foreach (XmlNode childNode1 in documentElement.ChildNodes)
      {
        if (childNode1.NodeType != XmlNodeType.Element || !(childNode1.Name == "DocParms"))
        {
          if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ExpScript")
          {
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
              this.LoadNodeFromXML(childNode2, rootNode);
          }
          else
            this.LoadNodeFromXML(childNode1, rootNode);
        }
      }
    }
    return rootNode;
  }

  private void btnStart_Click(object sender, EventArgs e)
  {
    this.ImportAttrTypes();
    this.ImportObjTypes();
    this.ImportRelTypes();
    this.ImportObjects();
    if (!this.ImportTemplates())
      return;
    this.ImportScripts();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExpertImport));
    this.btnSelDirectory = new Button();
    this.tbExportFolder = new TextBox();
    this.panel1 = new Panel();
    this.pBar = new ProgressBar();
    this.btnStart = new Button();
    this.btnClose = new Button();
    this.fbd = new FolderBrowserDialog();
    this.panel2 = new Panel();
    this.lbLog = new ListBox();
    this.label3 = new Label();
    this.panel3 = new Panel();
    this.tv = new TreeView();
    this.IL = new ImageList(this.components);
    this.label1 = new Label();
    this.panel4 = new Panel();
    this.uniTV = new TreeView();
    this.label2 = new Label();
    this.panel5 = new Panel();
    this.splitter1 = new Splitter();
    this.splitter2 = new Splitter();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel4.SuspendLayout();
    this.panel5.SuspendLayout();
    this.SuspendLayout();
    this.btnSelDirectory.Location = new Point(9, 10);
    this.btnSelDirectory.Name = "btnSelDirectory";
    this.btnSelDirectory.Size = new Size(122, 23);
    this.btnSelDirectory.TabIndex = 5;
    this.btnSelDirectory.Text = "Папка импорта...";
    this.btnSelDirectory.UseVisualStyleBackColor = true;
    this.btnSelDirectory.Click += new EventHandler(this.btnSelDirectory_Click);
    this.tbExportFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbExportFolder.Location = new Point(137, 12);
    this.tbExportFolder.Name = "tbExportFolder";
    this.tbExportFolder.ReadOnly = true;
    this.tbExportFolder.Size = new Size(810, 20);
    this.tbExportFolder.TabIndex = 6;
    this.panel1.Controls.Add((Control) this.pBar);
    this.panel1.Controls.Add((Control) this.btnStart);
    this.panel1.Controls.Add((Control) this.btnClose);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 572);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(955, 30);
    this.panel1.TabIndex = 7;
    this.pBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.pBar.Location = new Point(102, 6);
    this.pBar.Name = "pBar";
    this.pBar.Size = new Size(747, 18);
    this.pBar.TabIndex = 2;
    this.btnStart.Location = new Point(9, 3);
    this.btnStart.Name = "btnStart";
    this.btnStart.Size = new Size(75, 23);
    this.btnStart.TabIndex = 1;
    this.btnStart.Text = "Импорт!";
    this.btnStart.UseVisualStyleBackColor = true;
    this.btnStart.Click += new EventHandler(this.btnStart_Click);
    this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClose.DialogResult = DialogResult.Cancel;
    this.btnClose.Location = new Point(872, 4);
    this.btnClose.Name = "btnClose";
    this.btnClose.Size = new Size(75, 23);
    this.btnClose.TabIndex = 0;
    this.btnClose.Text = "Закрыть";
    this.btnClose.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.lbLog);
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(0, 386);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(955, 186);
    this.panel2.TabIndex = 8;
    this.lbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbLog.FormattingEnabled = true;
    this.lbLog.IntegralHeight = false;
    this.lbLog.Location = new Point(8, 19);
    this.lbLog.Name = "lbLog";
    this.lbLog.Size = new Size(938, 158);
    this.lbLog.TabIndex = 1;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(6, 3);
    this.label3.Name = "label3";
    this.label3.Size = new Size(72, 13);
    this.label3.TabIndex = 0;
    this.label3.Text = "Лог импорта";
    this.panel3.Controls.Add((Control) this.tv);
    this.panel3.Controls.Add((Control) this.label1);
    this.panel3.Dock = DockStyle.Fill;
    this.panel3.Location = new Point(0, 203);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(955, 399);
    this.panel3.TabIndex = 9;
    this.tv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tv.ImageIndex = 0;
    this.tv.ImageList = this.IL;
    this.tv.Location = new Point(8, 19);
    this.tv.Name = "tv";
    this.tv.SelectedImageIndex = 0;
    this.tv.Size = new Size(938, 155);
    this.tv.TabIndex = 3;
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Transparent;
    this.IL.Images.SetKeyName(0, "Folder.bmp");
    this.IL.Images.SetKeyName(1, "attr.bmp");
    this.IL.Images.SetKeyName(2, "_objType.bmp");
    this.IL.Images.SetKeyName(3, "link.ico");
    this.IL.Images.SetKeyName(4, "document.bmp");
    this.IL.Images.SetKeyName(5, "expObj.bmp");
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 3);
    this.label1.Name = "label1";
    this.label1.Size = new Size(230, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Импортируемые скрипты и их зависимости";
    this.panel4.Controls.Add((Control) this.uniTV);
    this.panel4.Controls.Add((Control) this.label2);
    this.panel4.Dock = DockStyle.Top;
    this.panel4.Location = new Point(0, 44);
    this.panel4.Name = "panel4";
    this.panel4.Size = new Size(955, 159);
    this.panel4.TabIndex = 11;
    this.uniTV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.uniTV.ImageIndex = 0;
    this.uniTV.ImageList = this.IL;
    this.uniTV.Location = new Point(8, 19);
    this.uniTV.Name = "uniTV";
    this.uniTV.SelectedImageIndex = 0;
    this.uniTV.Size = new Size(938, 134);
    this.uniTV.TabIndex = 4;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(6, 3);
    this.label2.Name = "label2";
    this.label2.Size = new Size(385, 13);
    this.label2.TabIndex = 0;
    this.label2.Text = "Использованные типы объектов, атрибутов и связей; ссылки на объекты";
    this.panel5.Controls.Add((Control) this.btnSelDirectory);
    this.panel5.Controls.Add((Control) this.tbExportFolder);
    this.panel5.Dock = DockStyle.Top;
    this.panel5.Location = new Point(0, 0);
    this.panel5.Name = "panel5";
    this.panel5.Size = new Size(955, 44);
    this.panel5.TabIndex = 12;
    this.splitter1.Dock = DockStyle.Top;
    this.splitter1.Location = new Point(0, 203);
    this.splitter1.Name = "splitter1";
    this.splitter1.Size = new Size(955, 3);
    this.splitter1.TabIndex = 13;
    this.splitter1.TabStop = false;
    this.splitter2.Dock = DockStyle.Bottom;
    this.splitter2.Location = new Point(0, 383);
    this.splitter2.Name = "splitter2";
    this.splitter2.Size = new Size(955, 3);
    this.splitter2.TabIndex = 14;
    this.splitter2.TabStop = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(955, 602);
    this.Controls.Add((Control) this.splitter2);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel4);
    this.Controls.Add((Control) this.panel5);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExpertImport);
    this.Text = "Импорт скриптов генерации документов";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.panel4.ResumeLayout(false);
    this.panel4.PerformLayout();
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.ResumeLayout(false);
  }

  public enum InfoType
  {
    itAttrType,
    itObjType,
    itRelType,
    itObjRef,
  }

  public enum ImportAction
  {
    iaReplaceName,
    iaReplaceGuid,
    iaReplaceId,
    iaCriticalErr,
  }

  public class ImportFixup
  {
    public ExpertImport.InfoType it;
    public long Id;
    public ExpertImport.ImportAction impAct = ExpertImport.ImportAction.iaReplaceGuid;
    public string newVal = "";

    public ImportFixup(ExpertImport.ImportAction ia, string newVal)
    {
      this.impAct = ia;
      this.newVal = newVal;
    }

    public ImportFixup(ExpertImport.InfoType it, string newVal)
    {
      this.it = it;
      this.newVal = newVal;
    }

    public ImportFixup(ExpertImport.InfoType it, long Id)
    {
      this.it = it;
      this.Id = Id;
    }

    public ImportFixup(ExpertImport.InfoType it, ExpertImport.ImportAction ia, long Id)
    {
      this.it = it;
      this.impAct = ia;
      this.Id = Id;
    }

    public ImportFixup(
      ExpertImport.InfoType it,
      ExpertImport.ImportAction ia,
      long Id,
      string newVal)
    {
      this.it = it;
      this.impAct = ia;
      this.newVal = newVal;
      this.Id = Id;
    }
  }
}
