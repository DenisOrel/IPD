// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ExcludedRazdels
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса ExcludedRazdels </summary>
public class ExcludedRazdels : ExtForm
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private ImageListBoxControl _AllRazdelsListBoxControl;
  private ImageListBoxControl _NoNumberRazdelsListBoxControl;
  private Button _BtnAddRazdel;
  private Button _BtnDelRazdel;
  private Label label1;
  private Label label2;
  private ImageList _ObjTypesImageList;
  private Label label3;
  private ImageComboBoxEdit _comboBoxListSource;
  public Button _BtnReset;
  private ToolTipController _ReadModeToolTip;
  private long[] _NonNumneringRazdels = new long[0];
  private SpecifNumberingFull _SpecifNumberingFull;
  private bool _NonNumneringRazdelsChanged;
  private int _RazdelObjImageIndex = -1;
  private HybridDictionary _razdelIDToDescriptorHash = new HybridDictionary();
  private IStructualControlSupport _iStructualControlSupport;
  private InitDataEventHandler _onInitDataEventDelegateThis;

  public ExcludedRazdels()
  {
    this.InitializeComponent();
    this.Init((SpecifNumberingFull) null);
  }

  public ExcludedRazdels(SpecifNumberingFull specifNumberingFull)
  {
    this.InitializeComponent();
    this.Init(specifNumberingFull);
  }

  public ExcludedRazdels(
    Control ownerControl,
    SpecifNumberingFull specifNumberingFull,
    IStructualControlSupport iStructualControlSupport)
    : base(ownerControl)
  {
    this.InitializeComponent();
    this._onInitDataEventDelegateThis = new InitDataEventHandler(this.OnInitData);
    this._iStructualControlSupport = iStructualControlSupport;
    iStructualControlSupport.OnInitDataEvent += this._onInitDataEventDelegateThis;
    this.Init(specifNumberingFull);
  }

  private void Init(SpecifNumberingFull specifNumberingFull)
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
    this._SpecifNumberingFull = specifNumberingFull;
    this._NonNumneringRazdelsChanged = specifNumberingFull.NonNumneringRazdelsChanged;
    this.NonNumneringRazdels = this._SpecifNumberingFull == null ? (long[]) null : (long[]) this._SpecifNumberingFull._NonNumneringRazdels.Clone();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1522);
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
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
    this._BtnAddRazdel = new Button();
    this._BtnDelRazdel = new Button();
    this._BtnReset = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this._AllRazdelsListBoxControl = new ImageListBoxControl();
    this._ObjTypesImageList = new ImageList(this.components);
    this._NoNumberRazdelsListBoxControl = new ImageListBoxControl();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this._comboBoxListSource = new ImageComboBoxEdit();
    ((ISupportInitialize) this._AllRazdelsListBoxControl).BeginInit();
    ((ISupportInitialize) this._NoNumberRazdelsListBoxControl).BeginInit();
    this._comboBoxListSource.Properties.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(328, 268);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 3;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(455, 268);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 4;
    this._BtnCancel.Text = "Отмена";
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._BtnAddRazdel.FlatStyle = FlatStyle.System;
    this._BtnAddRazdel.Location = new Point(234, 104);
    this._BtnAddRazdel.Name = "_BtnAddRazdel";
    this._BtnAddRazdel.Size = new Size(121, 27);
    this._BtnAddRazdel.TabIndex = 7;
    this._BtnAddRazdel.Text = "Добавить >>";
    this._EditModeToolTip.SetToolTip((Control) this._BtnAddRazdel, "Добавить выбранный раздел спецификации в список ненумеруемых разделов");
    this._BtnAddRazdel.Click += new EventHandler(this._BtnAddRazdel_Click);
    this._BtnDelRazdel.FlatStyle = FlatStyle.System;
    this._BtnDelRazdel.Location = new Point(234, 144 /*0x90*/);
    this._BtnDelRazdel.Name = "_BtnDelRazdel";
    this._BtnDelRazdel.Size = new Size(121, 27);
    this._BtnDelRazdel.TabIndex = 8;
    this._BtnDelRazdel.Text = "<< Удалить";
    this._EditModeToolTip.SetToolTip((Control) this._BtnDelRazdel, "Добавить раздел спецификации в список разделов со специальными настройками нумерации");
    this._BtnDelRazdel.Click += new EventHandler(this._BtnDelRazdel_Click);
    this._BtnReset.Enabled = false;
    this._BtnReset.FlatStyle = FlatStyle.System;
    this._BtnReset.Location = new Point(8, 268);
    this._BtnReset.Name = "_BtnReset";
    this._BtnReset.Size = new Size(121, 27);
    this._BtnReset.TabIndex = 13;
    this._BtnReset.Text = "По умолчанию";
    this._EditModeToolTip.SetToolTip((Control) this._BtnReset, "Вернуть список к значению по умолчанию");
    this._BtnReset.Visible = false;
    this._BtnReset.Click += new EventHandler(this._BtnReset_Click);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this._AllRazdelsListBoxControl.ImageList = this._ObjTypesImageList;
    this._AllRazdelsListBoxControl.ItemHeight = 18;
    this._AllRazdelsListBoxControl.Location = new Point(8, 24);
    this._AllRazdelsListBoxControl.Name = "_AllRazdelsListBoxControl";
    this._AllRazdelsListBoxControl.Size = new Size(217, 232);
    this._AllRazdelsListBoxControl.TabIndex = 5;
    this._AllRazdelsListBoxControl.ToolTip = "Список разделов спецификации, которые должны нумероваться";
    this._AllRazdelsListBoxControl.SelectedIndexChanged += new EventHandler(this._AllRazdelsListBoxControl_SelectedIndexChanged);
    this._AllRazdelsListBoxControl.DrawItem += new ListBoxDrawItemEventHandler(this._AllRazdelsListBoxControl_DrawItem);
    this._AllRazdelsListBoxControl.MouseDown += new MouseEventHandler(this._AllRazdelsListBoxControl_MouseDown);
    this._ObjTypesImageList.ColorDepth = ColorDepth.Depth8Bit;
    this._ObjTypesImageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this._ObjTypesImageList.TransparentColor = Color.Transparent;
    this._NoNumberRazdelsListBoxControl.ImageList = this._ObjTypesImageList;
    this._NoNumberRazdelsListBoxControl.ItemHeight = 18;
    this._NoNumberRazdelsListBoxControl.Location = new Point(359, 24);
    this._NoNumberRazdelsListBoxControl.Name = "_NoNumberRazdelsListBoxControl";
    this._NoNumberRazdelsListBoxControl.Size = new Size(217, 232);
    this._NoNumberRazdelsListBoxControl.TabIndex = 6;
    this._NoNumberRazdelsListBoxControl.ToolTip = "Список разделов спецификации, которые не должны нумероваться";
    this._NoNumberRazdelsListBoxControl.SelectedIndexChanged += new EventHandler(this._NoNumberRazdelsListBoxControl_SelectedIndexChanged);
    this._NoNumberRazdelsListBoxControl.DrawItem += new ListBoxDrawItemEventHandler(this._AllRazdelsListBoxControl_DrawItem);
    this._NoNumberRazdelsListBoxControl.MouseDown += new MouseEventHandler(this._NoNumberRazdelsListBoxControl_MouseDown);
    this.label1.Location = new Point(8, 4);
    this.label1.Name = "label1";
    this.label1.Size = new Size(217, 16 /*0x10*/);
    this.label1.TabIndex = 9;
    this.label1.Text = "Разделы спецификации";
    this.label1.TextAlign = ContentAlignment.BottomCenter;
    this.label2.Location = new Point(359, 4);
    this.label2.Name = "label2";
    this.label2.Size = new Size(217, 16 /*0x10*/);
    this.label2.TabIndex = 10;
    this.label2.Text = "Ненумеруемые разделы спецификации";
    this.label2.TextAlign = ContentAlignment.BottomCenter;
    this.label3.Location = new Point(14, 275);
    this.label3.Name = "label3";
    this.label3.Size = new Size(62, 13);
    this.label3.TabIndex = 11;
    this.label3.Text = "Список:";
    this.label3.TextAlign = ContentAlignment.MiddleRight;
    this._comboBoxListSource.EditValue = (object) false;
    this._comboBoxListSource.Location = new Point(82, 272);
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
    this._comboBoxListSource.TabIndex = 12;
    this._comboBoxListSource.ToolTip = "Выбор, откуда брать список";
    this._comboBoxListSource.SelectedIndexChanged += new EventHandler(this._comboBoxListSource_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(585, 304);
    this.Controls.Add((Control) this._BtnReset);
    this.Controls.Add((Control) this._comboBoxListSource);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this._NoNumberRazdelsListBoxControl);
    this.Controls.Add((Control) this._AllRazdelsListBoxControl);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._BtnDelRazdel);
    this.Controls.Add((Control) this._BtnAddRazdel);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExcludedRazdels);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выбор ненумеруемых разделов";
    this.Closed += new EventHandler(this.ExcludedRazdels_Closed);
    this.Load += new EventHandler(this.ExcludedRazdels_Load);
    ((ISupportInitialize) this._AllRazdelsListBoxControl).EndInit();
    ((ISupportInitialize) this._NoNumberRazdelsListBoxControl).EndInit();
    this._comboBoxListSource.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Массив идентификаторов ненумеруемых разделов </summary>
  public long[] NonNumneringRazdels
  {
    get => this._NonNumneringRazdels;
    set
    {
      this._NonNumneringRazdels = value;
      this.ReloadAllLists();
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
      long razdelId1 = this._AllRazdelsListBoxControl.SelectedValue == null ? 0L : ((ExcludedRazdels.RazdelDescriptor) this._AllRazdelsListBoxControl.SelectedValue).RazdelID;
      long razdelId2 = this._NoNumberRazdelsListBoxControl.SelectedValue == null ? 0L : ((ExcludedRazdels.RazdelDescriptor) this._NoNumberRazdelsListBoxControl.SelectedValue).RazdelID;
      Guid razdelGuid1 = razdelId1 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelId1);
      Guid razdelGuid2 = razdelId2 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelId2);
      this._SpecifNumberingFull = (SpecifNumberingFull) e.Tag;
      if (this._SpecifNumberingFull != null)
      {
        this._NonNumneringRazdelsChanged = this._SpecifNumberingFull.NonNumneringRazdelsChanged;
        this.NonNumneringRazdels = (long[]) this._SpecifNumberingFull._NonNumneringRazdels.Clone();
      }
      long razdelIdByGuid1 = razdelGuid1 == Guid.Empty ? 0L : SpecifRazdelNumbering.GetRazdelIDByGuid(razdelGuid1);
      long razdelIdByGuid2 = razdelGuid2 == Guid.Empty ? 0L : SpecifRazdelNumbering.GetRazdelIDByGuid(razdelGuid2);
      if (razdelIdByGuid1 != 0L)
      {
        ExcludedRazdels.RazdelDescriptor razdelDescriptor = (ExcludedRazdels.RazdelDescriptor) this._razdelIDToDescriptorHash[(object) razdelIdByGuid1];
        if (this._AllRazdelsListBoxControl.Items.IndexOf((object) razdelDescriptor.ListBoxItem) != -1)
          this._AllRazdelsListBoxControl.SelectedItem = (object) razdelDescriptor.ListBoxItem;
      }
      if (razdelIdByGuid2 != 0L)
      {
        ExcludedRazdels.RazdelDescriptor razdelDescriptor = (ExcludedRazdels.RazdelDescriptor) this._razdelIDToDescriptorHash[(object) razdelIdByGuid2];
        if (this._NoNumberRazdelsListBoxControl.Items.IndexOf((object) razdelDescriptor.ListBoxItem) != -1)
          this._NoNumberRazdelsListBoxControl.SelectedItem = (object) razdelDescriptor.ListBoxItem;
      }
      this.RefreshReadOnly();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Обновить все списки </summary>
  private void ReloadAllLists()
  {
    this.LockControls();
    try
    {
      this.InitAllListBoxItems();
      this.LoadNonNumberingRazdels();
      this.LoadPossibleRazdels();
      this.RefreshReadOnly();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Создание списка всех возможных ListBoxItems-ов </summary>
  private void InitAllListBoxItems()
  {
    this._razdelIDToDescriptorHash.Clear();
    if (!(this.OwnerControl is SpecifNumberingControlFull ownerControl))
      return;
    List<SpecificationSectionInfo> documentSections = AVSSpecification.GetAllowableDocumentSections(ownerControl.SpecificationTemplateObjectId, new AVSDocumentType?());
    string empty = string.Empty;
    foreach (SpecificationSectionInfo specificationSectionInfo in documentSections)
    {
      long sectionId = specificationSectionInfo.SectionID;
      string caption = specificationSectionInfo.Caption;
      ImageListBoxItem listBoxItem = new ImageListBoxItem();
      ExcludedRazdels.RazdelDescriptor razdelDescriptor = new ExcludedRazdels.RazdelDescriptor(sectionId, caption, listBoxItem);
      listBoxItem.Value = (object) razdelDescriptor;
      listBoxItem.ImageIndex = this._RazdelObjImageIndex;
      this._razdelIDToDescriptorHash.Add((object) sectionId, (object) razdelDescriptor);
    }
  }

  /// <summary> Загрузка списка всех разделов спецификации </summary>
  private void LoadPossibleRazdels()
  {
    this._AllRazdelsListBoxControl.Items.BeginUpdate();
    try
    {
      this._AllRazdelsListBoxControl.Items.Clear();
      foreach (ExcludedRazdels.RazdelDescriptor razdelDescriptor in (IEnumerable) this._razdelIDToDescriptorHash.Values)
      {
        if (Array.IndexOf<long>(this._NonNumneringRazdels, razdelDescriptor.RazdelID) == -1)
          this._AllRazdelsListBoxControl.Items.Add((object) razdelDescriptor.ListBoxItem);
      }
      this.CheckAnySelected(this._AllRazdelsListBoxControl);
    }
    finally
    {
      this._AllRazdelsListBoxControl.Items.EndUpdate();
    }
  }

  /// <summary> Загрузка списка ненумеруемых разделов спецификации </summary>
  private void LoadNonNumberingRazdels()
  {
    this._NoNumberRazdelsListBoxControl.Items.BeginUpdate();
    try
    {
      this._NoNumberRazdelsListBoxControl.Items.Clear();
      foreach (ExcludedRazdels.RazdelDescriptor razdelDescriptor in (IEnumerable) this._razdelIDToDescriptorHash.Values)
      {
        if (Array.IndexOf<long>(this._NonNumneringRazdels, razdelDescriptor.RazdelID) >= 0)
          this._NoNumberRazdelsListBoxControl.Items.Add((object) razdelDescriptor.ListBoxItem);
      }
      this.CheckAnySelected(this._NoNumberRazdelsListBoxControl);
    }
    finally
    {
      this._NoNumberRazdelsListBoxControl.Items.EndUpdate();
    }
  }

  /// <summary> Выбираю вервый попавшийся элесент в ListBox-е </summary>
  /// <param name="imageListBoxControl"></param>
  private void CheckAnySelected(ImageListBoxControl imageListBoxControl)
  {
    if (imageListBoxControl.ItemCount <= 0 || imageListBoxControl.SelectedItem != null)
      return;
    imageListBoxControl.SelectedIndex = 0;
  }

  /// <summary> Сохранить изменения </summary>
  protected virtual void SaveChanges()
  {
    if (this._SpecifNumberingFull == null || this.ReadOnly)
      return;
    this._SpecifNumberingFull._NonNumneringRazdels = this._NonNumneringRazdels;
    this._SpecifNumberingFull.NonNumneringRazdelsChanged = this._NonNumneringRazdelsChanged;
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
    this._comboBoxListSource.Visible = this._SpecifNumberingFull != null && this._SpecifNumberingFull.ParentLevel != null;
    if (this._comboBoxListSource.Visible)
    {
      this._comboBoxListSource.SelectedIndex = this._NonNumneringRazdelsChanged ? 1 : 0;
      this._comboBoxListSource.Properties.ReadOnly = this.ReadOnly;
      this._comboBoxListSource.BackColor = this._comboBoxListSource.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
      this._comboBoxListSource.Properties.Buttons[0].Visible = !this._comboBoxListSource.Properties.ReadOnly;
    }
    else
      this._comboBoxListSource.SelectedIndex = 1;
    this.label3.Visible = this._comboBoxListSource.Visible;
    this._BtnReset.Visible = !this._comboBoxListSource.Visible;
    this._BtnReset.Enabled = !this.ReadOnly;
    this._AllRazdelsListBoxControl.BackColor = this._comboBoxListSource.SelectedIndex != 1 || this.ReadOnly ? Color.WhiteSmoke : SystemColors.Window;
    this._NoNumberRazdelsListBoxControl.BackColor = this._comboBoxListSource.SelectedIndex != 1 || this.ReadOnly ? Color.WhiteSmoke : SystemColors.Window;
    this._BtnAddRazdel.Enabled = this._SpecifNumberingFull != null && !this.ReadOnly && this._AllRazdelsListBoxControl.SelectedItem != null && this._comboBoxListSource.SelectedIndex == 1;
    this._BtnDelRazdel.Enabled = this._SpecifNumberingFull != null && !this.ReadOnly && this._NoNumberRazdelsListBoxControl.SelectedItem != null && this._comboBoxListSource.SelectedIndex == 1;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly()
  {
    return this._SpecifNumberingFull == null || this._SpecifNumberingFull.ReadOnly;
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ExcludedRazdels_Load(object sender, EventArgs e)
  {
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ExcludedRazdels_Closed(object sender, EventArgs e)
  {
    if (this.ReadOnly || this.DialogResult != DialogResult.OK)
      return;
    this.SaveChanges();
  }

  /// <summary> Была нажата кнопка "добавить раздел в список ненумеруемых разделов" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnAddRazdel_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    long razdelId1 = this._AllRazdelsListBoxControl.SelectedValue == null ? 0L : ((ExcludedRazdels.RazdelDescriptor) this._AllRazdelsListBoxControl.SelectedValue).RazdelID;
    Guid guid1 = razdelId1 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelId1);
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this._comboBoxListSource.SelectedIndex != 1)
      return;
    if (wasUpdated)
    {
      long razdelId2 = this._AllRazdelsListBoxControl.SelectedValue == null ? 0L : ((ExcludedRazdels.RazdelDescriptor) this._AllRazdelsListBoxControl.SelectedValue).RazdelID;
      Guid guid2 = razdelId2 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelId2);
      if (guid1 != guid2)
        return;
    }
    ImageListBoxItem selectedItem = (ImageListBoxItem) this._AllRazdelsListBoxControl.SelectedItem;
    if (selectedItem != null)
    {
      this._AllRazdelsListBoxControl.Items.Remove((object) selectedItem);
      this._NoNumberRazdelsListBoxControl.Items.Add((object) selectedItem);
      this._NonNumneringRazdels = (long[]) ArrayEditHelper.AddItemToArray((Array) this._NonNumneringRazdels, (object) ((ExcludedRazdels.RazdelDescriptor) selectedItem.Value).RazdelID);
      this.CheckAnySelected(this._NoNumberRazdelsListBoxControl);
      this.Changed = true;
      this._NonNumneringRazdelsChanged = true;
    }
    else
      this.UpdateControls();
  }

  /// <summary> Была нажата кнопка "добавить раздел в список ненумеруемых разделов" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnDelRazdel_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    long razdelId1 = this._NoNumberRazdelsListBoxControl.SelectedValue == null ? 0L : ((ExcludedRazdels.RazdelDescriptor) this._NoNumberRazdelsListBoxControl.SelectedValue).RazdelID;
    Guid guid1 = razdelId1 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelId1);
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this._comboBoxListSource.SelectedIndex != 1)
      return;
    if (wasUpdated)
    {
      long razdelId2 = this._NoNumberRazdelsListBoxControl.SelectedValue == null ? 0L : ((ExcludedRazdels.RazdelDescriptor) this._NoNumberRazdelsListBoxControl.SelectedValue).RazdelID;
      Guid guid2 = razdelId2 == 0L ? Guid.Empty : SpecifRazdelNumbering.GetRazdelGuidByID(razdelId2);
      if (guid1 != guid2)
        return;
    }
    ImageListBoxItem selectedItem = (ImageListBoxItem) this._NoNumberRazdelsListBoxControl.SelectedItem;
    if (selectedItem != null)
    {
      this._NoNumberRazdelsListBoxControl.Items.Remove((object) selectedItem);
      this._AllRazdelsListBoxControl.Items.Add((object) selectedItem);
      int index = Array.IndexOf<long>(this._NonNumneringRazdels, ((ExcludedRazdels.RazdelDescriptor) selectedItem.Value).RazdelID);
      if (index >= 0)
        this._NonNumneringRazdels = (long[]) ArrayEditHelper.RemoveItemAt((Array) this._NonNumneringRazdels, index);
      this.CheckAnySelected(this._AllRazdelsListBoxControl);
      this.Changed = true;
      this._NonNumneringRazdelsChanged = true;
    }
    else
      this.UpdateControls();
  }

  /// <summary> Был выбран раздел спецификации </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _AllRazdelsListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls(false);
  }

  /// <summary> Был выбран раздел спецификации </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _NoNumberRazdelsListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls(false);
  }

  /// <summary> Двойной клик по списку разделов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _AllRazdelsListBoxControl_MouseDown(object sender, MouseEventArgs e)
  {
    if (this._comboBoxListSource.SelectedIndex != 1 || e.Clicks != 2 || this._AllRazdelsListBoxControl.IndexFromPoint(new Point(e.X, e.Y)) == -1)
      return;
    this._BtnAddRazdel_Click((object) null, (EventArgs) null);
  }

  /// <summary> Двойной клик по списку разделов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _NoNumberRazdelsListBoxControl_MouseDown(object sender, MouseEventArgs e)
  {
    if (this._comboBoxListSource.SelectedIndex != 1 || e.Clicks != 2 || this._NoNumberRazdelsListBoxControl.IndexFromPoint(new Point(e.X, e.Y)) == -1)
      return;
    this._BtnDelRazdel_Click((object) null, (EventArgs) null);
  }

  /// <summary> Был изменён источник данных для схемы </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxListSource_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = this._comboBoxListSource.SelectedIndex;
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly)
      return;
    if ((selectedIndex == 1 || !this._NonNumneringRazdelsChanged ? 6 : (int) MessageBox.Show("Сбросить изменения в списке ненумеруемых разделов?", "Список ненумеруемых разделов", MessageBoxButtons.YesNo)) == 6)
    {
      this._NonNumneringRazdelsChanged = selectedIndex == 1;
      if (!this._NonNumneringRazdelsChanged)
      {
        this._NonNumneringRazdels = this._SpecifNumberingFull.LoadDefaultNonNumneringRazdels();
        this._NonNumneringRazdelsChanged = false;
        this.ReloadAllLists();
      }
      this.Changed = true;
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
    if (this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly || MessageBox.Show("Сбросить изменения в списке ненумеруемых разделов?", "Список ненумеруемых разделов", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this._NonNumneringRazdelsChanged = true;
    this._NonNumneringRazdels = this._SpecifNumberingFull.LoadDefaultNonNumneringRazdels();
    this.ReloadAllLists();
    this.Changed = true;
  }

  private void _AllRazdelsListBoxControl_DrawItem(object sender, ListBoxDrawItemEventArgs e)
  {
    ImageListBoxControl imageListBoxControl = sender as ImageListBoxControl;
    if (!(e.Item is ExcludedRazdels.RazdelDescriptor razdelDescriptor) || imageListBoxControl == null)
      return;
    e.Handled = true;
    Graphics graphics = e.Graphics;
    Rectangle bounds = e.Bounds;
    int num1 = 2;
    Image image = (Image) null;
    if (razdelDescriptor.ListBoxItem.Images.Images.Count > razdelDescriptor.ListBoxItem.ImageIndex)
      image = razdelDescriptor.ListBoxItem.Images.Images[razdelDescriptor.ListBoxItem.ImageIndex];
    int num2 = 0;
    if (image != null)
      num2 = image.Width;
    else if (razdelDescriptor.ListBoxItem.Images.Images.Count > 0)
      num2 = razdelDescriptor.ListBoxItem.Images.Images[0].Width;
    Point point1;
    ref Point local1 = ref point1;
    int x1 = bounds.Location.X + num1;
    Point location = bounds.Location;
    int y1 = location.Y + (bounds.Height - num2) / 2;
    local1 = new Point(x1, y1);
    SizeF sizeF = graphics.MeasureString(razdelDescriptor.Caption, imageListBoxControl.Font);
    Point point2;
    ref Point local2 = ref point2;
    location = bounds.Location;
    int x2 = location.X + num1 + num2;
    location = bounds.Location;
    int y2 = location.Y + (bounds.Height - (int) sizeF.Height) / 2;
    local2 = new Point(x2, y2);
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
    {
      Pen pen = (Pen) SystemPens.ActiveBorder.Clone();
      if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
      {
        graphics.FillRectangle(SystemBrushes.Highlight, bounds);
      }
      else
      {
        pen = (Pen) SystemPens.InactiveBorder.Clone();
        graphics.FillRectangle(SystemBrushes.Control, bounds);
      }
      pen.DashStyle = DashStyle.Dot;
      Rectangle rect = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
      graphics.DrawRectangle(pen, rect);
    }
    graphics.DrawString(razdelDescriptor.Caption, imageListBoxControl.Font, (Brush) new SolidBrush(imageListBoxControl.ForeColor), (PointF) point2);
    if (image == null)
      return;
    graphics.DrawImage(image, point1);
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

  /// <summary> Дескриптор раздела спецификации </summary>
  private class RazdelDescriptor
  {
    public long RazdelID;
    public string Caption = string.Empty;
    public ImageListBoxItem ListBoxItem;

    public RazdelDescriptor(long razdelID, string caption, ImageListBoxItem listBoxItem)
    {
      this.RazdelID = razdelID;
      this.Caption = caption;
      this.ListBoxItem = listBoxItem;
    }

    public override string ToString() => this.Caption;
  }
}
