
// Type: Intermech.Search.GroupAttributesChanging.ObjectBlank
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;


namespace Intermech.Search.GroupAttributesChanging
{
    [Serializable]
    public sealed class ObjectBlank : BaseModel, IRevertibleChangeTracking, IChangeTracking
    {
      private ObjectBlankStatuses _statuses;
      private string _error;

      public ObjectBlank(
        long objectVersionID,
        int objectTypeID,
        bool canCheckOut,
        long checkedOutBy,
        AttributeBlank[] attributes)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
          throw new ArgumentException();
        if (attributes == null)
          throw new ArgumentNullException(nameof (attributes));
        this.ObjectVersionID = objectVersionID;
        this.ObjectTypeID = objectTypeID;
        this.CanCheckOut = canCheckOut;
        this.CheckedOutBy = checkedOutBy;
        this.Attributes = new AttributeBlankCollection(attributes);
        foreach (BaseModel attribute in this.Attributes)
          attribute.PropertyChanged += new PropertyChangedEventHandler(this.Attribute_PropertyChanged);
      }

      public long ObjectVersionID { get; private set; }

      public int ObjectTypeID { get; private set; }

      public bool CanCheckOut { get; private set; }

      public long CheckedOutBy { get; private set; }

      public AttributeBlankCollection Attributes { get; private set; }

      public ObjectBlankStatuses Statuses
      {
        get => this._statuses;
        set
        {
          if (this._statuses == value)
            return;
          this._statuses = value;
          this.OnPropertyChanged<ObjectBlankStatuses>((Expression<Func<ObjectBlankStatuses>>) (() => this.Statuses));
        }
      }

      public string Error
      {
        get => this._error;
        set
        {
          if (!(this._error != value))
            return;
          this._error = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.Error));
        }
      }

      public object GetAttributeValue(int attributeTypeID)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        return this.Attributes[attributeTypeID]?.Value;
      }

      public void SetAttributeValue(int attributeTypeID, object value)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        if (this.Attributes[attributeTypeID] == null)
          throw new ArgumentException();
        this.Attributes[attributeTypeID].Value = value;
      }

      public bool IsNotNullNotReadOnlyAttribute(int attributeTypeID)
      {
        AttributeBlank attributeBlank = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? this.Attributes[attributeTypeID] : throw new ArgumentException();
        return attributeBlank != null && attributeBlank.Value != null && !attributeBlank.IsReadOnly;
      }

      public bool IsAttributeChanged(int attributeTypeID)
      {
        AttributeBlank attributeBlank = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? this.Attributes[attributeTypeID] : throw new ArgumentException();
        return attributeBlank != null && attributeBlank.IsChanged;
      }

      public ObjectBlank Clone()
      {
        return new ObjectBlank(this.ObjectVersionID, this.ObjectTypeID, this.CanCheckOut, this.CheckedOutBy, this.Attributes.Select<AttributeBlank, AttributeBlank>((Func<AttributeBlank, AttributeBlank>) (o => o.Clone())).ToArray<AttributeBlank>())
        {
          _statuses = this._statuses,
          _error = this._error
        };
      }

      public bool IsChanged
      {
        get => this.Attributes.Any<AttributeBlank>((Func<AttributeBlank, bool>) (o => o.IsChanged));
      }

      public void AcceptChanges()
      {
        if (!this.IsChanged)
          return;
        foreach (AttributeBlank attribute in this.Attributes)
          attribute.AcceptChanges();
      }

      public void RejectChanges()
      {
        if (!this.IsChanged)
          return;
        foreach (AttributeBlank attribute in this.Attributes)
          attribute.RejectChanges();
      }

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        foreach (BaseModel attribute in this.Attributes)
          attribute.PropertyChanged += new PropertyChangedEventHandler(this.Attribute_PropertyChanged);
      }

      private void Attribute_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
        this.OnPropertyChanged<AttributeBlankCollection>((Expression<Func<AttributeBlankCollection>>) (() => this.Attributes));
        this.Statuses &= ~ObjectBlankStatuses.Sussess;
        this.Statuses &= ~ObjectBlankStatuses.Error;
        this.Error = (string) null;
      }
    }
}
