// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.AboutDialog
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Editor;

internal class AboutDialog : Form
{
  private IContainer components;
  private Button okButton;
  private Label labelCopyright;
  private Label labelVersion;
  private Label labelTitle;

  public AboutDialog()
  {
    this.InitializeComponent();
    int year = DateTime.Now.Year;
    try
    {
      year = File.GetCreationTime(this.GetType().Assembly.Location).Year;
    }
    catch
    {
    }
    this.labelVersion.Text = string.Format(LocalizationHolder.rm.GetString("Document.Editor_35"), (object) this.AssemblyVersion);
    this.labelCopyright.Text = string.Format(LocalizationHolder.rm.GetString("Document.Editor_44"), (object) year);
  }

  public string AssemblyTitle
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
      if (customAttributes.Length != 0)
      {
        AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute) customAttributes[0];
        if (assemblyTitleAttribute.Title != "")
          return assemblyTitleAttribute.Title;
      }
      return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
    }
  }

  public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

  public string AssemblyDescription
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
    }
  }

  public string AssemblyProduct
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyProductAttribute) customAttributes[0]).Product;
    }
  }

  public string AssemblyCopyright
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
    }
  }

  public string AssemblyCompany
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) customAttributes[0]).Company;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AboutDialog));
    this.okButton = new Button();
    this.labelCopyright = new Label();
    this.labelVersion = new Label();
    this.labelTitle = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.DialogResult = DialogResult.Cancel;
    this.okButton.Name = "okButton";
    componentResourceManager.ApplyResources((object) this.labelCopyright, "labelCopyright");
    this.labelCopyright.BackColor = Color.Transparent;
    this.labelCopyright.ForeColor = Color.Black;
    this.labelCopyright.Name = "labelCopyright";
    componentResourceManager.ApplyResources((object) this.labelVersion, "labelVersion");
    this.labelVersion.BackColor = Color.Transparent;
    this.labelVersion.ForeColor = Color.Black;
    this.labelVersion.Name = "labelVersion";
    this.labelTitle.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.labelTitle, "labelTitle");
    this.labelTitle.ForeColor = Color.MediumBlue;
    this.labelTitle.Name = "labelTitle";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.labelCopyright);
    this.Controls.Add((Control) this.labelVersion);
    this.Controls.Add((Control) this.labelTitle);
    this.Controls.Add((Control) this.okButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AboutDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
