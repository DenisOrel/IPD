
// Type: Intermech.Client.Core.UserControlFindReplaceTextInAttributes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Контрол для поиска и замены текста, предлагающий так же определить список атрибутов, по которым должен производиться поиск
/// </summary>
public class UserControlFindReplaceTextInAttributes : 
  UserControlFindReplaceText,
  IAttributesSelection
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox _groupBoxFindAttributes;
  private UserControlSelectAttributes _userControlSelectAttributes;

  public UserControlFindReplaceTextInAttributes()
  {
    this.InitializeComponent();
    this.UpdatePositions();
  }

  /// <summary> Признака видимости GroupBox-ов </summary>
  /// <param name="visible"> Признак видимости GroupBox-ов </param>
  protected override void SetGroupBoxesVisible(bool visible)
  {
    base.SetGroupBoxesVisible(visible);
    this._groupBoxFindAttributes.Enabled = visible;
    this._groupBoxFindAttributes.Anchor = visible ? AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right : AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._groupBoxFindAttributes.Visible = visible;
  }

  /// <summary> Установка высоты GroupBox-ов </summary>
  /// <param name="bottomGroupsHeight"> Высота GroupBox-ов </param>
  protected override void SetGroupBoxesHeight(int bottomGroupsHeight)
  {
    base.SetGroupBoxesHeight(bottomGroupsHeight);
    this._groupBoxFindAttributes.Height = bottomGroupsHeight;
  }

  /// <summary> Добавить в список атрибуты, которые могут принадлежать некоторым типам связей </summary>
  /// <param name="relationTypeIDs"> Идентификаторы типов связей, атрибуты которых должны быть добавлены в список </param>
  public void AddRelationAttributes(int[] relationTypeIDs)
  {
    this._userControlSelectAttributes.AddRelationAttributes(relationTypeIDs);
  }

  /// <summary> Добавить в список атрибуты, которые могут принадлежать некоторым типам объектов </summary>
  /// <param name="objectTypeIDs"> Идентификаторы типов объектов, атрибуты которых должны быть добавлены в список </param>
  public void AddObjectAttributes(int[] objectTypeIDs)
  {
    this._userControlSelectAttributes.AddObjectAttributes(objectTypeIDs);
  }

  /// <summary>
  /// Добавить в список атрибуты.
  /// Все добавленые атрибуты считаются принадлежащими связи
  /// (всё равно, в том случае, если атрибут относиться связи при чтении значения
  /// необходимо проверять его принадлежность связи, и, если он связи не принадлежит,
  /// пытаться прочитать его из объекта)
  /// </summary>
  /// <param name="attributeIDs"> Идентификаторы атрибутов которые должны быть добавлены в список </param>
  /// <returns> Список дескрипторов добавленных атрибутов </returns>
  public AttributeDescriptorList AddAttributes(int[] attributeIDs)
  {
    return this._userControlSelectAttributes.AddAttributes(attributeIDs);
  }

  /// <summary> Добавить в список атрибуты </summary>
  /// <param name="attributeIDs"> Идентификаторы атрибутов которые должны быть добавлены в список </param>
  /// <param name="isRelationAttributes"> Признак того, что дабавляемые атрибуты относятся к связи </param>
  /// <returns> Список дескрипторов добавленных атрибутов </returns>
  public AttributeDescriptorList AddAttributes(int[] attributeIDs, bool isRelationAttributes)
  {
    return this._userControlSelectAttributes.AddAttributes(attributeIDs, isRelationAttributes);
  }

  /// <summary> Добавить в список атрибуты </summary>
  /// <param name="attributeDescriptorList"> Список дескрипторов атрибутов, которые должны быть добавлены в список </param>
  public void AddAttributes(AttributeDescriptorList attributeDescriptorList)
  {
    this._userControlSelectAttributes.AddAttributes(attributeDescriptorList);
  }

  /// <summary> Выставить Checked = true у атрибутов с переданными идентификаторами </summary>
  /// <param name="attributeIDs"> Массив идентификаторов атрибутов, у которых свойство Checked должно стать = true </param>
  public void SetCheckedAttributes(int[] attributeIDs)
  {
    this._userControlSelectAttributes.SetCheckedAttributes(attributeIDs);
  }

  /// <summary> Выставить Checked = true у атрибутов с переданными идентификаторами </summary>
  /// <param name="attributeIDs"> Массив идентификаторов атрибутов, у которых свойство Checked должно стать = true </param>
  /// <param name="moveToTop"> Переместить ли данные атрибуты на самый верх списка выбора </param>
  public void SetCheckedAttributes(int[] attributeIDs, bool moveToTop)
  {
    this._userControlSelectAttributes.SetCheckedAttributes(attributeIDs, moveToTop);
  }

  /// <summary> Получить список дескрипторов отмеченых атрибутов </summary>
  /// <returns> Список дескрипторов отмеченых атрибутов </returns>
  public AttributeDescriptorList GetCheckedAttributesList()
  {
    return this._userControlSelectAttributes.GetCheckedAttributesList();
  }

  /// <summary> Очистить список атрибутов </summary>
  public void ClearAttributesList() => this._userControlSelectAttributes.ClearAttributesList();

  /// <summary> Отметить все атрибуты, доступные для выбора как отмеченые </summary>
  public void CheckAllAttributes() => this._userControlSelectAttributes.CheckAllAttributes();

  /// <summary> Снять отметки со всех отмеченых атрибутов </summary>
  public void UncheckAllAttributes() => this._userControlSelectAttributes.UncheckAllAttributes();

  /// <summary> Список загруженных атрибутов </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public AttributeDescriptorList LoadedAttributes
  {
    get => this._userControlSelectAttributes.LoadedAttributes;
    set => this._userControlSelectAttributes.LoadedAttributes = value;
  }

  /// <summary> Вызывается перед началом редактирования списка атрибутов (ускоряет работу, блокируя обновление визуальных контролов) </summary>
  public void BeginUpdate() => this._userControlSelectAttributes.BeginUpdate();

  /// <summary> Вызывается по окончании редактирования списка атрибутов (разблокирует обновление визуальных контролов, обновляет их содержимое) </summary>
  public void EndUpdate() => this._userControlSelectAttributes.EndUpdate();

  public override void UpdatePositions()
  {
    base.UpdatePositions();
    int num = 4;
    if (this._groupBoxFindAttributes == null)
      return;
    this._groupBoxFindAttributes.Top = this._groupBoxFindOptions.Top;
    this._groupBoxFindAttributes.Left = this._groupBoxFindOptions.Right + num;
    this._groupBoxFindAttributes.Height = this._groupBoxFindOptions.Height;
    this._groupBoxFindAttributes.Width = this.Width - num - this._groupBoxFindAttributes.Left;
    this._groupBoxFindAttributes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    if (this.IsExpanded)
      this._groupBoxFindAttributes.Visible = true;
    else
      this._groupBoxFindAttributes.Visible = false;
  }

  /// <summary> Показывать ли кнопку "Все атрибуты" </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool ShowButtonAllAttributes
  {
    get => this._userControlSelectAttributes.ShowButtonAllAttributes;
    set => this._userControlSelectAttributes.ShowButtonAllAttributes = value;
  }

  public override void PlaceControls() => base.PlaceControls();

  private void _btnShowMore_Click(object sender, EventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlFindReplaceTextInAttributes));
    this._groupBoxFindAttributes = new GroupBox();
    this._userControlSelectAttributes = new UserControlSelectAttributes();
    this._groupBoxFindOptions.SuspendLayout();
    this._groupBoxFindAttributes.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._groupBoxFindOptions, "_groupBoxFindOptions");
    componentResourceManager.ApplyResources((object) this._checkBoxRegularExpressions, "_checkBoxRegularExpressions");
    componentResourceManager.ApplyResources((object) this._checkBoxWholeWord, "_checkBoxWholeWord");
    componentResourceManager.ApplyResources((object) this._labelComboBoxWhereToFind, "_labelComboBoxWhereToFind");
    componentResourceManager.ApplyResources((object) this._checkBoxMathCase, "_checkBoxMathCase");
    componentResourceManager.ApplyResources((object) this._btnFindNext, "_btnFindNext");
    componentResourceManager.ApplyResources((object) this._btnClose, "_btnClose");
    componentResourceManager.ApplyResources((object) this._btnShowMore, "_btnShowMore");
    this._btnShowMore.Click += new EventHandler(this._btnShowMore_Click);
    componentResourceManager.ApplyResources((object) this._btnSelectExpression, "_btnSelectExpression");
    componentResourceManager.ApplyResources((object) this._btnReplaceAll, "_btnReplaceAll");
    componentResourceManager.ApplyResources((object) this._btnReplace, "_btnReplace");
    componentResourceManager.ApplyResources((object) this._groupBoxFindAttributes, "_groupBoxFindAttributes");
    this._groupBoxFindAttributes.Controls.Add((Control) this._userControlSelectAttributes);
    this._groupBoxFindAttributes.Name = "_groupBoxFindAttributes";
    this._groupBoxFindAttributes.TabStop = false;
    componentResourceManager.ApplyResources((object) this._userControlSelectAttributes, "_userControlSelectAttributes");
    this._userControlSelectAttributes.Name = "_userControlSelectAttributes";
    this._userControlSelectAttributes.Tag = (object) "  ";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._groupBoxFindAttributes);
    this.Name = nameof (UserControlFindReplaceTextInAttributes);
    this.PossibleSearchPlaces = new string[0];
    this.Tag = (object) "   ";
    this.Controls.SetChildIndex((Control) this._btnShowMore, 0);
    this.Controls.SetChildIndex((Control) this._btnReplace, 0);
    this.Controls.SetChildIndex((Control) this._btnReplaceAll, 0);
    this.Controls.SetChildIndex((Control) this._btnFindNext, 0);
    this.Controls.SetChildIndex((Control) this._btnSelectExpression, 0);
    this.Controls.SetChildIndex((Control) this._btnClose, 0);
    this.Controls.SetChildIndex((Control) this._groupBoxFindAttributes, 0);
    this.Controls.SetChildIndex((Control) this._groupBoxFindOptions, 0);
    this._groupBoxFindOptions.ResumeLayout(false);
    this._groupBoxFindOptions.PerformLayout();
    this._groupBoxFindAttributes.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
