// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.XmlTextEditor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class XmlTextEditor : DataEditorControl
{
  private IContainer components;
  private TextBox tbEdit;

  public XmlTextEditor() => this.InitializeComponent();

  public override void SetData(XmlDocument data, bool readOnly)
  {
    base.SetData(data, readOnly);
    this.tbEdit.TextChanged -= new EventHandler(this.OnXmlChanged);
    this.tbEdit.Text = data.OuterXml;
    this.tbEdit.ReadOnly = readOnly;
    this.tbEdit.TextChanged += new EventHandler(this.OnXmlChanged);
  }

  public override XmlDocument GetData()
  {
    XmlDocument data = new XmlDocument();
    data.LoadXml(this.tbEdit.Text);
    return data;
  }

  private void OnXmlChanged(object sender, EventArgs e) => this.RaiseDataChanged();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (XmlTextEditor));
    this.tbEdit = new TextBox();
    this.SuspendLayout();
    this.tbEdit.AcceptsTab = true;
    componentResourceManager.ApplyResources((object) this.tbEdit, "tbEdit");
    this.tbEdit.Name = "tbEdit";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tbEdit);
    this.Name = nameof (XmlTextEditor);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
