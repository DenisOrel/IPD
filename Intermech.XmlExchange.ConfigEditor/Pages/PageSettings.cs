// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.PageSettings
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal class PageSettings : UserControl, IPageConfigEditor
{
  private object _selectNode;
  private bool _readOnly;
  private IContainer components;
  private PropertyGrid PGSettings;

  public event EventHandler ModifyData;

  public event EventHandler UpdatePages;

  public PageSettings() => this.InitializeComponent();

  public void InitializeCustomComponent()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this.PGSettings.Font = font;
  }

  public bool EditData { get; private set; }

  public string PageName => "Настройки";

  public void LoadData(object selectNode, bool readOnly)
  {
    this._selectNode = selectNode;
    this._readOnly = readOnly;
    this.EditData = false;
    this.PGSettings.SelectedObject = (object) null;
    this.PGSettings.SelectedObject = (object) PageSettingEditors.GetPropertyCollection(this._selectNode, this._readOnly);
  }

  public void SaveData(bool save, bool refresh)
  {
    if (!this.EditData)
      return;
    if (this.PGSettings.SelectedObject != null && this.PGSettings.SelectedObject is IConfigItemProperties selectedObject)
    {
      if (save)
        selectedObject.SaveSettings();
      else
        selectedObject.ResetSettings();
    }
    this.EditData = false;
    EventHandler updatePages = this.UpdatePages;
    if (updatePages == null)
      return;
    updatePages((object) this, (EventArgs) null);
  }

  public void UpdateView() => this.PGSettings.Refresh();

  private void PGSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.EditData = true;
    EventHandler modifyData = this.ModifyData;
    if (modifyData == null)
      return;
    modifyData((object) this, (EventArgs) null);
  }

  private void ResizeDescriptionArea(ref PropertyGrid grid, string[] descriptionLines)
  {
    PropertyInfo property1 = grid.GetType().GetProperty("Controls");
    if (property1 == (PropertyInfo) null)
      return;
    foreach (Control control in (ArrangedElementCollection) property1.GetValue((object) grid, (object[]) null))
    {
      System.Type type = control.GetType();
      if (type.Name == "DocComment")
      {
        PropertyInfo property2 = type.GetProperty("Lines");
        if (!(property2 == (PropertyInfo) null))
        {
          Font font = control.Font;
          int num = 1;
          foreach (string descriptionLine in descriptionLines)
          {
            Size size = TextRenderer.MeasureText(descriptionLine, font);
            num += (int) Math.Ceiling((double) size.Width / (double) control.Width);
          }
          object obj = property2.GetValue((object) control);
          int result;
          if (obj != null && int.TryParse(obj.ToString(), out result))
            num = num > result ? num : result;
          property2.SetValue((object) control, (object) num, (object[]) null);
          if (type.BaseType != (System.Type) null)
          {
            FieldInfo field = type.BaseType.GetField("userSized", BindingFlags.Instance | BindingFlags.NonPublic);
            if (!(field == (FieldInfo) null))
              field.SetValue((object) control, (object) true);
          }
        }
      }
    }
  }

  private void PGSettings_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
  {
    if (e.NewSelection.PropertyDescriptor == null)
      return;
    foreach (Attribute attribute in e.NewSelection.PropertyDescriptor.Attributes)
    {
      if (attribute.GetType() == typeof (DescriptionAttribute))
      {
        string description = attribute is DescriptionAttribute descriptionAttribute ? descriptionAttribute.Description : (string) null;
        if (!string.IsNullOrEmpty(description))
        {
          string[] descriptionLines = description.Split(Environment.NewLine.ToCharArray());
          if (description.Split(Environment.NewLine.ToCharArray()).Length > 1)
            this.ResizeDescriptionArea(ref this.PGSettings, descriptionLines);
          this.PGSettings.HelpVisible = true;
        }
      }
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.PGSettings = new PropertyGrid();
    this.SuspendLayout();
    this.PGSettings.CommandsForeColor = Color.Coral;
    this.PGSettings.Dock = DockStyle.Fill;
    this.PGSettings.Location = new Point(0, 0);
    this.PGSettings.Name = "PGSettings";
    this.PGSettings.PropertySort = PropertySort.Categorized;
    this.PGSettings.RightToLeft = RightToLeft.No;
    this.PGSettings.Size = new Size(721, 366);
    this.PGSettings.TabIndex = 1;
    this.PGSettings.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PGSettings_PropertyValueChanged);
    this.PGSettings.SelectedGridItemChanged += new SelectedGridItemChangedEventHandler(this.PGSettings_SelectedGridItemChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.PGSettings);
    this.Name = nameof (PageSettings);
    this.Size = new Size(721, 366);
    this.ResumeLayout(false);
  }
}
