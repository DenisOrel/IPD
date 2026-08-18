// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.SearchSchemeCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class SearchSchemeCntrl : UserControl
{
  private long _searchSchemeID;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbSearchSchemeChoosing;
  private Button btnChooseScheme;
  private TextBox tbSchemeName;

  public long SearchSchemeID
  {
    set
    {
      this._searchSchemeID = value;
      this.UpdateControl();
    }
    get => this._searchSchemeID;
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

  public SearchSchemeCntrl() => this.InitializeComponent();

  private void btnChooseScheme_Click(object sender, EventArgs e)
  {
    long fromSelectorWindow = this.GetNewSearchSchemeFromSelectorWindow();
    if (fromSelectorWindow == 0L)
      return;
    this.SearchSchemeID = fromSelectorWindow;
    this.IsChanged = true;
  }

  private void UpdateControl()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.tbSchemeName.Text = sessionKeeper.Session.GetObjectInfo(this._searchSchemeID).Caption;
  }

  private long GetNewSearchSchemeFromSelectorWindow()
  {
    long num = 0;
    Intermech.Navigator.DBObjectTypes.Descriptor rootDescriptor = new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(new Guid("cad00129-306c-11d8-b4e9-00304f19f545")));
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Client_108"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    return objArray == null || objArray.Length == 0 ? num : (objArray[0] as IDBTypedObjectID).ObjectID;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.gbSearchSchemeChoosing = new GroupBox();
    this.btnChooseScheme = new Button();
    this.tbSchemeName = new TextBox();
    this.gbSearchSchemeChoosing.SuspendLayout();
    this.SuspendLayout();
    this.gbSearchSchemeChoosing.Controls.Add((Control) this.btnChooseScheme);
    this.gbSearchSchemeChoosing.Controls.Add((Control) this.tbSchemeName);
    this.gbSearchSchemeChoosing.Dock = DockStyle.Fill;
    this.gbSearchSchemeChoosing.Location = new Point(0, 0);
    this.gbSearchSchemeChoosing.Name = "gbSearchSchemeChoosing";
    this.gbSearchSchemeChoosing.Size = new Size(363, 53);
    this.gbSearchSchemeChoosing.TabIndex = 1;
    this.gbSearchSchemeChoosing.TabStop = false;
    this.gbSearchSchemeChoosing.Text = "Схема поиска";
    this.btnChooseScheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnChooseScheme.Location = new Point(322, 19);
    this.btnChooseScheme.Name = "btnChooseScheme";
    this.btnChooseScheme.Size = new Size(31 /*0x1F*/, 23);
    this.btnChooseScheme.TabIndex = 1;
    this.btnChooseScheme.Text = "...";
    this.btnChooseScheme.UseVisualStyleBackColor = true;
    this.btnChooseScheme.Click += new EventHandler(this.btnChooseScheme_Click);
    this.tbSchemeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSchemeName.Enabled = false;
    this.tbSchemeName.Location = new Point(6, 19);
    this.tbSchemeName.Name = "tbSchemeName";
    this.tbSchemeName.Size = new Size(310, 20);
    this.tbSchemeName.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.gbSearchSchemeChoosing);
    this.Name = nameof (SearchSchemeCntrl);
    this.Size = new Size(363, 53);
    this.gbSearchSchemeChoosing.ResumeLayout(false);
    this.gbSearchSchemeChoosing.PerformLayout();
    this.ResumeLayout(false);
  }
}
