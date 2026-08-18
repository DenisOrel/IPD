// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DesFormWrapper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
internal class DesFormWrapper : FormWrapper
{
  private DesForm _parent = new DesForm();

  /// <summary>Конструктор.</summary>
  public DesFormWrapper()
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="parent"></param>
  public DesFormWrapper(DesForm parent)
    : base((Form) parent)
  {
    this._parent = parent;
  }

  [CustomDisplayName("Attribute.FormDesigner_123")]
  [CustomCategory("Attribute.FormDesigner_11")]
  public FormLinks Links
  {
    get => this._parent.Links;
    set => this.SetValue(this._pdc[nameof (Links)], (object) value);
  }

  /// <summary>
  /// 
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner.FormEvents.DisplayName")]
  [CustomCategory("Attribute.FormDesigner.FormEvents.CategoryName")]
  [Description("")]
  [TypeConverter(typeof (FormDesignerEventsConverter))]
  [Editor(typeof (FormDesignerEventsEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  public FormDesignerAction[] FormDesignerEvents
  {
    get => this._parent.FormDesignerEvents;
    set => this.SetValue(this._pdc[nameof (FormDesignerEvents)], (object) value);
  }

  /// <summary>
  /// 
  /// </summary>
  [CustomDisplayName("Attribute_AttributeChangingEvents_Name")]
  [CustomCategory("Attribute_Category_Events")]
  [CustomDescription("Attribute_AttributeChangingEvents_Description")]
  [TypeConverter(typeof (FormDesignerEventsConverter))]
  [Editor(typeof (FormDesignerEventsEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  public FormDesignerAction[] AttributeChangingEvents
  {
    get => this._parent.AttributeChangingEvents;
    set => this.SetValue(this._pdc[nameof (AttributeChangingEvents)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает край родительского контейнера, к которому прикрепляется элемент управления.
  /// </summary>
  [Browsable(false)]
  public override DockStyle Dock
  {
    get => base.Dock;
    set => base.Dock = value;
  }

  /// <summary>Размер формы редактирования.</summary>
  [ReadOnly(true)]
  public override Size Size
  {
    get => base.Size;
    set => base.Size = value;
  }

  /// <summary>Возвращает или задает метку раздела в файле справки.</summary>
  [CustomDisplayName("Attribute_FormDesigner_Help_PartLabel")]
  [CustomCategory("Attribute_FormDesigner_Help_CategoryName")]
  [CustomDescription("Attribute_FormDesigner_Help_PartLabel_Description")]
  [DefaultValue("")]
  public string HelpPartLabel
  {
    get => this._parent.HelpPartLabel;
    set => this.SetValue(this._pdc[nameof (HelpPartLabel)], (object) value);
  }

  /// <summary>Возвращает или задает путь к файлу справки.</summary>
  [CustomDisplayName("Attribute_FormDesigner_Help_FileName")]
  [CustomCategory("Attribute_FormDesigner_Help_CategoryName")]
  [CustomDescription("Attribute_FormDesigner_Help_FileName_Description")]
  [TypeConverter(typeof (HelpPathToFileConverter))]
  [Editor(typeof (HelpPathToFileEditor), typeof (UITypeEditor))]
  [DefaultValue("")]
  public string HelpPathToFile
  {
    get => this._parent.HelpPathToFile;
    set => this.SetValue(this._pdc[nameof (HelpPathToFile)], (object) value);
  }
}
