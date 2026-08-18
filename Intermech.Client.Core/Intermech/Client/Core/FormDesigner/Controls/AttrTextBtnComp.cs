
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextBtnComp
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Designer(typeof (AttrTextBtnCompControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrTextBtnComp : AttrsControl
{
  /// <summary>Режим выбора объекта</summary>
  private ImbaseCatalogSelectMode _selectMode = ImbaseCatalogSelectMode.imcmNone;
  private long _id;
  private ControlButton _btnDots;
  private ControlButton _btnDel;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _txt;

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Control")]
  public new Color BackColor
  {
    get => this._txt.BackColor;
    set => this._txt.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._txt.BorderStyle;
    set => this._txt.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._txt.Font;
    set => this._txt.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._txt.ForeColor;
    set => this._txt.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._txt);
    set => this._toolTip.SetToolTip((Control) this._txt, value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._txt.Text;
    set
    {
      this._txt.Text = string.IsNullOrEmpty(this._designText) || !string.IsNullOrEmpty(value) ? value : this._designText;
    }
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment TextAlign
  {
    get => this._txt.TextAlign;
    set => this._txt.TextAlign = value;
  }

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      return this._id != 0L ? new object[1]
      {
        (object) Math.Abs(this._id)
      } : new object[1]{ (object) DBNull.Value };
    }
  }

  /// <summary>Идентификаторы справочников IMBASE.</summary>
  private List<long> CataloglIDs
  {
    get
    {
      List<long> cataloglIds = (List<long>) null;
      try
      {
        if (this.AttributeInfo.AttributeGuid != Guid.Empty)
        {
          int typeID = -1;
          if (this.ParentInfo.ElementKind == AttributableElements.Object)
          {
            if (this.ParentTypeID == -1)
            {
              QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this.ParentInfo.ElementIdentifier);
              if (!objectInfo.Empty)
                this.ParentTypeID = objectInfo.ObjectTypeID;
            }
            typeID = this.ParentTypeID;
          }
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            ImbaseExtendedItem imbaseExtendedItem = ExtendedServiceHelper.GetImbaseExtendedItem(sessionKeeper.Session, typeID, this._attrValues.AttributeID);
            if (imbaseExtendedItem != null)
            {
              cataloglIds = imbaseExtendedItem.CatalogIDs;
              this._selectMode = imbaseExtendedItem.SelectMode;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
      finally
      {
        if (cataloglIds == null || cataloglIds.Count == 0)
        {
          cataloglIds = (List<long>) null;
          this.Error = LocalizationHolder.rm.GetString("AttrTextBtnComp.ImbaseCatalog.NotRef");
        }
      }
      return cataloglIds;
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrTextBtnComp()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._txt.GotFocus += new EventHandler(this.On_txt_GotFocus);
    this._txt.LostFocus += new EventHandler(this.On_txt_LostFocus);
    this._btnDots = new ControlButton("Dots", 0)
    {
      Enabled = false
    };
    this._btnDots.Click += new EventHandler(this.On_btn_Click);
    this._btnDel = new ControlButton("Del", 4)
    {
      Enabled = false
    };
    this._btnDel.Click += new EventHandler(this.On_btnDel_Click);
    this.AddRightButtons(new List<ControlButton>()
    {
      this._btnDots,
      this._btnDel
    });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void On_btn_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null || this.ParentInfo == null || !(ApplicationServices.Container.GetService(typeof (IImbaseFilterSelector)) is IImbaseFilterSelector service))
      return;
    List<long> cataloglIds = this.CataloglIDs;
    if (cataloglIds == null)
      return;
    Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = new Dictionary<TypedInfoItem, IEnumerable<AttributeValues>>(2);
    IElementInfo elementInfo = (IElementInfo) null;
    if (this.DesForm != null)
    {
      elementInfo = this.DesForm.Info;
      List<AttributeValues> changedAttributes1 = this.DesForm.GetBaseElementChangedAttributes;
      if (changedAttributes1.Count > 0)
      {
        if (elementInfo.ElementKind == AttributableElements.Object)
          dict.Add((TypedInfoItem) new ObjInfoItem(elementInfo.ElementIdentifier, this.DesForm.ElementTypeID), (IEnumerable<AttributeValues>) changedAttributes1);
        else
          dict.Add((TypedInfoItem) new RelInfoItem(elementInfo.ElementIdentifier, this.DesForm.ElementTypeID), (IEnumerable<AttributeValues>) changedAttributes1);
      }
      List<AttributeValues> changedAttributes2 = this.DesForm.GetAdditionalElementChangedAttributes;
      if (changedAttributes2.Count > 0)
        dict.Add((TypedInfoItem) new RelInfoItem(this.DesForm.RelationInfo.ElementIdentifier), (IEnumerable<AttributeValues>) changedAttributes2);
    }
    int attributeId = this._attrValues.AttributeID;
    long id = this._id;
    long objID = this.ParentInfo.ElementKind == AttributableElements.Object ? this.ParentInfo.ElementIdentifier : (elementInfo != null ? elementInfo.ElementIdentifier : 0L);
    long num;
    if (attributeId == Intermech.Imbase.Consts.ImbaseObjectRefAttID)
    {
      if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
      {
        num = service.SelectImbaseObject(cataloglIds, (int[]) null, objID, id, this._selectMode, dict, attributeId);
      }
      else
      {
        long codeImbaseValue = this.GetCodeImbaseValue();
        service.RecordID = codeImbaseValue;
        num = service.SelectImbaseObject(cataloglIds, (int[]) null, objID, id, ImbaseCatalogSelectMode.imcmAllowSelectRow, dict, attributeId);
        long recordId = service.RecordID;
        if (codeImbaseValue != recordId)
        {
          this.Modified = true;
          this.UpdateCodeImbaseValue(recordId);
        }
      }
    }
    else
    {
      int[] needObjTypes = (int[]) null;
      if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
        needObjTypes = MetaDataHelper.GetLinkedObjectTypes(attributeId)?.ToArray();
      num = service.SelectImbaseObject(cataloglIds, needObjTypes, objID, id, this._selectMode, dict, attributeId);
    }
    if (num == this._id)
      return;
    this._id = num;
    this._txt.Text = this.GetCaptionForID(this._id);
    this._txt.Focus();
    this.UpdateSlaveAttribute();
    this.OnCompletionOfEditing();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnDel_Click(object sender, EventArgs e) => this.DeleteItem();

  /// <summary>Фокусирование текстового контрола.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_GotFocus(object sender, EventArgs e) => this.Error = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
      return;
    this.DeleteItem();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_LostFocus(object sender, EventArgs e)
  {
    this.Error = !this._disableNulls || !this.EnabledCtrl || this._id != 0L ? string.Empty : this._errMsg_NullValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_SizeChanged(object sender, EventArgs e)
  {
    this.Height = this._txt == null || this._txt.Height < 20 ? 22 : this._txt.Height + 2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    if (!this.IsDesignMode && this.AttributeInfo != null && this._attrValues != null)
    {
      this.CheckAccessibilityButtons();
      this.Modified = true;
    }
    this.Invalidate();
  }

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      base.Values = value;
      if (value != null)
      {
        object obj = value.Values[0];
        this._id = obj == null || obj == DBNull.Value ? 0L : Convert.ToInt64(obj);
      }
      this._txt.Text = this.GetCaptionForID(this._id);
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>Доступность контрола.</summary>
  [DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      base.EnabledCtrl = value;
      this.CheckAccessibilityButtons();
      if (this.IsDesignMode || !(this._txt.BackColor == SystemColors.Window))
        return;
      this._txt.BackColor = SystemColors.Control;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (this._txt.Text == this._designText)
      this._txt.Text = text;
    this._designText = text;
  }

  /// <summary>Проверка доступности кнопок.</summary>
  private void CheckAccessibilityButtons()
  {
    if (this.IsDesignMode)
    {
      this._buttons.Enabled = this.EnabledCtrl;
    }
    else
    {
      this._btnDots.Enabled = this.EnabledCtrl;
      this._btnDel.Enabled = this.EnabledCtrl && this._id != 0L;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void DeleteItem()
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    this._id = 0L;
    this._txt.Text = this.GetCaptionForID(this._id);
    this.UpdateSlaveAttribute();
    this.OnCompletionOfEditing();
  }

  /// <summary>
  /// 
  /// </summary>
  private string GetCaptionForID(long id)
  {
    string captionForId = string.Empty;
    if (this._id == 0L)
    {
      this.Error = !this._disableNulls || !this.EnabledCtrl || this._txt.Focused ? string.Empty : this._errMsg_NullValue;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._disableNulls)
          this.Error = string.Empty;
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(id, false);
        if (objectActualCopy != null)
        {
          captionForId = objectActualCopy.Caption;
          this._id = objectActualCopy.ObjectID;
        }
        else
          captionForId = $"{LocalizationHolder.rm.GetString("Client.Core_1132")} №{Convert.ToString(id)}";
      }
    }
    return captionForId;
  }

  /// <summary>Получение значения атрибута "Код IMBASE".</summary>
  /// <remark>Используется только в случае, когда с контролом связан атрибут "Ссылка на объект IMBASE"</remark>
  /// <returns>Значение атрибута "Код IMBASE"</returns>
  private long GetCodeImbaseValue()
  {
    long result = -1;
    if (this.DesForm != null)
    {
      int codeImbaseAttrID = MetaDataHelper.GetAttributeTypeID("cad0020f-306c-11d8-b4e9-00304f19f545");
      AttributeValues attributeValues = this.DesForm.GetAttributeValuesFromControls(this.ParentInfo.ElementIdentifier).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == codeImbaseAttrID)) ?? this.DesForm.GetAdditionalValues(this.ParentInfo.ElementIdentifier).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == codeImbaseAttrID));
      if (attributeValues == null && this.DesForm.Processor != null)
        attributeValues = this.DesForm.Processor.ActualAttributeValues.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == codeImbaseAttrID));
      if (attributeValues != null && attributeValues.Values.Length != 0 && !long.TryParse(Convert.ToString(attributeValues.Values[0]), out result))
        result = -1L;
    }
    return result;
  }

  /// <summary>Обновление значения атрибута "Код IMBASE".</summary>
  /// <param name="recID">Новое значение атрибута</param>
  private void UpdateCodeImbaseValue(long recID)
  {
    if (this.DesForm == null)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0020f-306c-11d8-b4e9-00304f19f545");
    object[] objArray1;
    if (recID <= -1L)
      objArray1 = new object[1]{ (object) DBNull.Value };
    else
      objArray1 = new object[1]{ (object) recID };
    object[] objArray2 = objArray1;
    AttributeValues attributeValues = new AttributeValues(attributeTypeId)
    {
      Descriptions = objArray2,
      Values = objArray2
    };
    List<AttributeValues> newObjectValues = (List<AttributeValues>) null;
    List<AttributeValues> newRelationValues = (List<AttributeValues>) null;
    if (this.ParentPoint == AttributeDestinationPoint.Default)
      newObjectValues = new List<AttributeValues>()
      {
        attributeValues
      };
    else
      newRelationValues = new List<AttributeValues>()
      {
        attributeValues
      };
    this.DesForm.AttributeChanging((IEnumerable<AttributeValues>) newObjectValues, (IEnumerable<AttributeValues>) newRelationValues);
  }

  /// <summary>
  /// При выборе значения для мастер атрибута возникает необходимость обновить значение связанного с ним атрибута.
  /// </summary>
  private void UpdateSlaveAttribute()
  {
    if (this.DesForm == null)
      return;
    AttributeProcessor attributeProcessor = this.ParentInfo.ElementKind == AttributableElements.Object ? this.DesForm.Processor : this.DesForm.RelationProcessor;
    if (attributeProcessor == null || !attributeProcessor.IsMasterAttribute(this._attrValues.AttributeID))
      return;
    AttributeValues attributeValues = attributeProcessor.ActualAttributeValues.FindByAttributeID(this._attrValues.AttributeID) ?? this._attrValues;
    AttributeValuesList deltaList = (AttributeValuesList) null;
    attributeProcessor.AssignMasterAttributePrim(attributeValues.AttributeID, (object) this._id, attributeProcessor.ActualAttributeValues, false, out deltaList);
    this.DesForm.UpdateSlaveAttribute(this.ParentInfo.ElementKind == AttributableElements.Object ? this.DesForm.Info : this.DesForm.RelationInfo, deltaList);
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._txt.Font);

  /// <summary>Необходимость сериализации свойства Text.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeText()
  {
    return !string.IsNullOrEmpty(this._designText) ? this._txt.Text != this._designText : !string.IsNullOrEmpty(this._txt.Text);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._txt.SizeChanged -= new EventHandler(this.On_txt_SizeChanged);
      this._txt.TextChanged -= new EventHandler(this.On_txt_TextChanged);
      this._txt.KeyDown -= new KeyEventHandler(this.On_txt_KeyDown);
      this._txt.GotFocus -= new EventHandler(this.On_txt_GotFocus);
      this._txt.LostFocus -= new EventHandler(this.On_txt_LostFocus);
      if (!this.IsDesignMode)
      {
        if (this._btnDots != null)
          this._btnDots.Click -= new EventHandler(this.On_btn_Click);
        if (this._btnDel != null)
          this._btnDel.Click -= new EventHandler(this.On_btnDel_Click);
      }
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrTextBtnComp));
    this._txt = new TextBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.Name = "_txt";
    this._txt.ReadOnly = true;
    this._txt.SizeChanged += new EventHandler(this.On_txt_SizeChanged);
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._txt);
    this.Name = nameof (AttrTextBtnComp);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
