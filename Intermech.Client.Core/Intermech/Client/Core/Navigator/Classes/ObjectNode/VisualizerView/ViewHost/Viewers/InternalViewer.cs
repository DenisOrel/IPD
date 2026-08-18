
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.InternalViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

/// <summary>
/// Внутренний просмотрщик - для просмотра текстовых и web документов
/// Со временем могут добавиться новый форматы
/// </summary>
internal class InternalViewer : UserControl, IViewer
{
  private Control _owner;
  private InternalViewerHost _internalViewerHost;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public InternalViewer()
  {
    this.InitializeComponent();
    this.InitializeDefaultPreviewHandlerHost();
  }

  private void InitializeDefaultPreviewHandlerHost()
  {
    this.PreviewHostsMapping = InternalViewHostMapping.PreviewHostsMapping;
  }

  public List<Tuple<InternalViewerHost, List<string>>> PreviewHostsMapping { get; set; }

  public void Init(Control owner)
  {
    this._owner = owner;
    this._owner.SuspendLayout();
    owner.Controls.Add((Control) this);
    this._owner.Resize += new EventHandler(this._owner_Resize);
    this._owner.ResumeLayout(false);
    this.OnResize_();
  }

  private void _owner_Resize(object sender, EventArgs e) => this.OnResize_();

  private void OnResize_()
  {
    this.Width = this.Parent.Width;
    this.Height = this.Parent.Height;
    this._internalViewerHost?.SetBounds(0, 0, this.Width, this.Height);
  }

  public void Open(FileItem fileItemInfo, System.IServiceProvider serviceProvider)
  {
    this.ClearPreView();
    if (File.Exists(fileItemInfo.FileFullName))
    {
      this._internalViewerHost = InternalHandlerHostFactory.Create(fileItemInfo.FileFullName, this.PreviewHostsMapping);
      if (this._internalViewerHost == null)
        return;
      this._internalViewerHost.Visible = false;
      this._internalViewerHost.Name = "internalViewerHost";
      this._internalViewerHost.Dock = DockStyle.Fill;
      this._internalViewerHost.Open(fileItemInfo.FileFullName);
      this.Controls.Add((Control) this._internalViewerHost);
      this._internalViewerHost.Resize += new EventHandler(this.viewerHost_Resize);
      this._internalViewerHost.Visible = true;
    }
    this.Refresh();
    this.Visible = true;
  }

  public void Close() => this.Visible = false;

  public void Clear()
  {
    this.Visible = false;
    this.ClearPreView();
    this._owner.Resize -= new EventHandler(this._owner_Resize);
    if (this._owner == null || !this._owner.Controls.Contains((Control) this))
      return;
    this._owner.Controls.Remove((Control) this);
  }

  private void ClearPreView()
  {
    this._internalViewerHost = this.Controls["internalViewerHost"] as InternalViewerHost;
    if (this._internalViewerHost == null)
      return;
    this._internalViewerHost.Resize -= new EventHandler(this.viewerHost_Resize);
    this.Controls.Remove((Control) this._internalViewerHost);
    using (this._internalViewerHost)
      this._internalViewerHost = (InternalViewerHost) null;
  }

  private void viewerHost_Resize(object sender, EventArgs e)
  {
    this._internalViewerHost?.SetBounds(0, 0, this.Width, this.Height);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
