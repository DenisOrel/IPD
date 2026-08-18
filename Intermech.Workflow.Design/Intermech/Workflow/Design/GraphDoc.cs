// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GraphDoc
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for GraphDoc.</summary>
[Serializable]
public class GraphDoc : MapDocument
{
  [NonSerialized]
  public GraphView View;
  internal VersionFlags VersionFlags;
  private bool _docHaveSubProcess;
  internal List<long> UnusedLinkIDs = new List<long>();
  internal List<long> ErroneusActivities = new List<long>();
  public const int ChangedLocation = 10023;
  private static int myDocCounter = 1;
  private static Hashtable myDocuments = new Hashtable();
  private static Random myRandom = new Random();
  private string _myLocation = string.Empty;
  private PointF _myNextNodePos = new PointF(30f, 30f);

  public GraphDoc()
  {
    this.Name = "GraphDoc " + (object) GraphDoc.NextDocumentID();
    this.LinksLayer = this.Layers.CreateNewLayerBefore(this.Layers.Default);
    this.LinksLayer.Identifier = (object) "Links";
    this.Layers.CreateNewLayerBefore(this.Layers.Default).Identifier = (object) "bottom";
    this.MaintainsPartID = true;
    this.IsModified = false;
  }

  public string Location
  {
    get => this._myLocation;
    set
    {
      string location = this._myLocation;
      if (!(location != value))
        return;
      GraphDoc.RemoveDocument(location);
      this._myLocation = value;
      GraphDoc.AddDocument(value, this);
      this.RaiseChanged(10023, 0, (object) null, 0, (object) location, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
    }
  }

  public MapComment InsertComment()
  {
    MapComment mapComment1 = new MapComment();
    mapComment1.Text = "Enter your comment here,\r\non multiple lines.";
    mapComment1.Position = this.NextNodePosition();
    MapComment mapComment2 = mapComment1;
    mapComment2.Label.Multiline = true;
    mapComment2.Label.Editable = true;
    this.StartTransaction();
    this.Add((MapObject) mapComment2);
    this.FinishTransaction("Insert Comment");
    return mapComment2;
  }

  public MapObject InsertOrgChart()
  {
    this.StartTransaction();
    MapTextNode a = this.MakePerson("Boss", 300f, 100f);
    this.Add((MapObject) a);
    MapTextNode b1 = this.MakePerson("Worker", 100f, 200f);
    this.Add((MapObject) b1);
    MapTextNode b2 = this.MakePerson("Peon", 200f, 200f);
    this.Add((MapObject) b2);
    MapTextNode b3 = this.MakePerson("Underling", 300f, 200f);
    this.Add((MapObject) b3);
    MapTextNode b4 = this.MakePerson("Resource", 400f, 200f);
    this.Add((MapObject) b4);
    this.Add((MapObject) this.MakeRelationship(a, b1));
    this.Add((MapObject) this.MakeRelationship(a, b2));
    this.Add((MapObject) this.MakeRelationship(a, b3));
    this.Add((MapObject) this.MakeRelationship(a, b4));
    this.FinishTransaction("Insert OrgChart");
    return (MapObject) a;
  }

  public MapTextNode MakePerson(string name, float x, float y)
  {
    MapTextNode mapTextNode = new MapTextNode();
    mapTextNode.LeftPort = (MapPort) null;
    mapTextNode.RightPort = (MapPort) null;
    mapTextNode.Text = name;
    (mapTextNode.Background as MapShape).Brush = (Brush) null;
    mapTextNode.Location = new PointF(x, y);
    return mapTextNode;
  }

  public MapLink MakeRelationship(MapTextNode a, MapTextNode b)
  {
    MapLink mapLink = new MapLink();
    mapLink.Orthogonal = true;
    mapLink.Style = MapStrokeStyle.RoundedLine;
    mapLink.Brush = (Brush) null;
    mapLink.FromPort = (IMapPort) a.BottomPort;
    mapLink.ToPort = (IMapPort) b.TopPort;
    return mapLink;
  }

  public PointF NextNodePosition()
  {
    PointF nextNodePos = this._myNextNodePos;
    this._myNextNodePos.X += 50f;
    if ((double) this._myNextNodePos.X <= 400.0)
      return nextNodePos;
    this._myNextNodePos.X = 40f;
    this._myNextNodePos.Y += 50f;
    if ((double) this._myNextNodePos.Y <= 300.0)
      return nextNodePos;
    this._myNextNodePos.Y = 40f;
    return nextNodePos;
  }

  public override void ChangeValue(MapChangedEventArgs e, bool undo)
  {
    if (e.Hint == 10023)
      this.Location = (string) e.GetValue(undo);
    else
      base.ChangeValue(e, undo);
  }

  public override void Undo()
  {
    base.Undo();
    this.View.UpdateModifiedState();
  }

  public override void Redo()
  {
    base.Redo();
    this.View.UpdateModifiedState();
  }

  public override bool FinishTransaction(string tname) => base.FinishTransaction(tname);

  public override bool AbortTransaction()
  {
    int num = base.AbortTransaction() ? 1 : 0;
    this.View.UpdateCommands();
    return num != 0;
  }

  public void Save()
  {
    List<WorkflowLink> workflowLinkList = new List<WorkflowLink>();
    float num = 0.0f;
    IEnumerator enumerator = (IEnumerator) this.GetEnumerator();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is WorkflowNode current2)
        {
          if ((double) num > (double) current2.SelectionObject.Top)
            num = current2.SelectionObject.Top;
          ActivityGraphData activityGraphData = new ActivityGraphData(string.Empty)
          {
            X = (int) Math.Round((double) current2.Left),
            Y = (int) Math.Round((double) current2.Top)
          };
          sessionKeeper.Session.AddObjectAttribute(current2.ActivityID, wfConsts.AttrGraphDataID, false, false, new object[1]
          {
            (object) activityGraphData.ToString()
          });
        }
        else if (enumerator.Current is WorkflowLink current1)
          workflowLinkList.Add(current1);
      }
      this.TopLeft = (double) num >= 0.0 ? new PointF(0.0f, 0.0f) : new PointF(0.0f, num - 10f);
      foreach (WorkflowLink workflowLink in workflowLinkList)
      {
        string str = string.Empty;
        if (workflowLink.RealLink.PointsCount > 2)
        {
          str += workflowLink.RealLink.PointsCount.ToString();
          for (int i = 0; i < workflowLink.RealLink.PointsCount; ++i)
          {
            PointF point = workflowLink.RealLink.GetPoint(i);
            str = $"{str}|{Math.Round((double) point.X).ToString()}|{Math.Round((double) point.Y).ToString()}";
          }
        }
        if (str != "")
          sessionKeeper.Session.AddObjectAttribute(workflowLink.LinkID, wfConsts.AttrGraphDataID, false, false, new object[1]
          {
            (object) new GraphData(string.Empty)
            {
              Values = {
                ["P"] = str
              }
            }.ToString()
          });
        else
          sessionKeeper.Session.GetObjectAttributeByID(workflowLink.LinkID, wfConsts.AttrGraphDataID)?.Delete(0L);
      }
    }
  }

  private float SmartToSingle(object obj)
  {
    string str = obj.ToString();
    if (str.Contains(","))
      str = str.Replace(',', '.');
    return (float) Math.Round((double) Convert.ToSingle(str, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public event EventHandler Loaded;

  public bool DocHaveSubProcess
  {
    get => this._docHaveSubProcess;
    set => this._docHaveSubProcess = value;
  }

  public void Load(long processID, bool isProcess)
  {
    Dictionary<long, WorkflowNode> dictionary = new Dictionary<long, WorkflowNode>();
    List<WorkflowLink> workflowLinkList = new List<WorkflowLink>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      WorkflowGraph workflowGraph = new WorkflowGraph(processID, sessionKeeper.Session);
      float num1 = 100f;
      float num2 = 0.0f;
      foreach (ActivityNode node in workflowGraph.Nodes)
      {
        WorkflowNode workflowNode = new WorkflowNode(node);
        dictionary[node.ObjectID] = workflowNode;
        workflowNode.View = this.View;
        workflowNode.SetFirstLocalScripts();
        if (!node.GraphData.Empty)
        {
          workflowNode.Left = (float) node.GraphData.X;
          workflowNode.Top = (float) node.GraphData.Y;
          if ((double) workflowNode.Left < (double) num1)
            num1 = workflowNode.Left;
          if ((double) workflowNode.Bottom > (double) num2)
            num2 = workflowNode.Bottom;
        }
        else
        {
          workflowNode.Left = num1;
          num1 += workflowNode.Width;
          workflowNode.Top = num2 + 32f;
        }
        workflowNode.Clones = node.CloneIDs;
        if (workflowNode.ActivityKind == ActivityKind.SubProcess)
          this._docHaveSubProcess = true;
        this.Add((MapObject) workflowNode);
      }
      int num3 = -1;
      foreach (ActivityLink link in workflowGraph.Links)
      {
        ++num3;
        long objectId1 = link.From.ObjectID;
        long objectId2 = link.To.ObjectID;
        WorkflowNode workflowNode1 = (WorkflowNode) null;
        dictionary.TryGetValue(objectId1, out workflowNode1);
        WorkflowNode workflowNode2 = (WorkflowNode) null;
        dictionary.TryGetValue(objectId2, out workflowNode2);
        long num4 = 0;
        Guid empty = Guid.Empty;
        if (workflowNode1 != null && workflowNode2 != null)
        {
          WorkflowLink workflowLink = WorkflowLinkCreator.Create(link);
          workflowLink.FromPort = (IMapPort) workflowNode1.Port;
          workflowLink.ToPort = (IMapPort) workflowNode2.Port;
          workflowLinkList.Add(workflowLink);
          workflowLink.UpdateCaption();
          this.LinksLayer.Add((MapObject) workflowLink);
          workflowLink.UpdateStroke();
          if (workflowLink.Backward)
          {
            if (workflowLink.FromPort != null)
              workflowLink.FromPort = workflowLink.FromPort.Node is WorkflowNode node1 ? (IMapPort) node1.BackwardPort : (IMapPort) null;
            if (workflowLink.ToPort != null)
              workflowLink.ToPort = workflowLink.ToPort.Node is WorkflowNode node2 ? (IMapPort) node2.BackwardPort : (IMapPort) null;
            string str = link.GraphData.Values["P"];
            if (str != null)
            {
              string[] strArray = str.Split('|');
              if (strArray.Length > 1 && workflowLink.RealLink.PointsCount > 2)
              {
                int int32 = Convert.ToInt32(strArray[0]);
                for (int i = 0; i < int32; ++i)
                {
                  PointF p = new PointF((float) Convert.ToInt32(strArray[i * 2 + 1]), (float) Convert.ToInt32(strArray[i * 2 + 2]));
                  workflowLink.RealLink.SetPoint(i, p);
                }
              }
            }
            else
            {
              PointF position = workflowNode1.Position;
              double y1 = (double) position.Y;
              position = workflowNode2.Position;
              double y2 = (double) position.Y;
              if ((double) Math.Abs((float) (y1 - y2)) == 0.0)
              {
                PointF[] points = workflowLink.Points;
                ref PointF local1 = ref points[2];
                position = workflowNode2.Position;
                double num5 = (double) position.Y - 15.0;
                local1.Y = (float) num5;
                ref PointF local2 = ref points[3];
                position = workflowNode2.Position;
                double num6 = (double) position.Y - 15.0;
                local2.Y = (float) num6;
                workflowLink.RealLink.SetPoints(points);
              }
            }
          }
        }
        else if (num4 != 0L)
          this.UnusedLinkIDs.Add(num4);
      }
      if (!isProcess)
      {
        foreach (ActivityNode node in workflowGraph.Nodes)
        {
          for (int index = 0; index < node.Statuses.Count; ++index)
          {
            if (node.Statuses[index] != ActivityStatus.OnApproach && node.ObjectIDs.Count > index)
              this.ErroneusActivities.Add(node.ObjectIDs[index]);
          }
        }
      }
    }
    foreach (WorkflowNode workflowNode in dictionary.Values)
      workflowNode.UpdateSpots(false);
    SimpleThreadPool simpleThreadPool = new SimpleThreadPool(5);
    simpleThreadPool.AllCompleted += new EventHandler(this.AsyncLoadCompleted);
    if (dictionary.Count > 0 && this.View != null)
    {
      Action<WorkflowNode> a = (Action<WorkflowNode>) (n =>
      {
        if (!n.Alive || this.View.IsEditorClosed)
          return;
        n.UpdateInfoImages(false);
      });
      foreach (WorkflowNode workflowNode in dictionary.Values)
      {
        WorkflowNode ln = workflowNode;
        simpleThreadPool.Enqueue((Action) (() => a(ln)));
      }
    }
    else
      this.AsyncLoadCompleted((object) null, (EventArgs) null);
  }

  private void AsyncLoadCompleted(object sender, EventArgs e)
  {
    EventHandler loaded = this.Loaded;
    if (loaded == null)
      return;
    loaded((object) this, (EventArgs) null);
  }

  public bool ReadOnly
  {
    get => !this.AllowEdit;
    set => this.SetModifiable(!value);
  }

  public static int NextDocumentID() => GraphDoc.myDocCounter++;

  public static GraphDoc FindDocument(string location)
  {
    return GraphDoc.myDocuments[(object) location] as GraphDoc;
  }

  internal static void AddDocument(string location, GraphDoc doc)
  {
    GraphDoc.myDocuments[(object) location] = (object) doc;
  }

  internal static void RemoveDocument(string location)
  {
    GraphDoc.myDocuments.Remove((object) location);
  }

  public static MapText MakeText(string s)
  {
    MapText mapText = new MapText();
    mapText.Alignment = 1;
    mapText.Text = s;
    mapText.Selectable = false;
    mapText.Editable = true;
    return mapText;
  }

  public static int NextRandom(int i) => GraphDoc.myRandom.Next(i);

  public string GenerateNodeName(WorkflowNode Node)
  {
    ArrayList arrayList = new ArrayList();
    IEnumerator enumerator = (IEnumerator) this.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if (enumerator.Current is WorkflowNode current && current != Node && current.Text.StartsWith(Node.Text))
        arrayList.Add((object) current.Text);
    }
    for (int index1 = 1; index1 < int.MaxValue; ++index1)
    {
      bool flag = false;
      string empty = string.Empty;
      string nodeName = index1 == 1 ? Node.Text : $"{Node.Text} {index1}";
      for (int index2 = 0; index2 < arrayList.Count; ++index2)
      {
        if (arrayList[index2].ToString().Equals(nodeName))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return nodeName;
    }
    return Node.Text;
  }
}
