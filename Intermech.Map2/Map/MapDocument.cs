// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapDocument
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;


namespace Intermech.Map
{
    [Serializable]
    public class MapDocument : 
      IMapCollection,
      ICollection,
      IEnumerable,
      IMapLayerCollectionContainer,
      IMapLayerAbilities
    {
      public const int RepaintAll = 100;
      public const int BeginUpdateAllViews = 101;
      public const int EndUpdateAllViews = 102;
      public const int UpdateAllViews = 103;
      public const int ChangedSize = 202;
      public const int ChangedTopLeft = 203;
      public const int ChangedFixedSize = 204;
      public const int ChangedAllowSelect = 207;
      public const int ChangedAllowCopy = 209;
      public const int ChangedAllowResize = 210;
      public const int ChangedAllowReshape = 211;
      public const int ChangedAllowDelete = 212;
      public const int AllArranged = 220;
      public const int ChangedAllowEdit = 215;
      public const int ChangedAllowInsert = 213;
      public const int ChangedAllowLink = 214;
      public const int ChangedAllowMove = 208 /*0xD0*/;
      public const int ChangedDataFormat = 206;
      public const int ChangedLinksLayer = 223;
      public const int ChangedMaintainsPartID = 224 /*0xE0*/;
      public const int ChangedName = 201;
      public const int ChangedPaperColor = 205;
      public const int ChangedUserFlags = 221;
      public const int ChangedUserObject = 222;
      public const int ChangedValidCycle = 225;
      internal const int FirstStateChangedHint = 200;
      public const int LastHint = 10000;
      private bool myAllowCopy;
      private bool myAllowDelete;
      private bool myAllowEdit;
      private bool myAllowInsert;
      private bool myAllowLink;
      private bool myAllowMove;
      private bool myAllowReshape;
      private bool myAllowResize;
      private bool myAllowSelect;
      internal static bool myCaching;
      [NonSerialized]
      private MapChangedEventArgs myChangedEventArgs;
      private static Hashtable myCycleMap;
      private string myDataFormat;
      private SizeF myDocumentSize;
      private PointF myDocumentTopLeft;
      private bool myFixedSize;
      [NonSerialized]
      private bool myIsModified;
      private int myLastPartID;
      private MapLayerCollection myLayers;
      private MapLayer myLinksLayer;
      private bool myMaintainsPartID;
      private string myName;
      private Color myPaperColor;
      [NonSerialized]
      private Hashtable myParts;
      [NonSerialized]
      private MapPositionArray myPositions;
      private MapUndoManager mySerializedUndoManager;
      private bool mySerializesUndoManager;
      [NonSerialized]
      private MapObject mySkippedAvoidable;
      private bool mySkipsUndoManager;
      private bool mySuspendsUpdates;
      private int myUndoEditIndex;
      [NonSerialized]
      private MapUndoManager myUndoManager;
      private int myUserFlags;
      private object myUserObject;
      private MapDocumentValidCycle myValidCycle;
      protected static readonly RectangleF NullRect = RectangleF.Empty;

      public event MapChangedEventHandler Changed;

      static MapDocument()
      {
        MapDocument.myCycleMap = new Hashtable();
        MapDocument.myCaching = true;
      }

      public MapDocument()
      {
        this.myUserFlags = 0;
        this.myUserObject = (object) null;
        this.myLayers = new MapLayerCollection();
        this.myLinksLayer = (MapLayer) null;
        this.myName = "";
        this.myFixedSize = false;
        this.myPaperColor = Color.Empty;
        this.myDataFormat = (string) null;
        this.myAllowSelect = true;
        this.myAllowMove = true;
        this.myAllowCopy = true;
        this.myAllowResize = true;
        this.myAllowReshape = true;
        this.myAllowDelete = true;
        this.myAllowInsert = true;
        this.myAllowLink = true;
        this.myAllowEdit = true;
        this.mySuspendsUpdates = false;
        this.mySkipsUndoManager = false;
        this.mySerializesUndoManager = false;
        this.myChangedEventArgs = (MapChangedEventArgs) null;
        this.myIsModified = false;
        this.myUndoManager = (MapUndoManager) null;
        this.mySerializedUndoManager = (MapUndoManager) null;
        this.myUndoEditIndex = -2;
        this.myValidCycle = MapDocumentValidCycle.All;
        this.myPositions = (MapPositionArray) null;
        this.mySkippedAvoidable = (MapObject) null;
        this.myMaintainsPartID = false;
        this.myLastPartID = -1;
        this.myParts = (Hashtable) null;
        this.myLayers.init((IMapLayerCollectionContainer) this);
        this.myLinksLayer = this.myLayers.Default;
        this.myIsModified = false;
      }

      public virtual bool AbortTransaction()
      {
        MapUndoManager undoManager = this.UndoManager;
        return undoManager != null && undoManager.AbortTransaction();
      }

      public virtual void Add(MapObject obj) => this.DefaultLayer.Add(obj);

      internal void AddAllParts(MapObject obj)
      {
        if (obj is IMapIdentifiablePart p)
          this.AddPart(p);
        if (!(obj is MapGroup mapGroup))
          return;
        foreach (MapObject mapObject in mapGroup.GetEnumerator())
          this.AddAllParts(mapObject);
      }

      private void AddAvoidableRectanglePorts(MapObject obj, ref RectangleF rect)
      {
        if (obj is MapGroup mapGroup)
        {
          foreach (MapObject mapObject in mapGroup)
            this.AddAvoidableRectanglePorts(mapObject, ref rect);
        }
        if (!(obj is MapPort mapPort))
          return;
        link = (MapLink) null;
        MapPortLinkEnumerator enumerator = mapPort.Links.GetEnumerator();
        do
          ;
        while (enumerator.MoveNext() && (!(enumerator.Current is MapLink link) || !link.AvoidsNodes));
        if (link == null)
          return;
        float endSegmentLength = mapPort.EndSegmentLength;
        if (link.FromPort == mapPort)
        {
          float fromLinkDir = mapPort.GetFromLinkDir((IMapLink) link);
          PointF fromLinkPoint = mapPort.GetFromLinkPoint((IMapLink) link);
          if ((double) fromLinkDir == 0.0)
            fromLinkPoint.X += endSegmentLength;
          else if ((double) fromLinkDir == 90.0)
            fromLinkPoint.Y += endSegmentLength;
          else if ((double) fromLinkDir == 180.0)
            fromLinkPoint.X -= endSegmentLength;
          else if ((double) fromLinkDir == 270.0)
            fromLinkPoint.Y -= endSegmentLength;
          rect = MapObject.UnionRect(rect, fromLinkPoint);
        }
        else
        {
          float toLinkDir = mapPort.GetToLinkDir((IMapLink) link);
          PointF toLinkPoint = mapPort.GetToLinkPoint((IMapLink) link);
          if ((double) toLinkDir == 0.0)
            toLinkPoint.X += endSegmentLength;
          else if ((double) toLinkDir == 90.0)
            toLinkPoint.Y += endSegmentLength;
          else if ((double) toLinkDir == 180.0)
            toLinkPoint.X -= endSegmentLength;
          else if ((double) toLinkDir == 270.0)
            toLinkPoint.Y -= endSegmentLength;
          rect = MapObject.UnionRect(rect, toLinkPoint);
        }
      }

      public MapObject AddCopy(MapObject obj, PointF loc)
      {
        PointF location = obj.Location;
        MapCollection coll = new MapCollection();
        coll.Add(obj);
        SizeF offset = MapTool.SubtractPoints(loc, location);
        return this.CopyFromCollection((IMapCollection) coll, false, false, offset, (MapCopyDictionary) null)[(object) obj] as MapObject;
      }

      internal void AddPart(IMapIdentifiablePart p)
      {
        if (this.myParts == null)
          this.myParts = new Hashtable(1000);
        int partId = p.PartID;
        if (partId == -1)
        {
          int key = ++this.myLastPartID;
          while (this.myParts[(object) key] != null)
            key = ++this.myLastPartID;
          this.myParts[(object) key] = (object) p;
          p.PartID = key;
        }
        else
        {
          IMapIdentifiablePart part = (IMapIdentifiablePart) this.myParts[(object) partId];
          if (part == null)
          {
            this.myParts[(object) partId] = (object) p;
          }
          else
          {
            if (part.PartID == partId)
              return;
            this.myParts[(object) partId] = (object) p;
            part.PartID = -1;
            this.AddPart(part);
          }
        }
      }

      private bool alreadyCopied(Hashtable copieds, MapObject o)
      {
        for (MapObject key = o; key != null; key = (MapObject) key.Parent)
        {
          if (copieds.Contains((object) key))
            return true;
        }
        return false;
      }

      public void BeginUpdateViews()
      {
        this.RaiseChanged(101, 0, (object) null, 0, (object) null, MapDocument.NullRect, 0, (object) null, MapDocument.NullRect);
      }

      public virtual bool CanCopyObjects() => this.AllowCopy;

      public virtual bool CanDeleteObjects() => this.AllowDelete;

      public virtual bool CanEditObjects() => this.AllowEdit;

      public virtual bool CanInsertObjects() => this.AllowInsert;

      public virtual bool CanLinkObjects() => this.AllowLink;

      public virtual bool CanMoveObjects() => this.AllowMove;

      public virtual bool CanRedo()
      {
        MapUndoManager undoManager = this.UndoManager;
        return undoManager != null && undoManager.CanRedo();
      }

      public virtual bool CanReshapeObjects() => this.AllowReshape;

      public virtual bool CanResizeObjects() => this.AllowResize;

      public virtual bool CanSelectObjects() => this.AllowSelect;

      public virtual bool CanUndo()
      {
        MapUndoManager undoManager = this.UndoManager;
        return undoManager != null && undoManager.CanUndo();
      }

      public virtual void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.Hint)
        {
          case 201:
            this.Name = (string) e.GetValue(undo);
            break;
          case 202:
            this.Size = e.GetSize(undo);
            break;
          case 203:
            this.TopLeft = e.GetPoint(undo);
            break;
          case 204:
            this.FixedSize = (bool) e.GetValue(undo);
            break;
          case 205:
            this.PaperColor = (Color) e.GetValue(undo);
            break;
          case 206:
            this.DataFormat = (string) e.GetValue(undo);
            break;
          case 207:
            this.AllowSelect = (bool) e.GetValue(undo);
            break;
          case 208 /*0xD0*/:
            this.AllowMove = (bool) e.GetValue(undo);
            break;
          case 209:
            this.AllowCopy = (bool) e.GetValue(undo);
            break;
          case 210:
            this.AllowResize = (bool) e.GetValue(undo);
            break;
          case 211:
            this.AllowReshape = (bool) e.GetValue(undo);
            break;
          case 212:
            this.AllowDelete = (bool) e.GetValue(undo);
            break;
          case 213:
            this.AllowInsert = (bool) e.GetValue(undo);
            break;
          case 214:
            this.AllowLink = (bool) e.GetValue(undo);
            break;
          case 215:
            this.AllowEdit = (bool) e.GetValue(undo);
            break;
          case 216:
          case 217:
          case 218:
          case 219:
            if (e.Hint < 10000)
              break;
            throw new ArgumentOutOfRangeException("Unknown Changed hint");
          case 220:
            ArrayList arrayList = (ArrayList) e.GetValue(undo);
            for (int index = 0; index < arrayList.Count; index += 2)
            {
              MapObject mapObject = (MapObject) arrayList[index];
              if (mapObject is MapLink mapLink)
              {
                PointF[] points = (PointF[]) arrayList[index + 1];
                mapLink.SetPoints(points);
              }
              else
              {
                RectangleF rectangleF = (RectangleF) arrayList[index + 1];
                mapObject.Bounds = rectangleF;
              }
            }
            this.InvalidateViews();
            break;
          case 221:
            this.UserFlags = e.GetInt(undo);
            break;
          case 222:
            this.UserObject = e.GetValue(undo);
            break;
          case 223:
            this.LinksLayer = (MapLayer) e.GetValue(undo);
            break;
          case 224 /*0xE0*/:
            this.MaintainsPartID = (bool) e.GetValue(undo);
            break;
          case 225:
            this.ValidCycle = (MapDocumentValidCycle) e.GetInt(undo);
            break;
          case 801:
            MapLayer mapLayer1 = (MapLayer) e.Object;
            if (!undo)
            {
              MapLayer oldValue = (MapLayer) e.OldValue;
              if (e.OldInt == 1)
              {
                this.Layers.InsertAfter(oldValue, mapLayer1);
                break;
              }
              this.Layers.InsertBefore(oldValue, mapLayer1);
              break;
            }
            this.Layers.Remove(mapLayer1);
            break;
          case 802:
            MapLayer mapLayer2 = (MapLayer) e.Object;
            if (!undo)
            {
              this.Layers.Remove(mapLayer2);
              break;
            }
            MapLayer oldValue1 = (MapLayer) e.OldValue;
            if (oldValue1 != null)
            {
              this.Layers.InsertBefore(oldValue1, mapLayer2);
              break;
            }
            this.Layers.InsertAfter(this.Layers.Top, mapLayer2);
            break;
          case 803:
            MapLayer moving = (MapLayer) e.Object;
            MapLayer oldValue2 = (MapLayer) e.OldValue;
            if (e.OldInt != 1)
            {
              this.Layers.MoveBefore(oldValue2, moving);
              break;
            }
            this.Layers.MoveAfter(oldValue2, moving);
            break;
          case 804:
            this.Layers.Default = (MapLayer) e.GetValue(undo);
            break;
          case 901:
            e.MapObject.ChangeValue(e, undo);
            break;
          case 902:
            MapLayer newValue1 = (MapLayer) e.NewValue;
            MapObject mapObject1 = e.MapObject;
            if (!undo)
            {
              newValue1.addToLayer(mapObject1, true);
              break;
            }
            newValue1.removeFromLayer(mapObject1, true);
            break;
          case 903:
            MapLayer oldValue3 = (MapLayer) e.OldValue;
            MapObject mapObject2 = e.MapObject;
            if (!undo)
            {
              oldValue3.removeFromLayer(mapObject2, true);
              break;
            }
            oldValue3.addToLayer(mapObject2, true);
            break;
          case 904:
            MapObject mapObject3 = e.MapObject;
            MapLayer oldValue4 = (MapLayer) e.OldValue;
            MapLayer newValue2 = (MapLayer) e.NewValue;
            if (!undo)
            {
              newValue2.changeLayer(mapObject3, oldValue4, true);
              break;
            }
            oldValue4.changeLayer(mapObject3, newValue2, true);
            break;
          case 905:
          case 906:
          case 907:
          case 908:
          case 909:
            if (e.Hint < 10000)
              break;
            throw new ArgumentOutOfRangeException("Unknown Changed hint");
          case 910:
            ((MapLayer) e.Object).AllowView = (bool) e.GetValue(undo);
            break;
          case 911:
            ((MapLayer) e.Object).AllowSelect = (bool) e.GetValue(undo);
            break;
          case 912:
            ((MapLayer) e.Object).AllowMove = (bool) e.GetValue(undo);
            break;
          case 913:
            ((MapLayer) e.Object).AllowCopy = (bool) e.GetValue(undo);
            break;
          case 914:
            ((MapLayer) e.Object).AllowResize = (bool) e.GetValue(undo);
            break;
          case 915:
            ((MapLayer) e.Object).AllowReshape = (bool) e.GetValue(undo);
            break;
          case 916:
            ((MapLayer) e.Object).AllowDelete = (bool) e.GetValue(undo);
            break;
          case 917:
            ((MapLayer) e.Object).AllowInsert = (bool) e.GetValue(undo);
            break;
          case 918:
            ((MapLayer) e.Object).AllowLink = (bool) e.GetValue(undo);
            break;
          case 919:
            ((MapLayer) e.Object).AllowEdit = (bool) e.GetValue(undo);
            break;
          case 930:
            ((MapLayer) e.Object).Identifier = e.GetValue(undo);
            break;
          default:
            if (e.Hint < 10000)
              break;
            throw new ArgumentOutOfRangeException("Unknown Changed hint");
        }
      }

      public virtual void Clear()
      {
        this.myParts = (Hashtable) null;
        this.InvalidatePositionArray((MapObject) null);
        foreach (MapLayer layer in this.Layers)
          layer.Clear();
        this.myDocumentSize = new SizeF(0.0f, 0.0f);
        this.myDocumentTopLeft = new PointF(0.0f, 0.0f);
      }

      public RectangleF ComputeBounds()
      {
        return MapDocument.ComputeBounds((IMapCollection) this, (MapView) null);
      }

      /// <summary>Получить минимальный прямоугольник, который включает границы всех объектов в коллекции.</summary>
      /// <param name="coll">коллекция  объектов</param>
      /// <param name="view">Может быть null</param>
      /// <returns>минимальный прямоугольник</returns>
      public static RectangleF ComputeBounds(IMapCollection coll, MapView view)
      {
        bool flag = false;
        float x = 0.0f;
        float y = 0.0f;
        float num1 = 0.0f;
        float num2 = 0.0f;
        foreach (MapObject mapObject in (IEnumerable) coll)
        {
          if (mapObject.CanView())
          {
            RectangleF bounds = mapObject.Bounds;
            RectangleF rectangleF = mapObject.ExpandPaintBounds(bounds, view);
            if (!flag)
            {
              flag = true;
              x = rectangleF.X;
              y = rectangleF.Y;
              num1 = rectangleF.X + rectangleF.Width;
              num2 = rectangleF.Y + rectangleF.Height;
            }
            else
            {
              if ((double) rectangleF.X < (double) x)
                x = rectangleF.X;
              if ((double) rectangleF.Y < (double) y)
                y = rectangleF.Y;
              if ((double) rectangleF.X + (double) rectangleF.Width > (double) num1)
                num1 = rectangleF.X + rectangleF.Width;
              if ((double) rectangleF.Y + (double) rectangleF.Height > (double) num2)
                num2 = rectangleF.Y + rectangleF.Height;
            }
          }
        }
        return flag ? new RectangleF(x, y, num1 - x, num2 - y) : RectangleF.Empty;
      }

      public virtual bool Contains(MapObject obj)
      {
        if (obj != null)
        {
          MapLayer layer = obj.Layer;
          if (layer != null)
            return layer.Document == this;
        }
        return false;
      }

      public MapObject[] CopyArray()
      {
        MapObject[] array = new MapObject[this.Count];
        this.CopyTo(array, 0);
        return array;
      }

      public MapCopyDictionary CopyFromCollection(IMapCollection coll)
      {
        SizeF offset = new SizeF();
        return this.CopyFromCollection(coll, false, false, offset, (MapCopyDictionary) null);
      }

      public virtual MapCopyDictionary CopyFromCollection(
        IMapCollection coll,
        bool copyableOnly,
        bool dragging,
        SizeF offset,
        MapCopyDictionary env)
      {
        if (env == null)
          env = this.CreateCopyDictionary();
        env.SourceCollection = coll;
        Hashtable copieds = new Hashtable();
        MapCollection mapCollection1 = (MapCollection) null;
        MapCollection mapCollection2 = new MapCollection();
        MapCollection mapCollection3 = (MapCollection) null;
        foreach (MapObject mapObject1 in (IEnumerable) coll)
        {
          MapObject mapObject2 = dragging ? mapObject1.DraggingObject : mapObject1;
          if (mapObject2 != null && (!copyableOnly || mapObject2.CanCopy()) && !this.alreadyCopied(copieds, mapObject2))
          {
            if (mapCollection1 != null && mapObject2 is MapGroup)
            {
              foreach (MapObject key in mapCollection1)
              {
                if (key.IsChildOf(mapObject2))
                {
                  copieds.Remove((object) key);
                  if (mapCollection3 == null)
                    mapCollection3 = new MapCollection();
                  mapCollection3.Add(key);
                  mapCollection2.Remove(key);
                }
              }
              if (mapCollection3 != null && !mapCollection3.IsEmpty)
              {
                foreach (MapObject mapObject3 in mapCollection3)
                  mapCollection1.Remove(mapObject3);
                mapCollection3.Clear();
              }
            }
            copieds.Add((object) mapObject2, (object) mapObject2);
            if (!mapObject2.IsTopLevel)
            {
              if (mapCollection1 == null)
                mapCollection1 = new MapCollection();
              mapCollection1.Add(mapObject2);
            }
            mapCollection2.Add(mapObject2);
          }
        }
        PointF pointF = new PointF();
        foreach (MapObject key in mapCollection2)
        {
          if (!(env[(object) key] is MapObject))
          {
            MapObject mapObject = env.Copy(key);
            if (mapObject != null)
            {
              PointF location = mapObject.Location;
              mapObject.Location = new PointF(location.X + offset.Width, location.Y + offset.Height);
              MapLayer layer = key.Layer;
              MapLayer mapLayer = (MapLayer) null;
              if (layer != null)
                mapLayer = layer.Document != this ? this.Layers.Find(layer.Identifier) : layer;
              if (mapLayer == null)
                mapLayer = this.DefaultLayer;
              if (!copyableOnly || mapLayer.CanInsertObjects())
                mapLayer.Add(mapObject);
            }
          }
        }
        foreach (MapObject delayed in env.Delayeds)
        {
          if (delayed != null && env[(object) delayed] is MapObject newobj)
            delayed.CopyObjectDelayed(env, newobj);
        }
        return env;
      }

      public virtual void CopyNewValueForRedo(MapChangedEventArgs e)
      {
        switch (e.Hint)
        {
          case 220:
            ArrayList arrayList = new ArrayList();
            foreach (MapObject mapObject in this)
            {
              arrayList.Add((object) mapObject);
              if (mapObject is MapLink mapLink)
              {
                PointF[] pointFArray = mapLink.CopyPointsArray();
                arrayList.Add((object) pointFArray);
              }
              else
              {
                RectangleF bounds = mapObject.Bounds;
                arrayList.Add((object) bounds);
              }
            }
            e.NewValue = (object) arrayList;
            break;
          case 901:
            e.MapObject.CopyNewValueForRedo(e);
            break;
        }
      }

      public virtual void CopyOldValueForUndo(MapChangedEventArgs e)
      {
        switch (e.Hint)
        {
          case 220:
            if (e.IsBeforeChanging)
              break;
            MapChangedEventArgs beforeChangingEdit = e.FindBeforeChangingEdit();
            if (beforeChangingEdit == null)
              break;
            e.OldValue = beforeChangingEdit.NewValue;
            break;
          case 901:
            if (e.MapObject == null)
              break;
            e.MapObject.CopyOldValueForUndo(e);
            break;
        }
      }

      public void CopyTo(MapObject[] array, int index) => this.CopyTo((Array) array, index);

      public virtual void CopyTo(Array array, int index)
      {
        foreach (MapLayer layer in this.Layers)
        {
          foreach (MapObject mapObject in layer)
            array.SetValue((object) mapObject, index++);
        }
      }

      public virtual MapCopyDictionary CreateCopyDictionary()
      {
        return new MapCopyDictionary()
        {
          DestinationDocument = this
        };
      }

      public void EndUpdateViews()
      {
        this.RaiseChanged(102, 0, (object) null, 0, (object) null, MapDocument.NullRect, 0, (object) null, MapDocument.NullRect);
      }

      public virtual void EnsureUniquePartID()
      {
        if (this.myParts == null)
          this.myParts = new Hashtable(1000);
        ArrayList arrayList = new ArrayList();
        IDictionaryEnumerator enumerator = this.myParts.GetEnumerator();
        while (enumerator.MoveNext())
        {
          DictionaryEntry entry = enumerator.Entry;
          int key = (int) entry.Key;
          if (((IMapIdentifiablePart) entry.Value).PartID != key)
            arrayList.Add((object) entry);
        }
        foreach (DictionaryEntry dictionaryEntry in arrayList)
        {
          int key = (int) dictionaryEntry.Key;
          IMapIdentifiablePart identifiablePart = (IMapIdentifiablePart) dictionaryEntry.Value;
          int partId = identifiablePart.PartID;
          if (this.myParts[(object) partId] == null)
          {
            this.myParts.Remove((object) key);
            this.myParts[(object) partId] = (object) identifiablePart;
          }
          else
            identifiablePart.PartID = key;
        }
        foreach (MapObject mapObject in this)
          this.AddAllParts(mapObject);
      }

      public MapObject FindNode(string s) => this.FindNode(s, false, false);

      public MapObject FindNode(string s, bool prefix, bool ignorecase)
      {
        string str1 = s;
        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        if (ignorecase)
          str1 = str1.ToUpper(currentCulture);
        foreach (MapObject node in this)
        {
          if (node is IMapLabeledNode mapLabeledNode)
          {
            string str2 = mapLabeledNode.Text;
            if (ignorecase)
              str2 = str2.ToUpper(currentCulture);
            if (prefix)
            {
              if (str2.StartsWith(str1))
                return node;
            }
            else if (str2 == str1)
              return node;
          }
        }
        return (MapObject) null;
      }

      public IMapIdentifiablePart FindPart(int id)
      {
        return this.myParts != null ? (IMapIdentifiablePart) this.myParts[(object) id] : (IMapIdentifiablePart) null;
      }

      public virtual bool FinishTransaction(string tname)
      {
        MapUndoManager undoManager = this.UndoManager;
        return undoManager != null && undoManager.FinishTransaction(tname);
      }

      public virtual RectangleF GetAvoidableRectangle(MapObject obj)
      {
        RectangleF bounds = obj.Bounds;
        obj.ExpandPaintBounds(bounds, (MapView) null);
        this.AddAvoidableRectanglePorts(obj, ref bounds);
        return bounds;
      }

      public virtual MapLayerCollectionObjectEnumerator GetEnumerator()
      {
        return this.Layers.GetObjectEnumerator(true);
      }

      internal MapPositionArray GetPositions() => this.GetPositions(true, (MapObject) null);

      internal MapPositionArray GetPositions(bool clearunoccupied, MapObject skip)
      {
        if (this.myPositions == null)
        {
          this.myPositions = new MapPositionArray();
          this.myPositions.CellSize = new SizeF(10f, 10f);
        }
        if (this.myPositions.Invalid)
        {
          RectangleF bounds = this.ComputeBounds();
          MapObject.InflateRect(ref bounds, 100f, 100f);
          this.myPositions.Initialize(bounds);
          foreach (MapObject mapObject in this)
            this.GetPositions1(mapObject, skip);
          this.myPositions.Invalid = false;
        }
        else if (clearunoccupied)
          this.myPositions.SetAllUnoccupied(int.MaxValue);
        return this.myPositions;
      }

      private void GetPositions1(MapObject obj, MapObject skip)
      {
        if (obj == skip)
          return;
        if (obj is MapSubGraph)
        {
          foreach (MapObject mapObject in (MapGroup) obj)
            this.GetPositions1(mapObject, skip);
        }
        else
        {
          if (!this.IsAvoidable(obj))
            return;
          RectangleF avoidableRectangle = this.GetAvoidableRectangle(obj);
          float width = this.myPositions.CellSize.Width;
          float height = this.myPositions.CellSize.Height;
          for (float x = avoidableRectangle.X + width / 2f; (double) x <= (double) avoidableRectangle.X + (double) avoidableRectangle.Width; x += width)
          {
            for (float y = avoidableRectangle.Y + height / 2f; (double) y <= (double) avoidableRectangle.Y + (double) avoidableRectangle.Height; y += height)
              this.myPositions.SetDist(x, y, 0);
          }
        }
      }

      private void InvalidatePositionArray(MapObject obj)
      {
        this.mySkippedAvoidable = (MapObject) null;
        if (this.myPositions == null || this.myPositions.Invalid || obj != null && !this.IsAvoidable(obj))
          return;
        this.myPositions.Invalid = true;
      }

      public void InvalidateViews()
      {
        this.RaiseChanged(100, 0, (object) null, 0, (object) null, MapDocument.NullRect, 0, (object) null, MapDocument.NullRect);
      }

      private void invokeOnChanged(
        int hint,
        int subhint,
        object obj,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect,
        bool before)
      {
        if (this.SuspendsUpdates)
          return;
        MapChangedEventArgs evt = this.myChangedEventArgs;
        if (evt == null)
        {
          evt = new MapChangedEventArgs();
          evt.Document = this;
        }
        evt.IsBeforeChanging = before;
        evt.Hint = hint;
        evt.SubHint = subhint;
        evt.Object = obj;
        evt.OldInt = oldI;
        evt.OldValue = oldVal;
        evt.OldRect = oldRect;
        evt.NewInt = newI;
        evt.NewValue = newVal;
        evt.NewRect = newRect;
        this.myChangedEventArgs = (MapChangedEventArgs) null;
        this.OnChanged(evt);
        this.myChangedEventArgs = evt;
      }

      public virtual bool IsAvoidable(MapObject obj) => obj is IMapNode;

      public bool IsUnoccupied(RectangleF r, MapObject skip)
      {
        if (skip != this.mySkippedAvoidable)
        {
          this.InvalidatePositionArray((MapObject) null);
          this.mySkippedAvoidable = skip;
        }
        return this.GetPositions(false, skip).IsUnoccupied(r.X, r.Y, r.Width, r.Height);
      }

      public static bool MakesDirectedCycle(IMapNode a, IMapNode b)
      {
        if (a == b)
          return true;
        lock (MapDocument.myCycleMap)
        {
          MapDocument.myCycleMap.Clear();
          MapDocument.myCycleMap.Add((object) a, (object) null);
          int num = MapDocument.MakesDirectedCycle1(a, b, MapDocument.myCycleMap) ? 1 : 0;
          MapDocument.myCycleMap.Clear();
          return num != 0;
        }
      }

      private static bool MakesDirectedCycle1(IMapNode a, IMapNode b, Hashtable map)
      {
        if (a == b)
          return true;
        if (!map.Contains((object) b))
        {
          map.Add((object) b, (object) null);
          foreach (IMapNode destination in b.Destinations)
          {
            if (destination != b && MapDocument.MakesDirectedCycle1(a, destination, map))
              return true;
          }
        }
        return false;
      }

      public static bool MakesDirectedCycleFast(IMapNode a, IMapNode b)
      {
        if (a == b)
          return true;
        foreach (IMapNode destination in b.Destinations)
        {
          if (destination != b && MapDocument.MakesDirectedCycleFast(a, destination))
            return true;
        }
        return false;
      }

      public static bool MakesUndirectedCycle(IMapNode a, IMapNode b)
      {
        if (a == b)
          return true;
        lock (MapDocument.myCycleMap)
        {
          MapDocument.myCycleMap.Clear();
          MapDocument.myCycleMap.Add((object) a, (object) null);
          int num = MapDocument.MakesUndirectedCycle1(a, b, MapDocument.myCycleMap) ? 1 : 0;
          MapDocument.myCycleMap.Clear();
          return num != 0;
        }
      }

      private static bool MakesUndirectedCycle1(IMapNode a, IMapNode b, Hashtable map)
      {
        if (a == b)
          return true;
        if (!map.Contains((object) b))
        {
          map.Add((object) b, (object) null);
          foreach (IMapNode node in b.Nodes)
          {
            if (node != b && MapDocument.MakesUndirectedCycle1(a, node, map))
              return true;
          }
        }
        return false;
      }

      public virtual void MergeLayersFrom(MapDocument other)
      {
        foreach (MapLayer layer in other.Layers)
        {
          object identifier = layer.Identifier;
          if (identifier != null && this.Layers.Find(identifier) == null)
            this.Layers.CreateNewLayerAfter(this.Layers.Top).Identifier = identifier;
        }
        MapLayer mapLayer = this.Layers.Find(other.DefaultLayer.Identifier);
        if (mapLayer == null)
          return;
        this.DefaultLayer = mapLayer;
      }

      IEnumerable IMapCollection.Backwards => (IEnumerable) this.Layers.GetObjectEnumerator(false);

      protected virtual void OnChanged(MapChangedEventArgs evt)
      {
        if (this.Changed != null)
          this.Changed((object) this, evt);
        int hint = evt.Hint;
        if (!this.SkipsUndoManager)
        {
          this.UndoManager?.DocumentChanged((object) this, evt);
          if ((hint < 0 || hint >= 200) && (hint != 901 || evt.SubHint != 1000))
            this.IsModified = true;
        }
        switch (hint)
        {
          case 801:
            this.InvalidatePositionArray((MapObject) null);
            break;
          case 802:
            this.InvalidatePositionArray((MapObject) null);
            if (evt.Object != this.LinksLayer)
              break;
            this.LinksLayer = this.DefaultLayer;
            break;
          case 901:
            if (evt.SubHint == 1001)
            {
              MapObject mapObject = evt.MapObject;
              this.UpdateDocumentBounds(mapObject);
              this.InvalidatePositionArray(mapObject);
              if (!mapObject.IsTopLevel)
                break;
              mapObject.Layer?.UpdateCache(mapObject, evt);
              break;
            }
            if (evt.SubHint == 1051)
            {
              if (!this.MaintainsPartID)
                break;
              this.AddAllParts(evt.MapObject);
              break;
            }
            if (evt.SubHint != 1052)
              break;
            this.RemoveAllParts(evt.MapObject);
            break;
          case 902:
            MapObject mapObject1 = evt.MapObject;
            if (this.MaintainsPartID)
              this.AddAllParts(mapObject1);
            this.UpdateDocumentBounds(mapObject1);
            this.InvalidatePositionArray(mapObject1);
            break;
          case 903:
            MapObject mapObject2 = evt.MapObject;
            this.RemoveAllParts(mapObject2);
            this.InvalidatePositionArray(mapObject2);
            break;
        }
      }

      public virtual MapObject PickObject(PointF p, bool selectableOnly)
      {
        if (!selectableOnly || this.CanSelectObjects())
        {
          foreach (MapLayer backward in this.Layers.Backwards)
          {
            MapObject mapObject = backward.PickObject(p, selectableOnly);
            if (mapObject != null)
              return mapObject;
          }
        }
        return (MapObject) null;
      }

      public virtual IMapCollection PickObjects(
        PointF p,
        bool selectableOnly,
        IMapCollection coll,
        int max)
      {
        if (selectableOnly && !this.CanSelectObjects())
          return (IMapCollection) null;
        if (coll == null)
          coll = (IMapCollection) new MapCollection();
        foreach (MapLayer backward in this.Layers.Backwards)
        {
          if (coll.Count >= max)
            return coll;
          backward.PickObjects(p, selectableOnly, coll, max);
        }
        return coll;
      }

      public virtual void RaiseChanged(
        int hint,
        int subhint,
        object obj,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        this.invokeOnChanged(hint, subhint, obj, oldI, oldVal, oldRect, newI, newVal, newRect, false);
      }

      public virtual void RaiseChanging(int hint, int subhint, object obj)
      {
        this.invokeOnChanged(hint, subhint, obj, 0, (object) null, MapDocument.NullRect, 0, (object) null, MapDocument.NullRect, true);
      }

      public virtual void Redo()
      {
        if (!this.CanRedo())
          return;
        this.UndoManager?.Redo();
      }

      public virtual void Remove(MapObject obj)
      {
        if (obj == null)
          return;
        MapLayer layer = obj.Layer;
        if (layer == null)
          return;
        if (layer.Document != this)
          throw new ArgumentException("Cannot remove object that does not belong to this document");
        layer.Remove(obj);
      }

      internal void RemoveAllParts(MapObject obj)
      {
        if (this.myParts == null)
          return;
        if (obj is IMapIdentifiablePart p)
          this.RemovePart(p);
        if (!(obj is MapGroup mapGroup))
          return;
        foreach (MapObject mapObject in mapGroup.GetEnumerator())
          this.RemoveAllParts(mapObject);
      }

      internal void RemovePart(IMapIdentifiablePart p)
      {
        if (this.myParts == null)
          return;
        this.myParts.Remove((object) p.PartID);
      }

      public virtual void SetModifiable(bool b)
      {
        this.AllowMove = b;
        this.AllowResize = b;
        this.AllowReshape = b;
        this.AllowDelete = b;
        this.AllowInsert = b;
        this.AllowLink = b;
        this.AllowEdit = b;
      }

      public virtual bool StartTransaction()
      {
        MapUndoManager undoManager = this.UndoManager;
        return undoManager != null && undoManager.StartTransaction();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.Layers.GetObjectEnumerator(true);

      public virtual void Undo()
      {
        if (!this.CanUndo())
          return;
        this.UndoManager?.Undo();
      }

      public virtual void UpdateDocumentBounds(MapObject obj)
      {
        if (obj == null || this.FixedSize)
          return;
        SizeF size = this.Size;
        PointF topLeft = this.TopLeft;
        RectangleF bounds = obj.Bounds;
        float x = Math.Min(topLeft.X, bounds.X);
        float y = Math.Min(topLeft.Y, bounds.Y);
        float num1 = Math.Max(topLeft.X + size.Width, bounds.X + bounds.Width);
        double num2 = (double) Math.Max(topLeft.Y + size.Height, bounds.Y + bounds.Height);
        float width = num1 - x;
        double num3 = (double) y;
        float height = (float) (num2 - num3);
        if ((double) x < (double) topLeft.X || (double) y < (double) topLeft.Y)
          this.TopLeft = new PointF(x, y);
        if ((double) width <= (double) size.Width && (double) height <= (double) size.Height)
          return;
        this.Size = new SizeF(width, height);
      }

      public void UpdateViews()
      {
        this.RaiseChanged(103, 0, (object) null, 0, (object) null, MapDocument.NullRect, 0, (object) null, MapDocument.NullRect);
      }

      [Category("Behavior")]
      [Description("Whether the user can copy selected objects in this document.")]
      [DefaultValue(true)]
      public virtual bool AllowCopy
      {
        get => this.myAllowCopy;
        set
        {
          bool allowCopy = this.myAllowCopy;
          if (allowCopy == value)
            return;
          this.myAllowCopy = value;
          this.RaiseChanged(209, 0, (object) null, 0, (object) allowCopy, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether the user can delete selected objects in this document.")]
      public virtual bool AllowDelete
      {
        get => this.myAllowDelete;
        set
        {
          bool allowDelete = this.myAllowDelete;
          if (allowDelete == value)
            return;
          this.myAllowDelete = value;
          this.RaiseChanged(212, 0, (object) null, 0, (object) allowDelete, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Category("Behavior")]
      [Description("Whether the user can edit objects in this document.")]
      [DefaultValue(true)]
      public virtual bool AllowEdit
      {
        get => this.myAllowEdit;
        set
        {
          bool allowEdit = this.myAllowEdit;
          if (allowEdit == value)
            return;
          this.myAllowEdit = value;
          this.RaiseChanged(215, 0, (object) null, 0, (object) allowEdit, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("Whether the user can insert objects into this document.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public virtual bool AllowInsert
      {
        get => this.myAllowInsert;
        set
        {
          bool allowInsert = this.myAllowInsert;
          if (allowInsert == value)
            return;
          this.myAllowInsert = value;
          this.RaiseChanged(213, 0, (object) null, 0, (object) allowInsert, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("Whether the user can link ports in this document.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public virtual bool AllowLink
      {
        get => this.myAllowLink;
        set
        {
          bool allowLink = this.myAllowLink;
          if (allowLink == value)
            return;
          this.myAllowLink = value;
          this.RaiseChanged(214, 0, (object) null, 0, (object) allowLink, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("Whether the user can move selected objects in this document.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool AllowMove
      {
        get => this.myAllowMove;
        set
        {
          bool allowMove = this.myAllowMove;
          if (allowMove == value)
            return;
          this.myAllowMove = value;
          this.RaiseChanged(208 /*0xD0*/, 0, (object) null, 0, (object) allowMove, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether the user can reshape resizable objects in this document.")]
      public virtual bool AllowReshape
      {
        get => this.myAllowReshape;
        set
        {
          bool allowReshape = this.myAllowReshape;
          if (allowReshape == value)
            return;
          this.myAllowReshape = value;
          this.RaiseChanged(211, 0, (object) null, 0, (object) allowReshape, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("Whether the user can resize selected objects in this document.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool AllowResize
      {
        get => this.myAllowResize;
        set
        {
          bool allowResize = this.myAllowResize;
          if (allowResize == value)
            return;
          this.myAllowResize = value;
          this.RaiseChanged(210, 0, (object) null, 0, (object) allowResize, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("Whether the user can select objects in this document.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool AllowSelect
      {
        get => this.myAllowSelect;
        set
        {
          bool allowSelect = this.myAllowSelect;
          if (allowSelect == value)
            return;
          this.myAllowSelect = value;
          this.RaiseChanged(207, 0, (object) this, 0, (object) allowSelect, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Browsable(false)]
      public virtual MapLayerCollectionObjectEnumerator Backwards
      {
        get => this.Layers.GetObjectEnumerator(false);
      }

      [Description("The total number of objects in all document layers.")]
      public virtual int Count
      {
        get
        {
          int count = 0;
          foreach (MapLayer layer in this.Layers)
            count += layer.Count;
          return count;
        }
      }

      [Description("The data format name used for the clipboard.")]
      public virtual string DataFormat
      {
        get
        {
          if (this.myDataFormat == null)
            this.myDataFormat = this.GetType().FullName;
          return this.myDataFormat;
        }
        set
        {
          if (this.myDataFormat == null)
            this.myDataFormat = this.GetType().FullName;
          string dataFormat = this.myDataFormat;
          if (value == null || !(dataFormat != value))
            return;
          this.myDataFormat = value;
          this.RaiseChanged(206, 0, (object) null, 0, (object) dataFormat, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("The default layer used when adding objects to the document.")]
      public virtual MapLayer DefaultLayer
      {
        get => this.Layers.Default;
        set => this.Layers.Default = value;
      }

      [DefaultValue(false)]
      [Category("Behavior")]
      [Description("Whether adding or moving objects in the document leaves the document size and top-left unchanged.")]
      public virtual bool FixedSize
      {
        get => this.myFixedSize;
        set
        {
          bool fixedSize = this.myFixedSize;
          if (fixedSize == value)
            return;
          this.myFixedSize = value;
          this.RaiseChanged(204, 0, (object) null, 0, (object) fixedSize, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Browsable(false)]
      public bool IsEmpty => this.Count == 0;

      public bool IsModified
      {
        get
        {
          if (this.UndoManager == null)
            return this.myIsModified;
          if (this.UndoManager.CurrentEdit != null)
            return true;
          return this.myIsModified && this.myUndoEditIndex != this.UndoManager.UndoEditIndex;
        }
        set
        {
          int num1 = this.myIsModified ? 1 : 0;
          this.myIsModified = value;
          if (!value && this.UndoManager != null)
          {
            this.myUndoEditIndex = this.UndoManager.UndoEditIndex;
            this.UndoManager.CurrentEdit = (MapUndoManagerCompoundEdit) null;
          }
          int num2 = value ? 1 : 0;
          if (num1 == num2)
            return;
          this.InvalidateViews();
        }
      }

      [Browsable(false)]
      public virtual bool IsSynchronized => false;

      [Browsable(false)]
      public virtual MapLayerCollection Layers => this.myLayers;

      [Description("The default layer used when adding links to the document.")]
      public virtual MapLayer LinksLayer
      {
        get => this.myLinksLayer;
        set
        {
          MapLayer linksLayer = this.myLinksLayer;
          if (linksLayer == value)
            return;
          this.myLinksLayer = value != null && value.Document == this ? value : throw new ArgumentException("The new value for MapDocument.LinksLayer must belong to this document.");
          this.RaiseChanged(223, 0, (object) null, 0, (object) linksLayer, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [DefaultValue(false)]
      [Description("Whether all the IMapIdentifiableParts in this document have a unique PartID")]
      [Category("Behavior")]
      public bool MaintainsPartID
      {
        get => this.myMaintainsPartID;
        set
        {
          bool maintainsPartId = this.myMaintainsPartID;
          if (maintainsPartId == value)
            return;
          this.myMaintainsPartID = value;
          this.RaiseChanged(224 /*0xE0*/, 0, (object) null, 0, (object) maintainsPartId, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
          if (value)
            this.EnsureUniquePartID();
          else
            this.myParts = (Hashtable) null;
        }
      }

      [Description("The user-visible name for this document.")]
      [DefaultValue("")]
      public virtual string Name
      {
        get => this.myName;
        set
        {
          string name = this.myName;
          if (value == null || !(name != value))
            return;
          this.myName = value;
          this.RaiseChanged(201, 0, (object) null, 0, (object) name, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("The color of the document's background.")]
      public virtual Color PaperColor
      {
        get => this.myPaperColor;
        set
        {
          Color paperColor = this.myPaperColor;
          if (!(paperColor != value))
            return;
          this.myPaperColor = value;
          this.RaiseChanged(205, 0, (object) null, 0, (object) paperColor, MapDocument.NullRect, 0, (object) value, MapDocument.NullRect);
        }
      }

      [Description("Whether the UndoManager is serialized along with the document")]
      public bool SerializesUndoManager
      {
        get => this.mySerializesUndoManager;
        set
        {
          this.mySerializesUndoManager = value;
          if (value)
            this.mySerializedUndoManager = this.myUndoManager;
          else
            this.mySerializedUndoManager = (MapUndoManager) null;
        }
      }

      /// <summary>размеры документа</summary>
      [Description("The size of this document.")]
      public virtual SizeF Size
      {
        get => this.myDocumentSize;
        set
        {
          if ((double) value.Width < 0.0 || (double) value.Height < 0.0)
          {
            if ((double) value.Width == -23.0 && (double) value.Height == -23.0)
              MapDocument.myCaching = true;
            if ((double) value.Width != -23.0 || (double) value.Height != -24.0)
              return;
            MapDocument.myCaching = false;
          }
          else
          {
            SizeF documentSize = this.myDocumentSize;
            if (!(documentSize != value))
              return;
            this.myDocumentSize = value;
            this.RaiseChanged(202, 0, (object) null, 0, (object) null, MapObject.MakeRect(documentSize), 0, (object) null, MapObject.MakeRect(value));
          }
        }
      }

      [Browsable(false)]
      public bool SkipsUndoManager
      {
        get => this.mySkipsUndoManager;
        set => this.mySkipsUndoManager = value;
      }

      [Browsable(false)]
      public bool SuspendsUpdates
      {
        get => this.mySuspendsUpdates;
        set
        {
          this.mySuspendsUpdates = value;
          if (value)
            return;
          this.InvalidatePositionArray((MapObject) null);
          foreach (MapLayer layer in this.Layers)
            layer.ResetCache();
        }
      }

      [Browsable(false)]
      public virtual object SyncRoot => (object) this;

      [Description("The top-left corner position of this document.")]
      public virtual PointF TopLeft
      {
        get => this.myDocumentTopLeft;
        set
        {
          PointF documentTopLeft = this.myDocumentTopLeft;
          if (!(documentTopLeft != value))
            return;
          this.myDocumentTopLeft = value;
          this.RaiseChanged(203, 0, (object) null, 0, (object) null, MapObject.MakeRect(documentTopLeft), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("The UndoManager for this document.")]
      public virtual MapUndoManager UndoManager
      {
        get => this.myUndoManager;
        set
        {
          if (this.myUndoManager == value)
            return;
          if (this.myUndoManager != null)
            this.myUndoManager.RemoveDocument(this);
          this.myUndoManager = value;
          if (this.SerializesUndoManager)
            this.mySerializedUndoManager = value;
          this.myIsModified = false;
          this.myUndoEditIndex = -2;
          if (this.myUndoManager == null)
            return;
          this.myUndoManager.AddDocument(this);
        }
      }

      [Description("An integer value associated with this document.")]
      [DefaultValue(0)]
      public virtual int UserFlags
      {
        get => this.myUserFlags;
        set
        {
          int userFlags = this.myUserFlags;
          if (userFlags == value)
            return;
          this.myUserFlags = value;
          this.RaiseChanged(221, 0, (object) null, userFlags, (object) null, MapDocument.NullRect, value, (object) null, MapDocument.NullRect);
        }
      }

      [DefaultValue(null)]
      [Description("An object associated with this document.")]
      public virtual object UserObject
      {
        get => this.myUserObject;
        set
        {
          object userObject = this.myUserObject;
          if (userObject == value)
            return;
          this.myUserObject = value;
          this.RaiseChanged(222, 0, (object) null, 0, userObject, MapDocument.NullRect, 0, value, MapDocument.NullRect);
        }
      }

      [DefaultValue(0)]
      [Category("Behavior")]
      [Description("Whether a valid link can produce a cycle in the graph.")]
      public virtual MapDocumentValidCycle ValidCycle
      {
        get => this.myValidCycle;
        set
        {
          MapDocumentValidCycle validCycle = this.myValidCycle;
          if (validCycle == value)
            return;
          this.myValidCycle = value;
          this.RaiseChanged(225, 0, (object) null, (int) validCycle, (object) null, MapDocument.NullRect, (int) value, (object) 0, MapDocument.NullRect);
        }
      }
    }
}
