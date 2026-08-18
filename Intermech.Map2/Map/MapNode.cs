// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapNode
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapNode : MapGroup, IMapNode, IMapGraphPart, IMapLabeledNode, IMapIdentifiablePart
    {
      public const int ChangedNodeUserFlags = 2000;
      public const int ChangedNodeUserObject = 2001;
      public const int ChangedPartID = 2004;
      internal const int ChangedPropertiesDelegatedToSelectionObject = 2003;
      public const int ChangedToolTipText = 2002;
      private const int flagPropsOnSelObj = 4194304 /*0x400000*/;
      private int myPartID;
      [NonSerialized]
      internal ArrayList myParts;
      private string myToolTipText;
      private int myUserFlags;
      private object myUserObject;

      public MapNode()
      {
        this.myToolTipText = (string) null;
        this.myParts = (ArrayList) null;
        this.myUserFlags = 0;
        this.myUserObject = (object) null;
        this.myPartID = -1;
      }

      private void addItem(ArrayList items, IMapGraphPart obj)
      {
        if (obj == null || items.Contains((object) obj))
          return;
        items.Add((object) obj);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2000:
            this.UserFlags = e.GetInt(undo);
            break;
          case 2001:
            this.UserObject = e.GetValue(undo);
            break;
          case 2002:
            this.ToolTipText = (string) e.GetValue(undo);
            break;
          case 2003:
            this.PropertiesDelegatedToSelectionObject = (bool) e.GetValue(undo);
            break;
          case 2004:
            this.PartID = e.GetInt(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      private void considerLink(IMapLink l, IMapPort p, MapNode.Search s, ArrayList items)
      {
        bool flag = (s & MapNode.Search.NotSelf) == (MapNode.Search) 0;
        if (l.FromPort == p && (flag || l.ToPort.MapObject == null || !l.ToPort.MapObject.IsChildOf((MapObject) this)))
        {
          if ((s & MapNode.Search.LinksOut) != (MapNode.Search) 0)
            this.addItem(items, (IMapGraphPart) l);
          if ((s & MapNode.Search.NodesOut) != (MapNode.Search) 0)
            this.addItem(items, (IMapGraphPart) l.ToNode);
        }
        if (l.ToPort != p || !flag && l.FromPort.MapObject != null && l.FromPort.MapObject.IsChildOf((MapObject) this))
          return;
        if ((s & MapNode.Search.LinksIn) != (MapNode.Search) 0)
          this.addItem(items, (IMapGraphPart) l);
        if ((s & MapNode.Search.NodesIn) == (MapNode.Search) 0)
          return;
        this.addItem(items, (IMapGraphPart) l.FromNode);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapNode mapNode = (MapNode) base.CopyObject(env);
        if (mapNode != null)
        {
          mapNode.myParts = (ArrayList) null;
          mapNode.myPartID = -1;
        }
        return (MapObject) mapNode;
      }

      internal void CopyPropertiesFromSelectionObject(MapObject oldobj, MapObject newobj)
      {
        if (oldobj == null || newobj == null || oldobj != this.SelectionObject)
          return;
        newobj.Center = oldobj.Center;
        newobj.Selectable = oldobj.Selectable;
        newobj.Resizable = oldobj.Resizable;
        newobj.Reshapable = oldobj.Reshapable;
        newobj.ResizesRealtime = oldobj.ResizesRealtime;
        newobj.Shadowed = oldobj.Shadowed;
      }

      public override void DoBeginEdit(MapView view)
      {
        if (this.Label == null)
          return;
        this.Label.DoBeginEdit(view);
      }

      internal ArrayList findAll(MapNode.Search s)
      {
        ArrayList items = this.myParts;
        if (items == null)
        {
          items = new ArrayList();
        }
        else
        {
          items.Clear();
          this.myParts = (ArrayList) null;
        }
        this.findAllAux((MapObject) this, s, items);
        return items;
      }

      private void findAllAux(MapObject obj, MapNode.Search s, ArrayList items)
      {
        if (obj is IMapPort p)
        {
          if ((s & MapNode.Search.Ports) != (MapNode.Search) 0)
            this.addItem(items, (IMapGraphPart) p);
          if (p is MapPort mapPort)
          {
            foreach (IMapLink link in mapPort.Links)
              this.considerLink(link, p, s, items);
          }
          else
          {
            foreach (IMapLink link in p.Links)
              this.considerLink(link, p, s, items);
          }
        }
        if (!(obj is MapGroup mapGroup))
          return;
        foreach (MapObject mapObject in mapGroup.GetEnumerator())
          this.findAllAux(mapObject, s, items);
      }

      internal static MapText FindLabel(MapObject obj)
      {
        switch (obj)
        {
          case MapText label1:
            return label1;
          case MapGroup mapGroup:
            foreach (MapObject mapObject in mapGroup.GetEnumerator())
            {
              MapText label = MapNode.FindLabel(mapObject);
              if (label != null)
                return label;
            }
            break;
        }
        return (MapText) null;
      }

      private MapNodeLinkEnumerator GetLinkEnumerator(MapNode.Search s)
      {
        return new MapNodeLinkEnumerator(this, s);
      }

      private MapNodeNodeEnumerator GetNodeEnumerator(MapNode.Search s)
      {
        return new MapNodeNodeEnumerator(this, s);
      }

      private MapNodePortEnumerator GetPortEnumerator()
      {
        return new MapNodePortEnumerator(this, MapNode.Search.Ports);
      }

      public override string GetToolTip(MapView view) => this.ToolTipText;

      IEnumerable IMapNode.DestinationLinks
      {
        get => (IEnumerable) this.GetLinkEnumerator(MapNode.Search.LinksOut);
      }

      IEnumerable IMapNode.Destinations
      {
        get => (IEnumerable) this.GetNodeEnumerator(MapNode.Search.NodesOut);
      }

      IEnumerable IMapNode.Links
      {
        get => (IEnumerable) this.GetLinkEnumerator(MapNode.Search.LinksIn | MapNode.Search.LinksOut);
      }

      IEnumerable IMapNode.Nodes
      {
        get => (IEnumerable) this.GetNodeEnumerator(MapNode.Search.NodesIn | MapNode.Search.NodesOut);
      }

      IEnumerable IMapNode.Ports => (IEnumerable) this.GetPortEnumerator();

      IEnumerable IMapNode.SourceLinks => (IEnumerable) this.GetLinkEnumerator(MapNode.Search.LinksIn);

      IEnumerable IMapNode.Sources => (IEnumerable) this.GetNodeEnumerator(MapNode.Search.NodesIn);

      [Description("Gets an enumerator over all of the links going out of this node.")]
      public virtual MapNodeLinkEnumerator DestinationLinks
      {
        get => this.GetLinkEnumerator(MapNode.Search.LinksOut);
      }

      [Description("Gets an enumerator over all of the nodes that have links going out of this node.")]
      public virtual MapNodeNodeEnumerator Destinations
      {
        get => this.GetNodeEnumerator(MapNode.Search.NodesOut);
      }

      [Description("Returns itself as a MapObject.")]
      public MapObject MapObject
      {
        get => (MapObject) this;
        set
        {
        }
      }

      public virtual MapText Label
      {
        get => MapNode.FindLabel((MapObject) this);
        set
        {
        }
      }

      [Description("Gets an enumerator over all of the links connected to this node.")]
      public virtual MapNodeLinkEnumerator Links
      {
        get => this.GetLinkEnumerator(MapNode.Search.LinksIn | MapNode.Search.LinksOut);
      }

      public override PointF Location
      {
        get
        {
          return this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this ? this.SelectionObject.Center : this.Position;
        }
        set
        {
          if (this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this)
          {
            SizeF sizeF = MapTool.SubtractPoints(this.SelectionObject.Center, this.Position);
            this.Position = new PointF(value.X - sizeF.Width, value.Y - sizeF.Height);
          }
          else
            this.Position = value;
        }
      }

      [Description("Gets an enumerator over all of the nodes that are connected to this node.")]
      public virtual MapNodeNodeEnumerator Nodes
      {
        get => this.GetNodeEnumerator(MapNode.Search.NodesIn | MapNode.Search.NodesOut);
      }

      [Category("Ownership")]
      [Description("The unique ID of this part in its document.")]
      public int PartID
      {
        get => this.myPartID;
        set
        {
          int partId = this.myPartID;
          if (partId == value)
            return;
          this.myPartID = value;
          this.Changed(2004, partId, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("Gets an enumerator over all of the ports that are part of this node.")]
      public virtual MapNodePortEnumerator Ports => this.GetPortEnumerator();

      internal bool PropertiesDelegatedToSelectionObject
      {
        get => (this.InternalFlags & 4194304 /*0x400000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 4194304 /*0x400000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 4194304 /*0x400000*/;
          else
            this.InternalFlags &= -4194305;
          this.Changed(2003, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override bool Reshapable
      {
        get
        {
          return this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this ? this.SelectionObject.Reshapable : base.Reshapable;
        }
        set
        {
          if (this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this)
            this.SelectionObject.Reshapable = value;
          else
            base.Reshapable = value;
        }
      }

      public override bool Resizable
      {
        get
        {
          return this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this ? this.SelectionObject.Resizable : base.Resizable;
        }
        set
        {
          if (this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this)
            this.SelectionObject.Resizable = value;
          else
            base.Resizable = value;
        }
      }

      public override bool ResizesRealtime
      {
        get
        {
          return this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this ? this.SelectionObject.ResizesRealtime : base.ResizesRealtime;
        }
        set
        {
          if (this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this)
            this.SelectionObject.ResizesRealtime = value;
          else
            base.ResizesRealtime = value;
        }
      }

      public override bool Shadowed
      {
        get
        {
          return this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this ? this.SelectionObject.Shadowed : base.Shadowed;
        }
        set
        {
          if (this.PropertiesDelegatedToSelectionObject && this.SelectionObject != this)
            this.SelectionObject.Shadowed = value;
          else
            base.Shadowed = value;
        }
      }

      [Description("Gets an enumerator over all of the links coming into this node.")]
      public virtual MapNodeLinkEnumerator SourceLinks
      {
        get => this.GetLinkEnumerator(MapNode.Search.LinksIn);
      }

      [Description("Gets an enumerator over all of the nodes that have links coming into this node.")]
      public virtual MapNodeNodeEnumerator Sources => this.GetNodeEnumerator(MapNode.Search.NodesIn);

      public virtual string Text
      {
        get
        {
          MapText label = this.Label;
          return label != null ? label.Text : "";
        }
        set
        {
          MapText label = this.Label;
          if (label == null)
            return;
          label.Text = value;
        }
      }

      [Description("A string to be displayed in a tooltip.")]
      public virtual string ToolTipText
      {
        get => this.myToolTipText;
        set
        {
          string toolTipText = this.myToolTipText;
          if (!(toolTipText != value))
            return;
          this.myToolTipText = value;
          this.Changed(2002, 0, (object) toolTipText, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(0)]
      [Description("An integer value associated with this node.")]
      public virtual int UserFlags
      {
        get => this.myUserFlags;
        set
        {
          int userFlags = this.myUserFlags;
          if (userFlags == value)
            return;
          this.myUserFlags = value;
          this.Changed(2000, userFlags, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("An object associated with this node.")]
      [DefaultValue(null)]
      public virtual object UserObject
      {
        get => this.myUserObject;
        set
        {
          object userObject = this.myUserObject;
          if (userObject == value)
            return;
          this.myUserObject = value;
          this.Changed(2001, 0, userObject, MapObject.NullRect, 0, value, MapObject.NullRect);
        }
      }

      [Flags]
      internal enum Search
      {
        LinksIn = 2,
        LinksOut = 4,
        NodesIn = 8,
        NodesOut = 16, // 0x00000010
        NotSelf = 32, // 0x00000020
        Ports = 1,
      }
    }
}
