
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Intermech.Search.AttributeChangeHistory;

public sealed class AttributeChangeHistoryForm : Form
{
  private const string AttributeChangeHistoryControlMementoKey = "AttributeChangeHistoryControlMemento";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private AttributeChangeHistoryControl _attributeChangeHistoryControl;

  public AttributeChangeHistoryForm() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long[] ObjectVersionIds
  {
    get => this._attributeChangeHistoryControl.ObjectVersionIds;
    set => this._attributeChangeHistoryControl.ObjectVersionIds = value;
  }

  private void AttributeChangeHistoryForm_Load(object sender, EventArgs e)
  {
    Hashtable hashtable = new Hashtable();
    FormStorage.LoadLayout((Control) this, (IDictionary) hashtable);
    if (!hashtable.ContainsKey((object) "AttributeChangeHistoryControlMemento"))
      return;
    string text = hashtable[(object) "AttributeChangeHistoryControlMemento"] as string;
    if (string.IsNullOrEmpty(text))
      return;
    AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento memento = this.DeserializeAttributeChangeHistoryControlMemento(text);
    if (memento == null)
      return;
    this._attributeChangeHistoryControl.SetMemento(memento);
  }

  private void AttributeChangeHistoryForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Hashtable()
    {
      {
        (object) "AttributeChangeHistoryControlMemento",
        (object) this.SerializeAttributeChangeHistoryControlMemento(this._attributeChangeHistoryControl.GetMemento())
      }
    });
  }

  private string SerializeAttributeChangeHistoryControlMemento(
    AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento memento)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) memento);
      return Convert.ToBase64String(serializationStream.GetBuffer());
    }
  }

  private AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento DeserializeAttributeChangeHistoryControlMemento(
    string text)
  {
    try
    {
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(text)))
        return new BinaryFormatter().Deserialize((Stream) serializationStream) as AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento;
    }
    catch
    {
      return (AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento) null;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._attributeChangeHistoryControl = new AttributeChangeHistoryControl();
    this._attributeChangeHistoryControl.BeginInit();
    this.SuspendLayout();
    this._attributeChangeHistoryControl.Dock = DockStyle.Fill;
    this._attributeChangeHistoryControl.Location = new Point(0, 0);
    this._attributeChangeHistoryControl.Name = "_attributeChangeHistoryControl";
    this._attributeChangeHistoryControl.Size = new Size(934, 461);
    this._attributeChangeHistoryControl.TabIndex = 1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(934, 461);
    this.Controls.Add((Control) this._attributeChangeHistoryControl);
    this.MinimumSize = new Size(550, 500);
    this.Name = nameof (AttributeChangeHistoryForm);
    this.ShowIcon = false;
    this.Text = "История изменения атрибутов";
    this.FormClosed += new FormClosedEventHandler(this.AttributeChangeHistoryForm_FormClosed);
    this.Load += new EventHandler(this.AttributeChangeHistoryForm_Load);
    this._attributeChangeHistoryControl.EndInit();
    this.ResumeLayout(false);
  }
}
