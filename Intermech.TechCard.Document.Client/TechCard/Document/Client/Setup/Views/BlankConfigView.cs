// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.Views.BlankConfigView
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.TechCard.Document.Client.Configs.Visual;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup.Views;

public class BlankConfigView : UserControl, IConfigView
{
  [NotNull]
  private IConfigViewSettings _settings;
  [NotNull]
  private readonly IConfigViewController _controller;
  private bool _isUpdating;
  private IContainer components;
  private TableLayoutPanel tlpPageBlank;
  private Label lblBlank;
  private TextBox tbBlank;
  private TableLayoutPanel tlpDocNumConfigs;
  private NumericUpDown udStepNumber;
  private Label lblStepNumber;
  private NumericUpDown udFirstNumber;
  private Label lblNumerical;
  private NumericUpDown udCharsCount;
  private Label lblCharsCount;
  private Label lblFirstNumber;
  private TableLayoutPanel tlpDocDependency;
  private Label lblDocType;
  private ComboBox cbxDocType;
  private Label lblGroups;
  private ComboBox cbxGroups;
  private Label lbkDocProps;
  private CheckBox cbContents;
  private CheckBox cbStatement;
  private CheckBox cbRouteCard;
  private CheckBox cbOperatingCard;
  private CheckBox cbPlaceToolIntoEmptyFields;
  private Label lblAddParams;
  private CheckBox cbNoRepeatTool;
  private CheckBox cbEmptyStringBeforeOperation;
  private CheckBox cbShowToolType;
  private CheckBox cbShopToolList;
  private CheckBox cbSketchDocument;
  private CheckBox cbPickingCardStructure;
  private CheckBox cbPartGroupDocument;
  private CheckBox cbOperationalList;
  private CheckBox cbForPartDocument;
  private CheckBox cbPickingCard;
  private CheckBox cbDoNotNumberPages;
  private CheckBox cbEnterInContents;
  private CheckBox cbDocumentNotInSet;
  private TableLayoutPanel tlpPrintConfigs;
  private ComboBox cbxNewShopSetup;
  private ComboBox cbxStepSetup;
  private ComboBox cbxToolSetup;
  private ComboBox cbxMaterialSetup;
  private Label lblPrintProps;
  private Label lblNewShopSetup;
  private Label lblStepSetup;
  private Label lblToolSetup;
  private Label lblAuxiliaryNaterialSetup;

  [NotNull]
  private BlankConfig BlankConfig => this._settings.ConfigElement as BlankConfig;

  private void SetupControls()
  {
    this._isUpdating = true;
    this.cbxDocType.BindEnumToCombobox<DocumentOwnership>(DocumentOwnership.Process, (Func<DocumentOwnership, bool>) (valueToFilter => Array.IndexOf<DocumentOwnership>(new DocumentOwnership[2]
    {
      DocumentOwnership.Operation,
      DocumentOwnership.Process
    }, valueToFilter) >= 0));
    this.lblDocType.Text = EnumTypeHelper.GetDescription(typeof (DocumentOwnership));
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid("d21520ad-fad9-474d-9fd9-4c582b902b63"));
    if (attributeType != null)
    {
      int? count = attributeType.PossibleValues?.Count;
      int num = 0;
      if (count.GetValueOrDefault() > num & count.HasValue)
      {
        this.cbxGroups.Enabled = true;
        ComboBox.ObjectCollection items = this.cbxGroups.Items;
        items.Add((object) (LocalizationHolder.rm.GetString("TechCard.Document_177") ?? throw new InvalidOperationException()));
        for (int index = 0; index < attributeType.PossibleValues.Count; ++index)
          this.cbxGroups.Items.Add((object) Convert.ToString(attributeType.PossibleValues[index]));
        goto label_8;
      }
    }
    this.cbxGroups.Enabled = false;
    this.cbxGroups.Visible = false;
label_8:
    this.cbxNewShopSetup.BindEnumToCombobox<NewShopSetupType>(NewShopSetupType.OnSelectPage);
    this.cbxStepSetup.BindEnumToCombobox<StepSetupType>(StepSetupType.StringsOtpAlternate);
    this.cbxToolSetup.BindEnumToCombobox<ToolSetupType>(ToolSetupType.InLine);
    this.cbxMaterialSetup.BindEnumToCombobox<MaterialSetupType>(MaterialSetupType.InLine);
    this.cbStatement.Visible = false;
    this.cbPickingCard.Visible = false;
    this.cbPickingCardStructure.Visible = false;
    this.cbDocumentNotInSet.Visible = false;
    this.cbPartGroupDocument.Visible = false;
    this.cbPlaceToolIntoEmptyFields.Visible = false;
    this._isUpdating = false;
  }

  private void FillControlsFromConfig()
  {
    this._isUpdating = true;
    this.tbBlank.Text = this.BlankConfig.DocumentName;
    this.udCharsCount.Value = (Decimal) this.BlankConfig.CharactersInDocumentNumber;
    this.udFirstNumber.Value = (Decimal) this.BlankConfig.FirstNumberPageInDocument;
    this.udStepNumber.Value = (Decimal) this.BlankConfig.NumberingInterval;
    this.cbxDocType.SelectedValue = (object) this.BlankConfig.DocumentType;
    this.cbContents.Checked = this.BlankConfig.Contents;
    this.cbStatement.Checked = this.BlankConfig.Statement;
    this.cbRouteCard.Checked = this.BlankConfig.RouteCard;
    this.cbOperatingCard.Checked = this.BlankConfig.OperatingCard;
    this.cbShopToolList.Checked = this.BlankConfig.ShopToolList;
    this.cbOperationalList.Checked = this.BlankConfig.OperationalList;
    this.cbPickingCard.Checked = this.BlankConfig.PickingCard;
    this.cbPickingCardStructure.Checked = this.BlankConfig.PickingCardStructure;
    this.cbEmptyStringBeforeOperation.Checked = this.BlankConfig.EmptyStringBeforeOperation;
    this.cbEnterInContents.Checked = this.BlankConfig.EnterInContents;
    this.cbDocumentNotInSet.Checked = this.BlankConfig.DocumentNotInSet;
    this.cbDoNotNumberPages.Checked = this.BlankConfig.DoNotNumberPages;
    this.cbForPartDocument.Checked = this.BlankConfig.ForPartDocument;
    this.cbPartGroupDocument.Checked = this.BlankConfig.PartGroupDocument;
    this.cbSketchDocument.Checked = this.BlankConfig.SketchDocument;
    this.cbShowToolType.Checked = this.BlankConfig.ShowToolType;
    this.cbNoRepeatTool.Checked = this.BlankConfig.NoRepeatTool;
    this.cbPlaceToolIntoEmptyFields.Checked = this.BlankConfig.PlaceToolIntoEmptyFields;
    this.cbxNewShopSetup.SelectedValue = (object) this.BlankConfig.NewShopSetup;
    this.cbxStepSetup.SelectedValue = (object) this.BlankConfig.StepSetup;
    this.cbxToolSetup.SelectedValue = (object) this.BlankConfig.ToolSetup;
    this.cbxMaterialSetup.SelectedValue = (object) this.BlankConfig.MaterialSetup;
    this._isUpdating = false;
  }

  private void SaveValuesFromControls()
  {
    this.BlankConfig.DocumentName = this.tbBlank.Enabled ? this.tbBlank.Text : string.Empty;
    this.BlankConfig.CharactersInDocumentNumber = this.udCharsCount.Enabled ? Convert.ToInt32(this.udCharsCount.Value) : 0;
    this.BlankConfig.FirstNumberPageInDocument = this.udFirstNumber.Enabled ? Convert.ToInt32(this.udFirstNumber.Value) : 0;
    this.BlankConfig.NumberingInterval = this.udStepNumber.Enabled ? Convert.ToInt32(this.udStepNumber.Value) : 0;
    this.BlankConfig.DocumentType = !this.cbxDocType.Enabled || this.cbxDocType.SelectedValue == null ? DocumentOwnership.Process : Convert.ToString(this.cbxDocType.SelectedValue).ToEnum<DocumentOwnership>();
    this.BlankConfig.Contents = this.cbContents.Enabled && this.cbContents.Checked;
    this.BlankConfig.Statement = this.cbStatement.Enabled && this.cbStatement.Checked;
    this.BlankConfig.RouteCard = this.cbRouteCard.Enabled && this.cbRouteCard.Checked;
    this.BlankConfig.OperatingCard = this.cbOperatingCard.Enabled && this.cbOperatingCard.Checked;
    this.BlankConfig.ShopToolList = this.cbShopToolList.Enabled && this.cbShopToolList.Checked;
    this.BlankConfig.OperationalList = this.cbOperationalList.Enabled && this.cbOperationalList.Checked;
    this.BlankConfig.PickingCard = this.cbPickingCard.Enabled && this.cbPickingCard.Checked;
    this.BlankConfig.PickingCardStructure = this.cbPickingCardStructure.Enabled && this.cbPickingCardStructure.Checked;
    this.BlankConfig.EmptyStringBeforeOperation = this.cbEmptyStringBeforeOperation.Enabled && this.cbEmptyStringBeforeOperation.Checked;
    this.BlankConfig.EnterInContents = this.cbEnterInContents.Enabled && this.cbEnterInContents.Checked;
    this.BlankConfig.DocumentNotInSet = this.cbDocumentNotInSet.Enabled && this.cbDocumentNotInSet.Checked;
    this.BlankConfig.DoNotNumberPages = this.cbDoNotNumberPages.Enabled && this.cbDoNotNumberPages.Checked;
    this.BlankConfig.ForPartDocument = this.cbForPartDocument.Enabled && this.cbForPartDocument.Checked;
    this.BlankConfig.PartGroupDocument = this.cbPartGroupDocument.Enabled && this.cbPartGroupDocument.Checked;
    this.BlankConfig.SketchDocument = this.cbSketchDocument.Enabled && this.cbSketchDocument.Checked;
    this.BlankConfig.ShowToolType = this.cbShowToolType.Enabled && this.cbShowToolType.Checked;
    this.BlankConfig.NoRepeatTool = this.cbNoRepeatTool.Enabled && this.cbNoRepeatTool.Checked;
    this.BlankConfig.PlaceToolIntoEmptyFields = this.cbPlaceToolIntoEmptyFields.Enabled && this.cbPlaceToolIntoEmptyFields.Checked;
    this.BlankConfig.NewShopSetup = this.cbxNewShopSetup.Enabled ? this.cbxNewShopSetup.SelectedValue.ToString().ToEnum<NewShopSetupType>() : NewShopSetupType.OnCapitalPage;
    this.BlankConfig.StepSetup = this.cbxStepSetup.Enabled ? this.cbxStepSetup.SelectedValue.ToString().ToEnum<StepSetupType>() : StepSetupType.SolidText;
    this.BlankConfig.ToolSetup = this.cbxToolSetup.Enabled ? this.cbxToolSetup.SelectedValue.ToString().ToEnum<ToolSetupType>() : ToolSetupType.InLine;
    this.BlankConfig.MaterialSetup = this.cbxMaterialSetup.Enabled ? this.cbxMaterialSetup.SelectedValue.ToString().ToEnum<MaterialSetupType>() : MaterialSetupType.InLine;
  }

  private void EnableControls()
  {
    this._isUpdating = true;
    this.tbBlank.Enabled = !this._settings.ReadOnly;
    this.udCharsCount.Enabled = !this._settings.ReadOnly;
    this.udFirstNumber.Enabled = !this._settings.ReadOnly;
    this.udStepNumber.Enabled = !this._settings.ReadOnly;
    this.cbxDocType.Enabled = !this._settings.ReadOnly;
    this.cbPartGroupDocument.Enabled = !this._settings.ReadOnly;
    DocumentOwnership documentOwnership = DocumentOwnership.Album;
    if (this.cbxDocType.SelectedIndex >= 0)
      documentOwnership = this.cbxDocType.SelectedValue.ToString().ToEnum<DocumentOwnership>();
    switch (documentOwnership)
    {
      case DocumentOwnership.Complect:
        this.cbContents.Enabled = !this._settings.ReadOnly;
        this.cbStatement.Enabled = !this._settings.ReadOnly;
        this.cbRouteCard.Enabled = false;
        this.cbOperatingCard.Enabled = false;
        this.cbShopToolList.Enabled = false;
        this.cbOperationalList.Enabled = false;
        this.cbPickingCard.Enabled = false;
        this.cbPickingCardStructure.Enabled = false;
        this.cbEnterInContents.Enabled = !this._settings.ReadOnly;
        this.cbDocumentNotInSet.Enabled = false;
        this.cbDoNotNumberPages.Enabled = !this._settings.ReadOnly;
        this.cbForPartDocument.Enabled = false;
        this.cbSketchDocument.Enabled = false;
        this.cbShowToolType.Enabled = false;
        this.cbNoRepeatTool.Enabled = false;
        this.cbPlaceToolIntoEmptyFields.Enabled = false;
        this.cbxNewShopSetup.Enabled = false;
        this.cbxStepSetup.Enabled = false;
        this.cbxToolSetup.Enabled = false;
        this.cbxMaterialSetup.Enabled = false;
        break;
      case DocumentOwnership.Album:
        this.cbContents.Enabled = !this._settings.ReadOnly;
        this.cbStatement.Enabled = !this._settings.ReadOnly;
        this.cbRouteCard.Enabled = false;
        this.cbOperatingCard.Enabled = false;
        this.cbShopToolList.Enabled = false;
        this.cbOperationalList.Enabled = false;
        this.cbPickingCard.Enabled = false;
        this.cbPickingCardStructure.Enabled = false;
        this.cbEnterInContents.Enabled = !this._settings.ReadOnly;
        this.cbDocumentNotInSet.Enabled = false;
        this.cbDoNotNumberPages.Enabled = !this._settings.ReadOnly;
        this.cbForPartDocument.Enabled = false;
        this.cbSketchDocument.Enabled = false;
        this.cbShowToolType.Enabled = false;
        this.cbNoRepeatTool.Enabled = false;
        this.cbPlaceToolIntoEmptyFields.Enabled = false;
        this.cbxNewShopSetup.Enabled = false;
        this.cbxStepSetup.Enabled = false;
        this.cbxToolSetup.Enabled = false;
        this.cbxMaterialSetup.Enabled = false;
        break;
      case DocumentOwnership.Article:
        this.cbContents.Enabled = false;
        this.cbStatement.Enabled = !this._settings.ReadOnly;
        this.cbRouteCard.Enabled = false;
        this.cbOperatingCard.Enabled = false;
        this.cbShopToolList.Enabled = !this._settings.ReadOnly;
        this.cbOperationalList.Enabled = false;
        this.cbPickingCard.Enabled = !this._settings.ReadOnly;
        this.cbPickingCardStructure.Enabled = false;
        this.cbEnterInContents.Enabled = false;
        this.cbDocumentNotInSet.Enabled = false;
        this.cbDoNotNumberPages.Enabled = false;
        this.cbForPartDocument.Enabled = false;
        this.cbSketchDocument.Enabled = false;
        this.cbShowToolType.Enabled = false;
        this.cbNoRepeatTool.Enabled = false;
        this.cbPlaceToolIntoEmptyFields.Enabled = false;
        this.cbxNewShopSetup.Enabled = false;
        this.cbxStepSetup.Enabled = false;
        this.cbxToolSetup.Enabled = false;
        this.cbxMaterialSetup.Enabled = false;
        break;
      case DocumentOwnership.Process:
        this.cbContents.Enabled = false;
        this.cbStatement.Enabled = false;
        this.cbRouteCard.Enabled = !this._settings.ReadOnly;
        this.cbOperatingCard.Enabled = false;
        this.cbShopToolList.Enabled = false;
        this.cbOperationalList.Enabled = !this._settings.ReadOnly;
        this.cbPickingCard.Enabled = false;
        this.cbPickingCardStructure.Enabled = !this._settings.ReadOnly;
        this.cbEnterInContents.Enabled = false;
        this.cbDocumentNotInSet.Enabled = !this._settings.ReadOnly;
        this.cbDoNotNumberPages.Enabled = !this._settings.ReadOnly;
        this.cbForPartDocument.Enabled = !this._settings.ReadOnly;
        this.cbSketchDocument.Enabled = !this._settings.ReadOnly;
        this.cbShowToolType.Enabled = !this._settings.ReadOnly;
        this.cbNoRepeatTool.Enabled = !this._settings.ReadOnly;
        this.cbPlaceToolIntoEmptyFields.Enabled = false;
        this.cbxNewShopSetup.Enabled = !this._settings.ReadOnly;
        this.cbxStepSetup.Enabled = !this._settings.ReadOnly;
        this.cbxToolSetup.Enabled = !this._settings.ReadOnly;
        this.cbxMaterialSetup.Enabled = !this._settings.ReadOnly;
        break;
      case DocumentOwnership.OperGroup:
        this.cbContents.Enabled = false;
        this.cbStatement.Enabled = false;
        this.cbRouteCard.Enabled = false;
        this.cbOperatingCard.Enabled = !this._settings.ReadOnly;
        this.cbShopToolList.Enabled = false;
        this.cbOperationalList.Enabled = !this._settings.ReadOnly;
        this.cbPickingCard.Enabled = false;
        this.cbPickingCardStructure.Enabled = !this._settings.ReadOnly;
        this.cbEnterInContents.Enabled = false;
        this.cbDocumentNotInSet.Enabled = !this._settings.ReadOnly;
        this.cbDoNotNumberPages.Enabled = !this._settings.ReadOnly;
        this.cbForPartDocument.Enabled = !this._settings.ReadOnly;
        this.cbSketchDocument.Enabled = !this._settings.ReadOnly;
        this.cbShowToolType.Enabled = !this._settings.ReadOnly;
        this.cbNoRepeatTool.Enabled = !this._settings.ReadOnly;
        this.cbPlaceToolIntoEmptyFields.Enabled = !this._settings.ReadOnly;
        this.cbxNewShopSetup.Enabled = !this._settings.ReadOnly;
        this.cbxStepSetup.Enabled = !this._settings.ReadOnly;
        this.cbxToolSetup.Enabled = !this._settings.ReadOnly;
        this.cbxMaterialSetup.Enabled = !this._settings.ReadOnly;
        break;
      case DocumentOwnership.Operation:
        this.cbContents.Enabled = false;
        this.cbStatement.Enabled = false;
        this.cbRouteCard.Enabled = false;
        this.cbOperatingCard.Enabled = !this._settings.ReadOnly;
        this.cbShopToolList.Enabled = false;
        this.cbOperationalList.Enabled = false;
        this.cbPickingCard.Enabled = false;
        this.cbPickingCardStructure.Enabled = !this._settings.ReadOnly;
        this.cbEnterInContents.Enabled = false;
        this.cbDocumentNotInSet.Enabled = !this._settings.ReadOnly;
        this.cbDoNotNumberPages.Enabled = !this._settings.ReadOnly;
        this.cbForPartDocument.Enabled = !this._settings.ReadOnly;
        this.cbSketchDocument.Enabled = !this._settings.ReadOnly;
        this.cbShowToolType.Enabled = !this._settings.ReadOnly;
        this.cbNoRepeatTool.Enabled = !this._settings.ReadOnly;
        this.cbPlaceToolIntoEmptyFields.Enabled = false;
        this.cbxNewShopSetup.Enabled = !this._settings.ReadOnly;
        this.cbxStepSetup.Enabled = !this._settings.ReadOnly;
        this.cbxToolSetup.Enabled = !this._settings.ReadOnly;
        this.cbxMaterialSetup.Enabled = !this._settings.ReadOnly;
        break;
      case DocumentOwnership.InstrumentPosition:
        this.cbContents.Enabled = false;
        this.cbStatement.Enabled = false;
        this.cbRouteCard.Enabled = false;
        this.cbOperatingCard.Enabled = false;
        this.cbShopToolList.Enabled = false;
        this.cbOperationalList.Enabled = false;
        this.cbPickingCard.Enabled = false;
        this.cbPickingCardStructure.Enabled = false;
        this.cbEnterInContents.Enabled = false;
        this.cbDocumentNotInSet.Enabled = !this._settings.ReadOnly;
        this.cbDoNotNumberPages.Enabled = !this._settings.ReadOnly;
        this.cbForPartDocument.Enabled = false;
        this.cbSketchDocument.Enabled = !this._settings.ReadOnly;
        this.cbShowToolType.Enabled = false;
        this.cbNoRepeatTool.Enabled = false;
        this.cbPlaceToolIntoEmptyFields.Enabled = false;
        this.cbxNewShopSetup.Enabled = !this._settings.ReadOnly;
        this.cbxStepSetup.Enabled = !this._settings.ReadOnly;
        this.cbxToolSetup.Enabled = !this._settings.ReadOnly;
        this.cbxMaterialSetup.Enabled = !this._settings.ReadOnly;
        break;
    }
    this.cbEmptyStringBeforeOperation.Enabled = this.cbOperationalList.Enabled && this.cbOperationalList.Checked;
    if (!this._settings.ReadOnly)
      this.UncheckDisabledCheckboxes((Control) this);
    this._isUpdating = false;
  }

  private void UncheckDisabledCheckboxes([NotNull] Control parent)
  {
    foreach (object control in (ArrangedElementCollection) parent.Controls)
    {
      if (control is CheckBox checkBox && !checkBox.Enabled)
        checkBox.Checked = false;
      else
        this.UncheckDisabledCheckboxes(control as Control);
    }
  }

  private void SetDocKindGroupValue(CheckBox source)
  {
    if (source == this.cbContents)
    {
      if (!this.cbContents.Checked)
        return;
      this.cbStatement.Checked = false;
      this.cbShopToolList.Checked = false;
      this.cbPickingCard.Checked = false;
    }
    else if (source == this.cbStatement)
    {
      if (!this.cbStatement.Checked)
        return;
      this.cbContents.Checked = false;
      this.cbShopToolList.Checked = false;
      this.cbPickingCard.Checked = false;
    }
    else if (source == this.cbShopToolList)
    {
      if (!this.cbShopToolList.Checked)
        return;
      this.cbContents.Checked = false;
      this.cbStatement.Checked = false;
      this.cbPickingCard.Checked = false;
    }
    else
    {
      if (source != this.cbPickingCard || !this.cbPickingCard.Checked)
        return;
      this.cbContents.Checked = false;
      this.cbStatement.Checked = false;
      this.cbShopToolList.Checked = false;
    }
  }

  private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.EnableControls();
    bool flag = false;
    if (sender == this.cbxDocType)
      flag = this.cbxDocType.SelectedValue.ToString().ToEnum<DocumentOwnership>() != this.BlankConfig.DocumentType;
    else if (sender == this.cbxNewShopSetup)
      flag = this.cbxNewShopSetup.SelectedValue.ToString().ToEnum<NewShopSetupType>() != this.BlankConfig.NewShopSetup;
    else if (sender != this.cbxGroups)
    {
      if (sender == this.cbxStepSetup)
        flag = this.cbxStepSetup.SelectedValue.ToString().ToEnum<StepSetupType>() != this.BlankConfig.StepSetup;
      else if (sender == this.cbxToolSetup)
        flag = this.cbxToolSetup.SelectedValue.ToString().ToEnum<ToolSetupType>() != this.BlankConfig.ToolSetup;
      else if (sender == this.cbxMaterialSetup)
        flag = this.cbxMaterialSetup.SelectedValue.ToString().ToEnum<MaterialSetupType>() != this.BlankConfig.MaterialSetup;
    }
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  private void CheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    if (Array.IndexOf<CheckBox>(new CheckBox[5]
    {
      this.cbContents,
      this.cbStatement,
      this.cbShopToolList,
      this.cbPickingCard,
      this.cbOperationalList
    }, sender as CheckBox) >= 0)
      this.SetDocKindGroupValue(sender as CheckBox);
    this.EnableControls();
    bool flag = false;
    if (sender == this.cbContents)
      flag = this.cbContents.Checked != this.BlankConfig.Contents;
    else if (sender == this.cbStatement)
      flag = this.cbStatement.Checked != this.BlankConfig.Statement;
    else if (sender == this.cbShopToolList)
      flag = this.cbShopToolList.Checked != this.BlankConfig.ShopToolList;
    else if (sender == this.cbPickingCard)
      flag = this.cbPickingCard.Checked != this.BlankConfig.PickingCard;
    else if (sender == this.cbOperationalList)
      flag = this.cbOperationalList.Checked != this.BlankConfig.OperationalList;
    else if (sender == this.cbRouteCard)
      flag = this.cbRouteCard.Checked != this.BlankConfig.RouteCard;
    else if (sender == this.cbOperatingCard)
      flag = this.cbOperatingCard.Checked != this.BlankConfig.OperatingCard;
    else if (sender == this.cbPlaceToolIntoEmptyFields)
      flag = this.cbPlaceToolIntoEmptyFields.Checked != this.BlankConfig.PlaceToolIntoEmptyFields;
    else if (sender == this.cbNoRepeatTool)
      flag = this.cbNoRepeatTool.Checked != this.BlankConfig.NoRepeatTool;
    else if (sender == this.cbEmptyStringBeforeOperation)
      flag = this.cbEmptyStringBeforeOperation.Checked != this.BlankConfig.EmptyStringBeforeOperation;
    else if (sender == this.cbShowToolType)
      flag = this.cbShowToolType.Checked != this.BlankConfig.ShowToolType;
    else if (sender == this.cbSketchDocument)
      flag = this.cbSketchDocument.Checked != this.BlankConfig.SketchDocument;
    else if (sender == this.cbPickingCardStructure)
      flag = this.cbPickingCardStructure.Checked != this.BlankConfig.PickingCardStructure;
    else if (sender == this.cbForPartDocument)
      flag = this.cbForPartDocument.Checked != this.BlankConfig.ForPartDocument;
    else if (sender == this.cbDoNotNumberPages)
      flag = this.cbDoNotNumberPages.Checked != this.BlankConfig.DoNotNumberPages;
    else if (sender == this.cbEnterInContents)
      flag = this.cbEnterInContents.Checked != this.BlankConfig.EnterInContents;
    else if (sender == this.cbDocumentNotInSet)
      flag = this.cbDocumentNotInSet.Checked != this.BlankConfig.DocumentNotInSet;
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  private void NumericUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    bool flag = false;
    if (sender == this.udCharsCount)
      flag = this.udCharsCount.Value != (Decimal) this.BlankConfig.CharactersInDocumentNumber;
    else if (sender == this.udFirstNumber)
      flag = this.udFirstNumber.Value != (Decimal) this.BlankConfig.FirstNumberPageInDocument;
    else if (sender == this.udStepNumber)
      flag = this.udStepNumber.Value != (Decimal) this.BlankConfig.NumberingInterval;
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  private void tbBlank_TextChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, this.tbBlank.Text != this.BlankConfig.DocumentName);
  }

  public bool ApplyChanges(out IDocumentConfigElement config)
  {
    config = (IDocumentConfigElement) this.BlankConfig;
    if (this._settings.ReadOnly)
      return false;
    this.SaveValuesFromControls();
    return true;
  }

  public void CancelChanges()
  {
    if (this._settings.ReadOnly)
      return;
    this.SetupView(this._settings);
  }

  public void SetupView(IConfigViewSettings settings)
  {
    this._settings = settings;
    this.FillControlsFromConfig();
    this.EnableControls();
  }

  public BlankConfigView([NotNull] IConfigViewController controller, System.IServiceProvider services)
  {
    this.InitializeComponent();
    this._controller = controller;
    this.SetupControls();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tlpPageBlank = new TableLayoutPanel();
    this.lblBlank = new Label();
    this.tbBlank = new TextBox();
    this.tlpDocNumConfigs = new TableLayoutPanel();
    this.udStepNumber = new NumericUpDown();
    this.lblStepNumber = new Label();
    this.udFirstNumber = new NumericUpDown();
    this.lblNumerical = new Label();
    this.udCharsCount = new NumericUpDown();
    this.lblCharsCount = new Label();
    this.lblFirstNumber = new Label();
    this.tlpDocDependency = new TableLayoutPanel();
    this.lblDocType = new Label();
    this.cbxDocType = new ComboBox();
    this.lblGroups = new Label();
    this.cbxGroups = new ComboBox();
    this.lbkDocProps = new Label();
    this.cbContents = new CheckBox();
    this.cbStatement = new CheckBox();
    this.cbRouteCard = new CheckBox();
    this.cbOperatingCard = new CheckBox();
    this.cbPlaceToolIntoEmptyFields = new CheckBox();
    this.lblAddParams = new Label();
    this.cbNoRepeatTool = new CheckBox();
    this.cbEmptyStringBeforeOperation = new CheckBox();
    this.cbShowToolType = new CheckBox();
    this.cbShopToolList = new CheckBox();
    this.cbSketchDocument = new CheckBox();
    this.cbPickingCardStructure = new CheckBox();
    this.cbPartGroupDocument = new CheckBox();
    this.cbOperationalList = new CheckBox();
    this.cbForPartDocument = new CheckBox();
    this.cbPickingCard = new CheckBox();
    this.cbDoNotNumberPages = new CheckBox();
    this.cbEnterInContents = new CheckBox();
    this.cbDocumentNotInSet = new CheckBox();
    this.tlpPrintConfigs = new TableLayoutPanel();
    this.cbxNewShopSetup = new ComboBox();
    this.cbxStepSetup = new ComboBox();
    this.cbxToolSetup = new ComboBox();
    this.cbxMaterialSetup = new ComboBox();
    this.lblPrintProps = new Label();
    this.lblNewShopSetup = new Label();
    this.lblStepSetup = new Label();
    this.lblToolSetup = new Label();
    this.lblAuxiliaryNaterialSetup = new Label();
    this.tlpPageBlank.SuspendLayout();
    this.tlpDocNumConfigs.SuspendLayout();
    this.udStepNumber.BeginInit();
    this.udFirstNumber.BeginInit();
    this.udCharsCount.BeginInit();
    this.tlpDocDependency.SuspendLayout();
    this.tlpPrintConfigs.SuspendLayout();
    this.SuspendLayout();
    this.tlpPageBlank.ColumnCount = 5;
    this.tlpPageBlank.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageBlank.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250f));
    this.tlpPageBlank.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageBlank.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpPageBlank.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageBlank.Controls.Add((Control) this.lblBlank, 1, 1);
    this.tlpPageBlank.Controls.Add((Control) this.tbBlank, 1, 2);
    this.tlpPageBlank.Controls.Add((Control) this.tlpDocNumConfigs, 1, 4);
    this.tlpPageBlank.Controls.Add((Control) this.tlpDocDependency, 3, 4);
    this.tlpPageBlank.Controls.Add((Control) this.lbkDocProps, 1, 6);
    this.tlpPageBlank.Controls.Add((Control) this.cbContents, 1, 7);
    this.tlpPageBlank.Controls.Add((Control) this.cbStatement, 1, 8);
    this.tlpPageBlank.Controls.Add((Control) this.cbRouteCard, 1, 9);
    this.tlpPageBlank.Controls.Add((Control) this.cbOperatingCard, 1, 10);
    this.tlpPageBlank.Controls.Add((Control) this.lblAddParams, 3, 6);
    this.tlpPageBlank.Controls.Add((Control) this.cbEmptyStringBeforeOperation, 1, 15);
    this.tlpPageBlank.Controls.Add((Control) this.cbShopToolList, 1, 11);
    this.tlpPageBlank.Controls.Add((Control) this.cbPickingCardStructure, 1, 14);
    this.tlpPageBlank.Controls.Add((Control) this.cbOperationalList, 1, 12);
    this.tlpPageBlank.Controls.Add((Control) this.cbPickingCard, 1, 13);
    this.tlpPageBlank.Controls.Add((Control) this.cbEnterInContents, 3, 7);
    this.tlpPageBlank.Controls.Add((Control) this.cbDocumentNotInSet, 3, 8);
    this.tlpPageBlank.Controls.Add((Control) this.tlpPrintConfigs, 1, 17);
    this.tlpPageBlank.Controls.Add((Control) this.cbForPartDocument, 3, 9);
    this.tlpPageBlank.Controls.Add((Control) this.cbPartGroupDocument, 3, 10);
    this.tlpPageBlank.Controls.Add((Control) this.cbSketchDocument, 3, 11);
    this.tlpPageBlank.Controls.Add((Control) this.cbShowToolType, 3, 12);
    this.tlpPageBlank.Controls.Add((Control) this.cbNoRepeatTool, 3, 13);
    this.tlpPageBlank.Controls.Add((Control) this.cbPlaceToolIntoEmptyFields, 3, 14);
    this.tlpPageBlank.Controls.Add((Control) this.cbDoNotNumberPages, 3, 15);
    this.tlpPageBlank.Dock = DockStyle.Fill;
    this.tlpPageBlank.Location = new Point(0, 0);
    this.tlpPageBlank.Margin = new Padding(0);
    this.tlpPageBlank.Name = "tlpPageBlank";
    this.tlpPageBlank.RowCount = 18;
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 10f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 95f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageBlank.RowStyles.Add(new RowStyle(SizeType.Absolute, 120f));
    this.tlpPageBlank.Size = new Size(517, 686);
    this.tlpPageBlank.TabIndex = 131;
    this.lblBlank.AutoSize = true;
    this.lblBlank.Dock = DockStyle.Fill;
    this.lblBlank.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblBlank.Location = new Point(10, 10);
    this.lblBlank.Margin = new Padding(0);
    this.lblBlank.Name = "lblBlank";
    this.lblBlank.Size = new Size(250, 20);
    this.lblBlank.TabIndex = (int) sbyte.MaxValue;
    this.lblBlank.Text = "Наименование документа:";
    this.tbBlank.Dock = DockStyle.Fill;
    this.tbBlank.Location = new Point(10, 30);
    this.tbBlank.Margin = new Padding(0);
    this.tbBlank.Name = "tbBlank";
    this.tbBlank.Size = new Size(250, 20);
    this.tbBlank.TabIndex = 108;
    this.tbBlank.TextChanged += new EventHandler(this.tbBlank_TextChanged);
    this.tlpDocNumConfigs.ColumnCount = 2;
    this.tlpDocNumConfigs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150f));
    this.tlpDocNumConfigs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpDocNumConfigs.Controls.Add((Control) this.udStepNumber, 1, 3);
    this.tlpDocNumConfigs.Controls.Add((Control) this.lblStepNumber, 0, 3);
    this.tlpDocNumConfigs.Controls.Add((Control) this.udFirstNumber, 1, 2);
    this.tlpDocNumConfigs.Controls.Add((Control) this.lblNumerical, 0, 0);
    this.tlpDocNumConfigs.Controls.Add((Control) this.udCharsCount, 1, 1);
    this.tlpDocNumConfigs.Controls.Add((Control) this.lblCharsCount, 0, 1);
    this.tlpDocNumConfigs.Controls.Add((Control) this.lblFirstNumber, 0, 2);
    this.tlpDocNumConfigs.Dock = DockStyle.Fill;
    this.tlpDocNumConfigs.Location = new Point(10, 70);
    this.tlpDocNumConfigs.Margin = new Padding(0);
    this.tlpDocNumConfigs.Name = "tlpDocNumConfigs";
    this.tlpDocNumConfigs.RowCount = 4;
    this.tlpDocNumConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpDocNumConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpDocNumConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpDocNumConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpDocNumConfigs.Size = new Size(250, 95);
    this.tlpDocNumConfigs.TabIndex = 3;
    this.udStepNumber.Location = new Point(150, 70);
    this.udStepNumber.Margin = new Padding(0);
    this.udStepNumber.Name = "udStepNumber";
    this.udStepNumber.Size = new Size(50, 20);
    this.udStepNumber.TabIndex = 128 /*0x80*/;
    this.udStepNumber.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this.lblStepNumber.AutoSize = true;
    this.lblStepNumber.Location = new Point(0, 72);
    this.lblStepNumber.Margin = new Padding(0, 2, 0, 0);
    this.lblStepNumber.Name = "lblStepNumber";
    this.lblStepNumber.Size = new Size(117, 13);
    this.lblStepNumber.TabIndex = 115;
    this.lblStepNumber.Text = "Интервал нумерации:";
    this.udFirstNumber.Location = new Point(150, 45);
    this.udFirstNumber.Margin = new Padding(0);
    this.udFirstNumber.Name = "udFirstNumber";
    this.udFirstNumber.Size = new Size(50, 20);
    this.udFirstNumber.TabIndex = (int) sbyte.MaxValue;
    this.udFirstNumber.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this.lblNumerical.AutoSize = true;
    this.tlpDocNumConfigs.SetColumnSpan((Control) this.lblNumerical, 2);
    this.lblNumerical.Dock = DockStyle.Fill;
    this.lblNumerical.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblNumerical.Location = new Point(0, 0);
    this.lblNumerical.Margin = new Padding(0);
    this.lblNumerical.Name = "lblNumerical";
    this.lblNumerical.Size = new Size(250, 20);
    this.lblNumerical.TabIndex = 109;
    this.lblNumerical.Text = "Нумерация документа:";
    this.udCharsCount.Location = new Point(150, 20);
    this.udCharsCount.Margin = new Padding(0);
    this.udCharsCount.Name = "udCharsCount";
    this.udCharsCount.Size = new Size(50, 20);
    this.udCharsCount.TabIndex = 126;
    this.udCharsCount.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this.lblCharsCount.AutoSize = true;
    this.lblCharsCount.Location = new Point(0, 22);
    this.lblCharsCount.Margin = new Padding(0, 2, 0, 0);
    this.lblCharsCount.Name = "lblCharsCount";
    this.lblCharsCount.Size = new Size(111, 13);
    this.lblCharsCount.TabIndex = 111;
    this.lblCharsCount.Text = "Cимволов в номере:";
    this.lblFirstNumber.AutoSize = true;
    this.lblFirstNumber.Location = new Point(0, 47);
    this.lblFirstNumber.Margin = new Padding(0, 2, 0, 0);
    this.lblFirstNumber.Name = "lblFirstNumber";
    this.lblFirstNumber.Size = new Size(115, 13);
    this.lblFirstNumber.TabIndex = 113;
    this.lblFirstNumber.Text = "Начать нумерацию с:";
    this.tlpDocDependency.ColumnCount = 1;
    this.tlpDocDependency.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpDocDependency.Controls.Add((Control) this.lblDocType, 0, 0);
    this.tlpDocDependency.Controls.Add((Control) this.cbxDocType, 0, 1);
    this.tlpDocDependency.Controls.Add((Control) this.lblGroups, 0, 2);
    this.tlpDocDependency.Controls.Add((Control) this.cbxGroups, 0, 3);
    this.tlpDocDependency.Dock = DockStyle.Fill;
    this.tlpDocDependency.Location = new Point(270, 70);
    this.tlpDocDependency.Margin = new Padding(0);
    this.tlpDocDependency.Name = "tlpDocDependency";
    this.tlpDocDependency.RowCount = 4;
    this.tlpDocDependency.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpDocDependency.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpDocDependency.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpDocDependency.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpDocDependency.Size = new Size(237, 95);
    this.tlpDocDependency.TabIndex = 128 /*0x80*/;
    this.lblDocType.AutoSize = true;
    this.lblDocType.Dock = DockStyle.Fill;
    this.lblDocType.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblDocType.Location = new Point(0, 0);
    this.lblDocType.Margin = new Padding(0);
    this.lblDocType.Name = "lblDocType";
    this.lblDocType.Size = new Size(237, 20);
    this.lblDocType.TabIndex = 85;
    this.lblDocType.Text = "Принадлежность документа:";
    this.cbxDocType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxDocType.FormattingEnabled = true;
    this.cbxDocType.Location = new Point(0, 20);
    this.cbxDocType.Margin = new Padding(0);
    this.cbxDocType.Name = "cbxDocType";
    this.cbxDocType.Size = new Size(220, 21);
    this.cbxDocType.TabIndex = 86;
    this.cbxDocType.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
    this.lblGroups.AutoSize = true;
    this.lblGroups.Dock = DockStyle.Fill;
    this.lblGroups.Location = new Point(0, 50);
    this.lblGroups.Margin = new Padding(0, 5, 0, 0);
    this.lblGroups.Name = "lblGroups";
    this.lblGroups.Size = new Size(237, 20);
    this.lblGroups.TabIndex = 87;
    this.lblGroups.Text = "Группа документов";
    this.cbxGroups.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxGroups.FormattingEnabled = true;
    this.cbxGroups.Location = new Point(0, 70);
    this.cbxGroups.Margin = new Padding(0);
    this.cbxGroups.Name = "cbxGroups";
    this.cbxGroups.Size = new Size(220, 21);
    this.cbxGroups.TabIndex = 88;
    this.cbxGroups.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
    this.lbkDocProps.AutoSize = true;
    this.lbkDocProps.Dock = DockStyle.Fill;
    this.lbkDocProps.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lbkDocProps.Location = new Point(10, 185);
    this.lbkDocProps.Margin = new Padding(0);
    this.lbkDocProps.Name = "lbkDocProps";
    this.lbkDocProps.Size = new Size(250, 20);
    this.lbkDocProps.TabIndex = 116;
    this.lbkDocProps.Text = "Вид документа:";
    this.cbContents.AutoSize = true;
    this.cbContents.Location = new Point(10, 205);
    this.cbContents.Margin = new Padding(0);
    this.cbContents.Name = "cbContents";
    this.cbContents.Size = new Size(87, 17);
    this.cbContents.TabIndex = 99;
    this.cbContents.Text = "Оглавление";
    this.cbContents.UseVisualStyleBackColor = true;
    this.cbContents.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbStatement.AutoSize = true;
    this.cbStatement.Location = new Point(10, 230);
    this.cbStatement.Margin = new Padding(0);
    this.cbStatement.Name = "cbStatement";
    this.cbStatement.Size = new Size(233, 17);
    this.cbStatement.TabIndex = 98;
    this.cbStatement.Text = "Ведомость технологических документов";
    this.cbStatement.UseVisualStyleBackColor = true;
    this.cbStatement.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbRouteCard.AutoSize = true;
    this.cbRouteCard.Location = new Point(10, (int) byte.MaxValue);
    this.cbRouteCard.Margin = new Padding(0);
    this.cbRouteCard.Name = "cbRouteCard";
    this.cbRouteCard.Size = new Size(121, 17);
    this.cbRouteCard.TabIndex = 100;
    this.cbRouteCard.Text = "Маршрутная карта";
    this.cbRouteCard.UseVisualStyleBackColor = true;
    this.cbRouteCard.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbOperatingCard.AutoSize = true;
    this.cbOperatingCard.Location = new Point(10, 280);
    this.cbOperatingCard.Margin = new Padding(0);
    this.cbOperatingCard.Name = "cbOperatingCard";
    this.cbOperatingCard.Size = new Size(132, 17);
    this.cbOperatingCard.TabIndex = 101;
    this.cbOperatingCard.Text = "Операционная карта";
    this.cbOperatingCard.UseVisualStyleBackColor = true;
    this.cbOperatingCard.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbPlaceToolIntoEmptyFields.AutoSize = true;
    this.cbPlaceToolIntoEmptyFields.Location = new Point(270, 380);
    this.cbPlaceToolIntoEmptyFields.Margin = new Padding(0);
    this.cbPlaceToolIntoEmptyFields.Name = "cbPlaceToolIntoEmptyFields";
    this.cbPlaceToolIntoEmptyFields.Size = new Size(204, 17);
    this.cbPlaceToolIntoEmptyFields.TabIndex = 96 /*0x60*/;
    this.cbPlaceToolIntoEmptyFields.Text = "Помещать оснастку в пустые поля";
    this.cbPlaceToolIntoEmptyFields.UseVisualStyleBackColor = true;
    this.cbPlaceToolIntoEmptyFields.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.lblAddParams.AutoSize = true;
    this.lblAddParams.Dock = DockStyle.Fill;
    this.lblAddParams.Location = new Point(270, 185);
    this.lblAddParams.Margin = new Padding(0);
    this.lblAddParams.Name = "lblAddParams";
    this.lblAddParams.Size = new Size(237, 20);
    this.lblAddParams.TabIndex = 97;
    this.lblAddParams.Text = "Дополнительные параметры:";
    this.cbNoRepeatTool.AutoSize = true;
    this.cbNoRepeatTool.Location = new Point(270, 355);
    this.cbNoRepeatTool.Margin = new Padding(0);
    this.cbNoRepeatTool.Name = "cbNoRepeatTool";
    this.cbNoRepeatTool.Size = new Size(144 /*0x90*/, 17);
    this.cbNoRepeatTool.TabIndex = 95;
    this.cbNoRepeatTool.Text = "Не повторять оснастку";
    this.cbNoRepeatTool.UseVisualStyleBackColor = true;
    this.cbNoRepeatTool.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbEmptyStringBeforeOperation.AutoSize = true;
    this.cbEmptyStringBeforeOperation.Location = new Point(10, 405);
    this.cbEmptyStringBeforeOperation.Margin = new Padding(0);
    this.cbEmptyStringBeforeOperation.Name = "cbEmptyStringBeforeOperation";
    this.cbEmptyStringBeforeOperation.Size = new Size(190, 17);
    this.cbEmptyStringBeforeOperation.TabIndex = 106;
    this.cbEmptyStringBeforeOperation.Text = "Пустая строка перед операцией";
    this.cbEmptyStringBeforeOperation.UseVisualStyleBackColor = true;
    this.cbEmptyStringBeforeOperation.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbShowToolType.AutoSize = true;
    this.cbShowToolType.Location = new Point(270, 330);
    this.cbShowToolType.Margin = new Padding(0);
    this.cbShowToolType.Name = "cbShowToolType";
    this.cbShowToolType.Size = new Size(160 /*0xA0*/, 17);
    this.cbShowToolType.TabIndex = 94;
    this.cbShowToolType.Text = "Показывать вид оснастки";
    this.cbShowToolType.UseVisualStyleBackColor = true;
    this.cbShowToolType.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbShopToolList.AutoSize = true;
    this.cbShopToolList.Location = new Point(10, 305);
    this.cbShopToolList.Margin = new Padding(0);
    this.cbShopToolList.Name = "cbShopToolList";
    this.cbShopToolList.Size = new Size(189, 17);
    this.cbShopToolList.TabIndex = 102;
    this.cbShopToolList.Text = "Поцеховая ведомость оснастки";
    this.cbShopToolList.UseVisualStyleBackColor = true;
    this.cbShopToolList.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbSketchDocument.AutoSize = true;
    this.cbSketchDocument.Location = new Point(270, 305);
    this.cbSketchDocument.Margin = new Padding(0);
    this.cbSketchDocument.Name = "cbSketchDocument";
    this.cbSketchDocument.Size = new Size(133, 17);
    this.cbSketchDocument.TabIndex = 93;
    this.cbSketchDocument.Text = "Документ с эскизом";
    this.cbSketchDocument.UseVisualStyleBackColor = true;
    this.cbSketchDocument.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbPickingCardStructure.AutoSize = true;
    this.cbPickingCardStructure.Location = new Point(10, 380);
    this.cbPickingCardStructure.Margin = new Padding(0);
    this.cbPickingCardStructure.Name = "cbPickingCardStructure";
    this.cbPickingCardStructure.Size = new Size(204, 17);
    this.cbPickingCardStructure.TabIndex = 105;
    this.cbPickingCardStructure.Text = "Структура комплектовочной карты";
    this.cbPickingCardStructure.UseVisualStyleBackColor = true;
    this.cbPickingCardStructure.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbPartGroupDocument.AutoSize = true;
    this.cbPartGroupDocument.Location = new Point(270, 280);
    this.cbPartGroupDocument.Margin = new Padding(0);
    this.cbPartGroupDocument.Name = "cbPartGroupDocument";
    this.cbPartGroupDocument.Size = new Size(173, 17);
    this.cbPartGroupDocument.TabIndex = 92;
    this.cbPartGroupDocument.Text = "Документ на группу изделий";
    this.cbPartGroupDocument.UseVisualStyleBackColor = true;
    this.cbPartGroupDocument.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbOperationalList.AutoSize = true;
    this.cbOperationalList.Location = new Point(10, 330);
    this.cbOperationalList.Margin = new Padding(0);
    this.cbOperationalList.Name = "cbOperationalList";
    this.cbOperationalList.Size = new Size(170, 17);
    this.cbOperationalList.TabIndex = 103;
    this.cbOperationalList.Text = "Пооперационная ведомость";
    this.cbOperationalList.UseVisualStyleBackColor = true;
    this.cbOperationalList.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbForPartDocument.AutoSize = true;
    this.cbForPartDocument.Location = new Point(270, (int) byte.MaxValue);
    this.cbForPartDocument.Margin = new Padding(0);
    this.cbForPartDocument.Name = "cbForPartDocument";
    this.cbForPartDocument.Size = new Size(146, 17);
    this.cbForPartDocument.TabIndex = 91;
    this.cbForPartDocument.Text = "Подетальный документ";
    this.cbForPartDocument.UseVisualStyleBackColor = true;
    this.cbForPartDocument.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbPickingCard.AutoSize = true;
    this.cbPickingCard.Location = new Point(10, 355);
    this.cbPickingCard.Margin = new Padding(0);
    this.cbPickingCard.Name = "cbPickingCard";
    this.cbPickingCard.Size = new Size(149, 17);
    this.cbPickingCard.TabIndex = 104;
    this.cbPickingCard.Text = "Комплектовочная карта";
    this.cbPickingCard.UseVisualStyleBackColor = true;
    this.cbPickingCard.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbDoNotNumberPages.AutoSize = true;
    this.cbDoNotNumberPages.Location = new Point(270, 405);
    this.cbDoNotNumberPages.Margin = new Padding(0);
    this.cbDoNotNumberPages.Name = "cbDoNotNumberPages";
    this.cbDoNotNumberPages.Size = new Size(155, 17);
    this.cbDoNotNumberPages.TabIndex = 90;
    this.cbDoNotNumberPages.Text = "Не нумеровать страницы";
    this.cbDoNotNumberPages.UseVisualStyleBackColor = true;
    this.cbDoNotNumberPages.Visible = false;
    this.cbDoNotNumberPages.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbEnterInContents.AutoSize = true;
    this.cbEnterInContents.Location = new Point(270, 205);
    this.cbEnterInContents.Margin = new Padding(0);
    this.cbEnterInContents.Name = "cbEnterInContents";
    this.cbEnterInContents.Size = new Size(139, 17);
    this.cbEnterInContents.TabIndex = 83;
    this.cbEnterInContents.Text = "Вносить в оглавление";
    this.cbEnterInContents.UseVisualStyleBackColor = true;
    this.cbEnterInContents.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.cbDocumentNotInSet.AutoSize = true;
    this.cbDocumentNotInSet.Location = new Point(270, 230);
    this.cbDocumentNotInSet.Margin = new Padding(0);
    this.cbDocumentNotInSet.Name = "cbDocumentNotInSet";
    this.cbDocumentNotInSet.Size = new Size(159, 17);
    this.cbDocumentNotInSet.TabIndex = 89;
    this.cbDocumentNotInSet.Text = "Документ не в комплекте";
    this.cbDocumentNotInSet.UseVisualStyleBackColor = true;
    this.cbDocumentNotInSet.CheckedChanged += new EventHandler(this.CheckBox_CheckedChanged);
    this.tlpPrintConfigs.ColumnCount = 2;
    this.tlpPrintConfigs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150f));
    this.tlpPrintConfigs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpPrintConfigs.Controls.Add((Control) this.cbxNewShopSetup, 1, 1);
    this.tlpPrintConfigs.Controls.Add((Control) this.cbxStepSetup, 1, 2);
    this.tlpPrintConfigs.Controls.Add((Control) this.cbxToolSetup, 1, 3);
    this.tlpPrintConfigs.Controls.Add((Control) this.cbxMaterialSetup, 1, 4);
    this.tlpPrintConfigs.Controls.Add((Control) this.lblPrintProps, 0, 0);
    this.tlpPrintConfigs.Controls.Add((Control) this.lblNewShopSetup, 0, 1);
    this.tlpPrintConfigs.Controls.Add((Control) this.lblStepSetup, 0, 2);
    this.tlpPrintConfigs.Controls.Add((Control) this.lblToolSetup, 0, 3);
    this.tlpPrintConfigs.Controls.Add((Control) this.lblAuxiliaryNaterialSetup, 0, 4);
    this.tlpPrintConfigs.Dock = DockStyle.Fill;
    this.tlpPrintConfigs.Location = new Point(10, 450);
    this.tlpPrintConfigs.Margin = new Padding(0);
    this.tlpPrintConfigs.Name = "tlpPrintConfigs";
    this.tlpPrintConfigs.RowCount = 5;
    this.tlpPrintConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPrintConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPrintConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPrintConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPrintConfigs.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPrintConfigs.Size = new Size(250, 236);
    this.tlpPrintConfigs.TabIndex = 129;
    this.cbxNewShopSetup.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxNewShopSetup.FormattingEnabled = true;
    this.cbxNewShopSetup.Location = new Point(150, 20);
    this.cbxNewShopSetup.Margin = new Padding(0);
    this.cbxNewShopSetup.Name = "cbxNewShopSetup";
    this.cbxNewShopSetup.Size = new Size(100, 21);
    this.cbxNewShopSetup.TabIndex = 122;
    this.cbxNewShopSetup.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
    this.cbxStepSetup.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxStepSetup.FormattingEnabled = true;
    this.cbxStepSetup.Location = new Point(150, 45);
    this.cbxStepSetup.Margin = new Padding(0);
    this.cbxStepSetup.Name = "cbxStepSetup";
    this.cbxStepSetup.Size = new Size(100, 21);
    this.cbxStepSetup.TabIndex = 123;
    this.cbxStepSetup.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
    this.cbxToolSetup.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxToolSetup.FormattingEnabled = true;
    this.cbxToolSetup.Location = new Point(150, 70);
    this.cbxToolSetup.Margin = new Padding(0);
    this.cbxToolSetup.Name = "cbxToolSetup";
    this.cbxToolSetup.Size = new Size(100, 21);
    this.cbxToolSetup.TabIndex = 124;
    this.cbxToolSetup.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
    this.cbxMaterialSetup.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxMaterialSetup.FormattingEnabled = true;
    this.cbxMaterialSetup.Location = new Point(150, 95);
    this.cbxMaterialSetup.Margin = new Padding(0);
    this.cbxMaterialSetup.Name = "cbxMaterialSetup";
    this.cbxMaterialSetup.Size = new Size(100, 21);
    this.cbxMaterialSetup.TabIndex = 125;
    this.cbxMaterialSetup.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
    this.lblPrintProps.AutoSize = true;
    this.tlpPrintConfigs.SetColumnSpan((Control) this.lblPrintProps, 2);
    this.lblPrintProps.Dock = DockStyle.Fill;
    this.lblPrintProps.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblPrintProps.Location = new Point(0, 0);
    this.lblPrintProps.Margin = new Padding(0);
    this.lblPrintProps.Name = "lblPrintProps";
    this.lblPrintProps.Size = new Size(250, 20);
    this.lblPrintProps.TabIndex = 117;
    this.lblPrintProps.Text = "Параметры печати:";
    this.lblNewShopSetup.AutoSize = true;
    this.lblNewShopSetup.Dock = DockStyle.Fill;
    this.lblNewShopSetup.Location = new Point(0, 22);
    this.lblNewShopSetup.Margin = new Padding(0, 2, 0, 0);
    this.lblNewShopSetup.Name = "lblNewShopSetup";
    this.lblNewShopSetup.Size = new Size(150, 23);
    this.lblNewShopSetup.TabIndex = 118;
    this.lblNewShopSetup.Text = "Новый цех:";
    this.lblStepSetup.AutoSize = true;
    this.lblStepSetup.Dock = DockStyle.Fill;
    this.lblStepSetup.Location = new Point(0, 47);
    this.lblStepSetup.Margin = new Padding(0, 2, 0, 0);
    this.lblStepSetup.Name = "lblStepSetup";
    this.lblStepSetup.Size = new Size(150, 23);
    this.lblStepSetup.TabIndex = 119;
    this.lblStepSetup.Text = "Переход:";
    this.lblToolSetup.AutoSize = true;
    this.lblToolSetup.Dock = DockStyle.Fill;
    this.lblToolSetup.Location = new Point(0, 72);
    this.lblToolSetup.Margin = new Padding(0, 2, 0, 0);
    this.lblToolSetup.Name = "lblToolSetup";
    this.lblToolSetup.Size = new Size(150, 23);
    this.lblToolSetup.TabIndex = 120;
    this.lblToolSetup.Text = "Оснастка:";
    this.lblAuxiliaryNaterialSetup.AutoSize = true;
    this.lblAuxiliaryNaterialSetup.Dock = DockStyle.Fill;
    this.lblAuxiliaryNaterialSetup.Location = new Point(0, 97);
    this.lblAuxiliaryNaterialSetup.Margin = new Padding(0, 2, 0, 0);
    this.lblAuxiliaryNaterialSetup.Name = "lblAuxiliaryNaterialSetup";
    this.lblAuxiliaryNaterialSetup.Size = new Size(150, 139);
    this.lblAuxiliaryNaterialSetup.TabIndex = 121;
    this.lblAuxiliaryNaterialSetup.Text = "Всп. материал:";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpPageBlank);
    this.Name = nameof (BlankConfigView);
    this.Size = new Size(517, 686);
    this.tlpPageBlank.ResumeLayout(false);
    this.tlpPageBlank.PerformLayout();
    this.tlpDocNumConfigs.ResumeLayout(false);
    this.tlpDocNumConfigs.PerformLayout();
    this.udStepNumber.EndInit();
    this.udFirstNumber.EndInit();
    this.udCharsCount.EndInit();
    this.tlpDocDependency.ResumeLayout(false);
    this.tlpDocDependency.PerformLayout();
    this.tlpPrintConfigs.ResumeLayout(false);
    this.tlpPrintConfigs.PerformLayout();
    this.ResumeLayout(false);
  }
}
