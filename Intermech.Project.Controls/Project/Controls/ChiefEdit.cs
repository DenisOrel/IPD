// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ChiefEdit
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Project.Controls.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class ChiefEdit : UserControl
{
  private readonly ClientProject _project;
  private long _userID;
  private Task _task;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _label10;
  private ButtonEdit _edit;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ButtonEdit Edit
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._edit.CheckInitializedIn<ButtonEdit>((object) this);
    }
  }

  public ChiefEdit() => this.InitializeComponent();

  private void Edit_ButtonClick([CanBeNull] object sender, [NotNull] ButtonPressedEventArgs e)
  {
    IDBTypedObjectID[] dbTypedObjectIdArray = ClientProject.BrowseForResources((Intermech.Project.Project) this._project, true);
    if (dbTypedObjectIdArray == null || dbTypedObjectIdArray.Length == 0)
      return;
    IDBTypedObjectID dbTypedObjectId = dbTypedObjectIdArray[0];
    this._userID = dbTypedObjectId.ObjectID;
    this.Text = dbTypedObjectId.Caption ?? string.Empty;
    this.Inherited = false;
  }

  [NotNull]
  public override string Text
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Edit.Text;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.Edit.Text = value;
  }

  protected long UserID
  {
    get => this._userID;
    set
    {
      if (this._userID == value)
        return;
      this._userID = value;
      long objectID = this._userID;
      if (this._userID == 0L && this._task != null)
      {
        objectID = this._task.InheritedChiefID;
        this.Inherited = true;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.Text = sessionKeeper.Session.GetObject(objectID, false)?.Caption ?? "???";
    }
  }

  private void Edit_DoubleClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.Edit_ButtonClick((object) null, new ButtonPressedEventArgs((EditorButton) null));
  }

  public bool Inherited
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Edit.BackColor == SystemColors.Control;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.Edit.BackColor = value ? SystemColors.Control : SystemColors.Window;
    }
  }

  public void FromTask([NotNull] Task t)
  {
    this._task = t;
    this.UserID = t.ChiefID;
    this.Inherited = t.ChiefIsInherited;
  }

  public void ToTask([NotNull] Task t)
  {
    if (t.ChiefID == this.UserID && this.Inherited == t.ChiefIsInherited)
      return;
    t.ChiefID = this.UserID;
  }

  public bool AllowDel { get; set; }

  private void Edit_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
  {
    if (!this.AllowDel || e.KeyCode != Keys.Delete)
      return;
    this.UserID = 0L;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChiefEdit));
    this._label10 = new Label();
    this._edit = new ButtonEdit();
    this._edit.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._label10, "_label10");
    this._label10.ImageKey = Resources.False;
    this._label10.Name = "_label10";
    componentResourceManager.ApplyResources((object) this._edit, "_edit");
    this._edit.Name = "_edit";
    this._edit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._edit.Properties.ReadOnly = true;
    this._edit.ButtonClick += new ButtonPressedEventHandler(this.Edit_ButtonClick);
    this._edit.DoubleClick += new EventHandler(this.Edit_DoubleClick);
    this._edit.KeyDown += new KeyEventHandler(this.Edit_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._label10);
    this.Controls.Add((Control) this._edit);
    this.Name = nameof (ChiefEdit);
    this._edit.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
