
// Type: Intermech.Client.Core.FormDesigner.ScriptForButtonsActionParams
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
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>
/// 
/// </summary>
[TypeConverter(typeof (ScriptForButtonsActionParams.ScriptForButtonsActionTypeConverter))]
[Serializable]
internal class ScriptForButtonsActionParams : IFormDesignerActionParams, ISerializable
{
  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(typeof (Guid), "00000000-0000-0000-0000-000000000000")]
  [CustomDisplayName("ClientCore_Script")]
  [TypeConverter(typeof (Guid2ObjectCaptionConverter))]
  [Editor(typeof (ScriptForButtonsActionParams.EditorSelector), typeof (UITypeEditor))]
  public Guid Script { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(typeof (EnabledScriptForButtons), "Always")]
  [CustomDisplayName("ClientCore_ButtonEnabled")]
  [TypeConverter(typeof (ScriptForButtonsActionParams.ButtonEnabledConverter))]
  [Editor(typeof (ScriptForButtonsActionParams.ButtonEnabledEditor), typeof (UITypeEditor))]
  public EnabledScriptForButtons ButtonEnabled { get; set; }

  /// <summary>Конструктор.</summary>
  public ScriptForButtonsActionParams()
  {
    this.Script = Guid.Empty;
    this.ButtonEnabled = EnabledScriptForButtons.Always;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public ScriptForButtonsActionParams(SerializationInfo info, StreamingContext context)
  {
    this.Script = (Guid) info.GetValue(nameof (Script), typeof (Guid));
    try
    {
      this.ButtonEnabled = (EnabledScriptForButtons) info.GetValue(nameof (ButtonEnabled), typeof (EnabledScriptForButtons));
    }
    catch
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Script", (object) this.Script);
    info.AddValue("ButtonEnabled", (object) this.ButtonEnabled);
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object Component { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public class ButtonEnabledConverter : EnumConverter
  {
    /// <summary>
    /// 
    /// </summary>
    internal BidirectHashtable Hash { get; private set; }

    /// <summary>Конструктор.</summary>
    public ButtonEnabledConverter()
      : base(typeof (EnabledScriptForButtons))
    {
      this.Hash = new BidirectHashtable();
      this.Hash.Add((object) EnabledScriptForButtons.Always, (object) LocalizationHolder.rm.GetString("ClientCore_Always"));
      this.Hash.Add((object) EnabledScriptForButtons.DataChanged, (object) LocalizationHolder.rm.GetString("ClientCore_DataChanged"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return !(value.GetType() == typeof (string)) ? base.ConvertFrom(context, culture, value) : this.Hash[value];
    }

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
      System.Type destinationType)
    {
      return !(destinationType == typeof (string)) ? base.ConvertTo(context, culture, value, destinationType) : this.Hash[value];
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class ScriptForButtonsActionTypeConverter : TypeConverter
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
      System.Type destinationType)
    {
      return !(destinationType == typeof (string)) ? base.ConvertTo(context, culture, value, destinationType) : (object) LocalizationHolder.rm.GetString("Client.Core_144");
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
      return !(value is IFormDesignerActionParams component) ? (PropertyDescriptorCollection) null : TypeDescriptor.GetProperties((object) component, attributes);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class ButtonEnabledEditor : UITypeEditor
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
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
      System.IServiceProvider provider,
      object value)
    {
      ScriptForButtonsActionParams.ButtonEnabledConverter enabledConverter = new ScriptForButtonsActionParams.ButtonEnabledConverter();
      IWindowsFormsEditorService svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      ListBox listBox1 = new ListBox();
      listBox1.BorderStyle = BorderStyle.None;
      listBox1.Height = 32 /*0x20*/;
      ListBox listBox2 = listBox1;
      listBox2.Items.AddRange(enabledConverter.Hash.forward.Values.OfType<object>().ToArray<object>());
      listBox2.Sorted = true;
      listBox2.SelectedItem = enabledConverter.Hash[value];
      EventHandler eventHandler = (EventHandler) ((sender, e) => svc.CloseDropDown());
      listBox2.Click += eventHandler;
      svc.DropDownControl((Control) listBox2);
      listBox2.Click -= eventHandler;
      return enabledConverter.Hash[listBox2.SelectedItem];
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class EditorSelector : UITypeEditor
  {
    private int _objtypeScriptsForButtonsObjID;

    /// <summary>
    /// Тип объектов "Сценарии для кнопок форм редактирования"
    /// </summary>
    private int objtypeScriptsForButtonsObjID
    {
      get
      {
        if (this._objtypeScriptsForButtonsObjID == 0)
          this._objtypeScriptsForButtonsObjID = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid("cadd9962-306c-11d8-b4e9-00304f19f545"), true).ObjectType;
        return this._objtypeScriptsForButtonsObjID;
      }
    }

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
      System.IServiceProvider provider,
      object value)
    {
      IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(this.objtypeScriptsForButtonsObjID);
      long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ClientCore_Script_Select"), string.Empty, rootDescriptor, SelectionOptions.SelectObjects);
      if (numArray != null && numArray.Length != 0)
      {
        QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(numArray[0]);
        value = !objectInfo.Empty ? (object) objectInfo.VersionGuid : throw new ApplicationException(LocalizationHolder.rm.GetString("FormDesigner_Scenario_Info_Null"));
      }
      return value;
    }
  }
}
