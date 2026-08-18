
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.ObjectEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class ObjectEditControl : EditControl<TextBoxButton>
{
  protected IConditionDataProvider dataProvider;
  protected int attributeID;
  protected SelectionParameterTypes paramType;
  protected Dictionary<object, string> pValues;
  private object _enableObjectTypes;
  protected readonly int[] selection4types;
  protected readonly RelationalOperators relOperator;

  public ObjectEditControl(
    IConditionDataProvider dataProvider,
    int attributeID,
    int[] selection4types,
    SelectionParameterTypes paramType,
    Dictionary<object, string> pValues,
    bool firstValue,
    RelationalOperators relOperator = RelationalOperators.Empty)
    : base(firstValue)
  {
    this.dataProvider = dataProvider;
    this.attributeID = attributeID;
    this.paramType = paramType;
    this.pValues = pValues;
    this.relOperator = relOperator;
    this.InitEnableObjectTypes(attributeID, relOperator);
    this.selection4types = selection4types;
  }

  private void InitEnableObjectTypes(int attributeID, RelationalOperators relOperator)
  {
    if (this.attributeID != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetAttributeType(this.attributeID, false) is IDBObjectLinkAttributeType attributeType))
          return;
        int[] validObjectTypes = attributeType.GetValidObjectTypes();
        if (validObjectTypes == null || validObjectTypes.Length == 0)
          return;
        this._enableObjectTypes = (object) Array.FindAll<int>(validObjectTypes, (Predicate<int>) (_ => _ != -1));
      }
    }
    else
    {
      if (relOperator != RelationalOperators.ExistsInVersionContext)
        return;
      this._enableObjectTypes = (object) MetaDataHelper.GetEditingContextTopObjectsIDs();
    }
  }

  protected override void OnSetValue(object value)
  {
    this.control.SetText(this.dataProvider.ConvertToString((object) this.attributeID, this.relOperator, this.paramType, value, this.pValues, (object) null));
  }

  protected override void OnCreateControl()
  {
    this.control = new TextBoxButton(true, string.Empty);
    this.control.OnOpenDialog += new OnOpenDialogEventHandler(this.OnOpenDialog);
    this.control.OnDeleteKey += new EventHandler(this.OnDeleteKey);
  }

  private void OnDeleteKey(object sender, EventArgs e)
  {
    this.Value = (object) null;
    this.OnValueChanged((object) this, new EventArgs());
  }

  public override bool OnAddNewValue(OnOpenDialogEventArgs e)
  {
    this.control.OpenDialog_Click((object) this, e);
    return this.control.ValueChangedFromDialog;
  }

  protected virtual IButtonDialog ButtonDialog
  {
    get
    {
      return (IButtonDialog) new ObjectButtonDialog(this.dataProvider, this.attributeID, this.selection4types, this._enableObjectTypes, this.Value);
    }
  }

  protected override object defaultValue => (object) null;

  protected virtual bool OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    bool flag1 = false;
    bool flag2 = false;
    IAttributePropertyDescriber describer = ServicesManager.GetService<IAttributePropertyDescriberService>().GetDescriber(this.attributeID);
    if (describer != null)
    {
      DescriberButtonDialog dialog = new DescriberButtonDialog(describer, this.dataProvider, this.attributeID, this.Value);
      flag1 = this.OpenDialog((IButtonDialog) dialog, e);
      flag2 = dialog.Handled;
    }
    if (!flag2 && this.dataProvider.EnabledParameterTypes.Contains(this.paramType))
      flag1 = this.OpenDialog(this.ButtonDialog, e);
    if (flag1)
      this.OnValueChanged((object) this, new EventArgs());
    return flag1;
  }

  protected bool OpenDialog(IButtonDialog dialog, OnOpenDialogEventArgs e)
  {
    if (!dialog.OnOpenDialog(e.Multiselect))
      return false;
    e.SelectedValues = dialog.Value;
    this.Value = dialog.Value is IList list ? list[list.Count - 1] : dialog.Value;
    this.control.SetText(dialog.Text);
    return true;
  }

  public static IEditControl GetEditControl(
    IConditionDataProvider dataProvider,
    int attributeID,
    RelationalOperators relOperator,
    SelectionParameterTypes paramType,
    Dictionary<object, string> pValues,
    int[] objectTypeIDs,
    bool firstValue)
  {
    switch (paramType)
    {
      case SelectionParameterTypes.sptSiteID:
        return (IEditControl) new SiteIDEditControl(dataProvider, attributeID, pValues, firstValue);
      case SelectionParameterTypes.sptCheckOutBy:
      case SelectionParameterTypes.sptUser:
        return relOperator == RelationalOperators.Equal || relOperator == RelationalOperators.NotEqual ? (IEditControl) new UserGroupEditControl(dataProvider, attributeID, objectTypeIDs, paramType, pValues, firstValue) : (IEditControl) new UserEditControl(dataProvider, attributeID, objectTypeIDs, pValues, firstValue);
      case SelectionParameterTypes.sptObjectType:
        return (IEditControl) new ObjectTypeEditControl(dataProvider, attributeID, pValues, firstValue);
      case SelectionParameterTypes.sptLifecycleLevel:
        return (IEditControl) new LCLevelEditControl(dataProvider, attributeID, pValues, firstValue);
      case SelectionParameterTypes.sptSubjectArea:
        return (IEditControl) new SubjectAreaEditControl(dataProvider, attributeID, pValues, firstValue);
      case SelectionParameterTypes.sptLifecycleStep:
        return (IEditControl) new LCStepEditControl(dataProvider, attributeID, pValues, firstValue);
      case SelectionParameterTypes.sptGlobalID:
        return (IEditControl) new GlobalIDEditControl(dataProvider, attributeID, pValues, firstValue);
      case SelectionParameterTypes.sptMeasured:
        return (IEditControl) new MeasureEditControl(dataProvider, attributeID, pValues, firstValue);
      default:
        return SelectionParameter.IsLinkRelationOpr(relOperator) ? (IEditControl) new LinkedObjectTypeEditControl(dataProvider, attributeID) : (IEditControl) new ObjectEditControl(dataProvider, attributeID, objectTypeIDs, paramType, pValues, firstValue, relOperator);
    }
  }

  protected override bool ValidValue(object value) => value is long || value is Guid;
}
