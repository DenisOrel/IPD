
// Type: Intermech.Search.GroupAttributesChanging.AttributeBlank
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Linq.Expressions;


namespace Intermech.Search.GroupAttributesChanging
{
    [Serializable]
    public sealed class AttributeBlank : BaseModel, IRevertibleChangeTracking, IChangeTracking
    {
      private object _value;
      private object _valueBackup;

      public AttributeBlank(int attributeTypeID, bool isReadOnly, bool isEditable, object value)
      {
        this.AttributeTypeID = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? attributeTypeID : throw new ArgumentException();
        this.IsReadOnly = isReadOnly;
        this.IsEditable = isEditable;
        this._value = value;
        this.SetValueBackup(value);
      }

      public int AttributeTypeID { get; private set; }

      public bool IsReadOnly { get; private set; }

      public bool IsEditable { get; private set; }

      public object Value
      {
        get => this._value;
        set
        {
          if (this.IsReadOnly)
            throw new InvalidOperationException();
          if (object.Equals(this._value, value))
            return;
          this._value = value;
          this.IsChanged = true;
          this.OnPropertyChanged<object>((Expression<Func<object>>) (() => this.Value));
        }
      }

      public object ValueBackup => this._valueBackup;

      public bool IsChanged { get; private set; }

      public void RejectChanges()
      {
        if (this.IsReadOnly || !this.IsChanged)
          return;
        this._value = this._valueBackup;
        this.SetValueBackup(this._value);
        this.IsChanged = false;
        this.OnPropertyChanged<object>((Expression<Func<object>>) (() => this.Value));
      }

      public void AcceptChanges()
      {
        if (this.IsReadOnly || !this.IsChanged)
          return;
        this.SetValueBackup(this.Value);
        this.IsChanged = false;
      }

      public AttributeBlank Clone()
      {
        return new AttributeBlank(this.AttributeTypeID, this.IsReadOnly, this.IsEditable, this.Value)
        {
          IsChanged = this.IsChanged,
          _valueBackup = this._valueBackup
        };
      }

      private void SetValueBackup(object value)
      {
        this._valueBackup = value is ICloneable ? ((ICloneable) value).Clone() : value;
      }
    }
}
