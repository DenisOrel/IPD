// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GraphView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Map;
using Intermech.Navigator.DBObjects;
using Intermech.PropertyEditors;
using Intermech.Remoting.Sponsors;
using Intermech.Workflow.Briefcase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
public class GraphView : MapView, IBriefcaseContext
{
  private wfEditorForm _form;
  private long _processID;
  private bool _isProcess;
  internal List<MapObject> DeletedObjects = new List<MapObject>();
  private List<int> _deletedVars = new List<int>();
  private bool _modified;
  public LinkKind CurrentLinkKind;
  private ContextMenuBarItem _contextMenu;
  private MapObject myPrimarySelection;
  private MenuItemBase _saveMI;
  private MenuItemBase _pasteMI;
  private bool _isSPressed;
  private Pen _myLinkHighlightPen = new Pen(Color.DeepPink, 7f);
  private Pen _myPortHighlightPen = new Pen(Color.Red, 2f);
  private Brush _myPortHighlightBrush;
  private bool _myOriginalScale = true;
  private PointF _myOriginalDocPosition;
  private float _myOriginalDocScale = 1f;
  private bool _justCreated;
  private bool _loading;
  private int _processVersion;
  private List<Intermech.Expressions.Variable> _allObjectsAttributes = new List<Intermech.Expressions.Variable>(0);
  private bool _isEditorClosed;
  private Dictionary<long, List<int>> _activitiesWithScripts;
  private readonly object _activitiesWithScriptsLock = new object();
  private SimpleBriefcase _briefcase;

  public bool CurrentLinkIsBackward => this.CurrentLinkKind == LinkKind.Backward;

  public wfEditorForm Form
  {
    get => this._form;
    set => this._form = value;
  }

  public IDBObject GetProcess(IUserSession session)
  {
    IDBObject dbObject = session.GetObject(this.ProcessID, false);
    if (dbObject == null && this.ProcessID < 0L)
    {
      this.ProcessID *= -1L;
      dbObject = session.GetObject(this.ProcessID, false);
    }
    return dbObject != null ? dbObject : throw new Exception($"Объект процесса '{this.ProcessID}' не найден!");
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ProcessID
  {
    get => this._processID;
    set => this._processID = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string ProcessName
  {
    get
    {
      if (this.DesignMode)
        return string.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return this.GetProcessName(this.GetProcess(sessionKeeper.Session));
    }
    set
    {
      if (this.DesignMode)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.SetProcessName(this.GetProcess(sessionKeeper.Session), value, true);
    }
  }

  private void SetProcessName(IDBObject proc, string name, bool refreshTitle)
  {
    IDBAttribute attributeById = proc.GetAttributeByID(wfConsts.AttrNameID);
    if (attributeById != null)
      attributeById.AsString = name;
    if (!refreshTitle)
      return;
    this.RefreshTitle(name);
  }

  private string GetProcessName(IDBObject proc)
  {
    IDBAttribute attributeById = proc.GetAttributeByID(wfConsts.AttrNameID);
    return attributeById != null ? attributeById.AsString : "???";
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Guid ProcessGuid
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return (this.GetProcess(sessionKeeper.Session) as IDBGuid).GUID;
    }
  }

  public GraphView() => this.DragsRealtime = true;

  /// <summary>A new GraphView will have a GraphDoc as its document.</summary>
  /// <returns>A <see cref="T:Intermech.Workflow.Design.GraphDoc" /></returns>
  public override MapDocument CreateDocument()
  {
    GraphDoc document = new GraphDoc();
    document.View = this;
    document.UndoManager = new MapUndoManager();
    document.Loaded += new EventHandler(this.doc_Loaded);
    return (MapDocument) document;
  }

  public override IMapLink CreateLink(IMapPort from, IMapPort to)
  {
    WorkflowNode node1 = from.Node as WorkflowNode;
    WorkflowNode node2 = to.Node as WorkflowNode;
    LinkKind kind = this.CurrentLinkKind;
    TempFormula tf = (TempFormula) null;
    ExpressionInfo expression = (ExpressionInfo) null;
    bool flag1 = false;
    if (kind == LinkKind.Forward)
    {
      if (node1.ActivityType == wfConsts.CondTypeID)
        kind = CondLinkForm.QueryLinkKind();
      else if (node1.ActivityType == wfConsts.CaseTypeID)
      {
        if (!node1.UseExpertSystem)
        {
          int objectTypeForLink = -1;
          bool flag2 = false;
          if (this.ProcessID != 0L && node1.IsFlagSet(ActivityFlags.FilterObjects))
          {
            flag2 = true;
            List<int> applicableAttachmentTypes = wfFunx.GetApplicableAttachmentTypes(wfConsts.ActivitiesTypeID, wfConsts.AttachmentRelationTypeID);
            new AllowedTypes(this.ProcessID).Filter(applicableAttachmentTypes);
            SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Любой тип объекта", typeof (ObjectTypeFolder), false)
            {
              SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(applicableAttachmentTypes.ToArray(), true, true)
            };
            if (selectorForm.ShowDialog() != DialogResult.OK)
              return (IMapLink) null;
            if (selectorForm.IDList.Count > 0)
              objectTypeForLink = Convert.ToInt32(selectorForm.IDList[0]);
          }
          expression = new ExpressionInfo(objectTypeForLink, Guid.Empty, -1L, string.Empty);
          List<Intermech.Expressions.Variable> activityVariable = new List<Intermech.Expressions.Variable>(0);
          List<AttributeValues> attributeValuesList = new List<AttributeValues>(0);
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (this.GetProcess(sessionKeeper.Session) is IScheme process)
            {
              foreach (Intermech.Interfaces.Workflow.IVariable variable in (IEnumerable) process.Variables)
              {
                Intermech.Expressions.Variable expressionVariable = MiscFunx.CreateExpressionVariable(variable.Name, variable.Type);
                activityVariable.Add(expressionVariable);
                AttributeValues attributeValues = new AttributeValues(variable.AttributeID, variable.TypedValue)
                {
                  AttributeName = variable.Name
                };
                attributeValuesList.Add(attributeValues);
              }
              foreach (Intermech.Interfaces.Workflow.IVariable globalVariable in (IEnumerable) process.GlobalVariables)
              {
                Intermech.Expressions.Variable expressionVariable = MiscFunx.CreateExpressionVariable(globalVariable.Name, globalVariable.Type);
                activityVariable.Add(expressionVariable);
                AttributeValues attributeValues = new AttributeValues(globalVariable.AttributeID, globalVariable.TypedValue)
                {
                  AttributeName = globalVariable.Name
                };
                attributeValuesList.Add(attributeValues);
              }
            }
            attributeValuesList.AddRange((IEnumerable<AttributeValues>) sessionKeeper.Session.GetObjectAttributesValues(node1.ActivityID, GetAttributeValuesModes.IncludeObligatoryAttributes, false, false));
            List<Intermech.Expressions.Variable> variables = new List<Intermech.Expressions.Variable>(0);
            if (expression.ObjectTypeForLink == -1)
            {
              if (flag2)
              {
                if (this.AllObjectsAttributes.Count > 0)
                  variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) this.AllObjectsAttributes);
                else
                  variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.GetAllAttributesVariables());
              }
            }
            else
            {
              BasicAttributeProperties[] enabledAttributes = sessionKeeper.Session.GetObjectType(expression.ObjectTypeForLink).Attributes.GetEnabledAttributes(true);
              variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.ConvertBasicAttributePropertiesToVariable(enabledAttributes));
            }
            kind = CaseLinkForm.QueryLinkKind(ref expression, variables, activityVariable, attributeValuesList.ToArray());
          }
        }
        else
        {
          tf = new TempFormula();
          tf.Init();
          kind = CaseLinkForm.QueryLinkKind(ref tf, this._processID, node1.IsFlagSet(ActivityFlags.FilterObjects));
        }
      }
      if (kind == LinkKind.Backward)
        return (IMapLink) null;
      flag1 = true;
    }
    bool flag3 = false;
    if (node2.ActivityType == wfConsts.TimerTypeID)
    {
      using (TimerLinkForm timerLinkForm = new TimerLinkForm())
      {
        switch (timerLinkForm.ShowDialog())
        {
          case DialogResult.Cancel:
            return (IMapLink) null;
          case DialogResult.Abort:
            flag3 = true;
            break;
        }
      }
    }
    WorkflowLink link = WorkflowLinkCreator.Create(kind);
    link.FromPort = from;
    link.ToPort = to;
    link.LinkKind = kind;
    link.Backward = this.CurrentLinkIsBackward;
    link.ResetTimer = flag3;
    switch (kind)
    {
      case LinkKind.Backward:
        node1.LinksChanged();
        break;
      case LinkKind.ParallelBlock:
        node2.LinksChanged();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = node2.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrCollectorID);
          if (attributeById != null)
          {
            if (node2.ActivityKind != ActivityKind.Stop)
              attributeById.AsBoolean = true;
          }
        }
        node2.IsParallelBlockFinish = true;
        node2.UpdateInfoImages();
        break;
    }
    if (flag1 && (node1.ExpertConditions != null || node1.ExpressionConditions != null))
    {
      link.InitServerObjectIfNeed();
      if (node1.UseExpertSystem)
        node1.ExpertConditions?.Add(link.LinkID, tf);
      else if (expression != null)
      {
        expression.LinkID = Math.Abs(link.LinkID);
        node1.ExpressionConditions?.Add(expression);
      }
      node1.SaveConditions();
      node1.UpdateInfoImages();
    }
    link.UpdateCaption();
    link.UpdateNodesRefs();
    this.Document.LinksLayer.Add((MapObject) link);
    if (flag3)
    {
      link.InitServerObjectIfNeed();
      node2.ResetTimerLinks.Add(Math.Abs(link.LinkID));
    }
    return (IMapLink) link;
  }

  /// <summary>
  /// A convenience property for getting the view's MapDocument as a GraphDoc.
  /// </summary>
  public GraphDoc Doc => this.Document as GraphDoc;

  public virtual void UpdateFormInfo()
  {
    this.UpdateTitle();
    this.Form?.SetPropertiesInfo((object) this);
  }

  public virtual void UpdateTitle()
  {
    if (this._form == null)
      return;
    string name = this.Document.Name;
    if (this.ReadOnly)
      name += LocalizationHolder.rm.GetString("Workflow.Design_48");
    if (this.Modified)
      name += "*";
    this._form.Text = name;
    if (!(this._form.Parent is DockControl parent))
      return;
    parent.Text = name;
  }

  public void RefreshTitle()
  {
    this.Document.Name = CaptionTransform.GetCaption(this.ProcessName, (long) this._processVersion);
    this.UpdateTitle();
  }

  public void RefreshTitle(string newTitle)
  {
    this.Document.Name = newTitle;
    this.UpdateTitle();
  }

  /// <summary>
  /// Add page numbers to printed pages, in case someone drops the pages and needs to resort them.
  /// </summary>
  /// <param name="g"></param>
  /// <param name="e"></param>
  /// <param name="hpnum"></param>
  /// <param name="hpmax"></param>
  /// <param name="vpnum"></param>
  /// <param name="vpmax"></param>
  protected override void PrintDecoration(
    Graphics g,
    PrintPageEventArgs e,
    int hpnum,
    int hpmax,
    int vpnum,
    int vpmax)
  {
    string str = $"{hpnum.ToString()},{vpnum.ToString()}";
    Font font = new Font("Verdana", 10f);
    SizeF sizeF = g.MeasureString(str, font);
    PointF point;
    ref PointF local = ref point;
    Rectangle marginBounds = e.MarginBounds;
    int x1 = marginBounds.X;
    marginBounds = e.MarginBounds;
    int num = marginBounds.Width / 2;
    double x2 = (double) (x1 + num) - (double) sizeF.Width / 2.0;
    marginBounds = e.MarginBounds;
    int y1 = marginBounds.Y;
    marginBounds = e.MarginBounds;
    int height = marginBounds.Height;
    double y2 = (double) (y1 + height);
    local = new PointF((float) x2, (float) y2);
    g.DrawString(str, font, Brushes.Blue, point);
    base.PrintDecoration(g, e, hpnum, hpmax, vpnum, vpmax);
  }

  protected override void OnDoubleClick(EventArgs e)
  {
    base.OnDoubleClick(e);
    if (!this.Selection.IsEmpty)
      return;
    this.Properties_Command((object) null, (EventArgs) null);
  }

  /// <summary>
  /// If the document's name changes, update the title;
  /// if the document's location changes, update the status bar
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="evt"></param>
  protected override void OnDocumentChanged(object sender, MapChangedEventArgs evt)
  {
    base.OnDocumentChanged(sender, evt);
    if (this._loading || evt.Hint == 100 || evt.Object is MapImageEx || evt.Object is MapPort || evt.Object == null && evt.Hint == 202)
      return;
    WorkflowNode workflowNode1 = (WorkflowNode) null;
    if (evt.Object is WorkflowNode workflowNode2)
    {
      workflowNode1 = workflowNode2;
      if (workflowNode1.UpdatingInfoImages)
        return;
    }
    if (evt.Hint == 902)
    {
      if (workflowNode1 != null)
      {
        if (workflowNode1.ActivityID != 0L)
        {
          if (workflowNode1.Copied)
          {
            workflowNode1.InitClone();
            workflowNode1.ReplaceLocalScripts();
          }
          else
            this.DeletedObjects.Remove((MapObject) workflowNode1);
        }
        else
          workflowNode1.InitNew(this.ProcessID);
        workflowNode1.View = this;
        workflowNode1.JustCreated = true;
        if (workflowNode1.ActivityType == wfConsts.SubProcessTypeID && workflowNode1.View.Doc != null && !workflowNode1.View.Doc.DocHaveSubProcess)
        {
          workflowNode1.View.Doc.DocHaveSubProcess = true;
          this.UpdateCommands();
        }
      }
      else if (evt.Object is WorkflowLink workflowLink1)
      {
        if (workflowLink1.LinkID != 0L)
        {
          if (workflowLink1.Copied)
            workflowLink1.InitClone();
          else
            this.DeletedObjects.Remove((MapObject) workflowLink1);
        }
        else
          workflowLink1.InitNew(this.ProcessID);
      }
    }
    else if (evt.Hint == 903)
    {
      if (workflowNode1 != null)
      {
        this.DeletedObjects.Add((MapObject) workflowNode1);
        workflowNode1.Deleted();
      }
      else if (evt.Object is WorkflowLink workflowLink2)
      {
        workflowLink2.DeletedFromDocument();
        this.DeletedObjects.Add((MapObject) workflowLink2);
      }
    }
    this.SetModified(true);
  }

  /// <summary>
  /// If the view's document is replaced, update the title;
  /// if the view's scale changes, update the status bar
  /// </summary>
  /// <param name="evt"></param>
  protected override void OnPropertyChanged(PropertyChangedEventArgs evt)
  {
    base.OnPropertyChanged(evt);
    if (!(evt.PropertyName == "Document"))
      return;
    this.UpdateFormInfo();
  }

  protected override void OnObjectGotSelection(MapSelectionEventArgs evt)
  {
    base.OnObjectGotSelection(evt);
    if (!object.Equals((object) this.myPrimarySelection, (object) this.Selection.Primary))
      this.myPrimarySelection = this.Selection.Primary;
    this.Form?.SetPropertiesInfo((object) this.Selection.Primary);
  }

  protected override void OnObjectLostSelection(MapSelectionEventArgs evt)
  {
    base.OnObjectLostSelection(evt);
    if (!object.Equals((object) this.myPrimarySelection, (object) this.Selection.Primary))
      this.myPrimarySelection = this.Selection.Primary;
    this.Form?.SetPropertiesInfo((object) this.Selection.Primary);
  }

  public override void EditCut()
  {
    string message = string.Empty;
    foreach (MapObject mapObject in (MapCollection) this.Selection)
    {
      if (mapObject is WorkflowLink workflowLink && (workflowLink.LinkKind == LinkKind.True || workflowLink.LinkKind == LinkKind.False))
      {
        WorkflowNode fromNode = workflowLink.FromNode as WorkflowNode;
        WorkflowNode toNode = workflowLink.ToNode as WorkflowNode;
        if (string.IsNullOrEmpty(message))
          message = $"Внимание! В копируемом участке содержатся связи от действий с условием, данные связи следует исключить из копируемых. Список действий содержащие данные связи: \nДействие выбора: '{fromNode.Text}'. Действие куда приходит ссылка: '{toNode.Text}'\n";
        else
          message = $"{message}Действие выбора: '{fromNode.Text}'. Действие куда приходит ссылка: '{toNode.Text}'\n";
      }
    }
    if (!string.IsNullOrEmpty(message))
      throw new KernelException(message);
    base.EditCut();
  }

  public override void EditCopy()
  {
    string message = string.Empty;
    foreach (MapObject mapObject in (MapCollection) this.Selection)
    {
      if (mapObject is WorkflowLink workflowLink && (workflowLink.LinkKind == LinkKind.True || workflowLink.LinkKind == LinkKind.False))
      {
        WorkflowNode fromNode = workflowLink.FromNode as WorkflowNode;
        WorkflowNode toNode = workflowLink.ToNode as WorkflowNode;
        if (string.IsNullOrEmpty(message))
          message = $"Внимание! В копируемом участке содержатся связи от действий с условием, данные связи следует исключить из копируемых. Список действий содержащие данные связи: \nДействие выбора: '{fromNode.Text}'. Действие куда приходит ссылка: '{toNode.Text}'\n";
        else
          message = $"{message}Действие выбора: '{fromNode.Text}'. Действие куда приходит ссылка: '{toNode.Text}'\n";
      }
    }
    if (!string.IsNullOrEmpty(message))
      throw new KernelException(message);
    base.EditCopy();
  }

  /// <summary>Проверить схему на валидность</summary>
  /// <param name="proc"></param>
  /// <param name="checkSubProcessSchemes">проверять ли подпроцессы на наличие отладочных шаблонов</param>
  /// <returns></returns>
  public string ValidateScheme(IDBObject proc, bool checkSubProcessSchemes = true)
  {
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    List<WorkflowNode> workflowNodeList = new List<WorkflowNode>(0);
    foreach (MapObject mapObject in this.Document)
    {
      if (mapObject is WorkflowNode workflowNode)
      {
        longList1.Add(workflowNode.ActivityID);
        workflowNode.UpdateInfoImages();
        if (workflowNode.IsParallelBlockFinish)
          workflowNodeList.Add(workflowNode);
      }
      else if (mapObject is WorkflowLink workflowLink)
        longList2.Add(workflowLink.LinkID);
    }
    string s = proc is IScheme scheme ? scheme.Validate(longList1.ToArray(), longList2.ToArray(), this.DeletedObjectIDs.ToArray(), checkSubProcessSchemes) : (string) null;
    foreach (WorkflowNode workflowNode in workflowNodeList)
    {
      foreach (WorkflowLink link in workflowNode.Links)
      {
        if (link != null && link.LinkKind == LinkKind.Backward)
        {
          string str = $"Конец блока параллельного выполнения на действии \"{workflowNode.Text}\" не может содержать обратные ссылки";
          MiscFunx.AddNewLined(ref s, str);
          workflowNode.SetInvalidIcon(str);
          break;
        }
      }
    }
    return s;
  }

  public string ValidateScheme()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject dbObject = this.GetProcess(sessionKeeper.Session);
        if (dbObject == null)
          return "Объект шаблона процесса не получен.";
        remoteLock.Add((object) dbObject);
        string str = this.ValidateScheme(dbObject);
        SchemeStatus schemeStatus = str == string.Empty ? SchemeStatus.Valid : SchemeStatus.Invalid;
        ((ISchemeCheckOut) dbObject).CheckOutSchemeWithoutEditable = wfConsts.CheckOutMode;
        if (this.ReadOnly && dbObject.CheckoutBy == 0L)
          dbObject = dbObject.CheckOut();
        IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrActivityStatusID);
        try
        {
          if (attributeById != null)
          {
            if (attributeById.AsInteger != (long) schemeStatus)
              attributeById.AsInteger = (long) schemeStatus;
          }
          else
            dbObject.Attributes.AddAttribute(wfConsts.AttrActivityStatusID, false, new object[1]
            {
              (object) (long) schemeStatus
            });
          if (!this.ReadOnly)
            this.Modified = true;
        }
        finally
        {
          if (this.ReadOnly && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
            dbObject.CheckIn();
        }
        return str;
      }
    }
  }

  public void ValidateScheme(object sender, EventArgs e)
  {
    MessageBoxIcon icon = MessageBoxIcon.Hand;
    string str = this.ValidateScheme();
    if (string.IsNullOrEmpty(str))
    {
      str = LocalizationHolder.rm.GetString("Workflow.Design_49");
      icon = MessageBoxIcon.Asterisk;
    }
    int num = (int) MessageBox.Show($"{LocalizationHolder.rm.GetString("Workflow.Design_50")}\r\n{str}", LocalizationHolder.rm.GetString("Workflow.Design_51"), MessageBoxButtons.OK, icon);
  }

  /// <summary>Устанавливаем схему в релиз</summary>
  public void SetSchemeToRelease(Label debugLabel)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject process = this.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) process);
        string str = this.ValidateScheme(process);
        if (!string.IsNullOrEmpty(str))
          throw new KernelException("Невозможно завершить режим отладки т.к. шаблон является некорректным: \n " + str);
        IDBAttribute byId = process.Attributes.FindByID(wfConsts.AttrIsDebugID);
        if (byId == null)
          return;
        byId.AsBoolean = false;
        this.Modified = true;
        debugLabel.Visible = false;
      }
    }
  }

  public bool SchemeIsDebug()
  {
    if (this.ProcessID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute byId = this.GetProcess(sessionKeeper.Session).Attributes.FindByID(wfConsts.AttrIsDebugID);
      return byId != null && byId.AsBoolean;
    }
  }

  public bool SchemeHaveSubProcess()
  {
    if (this.Doc != null)
      return this.Doc.DocHaveSubProcess;
    foreach (MapObject mapObject in this.Document)
    {
      if (mapObject is WorkflowNode workflowNode && workflowNode.ActivityKind == ActivityKind.SubProcess)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Проверка схемы на валидность без дополнительной проверки отладочности шаблона на действии подпроцесс
  /// </summary>
  /// <returns></returns>
  public bool SchemeHasInvalidSubProcess()
  {
    bool flag = false;
    foreach (MapObject mapObject in this.Document)
    {
      if (mapObject is WorkflowNode workflowNode && workflowNode.ActivityKind == ActivityKind.SubProcess)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject proc = sessionKeeper.Session.GetObject(this.ProcessID, false);
      if (proc == null && this.ProcessID < 0L)
      {
        this.ProcessID *= -1L;
        proc = sessionKeeper.Session.GetObject(this.ProcessID, false);
      }
      return !string.IsNullOrEmpty(this.ValidateScheme(proc, false));
    }
  }

  /// <summary>
  /// Снять режим отладки головному шаблону и всем шаблонам в действиях подпроцесс
  /// </summary>
  /// <param name="isDebugLabel"></param>
  public void SetAllSchemesToRelease(Label isDebugLabel)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject process = this.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) process);
        string str = this.ValidateScheme(process, false);
        if (!string.IsNullOrEmpty(str))
          throw new KernelException("Невозможно завершить режим отладки т.к. шаблон является некорректным: \n " + str);
        IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
        customService.StartTransaction();
        bool flag = true;
        MapLayerCollectionObjectEnumerator enumerator = this.Document.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current is WorkflowNode current && current.ActivityKind == ActivityKind.SubProcess && !this.SetSubProcessSchemesToRelease(current.ActivityID, sessionKeeper.Session))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          IDBAttribute byId = process.Attributes.FindByID(wfConsts.AttrIsDebugID);
          if (byId != null)
          {
            byId.AsBoolean = false;
            this.Modified = true;
            isDebugLabel.Visible = false;
          }
          customService.Commit();
        }
        else
        {
          customService.Rollback();
          throw new KernelException("Невозможно завершить режим отладки, обнаружены проблемы в подпроцессах.");
        }
      }
    }
  }

  private bool SetSubProcessSchemesToRelease(long subProcessActivityID, IUserSession userSession)
  {
    if (!(userSession.GetObject(subProcessActivityID, false) is ISubProcess subProcess1))
      return true;
    if (!(userSession.GetObject(subProcess1.SubSchemeID, false) is IScheme scheme))
      return false;
    using (RemoteLock remoteLock = new RemoteLock())
    {
      remoteLock.Add((object) scheme);
      if (string.IsNullOrEmpty(scheme.Validate(false)))
      {
        List<IActivity> list = ((IEnumerable<IActivity>) scheme.Activities).Where<IActivity>((System.Func<IActivity, bool>) (x => x.Kind == ActivityKind.SubProcess)).ToList<IActivity>();
        if (list.Count <= 0)
          return this.SetDebugAttribute(userSession, scheme);
        foreach (IActivity activity in list)
        {
          if (activity is ISubProcess subProcess2 && !this.SetSubProcessSchemesToRelease(subProcess2.ObjectID, userSession))
            return false;
        }
        return this.SetDebugAttribute(userSession, scheme);
      }
    }
    return false;
  }

  private bool SetDebugAttribute(IUserSession userSession, IScheme subProcessScheme)
  {
    if (subProcessScheme.CheckoutBy == 0L)
      subProcessScheme = subProcessScheme.CheckOut(false) as IScheme;
    long? checkoutBy = subProcessScheme?.CheckoutBy;
    long userId = userSession.UserID;
    if (!(checkoutBy.GetValueOrDefault() == userId & checkoutBy.HasValue))
      return false;
    IDBAttribute byId = subProcessScheme.Attributes.FindByID(wfConsts.AttrIsDebugID);
    if (byId != null)
      byId.AsBoolean = false;
    subProcessScheme.CheckIn();
    return true;
  }

  public void ProcessHistory_Command(object sender, EventArgs e)
  {
    wfFunx.ShowProcessHistory(this.ProcessID);
  }

  public void LaunchProcess_Command(object sender, EventArgs e)
  {
    if (this._form != null && this._form.View.Modified)
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Design_52"));
    wfFunx.CreateProcess(this.ProcessID);
  }

  public void Save_Command(object sender, EventArgs e) => this._form.Save();

  public void SaveAs_Command(object sender, EventArgs e) => this._form.SaveAs();

  private ContextMenuBarItem ContextMenu
  {
    get
    {
      if (this._contextMenu == null)
      {
        this._contextMenu = new ContextMenuBarItem();
        if (this.CanInsertObjects())
        {
          this._pasteMI = (MenuItemBase) this._contextMenu.Items[this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_53"), new EventHandler(this.Paste_Command))];
          this._pasteMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgPaste");
        }
        if (this._isProcess)
        {
          this._contextMenu.Items[this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_54"), new EventHandler(this.ProcessHistory_Command))].BeginGroup = true;
        }
        else
        {
          this._saveMI = (MenuItemBase) this._contextMenu.Items[this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_55"), new EventHandler(this.Save_Command))];
          this._saveMI.BeginGroup = true;
          this._saveMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgSave");
          if (!this.AllowEdit || this.Form == null)
            this._saveMI.Visible = false;
          int index = this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_57"), new EventHandler(this.ValidateScheme));
          this._contextMenu.Items[index].BeginGroup = true;
          if (this.Form == null)
            this._contextMenu.Items[index].Visible = false;
          this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_58"), new EventHandler(this.LaunchProcess_Command));
        }
        this._contextMenu.Items[this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Variables_Cmd"), new EventHandler(this.EditVariables))].BeginGroup = true;
        if (this._isProcess)
          this._contextMenu.Items[this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Legend_Command"), new EventHandler(this.Legend_Command))].BeginGroup = true;
        int index1 = this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_60"), new EventHandler(this.Properties_Command));
        this._contextMenu.Items[index1].BeginGroup = true;
        this._contextMenu.Items[index1].ImageIndex = BaseHolder.NamedList.ImageIndex("imgProp");
      }
      if (this._saveMI != null)
        this._saveMI.Enabled = this.Modified;
      if (this._pasteMI != null)
        this._pasteMI.Enabled = this.CanEditPaste();
      return this._contextMenu;
    }
  }

  /// <summary>
  /// Bring up a context menu when the user context clicks in the background.
  /// </summary>
  /// <param name="evt"></param>
  protected override void OnBackgroundContextClicked(MapInputEventArgs evt)
  {
    base.OnBackgroundContextClicked(evt);
    this.ContextMenu.Show(BaseHolder.PopupHost, (Control) this, evt.ViewPoint);
  }

  /// <summary>
  /// Called when the user clicks on the background context menu Paste menu item.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <remarks>
  /// This calls <see cref="M:Intermech.Map.MapView.EditPaste" /> and selects all of the newly pasted objects.
  /// </remarks>
  public void Paste_Command(object sender, EventArgs e)
  {
    PointF docPoint = this.LastInput.DocPoint;
    this.StartTransaction();
    this.Selection.Clear();
    this.EditPaste();
    RectangleF bounds = MapDocument.ComputeBounds((IMapCollection) this.Selection, (MapView) this);
    this.MoveSelection(this.Selection, new SizeF(docPoint.X - bounds.X, docPoint.Y - bounds.Y), true);
    this.FinishTransaction("Context Paste");
  }

  /// <summary>Bring up the properties dialog for this view.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void Properties_Command(object sender, EventArgs e)
  {
    if (Control.ModifierKeys == (Keys.Shift | Keys.Control))
    {
      using (ActivPropForm activPropForm = new ActivPropForm())
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          using (RemoteLock remoteLock = new RemoteLock())
          {
            IDBObject process = this.GetProcess(sessionKeeper.Session);
            remoteLock.Add((object) process);
            activPropForm.GetProperties(process, (WorkflowNode) null);
            activPropForm.ReadOnly = this.ReadOnly;
          }
        }
        if (activPropForm.ShowDialog() != DialogResult.OK)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          using (RemoteLock remoteLock = new RemoteLock())
          {
            activPropForm.ReadOnly = this.ReadOnly;
            IDBObject process = this.GetProcess(sessionKeeper.Session);
            remoteLock.Add((object) process);
            if (!activPropForm.SetProperties(process))
              return;
            this.Modified = true;
            if (!activPropForm.NameModified)
              return;
            this.RefreshTitle();
          }
        }
      }
    }
    else
    {
      using (ActivityProperty activityProperty = new ActivityProperty())
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          using (RemoteLock remoteLock = new RemoteLock())
          {
            IDBObject process = this.GetProcess(sessionKeeper.Session);
            remoteLock.Add((object) process);
            activityProperty.LoadProperty(process, (WorkflowNode) null);
            activityProperty.ReadOnly = this.ReadOnly;
          }
        }
        if (activityProperty.ShowDialog() != DialogResult.OK)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          using (RemoteLock remoteLock = new RemoteLock())
          {
            activityProperty.ReadOnly = this.ReadOnly;
            IDBObject process = this.GetProcess(sessionKeeper.Session);
            remoteLock.Add((object) process);
            if (!activityProperty.SaveProperty(process))
              return;
            this.Modified = true;
            if (!activityProperty.NameModified)
              return;
            this.RefreshTitle();
          }
        }
      }
    }
  }

  public void Legend_Command(object sender, EventArgs e)
  {
    using (LegendForm legendForm = new LegendForm())
    {
      int num = (int) legendForm.ShowDialog();
    }
  }

  public bool RemoveVariableReferences(string name, int varAttrID)
  {
    string str = this.ProcessVariableReferences(varAttrID, false);
    if (!string.IsNullOrEmpty(str))
    {
      if (MessageBox.Show((IWin32Window) null, string.Format(LocalizationHolder.rm.GetString(sc_21824.ssp_workflow_21825()), (object) name, (object) str), LocalizationHolder.rm.GetString("Workflow.Design_62"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
        return false;
      this.ProcessVariableReferences(varAttrID, true);
    }
    return true;
  }

  /// <summary>
  /// Показывает предупреждение, если переменная используется в действиях, при положительном ответе удаляет из этих действий.
  /// Добавляет в список удаленных.
  /// </summary>
  /// <returns></returns>
  public bool DeleteVariable(string name, int varAttrID)
  {
    int num = this.RemoveVariableReferences(name, varAttrID) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (this._deletedVars.Contains(varAttrID))
      return num != 0;
    this._deletedVars.Add(varAttrID);
    return num != 0;
  }

  /// <summary>
  /// Должно быть вызвано каждый раз, когда переменная добавляется в список
  /// </summary>
  /// <param name="typeID"></param>
  /// <returns></returns>
  public void UseVariable(int typeID) => this._deletedVars.Remove(typeID);

  /// <summary>
  /// Finds all var references and performs deletion if parameter supplied
  /// </summary>
  /// <param name="varAttrID"></param>
  /// <param name="doDeletion">If True, the variable reference will be deleted</param>
  /// <returns>Activity names where the variable used in</returns>
  private string ProcessVariableReferences(int varAttrID, bool doDeletion)
  {
    string empty = string.Empty;
    List<WorkflowNode> workflowNodeList = new List<WorkflowNode>();
    foreach (MapObject mapObject in this.Document)
    {
      if (mapObject is WorkflowNode workflowNode && workflowNode.ProcessVariableReferences(varAttrID, doDeletion) && !doDeletion)
      {
        if (empty != "")
          empty += ", ";
        empty += workflowNode.Text;
      }
    }
    return empty;
  }

  public void EditVariables(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (VariablesForm variablesForm = new VariablesForm(this, sessionKeeper.Session))
      {
        variablesForm.ReadOnly = this.ReadOnly;
        if (variablesForm.ShowDialog() != DialogResult.OK || !variablesForm.Modified)
          return;
        this.Modified = true;
      }
    }
  }

  /// <summary>Handle a drop from the tree view.</summary>
  /// <param name="evt"></param>
  protected override IMapCollection DoExternalDrop(DragEventArgs evt)
  {
    object data = evt.Data.GetData(typeof (TreeNode));
    if (data != null && data is TreeNode treeNode)
    {
      PointF doc = this.ConvertViewToDoc(this.PointToClient(new Point(evt.X, evt.Y)));
      this.StartTransaction();
      this.Selection.Clear();
      this.Selection.HotSpot = new SizeF(0.0f, 0.0f);
      MapObject tag = treeNode.Tag as MapObject;
      if (tag != null)
        this.Selection.Add(this.Document.AddCopy(tag, doc));
      this.FinishTransaction("Insert from TreeView");
      return (IMapCollection) this.Selection;
    }
    try
    {
      return base.DoExternalDrop(evt);
    }
    catch (AccessDeniedException ex)
    {
      ExceptionHelper.ExceptionService.ShowException((Exception) ex);
      throw ex;
    }
  }

  public virtual void AlignLeftSides()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Alignment failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float left = primary.SelectionObject.Left;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Left = left;
        }
        this.FinishTransaction("Align Left Sides");
        break;
    }
  }

  public virtual void AlignHorizontalCenters()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Alignment failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float x = primary.SelectionObject.Center.X;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Center = new PointF(x, selectionObject.Center.Y);
        }
        this.FinishTransaction("Align Horizontal Centers");
        break;
    }
  }

  public virtual void AlignRightSides()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Alignment failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float right = primary.SelectionObject.Right;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Right = right;
        }
        this.FinishTransaction("Align Right Sides");
        break;
    }
  }

  public virtual void AlignTops()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Alignment failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float top = primary.SelectionObject.Top;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Top = top;
        }
        this.FinishTransaction("Align Tops");
        break;
    }
  }

  public virtual void AlignVerticalCenters()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Alignment failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float y = primary.SelectionObject.Center.Y;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Center = new PointF(selectionObject.Center.X, y);
        }
        this.FinishTransaction("Align Vertical Centers");
        break;
    }
  }

  public virtual void AlignBottoms()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Alignment failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float bottom = primary.SelectionObject.Bottom;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Bottom = bottom;
        }
        this.FinishTransaction("Align Bottoms");
        break;
    }
  }

  public virtual void MakeWidthsSame()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Sizing failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float width = primary.SelectionObject.Width;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Width = width;
        }
        this.FinishTransaction("Same Widths");
        break;
    }
  }

  public virtual void MakeHeightsSame()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Sizing failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        float height = primary.SelectionObject.Height;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Height = height;
        }
        this.FinishTransaction("Same Heights");
        break;
    }
  }

  public virtual void MakeSizesSame()
  {
    MapObject primary = this.Selection.Primary;
    switch (primary)
    {
      case null:
      case IMapLink _:
        int num = (int) MessageBox.Show("Sizing failure: Primary Selection is empty or a link instead of a node.");
        break;
      default:
        this.StartTransaction();
        SizeF size = primary.SelectionObject.Size;
        foreach (MapObject mapObject in (MapCollection) this.Selection)
        {
          MapObject selectionObject = mapObject.SelectionObject;
          if (!(selectionObject is IMapLink))
            selectionObject.Size = size;
        }
        this.FinishTransaction("Same Sizes");
        break;
    }
  }

  public override void ZoomIn()
  {
    this._myOriginalScale = true;
    this.DocScale = (float) (Math.Round((double) this.DocScale / 0.89999997615814209 * 100.0) / 100.0);
  }

  public override void ZoomOut()
  {
    this._myOriginalScale = true;
    this.DocScale = (float) (Math.Round((double) this.DocScale * 0.89999997615814209 * 100.0) / 100.0);
  }

  public virtual void ZoomNormal()
  {
    this._myOriginalScale = true;
    this.DocScale = 1f;
  }

  public override void ZoomToFit()
  {
    if (this._myOriginalScale)
    {
      this._myOriginalDocPosition = this.DocPosition;
      this._myOriginalDocScale = this.DocScale;
      this.RescaleToFit();
    }
    else
    {
      this.DocPosition = this._myOriginalDocPosition;
      this.DocScale = this._myOriginalDocScale;
    }
    this._myOriginalScale = !this._myOriginalScale;
  }

  public void ShowLongestPath()
  {
    this.StartTransaction();
    MapObject primary = this.Selection.Primary;
    if (primary is IMapNode)
    {
      this.RemoveAllLinkHighlights();
      ArrayList path = new ArrayList();
      Hashtable table = new Hashtable();
      this.ComputeDistances(primary, 0, path, table);
      this.DisplayLongestPaths(table);
    }
    else
    {
      int num = (int) MessageBox.Show("Primary Selection is null or not a node.");
    }
    this.FinishTransaction("show longest path");
  }

  private void ComputeDistances(MapObject obj, int dist, ArrayList path, Hashtable table)
  {
    if (obj == null || path.Contains((object) obj))
      return;
    path.Add((object) obj);
    object obj1 = table[(object) obj];
    if (obj1 == null || (int) obj1 < dist)
    {
      table[(object) obj] = (object) dist;
      if (obj is IMapNode mapNode)
      {
        foreach (IMapGraphPart destination in mapNode.Destinations)
          this.ComputeDistances(destination.MapObject, dist + 1, path, table);
      }
    }
    path.Remove((object) obj);
  }

  private void DisplayLongestPaths(Hashtable table)
  {
    int num1 = 0;
    foreach (MapObject key in this.Document)
    {
      if (table[(object) key] != null)
      {
        int num2 = (int) table[(object) key];
        if (num2 > num1)
          num1 = num2;
      }
    }
    foreach (MapObject key in this.Document)
    {
      if (table[(object) key] != null)
      {
        int dist = (int) table[(object) key];
        if (dist == num1)
          this.ShowPath(key, dist, table);
      }
    }
  }

  private void ShowPath(MapObject obj, int dist, Hashtable table)
  {
    if (dist <= 0 || !(obj is IMapNode n))
      return;
    foreach (IMapLink sourceLink in n.SourceLinks)
    {
      IMapNode otherNode = sourceLink.GetOtherNode(n);
      if (table[(object) otherNode.MapObject] != null && (int) table[(object) otherNode.MapObject] == dist - 1)
      {
        this.SetLinkHighlight(sourceLink.MapObject, true);
        this.ShowPath(otherNode.MapObject, dist - 1, table);
      }
    }
  }

  public void SetLinkHighlight(MapObject obj, bool h)
  {
    switch (obj)
    {
      case MapLink mapLink:
        if (h && mapLink.HighlightPen == null)
          mapLink.HighlightPen = this.LinkHighlightPen;
        mapLink.Highlight = h;
        break;
      case MapLabeledLink mapLabeledLink:
        if (h && mapLabeledLink.HighlightPen == null)
          mapLabeledLink.HighlightPen = this.LinkHighlightPen;
        mapLabeledLink.Highlight = h;
        break;
    }
  }

  public void RemoveAllLinkHighlights()
  {
    this.StartTransaction();
    foreach (MapObject mapObject in this.Document)
      this.SetLinkHighlight(mapObject, false);
    this.FinishTransaction("remove all link highlights");
  }

  public override void DoKeyDown()
  {
    MapInputEventArgs lastInput = this.LastInput;
    switch (lastInput.Key)
    {
      case Keys.Escape:
        this.RemoveAllLinkHighlights();
        break;
      case Keys.Multiply:
        this.ZoomNormal();
        break;
      case Keys.Add:
        this.ZoomIn();
        break;
      case Keys.Subtract:
        this.ZoomOut();
        break;
      default:
        if (lastInput.Shift && lastInput.Control && lastInput.Alt)
        {
          if (lastInput.Key == Keys.S)
          {
            this._isSPressed = true;
            break;
          }
          if (this._isSPressed && lastInput.Key == Keys.Q)
          {
            this.SetStartProcessFromThis();
            break;
          }
          break;
        }
        break;
    }
    base.DoKeyDown();
  }

  private void SetStartProcessFromThis()
  {
    if (!Holder.IsAdmin || !this.ReadOnly)
      return;
    if (WorkflowNodeContextMenu.IsVisibleStartProcessFromThis)
    {
      if (MessageBox.Show("Внимание! Выключить режим запуска процесса с любого места?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      WorkflowNodeContextMenu.IsVisibleStartProcessFromThis = false;
      if (WorkflowNodeContextMenu.StartProcessFromThisMI == null)
        return;
      WorkflowNodeContextMenu.StartProcessFromThisMI.Visible = false;
    }
    else
    {
      if (MessageBox.Show("Внимание! Данный режим работы запуска процесса с любого места эксперементальный. Любые действия делаются на Ваш риск. Вы точно хотите включить данный режим?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      WorkflowNodeContextMenu.IsVisibleStartProcessFromThis = true;
      if (WorkflowNodeContextMenu.StartProcessFromThisMI == null)
        return;
      WorkflowNodeContextMenu.StartProcessFromThisMI.Visible = true;
    }
  }

  public Pen LinkHighlightPen
  {
    get => this._myLinkHighlightPen;
    set
    {
      this._myLinkHighlightPen = value;
      foreach (MapObject mapObject in this.Document)
      {
        if (mapObject is MapLink mapLink)
          mapLink.HighlightPen = value;
        else if (mapObject is MapLabeledLink mapLabeledLink)
          mapLabeledLink.HighlightPen = value;
      }
    }
  }

  public Pen PortHighlightPen
  {
    get => this._myPortHighlightPen;
    set => this._myPortHighlightPen = value;
  }

  public Brush PortHighlightBrush
  {
    get => this._myPortHighlightBrush;
    set => this._myPortHighlightBrush = value;
  }

  public void SaveProcess()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.SaveProcess(this.GetProcess(sessionKeeper.Session));
  }

  public bool JustCreated => this._justCreated;

  public IDBObject SaveProcess(IDBObject proc) => this.SaveProcess(proc, false);

  public IDBObject SaveProcess(IDBObject proc, bool doRename)
  {
    this._justCreated = false;
    using (RemoteLock remoteLock = new RemoteLock())
    {
      remoteLock.Add((object) proc);
      IDBTransactions customService = (IDBTransactions) proc.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        this.Save();
        if (!this._isProcess)
        {
          SchemeStatus schemeStatus = this.ValidateScheme(proc) == string.Empty ? SchemeStatus.Valid : SchemeStatus.Invalid;
          IDBAttribute attributeById = proc.GetAttributeByID(wfConsts.AttrActivityStatusID);
          if (attributeById != null)
            attributeById.AsInteger = (long) schemeStatus;
          else
            proc.Attributes.AddAttribute(wfConsts.AttrActivityStatusID, false, new object[1]
            {
              (object) (long) schemeStatus
            });
          if (this._briefcase != null && schemeStatus == SchemeStatus.Valid)
            proc.GetAttributeByID(wfConsts.AttrBriefcaseID)?.Delete(0L);
        }
        if (doRename)
          this.SetProcessName(proc, this.GetProcessName(proc) + this.ProcessID.ToString(), false);
        if (proc.IsCreationMode)
        {
          proc.CommitCreation(false);
          proc = proc.CheckOut();
          this._processID = proc.ObjectID;
          this._justCreated = true;
        }
        IEnumerator enumerator = (IEnumerator) this.Doc.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current is WorkflowNode current1)
            current1.Save(proc);
        }
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current is WorkflowLink current2)
            current2.Save(proc);
        }
        this.AfterSave(proc);
        customService.Commit();
        this.RemoveVariableAttribute(proc);
      }
      catch
      {
        if (customService.InTransaction)
          customService.Rollback();
        throw;
      }
      this.Modified = false;
    }
    if (this._justCreated)
    {
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", (IList<long>) new long[1]
      {
        this._processID
      });
      BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    }
    return proc;
  }

  public void SaveProcess(
    IDBObject oldproc,
    long newID,
    string name,
    bool checkInOldProcess,
    bool freeOldWorkCopy)
  {
    using (RemoteLock remoteLock = new RemoteLock())
    {
      remoteLock.Add((object) oldproc);
      IDBTransactions customService = (IDBTransactions) oldproc.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        if (name != string.Empty && oldproc.IsCreationMode)
          this.SetProcessName(oldproc, name, false);
        oldproc = this.SaveProcess(oldproc, newID != 0L);
        if (Math.Abs(this._processID) != Math.Abs(newID))
        {
          this._processID = (oldproc as IScheme).SaveAs(newID, name);
          if (!string.IsNullOrEmpty(name))
          {
            this.RefreshTitle(name);
            this.Modified = false;
          }
          newID = this._processID;
          try
          {
            if (this._justCreated)
            {
              if (oldproc.CheckoutBy != 0L)
              {
                this.DeleteProcess(oldproc);
                oldproc.Session.GetObject(Math.Abs(oldproc.ObjectID), false)?.Delete(0L);
              }
              else
                this.DeleteProcess(oldproc);
            }
            else if (checkInOldProcess)
              this.CheckInProcess(oldproc, true);
            else if (freeOldWorkCopy)
              this.CancelProcessChanges(oldproc, true);
          }
          finally
          {
            this._processID = newID;
          }
        }
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  public void LoadProcess()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject process = this.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) process);
        this.LoadProcess(process);
      }
    }
  }

  public void LoadProcess(IDBObject proc)
  {
    this._loading = true;
    try
    {
      this.Doc.SkipsUndoManager = true;
      this._processVersion = proc.VersionID;
      this.Doc.Name = CaptionTransform.GetCaption(this.GetProcessName(proc), (long) this._processVersion);
      this._isProcess = proc.TypeID == wfConsts.ProcessesTypeID;
      this.DeletedObjects.Clear();
      this._deletedVars.Clear();
      this.Doc.Clear();
      this.Doc.TopLeft = new PointF();
      this.myOrigin = new PointF();
      lock (this._activitiesWithScriptsLock)
        this._activitiesWithScripts = (Dictionary<long, List<int>>) null;
      this.Doc.Load(this.ProcessID, this._isProcess);
      this.DocPosition = this.Document.TopLeft;
      this._briefcase = SimpleBriefcase.Load(proc);
    }
    finally
    {
      this._loading = false;
      this.Modified = false;
    }
  }

  public List<Intermech.Expressions.Variable> AllObjectsAttributes
  {
    get
    {
      if (this._allObjectsAttributes == null || this._allObjectsAttributes.Count == 0)
        this._allObjectsAttributes = new List<Intermech.Expressions.Variable>((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.GetAllAttributesVariables());
      return this._allObjectsAttributes;
    }
  }

  private void doc_Loaded(object sender, EventArgs e) => this._isSPressed = false;

  private void AfterEdit(IDBObject proc, bool isDel = false)
  {
    foreach (MapObject mapObject in (MapDocument) this.Doc)
    {
      if (mapObject is WorkflowNode workflowNode)
      {
        workflowNode.AfterEdit();
        if (isDel && workflowNode.IsNew && !this.DeletedObjects.Contains(mapObject))
          this.DeletedObjects.Add(mapObject);
      }
    }
    foreach (MapObject deletedObject in this.DeletedObjects)
    {
      if (deletedObject is WorkflowNode workflowNode)
        workflowNode.AfterEdit();
    }
    for (int index = 0; index < this.DeletedObjects.Count; ++index)
    {
      WorkflowNode deletedObject = this.DeletedObjects[index] as WorkflowNode;
      if (deletedObject != null & isDel && deletedObject.IsNew)
      {
        this.DeleteObject(proc, deletedObject);
        --index;
      }
    }
  }

  private List<long> DeletedObjectIDs
  {
    get
    {
      List<long> deletedObjectIds = new List<long>();
      foreach (MapObject deletedObject in this.DeletedObjects)
      {
        if (deletedObject is WorkflowNode workflowNode)
          deletedObjectIds.Add(workflowNode.ActivityID);
        else if (deletedObject is WorkflowLink workflowLink)
          deletedObjectIds.Add(workflowLink.LinkID);
      }
      return deletedObjectIds;
    }
  }

  private void AfterSave(IDBObject proc)
  {
    foreach (MapObject mapObject in (MapDocument) this.Doc)
    {
      if (mapObject is WorkflowNode workflowNode)
        workflowNode.AfterSave();
    }
    List<long> deletedObjectIds = this.DeletedObjectIDs;
    deletedObjectIds.AddRange((IEnumerable<long>) this.Doc.UnusedLinkIDs);
    (proc as IScheme).DeleteObjects(deletedObjectIds.ToArray());
    foreach (MapObject deletedObject in this.DeletedObjects)
    {
      if (deletedObject is WorkflowNode workflowNode)
        workflowNode.AfterDelete();
    }
    this.Document.UndoManager.Clear();
  }

  /// <summary>
  /// удаляем атрибуты по переменным. Данный метод глушит ошибки, запрещено вызывать внутри транзакции!!!!
  /// </summary>
  /// <param name="proc"></param>
  private void RemoveVariableAttribute(IDBObject proc)
  {
    bool flag = false;
    foreach (int deletedVar in this._deletedVars)
    {
      if (MiscFunx.IsVariableUsed(proc.Session, deletedVar, 0L))
      {
        IDBAttributeType attributeType = proc.Session.GetAttributeType(deletedVar, false);
        if (attributeType != null)
        {
          try
          {
            attributeType.Delete(0L);
            flag = true;
          }
          catch
          {
          }
        }
      }
    }
    if (!flag)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session as IClientSession).ClientCache.ReloadCacheCategory(3, sessionKeeper.Session);
  }

  public void DeleteProcess(bool isNew = false)
  {
    if (isNew)
      this.ProcessID *= -1L;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject process = this.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) process);
        this.DeleteProcess(process);
      }
    }
  }

  private void DeleteProcess(IDBObject proc)
  {
    this.AfterEdit(proc, true);
    proc.Delete(0L);
    this._processID = 0L;
  }

  private void CancelProcessChanges(IDBObject proc, bool doCancel)
  {
    if (this.Modified)
    {
      IEnumerator enumerator = (IEnumerator) this.Doc.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is WorkflowNode current)
        {
          current.BeforeCancelChanges();
          if (current.JustCreated)
            this.DeleteObject(proc, current);
        }
      }
    }
    if (!doCancel || proc.CheckoutBy == 0L)
      return;
    proc.CancelChanges();
    this._processID = Math.Abs(proc.ObjectID);
    DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) new long[1]
    {
      -this._processID
    });
    BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
  }

  public void CancelProcessChanges(bool doCancel)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject process = this.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) process);
        this.CancelProcessChanges(process, doCancel);
      }
    }
  }

  public void CancelLocalScriptDelete()
  {
    IEnumerator enumerator = (IEnumerator) this.Doc.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if (enumerator.Current is WorkflowNode current)
      {
        if (current.FirstLocalScript.Count > 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(wfConsts.ScriptRelationTypeID);
            foreach (LocalScriptInfo localScriptInfo in current.FirstLocalScript)
            {
              if (sessionKeeper.Session.GetRelation(current.ActivityID, localScriptInfo.ScriptID, true) == null)
              {
                IDBRelation dbRelation = relationCollection.Create(current.ActivityID, localScriptInfo.ScriptID);
                IDBAttribute attributeById1 = dbRelation.GetAttributeByID(wfConsts.AttrScriptKindID);
                if (attributeById1 != null)
                  attributeById1.AsInteger = (long) localScriptInfo.ScriptKind;
                IDBAttribute attributeById2 = dbRelation.GetAttributeByID(wfConsts.AttrScriptExecSideID);
                if (attributeById2 != null)
                  attributeById2.AsInteger = (long) localScriptInfo.ExecSide;
              }
            }
          }
        }
        if (current.LocalScriptsToDeleted != null && current.LocalScriptsToDeleted.Count > 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
            {
              DeletingObjects deletingObjects = new DeletingObjects();
              foreach (long num in current.LocalScriptsToDeleted)
              {
                long script = num;
                if (!current.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == script)))
                {
                  IDBObject dbObject = sessionKeeper.Session.GetObject(script, false);
                  if (dbObject != null)
                    deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
                }
              }
              customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
            }
          }
          current.LocalScriptsToDeleted = new List<long>();
        }
        if (current.NewScripts != null && current.NewScripts.Count > 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
            {
              DeletingObjects deletingObjects = new DeletingObjects();
              foreach (KeyValuePair<int, long> newScript in current.NewScripts)
              {
                KeyValuePair<int, long> script = newScript;
                if (!current.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == script.Value)))
                {
                  IDBObject dbObject = sessionKeeper.Session.GetObject(script.Value, false);
                  if (dbObject != null)
                    deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
                }
              }
              customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
            }
          }
          current.NewScripts = new Dictionary<int, long>();
        }
        current.FirstLocalScript = new List<LocalScriptInfo>();
      }
    }
  }

  public void CheckInProcess(bool doCheckin)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject process = this.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) process);
        this.CheckInProcess(process, doCheckin);
      }
    }
  }

  private void CheckInProcess(IDBObject proc, bool doCheckin)
  {
    if (doCheckin)
    {
      if (proc.CheckoutBy != 0L)
      {
        try
        {
          proc.CheckIn();
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) new long[1]
          {
            -proc.ObjectID
          });
          BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        }
        catch (Exception ex)
        {
          wfFunx.SayError($"{LocalizationHolder.rm.GetString("ErrOnCheckIn")}\r\n{ex.Message}");
        }
      }
    }
    this.AfterEdit(proc);
  }

  private void DeleteObject(IDBObject proc, WorkflowNode node)
  {
    this.DeletedObjects.Remove((MapObject) node);
    if (proc is IScheme scheme)
      scheme.DeleteObject(node.ActivityID);
    node.AfterDelete();
  }

  public bool IsEditorClosed => this._isEditorClosed;

  public void EditorClosed()
  {
    this._isEditorClosed = true;
    foreach (MapObject mapObject in this.Document)
    {
      if (mapObject is WorkflowNode workflowNode)
        workflowNode.EditorClosed();
    }
  }

  public bool IsCreationMode
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return this.GetProcess(sessionKeeper.Session).IsCreationMode;
    }
  }

  protected void SetModified(bool value)
  {
    if (this._form == null || !this._form.IsEditMode || this._modified == value)
      return;
    this._modified = value;
    this.UpdateTitle();
    this.UpdateCommands();
  }

  public void UpdateModifiedState() => this.SetModified(this.Modified);

  public void UpdateCommands() => this._form?.UpdateCommands();

  public bool Modified
  {
    get
    {
      if (this._form == null || !this._form.IsEditMode)
        return false;
      return this.Doc.IsModified || this._modified;
    }
    set
    {
      this.Doc.IsModified = value;
      this.SetModified(value);
    }
  }

  public void Save() => this.Doc.Save();

  public bool IsLinked(WorkflowNode a, WorkflowNode b)
  {
    foreach (IMapLink link in a.Links)
    {
      if ((object.Equals((object) link.FromNode, (object) b) || object.Equals((object) link.ToNode, (object) b)) && link is WorkflowLink workflowLink && workflowLink.LinkKind == this.CurrentLinkKind)
        return true;
    }
    return false;
  }

  public bool ReadOnly => !this.AllowEdit;

  public void SnapToGrid()
  {
    foreach (MapObject mapObject in this.Document)
    {
      if (mapObject is WorkflowNode)
      {
        PointF location = mapObject.Location;
        PointF nearestGridPoint = this.FindNearestGridPoint(location);
        mapObject.DoMove((MapView) this, location, nearestGridPoint);
      }
    }
  }

  public bool IsProcess => this._isProcess;

  internal Dictionary<long, List<int>> ActivitiesWithScripts
  {
    get
    {
      lock (this._activitiesWithScriptsLock)
      {
        if (this._activitiesWithScripts == null)
          this.UpdateActivitiesWithScripts();
        return this._activitiesWithScripts;
      }
    }
  }

  internal void UpdateActivitiesWithScripts()
  {
    lock (this._activitiesWithScriptsLock)
    {
      if (this._activitiesWithScripts == null)
        this._activitiesWithScripts = new Dictionary<long, List<int>>();
      else
        this._activitiesWithScripts.Clear();
      List<long> objectIDs = new List<long>();
      foreach (MapObject mapObject in this.Document)
      {
        if (mapObject is WorkflowNode workflowNode)
          objectIDs.Add(workflowNode.ActivityID);
      }
      if (objectIDs.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (DataRow row in (InternalDataCollectionBase) MiscFunx.GetScriptIDs(sessionKeeper.Session, objectIDs).Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!this._activitiesWithScripts.ContainsKey(int64))
          {
            this._activitiesWithScripts.Add(int64, new List<int>()
            {
              Convert.ToInt32(row[2])
            });
          }
          else
          {
            int int32 = Convert.ToInt32(row[2]);
            if (!this._activitiesWithScripts[int64].Contains(int32))
              this._activitiesWithScripts[int64].Add(int32);
          }
        }
      }
    }
  }

  public override IMapTool Tool
  {
    get => base.Tool;
    set
    {
      if (value is MapToolContext mapToolContext)
        mapToolContext.SingleSelection = false;
      base.Tool = value;
    }
  }

  public override System.Type NewLinkClass
  {
    get
    {
      return this.CurrentLinkKind == LinkKind.ParallelBlock ? typeof (WorkflowParallelBlock) : typeof (WorkflowLink);
    }
    set => base.NewLinkClass = value;
  }

  public bool FixErrors()
  {
    if (this.Doc.ErroneusActivities.Count > 0)
    {
      int num1 = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin ? 1 : 0;
      string str = string.Format(LocalizationHolder.rm.GetString("ErrExecActsFound"), (object) this.Doc.ErroneusActivities.Count);
      if (num1 != 0)
      {
        if (MessageBox.Show($"{str} {LocalizationHolder.rm.GetString("ErrExecActsPrompt")}", (string) null, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (sessionKeeper.Session.GetObject(this.ProcessID, false) is IScheme scheme)
              scheme.DeleteObjects(this.Doc.ErroneusActivities.ToArray());
          }
          return true;
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show($"{str} {LocalizationHolder.rm.GetString("ErrExecActsAdminNeeded")}", (string) null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    return false;
  }

  public SimpleBriefcase Briefcase => this._briefcase;
}
