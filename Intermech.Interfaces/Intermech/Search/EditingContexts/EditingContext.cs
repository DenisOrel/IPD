
// Type: Intermech.Search.EditingContexts.EditingContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.EditingContexts
{
    [Serializable]
    public sealed class EditingContext : IObjectHolder
    {
      public EditingContext(long objectVersionID)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        this.Object = new _Object();
        this.Object.VersionID = objectVersionID;
        this.Items = new EditingContextItemCollection(this);
      }

      public EditingContext(_Object @object)
      {
        this.Object = @object != null && !ObjectHelper.IsUnknownObjectVersionID(@object.VersionID) ? @object : throw new ArgumentException();
        this.Items = new EditingContextItemCollection(this);
      }

      public long LinkedEditingContextID
      {
        get
        {
          return !(this.Object.Attributes.GetAttributeValue(EditingContextConstants.LinkedEditingContextIDAttributeTypeID) is long attributeValue) ? 0L : attributeValue;
        }
      }

      public EditingContextItemCollection Items { get; private set; }

      public _Object Object { get; private set; }
    }
}
