
// Type: Intermech.Search.EditingContexts.EditingContextItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.EditingContexts
{
    [Serializable]
    public sealed class EditingContextItem : IObjectHolder
    {
      private EditingContext _editingContext;

      public EditingContextItem(long objectVersionID)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        this.Object = new _Object();
        this.Object.VersionID = objectVersionID;
      }

      public EditingContextItem(_Object @object)
      {
        this.Object = @object != null && !ObjectHelper.IsUnknownObjectVersionID(@object.VersionID) ? @object : throw new ArgumentException();
      }

      public EditingContext EditingContext
      {
        get => this._editingContext;
        set
        {
          if (this._editingContext == value)
            return;
          EditingContext editingContext = this._editingContext;
          this._editingContext = value;
          editingContext?.Items.Remove(this);
          this._editingContext = value;
          if (this._editingContext == null)
            return;
          this._editingContext.Items.Add(this);
        }
      }

      public bool ReadOnly { get; set; }

      public _Object Object { get; private set; }
    }
}
