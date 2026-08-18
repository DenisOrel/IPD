// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClassWrapperForPropertyGrid
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Search;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for ClassWrapperForPropertyGrid.</summary>
[Serializable]
public class ClassWrapperForPropertyGrid : ICustomTypeDescriptor
{
  private object _baseClass;
  private bool _noCustomTypeDescriptor = true;
  private bool _updateGlobalizedProps;
  [NonSerialized]
  protected PropertyDescriptorCollection _globalizedProps;

  public object BaseClass => this._baseClass;

  public ClassWrapperForPropertyGrid(object baseClass) => this._baseClass = baseClass;

  public ClassWrapperForPropertyGrid(
    object baseClass,
    bool noCustomTypeDescriptor,
    bool updateGlobalizedProps)
    : this(baseClass)
  {
    this._noCustomTypeDescriptor = noCustomTypeDescriptor;
    this._updateGlobalizedProps = updateGlobalizedProps;
  }

  public void ResetOldValues()
  {
    if (this._globalizedProps == null)
      return;
    foreach (ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor globalizedProp in this._globalizedProps)
      globalizedProp.ResetOldValue(this._baseClass);
  }

  System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
  {
    return TypeDescriptor.GetAttributes(this._baseClass, this._noCustomTypeDescriptor);
  }

  string ICustomTypeDescriptor.GetClassName()
  {
    return TypeDescriptor.GetClassName(this._baseClass, this._noCustomTypeDescriptor);
  }

  string ICustomTypeDescriptor.GetComponentName()
  {
    return TypeDescriptor.GetComponentName(this._baseClass, this._noCustomTypeDescriptor);
  }

  TypeConverter ICustomTypeDescriptor.GetConverter()
  {
    return TypeDescriptor.GetConverter(this._baseClass, this._noCustomTypeDescriptor);
  }

  EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent(this._baseClass, this._noCustomTypeDescriptor);
  }

  System.ComponentModel.PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty(this._baseClass, this._noCustomTypeDescriptor);
  }

  object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor(this._baseClass, editorBaseType, this._noCustomTypeDescriptor);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
  {
    return TypeDescriptor.GetEvents(this._baseClass, this._noCustomTypeDescriptor);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents(this._baseClass, attributes, this._noCustomTypeDescriptor);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
  {
    System.ComponentModel.AttributeCollection attributes1 = ((ICustomTypeDescriptor) this).GetAttributes();
    if (attributes1 == null || attributes1.Count <= 0)
      return this.GetProperties(new Attribute[0]);
    Attribute[] attributes2 = new Attribute[attributes1.Count];
    attributes1.CopyTo((Array) attributes2, 0);
    return this.GetProperties(attributes2);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
  {
    return this.GetProperties(attributes);
  }

  protected virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._globalizedProps == null || this._updateGlobalizedProps)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._baseClass, attributes, this._noCustomTypeDescriptor);
      this._globalizedProps = new PropertyDescriptorCollection((System.ComponentModel.PropertyDescriptor[]) null);
      foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor1 in properties)
      {
        if (!this.HasIsAdminAttribute(propertyDescriptor1) || ClassWrapperForPropertyGrid.IsUserRoleAdmin())
        {
          System.ComponentModel.PropertyDescriptor propertyDescriptor2 = properties.Find(this.DependsOn(propertyDescriptor1), true);
          if (propertyDescriptor2 != null)
          {
            bool boolean = Convert.ToBoolean(propertyDescriptor2.GetValue(this._baseClass));
            if (!ClassWrapperForPropertyGrid.IsUserRoleAdmin() && !boolean)
              continue;
          }
          this._globalizedProps.Add((System.ComponentModel.PropertyDescriptor) new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(this, propertyDescriptor1, propertyDescriptor1.GetValue(this._baseClass)));
        }
      }
    }
    return this._globalizedProps;
  }

  object ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
  {
    return this._baseClass;
  }

  private bool HasIsAdminAttribute(System.ComponentModel.PropertyDescriptor propertyDescriptor)
  {
    return propertyDescriptor.Attributes[typeof (IsAdminAttribute)] != null;
  }

  public static bool IsUserRoleAdmin()
  {
    return !(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || service.IsAdmin;
  }

  private string DependsOn(System.ComponentModel.PropertyDescriptor propertyDescriptor)
  {
    return propertyDescriptor.Attributes[typeof (DependsOnAttribute)] is DependsOnAttribute attribute ? attribute.DependsOnName : string.Empty;
  }

  public event ClassWrapperForPropertyGrid.OnGetReadOnly GetReadOnly;

  /// <summary>
  /// Настраиваемый PropertyDescriptor,
  /// который является оболочкой для PropertyDescriptor выдаваемого
  /// TypeDescriptor.GetProperties
  /// </summary>
  public class LocalizedPropertyDescriptor : System.ComponentModel.PropertyDescriptor
  {
    private ArrayList _attributeList = new ArrayList();
    /// <summary>Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties</summary>
    private System.ComponentModel.PropertyDescriptor _propDesc;
    private string _displayName;
    private object _oldValue;
    private ClassWrapperForPropertyGrid _owner;

    public LocalizedPropertyDescriptor(
      ClassWrapperForPropertyGrid owner,
      System.ComponentModel.PropertyDescriptor propDesc,
      object oldValue)
      : base((MemberDescriptor) propDesc)
    {
      this._propDesc = propDesc;
      this._oldValue = oldValue;
      this._owner = owner;
    }

    /// <summary>Конструктор</summary>
    /// <param name="propDesc">Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties,
    /// на основе которого работает класс</param>
    /// <param name="oldValue"></param>
    public LocalizedPropertyDescriptor(System.ComponentModel.PropertyDescriptor propDesc, object oldValue)
      : this((ClassWrapperForPropertyGrid) null, propDesc, oldValue)
    {
    }

    internal void ResetOldValue(object component)
    {
      this._oldValue = this._propDesc.GetValue(component);
    }

    public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

    public override System.ComponentModel.AttributeCollection Attributes
    {
      get
      {
        Attribute[] attributeArray = new Attribute[this._attributeList.Count + this.AttributeArray.Length];
        this._attributeList.CopyTo((Array) attributeArray);
        for (int count = this._attributeList.Count; count < attributeArray.Length; ++count)
          attributeArray[count] = this.AttributeArray[count - this._attributeList.Count];
        return new System.ComponentModel.AttributeCollection(attributeArray);
      }
    }

    /// <summary>Просто обращается к исходному объекту</summary>
    public override string Category => this._propDesc.Category;

    /// <summary>
    /// Получает или устанавливает старое(неизмененное) значение для поля.
    /// При изменении в ProprtyGrid позволяет выделять жирным
    /// шрифтом измененные значения при помощи метода ShouldSerializeValue.
    /// </summary>
    public object OldValue
    {
      get => this._oldValue;
      set => this._oldValue = value;
    }

    /// <summary>
    /// Устанавливает отображаемое имя свойства без использования атрибута.
    /// </summary>
    /// <param name="value">Отображаемое имя</param>
    public void SetDisplayName(string value) => this._displayName = value;

    /// <summary>
    /// Это свойство возвращает название свойства, отображаемое в propertyGrid
    /// </summary>
    public override string DisplayName
    {
      get
      {
        if (this._displayName == null)
          this._displayName = !(this.Attributes[typeof (DisplayNameAttribute)] is DisplayNameAttribute attribute) ? this._propDesc.Name : attribute.DisplayName;
        return this._displayName;
      }
    }

    public override Type ComponentType => this._propDesc.ComponentType;

    public override bool IsReadOnly
    {
      get
      {
        return this._owner != null && this._owner.GetReadOnly != null ? this._owner.GetReadOnly((System.ComponentModel.PropertyDescriptor) this) : this._propDesc.IsReadOnly;
      }
    }

    public override Type PropertyType => this._propDesc.PropertyType;

    public override bool CanResetValue(object component) => this._propDesc.CanResetValue(component);

    public override object GetValue(object component)
    {
      return component is ClassWrapperForPropertyGrid wrapperForPropertyGrid ? this._propDesc.GetValue(wrapperForPropertyGrid._baseClass) : this._propDesc.GetValue(component);
    }

    public override void ResetValue(object component) => this._propDesc.ResetValue(component);

    public override void SetValue(object component, object value)
    {
      ClassWrapperForPropertyGrid wrapperForPropertyGrid = component as ClassWrapperForPropertyGrid;
      if (value is string && this.Converter.CanConvertFrom(value.GetType()))
      {
        if (wrapperForPropertyGrid != null)
          this._propDesc.SetValue(wrapperForPropertyGrid._baseClass, this.Converter.ConvertFrom(value));
        else
          this._propDesc.SetValue(component, this.Converter.ConvertFrom(value));
      }
      else if (wrapperForPropertyGrid != null)
        this._propDesc.SetValue(wrapperForPropertyGrid._baseClass, value);
      else
        this._propDesc.SetValue(component, value);
    }

    public override bool ShouldSerializeValue(object component)
    {
      return this._propDesc.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute ? !object.Equals(this.GetValue(component), attribute.Value) : !object.Equals(this.GetValue(component), this._oldValue);
    }

    public ClassWrapperForPropertyGrid Owner => this._owner;
  }

  public delegate bool OnGetReadOnly(System.ComponentModel.PropertyDescriptor prop);
}
