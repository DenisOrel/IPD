// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.PrintPreviewDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

public class PrintPreviewDlg : Intermech.Controls.PrintPreviewDlg
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public override void Print()
  {
    try
    {
      if (this.Tag is DocumentsComplect)
      {
        DocumentsComplect tag = this.Tag as DocumentsComplect;
        tag.BeforeShowPrintDialog();
        PrintComplectDialog printComplectDialog = new PrintComplectDialog(tag.PrintDocument, tag);
        if (printComplectDialog.ShowDialog() == DialogResult.OK)
          tag.PrintDocument.Print();
        printComplectDialog.Dispose();
      }
      else
      {
        if (new PrintDocumentDialog(this.Document, this.Tag as ImDocumentData).ShowDialog() != DialogResult.OK)
          return;
        this.Document.Print();
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintPreviewDlg));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (PrintPreviewDlg);
    this.ResumeLayout(false);
  }
}
