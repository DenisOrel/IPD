
// Type: Intermech.Search.AttributeEditingComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public class AttributeEditingComponent : Component, IKeyUpHandler
{
  private IAttributePropertyDescriberService _attributePropertyDescriberService;
  private Control _control;
  private bool _enabled;
  private AttributeEditingState _state = (AttributeEditingState) UndeterminedAttributeEditingState.Instance;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private UniversalAttributeEditor _editor;

  public AttributeEditingComponent()
  {
    this.InitializeComponent();
    this._editor.KeyUpHandler = (IKeyUpHandler) this;
  }

  public AttributeEditingComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
    this._editor.KeyUpHandler = (IKeyUpHandler) this;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IAttributePropertyDescriberService AttributePropertyDescriberService
  {
    get => this._attributePropertyDescriberService;
    set
    {
      if (this._attributePropertyDescriberService == value)
        return;
      this._attributePropertyDescriberService = value;
      this._editor.AttributePropertyDescriberService = this._attributePropertyDescriberService;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Control Control
  {
    get => this._control;
    set
    {
      if (this._control == value)
        return;
      this.Detach();
      this._control = value;
      this.Attach();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal UniversalAttributeEditor Editor
  {
    get => this._editor != null ? this._editor : throw new Exception();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Enabled
  {
    get => this._enabled;
    set
    {
      if (this._enabled == value)
        return;
      this._enabled = value;
      if (this._enabled)
        this.Attach();
      else
        this.Detach();
    }
  }

  public bool IsEditorVisible
  {
    get => this._editor != null && this._editor.Parent != null && this._editor.Visible;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INotificationService NotificationService { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeColumn NodeColumn { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INodeID NodeID { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Rectangle Bounds { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AttributeValues[] AttributesValues { get; set; }

  protected bool IsUndetermined => this._state is UndeterminedAttributeEditingState;

  private void Attach()
  {
    if (this.Control == null || !this.Enabled)
      return;
    this.Editor.Visible = false;
    this.Control.Controls.Add((Control) this.Editor);
    this.DoAttach();
  }

  protected virtual void DoAttach()
  {
  }

  private void Detach()
  {
    if (this.Control == null)
      return;
    this.HideEditor();
    this.DoDetach();
    this.Control.Controls.Remove((Control) this.Editor);
  }

  protected virtual void DoDetach()
  {
  }

  internal void InitializeEditor() => this._state.InitializeEditor(this);

  protected void AcceptChanges() => this._state.AcceptChanges(this);

  protected void ShowEditor()
  {
    this.Editor.Visible = true;
    UniversalAttributeEditor editor = this.Editor;
    Rectangle rectangle;
    if (this.Editor.MinimumSize.Height > this.Bounds.Height)
    {
      Rectangle bounds = this.Bounds;
      Point location = bounds.Location;
      bounds = this.Bounds;
      Size size = new Size(bounds.Width, this.Editor.MinimumSize.Height);
      rectangle = new Rectangle(location, size);
    }
    else
      rectangle = this.Bounds;
    editor.Bounds = rectangle;
    this.Editor.BringToFront();
    this.Editor.LocationChanged += new EventHandler(this.Editor_LocationChanged);
    this.Editor.Leave += new EventHandler(this.Editor_Leave);
    this.DoShowEditor();
    this.Editor.Focus();
    this.Editor.SetFocus();
  }

  protected virtual void DoShowEditor()
  {
  }

  protected void HideEditor()
  {
    this.Editor.LocationChanged -= new EventHandler(this.Editor_LocationChanged);
    this.Editor.Leave -= new EventHandler(this.Editor_Leave);
    this.Editor.Visible = false;
    this.DoHideEditor();
    this.SetUndetermined();
  }

  protected virtual void DoHideEditor()
  {
  }

  internal void SetState(AttributeEditingState state)
  {
    this._state = state != null ? state : throw new ArgumentNullException(nameof (state));
  }

  internal void SetUndetermined()
  {
    this.SetState((AttributeEditingState) UndeterminedAttributeEditingState.Instance);
  }

  public virtual int[] GetPresentAttributes() => new int[0];

  public void HandleKeyUp(Keys keyCode)
  {
    switch (keyCode)
    {
      case Keys.Return:
        if (!this.Editor.IsValid)
          break;
        this.AcceptChanges();
        Application.DoEvents();
        this.HideEditor();
        break;
      case Keys.Escape:
        this.HideEditor();
        break;
    }
  }

  private void Editor_Leave(object sender, EventArgs e) => this.HideEditor();

  private void Editor_LocationChanged(object sender, EventArgs e) => this.HideEditor();

  private void DisposeUniversalAttributeEditor()
  {
    if (this._editor == null)
      return;
    this._editor.Dispose();
    this._editor = (UniversalAttributeEditor) null;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.DisposeUniversalAttributeEditor();
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
    this._editor = new UniversalAttributeEditor();
    this._editor.Location = new Point(0, 0);
    this._editor.Name = "_editor";
    this._editor.Size = new Size(271, 364);
    this._editor.TabIndex = 0;
  }
}
