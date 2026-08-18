
// Type: Intermech.Search._Attribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.ComponentModel;


namespace Intermech.Search
{
    [Serializable]
    public class _Attribute : ICloneable, INotifyPropertyChanged
    {
      private bool? _isReadOnly;
      private object _value;

      public _Attribute(int typeID)
        : this(typeID, (object) null)
      {
      }

      public _Attribute(int typeID, object value)
      {
        this.TypeID = !AttributeTypeHelper.IsUnknownAttributeTypeID(typeID) ? typeID : throw new ArgumentException();
        this.Value = value;
      }

      public _Attribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
        : this((int) obligatoryObjectAttribute)
      {
      }

      public _Attribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute,
        object value)
        : this((int) obligatoryObjectAttribute, value)
      {
      }

      public int TypeID { get; private set; }

      public bool? IsReadOnly
      {
        get => this._isReadOnly;
        set
        {
          bool? isReadOnly = this._isReadOnly;
          bool? nullable = value;
          if (isReadOnly.GetValueOrDefault() == nullable.GetValueOrDefault() & isReadOnly.HasValue == nullable.HasValue)
            return;
          this._isReadOnly = value;
          this.OnPropertyChanged(nameof (IsReadOnly));
        }
      }

      public object Value
      {
        get => this._value;
        set
        {
          if (this._value == value)
            return;
          this._value = value;
          this.OnPropertyChanged(nameof (Value));
        }
      }

      public void CancelChanges()
      {
      }

      public _Attribute Clone()
      {
        return new _Attribute(this.TypeID)
        {
          IsReadOnly = this.IsReadOnly,
          Value = this.Value == null || !(this.Value is ICloneable) ? this.Value : ((ICloneable) this.Value).Clone()
        };
      }

      object ICloneable.Clone() => (object) this.Clone();

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }
    }
}
