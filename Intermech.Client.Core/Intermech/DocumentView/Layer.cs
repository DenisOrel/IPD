
// Type: Intermech.DocumentView.Layer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Drawing;


namespace Intermech.DocumentView;

/// <summary>
/// This represents a collection of objects that are to be drawn behind or in front of
/// objects in other layers.
/// </summary>
[Serializable]
public sealed class Layer : IDisposable
{
  private ArrayList _objects = new ArrayList();

  /// <summary>Add an object to this layer.</summary>
  /// <param name="obj"></param>
  /// <remarks>
  /// The <paramref name="obj" /> must not already belong to a different document or view, nor to a group.
  /// If the object already belongs to this layer, nothing happens.
  /// The object's <see cref="T:Intermech.DocumentView.Layer" /> property will be changed to be this layer.
  /// If the object already belonged to a different layer in this same document or view,
  /// the Changed hint will be <see cref="F:Intermech.Map.MapLayer.ChangedObjectLayer" />, otherwise it will be
  /// <see cref="F:Intermech.Map.MapLayer.InsertedObject" />.
  /// </remarks>
  public void Add(IObject obj)
  {
    if (obj == null)
      return;
    if (obj.Layer != null)
    {
      Layer layer = obj.Layer;
      if (layer == this)
        return;
      this.changeLayer(obj, layer, false);
    }
    else
      this.addToLayer(obj, false);
  }

  /// <summary>Clean layer</summary>
  public void Clear()
  {
    foreach (IObject iobject in this._objects)
      iobject.Dispose();
    this._objects.Clear();
  }

  internal IObject[] Objects => (IObject[]) this._objects.ToArray(typeof (IObject));

  private void changeLayer(IObject obj, Layer layer1, bool p)
  {
    obj.Layer._objects.Remove((object) obj);
    this.addToLayer(obj, false);
  }

  internal void addToLayer(IObject obj, bool undoing)
  {
    this._objects.Add((object) obj);
    obj.SetLayer(this, obj, undoing);
  }

  public void Paint(Graphics g, IView view, RectangleF clipRect)
  {
    foreach (IObject iobject in this.Objects)
      iobject.Paint(g, view);
  }

  public void Dispose() => this.Clear();
}
