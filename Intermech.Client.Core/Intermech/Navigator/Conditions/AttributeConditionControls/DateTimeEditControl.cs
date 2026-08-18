
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.DateTimeEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class DateTimeEditControl : EditControl<DateTimeTextBox>
{
  private int _attributeID;
  private int[] _objectTypeIDs;
  private IConditionDataProvider _dataProvider;
  private DateTimePickerFormat _format;
  private string _formatString;
  private bool _externalFormat = true;

  public DateTimeEditControl(
    bool firstValue,
    IConditionDataProvider dataProvider,
    int attributeID,
    int[] objectTypeIDs)
    : this(firstValue, dataProvider, attributeID, objectTypeIDs, DateTimePickerFormat.Short, (string) null)
  {
    this._externalFormat = false;
  }

  public DateTimeEditControl(
    bool firstValue,
    IConditionDataProvider dataProvider,
    int attributeID,
    int[] objectTypeIDs,
    DateTimePickerFormat format,
    string formatString)
    : base(firstValue)
  {
    this._dataProvider = dataProvider;
    this._attributeID = attributeID;
    this._objectTypeIDs = objectTypeIDs;
    this._format = format;
    this._formatString = formatString;
  }

  public override object Value
  {
    get => this.control.Value;
    set
    {
      if (value is DateTime dateTime)
        this.control.Value = (object) dateTime;
      else
        this.control.Value = (object) null;
    }
  }

  protected override object defaultValue => (object) DateTime.Now;

  protected override void OnCreateControl()
  {
    this.control = this.GetControlForDialog() as DateTimeTextBox;
    this.control.ValueChanged += new EventHandler(((EditControl<DateTimeTextBox>) this).OnValueChanged);
    this.control.OnDeleteKey += new EventHandler(this.Control_OnDeleteKey);
  }

  private void Control_OnDeleteKey(object sender, EventArgs e)
  {
    this.Value = (object) null;
    this.OnValueChanged((object) this, new EventArgs());
  }

  protected override Control GetControlForDialog()
  {
    DateTimeTextBox controlForDialog = new DateTimeTextBox();
    string formatString = "G";
    if (this._externalFormat)
      formatString = this._formatString;
    else if (this._attributeID != 0)
      this._dataProvider.GetDateAttributeFormat(this._attributeID, this._objectTypeIDs, out DateTimePickerFormat _, out formatString);
    controlForDialog.FormatString = formatString;
    return (Control) controlForDialog;
  }

  protected override void OnOKControlDialog(Control control)
  {
    this.Value = (control as DateTimeTextBox).Value;
  }

  protected override bool ValidValue(object value) => value is DateTime;
}
