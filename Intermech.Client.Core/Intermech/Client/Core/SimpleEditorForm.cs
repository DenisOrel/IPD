
// Type: Intermech.Client.Core.SimpleEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Форма редактора значений атрибутов.</summary>
internal class SimpleEditorForm : Form
{
  private List<AttributeValues> _originalValues;
  private List<AttributeValues> _newValues;
  private SimpleEditorForm.SimpleEditorFormMode _mode;
  private IElementInfo _elementInfo;
  private Dictionary<int, string> _masks;
  /// <summary>Созданные контролы</summary>
  private List<IAttributeEditor> _ctrls;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlTop;
  private Panel _pnlBottom;
  private Panel _pnlButton;
  private Button _btnOK;
  private Button _btnCancel;
  private ToolTip _tt;

  /// <summary>Возвращает измененные значения.</summary>
  public AttributeValues[] Values => (this._newValues ?? new List<AttributeValues>(0)).ToArray();

  /// <summary>
  /// 
  /// </summary>
  public Dictionary<int, List<long>> SelectedObjInfo { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="elementInfo"></param>
  /// <param name="attributeValues"></param>
  /// <param name="mode"></param>
  /// <param name="masks"></param>
  public SimpleEditorForm(
    IElementInfo elementInfo,
    AttributeValues[] attributeValues,
    SimpleEditorForm.SimpleEditorFormMode mode,
    Dictionary<int, string> masks)
  {
    this.InitializeComponent();
    this._elementInfo = elementInfo;
    this._originalValues = new List<AttributeValues>((IEnumerable<AttributeValues>) attributeValues);
    this._ctrls = new List<IAttributeEditor>(this._originalValues.Count);
    this._mode = mode;
    this._masks = masks ?? new Dictionary<int, string>(0);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    int x1 = 8;
    int x2 = 250;
    int y = 8;
    this._pnlTop.SuspendLayout();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (AttributeValues originalValue in this._originalValues)
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(originalValue.AttributeID);
          System.Type type;
          if (attributeType.AttributeID == -7)
          {
            type = typeof (AttrTextEdit);
          }
          else
          {
            bool bMasked = this._masks.ContainsKey(originalValue.AttributeID);
            type = ComponentTypeProducer.GetComponentType(attributeType.MultipleValued, attributeType.AttributeType, bMasked);
            if (type == typeof (AttrTextBtn) && this.SelectedObjInfo != null && this.SelectedObjInfo.Count == 1)
            {
              ExtendedServiceHelper.ObjTypeInfo objTypeData = ExtendedServiceHelper.GetObjTypeData(this.SelectedObjInfo.Keys.ElementAt<int>(0), sessionKeeper.Session);
              if (objTypeData != null)
              {
                ImbaseExtendedItem imbaseExtendedItem = objTypeData.GetValue(originalValue.AttributeID, sessionKeeper.Session);
                if (imbaseExtendedItem != null && imbaseExtendedItem.SelectMode != ImbaseCatalogSelectMode.imcmNone && imbaseExtendedItem.CatalogIDs != null && imbaseExtendedItem.CatalogIDs.Count > 0)
                  type = typeof (AttrTextBtnComp);
              }
            }
          }
          Label label1 = new Label();
          label1.Location = new Point(x1, y);
          label1.Text = attributeType.Name;
          label1.Width = 234;
          label1.TextAlign = ContentAlignment.MiddleLeft;
          Label label2 = label1;
          this._tt.SetToolTip((Control) label2, label2.Text);
          IAttributeEditor instance = type != (System.Type) null ? Activator.CreateInstance(type) as IAttributeEditor : (IAttributeEditor) null;
          Control control = (Control) null;
          if (instance != null)
          {
            if (instance is ILockModify lockModify)
              lockModify.LockModify = true;
            try
            {
              control = instance as Control;
              control.Location = new Point(x2, y);
              control.Width = 250;
              control.Enabled = !originalValue.ReadOnly;
              control.Anchor |= AnchorStyles.Right;
              label2.Height = control.Height;
              if (control is AttrMemoEdit)
              {
                Size size = TextRenderer.MeasureText("AttrMemoEdit", control.Font);
                control.Height = size.Height * 6;
              }
              instance.AttributeInfo = new AttributeInfo((attributeType as IDBGuid).GUID, Guid.Empty);
              this._pnlTop.Controls.AddRange(new Control[2]
              {
                (Control) label2,
                control
              });
              switch (instance)
              {
                case IExtendedParent4Control extendedParent4Control:
                  extendedParent4Control.ParentInfo = this._elementInfo;
                  if (this._elementInfo != null)
                  {
                    extendedParent4Control.ParentTypeID = sessionKeeper.Session.GetObjectInfo(this._elementInfo.ElementIdentifier).ObjectTypeID;
                    break;
                  }
                  break;
                case IParent4Control parent4Control:
                  parent4Control.ParentInfo = this._elementInfo;
                  break;
              }
              instance.SetPossibleValues(this.GetPossibleValues(attributeType), attributeType.PossibleValueFieldName, "F_DESCRIPTION");
              instance.Values = originalValue;
              if (control is AttrMaskedTextEdit attrMaskedTextEdit)
              {
                string str = this._masks.ContainsKey(originalValue.AttributeID) ? this._masks[originalValue.AttributeID] : string.Empty;
                attrMaskedTextEdit.Mask = str;
              }
            }
            finally
            {
              if (lockModify != null)
                lockModify.LockModify = false;
            }
            this._ctrls.Add(instance);
          }
          else
          {
            Label label3 = new Label();
            label3.Location = new Point(x1, y);
            label3.Text = LocalizationHolder.rm.GetString("Client.Core_1025");
            label3.Width = 250;
            label3.TextAlign = ContentAlignment.MiddleLeft;
            control = (Control) label3;
            this._pnlTop.Controls.AddRange(new Control[2]
            {
              (Control) label2,
              control
            });
          }
          y += control.Height + 8;
        }
      }
      Rectangle workingArea = Screen.GetWorkingArea(new Point(0, 0));
      this.ClientSize = new Size(508, y + this._pnlBottom.Height + 200 > workingArea.Height ? workingArea.Height - 200 : y + this._pnlBottom.Height);
      this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, this.Height);
    }
    finally
    {
      this._pnlTop.ResumeLayout(false);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
    {
      List<AttributeValues> collection = new List<AttributeValues>();
      this._newValues = new List<AttributeValues>(this._ctrls.Count);
      foreach (IAttributeEditor ctrl in this._ctrls)
      {
        if (ctrl is IDataFormatError dataFormatError && dataFormatError.IsDataFormatError)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Client.Core.FormDesigner.DataFormatError"), LocalizationHolder.rm.GetString("Client.Core_1149"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          e.Cancel = true;
          break;
        }
        AttributeValues values = ctrl.Values;
        if (values.Values[0] == DBNull.Value)
          collection.Add(values);
        else if (values.AttributeID == -14)
        {
          long result = 0;
          if (!long.TryParse(Convert.ToString(values.Values[0]), out result) || result == 0L)
            collection.Add(values);
          else
            this._newValues.Add(values);
        }
        else
          this._newValues.Add(values);
      }
      if (!e.Cancel && collection.Count > 0)
      {
        switch (MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Attributes_ChangeValues_NullValue"), LocalizationHolder.rm.GetString("Client.Core_971"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            break;
          case DialogResult.Yes:
            this._newValues.AddRange((IEnumerable<AttributeValues>) collection);
            break;
        }
      }
    }
    if (e.Cancel)
      return;
    base.OnClosing(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnKeyPress(KeyPressEventArgs e)
  {
    base.OnKeyPress(e);
    if (e.KeyChar != '\r' || this.ActiveControl is AttrMemoEdit)
      return;
    this._btnOK.PerformClick();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrType"></param>
  /// <returns></returns>
  private DataTable GetPossibleValues(IDBAttributeType attrType)
  {
    DataTable possibleValues = (DataTable) null;
    if (attrType != null)
    {
      possibleValues = attrType.GetPossibleValues();
      if (possibleValues != null && (attrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
      {
        DataRow row = possibleValues.NewRow();
        row[attrType.PossibleValueFieldName] = (object) DBNull.Value;
        row["F_DESCRIPTION"] = (object) string.Empty;
        possibleValues.Rows.Add(row);
      }
    }
    return possibleValues;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SimpleEditorForm));
    this._pnlBottom = new Panel();
    this._pnlButton = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._pnlTop = new Panel();
    this._tt = new ToolTip(this.components);
    this._pnlBottom.SuspendLayout();
    this._pnlButton.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._pnlButton);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    this._pnlButton.Controls.Add((Control) this._btnCancel);
    this._pnlButton.Controls.Add((Control) this._btnOK);
    componentResourceManager.ApplyResources((object) this._pnlButton, "_pnlButton");
    this._pnlButton.Name = "_pnlButton";
    this._btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Name = "_btnCancel";
    this._btnOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.Name = "_btnOK";
    componentResourceManager.ApplyResources((object) this._pnlTop, "_pnlTop");
    this._pnlTop.Name = "_pnlTop";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._pnlTop);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SimpleEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._pnlBottom.ResumeLayout(false);
    this._pnlButton.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Режим редактирования значений атрибутов.</summary>
  public enum SimpleEditorFormMode
  {
    /// <summary>Добавлять значения</summary>
    /// <remarks>Значения будут добавляться всегда</remarks>
    AddAttributes,
    /// <summary>Изменять значения</summary>
    /// <remarks>Значения будут сохраняться только когда они изменены в контроле</remarks>
    EditAttributes,
  }
}
