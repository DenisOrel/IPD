// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ComputeAdresseeForObjectCntrl
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

public class ComputeAdresseeForObjectCntrl : ComputeAddresseeCntrl
{
  private AdresseeSourceType _adresseeSourceType = AdresseeSourceType.ObjectAuthor;
  private ObjectsCollectMethod objectsCollectMethod = ObjectsCollectMethod.Initiator;
  private AutoNotificationSettings _notifSettings;
  private readonly AttributeChoosingCntrl attributeChoosingCntrl;
  private readonly ScriptChoosingCntrl authorFromScriptCntrl;
  private readonly ApplicabilityAndCompositionCntrl applicabilityCntrl;
  private readonly ApplicabilityAndCompositionCntrl compositionCntrl;
  private readonly ScriptChoosingCntrl objSetFromScriptCntrl;
  private readonly SearchSchemeCntrl searchSchemeCntrl;
  private IContainer components;
  private SplitContainer splitContCommon;
  private SplitContainer splitContAdressee;
  private SplitContainer splitContObjSet;
  private GroupBox gbAdressee;
  private RadioButton rbAuthorInScriptResalt;
  private RadioButton rbAuthorsDepartmentChief;
  private RadioButton rbAuthorInAttribute;
  private RadioButton rbProjectManager;
  private RadioButton rbObjectOwner;
  private RadioButton rbObjectAuthor;
  private GroupBox gbObjectsSetSource;
  private RadioButton rbGetBySearchSchemeObjects;
  private RadioButton rbFindByScriptObjectSet;
  private RadioButton rbInitiatorArticles;
  private RadioButton rbInitiatorComposition;
  private RadioButton rbInitiatorApplicability;
  private RadioButton rbInitiator;
  private RadioButton rbOwnersDepartmentChief;

  public ComputeAdresseeForObjectCntrl(AutoNotificationSettings notificationSettings)
  {
    this._notifSettings = notificationSettings;
    this.InitializeComponent();
    this.attributeChoosingCntrl = new AttributeChoosingCntrl();
    this.authorFromScriptCntrl = new ScriptChoosingCntrl();
    this.applicabilityCntrl = new ApplicabilityAndCompositionCntrl();
    this.compositionCntrl = new ApplicabilityAndCompositionCntrl();
    this.objSetFromScriptCntrl = new ScriptChoosingCntrl();
    this.searchSchemeCntrl = new SearchSchemeCntrl();
    this.splitContAdressee.Panel2.Controls.Add((Control) this.attributeChoosingCntrl);
    this.splitContAdressee.Panel2.Controls.Add((Control) this.authorFromScriptCntrl);
    this.splitContObjSet.Panel2.Controls.Add((Control) this.applicabilityCntrl);
    this.splitContObjSet.Panel2.Controls.Add((Control) this.compositionCntrl);
    this.splitContObjSet.Panel2.Controls.Add((Control) this.objSetFromScriptCntrl);
    this.splitContObjSet.Panel2.Controls.Add((Control) this.searchSchemeCntrl);
    this.applicabilityCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.compositionCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.attributeChoosingCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.compositionCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.objSetFromScriptCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.searchSchemeCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.authorFromScriptCntrl.Modified += new EventHandler(this.OnInnerControlModified);
    this.UpdateControl();
  }

  private void UpdateControl()
  {
    this.rbAuthorInScriptResalt.Visible = false;
    this.rbFindByScriptObjectSet.Visible = false;
    ComputeAdressee adressee = this._notifSettings.Adressee as ComputeAdressee;
    this.attributeChoosingCntrl.Visible = false;
    this.attributeChoosingCntrl.Dock = DockStyle.Fill;
    this.authorFromScriptCntrl.Visible = false;
    this.authorFromScriptCntrl.Dock = DockStyle.Fill;
    this.applicabilityCntrl.Visible = false;
    this.applicabilityCntrl.Dock = DockStyle.Fill;
    this.compositionCntrl.Visible = false;
    this.compositionCntrl.Dock = DockStyle.Fill;
    this.objSetFromScriptCntrl.Visible = false;
    this.objSetFromScriptCntrl.Dock = DockStyle.Fill;
    this.searchSchemeCntrl.Visible = false;
    this.searchSchemeCntrl.Dock = DockStyle.Fill;
    if (adressee == null)
      return;
    AdresseeSource adresseeSource = adressee.AdresseeSource;
    ObjectSetSource objectSetSource = adressee.ObjectSetSource;
    switch (adresseeSource.AdresseeSourceType)
    {
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
      case AdresseeSourceType.OwnersDepartmentChief:
        this.rbOwnersDepartmentChief.Checked = true;
        break;
      case AdresseeSourceType.GetByScript:
        if (adresseeSource is AuthorInScript authorInScript)
          this.authorFromScriptCntrl.ScriptID = authorInScript.ScriptID;
        if (this.rbAuthorInScriptResalt.Checked)
        {
          this.splitContAdressee.Panel2Collapsed = false;
          this.authorFromScriptCntrl.Visible = true;
        }
        this.rbAuthorInScriptResalt.Checked = true;
        break;
    }
    switch (objectSetSource.ObjectsCollectMethod)
    {
      case ObjectsCollectMethod.Initiator:
        this.rbInitiator.Checked = true;
        break;
      case ObjectsCollectMethod.InitiatorApplicability:
        if (objectSetSource is ObjectSetFromApplicabilityOrComposition applicabilityOrComposition1)
          this.applicabilityCntrl.SetData(applicabilityOrComposition1.ObjTypesIDs, applicabilityOrComposition1.RelTypesIDs, applicabilityOrComposition1.VersionRuleID);
        if (this.rbInitiatorApplicability.Checked)
        {
          this.splitContObjSet.Panel2Collapsed = false;
          this.applicabilityCntrl.Visible = true;
        }
        this.rbInitiatorApplicability.Checked = true;
        break;
      case ObjectsCollectMethod.InitiatorComposition:
        if (objectSetSource is ObjectSetFromApplicabilityOrComposition applicabilityOrComposition2)
          this.compositionCntrl.SetData(applicabilityOrComposition2.ObjTypesIDs, applicabilityOrComposition2.RelTypesIDs, applicabilityOrComposition2.VersionRuleID);
        if (this.rbInitiatorComposition.Checked)
        {
          this.splitContObjSet.Panel2Collapsed = false;
          this.compositionCntrl.Visible = true;
        }
        this.rbInitiatorComposition.Checked = true;
        break;
      case ObjectsCollectMethod.InitiatorArticles:
        this.rbInitiatorArticles.Checked = true;
        break;
      case ObjectsCollectMethod.FindByScriptObjects:
        if (objectSetSource is ObjectSetFromScript objectSetFromScript)
          this.objSetFromScriptCntrl.ScriptID = objectSetFromScript.ScriptID;
        if (this.rbFindByScriptObjectSet.Checked)
        {
          this.splitContObjSet.Panel2Collapsed = false;
          this.objSetFromScriptCntrl.Visible = true;
        }
        this.rbFindByScriptObjectSet.Checked = true;
        break;
      case ObjectsCollectMethod.GetBySearchSchemeObjects:
        if (objectSetSource is ObjectSetFromSearchScheme fromSearchScheme)
          this.searchSchemeCntrl.SearchSchemeID = fromSearchScheme.SearchSchemeID;
        if (this.rbGetBySearchSchemeObjects.Checked)
        {
          this.splitContObjSet.Panel2Collapsed = false;
          this.searchSchemeCntrl.Visible = true;
        }
        this.rbGetBySearchSchemeObjects.Checked = true;
        break;
    }
  }

  private void OnInnerControlModified(object sender, EventArgs e) => this.IsChanged = true;

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

  private void rbAuthorDepartmentChief_CheckedChanged(object sender, EventArgs e)
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

  private void rbScriptResalt_CheckedChanged(object sender, EventArgs e)
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

  private void rbInitiatorApplicability_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbInitiatorApplicability.Checked)
    {
      this.splitContObjSet.Panel2Collapsed = false;
      this.applicabilityCntrl.Visible = true;
      this.objectsCollectMethod = ObjectsCollectMethod.InitiatorApplicability;
    }
    else
    {
      this.splitContObjSet.Panel2Collapsed = true;
      this.applicabilityCntrl.Visible = false;
    }
    this.IsChanged = true;
  }

  private void rbInitiatorComposition_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbInitiatorComposition.Checked)
    {
      this.splitContObjSet.Panel2Collapsed = false;
      this.compositionCntrl.Visible = true;
      this.objectsCollectMethod = ObjectsCollectMethod.InitiatorComposition;
    }
    else
    {
      this.splitContObjSet.Panel2Collapsed = true;
      this.compositionCntrl.Visible = false;
    }
    this.IsChanged = true;
  }

  private void rbFindByScriptObjects_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbFindByScriptObjectSet.Checked)
    {
      this.splitContObjSet.Panel2Collapsed = false;
      this.objSetFromScriptCntrl.Visible = true;
      this.objectsCollectMethod = ObjectsCollectMethod.FindByScriptObjects;
    }
    else
    {
      this.splitContObjSet.Panel2Collapsed = true;
      this.objSetFromScriptCntrl.Visible = false;
    }
    this.IsChanged = true;
  }

  private void rbGetBySearchSchemeObjects_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbGetBySearchSchemeObjects.Checked)
    {
      this.splitContObjSet.Panel2Collapsed = false;
      this.searchSchemeCntrl.Visible = true;
      this.objectsCollectMethod = ObjectsCollectMethod.GetBySearchSchemeObjects;
    }
    else
    {
      this.splitContObjSet.Panel2Collapsed = true;
      this.searchSchemeCntrl.Visible = false;
    }
    this.IsChanged = true;
  }

  private void rbInitiator_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbInitiator.Checked)
      this.objectsCollectMethod = ObjectsCollectMethod.Initiator;
    this.IsChanged = true;
  }

  private void rbInitiatorArticles_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbInitiatorArticles.Checked)
      this.objectsCollectMethod = ObjectsCollectMethod.InitiatorArticles;
    this.IsChanged = true;
  }

  public override void SaveSettings()
  {
    this._notifSettings.Adressee = (Adressee) new ComputeAdressee(this._adresseeSourceType != AdresseeSourceType.AuthorInAttribute ? (this._adresseeSourceType != AdresseeSourceType.GetByScript ? new AdresseeSource(this._adresseeSourceType) : (AdresseeSource) new AuthorInScript(this._adresseeSourceType, this.authorFromScriptCntrl.ScriptID)) : (AdresseeSource) new AuthorInAttribute(this._adresseeSourceType, this.attributeChoosingCntrl.AttrID), this.objectsCollectMethod != ObjectsCollectMethod.InitiatorApplicability ? (this.objectsCollectMethod != ObjectsCollectMethod.InitiatorComposition ? (this.objectsCollectMethod != ObjectsCollectMethod.FindByScriptObjects ? (this.objectsCollectMethod != ObjectsCollectMethod.GetBySearchSchemeObjects ? new ObjectSetSource(this.objectsCollectMethod) : (ObjectSetSource) new ObjectSetFromSearchScheme(this.objectsCollectMethod, this.searchSchemeCntrl.SearchSchemeID)) : (ObjectSetSource) new ObjectSetFromScript(this.objectsCollectMethod, this.objSetFromScriptCntrl.ScriptID)) : (ObjectSetSource) new ObjectSetFromApplicabilityOrComposition(this.objectsCollectMethod, this.compositionCntrl.ObjTypes, this.compositionCntrl.RelTypes, this.compositionCntrl.RuleID)) : (ObjectSetSource) new ObjectSetFromApplicabilityOrComposition(this.objectsCollectMethod, this.applicabilityCntrl.ObjTypes, this.applicabilityCntrl.RelTypes, this.applicabilityCntrl.RuleID));
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
    this.rbAuthorInScriptResalt = new RadioButton();
    this.rbAuthorsDepartmentChief = new RadioButton();
    this.rbAuthorInAttribute = new RadioButton();
    this.rbProjectManager = new RadioButton();
    this.rbObjectOwner = new RadioButton();
    this.rbObjectAuthor = new RadioButton();
    this.splitContObjSet = new SplitContainer();
    this.gbObjectsSetSource = new GroupBox();
    this.rbGetBySearchSchemeObjects = new RadioButton();
    this.rbFindByScriptObjectSet = new RadioButton();
    this.rbInitiatorArticles = new RadioButton();
    this.rbInitiatorComposition = new RadioButton();
    this.rbInitiatorApplicability = new RadioButton();
    this.rbInitiator = new RadioButton();
    this.splitContCommon.BeginInit();
    this.splitContCommon.Panel1.SuspendLayout();
    this.splitContCommon.Panel2.SuspendLayout();
    this.splitContCommon.SuspendLayout();
    this.splitContAdressee.BeginInit();
    this.splitContAdressee.Panel1.SuspendLayout();
    this.splitContAdressee.SuspendLayout();
    this.gbAdressee.SuspendLayout();
    this.splitContObjSet.BeginInit();
    this.splitContObjSet.Panel1.SuspendLayout();
    this.splitContObjSet.SuspendLayout();
    this.gbObjectsSetSource.SuspendLayout();
    this.SuspendLayout();
    this.splitContCommon.Dock = DockStyle.Fill;
    this.splitContCommon.Location = new Point(0, 0);
    this.splitContCommon.Name = "splitContCommon";
    this.splitContCommon.Orientation = Orientation.Horizontal;
    this.splitContCommon.Panel1.AutoScroll = true;
    this.splitContCommon.Panel1.Controls.Add((Control) this.splitContAdressee);
    this.splitContCommon.Panel2.AutoScroll = true;
    this.splitContCommon.Panel2.Controls.Add((Control) this.splitContObjSet);
    this.splitContCommon.Size = new Size(1136, 600);
    this.splitContCommon.SplitterDistance = 198;
    this.splitContCommon.TabIndex = 0;
    this.splitContAdressee.Dock = DockStyle.Fill;
    this.splitContAdressee.Location = new Point(0, 0);
    this.splitContAdressee.Name = "splitContAdressee";
    this.splitContAdressee.Panel1.AutoScroll = true;
    this.splitContAdressee.Panel1.Controls.Add((Control) this.gbAdressee);
    this.splitContAdressee.Panel2.AutoScroll = true;
    this.splitContAdressee.Panel2Collapsed = true;
    this.splitContAdressee.Size = new Size(1136, 198);
    this.splitContAdressee.SplitterDistance = 313;
    this.splitContAdressee.SplitterWidth = 6;
    this.splitContAdressee.TabIndex = 0;
    this.gbAdressee.Controls.Add((Control) this.rbOwnersDepartmentChief);
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
    this.gbAdressee.Size = new Size(1136, 198);
    this.gbAdressee.TabIndex = 3;
    this.gbAdressee.TabStop = false;
    this.gbAdressee.Text = "Адресат";
    this.rbOwnersDepartmentChief.AutoSize = true;
    this.rbOwnersDepartmentChief.Location = new Point(7, 140);
    this.rbOwnersDepartmentChief.Name = "rbOwnersDepartmentChief";
    this.rbOwnersDepartmentChief.Size = new Size(397, 17);
    this.rbOwnersDepartmentChief.TabIndex = 6;
    this.rbOwnersDepartmentChief.Text = "Руководитель подразделения, которому принадлежит владелец объекта";
    this.rbOwnersDepartmentChief.UseVisualStyleBackColor = true;
    this.rbOwnersDepartmentChief.CheckedChanged += new EventHandler(this.rbOwnersDepartmentChief_CheckedChanged);
    this.rbAuthorInScriptResalt.AutoSize = true;
    this.rbAuthorInScriptResalt.Location = new Point(7, 163);
    this.rbAuthorInScriptResalt.Name = "rbAuthorInScriptResalt";
    this.rbAuthorInScriptResalt.Size = new Size(186, 17);
    this.rbAuthorInScriptResalt.TabIndex = 5;
    this.rbAuthorInScriptResalt.Text = "Результат выполнения скрипта";
    this.rbAuthorInScriptResalt.UseVisualStyleBackColor = true;
    this.rbAuthorInScriptResalt.CheckedChanged += new EventHandler(this.rbScriptResalt_CheckedChanged);
    this.rbAuthorsDepartmentChief.AutoSize = true;
    this.rbAuthorsDepartmentChief.Location = new Point(7, 116);
    this.rbAuthorsDepartmentChief.Name = "rbAuthorsDepartmentChief";
    this.rbAuthorsDepartmentChief.Size = new Size(378, 17);
    this.rbAuthorsDepartmentChief.TabIndex = 4;
    this.rbAuthorsDepartmentChief.Text = "Руководитель подразделения, которому принадлежит автор объекта";
    this.rbAuthorsDepartmentChief.UseVisualStyleBackColor = true;
    this.rbAuthorsDepartmentChief.CheckedChanged += new EventHandler(this.rbAuthorDepartmentChief_CheckedChanged);
    this.rbAuthorInAttribute.AutoSize = true;
    this.rbAuthorInAttribute.Location = new Point(7, 92);
    this.rbAuthorInAttribute.Name = "rbAuthorInAttribute";
    this.rbAuthorInAttribute.Size = new Size(150, 17);
    this.rbAuthorInAttribute.TabIndex = 3;
    this.rbAuthorInAttribute.Text = "Автор указан в атрибуте";
    this.rbAuthorInAttribute.UseVisualStyleBackColor = true;
    this.rbAuthorInAttribute.CheckedChanged += new EventHandler(this.rbAuthorInAttribute_CheckedChanged);
    this.rbProjectManager.AutoSize = true;
    this.rbProjectManager.Location = new Point(7, 68);
    this.rbProjectManager.Name = "rbProjectManager";
    this.rbProjectManager.Size = new Size(122, 17);
    this.rbProjectManager.TabIndex = 2;
    this.rbProjectManager.Text = "Менеджер проекта";
    this.rbProjectManager.UseVisualStyleBackColor = true;
    this.rbProjectManager.CheckedChanged += new EventHandler(this.rbProjectManager_CheckedChanged);
    this.rbObjectOwner.AutoSize = true;
    this.rbObjectOwner.Location = new Point(7, 43);
    this.rbObjectOwner.Name = "rbObjectOwner";
    this.rbObjectOwner.Size = new Size(119, 17);
    this.rbObjectOwner.TabIndex = 1;
    this.rbObjectOwner.Text = "Владелец объекта";
    this.rbObjectOwner.UseVisualStyleBackColor = true;
    this.rbObjectOwner.CheckedChanged += new EventHandler(this.rbObjectOwner_CheckedChanged);
    this.rbObjectAuthor.AutoSize = true;
    this.rbObjectAuthor.Checked = true;
    this.rbObjectAuthor.Location = new Point(7, 19);
    this.rbObjectAuthor.Name = "rbObjectAuthor";
    this.rbObjectAuthor.Size = new Size(100, 17);
    this.rbObjectAuthor.TabIndex = 0;
    this.rbObjectAuthor.TabStop = true;
    this.rbObjectAuthor.Text = "Автор объекта";
    this.rbObjectAuthor.UseVisualStyleBackColor = true;
    this.rbObjectAuthor.CheckedChanged += new EventHandler(this.rbObjectAuthor_CheckedChanged);
    this.splitContObjSet.Dock = DockStyle.Fill;
    this.splitContObjSet.Location = new Point(0, 0);
    this.splitContObjSet.Name = "splitContObjSet";
    this.splitContObjSet.Panel1.AutoScroll = true;
    this.splitContObjSet.Panel1.Controls.Add((Control) this.gbObjectsSetSource);
    this.splitContObjSet.Panel2.AutoScroll = true;
    this.splitContObjSet.Panel2Collapsed = true;
    this.splitContObjSet.Size = new Size(1136, 398);
    this.splitContObjSet.SplitterDistance = 561;
    this.splitContObjSet.SplitterWidth = 6;
    this.splitContObjSet.TabIndex = 0;
    this.gbObjectsSetSource.AutoSize = true;
    this.gbObjectsSetSource.BackColor = SystemColors.Control;
    this.gbObjectsSetSource.Controls.Add((Control) this.rbGetBySearchSchemeObjects);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbFindByScriptObjectSet);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbInitiatorArticles);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbInitiatorComposition);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbInitiatorApplicability);
    this.gbObjectsSetSource.Controls.Add((Control) this.rbInitiator);
    this.gbObjectsSetSource.Dock = DockStyle.Fill;
    this.gbObjectsSetSource.FlatStyle = FlatStyle.System;
    this.gbObjectsSetSource.Location = new Point(0, 0);
    this.gbObjectsSetSource.Name = "gbObjectsSetSource";
    this.gbObjectsSetSource.Size = new Size(1136, 398);
    this.gbObjectsSetSource.TabIndex = 1;
    this.gbObjectsSetSource.TabStop = false;
    this.gbObjectsSetSource.Text = "Способ определения набора объектов";
    this.rbGetBySearchSchemeObjects.AutoSize = true;
    this.rbGetBySearchSchemeObjects.Location = new Point(7, 115);
    this.rbGetBySearchSchemeObjects.Name = "rbGetBySearchSchemeObjects";
    this.rbGetBySearchSchemeObjects.Size = new Size(253, 17);
    this.rbGetBySearchSchemeObjects.TabIndex = 5;
    this.rbGetBySearchSchemeObjects.Text = "Список объектов, собранных схемой поиска";
    this.rbGetBySearchSchemeObjects.UseVisualStyleBackColor = true;
    this.rbGetBySearchSchemeObjects.CheckedChanged += new EventHandler(this.rbGetBySearchSchemeObjects_CheckedChanged);
    this.rbFindByScriptObjectSet.AutoSize = true;
    this.rbFindByScriptObjectSet.Location = new Point(7, 138);
    this.rbFindByScriptObjectSet.Name = "rbFindByScriptObjectSet";
    this.rbFindByScriptObjectSet.Size = new Size(243, 17);
    this.rbFindByScriptObjectSet.TabIndex = 4;
    this.rbFindByScriptObjectSet.Text = "Список объектов, собранных скриптом ЭС";
    this.rbFindByScriptObjectSet.UseVisualStyleBackColor = true;
    this.rbFindByScriptObjectSet.CheckedChanged += new EventHandler(this.rbFindByScriptObjects_CheckedChanged);
    this.rbInitiatorArticles.AutoSize = true;
    this.rbInitiatorArticles.Location = new Point(7, 92);
    this.rbInitiatorArticles.Name = "rbInitiatorArticles";
    this.rbInitiatorArticles.Size = new Size(132, 17);
    this.rbInitiatorArticles.TabIndex = 3;
    this.rbInitiatorArticles.Text = "Исполнения изделий";
    this.rbInitiatorArticles.UseVisualStyleBackColor = true;
    this.rbInitiatorArticles.CheckedChanged += new EventHandler(this.rbInitiatorArticles_CheckedChanged);
    this.rbInitiatorComposition.AutoSize = true;
    this.rbInitiatorComposition.Location = new Point(7, 68);
    this.rbInitiatorComposition.Name = "rbInitiatorComposition";
    this.rbInitiatorComposition.Size = new Size(536, 17);
    this.rbInitiatorComposition.TabIndex = 2;
    this.rbInitiatorComposition.Text = "Версии объектов указанных типов, из которых состоит объект-инициатор указанным типом связей";
    this.rbInitiatorComposition.UseVisualStyleBackColor = true;
    this.rbInitiatorComposition.CheckedChanged += new EventHandler(this.rbInitiatorComposition_CheckedChanged);
    this.rbInitiatorApplicability.AutoSize = true;
    this.rbInitiatorApplicability.Location = new Point(7, 44);
    this.rbInitiatorApplicability.Name = "rbInitiatorApplicability";
    this.rbInitiatorApplicability.Size = new Size(525, 17);
    this.rbInitiatorApplicability.TabIndex = 1;
    this.rbInitiatorApplicability.Text = "Версии объектов указанных типов, в которые входит объект-инициатор указанным типом связей";
    this.rbInitiatorApplicability.UseVisualStyleBackColor = true;
    this.rbInitiatorApplicability.CheckedChanged += new EventHandler(this.rbInitiatorApplicability_CheckedChanged);
    this.rbInitiator.AutoSize = true;
    this.rbInitiator.Checked = true;
    this.rbInitiator.Location = new Point(7, 20);
    this.rbInitiator.Name = "rbInitiator";
    this.rbInitiator.Size = new Size(119, 17);
    this.rbInitiator.TabIndex = 0;
    this.rbInitiator.TabStop = true;
    this.rbInitiator.Text = "Объект-инициатор";
    this.rbInitiator.UseVisualStyleBackColor = true;
    this.rbInitiator.CheckedChanged += new EventHandler(this.rbInitiator_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.splitContCommon);
    this.Name = nameof (ComputeAdresseeForObjectCntrl);
    this.Size = new Size(1136, 600);
    this.splitContCommon.Panel1.ResumeLayout(false);
    this.splitContCommon.Panel2.ResumeLayout(false);
    this.splitContCommon.EndInit();
    this.splitContCommon.ResumeLayout(false);
    this.splitContAdressee.Panel1.ResumeLayout(false);
    this.splitContAdressee.EndInit();
    this.splitContAdressee.ResumeLayout(false);
    this.gbAdressee.ResumeLayout(false);
    this.gbAdressee.PerformLayout();
    this.splitContObjSet.Panel1.ResumeLayout(false);
    this.splitContObjSet.Panel1.PerformLayout();
    this.splitContObjSet.EndInit();
    this.splitContObjSet.ResumeLayout(false);
    this.gbObjectsSetSource.ResumeLayout(false);
    this.gbObjectsSetSource.PerformLayout();
    this.ResumeLayout(false);
  }
}
