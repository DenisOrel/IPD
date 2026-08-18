
// Type: Intermech.Navigator.DBObjects.VersionRulesEditorView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Вьюшка для редактирования правила отбора версий объектов
/// </summary>
[ViewDescriptionProvider(typeof (VersionRulesEditorView.VersionRulesEditorViewDescriptionProvider))]
public class VersionRulesEditorView : UserControl, IView
{
  public long ObjectID;
  public long ObjectType;
  public string ObjectName = "";
  /// <summary>
  /// Ссылка на интерфейс IFiltrationClass окна-владельца, для того, чтобы получать настройки фильтрации состава
  /// </summary>
  public IFiltrationClass FiltrationClass;
  private VersionRulesEditorForm EditorForm;
  private int _imageIndex = -1;
  private bool _initmode;
  private bool _loaded;
  private System.ComponentModel.Container components;

  /// <summary>Создать экземпляр панели</summary>
  public VersionRulesEditorView()
  {
    this.InitializeComponent();
    this._initmode = false;
  }

  /// <summary>Убрать свой хлам из системы</summary>
  protected override void Dispose(bool disposing)
  {
    this.ObjectID = 0L;
    this.ObjectType = 0L;
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void Initialize(long objID, int objType, long relID, System.IServiceProvider services)
  {
    this.ObjectID = objID;
    this.ObjectType = (long) objType;
    this.ObjectName = "";
    this.FiltrationClass = (IFiltrationClass) services.GetService(typeof (IFiltrationClass));
    this._initmode = true;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionRulesEditorView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (VersionRulesEditorView);
    this.ResumeLayout(false);
  }

  /// <summary>Инициализировать вьюшку</summary>
  /// <param name="items">Список выделенных объектов</param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.FiltrationClass = (IFiltrationClass) provider.GetService(typeof (IFiltrationClass));
    if (items.Count < 1)
    {
      this.ObjectID = 0L;
    }
    else
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
      this.ObjectID = itemData.ObjectID;
      this.ObjectType = (long) itemData.ObjectType;
      this._initmode = true;
      this._loaded = false;
    }
  }

  /// <summary>
  /// Вернуть номер изображения вьюшки из глобального списка
  /// </summary>
  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgVersionRuleEditor");
      return this._imageIndex;
    }
  }

  /// <summary>Вернуть порядковый номер вьюшки в списке всех вьюшек</summary>
  public int OrderID => 5;

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption => VersionRulesEditorView.RulesEditorViewConsts.MenuCaption;

  /// <summary>
  /// Выполнить действия при активации объекта подходящего типа
  /// </summary>
  /// <param name="previousView">Предыдущая вьюшка</param>
  public void Activate(IView previousView)
  {
    if (this.EditorForm == null)
    {
      this.EditorForm = new VersionRulesEditorForm();
      this.EditorForm.FiltrationClass = this.FiltrationClass;
      this.EditorForm.SetParent((Control) this);
      this.EditorForm.ParentMode = 2;
    }
    if (this._initmode || this.EditorForm != null && this.EditorForm.IsChanged)
    {
      this.ObjectName = "";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.ObjectName = sessionKeeper.Session.GetObject(this.ObjectID).Caption;
      this._initmode = false;
      this._loaded = false;
    }
    if (this._loaded)
      return;
    this.EditorForm.FiltrationClass = this.FiltrationClass;
    this.EditorForm.RuleObjectID = this.ObjectID;
    this.EditorForm.RuleObjectName = this.ObjectName;
    this.EditorForm.LoadObjectData(0);
    this._loaded = true;
  }

  /// <summary>Переход на другой объект, деактивация вьюшки</summary>
  /// <param name="nextView">Следующая вьюшка</param>
  public void Deactivate(IView nextView)
  {
    if (this.EditorForm == null || !this.EditorForm.IsChanged || MessageBox.Show($"{VersionRulesEditorView.RulesEditorViewConsts.ViewDialog1}\"{this.ObjectName}\"?", VersionRulesEditorView.RulesEditorViewConsts.ViewDialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    this.EditorForm.SaveObjectData();
  }

  /// <summary>
  /// Свалка констант для вьюшки-редактора правил отбора версий
  /// </summary>
  public abstract class RulesEditorViewConsts
  {
    public static readonly string MenuCaption = LocalizationHolder.rm.GetString("Client.Core_735");
    public static readonly string ViewDialog1 = LocalizationHolder.rm.GetString("Client.Core_265");
    public static readonly string ViewDialog2 = LocalizationHolder.rm.GetString("Client.Core_266");
  }

  private sealed class VersionRulesEditorViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = VersionRulesEditorView.RulesEditorViewConsts.MenuCaption,
        ImageIndex = Holder.NamedImageList.ImageIndex("imgVersionRuleEditor"),
        OrderID = 5
      };
    }
  }
}
