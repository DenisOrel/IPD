// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapLayerCollection
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public sealed class MapLayerCollection : ICollection, IEnumerable
    {
      public const int InsertedLayer = 801;
      public const int RemovedLayer = 802;
      public const int MovedLayer = 803;
      public const int ChangedDefault = 804;
      private MapLayer _defaultLayer;
      private IMapLayerCollectionContainer _layerCollectionContainer;
      private ArrayList _layers;
      private static readonly RectangleF NullRect = RectangleF.Empty;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="hint"></param>
      /// <param name="subhint"></param>
      /// <param name="obj"></param>
      /// <param name="oldI"></param>
      /// <param name="oldVal"></param>
      /// <param name="oldRect"></param>
      /// <param name="newI"></param>
      /// <param name="newVal"></param>
      /// <param name="newRect"></param>
      private void RaiseChanged(
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
        if (this.LayerCollectionContainer == null)
          return;
        this.LayerCollectionContainer.RaiseChanged(hint, subhint, obj, oldI, oldVal, oldRect, newI, newVal, newRect);
      }

      public MapLayerCollection()
      {
        this._layerCollectionContainer = (IMapLayerCollectionContainer) null;
        this._layers = new ArrayList();
        this._defaultLayer = (MapLayer) null;
      }

      public MapLayer[] CopyArray()
      {
        MapLayer[] array = new MapLayer[this.Count];
        this.CopyTo(array, 0);
        return array;
      }

      public void CopyTo(Array array, int index) => this._layers.CopyTo(array, index);

      public void CopyTo(MapLayer[] array, int index) => this._layers.CopyTo((Array) array, index);

      public MapLayer CreateNewLayerAfter(MapLayer dest)
      {
        if (dest != null && this.IndexOf(dest) < 0)
          throw new ArgumentException("Cannot create a new layer after a layer that is not in this layer collection.");
        MapLayer newlayer = new MapLayer();
        newlayer.init(this.LayerCollectionContainer);
        this.InsertAfter(dest, newlayer);
        return newlayer;
      }

      public MapLayer CreateNewLayerBefore(MapLayer dest)
      {
        if (dest != null && this.IndexOf(dest) < 0)
          throw new ArgumentException("Cannot create a new layer before a layer that is not in this layer collection.");
        MapLayer newlayer = new MapLayer();
        newlayer.init(this.LayerCollectionContainer);
        this.InsertBefore(dest, newlayer);
        return newlayer;
      }

      public MapLayer Find(object identifier)
      {
        if (identifier != null)
        {
          foreach (MapLayer backward in this.Backwards)
          {
            object identifier1 = backward.Identifier;
            if (identifier1 != null && identifier1.Equals(identifier))
              return backward;
          }
        }
        return (MapLayer) null;
      }

      public MapLayerCollectionEnumerator GetEnumerator()
      {
        return new MapLayerCollectionEnumerator(this._layers, true);
      }

      public MapLayerCollectionObjectEnumerator GetObjectEnumerator(bool forward)
      {
        return new MapLayerCollectionObjectEnumerator(this._layers, forward);
      }

      internal int IndexOf(MapLayer layer) => this._layers.IndexOf((object) layer);

      internal void init(IMapLayerCollectionContainer lcc)
      {
        this._layerCollectionContainer = lcc;
        this._defaultLayer = new MapLayer();
        this._defaultLayer.init(this._layerCollectionContainer);
        this._layers.Add((object) this._defaultLayer);
        this._defaultLayer.Identifier = (object) 0;
      }

      internal void InsertAfter(MapLayer dest, MapLayer newlayer)
      {
        if (dest == null)
          dest = this.Top;
        int oldI = this.IndexOf(dest);
        if (oldI < 0 && dest != null)
          return;
        this._layers.Insert(oldI + 1, (object) newlayer);
        this.RaiseChanged(801, 1, (object) newlayer, oldI, (object) dest, MapLayerCollection.NullRect, oldI + 1, (object) newlayer, MapLayerCollection.NullRect);
      }

      internal void InsertBefore(MapLayer dest, MapLayer newlayer)
      {
        if (dest == null)
          dest = this.Bottom;
        int num = this.IndexOf(dest);
        if (num < 0 && dest != null)
          return;
        this._layers.Insert(num, (object) newlayer);
        this.RaiseChanged(801, 0, (object) newlayer, num, (object) dest, MapLayerCollection.NullRect, num, (object) newlayer, MapLayerCollection.NullRect);
      }

      public void InsertDocumentLayerAfter(MapLayer dest, MapLayer doclayer)
      {
        if (this.IndexOf(doclayer) >= 0)
          return;
        if (dest != null && this.IndexOf(dest) < 0)
          throw new ArgumentException("Cannot insert a document layer after a layer that is not in this layer collection.");
        MapView view = this.View;
        if (view == null)
          throw new ArgumentException("Cannot insert a layer into a document layer collection.");
        if (doclayer == null || !doclayer.IsInDocument || view.Document != doclayer.Document)
          throw new ArgumentException("Layer to be inserted into a view layer collection must be a document layer in the view's document.");
        this.InsertAfter(dest, doclayer);
      }

      public void InsertDocumentLayerBefore(MapLayer dest, MapLayer doclayer)
      {
        if (this.IndexOf(doclayer) >= 0)
          return;
        if (dest != null && this.IndexOf(dest) < 0)
          throw new ArgumentException("Cannot insert a document layer before a layer that is not in this layer collection.");
        MapView view = this.View;
        if (view == null)
          throw new ArgumentException("Cannot insert a layer into a document layer collection.");
        if (doclayer == null || !doclayer.IsInDocument || view.Document != doclayer.Document)
          throw new ArgumentException("Layer to be inserted into a view layer collection must be a document layer in the view's document.");
        this.InsertBefore(dest, doclayer);
      }

      public void MoveAfter(MapLayer dest, MapLayer moving)
      {
        if (dest == null)
          dest = this.Top;
        int num1 = dest != moving ? this.IndexOf(moving) : throw new ArgumentException("Cannot move a layer after (on top of) itself");
        if (num1 < 0)
          throw new ArgumentException("MoveAfter layer to be moved must be in the MapLayerCollection");
        int num2 = this.IndexOf(dest);
        if (num2 < 0)
          throw new ArgumentException("MoveAfter destination layer must be in the MapLayerCollection");
        if (num2 > num1)
          --num2;
        if (num2 + 1 == num1)
          return;
        this._layers.RemoveAt(num1);
        this._layers.Insert(num2 + 1, (object) moving);
        this.RaiseChanged(803, 1, (object) moving, num1, (object) dest, MapLayerCollection.NullRect, num2 + 1, (object) dest, MapLayerCollection.NullRect);
      }

      public void MoveBefore(MapLayer dest, MapLayer moving)
      {
        if (dest == null)
          dest = this.Bottom;
        int num1 = dest != moving ? this.IndexOf(moving) : throw new ArgumentException("Cannot move a layer before (behind) itself");
        if (num1 < 0)
          throw new ArgumentException("MoveBefore layer to be moved must be in the MapLayerCollection");
        int num2 = this.IndexOf(dest);
        if (num2 < 0)
          throw new ArgumentException("MoveBefore destination layer must be in the MapLayerCollection");
        if (num2 > num1)
          --num2;
        if (num2 == num1)
          return;
        this._layers.RemoveAt(num1);
        this._layers.Insert(num2, (object) moving);
        this.RaiseChanged(803, 0, (object) moving, num1, (object) dest, MapLayerCollection.NullRect, num2, (object) dest, MapLayerCollection.NullRect);
      }

      public void Remove(MapLayer layer)
      {
        if (layer == null)
          return;
        int index = this.IndexOf(layer);
        if (index < 0)
          return;
        if (layer.LayerCollectionContainer == this.LayerCollectionContainer)
          layer.Clear();
        MapLayer mapLayer = (MapLayer) null;
        foreach (MapLayer layer1 in this._layers)
        {
          if (layer1 != layer && layer1.LayerCollectionContainer == this.LayerCollectionContainer)
          {
            mapLayer = layer1;
            break;
          }
        }
        if (mapLayer == null)
          return;
        MapLayer oldVal = (MapLayer) null;
        if (index + 1 < this._layers.Count)
          oldVal = (MapLayer) this._layers[index + 1];
        this._layers.RemoveAt(index);
        this.RaiseChanged(802, 0, (object) layer, 0, (object) oldVal, MapLayerCollection.NullRect, 0, (object) null, MapLayerCollection.NullRect);
        if (layer != this.Default)
          return;
        this.Default = mapLayer;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new MapLayerCollectionEnumerator(this._layers, true);
      }

      public MapLayerCollectionEnumerator Backwards
      {
        get => new MapLayerCollectionEnumerator(this._layers, false);
      }

      public MapLayer Bottom => this.Count <= 0 ? (MapLayer) null : (MapLayer) this._layers[0];

      public int Count => this._layers.Count;

      public MapLayer Default
      {
        get => this._defaultLayer;
        set
        {
          MapLayer defaultLayer = this._defaultLayer;
          if (defaultLayer == value)
            return;
          if (value == null || value.LayerCollectionContainer != this.LayerCollectionContainer)
            throw new ArgumentException("The new MapLayerCollection.Default layer must belong to the same document or view.");
          this._defaultLayer = value;
          this.RaiseChanged(804, 0, (object) null, 0, (object) defaultLayer, MapLayerCollection.NullRect, 0, (object) value, MapLayerCollection.NullRect);
        }
      }

      public MapDocument Document => this._layerCollectionContainer as MapDocument;

      public bool IsSynchronized => false;

      public IMapLayerCollectionContainer LayerCollectionContainer => this._layerCollectionContainer;

      public object SyncRoot => (object) this.LayerCollectionContainer;

      public MapLayer Top
      {
        get => this.Count <= 0 ? (MapLayer) null : (MapLayer) this._layers[this.Count - 1];
      }

      public MapView View => this._layerCollectionContainer as MapView;
    }
}
