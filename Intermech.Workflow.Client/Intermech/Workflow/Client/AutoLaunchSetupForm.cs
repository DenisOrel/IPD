// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoLaunchSetupForm
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Intermech.Controls;
using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class AutoLaunchSetupForm : FormEx
{
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private ButtonEdit TypeBox;
  private Label label1;
  private Label label2;
  private ButtonEdit SchemeBox;
  private Label label3;
  private System.Windows.Forms.ComboBox processPriorityCombo;

  public AutoLaunchSetupForm()
  {
    this.InitializeComponent();
    this.TypeBox.Tag = (object) 0;
    this.SchemeBox.Tag = (object) 0;
    this.processPriorityCombo.DisplayMember = "Description";
    this.processPriorityCombo.ValueMember = "Value";
    List<\u003C\u003Ef__AnonymousType0<string, Enum>> list = Enum.GetValues(typeof (ProcessPriority)).Cast<Enum>().Select(value => new
    {
      Description = Attribute.GetCustomAttribute((MemberInfo) value.GetType().GetField(value.ToString()), typeof (CustomDescription)) is CustomDescription customAttribute ? customAttribute.Description : (string) null,
      value = value
    }).OrderBy(item => item.value).ToList();
    int index = list.FindIndex(x => object.Equals((object) x.value, (object) ProcessPriority.Unreal));
    if (index != -1)
      list.RemoveAt(index);
    this.processPriorityCombo.DataSource = (object) list;
    this.processPriorityCombo.SelectedValue = (object) ProcessPriority.Normal;
  }

  private void TypeBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    using (SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString(sc_21615.ssp_workflow_21616()), 4, false))
    {
      if (selectorForm.ShowDialog() == DialogResult.OK)
      {
        if (selectorForm.IDList.Count > 0)
        {
          this.TypeBox.Tag = (object) Convert.ToInt32(selectorForm.IDList[0]);
          this.TypeBox.Text = selectorForm.NameList[0].ToString();
        }
      }
    }
    this.UpdateEnabled();
  }

  private void UpdateEnabled()
  {
    this.OkButton.Enabled = !0.Equals(this.TypeBox.Tag) && !0.Equals(this.SchemeBox.Tag);
  }

  private void LCStepSetupForm_Load(object sender, EventArgs e) => this.UpdateEnabled();

  public AutoLaunchInfo LaunchInfo
  {
    get
    {
      return new AutoLaunchInfo(Convert.ToInt32(this.TypeBox.Tag), Convert.ToInt64(this.SchemeBox.Tag))
      {
        TypeName = this.TypeBox.Text,
        SchemeName = this.SchemeBox.Text,
        ProcessPriority = (ProcessPriority) this.processPriorityCombo.SelectedValue
      };
    }
    set
    {
      this.TypeBox.Tag = (object) value.TypeID;
      this.TypeBox.Text = value.TypeName;
      this.SchemeBox.Tag = (object) value.SchemeID;
      this.SchemeBox.Text = value.SchemeName;
      this.processPriorityCombo.SelectedValue = (object) value.ProcessPriority;
    }
  }

  private void SchemeBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    this.SchemeBox.Tag = (object) wfFunx.BrowseForScheme();
    this.SchemeBox.Text = wfFunx.LastBrowsedSchemeName;
    this.UpdateEnabled();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoLaunchSetupForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.TypeBox = new ButtonEdit();
    this.label1 = new Label();
    this.label2 = new Label();
    this.SchemeBox = new ButtonEdit();
    this.label3 = new Label();
    this.processPriorityCombo = new System.Windows.Forms.ComboBox();
    this.Panel2.SuspendLayout();
    this.TypeBox.Properties.BeginInit();
    this.SchemeBox.Properties.BeginInit();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    componentResourceManager.ApplyResources((object) this.TypeBox, "TypeBox");
    this.TypeBox.Name = "TypeBox";
    this.TypeBox.Properties.BorderStyle = BorderStyles.Flat;
    this.TypeBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 12, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.TypeBox.Properties.ReadOnly = true;
    this.TypeBox.Tag = (object) "";
    this.TypeBox.ButtonClick += new ButtonPressedEventHandler(this.TypeBox_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.SchemeBox, "SchemeBox");
    this.SchemeBox.Name = "SchemeBox";
    this.SchemeBox.Properties.BorderStyle = BorderStyles.Flat;
    this.SchemeBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 12, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.SchemeBox.Properties.ReadOnly = true;
    this.SchemeBox.ButtonClick += new ButtonPressedEventHandler(this.SchemeBox_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.processPriorityCombo, "processPriorityCombo");
    this.processPriorityCombo.FormattingEnabled = true;
    this.processPriorityCombo.Name = "processPriorityCombo";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.processPriorityCombo);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.SchemeBox);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.TypeBox);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoLaunchSetupForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.LCStepSetupForm_Load);
    this.Panel2.ResumeLayout(false);
    this.TypeBox.Properties.EndInit();
    this.SchemeBox.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
