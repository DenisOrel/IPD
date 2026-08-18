
// Type: Intermech.Expressions.ExpressionEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Expressions.Exceptions;
using Intermech.Expressions.Functions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Expressions;

public class ExpressionEditor : Form
{
  private Parser _parser;
  private string _expression;
  private ExpressionTree _expressionTree;
  private int _thisAttId;
  private Hashtable _imageIndexes = new Hashtable();
  private ICategoryTypeIconService _iconService;
  private ParseEventHandler _parseHandler;
  private static Size _size;
  private static Point _location;
  private static string _dockConfiguration;
  private static IConfigurationManager _configurationManager = ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager;
  private const string CONFIG_NAME = "ExpressionConfig";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button okButton;
  private Button cancelButton;
  private Panel panel1;
  private DockContainer leftDock;
  private DockManager dockManager;
  private DockContainer rightDock;
  private DockControl functionsDC;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private DockControl fastDC;
  private DockControl varsDC;
  private DockControl constDC;
  private ParserTexBox editPanel;
  private ImageList imageList;
  private TreeView functionsTree;
  private Label errLabel;
  private Button button15;
  private Button button16;
  private Button button17;
  private Button button18;
  private Button button19;
  private Button button20;
  private Button button9;
  private Button button10;
  private Button button11;
  private Button button12;
  private Button button13;
  private Button button14;
  private Button button8;
  private Button button7;
  private Button button6;
  private Button button5;
  private Button button4;
  private Button button3;
  private Button button2;
  private Button button1;
  private Button button22;
  private Button button23;
  private Button button24;
  private Button button25;
  private Button button26;
  private Button button27;
  private Button button28;
  private Button button21;
  private Button button29;
  private Button button30;
  private Button button31;
  private Button button32;
  private Button button33;
  private Button button34;
  private Button button35;
  private Button button36;
  private TreeView varsTree;
  private TreeView constTree;
  private FilterBox functionsFilter;
  private FilterBox varsFilter;
  private Splitter splitter1;
  private Label lbFuncDescr;
  private Label lbFuncTitle;
  private SplitContainer splitContainer1;

  static ExpressionEditor()
  {
    if (ExpressionEditor._configurationManager == null)
      return;
    ExpressionEditor._configurationManager.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(ExpressionEditor.ConfigurationManager_ConfigurationBeforeSave);
    IConfiguration configuration = ExpressionEditor._configurationManager.Open("ExpressionConfig");
    if (configuration == null)
      return;
    if (configuration.HasProperty("Size"))
      ExpressionEditor._size = (Size) TypeDescriptor.GetConverter(typeof (Size)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) configuration.GetProperty("Size"));
    if (configuration.HasProperty("Location"))
      ExpressionEditor._location = (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) configuration.GetProperty("Location"));
    if (!configuration.HasProperty("DockConfiguration"))
      return;
    ExpressionEditor._dockConfiguration = configuration.GetProperty("DockConfiguration");
  }

  private static void ConfigurationManager_ConfigurationBeforeSave(
    IConfigurationManager configurationManager)
  {
    if (ExpressionEditor._dockConfiguration == null || ExpressionEditor._dockConfiguration.Length <= 0)
      return;
    IConfiguration configuration = ExpressionEditor._configurationManager.Create("ExpressionConfig");
    configuration.SetProperty("Size", (string) TypeDescriptor.GetConverter(typeof (Size)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) ExpressionEditor._size, typeof (string)));
    configuration.SetProperty("Location", (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) ExpressionEditor._location, typeof (string)));
    configuration.SetProperty("DockConfiguration", ExpressionEditor._dockConfiguration);
  }

  public ExpressionEditor()
  {
    this.InitializeComponent();
    this._parser = new Parser();
    this._parser.UseCache = false;
    IComparer comparer = (IComparer) new ExpressionEditor.NodeComparer();
    this.functionsTree.TreeViewNodeSorter = comparer;
    this.varsTree.TreeViewNodeSorter = comparer;
    this.constTree.TreeViewNodeSorter = comparer;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 888);
  }

  private void FillTrees()
  {
    this.FillFunctionsTree();
    this.FillConstTree();
  }

  private void FillConstTree()
  {
    ConstantsCollection constants = this._parser.Constants;
    try
    {
      this.constTree.BeginUpdate();
      this.constTree.Nodes.Clear();
      int count = constants.Count;
      for (int index = 0; index < count; ++index)
      {
        Constant constant = constants[index];
        this.constTree.Nodes.Add(new TreeNode(constant.Name, 4, 4)
        {
          Tag = (object) constant
        });
      }
    }
    finally
    {
      this.constTree.EndUpdate();
      this.constTree.Sort();
    }
  }

  private void FillVariablesTree(bool clear)
  {
    VariablesCollection variables = this._parser.Variables;
    try
    {
      this.varsTree.BeginUpdate();
      if (clear)
        this.varsTree.Nodes.Clear();
      TreeNode treeNode1 = this.varsTree.Nodes.Add("math", LocalizationHolder.rm.GetString("Client.Core_157"), 0, 1);
      TreeNode treeNode2 = this.varsTree.Nodes.Add("string", LocalizationHolder.rm.GetString("Client.Core_158"), 0, 1);
      TreeNode treeNode3 = this.varsTree.Nodes.Add("bool", LocalizationHolder.rm.GetString("Client.Core_159"), 0, 1);
      TreeNode treeNode4 = this.varsTree.Nodes.Add("date", LocalizationHolder.rm.GetString("Client.Core_160"), 0, 1);
      TreeNode treeNode5 = this.varsTree.Nodes.Add("other", LocalizationHolder.rm.GetString("Client.Core_161"), 0, 1);
      TreeNode treeNode6 = this.varsTree.Nodes.Add("all", LocalizationHolder.rm.GetString("Client.Core_116"), 0, 1);
      treeNode1.Tag = (object) 0;
      treeNode2.Tag = (object) 1;
      treeNode3.Tag = (object) 2;
      treeNode4.Tag = (object) 3;
      treeNode5.Tag = (object) 4;
      treeNode6.Tag = (object) 5;
      int count = variables.Count;
      for (int index = 0; index < count; ++index)
      {
        Variable variable = variables[index];
        int num = 2;
        if (variable.FieldType != FieldTypes.ftUnknown)
          num = this.GetAttributeImageIndex(variable.FieldType);
        treeNode6.Nodes.Add(new TreeNode(variable.ToString(), num, num)
        {
          Tag = (object) variable
        });
        TreeNode node = new TreeNode(variable.ToString(), num, num);
        node.Tag = (object) variable;
        if (variable.Type == typeof (double))
          treeNode1.Nodes.Add(node);
        else if (variable.Type == typeof (string))
          treeNode2.Nodes.Add(node);
        else if (variable.Type == typeof (bool))
          treeNode3.Nodes.Add(node);
        else if (variable.Type == typeof (DateTime))
          treeNode4.Nodes.Add(node);
        else
          treeNode5.Nodes.Add(node);
      }
    }
    finally
    {
      if (variables.Count < 100)
        this.varsTree.Sort();
      this.varsTree.EndUpdate();
    }
  }

  private void FillFunctionsTree()
  {
    FunctionsCollection functions = this._parser.Functions;
    try
    {
      this.functionsTree.BeginUpdate();
      this.functionsTree.Nodes.Clear();
      TreeNode treeNode1 = this.functionsTree.Nodes.Add("math", LocalizationHolder.rm.GetString("Client.Core_162"), 0, 1);
      TreeNode treeNode2 = this.functionsTree.Nodes.Add("string", LocalizationHolder.rm.GetString("Client.Core_158"), 0, 1);
      TreeNode treeNode3 = this.functionsTree.Nodes.Add("bool", LocalizationHolder.rm.GetString("Client.Core_159"), 0, 1);
      TreeNode treeNode4 = this.functionsTree.Nodes.Add("date", LocalizationHolder.rm.GetString("Client.Core_160"), 0, 1);
      TreeNode treeNode5 = this.functionsTree.Nodes.Add("other", LocalizationHolder.rm.GetString("Client.Core_161"), 0, 1);
      TreeNode treeNode6 = this.functionsTree.Nodes.Add("all", LocalizationHolder.rm.GetString("Client.Core_116"), 0, 1);
      treeNode1.Tag = (object) 0;
      treeNode2.Tag = (object) 1;
      treeNode3.Tag = (object) 2;
      treeNode4.Tag = (object) 3;
      treeNode5.Tag = (object) 4;
      treeNode6.Tag = (object) 5;
      int count = functions.Count;
      for (int index = 0; index < count; ++index)
      {
        Function function = functions[index];
        treeNode6.Nodes.Add(new TreeNode(function.Name, 3, 3)
        {
          Tag = (object) function
        });
        TreeNode node = new TreeNode(function.Name, 3, 3);
        node.Tag = (object) function;
        switch (function.Category)
        {
          case FunctionCategory.Math:
            treeNode1.Nodes.Add(node);
            break;
          case FunctionCategory.String:
            treeNode2.Nodes.Add(node);
            break;
          case FunctionCategory.Logical:
            treeNode3.Nodes.Add(node);
            break;
          case FunctionCategory.Date:
            treeNode4.Nodes.Add(node);
            break;
          default:
            treeNode5.Nodes.Add(node);
            break;
        }
      }
    }
    finally
    {
      this.functionsTree.EndUpdate();
      this.functionsTree.Sort();
    }
  }

  private bool InternalShowDialog(ref string expression)
  {
    if (this.ShowDialog() == DialogResult.OK)
    {
      string expression1 = this.Expression;
      if (expression1 != expression)
      {
        expression = expression1;
        return true;
      }
    }
    return false;
  }

  public static bool EditExpression(
    ref string expression,
    AttributeTypeProperties[] atps,
    int attToSkip,
    ParseEventHandler handler)
  {
    ExpressionEditor expressionEditor = new ExpressionEditor();
    expressionEditor.SetData(expression, atps, attToSkip, handler);
    return expressionEditor.InternalShowDialog(ref expression);
  }

  public static bool EditExpression(
    ref string expression,
    ICollection variables,
    CreateVariableEventHandler createVariableHandler)
  {
    ExpressionEditor expressionEditor = new ExpressionEditor();
    expressionEditor.SetData(expression, variables, createVariableHandler);
    return expressionEditor.InternalShowDialog(ref expression);
  }

  public static bool EditExpression(
    ref string expression,
    List<Variable> variables,
    int attToSkip,
    ParseEventHandler handler,
    List<Variable> additionalVariables)
  {
    ExpressionEditor expressionEditor = new ExpressionEditor();
    expressionEditor.SetData(expression, variables, attToSkip, handler, additionalVariables);
    return expressionEditor.InternalShowDialog(ref expression);
  }

  private void SetData(
    string expression,
    AttributeTypeProperties[] atps,
    int attToSkip,
    ParseEventHandler handler)
  {
    this._parseHandler = handler;
    this._thisAttId = attToSkip;
    this._parser.Variables.Clear();
    this._parser.Variables.AddRange((ICollection) this.FillTreeWithAttributeTypeProperties(atps));
    this.varsFilter.Visible = false;
    this.Expression = expression;
    this.ActiveControl = (Control) this.editPanel;
  }

  private void SetData(
    string expression,
    ICollection variables,
    CreateVariableEventHandler handler)
  {
    this._parser.Variables.Clear();
    this._parser.Variables.AddRange(variables);
    this.FillVariablesTree(true);
    if (handler != null)
    {
      this._parser.AutoDetectVariables = true;
      this._parser.CreateVariable += handler;
    }
    this.Expression = expression;
    this.ActiveControl = (Control) this.editPanel;
  }

  private void SetData(
    string expression,
    List<Variable> variables,
    int attToSkip,
    ParseEventHandler handler,
    List<Variable> additionalVariables)
  {
    this._parseHandler = handler;
    this._thisAttId = attToSkip;
    this._parser.Variables.Clear();
    this._parser.Variables.AddRange((ICollection) variables);
    if (additionalVariables != null)
      this._parser.Variables.AddRange((ICollection) additionalVariables);
    this.FillVariablesTree();
    this.varsFilter.Visible = false;
    this.Expression = expression;
    this.ActiveControl = (Control) this.editPanel;
  }

  public bool Valid
  {
    get => this.okButton.Enabled;
    set => this.okButton.Enabled = value;
  }

  private string Expression
  {
    get => this._expression;
    set
    {
      this._expression = value;
      this.editPanel.Text = value;
      if (string.IsNullOrEmpty(value))
        return;
      this.editPanel.SelectionStart = value.Length;
    }
  }

  private void EditPanel_TextChanged(object sender, EventArgs e)
  {
    this._expression = this.editPanel.Text.Trim();
    bool flag = false;
    this.errLabel.Text = string.Empty;
    this._expressionTree = (ExpressionTree) null;
    if (this._expression.Length > 0)
    {
      try
      {
        this._expressionTree = this._parser.Parse(this._expression);
        this.OnSuccessParse();
      }
      catch (Exception ex)
      {
        if (ex is ParseException)
          this.editPanel.Exception = ex as ParseException;
        flag = true;
        this.errLabel.ForeColor = Color.Red;
        this.errLabel.Text = ex.Message;
      }
      if (!flag)
        this.editPanel.Exception = (ParseException) null;
    }
    this.okButton.Enabled = !flag;
  }

  private void OnSuccessParse()
  {
    if (this._parseHandler == null)
      return;
    ParseEventArgs args = new ParseEventArgs(this._expressionTree);
    this._parseHandler((object) this, args);
    if (args.Result == null)
      return;
    this.errLabel.ForeColor = Color.Green;
    this.errLabel.Text = args.Result.ToString();
  }

  private void FastButton_Click(object sender, EventArgs e)
  {
    if (!(sender is Button button))
      return;
    this.InsertText(button.Text);
  }

  private void InsertText(string str)
  {
    this.editPanel.Select();
    this.editPanel.SelectedText = str;
  }

  private void OnFunctionsTree_DoubleClick(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.functionsTree.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is Function tag))
      return;
    this.InsertText(tag.Name);
  }

  private void OnFunctionsFilter_Find(object sender, EventArgs e)
  {
    if (!(sender is FilterBox filterBox) || !this.FindInTree(this.functionsTree, filterBox.FindText) && !filterBox.CanClear)
      return;
    filterBox.CanClear = true;
  }

  private bool FindInTree(TreeView treeView, string text)
  {
    bool inTree = false;
    try
    {
      List<TreeNode> treeNodeList = new List<TreeNode>();
      treeView.BeginUpdate();
      int count1 = treeView.Nodes.Count;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        TreeNodeCollection nodes = treeView.Nodes[index1].Nodes;
        int count2 = nodes.Count;
        for (int index2 = 0; index2 < count2; ++index2)
        {
          TreeNode treeNode = nodes[index2];
          if (treeNode.Tag is IIdentifier)
          {
            if (treeNode.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)
              inTree = true;
            else
              treeNodeList.Add(treeNode);
          }
        }
      }
      if (treeNodeList.Count > 0 & inTree)
      {
        int count3 = treeNodeList.Count;
        for (int index = 0; index < count3; ++index)
          treeNodeList[index].Remove();
      }
    }
    finally
    {
      treeView.EndUpdate();
    }
    return inTree;
  }

  private void OnFunctionsFilter_Clear(object sender, EventArgs e)
  {
    this.FillFunctionsTree();
    if (!(sender is FilterBox filterBox))
      return;
    filterBox.CanClear = false;
  }

  private void OnVarsFilter_Find(object sender, EventArgs e)
  {
    if (!(sender is FilterBox filterBox) || !this.FindInTree(this.varsTree, filterBox.FindText) && !filterBox.CanClear)
      return;
    filterBox.CanClear = true;
  }

  private void OnVarsFilter_Clear(object sender, EventArgs e)
  {
    this.FillVariablesTree(true);
    if (!(sender is FilterBox filterBox))
      return;
    filterBox.CanClear = false;
  }

  private void OnFunctionsTree_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.OnFunctionsTree_DoubleClick((object) this.functionsTree, EventArgs.Empty);
  }

  private void OnVarsTree_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.OnVarsTree_DoubleClick((object) this.varsTree, EventArgs.Empty);
  }

  private void OnConstTree_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.OnConstTree_DoubleClick((object) this.constTree, EventArgs.Empty);
  }

  private void OnVarsTree_DoubleClick(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.varsTree.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is Variable tag))
      return;
    if (this.editPanel.Text.StartsWith("@"))
      this.InsertText('{'.ToString() + tag.Name + (object) '}');
    else
      this.InsertText($"[{tag.Name}]");
  }

  private void OnConstTree_DoubleClick(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.constTree.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is Constant tag))
      return;
    this.InsertText(tag.Name);
  }

  private void ExpressionEditor_Shown(object sender, EventArgs e) => this.FillTrees();

  private void ExpressionEditor_Load(object sender, EventArgs e)
  {
    if (!ExpressionEditor._location.IsEmpty)
      this.Location = ExpressionEditor._location;
    if (!ExpressionEditor._size.IsEmpty)
      this.Size = ExpressionEditor._size;
    if (ExpressionEditor._dockConfiguration == null || ExpressionEditor._dockConfiguration.Length <= 0)
      return;
    this.dockManager.SetLayout(ExpressionEditor._dockConfiguration);
  }

  private void ExpressionEditor_FormClosing(object sender, FormClosingEventArgs e)
  {
    ExpressionEditor._dockConfiguration = this.dockManager.GetLayout();
    ExpressionEditor._location = this.Location;
    ExpressionEditor._size = this.Size;
  }

  /// <summary>
  /// Возвращает массив переменных, созданный на основе всех
  /// допустимых переменных системы
  /// </summary>
  /// <returns></returns>
  private Variable[] FillTreeWithAllAttributes()
  {
    List<Variable> vars = new List<Variable>();
    try
    {
      this.varsTree.BeginUpdate();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetAttributesGroupCollection().Select("F_GROUP_NAME", (object[]) null);
        int attributeGroupImageIndex = this.GetAttributeGroupImageIndex();
        TreeNode treeNode1 = new TreeNode(LocalizationHolder.rm.GetString("Client.Core_163"));
        treeNode1.ImageIndex = attributeGroupImageIndex;
        treeNode1.SelectedImageIndex = attributeGroupImageIndex;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          TreeNode treeNode2 = new TreeNode(Convert.ToString(row["F_GROUP_NAME"]), attributeGroupImageIndex, attributeGroupImageIndex);
          int int32 = Convert.ToInt32(row["F_GROUP_ID"]);
          if (int32 != -1)
          {
            IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(int32);
            this.FillAttsNode(treeNode2, treeNode1, (IDBCollection) attributeTypeCollection, vars, (List<int>) null);
            if (treeNode2.Nodes.Count > 0)
              this.varsTree.Nodes.Add(treeNode2);
          }
        }
        if (treeNode1.Nodes.Count > 0)
          this.varsTree.Nodes.Add(treeNode1);
      }
    }
    finally
    {
      this.varsTree.EndUpdate();
    }
    return vars.ToArray();
  }

  private Variable[] FillTreeWithRelationAttributes(int relationTypeId)
  {
    List<Variable> vars = new List<Variable>();
    try
    {
      this.varsTree.BeginUpdate();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationType relationType = sessionKeeper.Session.GetRelationType(relationTypeId);
        if (relationType.AnyAttributes)
          return this.FillTreeWithAllAttributes();
        DataTable dataTable = relationType.Attributes.Select(string.Empty, (object[]) null);
        List<int> validIds = new List<int>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          validIds.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        int attributeGroupImageIndex = this.GetAttributeGroupImageIndex();
        TreeNode treeNode = new TreeNode(relationType.TypeName, attributeGroupImageIndex, attributeGroupImageIndex);
        this.FillAttsNode(treeNode, (TreeNode) null, (IDBCollection) sessionKeeper.Session.GetAttributeTypeCollection(-1), vars, validIds);
        if (treeNode.Nodes.Count > 0)
        {
          this.varsTree.Nodes.Add(treeNode);
          treeNode.Expand();
        }
      }
    }
    finally
    {
      this.varsTree.EndUpdate();
    }
    return vars.ToArray();
  }

  private Variable[] FillTreeWithTypeAttributes(int objectTypeId)
  {
    List<Variable> vars = new List<Variable>();
    try
    {
      this.varsTree.BeginUpdate();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objectTypeId);
        if (objectType.AnyAttributes)
          return this.FillTreeWithAllAttributes();
        DataTable dataTable = objectType.Attributes.Select(string.Empty, (object[]) null);
        List<int> validIds = new List<int>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          validIds.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        int attributeGroupImageIndex = this.GetAttributeGroupImageIndex();
        TreeNode treeNode = new TreeNode(objectType.ObjectTypeName, attributeGroupImageIndex, attributeGroupImageIndex);
        this.FillAttsNode(treeNode, (TreeNode) null, (IDBCollection) sessionKeeper.Session.GetAttributeTypeCollection(-1), vars, validIds);
        if (treeNode.Nodes.Count > 0)
        {
          this.varsTree.Nodes.Add(treeNode);
          treeNode.Expand();
        }
      }
    }
    finally
    {
      this.varsTree.EndUpdate();
    }
    return vars.ToArray();
  }

  private Variable[] FillTreeWithAttributeTypeProperties(AttributeTypeProperties[] atps)
  {
    List<Variable> variableList = new List<Variable>();
    try
    {
      this.varsTree.BeginUpdate();
      this.varsTree.Nodes.Clear();
      int attributeGroupImageIndex = this.GetAttributeGroupImageIndex();
      TreeNode node = new TreeNode(LocalizationHolder.rm.GetString("Client.Core_164"));
      node.ImageIndex = attributeGroupImageIndex;
      node.SelectedImageIndex = attributeGroupImageIndex;
      int length = atps.Length;
      for (int index = 0; index < length; ++index)
      {
        if (atps[index].AttributeID != this._thisAttId)
        {
          FieldTypes fieldType = atps[index].FieldType;
          string name = atps[index].Name;
          Variable variable = new Variable(name, ExpressionEditor.GetTypeOfAttributeValue(fieldType));
          variableList.Add(variable);
          int attributeImageIndex = this.GetAttributeImageIndex(fieldType);
          node.Nodes.Add(new TreeNode(name, attributeImageIndex, attributeImageIndex)
          {
            Tag = (object) variable
          });
        }
      }
      this.varsTree.Nodes.Add(node);
      node.Expand();
    }
    finally
    {
      this.varsTree.EndUpdate();
    }
    return variableList.ToArray();
  }

  /// <summary>
  /// Метод создания нодов в дереве, отличается от стандартного что числами являются не только double но и int, long
  /// </summary>
  private void FillVariablesTree()
  {
    VariablesCollection variables = this._parser.Variables;
    try
    {
      this.varsTree.BeginUpdate();
      this.varsTree.Nodes.Clear();
      TreeNode treeNode1 = this.varsTree.Nodes.Add("math", LocalizationHolder.rm.GetString("Client.Core_157"), 0, 1);
      TreeNode treeNode2 = this.varsTree.Nodes.Add("string", LocalizationHolder.rm.GetString("Client.Core_158"), 0, 1);
      TreeNode treeNode3 = this.varsTree.Nodes.Add("bool", LocalizationHolder.rm.GetString("Client.Core_159"), 0, 1);
      TreeNode treeNode4 = this.varsTree.Nodes.Add("date", LocalizationHolder.rm.GetString("Client.Core_160"), 0, 1);
      TreeNode treeNode5 = this.varsTree.Nodes.Add("other", LocalizationHolder.rm.GetString("Client.Core_161"), 0, 1);
      TreeNode treeNode6 = this.varsTree.Nodes.Add("all", LocalizationHolder.rm.GetString("Client.Core_116"), 0, 1);
      treeNode1.Tag = (object) 0;
      treeNode2.Tag = (object) 1;
      treeNode3.Tag = (object) 2;
      treeNode4.Tag = (object) 3;
      treeNode5.Tag = (object) 4;
      treeNode6.Tag = (object) 5;
      int count = variables.Count;
      for (int index = 0; index < count; ++index)
      {
        Variable variable = variables[index];
        int attributeImageIndex = this.GetAttributeImageIndex(variable.FieldType);
        TreeNode node1 = new TreeNode(variable.Name, attributeImageIndex, attributeImageIndex)
        {
          Tag = (object) variable
        };
        if (variable.Type == typeof (double) || variable.Type == typeof (int) || variable.Type == typeof (long))
          treeNode1.Nodes.Add(node1);
        else if (variable.Type == typeof (string))
          treeNode2.Nodes.Add(node1);
        else if (variable.Type == typeof (bool))
          treeNode3.Nodes.Add(node1);
        else if (variable.Type == typeof (DateTime))
          treeNode4.Nodes.Add(node1);
        else
          treeNode5.Nodes.Add(node1);
        TreeNode node2 = new TreeNode(variable.Name, attributeImageIndex, attributeImageIndex)
        {
          Tag = (object) variable
        };
        treeNode6.Nodes.Add(node2);
      }
    }
    finally
    {
      if (variables.Count < 100)
        this.varsTree.Sort();
      this.varsTree.EndUpdate();
    }
  }

  private void FillAttsNode(
    TreeNode rootNode,
    TreeNode allVars,
    IDBCollection atts,
    List<Variable> vars,
    List<int> validIds)
  {
    foreach (DataRow row in (InternalDataCollectionBase) atts.Select(string.Empty, (object[]) null).Rows)
    {
      int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      if (int32 > 0 && validIds != null && validIds.Contains(int32))
      {
        FieldTypes uint32 = (FieldTypes) Convert.ToUInt32(row["F_ATTRIBUTE_TYPE"]);
        string str = Convert.ToString(row["F_NAME"]);
        System.Type ofAttributeValue = ExpressionEditor.GetTypeOfAttributeValue(uint32);
        Variable variable = new Variable(str, ofAttributeValue);
        int attributeImageIndex = this.GetAttributeImageIndex(uint32);
        rootNode.Nodes.Add(new TreeNode(str, attributeImageIndex, attributeImageIndex)
        {
          Tag = (object) variable
        });
        if (allVars != null)
          allVars.Nodes.Add(new TreeNode(str, attributeImageIndex, attributeImageIndex)
          {
            Tag = (object) variable
          });
        vars.Add(variable);
      }
    }
  }

  private static System.Type GetTypeOfAttributeValue(FieldTypes fieldType)
  {
    System.Type o = AttributesTypeHelper.GetTypeOfAttributeValue(fieldType);
    if (fieldType == FieldTypes.ftObjectLink || fieldType == FieldTypes.ftObjectLinkByID)
      o = typeof (string);
    if (typeof (MeasuredValue).Equals(o))
      o = typeof (double);
    return o;
  }

  private Variable VariableFromAttribute(IDBAttribute att) => (Variable) null;

  private bool AcceptAttribute(IDBAttribute att) => this._thisAttId != att.AttributeID;

  private ICategoryTypeIconService IconService
  {
    get
    {
      if (this._iconService == null)
        this._iconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      return this._iconService;
    }
  }

  private int GetAttributeGroupImageIndex()
  {
    ICategoryTypeIconService iconService = this.IconService;
    return iconService == null ? -1 : this.TranslateImageIndex(iconService.IndexOf(12, 0));
  }

  private int GetAttributeImageIndex(FieldTypes fieldType)
  {
    ICategoryTypeIconService iconService = this.IconService;
    return iconService == null ? -1 : this.TranslateImageIndex(iconService.IndexOf(3, 0, (object) fieldType));
  }

  private int TranslateImageIndex(int globalImageIndex)
  {
    if (this._imageIndexes.ContainsKey((object) globalImageIndex))
      return (int) this._imageIndexes[(object) globalImageIndex];
    Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb);
    using (Graphics g = Graphics.FromImage((Image) bitmap))
    {
      using (SolidBrush solidBrush = new SolidBrush(this.imageList.TransparentColor))
      {
        g.FillRectangle((Brush) solidBrush, 0, 0, bitmap.Width, bitmap.Height);
        this.IconService.ImageList.Draw(g, 0, 0, globalImageIndex);
      }
    }
    int count = this.imageList.Images.Count;
    this.imageList.Images.Add((Image) bitmap);
    this._imageIndexes[(object) globalImageIndex] = (object) count;
    return count;
  }

  private void OnFunctionsTree_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode selectedNode = this.functionsTree.SelectedNode;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    if (selectedNode != null && selectedNode.Tag is Function tag)
    {
      string[] strArray = tag.Description.Split(new char[1]
      {
        '\n'
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length != 0)
      {
        empty1 = strArray[0];
        if (strArray.Length > 1)
          empty2 = strArray[1];
      }
    }
    this.lbFuncTitle.Text = empty1;
    this.lbFuncDescr.Text = empty2;
  }

  private void EditPanel_MouseClick(object sender, MouseEventArgs e)
  {
    int indexFromPosition = this.editPanel.GetCharIndexFromPosition(this.editPanel.PointToClient(Control.MousePosition));
    string text = this.editPanel.Text;
    int length = text.Length;
    try
    {
      if (indexFromPosition >= length)
        return;
      bool flag = length > 0 && text[0] == '@';
      int index1 = indexFromPosition;
      int index2 = indexFromPosition;
      while (index1 >= 0 && text[index1] != '[' && (!flag || text[index1] != '{'))
        --index1;
      while (index2 < length && text[index2] != ']' && (!flag || text[index2] != '}'))
        ++index2;
      string name = text.Substring(index1 + 1, index2 - index1 - 1);
      if (name.Length <= 0)
        return;
      this.LocateNode(this.varsTree.Nodes, name);
    }
    catch
    {
    }
  }

  private void LocateNode(TreeNodeCollection nodes, string name)
  {
    if (nodes == null)
      return;
    int count = nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      TreeNode node = nodes[index];
      if (node.Tag is Variable tag && tag.Name.Equals(name))
      {
        node.TreeView.SelectedNode = node;
        break;
      }
      this.LocateNode(node.Nodes, name);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      if (this._parser != null)
        this._parser.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExpressionEditor));
    this.okButton = new Button();
    this.cancelButton = new Button();
    this.panel1 = new Panel();
    this.editPanel = new ParserTexBox();
    this.leftDock = new DockContainer();
    this.dockManager = new DockManager();
    this.imageList = new ImageList(this.components);
    this.bottomDock = new DockContainer();
    this.fastDC = new DockControl();
    this.button36 = new Button();
    this.button32 = new Button();
    this.button33 = new Button();
    this.button34 = new Button();
    this.button35 = new Button();
    this.button21 = new Button();
    this.button29 = new Button();
    this.button30 = new Button();
    this.button31 = new Button();
    this.button25 = new Button();
    this.button26 = new Button();
    this.button27 = new Button();
    this.button28 = new Button();
    this.button22 = new Button();
    this.button23 = new Button();
    this.button24 = new Button();
    this.button15 = new Button();
    this.button16 = new Button();
    this.button17 = new Button();
    this.button18 = new Button();
    this.button19 = new Button();
    this.button20 = new Button();
    this.button9 = new Button();
    this.button10 = new Button();
    this.button11 = new Button();
    this.button12 = new Button();
    this.button13 = new Button();
    this.button14 = new Button();
    this.button8 = new Button();
    this.button7 = new Button();
    this.button6 = new Button();
    this.button5 = new Button();
    this.button4 = new Button();
    this.button3 = new Button();
    this.button2 = new Button();
    this.button1 = new Button();
    this.topDock = new DockContainer();
    this.rightDock = new DockContainer();
    this.functionsDC = new DockControl();
    this.splitContainer1 = new SplitContainer();
    this.functionsTree = new TreeView();
    this.lbFuncDescr = new Label();
    this.lbFuncTitle = new Label();
    this.splitter1 = new Splitter();
    this.functionsFilter = new FilterBox();
    this.varsDC = new DockControl();
    this.varsTree = new TreeView();
    this.varsFilter = new FilterBox();
    this.constDC = new DockControl();
    this.constTree = new TreeView();
    this.errLabel = new Label();
    this.panel1.SuspendLayout();
    this.bottomDock.SuspendLayout();
    this.fastDC.SuspendLayout();
    this.rightDock.SuspendLayout();
    this.functionsDC.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.varsDC.SuspendLayout();
    this.constDC.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.DialogResult = DialogResult.OK;
    this.okButton.Name = "okButton";
    this.okButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.editPanel);
    this.panel1.Controls.Add((Control) this.leftDock);
    this.panel1.Controls.Add((Control) this.bottomDock);
    this.panel1.Controls.Add((Control) this.topDock);
    this.panel1.Controls.Add((Control) this.rightDock);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.editPanel, "editPanel");
    this.editPanel.Exception = (ParseException) null;
    this.editPanel.Name = "editPanel";
    this.editPanel.MouseClick += new MouseEventHandler(this.EditPanel_MouseClick);
    this.editPanel.TextChanged += new EventHandler(this.EditPanel_TextChanged);
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Guid = new Guid("de3e4d6a-e492-41b7-b350-35c0b54e057a");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.leftDock.Manager = this.dockManager;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    this.dockManager.DockingManager = DockingManager.Whidbey;
    this.dockManager.DocumentContainer = (DocumentContainer) null;
    this.dockManager.ImageList = this.imageList;
    this.dockManager.OwnerForm = (Form) this;
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Magenta;
    this.imageList.Images.SetKeyName(0, "Folder.bmp");
    this.imageList.Images.SetKeyName(1, "FilderOpened.bmp");
    this.imageList.Images.SetKeyName(2, "Function.bmp");
    this.imageList.Images.SetKeyName(3, "Variable.bmp");
    this.imageList.Images.SetKeyName(4, "Constant.bmp");
    this.imageList.Images.SetKeyName(5, "Keyboard.bmp");
    this.bottomDock.Controls.Add((Control) this.fastDC);
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("54db05ee-80bb-4182-8af8-2417edf99396");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(719, 184, new DockControl[1]
      {
        this.fastDC
      }, this.fastDC)
    });
    this.bottomDock.Manager = this.dockManager;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    this.fastDC.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.fastDC.Closable = false;
    this.fastDC.Controls.Add((Control) this.button36);
    this.fastDC.Controls.Add((Control) this.button32);
    this.fastDC.Controls.Add((Control) this.button33);
    this.fastDC.Controls.Add((Control) this.button34);
    this.fastDC.Controls.Add((Control) this.button35);
    this.fastDC.Controls.Add((Control) this.button21);
    this.fastDC.Controls.Add((Control) this.button29);
    this.fastDC.Controls.Add((Control) this.button30);
    this.fastDC.Controls.Add((Control) this.button31);
    this.fastDC.Controls.Add((Control) this.button25);
    this.fastDC.Controls.Add((Control) this.button26);
    this.fastDC.Controls.Add((Control) this.button27);
    this.fastDC.Controls.Add((Control) this.button28);
    this.fastDC.Controls.Add((Control) this.button22);
    this.fastDC.Controls.Add((Control) this.button23);
    this.fastDC.Controls.Add((Control) this.button24);
    this.fastDC.Controls.Add((Control) this.button15);
    this.fastDC.Controls.Add((Control) this.button16);
    this.fastDC.Controls.Add((Control) this.button17);
    this.fastDC.Controls.Add((Control) this.button18);
    this.fastDC.Controls.Add((Control) this.button19);
    this.fastDC.Controls.Add((Control) this.button20);
    this.fastDC.Controls.Add((Control) this.button9);
    this.fastDC.Controls.Add((Control) this.button10);
    this.fastDC.Controls.Add((Control) this.button11);
    this.fastDC.Controls.Add((Control) this.button12);
    this.fastDC.Controls.Add((Control) this.button13);
    this.fastDC.Controls.Add((Control) this.button14);
    this.fastDC.Controls.Add((Control) this.button8);
    this.fastDC.Controls.Add((Control) this.button7);
    this.fastDC.Controls.Add((Control) this.button6);
    this.fastDC.Controls.Add((Control) this.button5);
    this.fastDC.Controls.Add((Control) this.button4);
    this.fastDC.Controls.Add((Control) this.button3);
    this.fastDC.Controls.Add((Control) this.button2);
    this.fastDC.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.fastDC, "fastDC");
    this.fastDC.FloatingLocation = new Point(515, 312);
    this.fastDC.Guid = new Guid("73fbac8f-4d8c-4b65-a85a-46ced1d47c27");
    this.fastDC.Name = "fastDC";
    this.fastDC.TabImage = (Image) componentResourceManager.GetObject("fastDC.TabImage");
    this.fastDC.TabImageIndex = 5;
    componentResourceManager.ApplyResources((object) this.button36, "button36");
    this.button36.Name = "button36";
    this.button36.UseVisualStyleBackColor = true;
    this.button36.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button32, "button32");
    this.button32.Name = "button32";
    this.button32.UseVisualStyleBackColor = true;
    this.button32.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button33, "button33");
    this.button33.Name = "button33";
    this.button33.UseVisualStyleBackColor = true;
    this.button33.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button34, "button34");
    this.button34.Name = "button34";
    this.button34.UseMnemonic = false;
    this.button34.UseVisualStyleBackColor = true;
    this.button34.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button35, "button35");
    this.button35.Name = "button35";
    this.button35.UseVisualStyleBackColor = true;
    this.button35.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button21, "button21");
    this.button21.Name = "button21";
    this.button21.UseVisualStyleBackColor = true;
    this.button21.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button29, "button29");
    this.button29.Name = "button29";
    this.button29.UseVisualStyleBackColor = true;
    this.button29.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button30, "button30");
    this.button30.Name = "button30";
    this.button30.UseMnemonic = false;
    this.button30.UseVisualStyleBackColor = true;
    this.button30.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button31, "button31");
    this.button31.Name = "button31";
    this.button31.UseVisualStyleBackColor = true;
    this.button31.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button25, "button25");
    this.button25.Name = "button25";
    this.button25.UseVisualStyleBackColor = true;
    this.button25.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button26, "button26");
    this.button26.Name = "button26";
    this.button26.UseVisualStyleBackColor = true;
    this.button26.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button27, "button27");
    this.button27.Name = "button27";
    this.button27.UseVisualStyleBackColor = true;
    this.button27.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button28, "button28");
    this.button28.Name = "button28";
    this.button28.UseVisualStyleBackColor = true;
    this.button28.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button22, "button22");
    this.button22.Name = "button22";
    this.button22.UseVisualStyleBackColor = true;
    this.button22.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button23, "button23");
    this.button23.Name = "button23";
    this.button23.UseVisualStyleBackColor = true;
    this.button23.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button24, "button24");
    this.button24.Name = "button24";
    this.button24.UseVisualStyleBackColor = true;
    this.button24.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button15, "button15");
    this.button15.Name = "button15";
    this.button15.UseVisualStyleBackColor = true;
    this.button15.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button16, "button16");
    this.button16.Name = "button16";
    this.button16.UseVisualStyleBackColor = true;
    this.button16.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button17, "button17");
    this.button17.Name = "button17";
    this.button17.UseVisualStyleBackColor = true;
    this.button17.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button18, "button18");
    this.button18.Name = "button18";
    this.button18.UseVisualStyleBackColor = true;
    this.button18.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button19, "button19");
    this.button19.Name = "button19";
    this.button19.UseVisualStyleBackColor = true;
    this.button19.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button20, "button20");
    this.button20.Name = "button20";
    this.button20.UseVisualStyleBackColor = true;
    this.button20.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button9, "button9");
    this.button9.Name = "button9";
    this.button9.UseVisualStyleBackColor = true;
    this.button9.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button10, "button10");
    this.button10.Name = "button10";
    this.button10.UseVisualStyleBackColor = true;
    this.button10.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button11, "button11");
    this.button11.Name = "button11";
    this.button11.UseVisualStyleBackColor = true;
    this.button11.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button12, "button12");
    this.button12.Name = "button12";
    this.button12.UseVisualStyleBackColor = true;
    this.button12.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button13, "button13");
    this.button13.Name = "button13";
    this.button13.UseVisualStyleBackColor = true;
    this.button13.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button14, "button14");
    this.button14.Name = "button14";
    this.button14.UseVisualStyleBackColor = true;
    this.button14.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button8, "button8");
    this.button8.Name = "button8";
    this.button8.UseVisualStyleBackColor = true;
    this.button8.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button7, "button7");
    this.button7.Name = "button7";
    this.button7.UseVisualStyleBackColor = true;
    this.button7.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button6, "button6");
    this.button6.Name = "button6";
    this.button6.UseVisualStyleBackColor = true;
    this.button6.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button5, "button5");
    this.button5.Name = "button5";
    this.button5.UseVisualStyleBackColor = true;
    this.button5.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button4, "button4");
    this.button4.Name = "button4";
    this.button4.UseVisualStyleBackColor = true;
    this.button4.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.UseVisualStyleBackColor = true;
    this.button3.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.FastButton_Click);
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("ff36f7d3-6df1-4340-ba53-2334a51222d5");
    this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.topDock.Manager = this.dockManager;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    this.rightDock.Controls.Add((Control) this.functionsDC);
    this.rightDock.Controls.Add((Control) this.varsDC);
    this.rightDock.Controls.Add((Control) this.constDC);
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Guid = new Guid("e992f118-f819-4e0e-88c6-0211b020b909");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(280, 499, new DockControl[3]
      {
        this.functionsDC,
        this.varsDC,
        this.constDC
      }, this.functionsDC)
    });
    this.rightDock.Manager = this.dockManager;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    this.functionsDC.BackColor = SystemColors.Control;
    this.functionsDC.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.functionsDC.Closable = false;
    this.functionsDC.Controls.Add((Control) this.splitContainer1);
    this.functionsDC.Controls.Add((Control) this.splitter1);
    this.functionsDC.Controls.Add((Control) this.functionsFilter);
    componentResourceManager.ApplyResources((object) this.functionsDC, "functionsDC");
    this.functionsDC.FloatingLocation = new Point(515, 312);
    this.functionsDC.Guid = new Guid("c01e5773-165b-43b1-95ab-847f191b67df");
    this.functionsDC.Name = "functionsDC";
    this.functionsDC.TabImage = (Image) componentResourceManager.GetObject("functionsDC.TabImage");
    this.functionsDC.TabImageIndex = 2;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.FixedPanel = FixedPanel.Panel2;
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.functionsTree);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lbFuncDescr);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lbFuncTitle);
    this.functionsTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
    componentResourceManager.ApplyResources((object) this.functionsTree, "functionsTree");
    this.functionsTree.HideSelection = false;
    this.functionsTree.ImageList = this.imageList;
    this.functionsTree.LineColor = Color.LightGray;
    this.functionsTree.Name = "functionsTree";
    this.functionsTree.AfterSelect += new TreeViewEventHandler(this.OnFunctionsTree_AfterSelect);
    this.functionsTree.DoubleClick += new EventHandler(this.OnFunctionsTree_DoubleClick);
    this.functionsTree.KeyPress += new KeyPressEventHandler(this.OnFunctionsTree_KeyPress);
    this.lbFuncDescr.AutoEllipsis = true;
    componentResourceManager.ApplyResources((object) this.lbFuncDescr, "lbFuncDescr");
    this.lbFuncDescr.Name = "lbFuncDescr";
    this.lbFuncTitle.AutoEllipsis = true;
    componentResourceManager.ApplyResources((object) this.lbFuncTitle, "lbFuncTitle");
    this.lbFuncTitle.Name = "lbFuncTitle";
    this.splitter1.BackColor = Color.Silver;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.functionsFilter, "functionsFilter");
    this.functionsFilter.FindText = "";
    this.functionsFilter.Name = "functionsFilter";
    this.functionsFilter.Tag = (object) "  ";
    this.functionsFilter.Clear += new EventHandler(this.OnFunctionsFilter_Clear);
    this.functionsFilter.Find += new EventHandler(this.OnFunctionsFilter_Find);
    this.varsDC.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.varsDC.Closable = false;
    this.varsDC.Controls.Add((Control) this.varsTree);
    this.varsDC.Controls.Add((Control) this.varsFilter);
    componentResourceManager.ApplyResources((object) this.varsDC, "varsDC");
    this.varsDC.FloatingLocation = new Point(515, 312);
    this.varsDC.Guid = new Guid("9da29f8c-a33b-4aae-8c0d-26be2a23ef7a");
    this.varsDC.Name = "varsDC";
    this.varsDC.TabImage = (Image) componentResourceManager.GetObject("varsDC.TabImage");
    this.varsDC.TabImageIndex = 3;
    this.varsTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
    componentResourceManager.ApplyResources((object) this.varsTree, "varsTree");
    this.varsTree.HideSelection = false;
    this.varsTree.ImageList = this.imageList;
    this.varsTree.Name = "varsTree";
    this.varsTree.DoubleClick += new EventHandler(this.OnVarsTree_DoubleClick);
    this.varsTree.KeyPress += new KeyPressEventHandler(this.OnVarsTree_KeyPress);
    componentResourceManager.ApplyResources((object) this.varsFilter, "varsFilter");
    this.varsFilter.FindText = "";
    this.varsFilter.Name = "varsFilter";
    this.varsFilter.Tag = (object) "  ";
    this.varsFilter.Clear += new EventHandler(this.OnVarsFilter_Clear);
    this.varsFilter.Find += new EventHandler(this.OnVarsFilter_Find);
    this.constDC.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.constDC.Closable = false;
    this.constDC.Controls.Add((Control) this.constTree);
    componentResourceManager.ApplyResources((object) this.constDC, "constDC");
    this.constDC.FloatingLocation = new Point(515, 312);
    this.constDC.Guid = new Guid("73ec9021-0943-4d9a-a45f-ddd26bf8ffda");
    this.constDC.Name = "constDC";
    this.constDC.PrimaryControl = (Control) this.constTree;
    this.constDC.TabImage = (Image) componentResourceManager.GetObject("constDC.TabImage");
    this.constDC.TabImageIndex = 4;
    this.constTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
    componentResourceManager.ApplyResources((object) this.constTree, "constTree");
    this.constTree.HideSelection = false;
    this.constTree.ImageList = this.imageList;
    this.constTree.Name = "constTree";
    this.constTree.DoubleClick += new EventHandler(this.OnConstTree_DoubleClick);
    this.constTree.KeyPress += new KeyPressEventHandler(this.OnConstTree_KeyPress);
    componentResourceManager.ApplyResources((object) this.errLabel, "errLabel");
    this.errLabel.ForeColor = Color.Red;
    this.errLabel.Name = "errLabel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.cancelButton;
    this.Controls.Add((Control) this.errLabel);
    this.Controls.Add((Control) this.cancelButton);
    this.Controls.Add((Control) this.okButton);
    this.Controls.Add((Control) this.panel1);
    this.DoubleBuffered = true;
    this.Name = nameof (ExpressionEditor);
    this.FormClosing += new FormClosingEventHandler(this.ExpressionEditor_FormClosing);
    this.Load += new EventHandler(this.ExpressionEditor_Load);
    this.Shown += new EventHandler(this.ExpressionEditor_Shown);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.bottomDock.ResumeLayout(false);
    this.fastDC.ResumeLayout(false);
    this.rightDock.ResumeLayout(false);
    this.functionsDC.ResumeLayout(false);
    this.functionsDC.PerformLayout();
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.varsDC.ResumeLayout(false);
    this.varsDC.PerformLayout();
    this.constDC.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  internal class NodeComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      TreeNode treeNode1 = x as TreeNode;
      TreeNode treeNode2 = y as TreeNode;
      return treeNode1.Tag is int && treeNode2.Tag is int ? (int) treeNode1.Tag - (int) treeNode2.Tag : treeNode1.Text.CompareTo(treeNode2.Text);
    }
  }
}
