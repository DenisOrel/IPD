// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Expert;
using Intermech.FormDesigner;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.BM2;
using Intermech.Kernel.Search;
using Intermech.Map;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for WorkflowNode.</summary>
[Serializable]
public class WorkflowNode : MapIconicNode
{
  private int _activityType;
  private string _activityGuid = Guid.Empty.ToString();
  private long _activityID;
  private long _processid;
  public bool IsParallelBlockFinish;
  private bool _deleted;
  private bool _copied;
  [NonSerialized]
  private MapImageEx qimg;
  [NonSerialized]
  private GraphView _view;
  private List<long> _clones;
  [NonSerialized]
  private MapRectangle _back;
  private string _condition;
  private bool _formWasCreated;
  [NonSerialized]
  private ConditionList _expertConditions;
  [NonSerialized]
  private long _parentActivityID;
  [NonSerialized]
  private ActivityStatus _status;
  [NonSerialized]
  private Dictionary<string, MapImageEx> _infoImages;
  protected const int _collDefW = 9;
  protected const int _collDefH = 9;
  protected const int _charDefW = 8;
  protected const int _charDefH = 9;
  private bool _updatingInfoImages;
  internal bool IsNew;
  private List<ExpressionInfo> _expressionConditions;
  private ActivityStatus _inspectingStatus;
  public List<long> LocalScriptsToDeleted;
  public Dictionary<int, long> NewScripts;
  private long _formID = -2;
  private long _formToDelete;
  [NonSerialized]
  private DockControl _formEditor;
  private bool _inLayoutChildren;
  private bool _inAlignSpots;
  [NonSerialized]
  private List<BackPort> _backPorts = new List<BackPort>();
  public List<LocalScriptInfo> FirstLocalScript = new List<LocalScriptInfo>();
  [NonSerialized]
  private LongList _resetTimerLinks;
  private bool _justCreated;

  public long ProcessID => this._processid;

  public WorkflowNode()
  {
    this._doMapIconicLayoutChildren = false;
    this._back = this.CreateBackground();
    this.Add((MapObject) this._back);
  }

  public WorkflowNode(long processid, int atype)
    : this()
  {
    this._processid = processid;
    this._activityType = atype;
  }

  public WorkflowNode(long processid, Intermech.Workflow.ActivityInfo ai)
    : this(processid, ai.Type)
  {
    this.Initialize(ClientActivityInfos.ImageList, ai.ImageIndex, ai.ObjectName, ActivityStatus.OnApproach);
    this.ToolTipText = ai.TypeName;
  }

  public WorkflowNode(ActivityNode node)
    : this()
  {
    this._activityID = node.ObjectID;
    this._activityGuid = node.ObjectGuid;
    this._activityType = node.ObjectType;
    string name = node.Name;
    ActivityStatus complexStatus = this.GetComplexStatus(node);
    this._processid = node.ProcessID;
    this._parentActivityID = node.ParentActivityID;
    this.IsParallelBlockFinish = node.IsParallelBlockFinish;
    this.Initialize(ClientActivityInfos.ImageList, this.TypeImageIndex, name, complexStatus);
  }

  public int ActivityType => this._activityType;

  public Intermech.Workflow.ActivityInfo TypeInfo => ActivityInfos.FindByID(this._activityType);

  public ActivityKind ActivityKind
  {
    get
    {
      Intermech.Workflow.ActivityInfo typeInfo = this.TypeInfo;
      return typeInfo != null ? typeInfo.Kind : ActivityKind.None;
    }
  }

  public int TypeImageIndex
  {
    get
    {
      int typeImageIndex = -1;
      Intermech.Workflow.ActivityInfo typeInfo = this.TypeInfo;
      if (typeInfo != null)
        typeImageIndex = typeInfo.ImageIndex;
      return typeImageIndex;
    }
  }

  public long ActivityID => this._activityID;

  public List<long> Clones
  {
    get => this._clones;
    set
    {
      this._clones = value;
      this.UpdateClonesCountText();
    }
  }

  public bool UseExpertSystem
  {
    get
    {
      if (this._activityType != wfConsts.CaseTypeID && this._activityType != wfConsts.CondTypeID)
        return false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return new ExtProperties(sessionKeeper.Session.GetObject(this._activityID), wfConsts.AttrAddInfoID).Ini.ReadBoolean("Props", "useExpertSystem", false);
    }
  }

  private void UpdateClonesCountText()
  {
    if (this._clones == null)
      return;
    int count = this._clones.Count;
    if (count <= 0)
      return;
    this.Text = $"{this.Text} ({count + 1})";
  }

  private void PostInit(IDBObject act) => this._copied = false;

  public void UpdateInfoImages(bool validate = true)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateInfoImages(this.GetActivity(sessionKeeper.Session), validate);
  }

  private MapImageEx drawTextImg(string text, string tooltip)
  {
    TextMapImage textMapImage = new TextMapImage(text);
    if (text == "C")
      textMapImage.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Pixel);
    textMapImage.Width = 8f;
    textMapImage.Height = 9f;
    textMapImage.ToolTip = tooltip;
    return (MapImageEx) textMapImage;
  }

  private void _updateInfoImages(MapImageEx emi, MapImageEx mi, string key)
  {
    this._infoImages[key] = mi;
    if (mi == emi)
      return;
    if (emi == null)
    {
      mi.Visible = false;
      this.Add((MapObject) mi);
    }
    else
      this.Remove((MapObject) emi);
  }

  /// <summary>
  /// -1 нет скрипта либо это тип действия сценарий 0 это локальный тип скриптов, 1 общий тип скриптов, 2 имеется два скрипта разных типов
  /// </summary>
  protected int HasScripts
  {
    get
    {
      if (this._activityType == wfConsts.ScriptTypeID || this.View == null || !this.View.ActivitiesWithScripts.ContainsKey(this.ActivityID))
        return -1;
      List<int> activitiesWithScript = this.View.ActivitiesWithScripts[this.ActivityID];
      if (activitiesWithScript.Count > 1)
        return 2;
      if (activitiesWithScript.Count == 0)
        return -1;
      return activitiesWithScript[0] == wfConsts.WorkflowCommonScript ? 1 : 0;
    }
  }

  protected bool HasLC
  {
    get
    {
      if (this._activityType == wfConsts.LifeCycleTypeID)
        return false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = this.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrLCConfigAttrID);
        return attributeById != null && !attributeById.IsNull && attributeById.AsString != "";
      }
    }
  }

  protected bool HasMessages
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = this.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrNotificationsID);
        return attributeById != null && !attributeById.IsNull && attributeById.AsString != "";
      }
    }
  }

  internal void AlignInfoImages()
  {
    if (this._infoImages == null)
      return;
    int num = -1;
    foreach (KeyValuePair<string, MapImageEx> infoImage in this._infoImages)
    {
      MapImageEx mapImageEx = infoImage.Value;
      if (mapImageEx != null)
      {
        mapImageEx.Selectable = false;
        mapImageEx.Resizable = false;
        mapImageEx.Top = this.Image.Bottom - mapImageEx.Height;
        mapImageEx.Left = this.Image.Left + (float) num;
        num += Convert.ToInt32(mapImageEx.Width);
        mapImageEx.Visible = true;
      }
    }
  }

  internal bool UpdatingInfoImages => this._updatingInfoImages;

  protected void UpdateInfoImages(IDBObject act, bool validate = true)
  {
    if (act == null)
      return;
    this._updatingInfoImages = true;
    try
    {
      this._inLayoutChildren = true;
      try
      {
        string str = string.Empty;
        if (validate)
          str = !(act is ISubProcess subProcess) || this.View == null || !this.View.ReadOnly || !this.View.IsProcess ? (act as IActivity).Validate() : subProcess.Validate(false);
        if (string.IsNullOrEmpty(str))
        {
          if (this.qimg != null)
          {
            this.Remove((MapObject) this.qimg);
            this.qimg = (MapImageEx) null;
          }
        }
        else
        {
          if (this.qimg == null)
          {
            this.qimg = new MapImageEx();
            this.qimg.Image = Holder.QuestionImage;
            this.qimg.Selectable = false;
            this.qimg.Resizable = false;
            this.qimg.Width = 16f;
            this.qimg.Height = 16f;
            this.qimg.Left = (float) ((double) this.Image.Left + (double) this.Image.Width - (double) this.qimg.Width / 2.0);
            this.qimg.Top = this.Top + this.Height - this.Label.Height - this.qimg.Height;
            this.Add((MapObject) this.qimg);
          }
          this.qimg.ToolTip = LocalizationHolder.rm.GetString("Workflow.Design_124") + str;
        }
        bool flag = false;
        IDBAttribute attributeById = act.GetAttributeByID(wfConsts.AttrCollectorID);
        if (attributeById != null && this.ActivityKind != ActivityKind.Stop)
          flag = attributeById.AsBoolean;
        if (this._infoImages == null)
          this._infoImages = new Dictionary<string, MapImageEx>();
        MapImageEx emi = (MapImageEx) null;
        MapImageEx mi1 = (MapImageEx) null;
        this._infoImages.TryGetValue("coll", out emi);
        if (flag)
        {
          if (emi == null)
          {
            mi1 = new MapImageEx();
            mi1.Width = 9f;
            mi1.Height = 9f;
            mi1.ToolTip = LocalizationHolder.rm.GetString("Workflow.Design_125");
            Bitmap bitmap = new Bitmap(9, 9);
            using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap))
            {
              using (Pen pen = new Pen(Brushes.Black))
              {
                Point[] points = new Point[5]
                {
                  new Point(0, bitmap.Height / 2),
                  new Point(bitmap.Width / 2, 0),
                  new Point(bitmap.Width - 1, bitmap.Height / 2),
                  new Point(bitmap.Width / 2, bitmap.Height - 1),
                  new Point(0, bitmap.Height / 2)
                };
                graphics.FillPolygon(Brushes.White, points);
                graphics.DrawPolygon(pen, points);
                graphics.DrawLine(pen, bitmap.Width / 2, 0, bitmap.Width / 2, 9);
                graphics.DrawLine(pen, 0, bitmap.Height / 2, 9, bitmap.Height / 2);
              }
            }
            mi1.Image = (System.Drawing.Image) bitmap;
          }
          else
            mi1 = emi;
        }
        this._updateInfoImages(emi, mi1, "coll");
        MapImageEx mi2 = (MapImageEx) null;
        this._infoImages.TryGetValue("form", out emi);
        if (this.FormID > 0L)
          mi2 = emi ?? this.drawTextImg("F", LocalizationHolder.rm.GetString("HasForm"));
        this._updateInfoImages(emi, mi2, "form");
        MapImageEx mi3 = (MapImageEx) null;
        this._infoImages.TryGetValue("lc", out emi);
        if (this.HasLC)
          mi3 = emi ?? this.drawTextImg("L", LocalizationHolder.rm.GetString("HasLC"));
        this._updateInfoImages(emi, mi3, "lc");
        MapImageEx mi4 = (MapImageEx) null;
        this._infoImages.TryGetValue("m", out emi);
        if (this.HasMessages)
          mi4 = emi ?? this.drawTextImg("M", LocalizationHolder.rm.GetString("HasMessages"));
        this._updateInfoImages(emi, mi4, "m");
        MapImageEx mi5 = (MapImageEx) null;
        if (this.HasScripts == 0)
        {
          this._infoImages.TryGetValue("localscript", out emi);
          MapImageEx mi6 = emi ?? this.drawTextImg("S", LocalizationHolder.rm.GetString("HasScripts"));
          this._updateInfoImages(emi, mi6, "localscript");
          MapImageEx mi7 = (MapImageEx) null;
          this._infoImages.TryGetValue("commonscript", out emi);
          this._updateInfoImages(emi, mi7, "commonscript");
        }
        else if (this.HasScripts == 1)
        {
          this._infoImages.TryGetValue("commonscript", out emi);
          MapImageEx mi8 = emi ?? this.drawTextImg("C", LocalizationHolder.rm.GetString("HasScripts"));
          this._updateInfoImages(emi, mi8, "commonscript");
          MapImageEx mi9 = (MapImageEx) null;
          this._infoImages.TryGetValue("localscript", out emi);
          this._updateInfoImages(emi, mi9, "localscript");
        }
        else if (this.HasScripts == 2)
        {
          this._infoImages.TryGetValue("localscript", out emi);
          MapImageEx mi10 = emi ?? this.drawTextImg("S", LocalizationHolder.rm.GetString("HasScripts"));
          this._updateInfoImages(emi, mi10, "localscript");
          this._infoImages.TryGetValue("commonscript", out emi);
          MapImageEx mi11 = emi ?? this.drawTextImg("C", LocalizationHolder.rm.GetString("HasScripts"));
          this._updateInfoImages(emi, mi11, "commonscript");
        }
        else
        {
          this._infoImages.TryGetValue("localscript", out emi);
          this._updateInfoImages(emi, mi5, "localscript");
          this._infoImages.TryGetValue("commonscript", out emi);
          this._updateInfoImages(emi, mi5, "commonscript");
        }
      }
      finally
      {
        this._inLayoutChildren = false;
        this.LayoutChildren((MapObject) null);
      }
    }
    finally
    {
      this._updatingInfoImages = false;
    }
  }

  public void SetInvalidIcon(string errorText)
  {
    if (this.qimg == null)
    {
      MapImageEx mapImageEx = new MapImageEx();
      mapImageEx.Image = Holder.QuestionImage;
      mapImageEx.Selectable = false;
      mapImageEx.Resizable = false;
      mapImageEx.Width = 16f;
      mapImageEx.Height = 16f;
      this.qimg = mapImageEx;
      this.qimg.Left = (float) ((double) this.Image.Left + (double) this.Image.Width - (double) this.qimg.Width / 2.0);
      this.qimg.Top = this.Top + this.Height - this.Label.Height - this.qimg.Height;
      this.Add((MapObject) this.qimg);
    }
    if (string.IsNullOrEmpty(this.qimg.ToolTip))
      this.qimg.ToolTip = LocalizationHolder.rm.GetString("Workflow.Design_124") + errorText;
    else
      this.qimg.ToolTip = $"{this.qimg.ToolTip} \n{errorText}";
  }

  public IDBObject GetActivity(IUserSession session) => this.GetActivity(this._activityID, session);

  private IDBObject GetActivity(long activityID, IUserSession session)
  {
    if (this._processid == -1L)
      throw new AbortException();
    bool flag = this._activityID != activityID;
    IDBObject act;
    if (activityID != 0L)
    {
      act = session.GetObject(activityID);
      if (flag)
        return act;
    }
    else
      act = session.GetObjectCollection(this._activityType).Create();
    if (act != null)
    {
      this._activityID = act.ObjectID;
      this.PostInit(act);
    }
    return act;
  }

  public void InitClone()
  {
    long activityId = this._activityID;
    string str = LocalizationHolder.rm.GetString("Workflow.Design_127") + this.Text;
    this.Text = str;
    if (this.Document is GraphDoc)
    {
      str = (this.Document as GraphDoc).GenerateNodeName(this);
      this.Text = str;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(this._activityGuid), false);
      if (dbObject == null && this._activityGuid != Guid.Empty.ToString())
        throw new KernelException("Вставляемый объект не найден в текущей базе данных. Перемещение действий процессов недопустимо между разными базами данных.");
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this._activityType);
      if (dbObject == null && this._activityGuid == Guid.Empty.ToString())
        dbObject = sessionKeeper.Session.GetObject(this._activityID, false);
      IDBObject prototype = dbObject;
      IDBObject act = objectCollection.Create(prototype);
      this.PostInit(act);
      IDBAttribute attributeById = act.GetAttributeByID(wfConsts.AttrNameID);
      if (attributeById != null)
        attributeById.AsString = str;
      if (this._activityType == wfConsts.CaseTypeID)
      {
        act.GetAttributeByID(wfConsts.AttrConditionID)?.Clear();
        act.GetAttributeByID(wfConsts.AttrConditionFormulaID)?.Clear();
      }
      this._activityID = act.ObjectID;
      if (this.FormID > 0L)
        this.CreateForm(this.FormID);
      if (this.View != null)
        this.View.UpdateActivitiesWithScripts();
      else if ((this.Document is GraphDoc document ? document.View : (GraphView) null) != null)
      {
        (this.Document as GraphDoc).View.UpdateActivitiesWithScripts();
        this.View = (this.Document as GraphDoc).View;
      }
      this.UpdateInfoImages(act);
    }
    this.LayoutChildren((MapObject) null);
  }

  public void ReplaceLocalScripts()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(wfConsts.ScriptRelationTypeID);
      relationCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-21, RelationalOperators.In, (object) new long[1]
        {
          this.ActivityID
        }, LogicalOperators.AND, 0, false)
      }, new object[5]
      {
        (object) ObligatoryObjectAttributes.F_PROJ_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
        (object) wfConsts.AttrScriptKindID
      }, 0L, (object) null, -1);
      DataTable dataTable = relationCollection.Select(paramSet);
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.WorkflowLocalScript);
      this.FirstLocalScript = new List<LocalScriptInfo>();
      this.LocalScriptsToDeleted = new List<long>();
      this.NewScripts = new Dictionary<int, long>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row.ItemArray[2]) == wfConsts.WorkflowLocalScript)
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(Convert.ToInt64(row.ItemArray[3]));
          IDBObject prototype = sessionKeeper.Session.GetObject(Convert.ToInt64(row.ItemArray[1]));
          IDBObject dbObject = objectCollection.Create(prototype);
          string str = string.Empty;
          int int32 = Convert.ToInt32(row.ItemArray[4]);
          if (this.ActivityType != wfConsts.ScriptTypeID)
            str = int32 == 0 ? "[Перед] " : "[После] ";
          dbObject.Caption = string.Format("{2}{0}. {1}", (object) this.Document.Name, (object) this.Text, (object) str);
          dbObject.CommitCreation(true, false);
          if (this.NewScripts.ContainsKey(int32))
            this.NewScripts[int32] = dbObject.ObjectID;
          else
            this.NewScripts.Add(int32, dbObject.ObjectID);
          long objectId = dbObject.ObjectID;
          relation.ReplacePartObject(objectId);
        }
      }
    }
  }

  private void SetProcessID(IDBObject act, long pid)
  {
    IDBAttribute attributeById = act.GetAttributeByID(wfConsts.AttrProcessID);
    if (attributeById == null)
      throw new Exception("wf: Node.AssignParent - attribute \"Process\" does not exist!");
    attributeById.AsInteger = pid;
  }

  public void InitNew(long pid)
  {
    if (this.ActivityID != 0L)
      return;
    this._processid = pid;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject activity = this.GetActivity(sessionKeeper.Session);
      if (this._activityType == wfConsts.StartTypeID || this._activityType == wfConsts.TaskTypeID || this._activityType == wfConsts.ApproveTypeID)
      {
        IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrAddIDID);
        if (attributeById != null)
        {
          ActivityFlags asInteger = (ActivityFlags) attributeById.AsInteger;
          if (!asInteger.HasFlag((Enum) ActivityFlags.DenyDeletionFromMail))
          {
            ActivityFlags activityFlags = asInteger | ActivityFlags.DenyDeletionFromMail;
            activity.Attributes.AddAttribute(wfConsts.AttrAddIDID, false, new object[1]
            {
              (object) (int) activityFlags
            });
          }
        }
        else
        {
          ActivityFlags activityFlags = (ActivityFlags) (0 | 1);
          activity.Attributes.AddAttribute(wfConsts.AttrAddIDID, false, new object[1]
          {
            (object) (int) activityFlags
          });
        }
      }
      IDBAttribute attributeById1 = activity.GetAttributeByID(wfConsts.AttrNameID);
      string str1 = this.Text;
      if (this.Document is GraphDoc document)
      {
        str1 = document.GenerateNodeName(this);
        this.Text = str1;
      }
      string str2 = str1;
      attributeById1.AsString = str2;
      if (activity.IsCreationMode)
      {
        activity.CommitCreation(false, true);
        this._activityID = activity.ObjectID;
        this.SetProcessID(activity, this.ProcessID);
        this.IsNew = true;
      }
      this.UpdateInfoImages(activity);
    }
  }

  public string GetCondition(LinkKind lk, long LinkID)
  {
    if (this._activityType == wfConsts.CondTypeID)
    {
      if (this._condition == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttributeById1 = sessionKeeper.Session.GetObjectAttributeByID(this._activityID, wfConsts.AttrConditionID);
          if (objectAttributeById1 != null)
          {
            TempFormula tempFormula = MiscFunx.FormulaFromAttribute(objectAttributeById1);
            if (tempFormula == null)
            {
              IDBAttribute objectAttributeById2 = sessionKeeper.Session.GetObjectAttributeByID(this._activityID, wfConsts.AttrConditionFormulaID);
              if (objectAttributeById2 != null)
                this._condition = MiscFunx.GetExpressionFromAttr(objectAttributeById2).ToString();
            }
            else
              this._condition = tempFormula.ToString();
          }
        }
      }
      if (string.IsNullOrEmpty(this._condition))
        return "<?>";
      if (lk == LinkKind.False)
        return LocalizationHolder.rm.GetString("Workflow.Design_128") + this._condition;
    }
    else if (this._activityType == wfConsts.CaseTypeID && (lk == LinkKind.True || lk == LinkKind.False))
    {
      string condition = string.Empty;
      if (this.ExpertConditions == null || this.ExpertConditions.IsEmpty)
      {
        int index = this.ExpressionConditions.FindIndex((Predicate<ExpressionInfo>) (x => x.LinkID == Math.Abs(LinkID)));
        if (index != -1)
          condition = this.ExpressionConditions[index].FormulaForLink;
      }
      else
      {
        ConditionInfo conditionInfo = this.ExpertConditions.Find(LinkID);
        if (conditionInfo != null)
          condition = conditionInfo.ToString();
      }
      if (string.IsNullOrEmpty(condition))
        condition = "<?>";
      return condition;
    }
    return this._condition == null ? string.Empty : this._condition;
  }

  public ConditionList ExpertConditions
  {
    get
    {
      if (this._activityType == wfConsts.CaseTypeID && this._expertConditions == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._activityID, wfConsts.AttrConditionID);
          if (objectAttributeById != null)
            this._expertConditions = new ConditionList(objectAttributeById);
        }
      }
      return this._expertConditions;
    }
  }

  public List<ExpressionInfo> ExpressionConditions
  {
    get
    {
      if (this._activityType == wfConsts.CaseTypeID && this._expressionConditions == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._activityID, wfConsts.AttrConditionFormulaID);
          if (objectAttributeById != null)
            this._expressionConditions = new List<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(objectAttributeById));
        }
      }
      return this._expressionConditions;
    }
  }

  public void SaveConditions()
  {
    if (!this.UseExpertSystem)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(this._activityID, (object) wfConsts.AttrConditionFormulaID, false, false);
        if (this._expressionConditions == null)
          this._expressionConditions = new List<ExpressionInfo>(0);
        if (objectAttribute == null)
          return;
        MiscFunx.ExpressionsToAttribute(this._expressionConditions, objectAttribute);
      }
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(this._activityID, (object) wfConsts.AttrConditionID, false, false);
        if (objectAttribute == null)
          return;
        if (this._expertConditions == null)
          this._expertConditions = new ConditionList(objectAttribute);
        this._expertConditions.Save(objectAttribute);
      }
    }
  }

  internal void Deleted()
  {
    if (this._deleted)
      return;
    this.DeleteForm();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IObjectsDeleteService customService = sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) as IObjectsDeleteService;
      DeletingObjects deletingObjects = new DeletingObjects();
      IUserSession session = sessionKeeper.Session;
      foreach (DataRow row in (InternalDataCollectionBase) MiscFunx.GetScriptIDs(session, new List<long>()
      {
        this.ActivityID
      }).Rows)
      {
        long scriptID = Convert.ToInt64(row.ItemArray[1]);
        if (Convert.ToInt64(row.ItemArray[2]) == (long) wfConsts.WorkflowLocalScript)
        {
          if (!this.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == scriptID)))
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(scriptID, false);
            if (dbObject != null)
              deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
          }
          else
          {
            if (this.LocalScriptsToDeleted == null)
              this.LocalScriptsToDeleted = new List<long>();
            if (!this.LocalScriptsToDeleted.Contains(scriptID))
              this.LocalScriptsToDeleted.Add(scriptID);
          }
          if (this.NewScripts != null && this.NewScripts.ContainsValue(scriptID))
            this.NewScripts.Remove(this.NewScripts.FirstOrDefault<KeyValuePair<int, long>>((System.Func<KeyValuePair<int, long>, bool>) (x => x.Value == scriptID)).Key);
        }
      }
      if (customService != null)
      {
        if (deletingObjects.Count > 0)
          customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
      }
    }
    this._deleted = true;
  }

  internal void AfterDelete()
  {
    if (this.LocalScriptsToDeleted == null || this.LocalScriptsToDeleted.Count <= 0 || !this._deleted)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
      {
        DeletingObjects deletingObjects = new DeletingObjects();
        foreach (long objectID in this.LocalScriptsToDeleted)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
          if (dbObject != null)
            deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
        }
        customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
      }
    }
    this.LocalScriptsToDeleted = new List<long>();
  }

  public void Save(IDBObject process)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject activity = this.GetActivity(sessionKeeper.Session);
      if (activity.IsCreationMode)
      {
        activity.CommitCreation(false);
        IDBObject act = activity.CheckOut();
        this._activityID = act.ObjectID;
        this.SetProcessID(act, process.ObjectID);
      }
      if (!this.IsNew)
        return;
      this.IsNew = false;
    }
  }

  public void AfterSave()
  {
    this.FirstLocalScript = new List<LocalScriptInfo>();
    this.SetFirstLocalScripts();
    if (this.LocalScriptsToDeleted != null && this.LocalScriptsToDeleted.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
        {
          DeletingObjects deletingObjects = new DeletingObjects();
          foreach (long objectID in this.LocalScriptsToDeleted)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
            deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
          }
          customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
        }
      }
      this.LocalScriptsToDeleted = new List<long>();
    }
    this.NewScripts = new Dictionary<int, long>();
    if (this._resetTimerLinks == null || !this._resetTimerLinks.Modified)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject activity = this.GetActivity(sessionKeeper.Session);
      object[] initValues = new object[this._resetTimerLinks.Count];
      for (int index = 0; index < this._resetTimerLinks.Count; ++index)
        initValues[index] = (object) this._resetTimerLinks[index];
      if (initValues.Length != 0)
        activity.Attributes.AddAttribute(wfConsts.AttrObjectListID, false, initValues);
      else
        activity.GetAttributeByID(wfConsts.AttrObjectListID)?.Clear();
      this._resetTimerLinks.Modified = false;
    }
  }

  public void EditorClosed()
  {
    if (this._formEditor == null)
      return;
    this._formEditor.Close();
    this._formEditor = (DockControl) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.FormID);
      if (dbObject.CheckoutBy != sessionKeeper.Session.UserID)
        return;
      dbObject.CheckIn();
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) new long[1]
      {
        dbObject.ObjectID
      });
      BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    }
  }

  private void DeleteForm()
  {
    long formId = this.FormID;
    if (formId == 0L)
      return;
    if (this._formEditor != null)
    {
      this._formEditor.Close();
      this._formEditor = (DockControl) null;
    }
    this.FormID = (long) sc_21938.ssp_workflow_21939(708692627);
    if (this._formWasCreated)
    {
      this.DeleteForm(formId);
      this._formWasCreated = false;
    }
    else
      this._formToDelete = formId;
    this._view.Modified = true;
  }

  private void DeleteForm(long id)
  {
    if (this._formEditor != null)
    {
      this._formEditor.Close();
      this._formEditor = (DockControl) null;
    }
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(id);
        if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
          dbObject.CancelChanges();
        dbObject.Delete((long) sc_21938.ssp_workflow_21940(1590988975));
      }
    }
    catch (Exception ex)
    {
      this._formToDelete = 0L;
      throw ex;
    }
  }

  private void GrayImage()
  {
    System.Drawing.Image image = this.Image.Image;
    Bitmap bitmap = new Bitmap(image.Width, image.Height);
    Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap);
    float[][] newColorMatrix1 = new float[6][]
    {
      new float[5]{ 0.3f, 0.3f, 0.3f, 0.0f, 0.0f },
      new float[5]{ 0.59f, 0.59f, 0.59f, 0.0f, 0.0f },
      new float[5]{ 0.11f, 0.11f, 0.11f, 0.0f, 0.0f },
      null,
      null,
      null
    };
    float[] numArray1 = new float[6];
    numArray1[3] = 1f;
    newColorMatrix1[3] = numArray1;
    float[] numArray2 = new float[6];
    numArray2[4] = 1f;
    newColorMatrix1[4] = numArray2;
    float[] numArray3 = new float[6];
    numArray3[5] = 1f;
    newColorMatrix1[5] = numArray3;
    ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
    ImageAttributes imageAttr = new ImageAttributes();
    imageAttr.SetColorMatrix(newColorMatrix2);
    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
    graphics.Dispose();
    this.Image.Image = (System.Drawing.Image) bitmap;
  }

  public ActivityStatus GetComplexStatus(ActivityNode node)
  {
    if (node.Statuses.Count <= 0)
      return ActivityStatus.OnApproach;
    ActivityStatus[] activityStatusArray = new ActivityStatus[4]
    {
      ActivityStatus.Executed,
      ActivityStatus.CollectorWaiting,
      ActivityStatus.DefineWaiting,
      ActivityStatus.ParticipantWaiting
    };
    foreach (ActivityStatus complexStatus in activityStatusArray)
    {
      if (node.Statuses.IndexOf(complexStatus) != -1)
        return complexStatus;
    }
    return node.Statuses.Last<ActivityStatus>();
  }

  public override void Initialize(ImageList imglist, int imgindex, string name)
  {
    this.Initialize(imglist, imgindex, name, ActivityStatus.OnApproach);
  }

  public void Initialize(ImageList imglist, int imgindex, string name, ActivityStatus status)
  {
    base.Initialize(imglist, imgindex, name);
    this.ToolTipText = name;
    this._status = status;
    switch (status)
    {
      case ActivityStatus.CollectorWaiting:
        this._back.Pen = new Pen(Color.Yellow, 2f);
        break;
      case ActivityStatus.ParticipantWaiting:
        this._back.Pen = new Pen(Color.Blue, 2f);
        break;
      case ActivityStatus.Executed:
      case ActivityStatus.ScriptExecuted:
      case ActivityStatus.LCStepExecuted:
        this._back.Pen = new Pen(Color.Green, 2f);
        break;
      case ActivityStatus.Terminated:
        this.GrayImage();
        this._back.Pen = new Pen(Color.Maroon, 2f);
        break;
      case ActivityStatus.Completed:
      case ActivityStatus.Recalled:
        this.GrayImage();
        break;
    }
  }

  public GraphView View
  {
    get => this._view;
    set => this._view = value;
  }

  public void DrawRelationship(LinkKind kind)
  {
    GraphView view = this.View;
    if (view == null)
      return;
    view.Refresh();
    view.CurrentLinkKind = kind;
    WFLinkingNewTool wfLinkingNewTool = new WFLinkingNewTool((MapView) view, this);
    view.Tool = (IMapTool) wfLinkingNewTool;
  }

  public void Cut_Command(object sender, EventArgs e) => this.View.EditCut();

  public void Copy_Command(object sender, EventArgs e) => this.View.EditCopy();

  public override bool OnDoubleClick(MapInputEventArgs evt, MapView view)
  {
    this.Properties_Command((object) null, (EventArgs) null);
    return true;
  }

  public override bool OnSingleClick(MapInputEventArgs evt, MapView view)
  {
    base.OnSingleClick(evt, view);
    if (this._activityType == wfConsts.SubProcessTypeID && (Control.ModifierKeys & Keys.Control) != Keys.None)
      this.DoOpenSubprocess();
    return true;
  }

  private long ChooseInspectingActivity()
  {
    long num = this.ActivityID;
    this._inspectingStatus = this._status;
    if (this.Clones != null && this.Clones.Count > 0)
    {
      DataTable dataTable = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        dataTable = sessionKeeper.Session.GetObjectCollection(this.ActivityType).Select(new DBRecordSetParams(new ConditionStructure[3]
        {
          new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) this.ProcessID, LogicalOperators.AND, 0, false),
          new ConditionStructure(-2, RelationalOperators.Equal, (object) this.ActivityID, LogicalOperators.OR, 1, false),
          new ConditionStructure(wfConsts.AttrParentActivityID, RelationalOperators.Equal, (object) this._parentActivityID, (object) null, LogicalOperators.NONE, -1, false, AttributeSourceTypes.Auto, ColumnContents.ID)
        }, new object[5]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) wfConsts.AttrRecipID,
          (object) wfConsts.AttrActivityStatusID,
          (object) wfConsts.AttrStartedID,
          (object) wfConsts.AttrCompletedID
        }, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }, new SortOrders[1]{ SortOrders.ASC }));
      if (dataTable != null)
      {
        using (ChooseActivityForm chooseActivityForm = new ChooseActivityForm())
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject activity = this.GetActivity(sessionKeeper.Session);
            chooseActivityForm.CaptionLabel.Text = string.Format(chooseActivityForm.CaptionLabel.Text, (object) activity.Caption, (object) (this.Clones.Count + 1));
          }
          chooseActivityForm.DataSource = dataTable;
          if (chooseActivityForm.ShowDialog() != DialogResult.OK)
            return -1;
          num = chooseActivityForm.CurrentID;
          DataRow[] dataRowArray = dataTable.Select($"[{dataTable.Columns[0].ColumnName}] = {num.ToString()}");
          if (dataRowArray.Length != 0)
            this._inspectingStatus = !DBNull.Value.Equals(dataRowArray[0][2]) ? (ActivityStatus) Convert.ToInt32(dataRowArray[0][2]) : ActivityStatus.OnApproach;
        }
      }
    }
    return num;
  }

  private void UpdateVisibleProperties(IDBObject act)
  {
    this.View?.UpdateActivitiesWithScripts();
    this.UpdateInfoImages(act);
    IDBAttribute attributeById = act.GetAttributeByID(wfConsts.AttrNameID);
    if (attributeById != null)
      this.Text = attributeById.AsString;
    this.UpdateClonesCountText();
    this._condition = (string) null;
    this._expertConditions = (ConditionList) null;
    this._expressionConditions = (List<ExpressionInfo>) null;
    this.UpdateLinks();
    this.CheckParallelBlockLink();
  }

  public void Properties_Command(object sender, EventArgs e)
  {
    if (Control.ModifierKeys == (Keys.Shift | Keys.Control))
    {
      using (ActivPropForm activPropForm = new ActivPropForm())
      {
        if (this.LocalScriptsToDeleted != null && this.LocalScriptsToDeleted.Count > 0)
          activPropForm.LocalScriptsToDeleted = this.LocalScriptsToDeleted;
        if (this.NewScripts != null && this.NewScripts.Count > 0)
          activPropForm.NewScripts = this.NewScripts;
        long activityID = this.ChooseInspectingActivity();
        switch (activityID)
        {
          case -1:
            break;
          case 0:
            break;
          default:
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject activity = this.GetActivity(activityID, sessionKeeper.Session);
              activPropForm.GetProperties(activity, this);
            }
            activPropForm.ReadOnly = this._view.ReadOnly;
            if (activPropForm.ShowDialog() == DialogResult.OK)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBObject activity = this.GetActivity(activityID, sessionKeeper.Session);
                try
                {
                  if (!activPropForm.SetProperties(activity))
                    break;
                  this._view.Modified = true;
                  if (activPropForm.CaseLinksWithModifiedLinkType != null && activPropForm.CaseLinksWithModifiedLinkType.Count > 0)
                  {
                    this._expertConditions = (ConditionList) null;
                    foreach (KeyValuePair<long, LinkKind> keyValuePair in activPropForm.CaseLinksWithModifiedLinkType)
                    {
                      WorkflowLink link = this.FindLink(keyValuePair.Key);
                      if (link != null)
                        link.DBLinkKind = keyValuePair.Value;
                    }
                  }
                  this.UpdateVisibleProperties(activity);
                  break;
                }
                finally
                {
                  this.LocalScriptsToDeleted = activPropForm.LocalScriptsToDeleted;
                  this.NewScripts = activPropForm.NewScripts;
                }
              }
            }
            else
            {
              if (activPropForm.AddedNewScriptToDelete)
              {
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                {
                  if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
                  {
                    DeletingObjects deletingObjects = new DeletingObjects();
                    foreach (long num in activPropForm.LocalScriptsToDeleted)
                    {
                      long script = num;
                      if (!this.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == script)) && (this.LocalScriptsToDeleted == null || !this.LocalScriptsToDeleted.Contains(script)))
                      {
                        IDBObject dbObject = sessionKeeper.Session.GetObject(script);
                        deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
                      }
                    }
                    if (deletingObjects.Count > 0)
                      customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
                  }
                }
                activPropForm.LocalScriptsToDeleted = this.LocalScriptsToDeleted;
              }
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
                {
                  DeletingObjects deletingObjects = new DeletingObjects();
                  foreach (KeyValuePair<int, long> newScript in activPropForm.NewScripts)
                  {
                    KeyValuePair<int, long> script = newScript;
                    if (!this.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == script.Value)) && (this.NewScripts == null || !this.NewScripts.ContainsValue(script.Value)))
                    {
                      IDBObject dbObject = sessionKeeper.Session.GetObject(script.Value);
                      deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
                    }
                  }
                  if (deletingObjects.Count > 0)
                    customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
                }
                activPropForm.NewScripts = this.NewScripts;
                break;
              }
            }
        }
      }
    }
    else
    {
      using (ActivityProperty activityProperty = new ActivityProperty())
      {
        if (this.LocalScriptsToDeleted != null && this.LocalScriptsToDeleted.Count > 0)
          activityProperty.LocalScriptsToDeleted = this.LocalScriptsToDeleted;
        if (this.NewScripts != null && this.NewScripts.Count > 0)
          activityProperty.NewScripts = this.NewScripts;
        long activityID = this.ChooseInspectingActivity();
        switch (activityID)
        {
          case -1:
            break;
          case 0:
            break;
          default:
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject activity = this.GetActivity(activityID, sessionKeeper.Session);
              activityProperty.LoadProperty(activity, this);
            }
            activityProperty.ReadOnly = this._view.ReadOnly;
            if (activityProperty.ShowDialog() == DialogResult.OK)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBObject activity = this.GetActivity(activityID, sessionKeeper.Session);
                try
                {
                  if (!activityProperty.SaveProperty(activity))
                    break;
                  this._view.Modified = true;
                  if (activityProperty.CaseLinksWithModifiedLinkType != null && activityProperty.CaseLinksWithModifiedLinkType.Count > 0)
                  {
                    this._expertConditions = (ConditionList) null;
                    this._expressionConditions = (List<ExpressionInfo>) null;
                    foreach (KeyValuePair<long, LinkKind> keyValuePair in activityProperty.CaseLinksWithModifiedLinkType)
                    {
                      WorkflowLink link = this.FindLink(keyValuePair.Key);
                      if (link != null)
                        link.DBLinkKind = keyValuePair.Value;
                    }
                  }
                  this.UpdateVisibleProperties(activity);
                  break;
                }
                finally
                {
                  this.LocalScriptsToDeleted = activityProperty.LocalScriptsToDeleted;
                  this.NewScripts = activityProperty.NewScripts;
                }
              }
            }
            else
            {
              if (activityProperty.AddedNewScriptToDelete)
              {
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                {
                  if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
                  {
                    DeletingObjects deletingObjects = new DeletingObjects();
                    foreach (long num in activityProperty.LocalScriptsToDeleted)
                    {
                      long script = num;
                      if (!this.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == script)) && (this.LocalScriptsToDeleted == null || !this.LocalScriptsToDeleted.Contains(script)))
                      {
                        IDBObject dbObject = sessionKeeper.Session.GetObject(script);
                        deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
                      }
                    }
                    if (deletingObjects.Count > 0)
                      customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
                  }
                }
                activityProperty.LocalScriptsToDeleted = this.LocalScriptsToDeleted;
              }
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
                {
                  DeletingObjects deletingObjects = new DeletingObjects();
                  foreach (KeyValuePair<int, long> newScript in activityProperty.NewScripts)
                  {
                    KeyValuePair<int, long> script = newScript;
                    if (!this.FirstLocalScript.Any<LocalScriptInfo>((System.Func<LocalScriptInfo, bool>) (x => x.ScriptID == script.Value)) && (this.NewScripts == null || !this.NewScripts.ContainsValue(script.Value)))
                    {
                      IDBObject dbObject = sessionKeeper.Session.GetObject(script.Value);
                      deletingObjects.Add(0L, dbObject.ID, dbObject.ObjectID, true);
                    }
                  }
                  if (deletingObjects.Count > 0)
                    customService.Delete(sessionKeeper.Session.SessionGUID, deletingObjects, DeleteObjectsJobMode.IgnoreErrors);
                }
                activityProperty.NewScripts = this.NewScripts;
                break;
              }
            }
        }
      }
    }
  }

  private WorkflowLink FindLink(long id)
  {
    WorkflowLink link1 = (WorkflowLink) null;
    foreach (IMapLink link2 in this.Links)
    {
      if (link2 is WorkflowLink)
      {
        WorkflowLink link3 = (WorkflowLink) link2;
        if (Math.Abs(link3.LinkID) == id)
          return link3;
        link1 = (WorkflowLink) null;
      }
    }
    return link1;
  }

  public long FormID
  {
    get
    {
      if (this._formToDelete != 0L)
        return 0;
      if (this._formID == -2L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = this.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrFormID);
          this._formID = attributeById == null ? -1L : attributeById.AsInteger;
        }
      }
      return this._formID;
    }
    set
    {
      if (this._formID == value)
        return;
      this._formID = value;
      if (!this._deleted)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject activity = this.GetActivity(sessionKeeper.Session);
          IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrFormID);
          if (attributeById != null)
          {
            if (attributeById.AsInteger != value)
            {
              if (value == 0L)
                attributeById.Clear();
              else
                attributeById.AsInteger = value;
              this.UpdateInfoImages(activity);
            }
          }
        }
      }
      if (this._view == null)
        return;
      this._view.Modified = true;
    }
  }

  public void DoViewForm()
  {
    if (this.FormID == 0L)
      return;
    long formid = this.FormID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.FormID);
      if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
        formid = -dbObject.ObjectID;
    }
    long objid = this.ChooseInspectingActivity();
    switch (objid)
    {
      case -1:
      case 0:
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          objid = MiscFunx.GetObjectIDWithVars(this.GetActivity(sessionKeeper.Session), this.View.ProcessID);
          break;
        }
      default:
        if (!this.View.IsProcess || this._inspectingStatus == ActivityStatus.OnApproach || this._inspectingStatus == ActivityStatus.Terminated)
          goto case -1;
        break;
    }
    if (this._inspectingStatus == ActivityStatus.Executed)
      FormDlg.ViewForm(objid, formid, true);
    else
      FormDlg.ViewForm(objid, formid);
  }

  public void DoDelForm()
  {
    if (MessageFuncs.Ask(LocalizationHolder.rm.GetString("Workflow.Design_149")) != DialogResult.Yes)
      return;
    this.DeleteForm();
  }

  private IDBObject CreateForm(long proto = 0)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.FormsTypeID);
      IDBObject form = proto == 0L ? objectCollection.Create() : objectCollection.Create(proto);
      IDBAttribute attributeById = form.GetAttributeByID(sessionKeeper.Session.IdentHelper.NameID);
      if (attributeById != null)
      {
        if (this.View != null)
          attributeById.AsString = $"{this.View.Doc.Name}. {this.Label.Text}";
        else if ((this.Document as GraphDoc).View != null)
        {
          this.View = (this.Document as GraphDoc).View;
          attributeById.AsString = $"{this.View.Doc.Name}. {this.Label.Text}";
        }
        else
          attributeById.AsString = "Форма. " + this.Label.Text;
      }
      form.CommitCreation(false);
      this.FormID = form.ObjectID;
      this._formWasCreated = true;
      return form;
    }
  }

  public void DoEditForm()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this.FormID == 0L && this._formToDelete != 0L)
        this._formID = this._formToDelete;
      if (this._formID == 0L)
        dbObject = this.CreateForm();
      if (dbObject == null)
      {
        int activitiesLinkedToForm = wfFunx.FindActivitiesLinkedToForm(this.FormID, this.ActivityID);
        if (activitiesLinkedToForm > 0 && MessageFuncs.Confirm(string.Format(LocalizationHolder.rm.GetString("Workflow.ConfirmFormEdit"), (object) activitiesLinkedToForm)) == DialogResult.Cancel)
          return;
        dbObject = sessionKeeper.Session.GetObject(this.FormID);
      }
      long objectId = dbObject.CheckOut().ObjectID;
      if (this._formEditor != null)
      {
        this._formEditor.Activate();
      }
      else
      {
        ServiceContainer viewServices = new ServiceContainer();
        viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
        Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId), (System.IServiceProvider) viewServices);
        if (!(ApplicationServices.Container.GetService(typeof (IFormDesignerEditorService)) is IFormDesignerEditorService service))
          return;
        Control editorControl = service.GetEditorControl(objectId);
        if (editorControl is FormDesignerControl)
        {
          (editorControl as IFormDesignerEditorHookable).Hook = (IFormDesignerEditorHook) new FormEditorHook(this.View.ProcessID);
          ((FormDesignerControl) editorControl).AddToolBoxItems(this.GetToolBoxItems());
          ((FormDesignerControl) editorControl).Rollback();
        }
        if (editorControl != null && editorControl.Parent is FormDesignerView parent)
          parent.AllowLinkingObjects = false;
        this._formEditor = wfFunx.FindParentDock(editorControl);
        if (this._formEditor == null)
          return;
        this._formEditor.Closed += new EventHandler(this._formEditor_Closed);
      }
    }
  }

  private void _formEditor_Closed(object sender, EventArgs e)
  {
    this._formEditor = (DockControl) null;
    if (this.FormID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.FormID);
      if (dbObject.CheckoutBy != sessionKeeper.Session.UserID)
        return;
      dbObject.CheckIn();
      DBObjectsEventArgs e1 = new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) new long[1]
      {
        dbObject.ObjectID
      });
      BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e1);
    }
  }

  private List<IMToolBoxItem> GetToolBoxItems()
  {
    List<IMToolBoxItem> toolBoxItems = new List<IMToolBoxItem>();
    string str = LocalizationHolder.rm.GetString("Workflow.Design_ComponentsCategory");
    Assembly assembly = this.GetType().Assembly;
    IMToolBoxItem imToolBoxItem1 = new IMToolBoxItem(LocalizationHolder.rm.GetString(sc_21938.ssp_workflow_21941()), typeof (UsersTreeView), typeof (UsersTreeViewWrapper), new Bitmap(assembly.GetManifestResourceStream(assembly.GetName().Name + ".img.formeditor.userstreeview.bmp")));
    imToolBoxItem1.ItemCategory = str;
    imToolBoxItem1.Description = LocalizationHolder.rm.GetString("Workflow.Design_15");
    imToolBoxItem1.ItemCategory = str;
    toolBoxItems.Add(imToolBoxItem1);
    string name1 = LocalizationHolder.rm.GetString("Workflow.Design_153");
    Bitmap bitmap1 = new Bitmap(assembly.GetManifestResourceStream(assembly.GetName().Name + ".img.formeditor.userscombobox.bmp"));
    System.Type toolType1 = typeof (UsersComboBox);
    System.Type wrapperType1 = typeof (UsersComboBoxWrapper);
    Bitmap image1 = bitmap1;
    IMToolBoxItem imToolBoxItem2 = new IMToolBoxItem(name1, toolType1, wrapperType1, image1);
    imToolBoxItem2.Description = LocalizationHolder.rm.GetString("Workflow.Design_156");
    imToolBoxItem2.ItemCategory = str;
    toolBoxItems.Add(imToolBoxItem2);
    string name2 = LocalizationHolder.rm.GetString("Workflow.Design_154");
    Bitmap bitmap2 = new Bitmap(assembly.GetManifestResourceStream(assembly.GetName().Name + ".img.formeditor.radiogroup.bmp"));
    System.Type toolType2 = typeof (EnhRadioGroup);
    System.Type wrapperType2 = typeof (EnhRadioGroupWrapper);
    Bitmap image2 = bitmap2;
    IMToolBoxItem imToolBoxItem3 = new IMToolBoxItem(name2, toolType2, wrapperType2, image2);
    imToolBoxItem3.Description = LocalizationHolder.rm.GetString("Workflow.Design_157");
    imToolBoxItem3.ItemCategory = str;
    toolBoxItems.Add(imToolBoxItem3);
    return toolBoxItems;
  }

  public void DoShowVars()
  {
    long activityID = this.ChooseInspectingActivity();
    if (activityID == -1L || !wfFunx.ShowVariables(this._processid, activityID, this._inspectingStatus == ActivityStatus.Executed))
      return;
    this._view.Modified = true;
  }

  private void DoOpenSubprocess()
  {
    long activityID = this.ChooseInspectingActivity();
    if (activityID == -1L)
      return;
    long id = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject activity = this.GetActivity(activityID, sessionKeeper.Session);
      IDBAttribute dbAttribute = activity.GetAttributeByID(wfConsts.AttrSubprocessID) ?? activity.GetAttributeByID(wfConsts.AttrSubprocessSchemeID);
      if (dbAttribute != null)
        id = dbAttribute.AsInteger;
    }
    if (id == 0L)
      return;
    wfFunx.OpenProcess(id, !this._view.ReadOnly);
  }

  /// <summary>Bring up a WorkflowNode specific context menu.</summary>
  /// <param name="evt"></param>
  /// <param name="view"></param>
  /// <returns></returns>
  public override bool OnContextClick(MapInputEventArgs evt, MapView view)
  {
    if (!(view is GraphView))
      return base.OnContextClick(evt, view);
    WorkflowNodeContextMenu.InitMenu(this);
    MenuButtonItem menuButtonItem = WorkflowNodeContextMenu.Menu.Show(BaseHolder.PopupHost, (Control) view, evt.ViewPoint);
    if (menuButtonItem == WorkflowNodeContextMenu.PropsMI)
      this.Properties_Command((object) null, (EventArgs) null);
    else if (menuButtonItem == WorkflowNodeContextMenu.EditFormMI)
      this.DoEditForm();
    else if (menuButtonItem == WorkflowNodeContextMenu.AddLinkMI)
      this.DrawRelationship(LinkKind.Forward);
    else if (menuButtonItem == WorkflowNodeContextMenu.AddBLinkMI)
      this.DrawRelationship(LinkKind.Backward);
    else if (menuButtonItem == WorkflowNodeContextMenu.AddPBlockMI)
      this.DrawRelationship(LinkKind.ParallelBlock);
    else if (menuButtonItem == WorkflowNodeContextMenu.CopyMI)
      this.View.EditCopy();
    else if (menuButtonItem == WorkflowNodeContextMenu.CutMI)
      this.View.EditCut();
    else if (menuButtonItem == WorkflowNodeContextMenu.ViewFormMI)
      this.DoViewForm();
    else if (menuButtonItem == WorkflowNodeContextMenu.DelFormMI)
      this.DoDelForm();
    else if (menuButtonItem == WorkflowNodeContextMenu.VarsMI)
      this.DoShowVars();
    else if (menuButtonItem == WorkflowNodeContextMenu.DelMI)
      this.View.EditDelete();
    else if (menuButtonItem == WorkflowNodeContextMenu.SubProcMI)
      this.DoOpenSubprocess();
    else if (menuButtonItem == WorkflowNodeContextMenu.StartProcessFromThisMI)
      this.StartProcessFromThis();
    return true;
  }

  public bool IsPredecessor => this.Port != null;

  protected virtual MapRectangle CreateBackground()
  {
    MapRectangle background = new MapRectangle();
    background.Selectable = false;
    background.Resizable = false;
    background.Reshapable = false;
    background.Shadowed = true;
    background.Brush = Brushes.White;
    background.Pen = Pens.Gray;
    return background;
  }

  protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
  {
    base.CopyChildren(newgroup, env);
    if (!(newgroup is WorkflowNode workflowNode))
      return;
    for (int index = newgroup.Count - 1; index >= 0; --index)
    {
      if (newgroup[index] is TextMapImage)
        newgroup.RemoveAt(index);
      else if (newgroup[index] is MapRectangle)
        workflowNode._back = (MapRectangle) newgroup[index];
    }
    if (workflowNode.Image == null)
      return;
    workflowNode.Image.ImageList = ClientActivityInfos.ImageList;
  }

  public override void LayoutChildren(MapObject childchanged)
  {
    if (this._inAlignSpots || this._inLayoutChildren)
      return;
    this._inLayoutChildren = true;
    try
    {
      base.LayoutChildren(childchanged);
      if (this.Initializing)
        return;
      MapObject icon = this.Icon;
      if (icon != null)
      {
        this._back.Width = icon.Width + 6f;
        this._back.Height = icon.Height + 6f;
        icon.Position = new PointF(this._back.Left + 3f, this._back.Top + 4f);
        MapText label = this.Label;
        if (label != null)
        {
          if (this.DraggableLabel && childchanged == label)
          {
            this.myLabelOffset = new SizeF(label.Left - this._back.Left, label.Top - this._back.Top);
            return;
          }
          if ((double) this.myLabelOffset.Width > -99999.0)
            label.Position = new PointF(this._back.Left + this.myLabelOffset.Width, this._back.Top + this.myLabelOffset.Height);
          else
            label.SetSpotLocation(32 /*0x20*/, (MapObject) this._back, 128 /*0x80*/);
        }
        if (this.Port != null)
          this.Port.SetSpotLocation(1, (MapObject) this._back, 1);
      }
      this.AlignInfoImages();
    }
    finally
    {
      this._inLayoutChildren = false;
    }
  }

  private int BackPortsSorter(BackPort x, BackPort y) => Convert.ToInt32(x.weight - y.weight);

  private bool EmptyBackPortsRemover(BackPort port)
  {
    if (port.LinksCount != 0)
      return false;
    this.Remove((MapObject) port);
    this._backPorts.Remove(port);
    return true;
  }

  public void AlignSpots() => this.AlignSpots(true);

  public void AlignSpots(bool deleteEmptyPorts)
  {
    if (this._inAlignSpots)
      return;
    this._inAlignSpots = true;
    try
    {
      foreach (BackPort backPort in this._backPorts)
        backPort._updating = true;
      try
      {
        List<BackPort> backPortList1 = new List<BackPort>();
        Dictionary<int, List<BackPort>> dictionary = new Dictionary<int, List<BackPort>>();
        foreach (BackPort backPort in this._backPorts)
        {
          int fromSpot = backPort.FromSpot;
          List<BackPort> backPortList2;
          if (dictionary.ContainsKey(fromSpot))
          {
            backPortList2 = dictionary[fromSpot];
          }
          else
          {
            backPortList2 = new List<BackPort>();
            dictionary.Add(fromSpot, backPortList2);
          }
          backPortList2.Add(backPort);
        }
        if (deleteEmptyPorts)
        {
          foreach (KeyValuePair<int, List<BackPort>> keyValuePair in dictionary)
            keyValuePair.Value.RemoveAll(new Predicate<BackPort>(this.EmptyBackPortsRemover));
        }
        foreach (KeyValuePair<int, List<BackPort>> keyValuePair in dictionary)
          keyValuePair.Value.Sort(new Comparison<BackPort>(this.BackPortsSorter));
        foreach (KeyValuePair<int, List<BackPort>> keyValuePair in dictionary)
        {
          PointF spotLocation = this.GetSpotLocation(keyValuePair.Key);
          float num = (float) ((keyValuePair.Value.Count - 1) * 10) / 2f;
          if (keyValuePair.Key == 32 /*0x20*/ || keyValuePair.Key == 128 /*0x80*/)
            spotLocation.X -= num;
          else
            spotLocation.Y -= num;
          foreach (MapObject mapObject in keyValuePair.Value)
          {
            mapObject.SetSpotLocation(keyValuePair.Key, spotLocation);
            if (keyValuePair.Key == 32 /*0x20*/ || keyValuePair.Key == 128 /*0x80*/)
              spotLocation.X += 10f;
            else
              spotLocation.Y += 10f;
          }
        }
      }
      finally
      {
        foreach (BackPort backPort in this._backPorts)
          backPort._updating = false;
      }
    }
    finally
    {
      this._inAlignSpots = false;
    }
  }

  public override MapObject CopyObject(MapCopyDictionary env)
  {
    MapObject mapObject = base.CopyObject(env);
    if (!(mapObject is WorkflowNode workflowNode))
      return mapObject;
    workflowNode._backPorts = new List<BackPort>();
    workflowNode._infoImages = (Dictionary<string, MapImageEx>) null;
    workflowNode._copied = true;
    return mapObject;
  }

  public bool Copied => this._copied;

  protected virtual BackPort CreatePort(int spot)
  {
    BackPort port = new BackPort();
    port.Location = new PointF(this.Left + 5f, this.Top + 5f);
    port.Style = MapPortStyle.None;
    port.Size = new SizeF(4f, 4f);
    port.FromSpot = spot;
    port.ToSpot = spot;
    return port;
  }

  public int BackPortsCount => this._backPorts.Count;

  public BackPort BackwardPort
  {
    get
    {
      foreach (BackPort backPort in this._backPorts)
      {
        if (backPort.LinksCount == 0)
          return backPort;
      }
      this.SuspendsUpdates = true;
      try
      {
        BackPort port = this.CreatePort(32 /*0x20*/);
        port.SuspendsUpdates = true;
        try
        {
          this._backPorts.Add(port);
          this.Add((MapObject) port);
          this.AlignSpots(false);
          return port;
        }
        finally
        {
          port.SuspendsUpdates = false;
        }
      }
      finally
      {
        this.SuspendsUpdates = false;
      }
    }
  }

  public void UpdateSpot(BackPort port, WorkflowLink link)
  {
    if (port == null)
      return;
    port.UpdateSpot(link);
    this.AlignSpots();
  }

  internal void UpdateSpots(bool goLinkedNodes)
  {
    foreach (BackPort backPort in this._backPorts)
    {
      foreach (IMapLink link in backPort.Links)
      {
        if (link is WorkflowLink l)
        {
          backPort.UpdateSpot(l);
          if (goLinkedNodes)
            ((l.FromNode == this ? l.ToNode : l.FromNode) as WorkflowNode).UpdateSpots(false);
        }
      }
    }
    this.AlignSpots();
  }

  internal void SetFirstLocalScripts()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(wfConsts.ScriptRelationTypeID);
      relationCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[5]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) wfConsts.AttrScriptKindID,
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
        (object) wfConsts.AttrScriptExecSideID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
      });
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, this.ActivityID).Rows)
      {
        int int32 = Convert.ToInt32(row[4]);
        if (wfConsts.WorkflowLocalScript == int32)
        {
          LocalScriptInfo localScriptInfo = new LocalScriptInfo();
          if (!row[1].Equals((object) DBNull.Value))
            localScriptInfo.ScriptKind = (ScriptKind) Convert.ToInt32(row[1]);
          localScriptInfo.ScriptID = Convert.ToInt64(row[0]);
          if (!row[3].Equals((object) DBNull.Value))
            localScriptInfo.ExecSide = (ScriptExecSide) Convert.ToInt64(row[3]);
          localScriptInfo.ScriptType = WorkflowScriptType.Local;
          this.FirstLocalScript.Add(localScriptInfo);
        }
      }
    }
  }

  protected override void OnBoundsChanged(RectangleF old)
  {
    base.OnBoundsChanged(old);
    if (this._processid == -1L)
      return;
    this.UpdateSpots(true);
  }

  public void LinksChanged(WorkflowLink deletedLink = null)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (WorkflowLink link in this.Links)
    {
      if (link != null && link != deletedLink)
      {
        if (link.Backward && link.FromNode == this)
          flag1 = true;
        else if (link.LinkKind == LinkKind.ParallelBlock && link.ToNode == this)
          flag2 = true;
      }
    }
    int num = -1;
    if (flag2)
      num = 4;
    else if (flag1)
      num = 2;
    else if (deletedLink != null && (deletedLink.Backward || deletedLink.LinkKind == LinkKind.ParallelBlock))
      num = 0;
    if (deletedLink != null && deletedLink.LinkKind == LinkKind.ParallelBlock)
      this.IsParallelBlockFinish = false;
    if (deletedLink != null && deletedLink.Backward)
      this.CheckParallelBlockLink(deletedLink);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = this.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrRollbackKindID);
      if (attributeById == null || attributeById.AsInteger == (long) num || num == -1)
        return;
      attributeById.AsInteger = (long) num;
    }
  }

  public void LinkDeleted(WorkflowLink link, bool isOutgoing)
  {
    this.LinksChanged(link);
    if (!isOutgoing)
      return;
    if (this.ActivityType == wfConsts.CaseTypeID)
    {
      if (this.ExpertConditions != null && !this.ExpertConditions.IsEmpty)
      {
        int index = this.ExpertConditions.IndexOf(link.LinkID);
        if (index != -1)
          this.ExpertConditions.RemoveAt(index);
      }
      if (this.ExpressionConditions != null && this.ExpressionConditions.Count > 0)
      {
        int index = this.ExpressionConditions.FindIndex((Predicate<ExpressionInfo>) (x => x.LinkID == Math.Abs(link.LinkID)));
        if (index != -1)
          this.ExpressionConditions.RemoveAt(index);
      }
      this.SaveConditions();
    }
    this.UpdateInfoImages();
    LongList longList = (LongList) null;
    if (link.ToNode != null && link.ToNode is WorkflowNode)
      longList = (link.ToNode as WorkflowNode)._resetTimerLinks;
    if (longList == null)
      return;
    int index1 = longList.IndexOf(Math.Abs(link.LinkID));
    if (index1 == -1)
      return;
    longList.RemoveAt(index1);
  }

  public void AfterEdit()
  {
    if (this._formToDelete <= 0L)
      return;
    this.DeleteForm(this._formToDelete);
    this._formToDelete = 0L;
  }

  public void BeforeCancelChanges()
  {
    if (!this._formWasCreated)
      return;
    try
    {
      long formId = this.FormID;
      this.FormID = this._formToDelete;
      this._formToDelete = 0L;
      this.DeleteForm(formId);
    }
    catch
    {
    }
  }

  public override void Changed(
    int subhint,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect)
  {
    base.Changed(subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
  }

  private void CheckParallelBlockLink(WorkflowLink deletedLink = null)
  {
    if (!this.IsParallelBlockFinish)
      return;
    this.UpdateInfoImages();
    foreach (WorkflowLink link in this.Links)
    {
      if (link != null && (deletedLink == null || link != deletedLink) && link.LinkKind == LinkKind.Backward)
      {
        this.SetInvalidIcon($"Конец блока параллельного выполнения на действии \"{this.Text}\" не может содержать обратные ссылки");
        break;
      }
    }
  }

  public void UpdateLinks()
  {
    foreach (IMapLink link in this.Links)
    {
      if (link is WorkflowLink)
        (link as WorkflowLink).UpdateCaption();
    }
  }

  public LongList ResetTimerLinks
  {
    get
    {
      if (this._resetTimerLinks == null)
      {
        this._resetTimerLinks = new LongList();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = this.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrObjectListID);
          if (attributeById != null)
          {
            if (!attributeById.IsNull)
            {
              foreach (object obj in attributeById.Values)
                this._resetTimerLinks.Add((long) Convert.ToInt32(obj));
            }
            this._resetTimerLinks.Modified = false;
          }
        }
      }
      return this._resetTimerLinks;
    }
    set => this._resetTimerLinks = value;
  }

  protected override MapText CreateLabel(string name)
  {
    MapText label = (MapText) null;
    if (name != null)
    {
      label = (MapText) new WorkflowMapText(false);
      label.Text = name;
      label.Selectable = this.DraggableLabel;
    }
    return label;
  }

  public bool IsFlagSet(ActivityFlags flag)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return MiscFunx.IsFlagSet(this.GetActivity(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrAddIDID), ActivityFlags.FilterObjects);
  }

  /// <summary>Returns True if specified variable is used</summary>
  /// <param name="varAttrID"></param>
  /// <param name="doDeletion">If False, no deletion is performed, test only</param>
  /// <returns></returns>
  public bool ProcessVariableReferences(int varAttrID, bool doDeletion)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject activity = this.GetActivity(sessionKeeper.Session);
      if (activity != null)
      {
        WorkflowNode.UsedIn usedIn = WorkflowNode.UsedIn.None;
        if (this.ActivityKind != ActivityKind.Start && wfConsts.IsParticipantActivity(this.ActivityKind))
        {
          IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrParticipantsID);
          if (attributeById != null)
          {
            ParticipantList participantList = new ParticipantList(sessionKeeper.Session);
            participantList.AsString = attributeById.Value.ToString();
            if (participantList.ProcessVariableReferences(varAttrID, doDeletion))
            {
              usedIn |= WorkflowNode.UsedIn.Participants;
              if (doDeletion)
                attributeById.Value = (object) participantList.AsString;
            }
          }
        }
        if (doDeletion || usedIn == WorkflowNode.UsedIn.None)
        {
          IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrNotificationsID);
          if (attributeById != null)
          {
            Notifications notifications = new Notifications(sessionKeeper.Session);
            notifications.Load(attributeById);
            if (notifications.ProcessVariableReferences(varAttrID, doDeletion))
            {
              usedIn |= WorkflowNode.UsedIn.Notifications;
              if (doDeletion)
                notifications.Save(attributeById);
            }
          }
        }
        if (usedIn == WorkflowNode.UsedIn.None)
        {
          if (this.ActivityKind == ActivityKind.Condition)
          {
            IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrConditionID);
            if (attributeById != null)
            {
              TempFormula tempFormula = MiscFunx.FormulaFromAttribute(attributeById);
              if (tempFormula != null)
              {
                foreach (AttribPair usedAttr in tempFormula.usedAttrs)
                {
                  if (usedAttr.attribID == varAttrID)
                  {
                    usedIn |= WorkflowNode.UsedIn.Conditions;
                    if (doDeletion)
                    {
                      IBlobWriter blobWriter = attributeById as IBlobWriter;
                      BlobInformation blobInfo = new BlobInformation(0L, 0L, DateTime.Now, "", ArcMethods.NotPacked, "");
                      if (blobWriter.OpenBlob(blobInfo, false))
                      {
                        blobWriter.WriteDataBlock(new byte[0]);
                        break;
                      }
                      break;
                    }
                    break;
                  }
                }
              }
            }
            IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._activityID, wfConsts.AttrConditionFormulaID);
            if (objectAttributeById != null)
            {
              ExpressionInfo expressionFromAttr = MiscFunx.GetExpressionFromAttr(objectAttributeById);
              if (!string.IsNullOrEmpty(expressionFromAttr.FormulaForLink) && MiscFunx.CheckVariableInExpression(expressionFromAttr.FormulaForLink, varAttrID))
              {
                usedIn |= WorkflowNode.UsedIn.Conditions;
                if (doDeletion)
                {
                  IBlobWriter blobWriter = objectAttributeById as IBlobWriter;
                  BlobInformation blobInfo = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty);
                  if (blobWriter.OpenBlob(blobInfo, false))
                    blobWriter.WriteDataBlock(new byte[0]);
                }
              }
            }
          }
          else if (this.ActivityKind == ActivityKind.Case)
          {
            bool flag = false;
            if (this.ExpertConditions != null)
            {
              foreach (ConditionInfo expertCondition in this.ExpertConditions)
              {
                if (expertCondition.ExpertFormula != null)
                {
                  foreach (AttribPair usedAttr in expertCondition.ExpertFormula.usedAttrs)
                  {
                    if (usedAttr.attribID == varAttrID)
                    {
                      usedIn |= WorkflowNode.UsedIn.Conditions;
                      if (doDeletion)
                      {
                        expertCondition.ExpertFormula = new TempFormula();
                        expertCondition.ExpertFormula.Init();
                        flag = true;
                        break;
                      }
                      break;
                    }
                  }
                }
              }
            }
            if (this.ExpressionConditions != null)
            {
              for (int index = 0; index < this.ExpressionConditions.Count; ++index)
              {
                ExpressionInfo expressionCondition = this.ExpressionConditions[index];
                if (!expressionCondition.ElseLink && !string.IsNullOrEmpty(expressionCondition.FormulaForLink) && MiscFunx.CheckVariableInExpression(expressionCondition.FormulaForLink, varAttrID))
                {
                  usedIn |= WorkflowNode.UsedIn.Conditions;
                  if (doDeletion)
                  {
                    this.ExpressionConditions[index].FormulaForLink = string.Empty;
                    flag = true;
                    break;
                  }
                  break;
                }
              }
            }
            if (flag)
              this.SaveConditions();
          }
        }
        if (usedIn > WorkflowNode.UsedIn.None)
        {
          if (doDeletion)
            this.UpdateVisibleProperties(activity);
          return true;
        }
      }
    }
    return false;
  }

  public bool JustCreated
  {
    get => this._justCreated;
    set => this._justCreated = value;
  }

  public override MapObject SelectionObject => (MapObject) this._back;

  public override string ToString() => this.ToolTipText;

  /// <summary>
  /// Указывает, размещен ли узел на диаграмме, или уже удален
  /// </summary>
  public bool Alive => this.Layer != null;

  public bool Alien => this.ProcessID != this.View.ProcessID;

  public string DebugInfo
  {
    get
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["ObjectID"] = this._activityID.ToString();
      dictionary["ParentActivityID"] = this._parentActivityID.ToString();
      dictionary["ProcessID"] = this.ProcessID.ToString();
      dictionary["Alien"] = this.Alien ? "True" : "False";
      string debugInfo = string.Empty;
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
        debugInfo = $"{debugInfo}{keyValuePair.Key}={keyValuePair.Value}\r\n";
      return debugInfo;
    }
  }

  /// <summary>
  /// Метода для старта процесса с текущего места в шаблоне/запущенном процессе
  /// </summary>
  private void StartProcessFromThis()
  {
    ICurrentUserAndRole service = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (this.ProcessID == -1L || !this.View.ReadOnly)
      return;
    if (this.View.IsProcess)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.ProcessID, false);
        IExecuteService customService = sessionKeeper.Session.GetCustomService(typeof (IExecuteService)) as IExecuteService;
        if (dbObject != null && customService != null)
        {
          switch (dbObject)
          {
            case IProcess process2:
              if (process2.ProcessStatus == ActivityStatus.Executed)
              {
                if (((IEnumerable<IActivity>) process2.Activities).Where<IActivity>((System.Func<IActivity, bool>) (x => x.Executed)).ToList<IActivity>().Count == 0)
                {
                  switch (this.ActivityKind)
                  {
                    case ActivityKind.Start:
                    case ActivityKind.Stop:
                    case ActivityKind.Abort:
                      if (MessageBox.Show("Данное действие приведёт к повторному запуску процесса с самого начала. Выполнить стандартный запуск?", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                      wfFunx.CreateProcess(process2.PrototypeSchemeID);
                      return;
                    case ActivityKind.Task:
                    case ActivityKind.Approve:
                      customService.Execute(this.ProcessID, this.ActivityID, service.UserID);
                      return;
                    case ActivityKind.Script:
                    case ActivityKind.RemoteSubProcess:
                      customService.Execute(this.ProcessID, this.ActivityID, service.UserID);
                      return;
                    default:
                      customService.Execute(this.ProcessID, this.ActivityID, service.UserID);
                      return;
                  }
                }
                else
                {
                  int num = (int) MessageBox.Show("Процесс имеет выполняющиеся действия, запуск с выбранного места невозможен.");
                  break;
                }
              }
              else
              {
                if (MessageBox.Show("Данный процесс невозможно перезапустить с выбранного места. Выполнить стандартный запуск?", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
                  break;
                wfFunx.CreateProcess(process2.PrototypeSchemeID);
                break;
              }
            case IScheme _:
              if (sessionKeeper.Session.GetObject(this.View.ProcessID, false) is IProcess process1)
              {
                if (process1.ProcessStatus == ActivityStatus.Executed)
                {
                  if (((IEnumerable<IActivity>) process1.Activities).Where<IActivity>((System.Func<IActivity, bool>) (x => x.Executed)).ToList<IActivity>().Count == 0)
                  {
                    int lastIndex = ((IEnumerable<IActivity>) process1.Activities).ToList<IActivity>().FindLastIndex((Predicate<IActivity>) (x => x.Status == ActivityStatus.Completed));
                    if (lastIndex == -1)
                      break;
                    IActivity activity = process1.Activities[lastIndex];
                    customService.ExecuteCustomSender(this.View.ProcessID, this.ActivityID, activity.ObjectID, service.UserID);
                    break;
                  }
                  int num = (int) MessageBox.Show("Процесс имеет выполняющиеся действия, запуск с выбранного места невозможен.");
                  break;
                }
                if (MessageBox.Show("Данный процесс невозможно перезапустить с выбранного места. Выполнить стандартный запуск?", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
                  break;
                wfFunx.CreateProcess(process1.PrototypeSchemeID);
                break;
              }
              if (MessageBox.Show("Данный процесс невозможно перезапустить с выбранного места. Выполнить стандартный запуск?", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
                break;
              wfFunx.CreateProcess(dbObject.ObjectID);
              break;
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show("Объект процесса не найден, запуск невозможен.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }
    else
    {
      switch (this.ActivityKind)
      {
        case ActivityKind.Start:
          wfFunx.CreateProcess(this.ProcessID);
          break;
        case ActivityKind.Task:
        case ActivityKind.Approve:
          throw new NotImplementedException();
        case ActivityKind.Stop:
        case ActivityKind.Abort:
          int num2 = (int) MessageBox.Show("Запуск процесса с действий окончания невозможен, будет произведён запуск со старта.");
          wfFunx.CreateProcess(this.ProcessID);
          break;
        case ActivityKind.Script:
        case ActivityKind.RemoteSubProcess:
          throw new NotImplementedException();
        default:
          throw new NotImplementedException();
      }
    }
  }

  [Flags]
  private enum UsedIn
  {
    None = 0,
    Participants = 1,
    Notifications = 2,
    Conditions = 4,
  }
}
