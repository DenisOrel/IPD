// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.SignsSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class SignsSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private List<string> _alienSettingsActs = new List<string>();
  private WorkflowNode _activityNode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SignsForType signsForType;
  private Panel ApproveCheckSpacer;
  private GroupBox ApproveCheckGB;
  private Label UseAlienHint;
  private Label ApproveCheckHint;
  private Button ChooseApprovesButton;
  private TextBox ApprovesBox;
  private CheckBox UseAlienSettingsCheckBox;
  private CheckBox ApproveCheckOnlyCheckBox;
  private CheckBox graphForTypeCheckBox;
  private SignsGraphsSettingControl signsGraphsSettingControl;
  private EnhToolTip ToolTip;

  public SignsSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (this._readOnly)
        ControlFuncs.SetControlsReadOnly((Control) this, value);
      this.signsGraphsSettingControl.ReadOnly = value;
    }
  }

  public bool LoadSignsSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    WorkflowNode activityNode,
    IUserSession activitySession)
  {
    this._settings = settings;
    bool flag = false;
    this._activityNode = activityNode;
    this.graphForTypeCheckBox.Checked = settings.ExtProperties.ReadBool("GraphForType");
    IDBAttribute attributeById1 = activityObject.GetAttributeByID(wfConsts.AttrGraphForTypeID);
    if (attributeById1 != null)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (SignsDataItemModel));
      string s = attributeById1.Value.ToString();
      if (!string.IsNullOrEmpty(s))
      {
        using (TextReader textReader = (TextReader) new StringReader(s))
        {
          SignsDataItemModel signsDataItemModel = xmlSerializer.Deserialize(textReader) as SignsDataItemModel;
          foreach (SignsDataItem node in (Collection<SignsDataItem>) signsDataItemModel.Nodes)
          {
            node.SetChild();
            this.signsForType.SignsDataItemModel.Nodes.Add(node);
          }
          this.signsForType.SignsDataItemModel.PersonalSigns = signsDataItemModel.PersonalSigns;
        }
      }
    }
    IDBAttribute attributeById2 = activityObject.GetAttributeByID(wfConsts.AttrRequiredSignsID);
    if (attributeById2 != null)
    {
      this.signsGraphsSettingControl.LoadSignsGraphsSettingControl(settings, attributeById2, activityObject, activitySession);
      this.ApproveCheckOnlyCheckBox.Checked = settings.ExtProperties.ReadBool("TestOnly");
      this.AlienSettingsActs = settings.ExtProperties.Read("SettingsActs");
      this.ApproveCheckOnlyCheckBox_CheckedChanged((object) null, (EventArgs) null);
    }
    else
      flag = true;
    return flag;
  }

  private void ChooseApprovesButton_Click(object sender, EventArgs e)
  {
    using (CheckListForm checkListForm = new CheckListForm())
    {
      if (this._activityNode != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          MapLayerCollectionObjectEnumerator enumerator = this._activityNode.View.Doc.GetEnumerator();
          while (enumerator.MoveNext())
          {
            if (enumerator.Current is WorkflowNode current && current.ActivityKind == ActivityKind.Approve && current != this._activityNode)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(current.ActivityID);
              if (!objectInfo.Empty)
              {
                Guid versionGuid = objectInfo.VersionGuid;
                int index = checkListForm.ListBox.Items.Add((object) new GuidInfo(versionGuid, current.Text));
                if (this._alienSettingsActs.Contains(versionGuid.ToString()))
                  checkListForm.ListBox.SetItemChecked(index, true);
              }
            }
          }
        }
      }
      if (checkListForm.ShowDialog() != DialogResult.OK)
        return;
      this._alienSettingsActs.Clear();
      string str = "";
      foreach (GuidInfo checkedItem in checkListForm.ListBox.CheckedItems)
      {
        this._alienSettingsActs.Add(checkedItem.Guid.ToString());
        if (str != "")
          str += ", ";
        str += checkedItem.Name;
      }
      this.ApprovesBox.Text = str;
    }
  }

  private void graphForTypeCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.signsGraphsSettingControl.Visible = !this.graphForTypeCheckBox.Checked;
    this.signsForType.Visible = this.graphForTypeCheckBox.Checked;
    this.OnResize((EventArgs) null);
    Size size1 = this.Size;
    int width1 = size1.Width;
    size1 = this.Size;
    int height1 = size1.Height + 1;
    this.Size = new Size(width1, height1);
    Size size2 = this.Size;
    int width2 = size2.Width;
    size2 = this.Size;
    int height2 = size2.Height - 1;
    this.Size = new Size(width2, height2);
  }

  /// <summary>
  /// Гуиды версий объектов через запятую, из которых берем настройки
  /// </summary>
  public string AlienSettingsActs
  {
    get
    {
      return this.UseAlienSettingsCheckBox.Checked ? string.Join(",", (IEnumerable<string>) this._alienSettingsActs) : "";
    }
    set
    {
      if (!this.ApproveCheckOnlyCheckBox.Checked)
        return;
      List<string> stringList;
      if (!(value != ""))
        stringList = new List<string>();
      else
        stringList = new List<string>((IEnumerable<string>) value.Split(','));
      this._alienSettingsActs = stringList;
      this.UseAlienSettingsCheckBox.Checked = this._alienSettingsActs.Count > 0;
      if (!this.UseAlienSettingsCheckBox.Checked)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string str1 = "";
        foreach (string alienSettingsAct in this._alienSettingsActs)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid(alienSettingsAct));
          string str2 = objectInfo.Empty ? "??" : objectInfo.Caption;
          if (str1 != "")
            str1 += ", ";
          str1 += str2;
        }
        this.ApprovesBox.Text = str1;
      }
    }
  }

  private void ApproveCheckOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.UseAlienSettingsCheckBox.Enabled = !this.ReadOnly && this.ApproveCheckOnlyCheckBox.Checked;
    if (this.ApproveCheckOnlyCheckBox.Checked || !this.UseAlienSettingsCheckBox.Checked)
      return;
    this.UseAlienSettingsCheckBox.Checked = false;
  }

  private void UseAlienSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.graphForTypeCheckBox.Checked)
      this.signsGraphsSettingControl.Visible = !this.UseAlienSettingsCheckBox.Checked;
    this.graphForTypeCheckBox.Visible = !this.UseAlienSettingsCheckBox.Checked;
    this.signsForType.Visible = !this.UseAlienSettingsCheckBox.Checked && this.graphForTypeCheckBox.Checked;
    this.ApproveCheckGB.Dock = this.UseAlienSettingsCheckBox.Checked ? DockStyle.Top : DockStyle.Bottom;
    this.ApprovesBox.Visible = this.UseAlienSettingsCheckBox.Checked;
    this.ChooseApprovesButton.Visible = this.UseAlienSettingsCheckBox.Checked;
    this.OnResize((EventArgs) null);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    if (!this.UseAlienSettingsCheckBox.Checked)
    {
      this.ApproveCheckGB.Height = this.ApprovesBox.Top + 3;
      this.signsGraphsSettingControl.ResizeControl(this.ApproveCheckGB.Height);
    }
    else
    {
      this.ApproveCheckGB.Height = this.Height / 2;
      this.ApprovesBox.Height = this.ApproveCheckGB.Height - 50;
    }
  }

  public void ResizeControl() => this.OnResize((EventArgs) null);

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this.graphForTypeCheckBox.Checked)
    {
      if (!this.UseAlienSettingsCheckBox.Checked)
      {
        this._settings.ExtProperties.WriteBool("GraphForType", true, ExtPropertiesFlag.Approve);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          new XmlSerializer(typeof (SignsDataItemModel)).Serialize((Stream) memoryStream, (object) this.signsForType.SignsDataItemModel);
          memoryStream.Position = 0L;
          using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
          {
            activityToSave.Attributes.AddAttribute(wfConsts.AttrGraphForTypeID, false, new object[1]
            {
              (object) streamReader.ReadToEnd()
            });
            modified = true;
          }
        }
      }
      else
        this._settings.ExtProperties.WriteBool("GraphForType", false, ExtPropertiesFlag.Approve);
    }
    else
    {
      this._settings.ExtProperties.WriteBool("GraphForType", false, ExtPropertiesFlag.Approve);
      modified = this.signsGraphsSettingControl.Save(activityToSave, modified);
    }
    this._settings.ExtProperties.WriteBool("TestOnly", this.ApproveCheckOnlyCheckBox.Checked, ExtPropertiesFlag.Approve);
    this._settings.ExtProperties.Write("SettingsActs", this.AlienSettingsActs, ExtPropertiesFlag.Approve);
    return modified;
  }

  public int RanksPanelHeight
  {
    get => this.signsGraphsSettingControl.RanksPanelHeight;
    set => this.signsGraphsSettingControl.RanksPanelHeight = value;
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
    this.ApproveCheckSpacer = new Panel();
    this.ApproveCheckGB = new GroupBox();
    this.UseAlienHint = new Label();
    this.ApproveCheckHint = new Label();
    this.ChooseApprovesButton = new Button();
    this.ApprovesBox = new TextBox();
    this.UseAlienSettingsCheckBox = new CheckBox();
    this.ApproveCheckOnlyCheckBox = new CheckBox();
    this.graphForTypeCheckBox = new CheckBox();
    this.signsGraphsSettingControl = new SignsGraphsSettingControl();
    this.signsForType = new SignsForType();
    this.ToolTip = new EnhToolTip(this.components);
    this.ApproveCheckGB.SuspendLayout();
    this.SuspendLayout();
    this.ApproveCheckSpacer.Dock = DockStyle.Bottom;
    this.ApproveCheckSpacer.Location = new Point(10, 484);
    this.ApproveCheckSpacer.Name = "ApproveCheckSpacer";
    this.ApproveCheckSpacer.Size = new Size(724, 6);
    this.ApproveCheckSpacer.TabIndex = 13;
    this.ApproveCheckGB.Controls.Add((Control) this.UseAlienHint);
    this.ApproveCheckGB.Controls.Add((Control) this.ApproveCheckHint);
    this.ApproveCheckGB.Controls.Add((Control) this.ChooseApprovesButton);
    this.ApproveCheckGB.Controls.Add((Control) this.ApprovesBox);
    this.ApproveCheckGB.Controls.Add((Control) this.UseAlienSettingsCheckBox);
    this.ApproveCheckGB.Controls.Add((Control) this.ApproveCheckOnlyCheckBox);
    this.ApproveCheckGB.Dock = DockStyle.Bottom;
    this.ApproveCheckGB.Location = new Point(10, 490);
    this.ApproveCheckGB.Name = "ApproveCheckGB";
    this.ApproveCheckGB.Size = new Size(724, 110);
    this.ApproveCheckGB.TabIndex = 14;
    this.ApproveCheckGB.TabStop = false;
    this.UseAlienHint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.UseAlienHint.AutoSize = true;
    this.UseAlienHint.BackColor = SystemColors.Info;
    this.UseAlienHint.ImeMode = ImeMode.NoControl;
    this.UseAlienHint.Location = new Point(688, 27);
    this.UseAlienHint.Name = "UseAlienHint";
    this.UseAlienHint.Size = new Size(24, 17);
    this.UseAlienHint.TabIndex = 5;
    this.UseAlienHint.Text = "[?]";
    this.ToolTip.SetToolTip((Control) this.UseAlienHint, "Брать настройки для проверки подписей из указанных действий.");
    this.ApproveCheckHint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.ApproveCheckHint.AutoSize = true;
    this.ApproveCheckHint.BackColor = SystemColors.Info;
    this.ApproveCheckHint.ImeMode = ImeMode.NoControl;
    this.ApproveCheckHint.Location = new Point(688, 0);
    this.ApproveCheckHint.Name = "ApproveCheckHint";
    this.ApproveCheckHint.Size = new Size(24, 17);
    this.ApproveCheckHint.TabIndex = 4;
    this.ApproveCheckHint.Text = "[?]";
    this.ToolTip.SetToolTip((Control) this.ApproveCheckHint, "Режим автоматического выполнения без рассылки исполнителям, при котором действие только проверяет набор необходимых подписей.");
    this.ChooseApprovesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.ChooseApprovesButton.ImeMode = ImeMode.NoControl;
    this.ChooseApprovesButton.Location = new Point(621, 53);
    this.ChooseApprovesButton.Name = "ChooseApprovesButton";
    this.ChooseApprovesButton.Size = new Size(90, 27);
    this.ChooseApprovesButton.TabIndex = 3;
    this.ChooseApprovesButton.Text = "Выбрать...";
    this.ChooseApprovesButton.UseVisualStyleBackColor = true;
    this.ChooseApprovesButton.Visible = false;
    this.ChooseApprovesButton.Click += new EventHandler(this.ChooseApprovesButton_Click);
    this.ApprovesBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.ApprovesBox.Location = new Point(13, 55);
    this.ApprovesBox.Multiline = true;
    this.ApprovesBox.Name = "ApprovesBox";
    this.ApprovesBox.Size = new Size(600, 25);
    this.ApprovesBox.TabIndex = 2;
    this.ApprovesBox.Visible = false;
    this.UseAlienSettingsCheckBox.AutoSize = true;
    this.UseAlienSettingsCheckBox.ImeMode = ImeMode.NoControl;
    this.UseAlienSettingsCheckBox.Location = new Point(13, 27);
    this.UseAlienSettingsCheckBox.Name = "UseAlienSettingsCheckBox";
    this.UseAlienSettingsCheckBox.Size = new Size(386, 21);
    this.UseAlienSettingsCheckBox.TabIndex = 1;
    this.UseAlienSettingsCheckBox.Text = "Проверять подписи, используя настройки действий...";
    this.UseAlienSettingsCheckBox.UseVisualStyleBackColor = true;
    this.UseAlienSettingsCheckBox.CheckedChanged += new EventHandler(this.UseAlienSettingsCheckBox_CheckedChanged);
    this.ApproveCheckOnlyCheckBox.AutoSize = true;
    this.ApproveCheckOnlyCheckBox.BackColor = SystemColors.Window;
    this.ApproveCheckOnlyCheckBox.ImeMode = ImeMode.NoControl;
    this.ApproveCheckOnlyCheckBox.Location = new Point(13, 0);
    this.ApproveCheckOnlyCheckBox.Name = "ApproveCheckOnlyCheckBox";
    this.ApproveCheckOnlyCheckBox.Size = new Size(210, 21);
    this.ApproveCheckOnlyCheckBox.TabIndex = 0;
    this.ApproveCheckOnlyCheckBox.Text = "Только проверка подписей";
    this.ApproveCheckOnlyCheckBox.UseVisualStyleBackColor = false;
    this.ApproveCheckOnlyCheckBox.CheckedChanged += new EventHandler(this.ApproveCheckOnlyCheckBox_CheckedChanged);
    this.graphForTypeCheckBox.AutoSize = true;
    this.graphForTypeCheckBox.Dock = DockStyle.Top;
    this.graphForTypeCheckBox.ImeMode = ImeMode.NoControl;
    this.graphForTypeCheckBox.Location = new Point(10, 11);
    this.graphForTypeCheckBox.Name = "graphForTypeCheckBox";
    this.graphForTypeCheckBox.Size = new Size(724, 21);
    this.graphForTypeCheckBox.TabIndex = 15;
    this.graphForTypeCheckBox.Text = "Индивидуальная настройка граф для типа объекта";
    this.graphForTypeCheckBox.UseVisualStyleBackColor = true;
    this.graphForTypeCheckBox.CheckedChanged += new EventHandler(this.graphForTypeCheckBox_CheckedChanged);
    this.signsGraphsSettingControl.Dock = DockStyle.Fill;
    this.signsGraphsSettingControl.Location = new Point(10, 32 /*0x20*/);
    this.signsGraphsSettingControl.Name = "signsGraphsSettingControl";
    this.signsGraphsSettingControl.ReadOnly = false;
    this.signsGraphsSettingControl.Size = new Size(724, 452);
    this.signsGraphsSettingControl.TabIndex = 17;
    this.signsForType.Dock = DockStyle.Fill;
    this.signsForType.Location = new Point(10, 32 /*0x20*/);
    this.signsForType.Margin = new Padding(4, 4, 4, 4);
    this.signsForType.Name = "signsForType";
    this.signsForType.Size = new Size(724, 452);
    this.signsForType.TabIndex = 16 /*0x10*/;
    this.signsForType.Visible = false;
    this.ToolTip.AutoPopDelay = 3000;
    this.ToolTip.InitialDelay = 100;
    this.ToolTip.ReshowDelay = 100;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.signsGraphsSettingControl);
    this.Controls.Add((Control) this.signsForType);
    this.Controls.Add((Control) this.ApproveCheckSpacer);
    this.Controls.Add((Control) this.ApproveCheckGB);
    this.Controls.Add((Control) this.graphForTypeCheckBox);
    this.Name = nameof (SignsSettingPageControl);
    this.Padding = new Padding(10, 11, 11, 11);
    this.Size = new Size(745, 611);
    this.ApproveCheckGB.ResumeLayout(false);
    this.ApproveCheckGB.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
