// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocControlTabPagesControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

public class ArticleWithDocControlTabPagesControl : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal TabControl tcMain;
  internal TabPage tpMasterData;
  internal TabPage tpArticle;
  internal TabPage tpDocument;

  public ArticleWithDocControlTabPagesControl() => this.InitializeComponent();

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

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
    this.tcMain = new TabControl();
    this.tpMasterData = new TabPage();
    this.tpArticle = new TabPage();
    this.tpDocument = new TabPage();
    this.tcMain.SuspendLayout();
    this.SuspendLayout();
    this.tcMain.Controls.Add((Control) this.tpMasterData);
    this.tcMain.Controls.Add((Control) this.tpArticle);
    this.tcMain.Controls.Add((Control) this.tpDocument);
    this.tcMain.Dock = DockStyle.Fill;
    this.tcMain.Location = new Point(0, 0);
    this.tcMain.Name = "tcMain";
    this.tcMain.Padding = new Point(10, 7);
    this.tcMain.SelectedIndex = 0;
    this.tcMain.Size = new Size(802, 455);
    this.tcMain.TabIndex = 1;
    this.tpMasterData.AutoScroll = true;
    this.tpMasterData.AutoScrollMinSize = new Size(580, 346);
    this.tpMasterData.Location = new Point(4, 30);
    this.tpMasterData.Name = "tpMasterData";
    this.tpMasterData.Padding = new Padding(3);
    this.tpMasterData.Size = new Size(794, 421);
    this.tpMasterData.TabIndex = 0;
    this.tpMasterData.Text = "Основные данные";
    this.tpMasterData.UseVisualStyleBackColor = true;
    this.tpArticle.AutoScroll = true;
    this.tpArticle.AutoScrollMinSize = new Size(580, 346);
    this.tpArticle.Location = new Point(4, 30);
    this.tpArticle.Name = "tpArticle";
    this.tpArticle.Padding = new Padding(3);
    this.tpArticle.Size = new Size(794, 421);
    this.tpArticle.TabIndex = 1;
    this.tpArticle.Text = "Изделие";
    this.tpArticle.UseVisualStyleBackColor = true;
    this.tpDocument.AutoScroll = true;
    this.tpDocument.AutoScrollMinSize = new Size(580, 346);
    this.tpDocument.Location = new Point(4, 30);
    this.tpDocument.Name = "tpDocument";
    this.tpDocument.Padding = new Padding(3);
    this.tpDocument.Size = new Size(794, 421);
    this.tpDocument.TabIndex = 2;
    this.tpDocument.Text = "Документ";
    this.tpDocument.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.AutoScrollMinSize = new Size(500, 455);
    this.Controls.Add((Control) this.tcMain);
    this.Name = nameof (ArticleWithDocControlTabPagesControl);
    this.Size = new Size(802, 455);
    this.tcMain.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
