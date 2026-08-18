
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrObjectsList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Расширенный редактор списка ссылок на объекты (функционально AttrListBoxBtn, визуально ObjectsList)
/// </summary>
[Designer(typeof (AttrObjectsListDesigner))]
[RefreshProperties(RefreshProperties.All)]
[ToolboxItem(false)]
public class AttrObjectsList : AttrObjectsListBase
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public AttrObjectsList()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
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
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this._err.SetIconAlignment((Control) this, ErrorIconAlignment.TopLeft);
    this._err.SetIconPadding((Control) this, -16);
    this.Name = nameof (AttrObjectsList);
    this.Size = new Size(214, 126);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
  }
}
