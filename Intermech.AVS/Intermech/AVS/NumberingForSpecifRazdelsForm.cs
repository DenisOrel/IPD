// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NumberingForSpecifRazdelsForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса NumberingForSpecifRazdelsForm </summary>
public class NumberingForSpecifRazdelsForm : ExtForm
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private Button _BtnAddType;
  private Button _BtnDelType;
  private ToolTipController _ReadModeToolTip;
  private ToolTipController _EditModeToolTip;
  private ImageListBoxControl _ObjTypesListBoxControl;
  private ImageList _ObjTypesImageList;
  private SpecifNumberingForRazdelControl _SpecifNumberingControl;
  private ImageComboBoxEdit _comboBoxListSource;
  private Label label3;
  public Button _BtnReset;
  private Label label1;
  private SpecifRazdelNumbering _SpecifRazdelNumbering;
  private SpecifRazdelNumbering _OldSpecifRazdelNumbering;
  private int _RazdelObjImageIndex = -1;
  private HybridDictionary _ListItemToRazdelIDHash = new HybridDictionary();
  private IStructualControlSupport _iStructualControlSupport;
  private InitDataEventHandler _onInitDataEventDelegateThis;

  public NumberingForSpecifRazdelsForm()
  {
    this.InitializeComponent();
    this.Init((SpecifRazdelNumbering) null);
  }

  public NumberingForSpecifRazdelsForm(SpecifRazdelNumbering SpecifRazdelNumbering)
  {
    this.InitializeComponent();
    this.Init(SpecifRazdelNumbering);
  }

  public NumberingForSpecifRazdelsForm(
    Control ownerControl,
    SpecifRazdelNumbering specifRazdelNumbering,
    IStructualControlSupport iStructualControlSupport)
    : base(ownerControl)
  {
    this.InitializeComponent();
    this._onInitDataEventDelegateThis = new InitDataEventHandler(this.OnInitData);
    this._iStructualControlSupport = iStructualControlSupport;
    iStructualControlSupport.OnInitDataEvent += this._onInitDataEventDelegateThis;
    this.Init(specifRazdelNumbering);
  }

  private void Init(SpecifRazdelNumbering specifRazdelNumbering)
  {
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    if (service != null)
    {
      Icon icon = service.GetIcon(4, AvsIDCache.ObjType_SpecificationSection);
      if (icon != null)
      {
        this._ObjTypesImageList.Images.Add(icon);
        this._RazdelObjImageIndex = this._ObjTypesImageList.Images.Count - 1;
      }
    }
    this._OldSpecifRazdelNumbering = specifRazdelNumbering;
    this.SpecifRazdelNumbering = specifRazdelNumbering?.Clone();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1522);
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._EditModeToolTip != null)
      {
        this._EditModeToolTip.Dispose();
        this._EditModeToolTip = (ToolTipController) null;
      }
      if (this._ReadModeToolTip != null)
      {
        this._ReadModeToolTip.Dispose();
        this._ReadModeToolTip = (ToolTipController) null;
      }
    }
    if (this._iStructualControlSupport != null && this._onInitDataEventDelegateThis != null)
      this._iStructualControlSupport.OnInitDataEvent -= this._onInitDataEventDelegateThis;
    this._onInitDataEventDelegateThis = (InitDataEventHandler) null;
    this._iStructualControlSupport = (IStructualControlSupport) null;
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._BtnAddType = new Button();
    this._BtnDelType = new Button();
    this._BtnReset = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this.label1 = new Label();
    this._ObjTypesListBoxControl = new ImageListBoxControl();
    this._ObjTypesImageList = new ImageList(this.components);
    this._SpecifNumberingControl = new SpecifNumberingForRazdelControl();
    this._comboBoxListSource = new ImageComboBoxEdit();
    this.label3 = new Label();
    ((ISupportInitialize) this._ObjTypesListBoxControl).BeginInit();
    this._comboBoxListSource.Properties.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(668, 228);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 6;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(795, 228);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 7;
    this._BtnCancel.Text = "Отмена";
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._BtnAddType.Anchor = AnchorStyles.Bottom;
    this._BtnAddType.FlatStyle = FlatStyle.System;
    this._BtnAddType.Location = new Point(7, 228);
    this._BtnAddType.Name = "_BtnAddType";
    this._BtnAddType.Size = new Size(121, 27);
    this._BtnAddType.TabIndex = 3;
    this._BtnAddType.Text = "Добавить";
    this._EditModeToolTip.SetToolTip((Control) this._BtnAddType, "Добавить раздел спецификации в список разделов со специальными настройками нумерации");
    this._BtnAddType.Click += new EventHandler(this._BtnAddType_Click);
    this._BtnDelType.Anchor = AnchorStyles.Bottom;
    this._BtnDelType.FlatStyle = FlatStyle.System;
    this._BtnDelType.Location = new Point(134, 228);
    this._BtnDelType.Name = "_BtnDelType";
    this._BtnDelType.Size = new Size(121, 27);
    this._BtnDelType.TabIndex = 4;
    this._BtnDelType.Text = "Удалить";
    this._EditModeToolTip.SetToolTip((Control) this._BtnDelType, "Удалить выбранный раздел спецификации из списка разделов со специальными настройками нумерации");
    this._BtnDelType.Click += new EventHandler(this._BtnDelType_Click);
    this._BtnReset.Enabled = false;
    this._BtnReset.FlatStyle = FlatStyle.System;
    this._BtnReset.Location = new Point(261, 228);
    this._BtnReset.Name = "_BtnReset";
    this._BtnReset.Size = new Size(121, 27);
    this._BtnReset.TabIndex = 15;
    this._BtnReset.Text = "По умолчанию";
    this._EditModeToolTip.SetToolTip((Control) this._BtnReset, "Вернуть список к значению по умолчанию");
    this._BtnReset.Visible = false;
    this._BtnReset.Click += new EventHandler(this._BtnReset_Click);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label1.Location = new Point(7, 3);
    this.label1.Name = "label1";
    this.label1.Size = new Size(399, 15);
    this.label1.TabIndex = 10;
    this.label1.Text = "Разделы спецификации";
    this.label1.TextAlign = ContentAlignment.BottomCenter;
    this._ObjTypesListBoxControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this._ObjTypesListBoxControl.ImageList = this._ObjTypesImageList;
    this._ObjTypesListBoxControl.ItemHeight = 18;
    this._ObjTypesListBoxControl.Location = new Point(7, 19);
    this._ObjTypesListBoxControl.Name = "_ObjTypesListBoxControl";
    this._ObjTypesListBoxControl.Size = new Size(365, 202);
    this._ObjTypesListBoxControl.TabIndex = 0;
    this._ObjTypesListBoxControl.ToolTip = "Список разделов спецификации, для которых заданы специальные настройки нумерации позиций";
    this._ObjTypesListBoxControl.SelectedIndexChanged += new EventHandler(this._ObjTypesListBoxControl_SelectedIndexChanged);
    this._ObjTypesImageList.ColorDepth = ColorDepth.Depth8Bit;
    this._ObjTypesImageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this._ObjTypesImageList.TransparentColor = Color.Transparent;
    this._SpecifNumberingControl.AutoScroll = true;
    this._SpecifNumberingControl.BackColor = SystemColors.Control;
    this._SpecifNumberingControl.Location = new Point(395, 19);
    this._SpecifNumberingControl.MinimumSize = new Size(513, 179);
    this._SpecifNumberingControl.Name = "_SpecifNumberingControl";
    this._SpecifNumberingControl.Size = new Size(521, 179);
    this._SpecifNumberingControl.TabIndex = 11;
    this._comboBoxListSource.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._comboBoxListSource.EditValue = (object) false;
    this._comboBoxListSource.Location = new Point(264, 230);
    this._comboBoxListSource.Name = "_comboBoxListSource";
    this._comboBoxListSource.Properties.AutoComplete = false;
    this._comboBoxListSource.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxListSource.Properties.Items.AddRange(new ImageComboBoxItem[2]
    {
      new ImageComboBoxItem("Унаследован", (object) false, -1),
      new ImageComboBoxItem("Собственный", (object) true, -1)
    });
    this._comboBoxListSource.Size = new Size(143, 23);
    this._comboBoxListSource.TabIndex = 14;
    this._comboBoxListSource.ToolTip = "Выбор, откуда брать список";
    this._comboBoxListSource.SelectedIndexChanged += new EventHandler(this._comboBoxListSource_SelectedIndexChanged);
    this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label3.Location = new Point(261, 235);
    this.label3.Name = "label3";
    this.label3.Size = new Size(62, 13);
    this.label3.TabIndex = 13;
    this.label3.Text = "Список:";
    this.label3.TextAlign = ContentAlignment.MiddleRight;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(928, 265);
    this.Controls.Add((Control) this._BtnReset);
    this.Controls.Add((Control) this._comboBoxListSource);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this._SpecifNumberingControl);
    this.Controls.Add((Control) this._ObjTypesListBoxControl);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._BtnDelType);
    this.Controls.Add((Control) this._BtnAddType);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NumberingForSpecifRazdelsForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Специальные настройки нумерация для разделов спецификации";
    this.Closed += new EventHandler(this.NumberingForObjTypesForm_Closed);
    this.Load += new EventHandler(this.NumberingForObjTypesForm_Load);
    ((ISupportInitialize) this._ObjTypesListBoxControl).EndInit();
    this._comboBoxListSource.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Коллекция настроек нумерации позиций для типов объектов </summary>
  public SpecifRazdelNumbering SpecifRazdelNumbering
  {
    get => this._SpecifRazdelNumbering;
    set
    {
      this.LockControls();
      try
      {
        this._SpecifRazdelNumbering = value;
        this.RefreshTypesListControl();
        this.RefreshReadOnly();
        this.Changed = false;
        this.UpdateControls(true);
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      Size size = this.Size;
      int width1 = size.Width;
      int x = this._BtnCancel.Location.X;
      size = this._BtnCancel.Size;
      int width2 = size.Width;
      int num = x + width2;
      return width1 - num;
    }
  }

  /// <summary> Обработчик события "данные были обновленны" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void OnInitData(object sender, InitDataEventArgs e)
  {
    if (e.Tag == null)
      this.Close();
    this.LockControls();
    try
    {
      long razdelID = this._ObjTypesListBoxControl.SelectedItem == null ? 0L : (long) this._ListItemToRazdelIDHash[this._ObjTypesListBoxControl.SelectedItem];
      Guid razdelGuid = razdelID == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelID);
      if (e.Tag is SpecifNumberingFull tag)
        this._OldSpecifRazdelNumbering = tag.SpecifRazdelNumbering;
      this.SpecifRazdelNumbering = this._OldSpecifRazdelNumbering?.Clone();
      long razdelIdByGuid = razdelGuid == Guid.Empty ? 0L : SpecifRazdelNumbering.GetRazdelIDByGuid(razdelGuid);
      if (razdelIdByGuid != 0L)
      {
        foreach (long key in (IEnumerable) this._ListItemToRazdelIDHash.Values)
        {
          if (key == razdelIdByGuid)
          {
            this._ObjTypesListBoxControl.SelectedItem = this._ListItemToRazdelIDHash[(object) key];
            break;
          }
        }
      }
      this.RefreshReadOnly();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this._BtnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this._BtnOK.Enabled = !this.ReadOnly;
    if (this._EditModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._EditModeToolTip.Active)
        {
          this._EditModeToolTip.Active = false;
          this._ReadModeToolTip.Active = true;
        }
      }
      else if (this._ReadModeToolTip.Active)
      {
        this._ReadModeToolTip.Active = false;
        this._EditModeToolTip.Active = true;
      }
    }
    this._comboBoxListSource.Visible = this._SpecifRazdelNumbering != null && this._SpecifRazdelNumbering.SpecifNumberingFull != null && this._SpecifRazdelNumbering.SpecifNumberingFull.ParentLevel != null;
    if (this._comboBoxListSource.Visible)
    {
      this._comboBoxListSource.Properties.ReadOnly = this.ReadOnly;
      this._comboBoxListSource.BackColor = this._comboBoxListSource.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
      this._comboBoxListSource.Properties.Buttons[0].Visible = !this._comboBoxListSource.Properties.ReadOnly;
      this._comboBoxListSource.SelectedIndex = this._SpecifRazdelNumbering.Changed ? 1 : 0;
    }
    else
      this._comboBoxListSource.SelectedIndex = 1;
    this.label3.Visible = this._comboBoxListSource.Visible;
    this._BtnReset.Visible = !this._comboBoxListSource.Visible;
    this._BtnReset.Enabled = !this.ReadOnly && this._BtnReset.Visible;
    this._BtnAddType.Enabled = !this.ReadOnly && this._comboBoxListSource.SelectedIndex == 1;
    this._BtnDelType.Enabled = !this.ReadOnly && this._ObjTypesListBoxControl.SelectedItem != null && this._comboBoxListSource.SelectedIndex == 1;
    this._ObjTypesListBoxControl.BackColor = this._comboBoxListSource.SelectedIndex != 1 || this.ReadOnly ? Color.WhiteSmoke : SystemColors.Window;
    this.RefreshSelectedItem();
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly()
  {
    return this._SpecifRazdelNumbering == null || this._SpecifRazdelNumbering.SpecifNumberingFull == null || this._SpecifRazdelNumbering.SpecifNumberingFull.ReadOnly;
  }

  /// <summary>
  /// Обновление визуального списка типов объектов
  /// для которых задана специальная схема нумерации
  /// </summary>
  private void RefreshTypesListControl()
  {
    this._ObjTypesListBoxControl.Items.BeginUpdate();
    try
    {
      this._ListItemToRazdelIDHash.Clear();
      this._ObjTypesListBoxControl.Items.Clear();
      if (this._SpecifRazdelNumbering == null)
        return;
      foreach (KeyValuePair<long, SpecifNumbering> keyValuePair in this._SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash)
      {
        SpecifNumbering specifNumbering = keyValuePair.Value;
        this.AddRazdel(keyValuePair.Key, specifNumbering);
      }
      this.CheckAnySelected();
    }
    finally
    {
      this._ObjTypesListBoxControl.Items.EndUpdate();
    }
  }

  /// <summary> Контроль того, чтобы хоть один тип объекта был выбран. Если не выбран ни один, то выбирается первый </summary>
  public void CheckAnySelected()
  {
    if (this._ObjTypesListBoxControl.SelectedItem != null || this._ObjTypesListBoxControl.Items.Count <= 0)
      return;
    this._ObjTypesListBoxControl.SelectedIndex = 0;
    this.UpdateControls();
  }

  /// <summary> Получить наименование раздела спецификации </summary>
  /// <param name="razdelID"> Идентификатор раздела спецификации </param>
  /// <returns> наименование раздела спецификации </returns>
  public string GetRazdelName(long razdelID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(razdelID);
      return dbObject != null ? dbObject.Caption : string.Empty;
    }
  }

  /// <summary> Добавить в список раздел спецификации </summary>
  /// <param name="razdelID"> Идентификатор раздела спецификации </param>
  /// <returns> Схема нумерации </returns>
  private SpecifNumbering AddRazdel(long razdelID)
  {
    return this.AddRazdel(razdelID, (SpecifNumbering) null);
  }

  /// <summary> Добавить в список раздел спецификации </summary>
  /// <param name="razdelID"> Идентификатор раздела спецификации </param>
  /// <param name="specifNumbering"> Схема нумерации для данного раздела спецификации </param>
  /// <returns> Схема нумерации </returns>
  private SpecifNumbering AddRazdel(long razdelID, SpecifNumbering specifNumbering)
  {
    if (this._SpecifRazdelNumbering == null)
      return (SpecifNumbering) null;
    foreach (long num in (IEnumerable) this._ListItemToRazdelIDHash.Values)
    {
      if (num == razdelID)
        return (SpecifNumbering) null;
    }
    string razdelName = this.GetRazdelName(razdelID);
    if (!(razdelName != string.Empty))
      return (SpecifNumbering) null;
    SpecifNumbering specifNumbering1;
    if (specifNumbering == null)
    {
      specifNumbering1 = new SpecifNumbering();
      specifNumbering1.ParentLevel = (SpecifNumbering) this._SpecifRazdelNumbering.SpecifNumberingFull;
    }
    else
      specifNumbering1 = specifNumbering;
    ImageListBoxItem key = new ImageListBoxItem((object) razdelName, this._RazdelObjImageIndex);
    this._ObjTypesListBoxControl.Items.Add((object) key);
    this._ListItemToRazdelIDHash[(object) key] = (object) razdelID;
    return specifNumbering1;
  }

  /// <summary> Сохранить изменения </summary>
  protected virtual void SaveChanges()
  {
    this._OldSpecifRazdelNumbering.CopyParamsFrom(this._SpecifRazdelNumbering);
    this._OldSpecifRazdelNumbering.Changed = this._SpecifRazdelNumbering.Changed;
  }

  /// <summary> Обновление фрейма с информацией о схеме нумерации для выбраного типа объектов </summary>
  private void RefreshSelectedItem()
  {
    if (this._ObjTypesListBoxControl.SelectedItem != null)
    {
      this._SpecifNumberingControl.SpecifNumbering = this._SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash[(long) this._ListItemToRazdelIDHash[this._ObjTypesListBoxControl.SelectedItem]];
      this._SpecifNumberingControl.OverrideReadOnly = this._comboBoxListSource.SelectedIndex != 1;
    }
    else
      this._SpecifNumberingControl.SpecifNumbering = (SpecifNumbering) null;
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void NumberingForObjTypesForm_Load(object sender, EventArgs e)
  {
    this.RefreshSelectedItem();
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void NumberingForObjTypesForm_Closed(object sender, EventArgs e)
  {
    if (this.ReadOnly || this.DialogResult != DialogResult.OK)
      return;
    this.SaveChanges();
  }

  /// <summary> Тип объекта был выбран </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _ObjTypesListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.IsControlsLocked())
      this.RefreshSelectedItem();
    this.UpdateControls();
  }

  /// <summary> Была нажата кнопка "добавить тип объекта" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnAddType_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this._comboBoxListSource.SelectedIndex != 1 || !(this.OwnerControl is SpecifNumberingControlFull ownerControl))
      return;
    SelectSectionForm selectSectionForm = new SelectSectionForm(AVSSpecification.GetAllowableDocumentSections(ownerControl.SpecificationTemplateObjectId, new AVSDocumentType?()));
    selectSectionForm.Multiselect = true;
    if (selectSectionForm.ShowDialog() != DialogResult.OK)
      return;
    List<long> selectedSectionIds = selectSectionForm.GetSelectedSectionIDs();
    if (selectedSectionIds.Count <= 0)
      return;
    foreach (long num in selectedSectionIds)
    {
      SpecifNumbering specifNumbering = this.AddRazdel(num);
      if (specifNumbering != null)
        this._SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash[num] = specifNumbering;
    }
    this._SpecifRazdelNumbering.Changed = true;
    this.Changed = true;
    this.CheckAnySelected();
  }

  /// <summary> Была нажата кнопка "удалить" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnDelType_Click(object sender, EventArgs e)
  {
    int num1 = this._ObjTypesListBoxControl.SelectedIndex;
    long num2 = (long) this._ListItemToRazdelIDHash[this._ObjTypesListBoxControl.SelectedItem];
    if (this._SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash[num2] == null)
    {
      this.UpdateControls();
    }
    else
    {
      bool wasUpdated = false;
      long razdelID1 = this._ObjTypesListBoxControl.SelectedItem == null ? 0L : (long) this._ListItemToRazdelIDHash[this._ObjTypesListBoxControl.SelectedItem];
      Guid guid = razdelID1 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelID1);
      if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this._comboBoxListSource.SelectedIndex != 1)
        return;
      if (wasUpdated)
      {
        long razdelID2 = this._ObjTypesListBoxControl.SelectedItem == null ? 0L : (long) this._ListItemToRazdelIDHash[this._ObjTypesListBoxControl.SelectedItem];
        if ((razdelID2 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelID2)) != guid)
          return;
      }
      if (MessageBox.Show($"Удалить схему специальную нумерации для раздела '{this.GetRazdelName(num2)}'?", "Удаление специальной схемы нумерации изделий", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.LockControls();
      try
      {
        this._SpecifNumberingControl.SpecifNumbering = (SpecifNumbering) null;
        ImageListBoxItem selectedItem = (ImageListBoxItem) this._ObjTypesListBoxControl.SelectedItem;
        this._ListItemToRazdelIDHash.Remove((object) selectedItem);
        this._SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash.Remove(num2);
        this._ObjTypesListBoxControl.Items.Remove((object) selectedItem);
        if (num1 >= this._ObjTypesListBoxControl.Items.Count && num1 > 0)
          num1 = this._ObjTypesListBoxControl.Items.Count - 1;
        if (num1 < this._ObjTypesListBoxControl.Items.Count && num1 > 0)
          this._ObjTypesListBoxControl.SelectedIndex = num1;
        this._SpecifRazdelNumbering.Changed = true;
        this.UpdateControls(true);
        this.Changed = true;
      }
      finally
      {
        this.Changed = true;
        this.UnlockControls();
      }
    }
  }

  /// <summary> Был изменён источник данных </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxListSource_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = this._comboBoxListSource.SelectedIndex;
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly)
      return;
    if ((selectedIndex == 1 || !this._SpecifRazdelNumbering.Changed ? 6 : (int) MessageBox.Show("Сбросить изменения в настройках специальной нумерации для разделов?", "Настройки специальной нумерации для разделов", MessageBoxButtons.YesNo)) == 6)
    {
      this._SpecifRazdelNumbering.Changed = selectedIndex == 1;
      this.Changed = true;
      if (!this._SpecifRazdelNumbering.Changed)
      {
        this.LockControls();
        try
        {
          this._SpecifRazdelNumbering.LoadDefaultSchema();
          this.SpecifRazdelNumbering = this.SpecifRazdelNumbering;
          this.UpdateControls(true);
          this.Changed = true;
        }
        finally
        {
          this.UnlockControls();
        }
      }
      else
        this.UpdateControls(true);
    }
    else
      this.UpdateControls(true);
  }

  /// <summary> Была нажата кнопка "По умолчанию" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly || MessageBox.Show("Сбросить изменения в настройках специальной нумерации для разделов?", "Настройки специальной нумерации для разделов", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this._SpecifRazdelNumbering.Changed = true;
    this.Changed = true;
    this.LockControls();
    try
    {
      this._SpecifRazdelNumbering.LoadDefaultSchema();
      this.SpecifRazdelNumbering = this.SpecifRazdelNumbering;
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private class NumberingSelectorFilter : ISelectorFilter
  {
    private ArrayList _AllowableTypes = new ArrayList();

    /// <summary> Добавить в список возможных типов объектов новый тип объекта со всеми подтипами </summary>
    /// <param name="objTypeID"> Идентификатор типа объекта </param>
    public void AddType(int objTypeID) => this.AddType(objTypeID, true);

    /// <summary> Добавить в список возможных типов объектов новый тип объекта </summary>
    /// <param name="objTypeID"> Идентификатор типа объекта </param>
    /// <param name="withSubTypes"> Добавить ли так же все подтипы </param>
    public void AddType(int objTypeID, bool withSubTypes)
    {
      if (!this._AllowableTypes.Contains((object) objTypeID))
        this._AllowableTypes.Add((object) objTypeID);
      if (!withSubTypes)
        return;
      this.AddChildTypes(objTypeID);
    }

    /// <summary> Добавить в список возможных типов объектов все подтипы некоторого типа объекта </summary>
    /// <param name="objTypeID"> Идентификатор типа объекта </param>
    public void AddChildTypes(int objTypeID) => this.AddChildTypes(objTypeID, true);

    /// <summary> Добавить в список возможных типов объектов все подтипы некоторого типа объекта </summary>
    /// <param name="objTypeID"> Идентификатор типа объекта </param>
    /// <param name="recurce"> Добавлять рекурсивно </param>
    public void AddChildTypes(int objTypeID, bool recurce)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(objTypeID).Select(string.Empty);
        if (dataTable == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          if (!this._AllowableTypes.Contains((object) int32))
          {
            this._AllowableTypes.Add((object) int32);
            if (recurce)
              this.AddChildTypes(int32, true);
          }
        }
      }
    }

    /// <summary>Прошел фильтр</summary>
    /// <param name="category">Категория</param>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    bool ISelectorFilter.IsInFilter(int category, object id)
    {
      return category == 4 && id != null && this._AllowableTypes.Contains((object) (int) id);
    }
  }
}
