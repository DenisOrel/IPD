// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowLink
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Map;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for WorkflowLink.</summary>
[Serializable]
public class WorkflowLink : MapLabeledLink
{
  private long _linkID;
  private bool _deleted;
  private LinkKind _linkKind;
  private MapText _caption;
  private bool _resetTimer;
  private bool _copied;

  public IDBObject GetLink(IUserSession session)
  {
    IDBObject link = this._linkID == 0L ? session.GetObjectCollection(wfConsts.LinksTypeID).Create() : session.GetObject(this._linkID);
    this._linkID = link.ObjectID;
    return link;
  }

  public long LinkID => this._linkID;

  public WorkflowLink() => this.InitStyles();

  public WorkflowLink(long linkID)
  {
    this._linkID = linkID;
    this.InitStyles();
  }

  public WorkflowLink(ActivityLink l)
  {
    if (l != null)
    {
      this._linkID = l.ObjectID;
      this._linkKind = l.Kind;
    }
    this.InitStyles();
  }

  public void UpdateStroke()
  {
    this.LinkKind = this.LinkKind;
    if (!(this.ToNode is WorkflowNode toNode) || toNode.ActivityKind != ActivityKind.Timer)
      return;
    this.ResetTimer = toNode.ResetTimerLinks.IndexOf(Math.Abs(this.LinkID)) != -1;
  }

  private void UpdateNodesRefs(IDBObject link)
  {
    link.GetAttributeByID(wfConsts.AttrFromActivityID).AsInteger = Math.Abs((this.FromNode as WorkflowNode).ActivityID);
    link.GetAttributeByID(wfConsts.AttrToActivityID).AsInteger = Math.Abs((this.ToNode as WorkflowNode).ActivityID);
  }

  public void UpdateNodesRefs()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateNodesRefs(this.GetLink(sessionKeeper.Session));
  }

  public void Save(IDBObject proc)
  {
    if (this._deleted)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject link = this.GetLink(sessionKeeper.Session);
      if (!link.IsCreationMode)
        return;
      link.GetAttributeByID(wfConsts.AttrLinkKindID).AsInteger = (long) this.LinkKind;
      this.UpdateNodesRefs(link);
      link.CommitCreation(false);
      IDBObject dbObject = link.CheckOut();
      this._linkID = dbObject.ObjectID;
      IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrProcessID);
      if (attributeById == null)
        throw new Exception("wf: Link.Save - атрибут \"Процесс\" не найден!");
      attributeById.AsInteger = Math.Abs(proc.ObjectID);
    }
  }

  public void InitServerObjectIfNeed()
  {
    if (this._linkID != 0L)
      return;
    if (this._deleted)
      throw new Exception("wf: Попытка получить уже удалённую ссылку");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.GetLink(sessionKeeper.Session);
  }

  public void InitNew(long pid)
  {
    if (!this.Backward || !(this.FromNode is WorkflowNode))
      return;
    ((WorkflowNode) this.FromNode).LinksChanged();
  }

  public void DeletedFromDocument()
  {
    if (this.FromNode is WorkflowNode)
      ((WorkflowNode) this.FromNode).LinkDeleted(this, true);
    if (!(this.ToNode is WorkflowNode))
      return;
    ((WorkflowNode) this.ToNode).LinkDeleted(this, false);
  }

  public bool Backward
  {
    get => this.Orthogonal;
    set
    {
      this.Orthogonal = value;
      if (!value)
        return;
      this._linkKind = LinkKind.Backward;
      this.AdjustingStyle = MapLinkAdjustingStyle.Stretch;
      this.RealLink.CalculateStroke();
    }
  }

  public string Caption
  {
    get => this._caption == null ? "" : this._caption.Text;
    set
    {
      if (value == "")
      {
        this.MidLabel = (MapObject) null;
        this._caption = (MapText) null;
      }
      else
      {
        if (this._caption == null)
        {
          MapText mapText = (MapText) new WorkflowMapText(true);
          this.MidLabel = (MapObject) mapText;
          this.MidLabelCentered = true;
          this._caption = mapText;
        }
        this._caption.Text = value;
      }
    }
  }

  public LinkKind LinkKind
  {
    get => this._linkKind;
    set
    {
      this._linkKind = value;
      this.Backward = value == LinkKind.Backward;
      this.LinkKindChanged();
    }
  }

  public LinkKind DBLinkKind
  {
    get => this.LinkKind;
    set
    {
      if (this.LinkKind == value)
        return;
      this.LinkKind = value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.GetLink(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrLinkKindID).AsInteger = (long) value;
    }
  }

  public bool ResetTimer
  {
    get => this._resetTimer;
    set
    {
      if (this._resetTimer == value)
        return;
      this._resetTimer = value;
      Pen pen = new Pen(this.Pen.Brush, this.Pen.Width);
      if (value)
      {
        pen.DashStyle = DashStyle.DashDotDot;
        pen.DashOffset = 20f;
        pen.DashCap = DashCap.Round;
      }
      else
        pen.DashStyle = DashStyle.Solid;
      this.Pen = pen;
    }
  }

  public PointF[] Points
  {
    get
    {
      PointF[] points = new PointF[this.RealLink.PointsCount];
      for (int i = 0; i < this.RealLink.PointsCount; ++i)
        points[i] = this.RealLink.GetPoint(i);
      return points;
    }
  }

  public override void OnPortChanged(
    IMapPort port,
    int subhint,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect)
  {
    base.OnPortChanged(port, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
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

  private void UpdateNodes()
  {
    if (!(this.FromPort is BackPort) && !(this.ToPort is BackPort))
      return;
    if (this.FromNode is WorkflowNode fromNode)
      fromNode.UpdateSpot(this.FromPort as BackPort, this);
    if (!(this.ToNode is WorkflowNode toNode))
      return;
    toNode.UpdateSpot(this.ToPort as BackPort, this);
  }

  public override void DoResize(
    MapView view,
    RectangleF origRect,
    PointF newPoint,
    int whichHandle,
    MapInputState evttype,
    SizeF min,
    SizeF max)
  {
    base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
  }

  protected override void OnBoundsChanged(RectangleF old) => base.OnBoundsChanged(old);

  public void UpdateCaption()
  {
    if (!(this.FromNode is WorkflowNode fromNode))
      return;
    this.Caption = fromNode.GetCondition(this.LinkKind, this.LinkID);
  }

  public override bool OnContextClick(MapInputEventArgs evt, MapView view)
  {
    if (view is GraphView)
    {
      WorkflowLinkContextMenu.InitMenu(this, (GraphView) view);
      if (WorkflowLinkContextMenu.Menu.Show(BaseHolder.PopupHost, (Control) view, evt.ViewPoint) == WorkflowLinkContextMenu.DelMI)
        view.EditDelete();
    }
    return true;
  }

  public bool Copied => this._copied;

  public override MapObject CopyObject(MapCopyDictionary env)
  {
    MapObject mapObject = base.CopyObject(env);
    (mapObject as WorkflowLink)._copied = true;
    return mapObject;
  }

  internal void InitClone()
  {
    this._linkID = 0L;
    this.InitServerObjectIfNeed();
  }

  /// <summary>
  /// Указывает, размещена ли связь на диаграмме, или уже удалена
  /// </summary>
  public bool Alive => this.Layer != null;

  protected virtual void LinkKindChanged()
  {
    Brush brush = Brushes.Black;
    if (this._linkKind == LinkKind.True)
      brush = Brushes.Green;
    else if (this._linkKind == LinkKind.False)
      brush = Brushes.Red;
    this.Brush = brush;
    this.Pen = new Pen(brush, 1f);
  }

  protected virtual void InitStyles()
  {
    this.Style = MapStrokeStyle.RoundedLine;
    this.ToArrow = true;
  }
}
