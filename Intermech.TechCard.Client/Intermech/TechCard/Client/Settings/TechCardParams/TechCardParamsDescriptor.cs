// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.TechCardParams.TechCardParamsDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.PropertyEditors;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.TechCard.Client.Settings.TechCardParams;

/// <summary>
/// Временный объект - helper для отображения настроек в PropertyGrid.
/// </summary>
internal class TechCardParamsDescriptor : ICustomTypeDescriptor
{
  /// <summary>
  /// 
  /// </summary>
  private readonly Intermech.Interfaces.TechCard.TechCardParams _techParams;
  /// <summary>
  /// 
  /// </summary>
  [NonSerialized]
  private PropertyDescriptorCollection _propertyDescriptorCollection;

  /// <summary>Добавление общих настроек</summary>
  /// <param name="attributes"></param>
  /// <param name="userRole"></param>
  /// <returns></returns>
  private IList<PropertyDescriptor> CreateCommonProperties(
    Attribute[] attributes,
    ICurrentUserAndRole userRole)
  {
    IList<PropertyDescriptor> commonProperties = (IList<PropertyDescriptor>) new List<PropertyDescriptor>();
    string сategory = "Attribute.TechCard.Client_25";
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._techParams.Common, attributes, true);
    PropertyDescriptor propDesc1 = properties["NavTreeExpandLevel"];
    if (propDesc1 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_21"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_22"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["DefImbaseFilter"];
    if (propDesc2 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (EnumDescConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_24"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_23"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["PasteCommandMode"];
    if (propDesc3 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (EnumDescConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_46"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_45"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["ForceAddObj2Context"];
    if (propDesc4 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc4);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_26"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_27"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["ShowAllForms4Type"];
    if (propDesc5 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc5);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_50"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_49"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc6 = properties["ShowCard4ImbaseEdit"];
    if (propDesc6 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc6);
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_52"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_51"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc7 = properties["DisplayEcoVersionDialog"];
    if (propDesc7 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Common, propDesc7);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_68"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_67"));
      commonProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    return commonProperties;
  }

  /// <summary>Добавление настроек портала</summary>
  /// <param name="attributes"></param>
  /// <param name="userRole"></param>
  /// <returns></returns>
  private IList<PropertyDescriptor> CreatePortalProperties(
    Attribute[] attributes,
    ICurrentUserAndRole userRole)
  {
    IList<PropertyDescriptor> portalProperties = (IList<PropertyDescriptor>) new List<PropertyDescriptor>();
    string сategory = "Attribute.TechCard.TechCardParamsPortal_Category";
    PropertyDescriptor property = TypeDescriptor.GetProperties((object) this._techParams.Portal, attributes, true)["AutoLinkArticleMode"];
    if (property != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.Portal, property);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (FlagsConverter<Intermech.Interfaces.TechCard.TechCardParams.PortalSourceSystemType>)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (FlagsEditor<Intermech.Interfaces.TechCard.TechCardParams.PortalSourceSystemType>), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.TechCardParamsPortal_AutoLinkArticleMode_Info"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.TechCardParamsPortal_AutoLinkArticleMode"));
      portalProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    return portalProperties;
  }

  /// <summary>Добавление настроек маршрута обработки</summary>
  /// <param name="attributes"></param>
  /// <param name="userRole"></param>
  /// <returns></returns>
  private IList<PropertyDescriptor> CreateProcRouteProperties(
    Attribute[] attributes,
    ICurrentUserAndRole userRole)
  {
    IList<PropertyDescriptor> procRouteProperties = (IList<PropertyDescriptor>) new List<PropertyDescriptor>();
    string сategory = "Attribute.TechCard.Client_28";
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._techParams.ProcessRoute, attributes, true);
    PropertyDescriptor propDesc1 = properties["AutoCheckIn"];
    if (propDesc1 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.ProcessRoute, propDesc1);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_30"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_29"));
      procRouteProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["UniqueCehRoute"];
    if (propDesc2 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.ProcessRoute, propDesc2);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_34"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_33"));
      procRouteProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["UniqueBillet"];
    if (propDesc3 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.ProcessRoute, propDesc3);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_36"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_35"));
      procRouteProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["UniqueMemberSborkaZakaz"];
    if (propDesc4 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.ProcessRoute, propDesc4);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_41"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_40"));
      procRouteProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["ForbiddenInMultiArts"];
    if (propDesc5 != null)
    {
      TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor = new TechCardParamsDescriptor.TechParamsPropertyDescriptor((object) this._techParams.ProcessRoute, propDesc5);
      if (userRole == null || !userRole.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory(сategory));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Attribute.TechCard.Client_47"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Attribute.TechCard.Client_48"));
      procRouteProperties.Add((PropertyDescriptor) propertyDescriptor);
    }
    return procRouteProperties;
  }

  /// <summary>Добавление настроек ТП</summary>
  /// <param name="attributes"></param>
  /// <param name="userRole"></param>
  /// <returns></returns>
  private IList<PropertyDescriptor> CreateTechProcProperties(
    Attribute[] attributes,
    ICurrentUserAndRole userRole)
  {
    return (IList<PropertyDescriptor>) new List<PropertyDescriptor>();
  }

  /// <summary>Добавление настроек РМ</summary>
  /// <param name="attributes"></param>
  /// <param name="userRole"></param>
  /// <returns></returns>
  private IList<PropertyDescriptor> CreateCehRouteProperties(
    Attribute[] attributes,
    ICurrentUserAndRole userRole)
  {
    return (IList<PropertyDescriptor>) new List<PropertyDescriptor>();
  }

  /// <summary>Добавление настроек заготовки</summary>
  /// <param name="attributes"></param>
  /// <param name="userRole"></param>
  /// <returns></returns>
  private IList<PropertyDescriptor> CreateZagotProperties(
    Attribute[] attributes,
    ICurrentUserAndRole userRole)
  {
    return (IList<PropertyDescriptor>) new List<PropertyDescriptor>();
  }

  /// <summary>Создать коллекцию свойств для настроек</summary>
  /// <param name="attributes"></param>
  private void CreatePdc(Attribute[] attributes)
  {
    if (this._techParams == null)
      return;
    ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, true);
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    propertyDescriptorList.AddRange((IEnumerable<PropertyDescriptor>) this.CreateCommonProperties(attributes, service));
    propertyDescriptorList.AddRange((IEnumerable<PropertyDescriptor>) this.CreatePortalProperties(attributes, service));
    propertyDescriptorList.AddRange((IEnumerable<PropertyDescriptor>) this.CreateProcRouteProperties(attributes, service));
    propertyDescriptorList.AddRange((IEnumerable<PropertyDescriptor>) this.CreateTechProcProperties(attributes, service));
    propertyDescriptorList.AddRange((IEnumerable<PropertyDescriptor>) this.CreateCehRouteProperties(attributes, service));
    propertyDescriptorList.AddRange((IEnumerable<PropertyDescriptor>) this.CreateZagotProperties(attributes, service));
    this._propertyDescriptorCollection = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  /// <summary>Конструктор.</summary>
  /// <param name="techParams"></param>
  internal TechCardParamsDescriptor(Intermech.Interfaces.TechCard.TechCardParams techParams)
  {
    this._techParams = techParams;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._techParams, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public string GetClassName() => TypeDescriptor.GetClassName((object) this._techParams, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._techParams, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._techParams, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._techParams, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._techParams, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editorBaseType"></param>
  /// <returns></returns>
  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._techParams, editorBaseType, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._techParams, attributes, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._techParams, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._propertyDescriptorCollection == null)
      this.CreatePdc(attributes);
    return this._propertyDescriptorCollection ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public PropertyDescriptorCollection GetProperties() => this.GetProperties(new Attribute[0]);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pd"></param>
  /// <returns></returns>
  public object GetPropertyOwner(PropertyDescriptor pd)
  {
    return pd is TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor ? propertyDescriptor.Owner : (object) this._techParams;
  }

  /// <summary>
  /// 
  /// </summary>
  public void ResetOldValues()
  {
    if (this._propertyDescriptorCollection == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._propertyDescriptorCollection)
    {
      if (propertyDescriptor1 is TechCardParamsDescriptor.TechParamsPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetOldValue((object) this._techParams);
    }
  }

  /// <summary>
  /// Настраиваемый PropertyDescriptor,
  /// который является оболочкой для PropertyDescriptor выдаваемого
  /// TypeDescriptor.GetProperties
  /// </summary>
  internal class TechParamsPropertyDescriptor : PropertyDescriptor
  {
    /// <summary>
    /// 
    /// </summary>
    private string _category;
    /// <summary>
    /// 
    /// </summary>
    private string _displayName;
    /// <summary>
    /// 
    /// </summary>
    private bool? _readOnly;
    /// <summary>
    /// 
    /// </summary>
    private readonly ArrayList _attributeList = new ArrayList();
    /// <summary>
    /// Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties
    /// </summary>
    private readonly PropertyDescriptor _propDesc;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    internal void ResetOldValue(object component)
    {
      this.OldValue = this._propDesc.GetValue(this.Owner ?? component);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="propDesc"></param>
    public TechParamsPropertyDescriptor(object owner, PropertyDescriptor propDesc)
      : base((MemberDescriptor) propDesc)
    {
      this._propDesc = propDesc;
      this.Owner = owner;
      this.OldValue = propDesc?.GetValue(owner);
    }

    /// <summary>
    /// 
    /// </summary>
    public override AttributeCollection Attributes
    {
      get
      {
        Attribute[] attributeArray = new Attribute[this._attributeList.Count + this.AttributeArray.Length];
        this._attributeList.CopyTo((Array) attributeArray);
        for (int count = this._attributeList.Count; count < attributeArray.Length; ++count)
          attributeArray[count] = this.AttributeArray[count - this._attributeList.Count];
        return new AttributeCollection(attributeArray);
      }
    }

    /// <summary>Просто обращается к исходному объекту</summary>
    public override string Category
    {
      get
      {
        if (this._category != null)
          return this._category;
        this._category = this.Attributes[typeof (CategoryAttribute)] is CategoryAttribute attribute ? attribute.Category : this._propDesc.Category;
        return this._category;
      }
    }

    /// <summary>
    /// Получает или устанавливает старое(не измененное) значение для поля.
    /// При изменении в PropertyGrid позволяет выделять жирным
    /// шрифтом измененные значения при помощи метода ShouldSerializeValue.
    /// </summary>
    public object OldValue { get; set; }

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
        if (this._displayName != null)
          return this._displayName;
        this._displayName = this.Attributes[typeof (DisplayNameAttribute)] is DisplayNameAttribute attribute ? attribute.DisplayName : this._propDesc.Name;
        return this._displayName;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override Type ComponentType => this._propDesc.ComponentType;

    /// <summary>
    /// 
    /// </summary>
    public override bool IsReadOnly
    {
      get
      {
        if (this._readOnly.HasValue)
          return this._readOnly.Value;
        this._readOnly = new bool?(this.Attributes[typeof (ReadOnlyAttribute)] is ReadOnlyAttribute attribute ? attribute.IsReadOnly : this._propDesc.IsReadOnly);
        return this._readOnly.Value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override Type PropertyType => this._propDesc.PropertyType;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public override bool CanResetValue(object component) => this._propDesc.CanResetValue(component);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public override object GetValue(object component)
    {
      return this._propDesc.GetValue(this.Owner ?? component);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    public override void ResetValue(object component) => this._propDesc.ResetValue(component);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    public override void SetValue(object component, object value)
    {
      if (value is string)
      {
        TypeConverter converter = this.Converter;
        if ((converter != null ? (converter.CanConvertFrom(value.GetType()) ? 1 : 0) : 0) != 0)
        {
          this._propDesc.SetValue(this.Owner ?? component, this.Converter.ConvertFrom(value));
          return;
        }
      }
      this._propDesc.SetValue(this.Owner ?? component, value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public override bool ShouldSerializeValue(object component)
    {
      return this._propDesc.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute ? !object.Equals(this.GetValue(component), attribute.Value) : !object.Equals(this.GetValue(component), this.OldValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attr"></param>
    public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

    /// <summary>
    /// 
    /// </summary>
    public object Owner { get; }
  }
}
