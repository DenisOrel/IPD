// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ComputeAdresseeForRelationCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ComputeAdresseeForRelationCntrl : ComputeAddresseeCntrl
{
  private AutoNotificationSettings _notifSettings;
  private AdresseeSourceType _adresseeSourceType = AdresseeSourceType.RelationAuthor;
  private ObjectsCollectMethod objectsCollectMethod = ObjectsCollectMethod.RelationPart;
  private readonly AttributeChoosingCntrl attributeChoosingCntrl;
  private readonly ScriptChoosingCntrl authorFromScriptCntrl;
  private IContainer components;
  private SplitContainer splitContCommon;
  private SplitContainer splitContAdressee;
  private SplitContainer splitContObjectSet;
  private GroupBox gbObjectsSetSource;
  private RadioButton rbParentAndChildObjects;
  private RadioButton rbChildObject;
  private RadioButton rbParentObject;
  private GroupBox gbAdressee;
  private RadioButton rbOwnersDepartmentChief;
  private RadioButton rbRelationAuthor;
  private RadioButton rbAuthorInScriptResalt;
  private RadioButton rbAuthorsDepartmentChief;
  private RadioButton rbAuthorInAttribute;
  private RadioButton rbProjectManager;
  private RadioButton rbObjectOwner;
  private RadioButton rbObjectAuthor;

  public ComputeAdresseeForRelationCntrl(AutoNotificationSettings notificationSettings)
  {
    this._notifSettings = notificationSettings;
    this.InitializeComponent();
    this.attributeChoosingCntrl = new AttributeChoosingCntrl();
    this.attributeChoosingCntrl.Dock = DockStyle.Fill;
    this.authorFromScriptCntrl = new ScriptChoosingCntrl();
    this.splitContAdressee.Panel2.Controls.Add((Control) this.attributeChoosingCntrl);
    this.splitContAdressee.Panel2.Controls.Add((Control) this.authorFromScriptCntrl);
    this.authorFromScriptCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.attributeChoosingCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.UpdateControl();
  }

  private void UpdateControl()
  {
    this.rbAuthorInScriptResalt.Visible = false;
    ComputeAdressee adressee = this._notifSettings.Adressee as ComputeAdressee;
    this.attributeChoosingCntrl.Visible = false;
    this.authorFromScriptCntrl.Visible = false;
    if (adressee == null)
    {
      this.rbRelationAuthor.Checked = true;
      this.splitContCommon.Panel2Collapsed = true;
    }
    else
    {
      AdresseeSource adresseeSource = adressee.AdresseeSource;
      ObjectSetSource objectSetSource = adressee.ObjectSetSource;
      switch (adresseeSource.AdresseeSourceType)
      {
        case AdresseeSourceType.RelationAuthor:
          this.rbRelationAuthor.Checked = true;
          this.splitContCommon.Panel2Collapsed = true;
          break;
        case AdresseeSourceType.ObjectAuthor:
          this.rbObjectAuthor.Checked = true;
          break;
        case AdresseeSourceType.ObjectOwner:
          this.rbObjectOwner.Checked = true;
          break;
        case AdresseeSourceType.ProjectManager:
          this.rbProjectManager.Checked = true;
          break;
        case AdresseeSourceType.AuthorInAttribute:
          if (adresseeSource is AuthorInAttribute authorInAttribute)
            this.attributeChoosingCntrl.AttrID = authorInAttribute.AttrID;
          if (this.rbAuthorInAttribute.Checked)
          {
            this.splitContAdressee.Panel2Collapsed = false;
            this.attributeChoosingCntrl.Visible = true;
          }
          this.rbAuthorInAttribute.Checked = true;
          break;
        case AdresseeSourceType.AuthorsDepartmentChief:
          this.rbAuthorsDepartmentChief.Checked = true;
          break;
        case AdresseeSourceType.GetByScript:
          if (adresseeSource is AuthorInScript authorInScript)
            this.authorFromScriptCntrl.ScriptID = authorInScript.ScriptID;
          this.rbAuthorInScriptResalt.Checked = true;
          if (this.rbAuthorInScriptResalt.Checked)
          {
            this.splitContAdressee.Panel2Collapsed = false;
            this.authorFromScriptCntrl.Visible = true;
            break;
          }
          break;
      }
      switch (objectSetSource.ObjectsCollectMethod)
      {
        case ObjectsCollectMethod.RelationPart:
          this.rbChildObject.Checked = true;
          break;
        case ObjectsCollectMethod.RelationProject:
          this.rbParentObject.Checked = true;
          break;
        case ObjectsCollectMethod.RelationPartAndProjects:
          this.rbParentAndChildObjects.Checked = true;
          break;
      }
    }
  }

  private void OnInnerControlModified(object sender, EventArgs e) => this.IsChanged = true;

  private void rbRelationAuthor_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbRelationAuthor.Checked)
    {
      this._adresseeSourceType = AdresseeSourceType.RelationAuthor;
      this.splitContCommon.Panel2Collapsed = true;
    }
    else
      this.splitContCommon.Panel2Collapsed = false;
    this.IsChanged = true;
  }

  private void rbObjectAuthor_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbObjectAuthor.Checked)
      this._adresseeSourceType = AdresseeSourceType.ObjectAuthor;
    this.IsChanged = true;
  }

  private void rbObjectOwner_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbObjectOwner.Checked)
      this._adresseeSourceType = AdresseeSourceType.ObjectOwner;
    this.IsChanged = true;
  }

  private void rbProjectManager_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbProjectManager.Checked)
      this._adresseeSourceType = AdresseeSourceType.ProjectManager;
    this.IsChanged = true;
  }

  private void rbAuthorInAttribute_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbAuthorInAttribute.Checked)
    {
      this.splitContAdressee.Panel2Collapsed = false;
      this.attributeChoosingCntrl.Visible = true;
      this._adresseeSourceType = AdresseeSourceType.AuthorInAttribute;
    }
    else
    {
      this.splitContAdressee.Panel2Collapsed = true;
      this.attributeChoosingCntrl.Visible = false;
    }
    this.IsChanged = true;
  }

  private void rbAuthorInScriptResalt_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbAuthorInScriptResalt.Checked)
    {
      this.splitContAdressee.Panel2Collapsed = false;
      this.authorFromScriptCntrl.Visible = true;
      this._adresseeSourceType = AdresseeSourceType.GetByScript;
    }
    else
    {
      this.splitContAdressee.Panel2Collapsed = true;
      this.authorFromScriptCntrl.Visible = false;
    }
    this.IsChanged = true;
  }

  private void rbAuthorsDepartmentChief_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbAuthorsDepartmentChief.Checked)
      this._adresseeSourceType = AdresseeSourceType.AuthorsDepartmentChief;
    this.IsChanged = true;
  }

  private void rbOwnersDepartmentChief_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbOwnersDepartmentChief.Checked)
      this._adresseeSourceType = AdresseeSourceType.OwnersDepartmentChief;
    this.IsChanged = true;
  }

  private void rbParentObject_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbParentObject.Checked)
      this.objectsCollectMethod = ObjectsCollectMethod.RelationProject;
    this.IsChanged = true;
  }

  private void rbChildObject_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbChildObject.Checked)
      this.objectsCollectMethod = ObjectsCollectMethod.RelationPart;
    this.IsChanged = true;
  }

  private void rbParentAndChildObjects_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbParentAndChildObjects.Checked)
      this.objectsCollectMethod = ObjectsCollectMethod.RelationPartAndProjects;
    this.IsChanged = true;
  }

  public override void SaveSettings()
  {
    this._notifSettings.Adressee = (Adressee) new ComputeAdressee(this._adresseeSourceType != AdresseeSourceType.AuthorInAttribute ? (this._adresseeSourceType != AdresseeSourceType.GetByScript ? new AdresseeSource(this._adresseeSourceType) : (AdresseeSource) new AuthorInScript(this._adresseeSourceType, this.authorFromScriptCntrl.ScriptID)) : (AdresseeSource) new AuthorInAttribute(this._adresseeSourceType, this.attributeChoosingCntrl.AttrID), new ObjectSetSource(this.objectsCollectMethod));
  }

  public override void Refresh()
  {
    base.Refresh();
    this.UpdateControl();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.splitContCommon = new SplitContainer();
    this.splitContAdressee = new SplitContainer();
    this.gbAdressee = new GroupBox();
    this.rbOwnersDepartmentChief = new RadioButton();
    this.rbRelationAuthor = new RadioButton();
    this.rbAuthorInScriptResalt = new RadioButton();
    this.rbAuthorsDepartmentChief = new RadioButton();
    this.rbAuthorInAttribute = new RadioButton();
    this.rbProjectManager = new RadioButton();
    this.rbObjectOwner = new RadioButton();
    this.rbObjectAuthor = new RadioButton();
    this.splitContObjectSet = new SplitContainer();
    this.gbObjectsSetSource = new GroupBox();
    this.rbParentAndChildObjects = new RadioButton();
    this.rbChildObject = new RadioButton();
    this.rbParentObject = new RadioButton();
    this.splitContCommon.BeginInit();
    this.splitContCommon.Panel1.SuspendLayout();
    this.splitContCommon.Panel2.SuspendLayout();
    this.splitContCommon.SuspendLayout();
    this.splitContAdressee.BeginInit();
    this.splitContAdressee.Panel1.SuspendLayout();
    this.splitContAdressee.SuspendLayout();
    this.gbAdressee.SuspendLayout();
    this.splitContObjectSet.BeginInit();
    this.splitContObjectSet.Panel1.SuspendLayout();
    this.splitContObjectSet.SuspendLayout();
    this.gbObjectsSetSource.SuspendLayout();
    this.SuspendLayout();
    this.splitContCommon.Dock = DockStyle.Fill;
    this.splitContCommon.Location = new Point(0, 0);
    this.splitContCommon.Name = "splitContCommon";
    this.splitContCommon.Orientation = Orientation.Horizontal;
    this.splitContCommon.Panel1.AutoScroll = true;
    this.splitContCommon.Panel1.Controls.Add((Control) this.splitContAdressee);
    this.splitContCommon.Panel2.AutoScroll = true;
    this.splitContCommon.Panel2.Controls.Add((Control) this.splitContObjectSet);
    this.splitContCommon.Size = new Size(562, 430);
    this.splitContCommon.SplitterDistance = 210;
    this.splitContCommon.TabIndex = 0;
    this.splitContAdressee.Dock = DockStyle.Fill;
    this.splitContAdressee.Location = new Point(0, 0);
    this.splitContAdressee.Name = "splitContAdressee";
    this.splitContAdressee.Panel1.AutoScroll = true;
    this.splitContAdressee.Panel1.Controls.Add((Control) this.gbAdressee);
    this.splitContAdressee.Panel2.AutoScroll = true;
    this.splitContAdressee.Panel2Collapsed = true;
    this.splitContAdressee.Size = new Size(562, 210);
    this.splitContAdressee.SplitterDistance = 408;
    this.splitContAdressee.SplitterWidth = 7;
    this.splitContAdressee.TabIndex = 0;
    this.gbAdressee.BackColor = SystemColors.Control;
    this.gbAdressee.Controls.Add((Control) this.rbOwnersDepartmentChief);
    this.gbAdressee.Controls.Add((Control) this.rbRelationAuthor);
    this.gbAdressee.Controls.Add((Control) this.rbAuthorInScriptResalt);
    this.gbAdressee.Controls.Add((Control) this.rbAuthorsDepartmentChief);
    this.gbAdressee.Controls.Add((Control) this.rbAuthorInAttribute);
    this.gbAdressee.Controls.Add((Control) this.rbProjectManager);
    this.gbAdressee.Controls.Add((Control) this.rbObjectOwner);
    this.gbAdressee.Controls.Add((Control) this.rbObjectAuthor);
    this.gbAdressee.Cursor = Cursors.Default;
    this.gbAdressee.Dock = DockStyle.Fill;
    this.gbAdressee.FlatStyle = FlatStyle.System;
    this.gbAdressee.Location = new Point(0, 0);
    this.gbAdressee.Name = "gbAdressee";
    this.gbAdressee.Size = new Size(562, 210);
    this.gbAdressee.TabIndex = 5;
    this.gbAdressee.TabStop = false;
    this.gbAdressee.Text = "Адресат";
    this.rbOwnersDepartmentChief.AutoSize = true;
    this.rbOwnersDepartmentChief.Location = new Point(6, 164);
    this.rbOwnersDepartmentChief.Name = "rbOwnersDepartmentChief";
    this.rbOwnersDepartmentChief.Size = new Size(397, 17);
    this.rbOwnersDepartmentChief.TabIndex = 7;
    this.rbOwnersDepartmentChief.Text = "Руководитель подразделения, которому принадлежит владелец объекта";
    this.rbOwnersDepartmentChief.UseVisualStyleBackColor = true;
    this.rbOwnersDepartmentChief.CheckedChanged += new EventHandler(this.rbOwnersDepartmentChief_CheckedChanged);
    this.rbRelationAuthor.AutoSize = true;
    this.rbRelationAuthor.Checked = true;
    this.rbRelationAuthor.Location = new Point(6, 23);
    this.rbRelationAuthor.Name = "rbRelationAuthor";
    this.rbRelationAuthor.Size = new Size(88, 17);
    this.rbRelationAuthor.TabIndex = 6;
    this.rbRelationAuthor.TabStop = true;
    this.rbRelationAuthor.Text = "Автор связи";
    this.rbRelationAuthor.UseVisualStyleBackColor = true;
    this.rbRelationAuthor.CheckedChanged += new EventHandler(this.rbRelationAuthor_CheckedChanged);
    this.rbAuthorInScriptResalt.AutoSize = true;
    this.rbAuthorInScriptResalt.Location = new Point(6, 187);
    this.rbAuthorInScriptResalt.Name = "rbAuthorInScriptResalt";
    this.rbAuthorInScriptResalt.Size = new Size(186, 17);
    this.rbAuthorInScriptResalt.TabIndex = 5;
    this.rbAuthorInScriptResalt.Text = "Результат выполнения скрипта";
    this.rbAuthorInScriptResalt.UseVisualStyleBackColor = true;
    this.rbAuthorInScriptResalt.CheckedChanged += new EventHandler(this.rbAuthorInScriptResalt_CheckedChanged);
    this.rbAuthorsDepartmentChief.AutoSize = true;
    this.rbAuthorsDepartmentChief.Location = new Point(6, 143);
    this.rbAuthorsDepartmentChief.Name = "rbAuthorsDepartmentChief";
    this.rbAuthorsDepartmentChief.Size = new Size(372, 17);
    this.rbAuthorsDepartmentChief.TabIndex = 4;
    this.rbAuthorsDepartmentChief.Text = "Руководитель подразделения, котрому принадлежит автор объекта";
    this.rbAuthorsDepartmentChief.UseVisualStyleBackColor = true;
    this.rbAuthorsDepartmentChief.CheckedChanged += new EventHandler(this.rbAuthorsDepartmentChief_CheckedChanged);
    this.rbAuthorInAttribute.AutoSize = true;
    this.rbAuthorInAttribute.Location = new Point(6, 119);
    this.rbAuthorInAttribute.Name = "rbAuthorInAttribute";
    this.rbAuthorInAttribute.Size = new Size(150, 17);
    this.rbAuthorInAttribute.TabIndex = 3;
    this.rbAuthorInAttribute.Text = "Автор указан в атрибуте";
    this.rbAuthorInAttribute.UseVisualStyleBackColor = true;
    this.rbAuthorInAttribute.CheckedChanged += new EventHandler(this.rbAuthorInAttribute_CheckedChanged);
    this.rbProjectManager.AutoSize = true;
    this.rbProjectManager.Location = new Point(6, 95);
    this.rbProjectManager.Name = "rbProjectManager";
    this.rbProjectManager.Size = new Size(122, 17);
    this.rbProjectManager.TabIndex = 2;
    this.rbProjectManager.Text = "Менеджер проекта";
    this.rbProjectManager.UseVisualStyleBackColor = true;
    this.rbProjectManager.CheckedChanged += new EventHandler(this.rbProjectManager_CheckedChanged);
    this.rbObjectOwner.AutoSize = true;
    this.rbObjectOwner.Location = new Point(6, 70);
    this.rbObjectOwner.Name = "rbObjectOwner";
    this.rbObjectOwner.Size = new Size(119, 17);
    this.rbObjectOwner.TabIndex = 1;
    this.rbObjectOwner.Text = "Владелец объекта";
    this.rbObjectOwner.UseVisualStyleBackColor = true;
    this.rbObjectOwner.CheckedChanged += new EventHandler(this.rbObjectOwner_CheckedChanged);
    this.rbObjectAuthor.AutoSize = true;
    this.rbObjectAuthor.Location = new Point(6, 46);
    this.rbObjectAuthor.Name = "rbObjectAuthor";
    this.rbObjectAuthor.Size = new Size(100, 17);
    this.rbObjectAuthor.TabIndex = 0;
    this.rbObjectAuthor.Text = "Автор объекта";
    this.rbObjectAuthor.UseVisualStyleBackColor = true;
    this.rbObjectAuthor.CheckedChanged += new EventHandler(this.rbObjectAuthor_CheckedChanged);
    this.splitContObjectSet.Dock = DockStyle.Fill;
    this.splitContObjectSet.Location = new Point(0, 0);
    this.splitContObjectSet.Name = "splitContObjectSet";
    this.splitContObjectSet.Panel1.AutoScroll = true;
    this.splitContObjectSet.Panel1.Controls.Add((Control) this.gbObjectsSetSource);
    this.splitContObjectSet.Panel2.AutoScroll = true;
    this.splitContObjectSet.Panel2Collapsed = true;
    this.splitContObjectSet.Size = new Size(562, 216);
    this.splitContObjectSet.SplitterDistance = 218;
    this.splitContObjectSet.TabIndex = 0;
    this.gbObjectsSetSource.AutoSize = true;
    this.gbObjectsSetSource.BackColor = SystemColors.Control;
    this.gbObjectsSetSource.Controls.Add((Control) this.rbParentAndChildObjects);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbChildObject);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbParentObject);
    this.gbObjectsSetSource.Dock = DockStyle.Fill;
    this.gbObjectsSetSource.FlatStyle = FlatStyle.System;
    this.gbObjectsSetSource.Location = new Point(0, 0);
    this.gbObjectsSetSource.Name = "gbObjectsSetSource";
    this.gbObjectsSetSource.Size = new Size(562, 216);
    this.gbObjectsSetSource.TabIndex = 2;
    this.gbObjectsSetSource.TabStop = false;
    this.gbObjectsSetSource.Text = "Способ определения набора объектов";
    this.rbParentAndChildObjects.AutoSize = true;
    this.rbParentAndChildObjects.Location = new Point(7, 68);
    this.rbParentAndChildObjects.Name = "rbParentAndChildObjects";
    this.rbParentAndChildObjects.Size = new Size(236, 17);
    this.rbParentAndChildObjects.TabIndex = 2;
    this.rbParentAndChildObjects.Text = "Родительский и дочерний объекты связи";
    this.rbParentAndChildObjects.UseVisualStyleBackColor = true;
    this.rbParentAndChildObjects.CheckedChanged += new EventHandler(this.rbParentAndChildObjects_CheckedChanged);
    this.rbChildObject.AutoSize = true;
    this.rbChildObject.Location = new Point(7, 44);
    this.rbChildObject.Name = "rbChildObject";
    this.rbChildObject.Size = new Size(147, 17);
    this.rbChildObject.TabIndex = 1;
    this.rbChildObject.Text = "Дочерний объект связи";
    this.rbChildObject.UseVisualStyleBackColor = true;
    this.rbChildObject.CheckedChanged += new EventHandler(this.rbChildObject_CheckedChanged);
    this.rbParentObject.AutoSize = true;
    this.rbParentObject.Checked = true;
    this.rbParentObject.Location = new Point(7, 20);
    this.rbParentObject.Name = "rbParentObject";
    this.rbParentObject.Size = new Size(169, 17);
    this.rbParentObject.TabIndex = 0;
    this.rbParentObject.TabStop = true;
    this.rbParentObject.Text = "Родительский объект связи";
    this.rbParentObject.UseVisualStyleBackColor = true;
    this.rbParentObject.CheckedChanged += new EventHandler(this.rbParentObject_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContCommon);
    this.Name = nameof (ComputeAdresseeForRelationCntrl);
    this.Size = new Size(562, 430);
    this.splitContCommon.Panel1.ResumeLayout(false);
    this.splitContCommon.Panel2.ResumeLayout(false);
    this.splitContCommon.EndInit();
    this.splitContCommon.ResumeLayout(false);
    this.splitContAdressee.Panel1.ResumeLayout(false);
    this.splitContAdressee.EndInit();
    this.splitContAdressee.ResumeLayout(false);
    this.gbAdressee.ResumeLayout(false);
    this.gbAdressee.PerformLayout();
    this.splitContObjectSet.Panel1.ResumeLayout(false);
    this.splitContObjectSet.Panel1.PerformLayout();
    this.splitContObjectSet.EndInit();
    this.splitContObjectSet.ResumeLayout(false);
    this.gbObjectsSetSource.ResumeLayout(false);
    this.gbObjectsSetSource.PerformLayout();
    this.ResumeLayout(false);
  }
}
