
// Type: Intermech.Navigator.DBObjects.VersionRulesView
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

/// <summary>Вьюшка фильтрации состава</summary>
public class VersionRulesView : UserControl, IView
{
  public long ObjectID;
  public long ObjectType;
  public string ObjectName = "";
  /// <summary>
  /// Ссылка на интерфейс IFiltrationClass окна-владельца, для того, чтобы получать настройки фильтрации состава
  /// </summary>
  public IFiltrationClass FiltrationClass;
  /// <summary>Форма для панели "Фильтрация состава"</summary>
  private VersionRulesViewForm VerForm;
  /// <summary>Режим инициализации формы на вьюшке</summary>
  private bool _initmode;
  /// <summary>Номер значка в общей коллекции именованных рисунков</summary>
  private int _imageIndex;
  /// <summary>Загружен ли объект</summary>
  private bool _loaded;
  private System.ComponentModel.Container components;

  /// <summary>Создать экземпляр панели</summary>
  public VersionRulesView()
  {
    this.InitializeComponent();
    this._imageIndex = -1;
    this._initmode = false;
  }

  /// <summary>Убрать свой хлам из системы</summary>
  protected override void Dispose(bool disposing)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionRulesView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (VersionRulesView);
    this.ResumeLayout(false);
  }

  /// <summary>Инициализировать вьюшку</summary>
  /// <param name="items">Список выделенных объектов</param>
  /// <param name="services"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this.FiltrationClass = (IFiltrationClass) services.GetService(typeof (IFiltrationClass));
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
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgVersionRule");
      return this._imageIndex;
    }
  }

  /// <summary>Вернуть порядковый номер вьюшки в списке всех вьюшек</summary>
  public int OrderID => 15;

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption => VersionRulesView.VersionRulesViewConsts.MenuCaption;

  /// <summary>
  /// Выполнить действия при активации объекта в "Навигаторе"
  /// </summary>
  /// <param name="previousView">Предыдущая вьюшка</param>
  public void Activate(IView previousView)
  {
    if (this._initmode)
    {
      this.ObjectName = "";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.ObjectName = sessionKeeper.Session.GetObject(this.ObjectID).Caption;
      if (this.VerForm == null)
      {
        this.VerForm = new VersionRulesViewForm();
        this.VerForm.SetParent((Control) this);
        this.VerForm.ParentMode = 2;
        this.VerForm.FiltrationClass = this.FiltrationClass;
      }
      this._initmode = false;
    }
    if (this._loaded)
      return;
    this.VerForm.FiltrationClass = this.FiltrationClass;
    this.VerForm.LoadFilterData(0);
    this._loaded = true;
  }

  /// <summary>Переход на другой объект, деактивация вьюшки</summary>
  /// <param name="nextView">Следующая вьюшка</param>
  public void Deactivate(IView nextView)
  {
  }

  /// <summary>Свалка констант для вьюшки фильтрации состава</summary>
  public abstract class VersionRulesViewConsts
  {
    public static readonly string MenuCaption = LocalizationHolder.rm.GetString("Client.Core_833");
    public static readonly string ViewDialog1 = LocalizationHolder.rm.GetString("Client.Core_834");
    public static readonly string ViewDialog2 = LocalizationHolder.rm.GetString("Client.Core_833");
  }
}
