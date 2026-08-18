// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.PermittedTypesView.ArchivePermittedTypesView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.PermittedTypesView;

/// <summary>Закладка "Разрешенные типы документов"</summary>
[ViewDescriptionProvider(typeof (ArchivePermittedTypesView.ArchivePermittedTypesViewDescriptionProvider))]
public class ArchivePermittedTypesView : UserControl, IView
{
  /// <summary>Иконка закладки</summary>
  private const int _imageIndex = -1;
  /// <summary>
  /// Контрол, управляющий списком типов и режимом его использования
  /// </summary>
  private readonly PermittedTypesControl _control;
  /// <summary>ID выделенного архива</summary>
  private long _archiveID;
  /// <summary>Список ID для типов документов архива</summary>
  private Dictionary<int, string> _archiveTypesInfo;
  /// <summary>Все типы документов, существующие в данный момент</summary>
  private List<int> _allDocumentsTypes;
  /// <summary>Изменялась ли закладка</summary>
  private bool _isModified;
  /// <summary>Закладка вызвана первый раз</summary>
  private bool _isFirst;
  /// <summary>Нужно ли устанавливать настройки закладки</summary>
  private bool _needInstallSettings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _bottom;
  private Panel _buttons;
  private Button _bCancel;
  private Button _bApply;
  private Panel _controlPanel;

  /// <summary>Изменялась ли закладка</summary>
  public bool IsModified
  {
    get => this._isModified;
    set
    {
      this._isModified = value;
      this._buttons.Enabled = value;
    }
  }

  /// <summary>Конструктор</summary>
  public ArchivePermittedTypesView()
  {
    this.InitializeComponent();
    this._allDocumentsTypes = new List<int>();
    this._archiveTypesInfo = new Dictionary<int, string>();
    this._control = new PermittedTypesControl();
    this._controlPanel.SuspendLayout();
    this._controlPanel.Controls.Add((Control) this._control);
    this._control.Dock = DockStyle.Fill;
    this._control.OnModified += new EventHandler(this._control_OnModified);
    this._controlPanel.ResumeLayout(false);
  }

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._archiveID = items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData ? itemData.Value : throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_159")));
    this._isFirst = true;
    this._needInstallSettings = true;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.</param>
  public void Activate(IView previousView)
  {
    this._control._isCheckingManual = false;
    if (this._isFirst)
    {
      ArchivePermittedTypesView.SetAllDocumentsTypesList(ref this._allDocumentsTypes);
      this._control.ArchiveID = this._archiveID;
      this._control.AllDocumentsTypes = this._allDocumentsTypes;
      this._isFirst = false;
    }
    this._control.IsModified = false;
    if (!this._needInstallSettings)
    {
      this._control._isCheckingManual = true;
    }
    else
    {
      this.UpdateDocTypesControl();
      this._needInstallSettings = false;
      this.IsModified = false;
      this._control._isCheckingManual = true;
    }
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.</param>
  public void Deactivate(IView nextView)
  {
    if (!this.IsModified)
      return;
    if (MessageBox.Show(ServiceHolder.rm.GetString("Archives_156"), ServiceHolder.rm.GetString("Archives_155"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
      this._bApply_Click((object) null, (EventArgs) null);
    else
      this._bCancel_Click((object) null, (EventArgs) null);
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  /// <value></value>
  public string Caption => ServiceHolder.rm.GetString("Archives_155");

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  /// <value></value>
  public int ImageIndex => -1;

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  /// <value></value>
  public int OrderID => 28;

  /// <summary>Собирает с контрола список ID типов для архива</summary>
  /// <returns>Список ID типов</returns>
  private List<int> GetTypesIDsFromControl()
  {
    List<int> typesIdsFromControl = new List<int>();
    foreach (KeyValuePair<int, string> keyValuePair in this._control.ArchiveTypesInfo)
      typesIdsFromControl.Add(keyValuePair.Key);
    return typesIdsFromControl;
  }

  /// <summary>
  /// Обновляет контрол с информацией о типах документов архива
  /// </summary>
  private void UpdateDocTypesControl()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById1 = sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.ArchiveTypesUsingModeID);
      IDBAttribute objectAttributeById2 = sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, MetaDataHelper.GetAttributeTypeID("cad00149-306c-11d8-b4e9-00304f19f545"));
      if (objectAttributeById2 != null)
      {
        this.SetArchiveTypesInfo(((IEnumerable<string>) objectAttributeById2.Descriptions).ToList<string>());
        this._control.ArchiveTypesInfo = this._archiveTypesInfo;
      }
      this._control.TypesUsingMode = (ArchiveTypesUsingMode) objectAttributeById1.AsInteger;
    }
  }

  /// <summary>
  /// Заполняет словарик с информацией о настроечных типах архива
  /// </summary>
  /// <param name="_typesGuidsList">ГУИДЫ типов документов архива</param>
  private void SetArchiveTypesInfo(List<string> _typesGuidsList)
  {
    this._archiveTypesInfo.Clear();
    foreach (string typesGuids in _typesGuidsList)
    {
      if (GuidHelper.IsGuid(typesGuids))
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid(typesGuids));
        if (objectType != null)
          this._archiveTypesInfo.Add(objectType.ObjectTypeID, objectType.ObjectTypeName);
      }
    }
  }

  /// <summary>
  /// Заполняет список всеми типами документов, которые есть в системе.
  /// </summary>
  private static void SetAllDocumentsTypesList(ref List<int> allDocumentsTypes)
  {
    allDocumentsTypes.Clear();
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    allDocumentsTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
  }

  /// <summary>Сохраняет атрибуты настроек архива</summary>
  private void SaveSettingsAttributes(SessionKeeper sk)
  {
    IDBAttribute objectAttributeById1 = sk.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.ArchiveTypesUsingModeID);
    IDBAttribute objectAttributeById2 = sk.Session.GetObjectAttributeByID(this._archiveID, MetaDataHelper.GetAttributeTypeID("cad00149-306c-11d8-b4e9-00304f19f545"));
    objectAttributeById2.ClearValues();
    this._archiveTypesInfo = this._control.ArchiveTypesInfo;
    foreach (KeyValuePair<int, string> keyValuePair in this._archiveTypesInfo)
    {
      Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(keyValuePair.Key);
      objectAttributeById2.AddValue((object) objectTypeGuid);
    }
    objectAttributeById1.AsInteger = (long) this._control.TypesUsingMode;
  }

  /// <summary>Изменение контрола.</summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _control_OnModified(object sender, EventArgs e)
  {
    this.IsModified = this._control.IsModified;
  }

  /// <summary>Нажатие кнопки "Применить".</summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _bApply_Click(object sender, EventArgs e)
  {
    List<int> typesIdsFromControl = this.GetTypesIDsFromControl();
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (this.CanApplySettings(sk, typesIdsFromControl))
        this.SaveSettingsAttributes(sk);
    }
    this.IsModified = false;
  }

  /// <summary>
  /// Определяет, совместимы ли новые настройки и содержимое архива
  /// </summary>
  /// <param name="sk">Хранитель сессии</param>
  /// <param name="archiveTypes">Типы документов для архива</param>
  /// <returns>
  /// 	<c>true</c> если совместимы; иначе <c>false</c>.
  /// </returns>
  private bool CanApplySettings(SessionKeeper sk, List<int> archiveTypes)
  {
    if (!(sk.Session.GetCustomService(typeof (IArchiveService)) is IArchiveService customService))
      return false;
    if (!customService.CheckArchiveSettings(this._archiveID, this._control.TypesUsingMode, archiveTypes, sk.Session.SessionGUID))
      throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_158")));
    return true;
  }

  /// <summary>Нажатие кнопки "Отмена"</summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _bCancel_Click(object sender, EventArgs e)
  {
    this._needInstallSettings = true;
    this.Activate((IView) null);
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
    this._bottom = new Panel();
    this._buttons = new Panel();
    this._bCancel = new Button();
    this._bApply = new Button();
    this._controlPanel = new Panel();
    this._bottom.SuspendLayout();
    this._buttons.SuspendLayout();
    this.SuspendLayout();
    this._bottom.Controls.Add((Control) this._buttons);
    this._bottom.Dock = DockStyle.Bottom;
    this._bottom.Location = new Point(0, 544);
    this._bottom.Name = "_bottom";
    this._bottom.Size = new Size(648, 40);
    this._bottom.TabIndex = 3;
    this._buttons.Controls.Add((Control) this._bCancel);
    this._buttons.Controls.Add((Control) this._bApply);
    this._buttons.Dock = DockStyle.Right;
    this._buttons.Location = new Point(381, 0);
    this._buttons.Name = "_buttons";
    this._buttons.Size = new Size(267, 40);
    this._buttons.TabIndex = 0;
    this._bCancel.FlatStyle = FlatStyle.System;
    this._bCancel.ImeMode = ImeMode.NoControl;
    this._bCancel.Location = new Point(134, 6);
    this._bCancel.Name = "_bCancel";
    this._bCancel.Size = new Size(121, 27);
    this._bCancel.TabIndex = 2;
    this._bCancel.Text = "Отмена";
    this._bCancel.Click += new EventHandler(this._bCancel_Click);
    this._bApply.FlatStyle = FlatStyle.System;
    this._bApply.ImeMode = ImeMode.NoControl;
    this._bApply.Location = new Point(7, 6);
    this._bApply.Name = "_bApply";
    this._bApply.Size = new Size(121, 27);
    this._bApply.TabIndex = 1;
    this._bApply.Text = "Применить";
    this._bApply.Click += new EventHandler(this._bApply_Click);
    this._controlPanel.Dock = DockStyle.Fill;
    this._controlPanel.Location = new Point(0, 0);
    this._controlPanel.Name = "_controlPanel";
    this._controlPanel.Size = new Size(648, 544);
    this._controlPanel.TabIndex = 4;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._controlPanel);
    this.Controls.Add((Control) this._bottom);
    this.Name = nameof (ArchivePermittedTypesView);
    this.Size = new Size(648, 584);
    this._bottom.ResumeLayout(false);
    this._buttons.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class ArchivePermittedTypesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = ServiceHolder.rm.GetString("Archives_155"),
        ImageIndex = -1,
        OrderID = 28
      };
    }
  }
}
