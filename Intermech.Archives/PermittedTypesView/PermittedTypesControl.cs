// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.PermittedTypesView.PermittedTypesControl
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.PermittedTypesView;

/// <summary>
/// Контрол с настройкой разрешенных типов документов для архива
/// </summary>
public class PermittedTypesControl : UserControl
{
  private bool _isModified;
  private long _archiveID;
  private ArchiveTypesUsingMode _typesUsingMode;
  private List<int> _allDocumentsTypes = new List<int>();
  private Dictionary<int, string> _archiveTypesInfo = new Dictionary<int, string>();
  /// <summary>
  /// Флаг определяет, проставляется ли чекбокс после переключения с другой закладки или вручную.
  /// Необходим, чтобы при переключении закладок и включенном радиобаттоне Запрещенных типов
  /// не появлялось сообщение о наличии настроек авторазмещения в архиве.
  /// Сообщение должно появляться только при включении режима ручками на вкладке.
  /// </summary>
  public bool _isCheckingManual;
  private GroupBox _gbChosenMode;
  private RadioButton _rbBannedTypes;
  private RadioButton _rbPermittedTypes;
  private RadioButton _rbAnyType;
  private ListBox _lbDocTypes;
  private Panel _buttonsPanel;
  private Button _bAdd;
  private Button _bDelete;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  private event EventHandler _onModified;

  /// <summary>Производились ли изменения на контроле</summary>
  public bool IsModified
  {
    get => this._isModified;
    set
    {
      this._isModified = value;
      if (this._lbDocTypes.SelectedItems.Count == 0)
        this._bDelete.Enabled = false;
      if (this._onModified == null)
        return;
      this._onModified.DynamicInvoke((object) this, (object) new EventArgs());
    }
  }

  /// <summary>ИД архива</summary>
  public long ArchiveID
  {
    get => this._archiveID;
    set => this._archiveID = value;
  }

  /// <summary>Информация о типах документов архива (ID, Name)</summary>
  public Dictionary<int, string> ArchiveTypesInfo
  {
    get => this._archiveTypesInfo;
    set
    {
      this._archiveTypesInfo = value;
      this.UpdateListBoxItems();
      if (this._archiveTypesInfo.Any<KeyValuePair<int, string>>())
        return;
      this._bDelete.Enabled = false;
    }
  }

  /// <summary>
  /// Список всех типов документов, существующих в данный момент
  /// </summary>
  public List<int> AllDocumentsTypes
  {
    set => this._allDocumentsTypes = value;
  }

  /// <summary>Режим использования списка типов</summary>
  public ArchiveTypesUsingMode TypesUsingMode
  {
    get => this._typesUsingMode;
    set
    {
      switch (value)
      {
        case ArchiveTypesUsingMode.AnyType:
          this._rbAnyType.Checked = true;
          this._typesUsingMode = value;
          break;
        case ArchiveTypesUsingMode.PermittedTypes:
          this._rbPermittedTypes.Checked = true;
          this._typesUsingMode = value;
          break;
        case ArchiveTypesUsingMode.ForbiddenTypes:
          this._rbBannedTypes.Checked = true;
          this._typesUsingMode = value;
          break;
        default:
          this._rbAnyType.Checked = true;
          this._typesUsingMode = ArchiveTypesUsingMode.AnyType;
          break;
      }
    }
  }

  /// <summary>Конструктор</summary>
  public PermittedTypesControl()
  {
    this.InitializeComponent();
    this._bDelete.Enabled = false;
    this._allDocumentsTypes = new List<int>();
    this._archiveTypesInfo = new Dictionary<int, string>();
  }

  /// <summary>Получить из словаря ID по имени типа</summary>
  /// <param name="typeName">Название типа</param>
  /// <returns></returns>
  private int GetIDbyTypeName(string typeName)
  {
    foreach (KeyValuePair<int, string> keyValuePair in this._archiveTypesInfo)
    {
      if (keyValuePair.Value.Equals(typeName))
        return keyValuePair.Key;
    }
    return 0;
  }

  /// <summary>Получает новый список типов.</summary>
  /// <param name="chosenTypeIDs">ID выбранных типов</param>
  /// <returns></returns>
  private Dictionary<int, string> BuildNewTypesInfo(List<int> chosenTypeIDs)
  {
    Dictionary<int, string> archiveTypesInfoCopy = new Dictionary<int, string>((IDictionary<int, string>) this._archiveTypesInfo);
    foreach (int chosenTypeId in chosenTypeIDs)
    {
      if (!this._archiveTypesInfo.ContainsKey(chosenTypeId) && !PermittedTypesControl.HasParentInList(chosenTypeId, archiveTypesInfoCopy))
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(chosenTypeId);
        if (childrenIdRecursive.Count<int>() > 1)
        {
          foreach (int key in childrenIdRecursive)
          {
            if (archiveTypesInfoCopy.ContainsKey(key))
              archiveTypesInfoCopy.Remove(key);
          }
        }
        IMSObjectType objectType = MetaDataHelper.GetObjectType(childrenIdRecursive[0]);
        if (objectType != null)
          archiveTypesInfoCopy.Add(chosenTypeId, objectType.ObjectTypeName);
      }
    }
    return archiveTypesInfoCopy;
  }

  /// <summary>
  /// Определяет, находится ли в исходном списке родительский тип для типа.
  /// </summary>
  /// <param name="typeID">ID типа.</param>
  /// <param name="archiveTypesInfoCopy">Список типа</param>
  /// <returns>
  /// 	<c>true</c> если тип имеет родителя в списке; иначе <c>false</c>.
  /// </returns>
  private static bool HasParentInList(int typeID, Dictionary<int, string> archiveTypesInfoCopy)
  {
    foreach (int key in MetaDataHelper.GetObjectTypeParentsID(typeID))
    {
      if (archiveTypesInfoCopy.ContainsKey(key))
        return true;
    }
    return false;
  }

  /// <summary>Получить список ИД выбранных в селектор форм типов</summary>
  /// <param name="selectorForm">Форма выбора</param>
  /// <returns></returns>
  private static List<int> GetChosenTypesIDs(SelectorForm selectorForm)
  {
    ArrayList idList = selectorForm.IDList;
    List<int> chosenTypesIds = new List<int>();
    foreach (object obj in idList)
      chosenTypesIds.Add(Convert.ToInt32(obj));
    return chosenTypesIds;
  }

  /// <summary>Переписать содержимое листбокса</summary>
  private void UpdateListBoxItems()
  {
    this._lbDocTypes.Items.Clear();
    List<string> stringList = new List<string>();
    if (this._archiveTypesInfo.Count > 0)
    {
      foreach (KeyValuePair<int, string> keyValuePair in this._archiveTypesInfo)
        stringList.Add(keyValuePair.Value);
    }
    stringList.Sort();
    foreach (object obj in stringList)
      this._lbDocTypes.Items.Add(obj);
    this._lbDocTypes.Refresh();
    this._bDelete.Enabled = false;
  }

  /// <summary>Событие на изменение содержимого. Можно подписываться</summary>
  public event EventHandler OnModified
  {
    add => this._onModified += value;
    remove => this._onModified -= value;
  }

  /// <summary>
  /// Выбрано значение "Архив может содержать элементы любых типов".
  /// </summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _rbAnyType_CheckedChanged(object sender, EventArgs e)
  {
    if (!this._rbAnyType.Checked)
      return;
    this._typesUsingMode = ArchiveTypesUsingMode.AnyType;
    this._buttonsPanel.Enabled = false;
    this._lbDocTypes.SelectedItems.Clear();
    this._lbDocTypes.Enabled = false;
    this.IsModified = true;
  }

  /// <summary>
  /// Выбрано значение "Архив может содержать документы только перечисленных ниже типов".
  /// </summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _rbPermittedTypes_CheckedChanged(object sender, EventArgs e)
  {
    if (!this._rbPermittedTypes.Checked)
      return;
    this._typesUsingMode = ArchiveTypesUsingMode.PermittedTypes;
    this._buttonsPanel.Enabled = true;
    this._lbDocTypes.Enabled = true;
    if (this._lbDocTypes.SelectedItems.Count == 0)
      this._bDelete.Enabled = false;
    this.IsModified = true;
  }

  /// <summary>
  /// Выбрано значение "Архив не может содержать документы перечисленных ниже типов".
  /// </summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _rbBannedTypes_CheckedChanged(object sender, EventArgs e)
  {
    if (!this._rbBannedTypes.Checked)
      return;
    if (this._isCheckingManual)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.AutoPlaceDocTypesAttrID);
        if ((objectAttributeById.ValuesCount != 1 ? 0 : (objectAttributeById.IsNull ? 1 : 0)) == 0)
        {
          if (MessageBox.Show(ServiceHolder.rm.GetString("Archives_189"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
          {
            this._rbAnyType.Checked = true;
            return;
          }
        }
      }
    }
    this._typesUsingMode = ArchiveTypesUsingMode.ForbiddenTypes;
    this._buttonsPanel.Enabled = true;
    this._lbDocTypes.Enabled = true;
    if (this._lbDocTypes.SelectedItems.Count == 0)
      this._bDelete.Enabled = false;
    this.IsModified = true;
  }

  /// <summary>Нажата кнопка "Добавить"</summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _bAdd_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(ServiceHolder.rm.GetString("Archives_157"), 4, true)
    {
      OnUncheckActions = SelectorForm.CheckActions.UncheckChildren,
      OnCheckActions = SelectorForm.CheckActions.CheckChildren,
      SelectorFilter = (ISelectorFilter) new ObjTypeSelectorFilter(this._allDocumentsTypes)
    };
    if (selectorForm.ShowDialog() == DialogResult.Cancel || selectorForm.IDList.Count == 0)
      return;
    this.ArchiveTypesInfo = this.BuildNewTypesInfo(PermittedTypesControl.GetChosenTypesIDs(selectorForm));
    this.IsModified = true;
  }

  /// <summary>Нажата кнопка "Удалить"</summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _bDelete_Click(object sender, EventArgs e)
  {
    foreach (object selectedItem in this._lbDocTypes.SelectedItems)
    {
      string typeName = selectedItem.ToString();
      if (this._archiveTypesInfo.ContainsValue(typeName))
        this._archiveTypesInfo.Remove(this.GetIDbyTypeName(typeName));
    }
    if (!this._archiveTypesInfo.Any<KeyValuePair<int, string>>())
      this._bDelete.Enabled = false;
    this.UpdateListBoxItems();
    this.IsModified = true;
  }

  /// <summary>Клик мышкой по листбоксу</summary>
  /// <param name="sender">Источник события.</param>
  /// <param name="e">Аргументы</param>
  private void _lbDocTypes_MouseClick(object sender, MouseEventArgs e)
  {
    if (this._lbDocTypes.SelectedItems.Count == 0)
      return;
    this._bDelete.Enabled = true;
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
    this._gbChosenMode = new GroupBox();
    this._rbBannedTypes = new RadioButton();
    this._rbPermittedTypes = new RadioButton();
    this._rbAnyType = new RadioButton();
    this._lbDocTypes = new ListBox();
    this._buttonsPanel = new Panel();
    this._bAdd = new Button();
    this._bDelete = new Button();
    this._gbChosenMode.SuspendLayout();
    this._buttonsPanel.SuspendLayout();
    this.SuspendLayout();
    this._gbChosenMode.Controls.Add((Control) this._rbBannedTypes);
    this._gbChosenMode.Controls.Add((Control) this._rbPermittedTypes);
    this._gbChosenMode.Controls.Add((Control) this._rbAnyType);
    this._gbChosenMode.Dock = DockStyle.Top;
    this._gbChosenMode.Location = new Point(0, 0);
    this._gbChosenMode.Name = "_gbChosenMode";
    this._gbChosenMode.RightToLeft = RightToLeft.No;
    this._gbChosenMode.Size = new Size(496, 99);
    this._gbChosenMode.TabIndex = 0;
    this._gbChosenMode.TabStop = false;
    this._rbBannedTypes.AutoSize = true;
    this._rbBannedTypes.Location = new Point(19, 68);
    this._rbBannedTypes.Name = "_rbBannedTypes";
    this._rbBannedTypes.Size = new Size(365, 17);
    this._rbBannedTypes.TabIndex = 2;
    this._rbBannedTypes.TabStop = true;
    this._rbBannedTypes.Text = "Архив не может содержать документы перечисленных ниже типов";
    this._rbBannedTypes.UseVisualStyleBackColor = true;
    this._rbBannedTypes.CheckedChanged += new EventHandler(this._rbBannedTypes_CheckedChanged);
    this._rbPermittedTypes.AutoSize = true;
    this._rbPermittedTypes.Location = new Point(19, 44);
    this._rbPermittedTypes.Name = "_rbPermittedTypes";
    this._rbPermittedTypes.Size = new Size(388, 17);
    this._rbPermittedTypes.TabIndex = 1;
    this._rbPermittedTypes.TabStop = true;
    this._rbPermittedTypes.Text = "Архив может содержать документы только перечисленных ниже типов";
    this._rbPermittedTypes.UseVisualStyleBackColor = true;
    this._rbPermittedTypes.CheckedChanged += new EventHandler(this._rbPermittedTypes_CheckedChanged);
    this._rbAnyType.AutoSize = true;
    this._rbAnyType.Location = new Point(19, 20);
    this._rbAnyType.Name = "_rbAnyType";
    this._rbAnyType.Size = new Size(276, 17);
    this._rbAnyType.TabIndex = 0;
    this._rbAnyType.TabStop = true;
    this._rbAnyType.Text = "Архив может содержать документы любых типов";
    this._rbAnyType.UseVisualStyleBackColor = true;
    this._rbAnyType.CheckedChanged += new EventHandler(this._rbAnyType_CheckedChanged);
    this._lbDocTypes.Dock = DockStyle.Fill;
    this._lbDocTypes.FormattingEnabled = true;
    this._lbDocTypes.Location = new Point(0, 99);
    this._lbDocTypes.Name = "_lbDocTypes";
    this._lbDocTypes.Size = new Size(496, 245);
    this._lbDocTypes.TabIndex = 1;
    this._lbDocTypes.MouseClick += new MouseEventHandler(this._lbDocTypes_MouseClick);
    this._buttonsPanel.Controls.Add((Control) this._bAdd);
    this._buttonsPanel.Controls.Add((Control) this._bDelete);
    this._buttonsPanel.Dock = DockStyle.Bottom;
    this._buttonsPanel.Location = new Point(0, 298);
    this._buttonsPanel.Name = "_buttonsPanel";
    this._buttonsPanel.Size = new Size(496, 46);
    this._buttonsPanel.TabIndex = 2;
    this._bAdd.Location = new Point(3, 3);
    this._bAdd.Name = "_bAdd";
    this._bAdd.Size = new Size(121, 27);
    this._bAdd.TabIndex = 0;
    this._bAdd.Text = "Добавить тип";
    this._bAdd.UseVisualStyleBackColor = true;
    this._bAdd.Click += new EventHandler(this._bAdd_Click);
    this._bDelete.Location = new Point(130, 3);
    this._bDelete.Name = "_bDelete";
    this._bDelete.Size = new Size(121, 27);
    this._bDelete.TabIndex = 1;
    this._bDelete.Text = "Удалить тип";
    this._bDelete.UseVisualStyleBackColor = true;
    this._bDelete.Click += new EventHandler(this._bDelete_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._buttonsPanel);
    this.Controls.Add((Control) this._lbDocTypes);
    this.Controls.Add((Control) this._gbChosenMode);
    this.Name = nameof (PermittedTypesControl);
    this.RightToLeft = RightToLeft.No;
    this.Size = new Size(496, 344);
    this._gbChosenMode.ResumeLayout(false);
    this._gbChosenMode.PerformLayout();
    this._buttonsPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
