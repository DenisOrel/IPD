
// Type: Intermech.Search.CompositionPart
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search
{
    /// <summary>Часть состава</summary>
    [Serializable]
    /// <summary>Конструктор</summary>
    /// <param name="relation">Связь</param>
    /// <param name="part">Объект</param>
    public sealed class CompositionPart(Relation relation, _Object part) : RelationObjectBase(relation, part)
    {
      private _Object _parent;

      public CompositionPart()
        : this(new Relation(), new _Object())
      {
      }

      public _Object Parent
      {
        get => this._parent;
        set
        {
          if (this._parent == value)
            return;
          _Object parent = this._parent;
          this._parent = value;
          parent?.Composition.Remove(this);
          if (this._parent == null)
            return;
          this._parent.Composition.Add(this);
        }
      }
    }
}
