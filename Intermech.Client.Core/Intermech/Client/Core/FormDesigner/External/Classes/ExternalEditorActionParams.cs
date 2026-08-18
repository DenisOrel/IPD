
// Type: Intermech.Client.Core.FormDesigner.External.Classes.ExternalEditorActionParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;


namespace Intermech.Client.Core.FormDesigner.External.Classes;

/// <summary>
/// 
/// </summary>
[TypeConverter(typeof (ActionTypeConverter))]
[Serializable]
internal class ExternalEditorActionParams : IFormDesignerActionParams, ISerializable
{
  public IAttributeEditor AttributeEditor;
  public bool CurrentButtonState;
  public ExternalEditorParams ExternalEditorParams;

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.Client.Core_23")]
  [TypeConverter(typeof (AttributeInfo2TypeNamesConverter))]
  [System.ComponentModel.Editor(typeof (Intermech.Client.Core.FormDesigner.Controls.AttributeEditor), typeof (UITypeEditor))]
  [FieldTypes(new FieldTypes[] {FieldTypes.ftDouble, FieldTypes.ftInteger, FieldTypes.ftMemo, FieldTypes.ftString})]
  [MultiValueModes(new MultiValueModes[] {MultiValueModes.SingleValue, MultiValueModes.SingleValueFromList})]
  public AttributeInfo AttributeInfo { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.Client.Core_24")]
  [TypeConverter(typeof (Guid2ObjectCaptionConverter))]
  [System.ComponentModel.Editor(typeof (ExternalEditorActionParams.EditorSelector), typeof (UITypeEditor))]
  public Guid Editor { get; set; }

  /// <summary>Конструктор.</summary>
  public ExternalEditorActionParams()
  {
    this.AttributeInfo = new AttributeInfo();
    this.Editor = Guid.Empty;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public ExternalEditorActionParams(SerializationInfo info, StreamingContext context)
  {
    this.AttributeInfo = info.GetValue("Attribute", typeof (AttributeInfo)) as AttributeInfo;
    this.Editor = (Guid) info.GetValue(nameof (Editor), typeof (Guid));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Attribute", (object) this.AttributeInfo);
    info.AddValue("Editor", (object) this.Editor);
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object Component { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public class EditorSelector : UITypeEditor
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="provider"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(ExternalEditorConsts.ExternalEditorObjectType));
      long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_174"), string.Empty, rootDescriptor, SelectionOptions.Default);
      if (numArray != null && numArray.Length != 0)
      {
        QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(numArray[0]);
        if (!objectInfo.Empty)
          value = (object) objectInfo.VersionGuid;
      }
      return value;
    }
  }
}
