// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ScriptChoosingCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ScriptChoosingCntrl : UserControl
{
  private long _scriptID;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbScriptChoosing;
  private Button btnChooseScript;
  private TextBox tbChoosedScript;
  private Button btnClearScript;

  public long ScriptID
  {
    set
    {
      this._scriptID = value;
      this.UpdateControl();
    }
    get => this._scriptID;
  }

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public ScriptChoosingCntrl() => this.InitializeComponent();

  private void btnChooseScript_Click(object sender, EventArgs e)
  {
    IDBTypedObjectID scriptObject = ScriptChoosingCntrl.GetScriptObject();
    if (scriptObject == null)
      return;
    this.ScriptID = scriptObject.ObjectID;
    this.tbChoosedScript.Text = scriptObject.Caption;
    this.IsChanged = true;
  }

  private void btnClearScript_Click(object sender, EventArgs e)
  {
    this.ScriptID = 0L;
    this.tbChoosedScript.Text = string.Empty;
    this.IsChanged = true;
  }

  private void UpdateControl()
  {
    if (this.ScriptID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.tbChoosedScript.Text = sessionKeeper.Session.GetObjectInfo(this.ScriptID).Caption;
  }

  private static IDBTypedObjectID GetScriptObject()
  {
    object[] objArray = SelectionWindow.Select("Choose script", (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(new Guid("cad00003-306c-11d8-b4e9-00304f19f545"))), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    return objArray == null || objArray.Length == 0 ? (IDBTypedObjectID) null : objArray[0] as IDBTypedObjectID;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ScriptChoosingCntrl));
    this.gbScriptChoosing = new GroupBox();
    this.btnClearScript = new Button();
    this.btnChooseScript = new Button();
    this.tbChoosedScript = new TextBox();
    this.gbScriptChoosing.SuspendLayout();
    this.SuspendLayout();
    this.gbScriptChoosing.Controls.Add((Control) this.btnClearScript);
    this.gbScriptChoosing.Controls.Add((Control) this.btnChooseScript);
    this.gbScriptChoosing.Controls.Add((Control) this.tbChoosedScript);
    this.gbScriptChoosing.Dock = DockStyle.Fill;
    this.gbScriptChoosing.Location = new Point(0, 0);
    this.gbScriptChoosing.Name = "gbScriptChoosing";
    this.gbScriptChoosing.Size = new Size(366, 53);
    this.gbScriptChoosing.TabIndex = 0;
    this.gbScriptChoosing.TabStop = false;
    this.gbScriptChoosing.Text = "Скрипт";
    this.btnClearScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClearScript.Image = (Image) componentResourceManager.GetObject("btnClearScript.Image");
    this.btnClearScript.Location = new Point(329, 16 /*0x10*/);
    this.btnClearScript.Name = "btnClearScript";
    this.btnClearScript.Size = new Size(31 /*0x1F*/, 23);
    this.btnClearScript.TabIndex = 2;
    this.btnClearScript.UseVisualStyleBackColor = true;
    this.btnClearScript.Click += new EventHandler(this.btnClearScript_Click);
    this.btnChooseScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnChooseScript.Location = new Point(292, 17);
    this.btnChooseScript.Name = "btnChooseScript";
    this.btnChooseScript.Size = new Size(31 /*0x1F*/, 23);
    this.btnChooseScript.TabIndex = 1;
    this.btnChooseScript.Text = "...";
    this.btnChooseScript.UseVisualStyleBackColor = true;
    this.btnChooseScript.Click += new EventHandler(this.btnChooseScript_Click);
    this.tbChoosedScript.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbChoosedScript.Enabled = false;
    this.tbChoosedScript.Location = new Point(6, 19);
    this.tbChoosedScript.Name = "tbChoosedScript";
    this.tbChoosedScript.Size = new Size(280, 20);
    this.tbChoosedScript.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gbScriptChoosing);
    this.Name = nameof (ScriptChoosingCntrl);
    this.Size = new Size(366, 53);
    this.gbScriptChoosing.ResumeLayout(false);
    this.gbScriptChoosing.PerformLayout();
    this.ResumeLayout(false);
  }
}
