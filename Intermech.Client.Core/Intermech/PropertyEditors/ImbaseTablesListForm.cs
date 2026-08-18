
// Type: Intermech.PropertyEditors.ImbaseTablesListForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class ImbaseTablesListForm : TabPageForm
{
  private IViewState _viewState = (IViewState) new ImbaseTablesListForm.ViewState();
  private int _attId = -1;
  private System.IServiceProvider _provider = (System.IServiceProvider) new ServiceContainer();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MultipleObjectsView _tablesView;

  public ImbaseTablesListForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.instGuid = aInstGuid;
    (this._provider as ServiceContainer).AddService(typeof (IViewState), (object) this._viewState);
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    int id = (int) (this._folder as AttributeFolder).Id;
    if (this._attId == id)
      return;
    this._attId = id;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      try
      {
        if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
          return;
        List<long> tablesWithAtt = customService.GetTablesWithAtt(session.SessionGUID, this._attId);
        this._tablesView.Initialize((IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, Intermech.Imbase.Consts.ImbaseTableTypeID, "", (IList) tablesWithAtt), this._provider);
        this._tablesView.Activate((IView) null);
      }
      catch
      {
      }
    }
  }

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1011";

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTablesListForm));
    this._tablesView = new MultipleObjectsView();
    this.SuspendLayout();
    this._tablesView.AllowCustomGroupValues = true;
    this._tablesView.AllowEditing = true;
    this._tablesView.Control = (object) this._tablesView;
    this._tablesView.DisableKeyDownEvents = false;
    componentResourceManager.ApplyResources((object) this._tablesView, "_tablesView");
    this._tablesView.EditingMode = false;
    this._tablesView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._tablesView.Name = "_tablesView";
    this._tablesView.ViewContentType = ContentType.Folders | ContentType.NonFolders;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._tablesView);
    this.Name = nameof (ImbaseTablesListForm);
    this.Tag = (object) "   ";
    this.ResumeLayout(false);
  }

  private class ViewState : IViewState
  {
    ViewStateFlags IViewState.ViewState => ViewStateFlags.NodeInViews;
  }
}
