// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTypesWeightsEditorView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Закладка "Навигатора" для редактора "весов" типов объектов - "Сортировка документов по типу"
/// (назначается для объекта "Общий шаблон спецификаций")
/// </summary>
[ViewDescriptionProvider(typeof (DocumentTypesWeightsEditorView.CustomViewDescriptionProvider))]
public class DocumentTypesWeightsEditorView : UserControl, IView
{
  protected DocumentTypesWeightsEditorForm form;
  /// <summary>Индекс изображения для закладки</summary>
  protected int imageIndex = -1;
  /// <summary>
  /// Идентификатор версии объекта, для которого отобразилась закладка
  /// </summary>
  protected long objectID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создать закладку</summary>
  public DocumentTypesWeightsEditorView() => this.InitializeComponent();

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  void IView.Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.objectID = 0L;
    if (items.Count != 1)
      return;
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    if (itemData == null || Math.Abs(itemData.ObjectID) != Math.Abs(DocumentTypeWeightHelper.objectCommonSpecificationsTemplate))
      return;
    this.objectID = itemData.ObjectID;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  void IView.Activate(IView previousView)
  {
    if (this.form == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DocumentTypeWeightHelper.InitStaticFields(sessionKeeper.Session);
        DocumentTypeWeightHelper.LoadFromObject(sessionKeeper.Session, DocumentTypeWeightHelper.objectCommonSpecificationsTemplate, DocumentTypeWeightHelper.attrDocumentTypesWeights);
      }
      this.form = new DocumentTypesWeightsEditorForm(DocumentTypeWeightHelper.items, 1);
      this.form.SetParent((Control) this);
      this.form.OnApplyPressed += new DocumentTypesWeightsChangedEventHandler(this.editor_OnApplyPressed);
      this.form.OnCancelPressed += new DocumentTypesWeightsChangedEventHandler(this.editor_OnCancelPressed);
    }
    else
      this.form.Items = DocumentTypeWeightHelper.items;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.
  /// </param>
  void IView.Deactivate(IView nextView)
  {
    if (this.form != null && this.form.IsChanged && !this.form.ReadOnly && MessageBox.Show("Сохранить изменения в коллекцию типов объектов-документов?", "Сортировка документов по типу", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        DocumentTypeWeightHelper.SaveToObject(sessionKeeper.Session, this.objectID, DocumentTypeWeightHelper.attrDocumentTypesWeights, this.form.Items);
    }
    if (this.form == null)
      return;
    this.form.Items = new DocumentTypeWeightCollection();
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  string IView.Caption
  {
    [DebuggerStepThrough] get => "Сортировка документов по типу";
  }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет инициализирована в методе Initialize.
  /// </summary>
  int IView.ImageIndex
  {
    [DebuggerStepThrough] get
    {
      if (this.imageIndex < 0)
        this.imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("");
      return this.imageIndex;
    }
  }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  int IView.OrderID
  {
    [DebuggerStepThrough] get => 15;
  }

  /// <summary>Нажата кнопка "ОК"/"Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnApplyPressed(object sender, DocumentTypesWeightsEventArgs e)
  {
    if (this.form.ReadOnly)
    {
      int num = (int) MessageBox.Show("Сохранить изменения нельзя, вероятно, объект не взят на редактирование", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DocumentTypeWeightHelper.InitStaticFields(sessionKeeper.Session);
        DocumentTypeWeightHelper.SaveToObject(sessionKeeper.Session, DocumentTypeWeightHelper.objectCommonSpecificationsTemplate, DocumentTypeWeightHelper.attrDocumentTypesWeights, e.Items);
        DocumentTypeWeightHelper.items.Assign(e.Items);
        this.form.IsChanged = false;
      }
    }
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnCancelPressed(object sender, DocumentTypesWeightsEventArgs e)
  {
    if (this.form.ReadOnly || MessageBox.Show("Отменить изменения, сделанные для типов объектов-документов?", "Отмена изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DocumentTypeWeightHelper.InitStaticFields(sessionKeeper.Session);
      DocumentTypeWeightHelper.LoadFromObject(sessionKeeper.Session, DocumentTypeWeightHelper.objectCommonSpecificationsTemplate, DocumentTypeWeightHelper.attrDocumentTypesWeights);
    }
    this.form.Init(DocumentTypeWeightHelper.items, this.form.ParentMode);
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
    this.SuspendLayout();
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.MinimumSize = new Size(200, 100);
    this.Name = nameof (DocumentTypesWeightsEditorView);
    this.Size = new Size(200, 100);
    this.ResumeLayout(false);
  }

  private sealed class CustomViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Приоритеты типов документов",
        ImageIndex = -1,
        OrderID = 15
      };
    }
  }
}
