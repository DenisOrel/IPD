// Decompiled with JetBrains decompiler
// Type: Intermech.Techcard.Client.FormDesigner.External.CAD.Classes.ExternalCADActionParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.TechCard.Client.FormDesigner.CAD.Classes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Techcard.Client.FormDesigner.External.CAD.Classes;

/// <summary>
/// Implementation of IFormDesignerActionParams for CAD action
///  </summary>
[TypeConverter(typeof (ExternalCADActionParams.ExternalCADActionTypeConverter))]
[Serializable]
internal class ExternalCADActionParams : IFormDesignerActionParams, ISerializable
{
  /// <summary>
  /// 
  /// </summary>
  [NonSerialized]
  private object _component;
  /// <summary>
  /// 
  /// </summary>
  private ExternalCADMethod _method;

  /// <summary>Constructor</summary>
  public ExternalCADActionParams()
  {
  }

  [DefaultValue(ExternalCADMethod.undefined)]
  [CustomDisplayName("Attribute.TechCard.Client_20")]
  [TypeConverter(typeof (EnumDescConverter))]
  public ExternalCADMethod Method
  {
    get => this._method;
    set => this._method = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Method", (object) this._method);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected ExternalCADActionParams(SerializationInfo info, StreamingContext context)
  {
    this._method = (ExternalCADMethod) info.GetValue(nameof (Method), typeof (ExternalCADMethod));
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object Component
  {
    get => this._component;
    set => this._component = value;
  }

  /// <summary>Type converter for property grid</summary>
  private class ExternalCADActionTypeConverter : TypeConverter
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <param name="destinationType"></param>
    /// <returns></returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      return destinationType == typeof (string) ? (object) LocalizationHolder.rm.GetString("TechCard.Client_363") : base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="value"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      PropertyDescriptorCollection properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
      object obj = context.Instance;
      if (obj is ClassWrapperForPropertyGrid)
        obj = (obj as ClassWrapperForPropertyGrid).BaseClass;
      if (obj is IWrapper)
        obj = (obj as IWrapper).BaseClass;
      if (!(obj is AttrButton attrButton))
        return properties;
      attrButton.FormDesignerActionParams.Component = (object) attrButton;
      return TypeDescriptor.GetProperties((object) attrButton.FormDesignerActionParams, attributes);
    }
  }
}
