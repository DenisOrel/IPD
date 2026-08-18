
// Type: Intermech.Navigator.DBObjects.EventLogPropertiesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Вьюшка для редактирования правила отбора версий объектов
/// </summary>
public class EventLogPropertiesView : UserControl, IView
{
  public long EventID = -1;
  private EventLogPropertiesForm EditorForm;
  private int _imageIndex = -1;
  private bool _initmode;
  private bool _loaded;
  private System.ComponentModel.Container components;

  /// <summary>Создать экземпляр панели</summary>
  public EventLogPropertiesView()
  {
    this.InitializeComponent();
    this._initmode = false;
  }

  protected override void Dispose(bool disposing)
  {
    this.EventID = -1L;
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
    this.Name = "CalcFormulaView";
    this.Size = new Size(336, 120);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items.Count < 1)
    {
      this.EventID = -1L;
    }
    else
    {
      this.EventID = ((IEventID) items.GetItemData(0, typeof (IEventID))).Value;
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
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgDocument");
      return this._imageIndex;
    }
  }

  /// <summary>Вернуть порядковый номер вьюшки в списке всех вьюшек</summary>
  public int OrderID => 3;

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption
  {
    get
    {
      return this.EventID <= 0L ? LocalizationHolder.rm.GetString("Client.Core_609") : LocalizationHolder.rm.GetString("Client.Core_608") + this.EventID.ToString();
    }
  }

  /// <summary>
  /// Выполнить действия при активации объекта подходящего типа
  /// </summary>
  /// <param name="previousView">Предыдущая вьюшка</param>
  public void Activate(IView previousView)
  {
    if (this._initmode)
    {
      if (this.EditorForm == null)
      {
        this.EditorForm = new EventLogPropertiesForm();
        this.EditorForm.SetParent((Control) this);
        this.EditorForm.ParentMode = 0;
      }
      this._initmode = false;
    }
    if (this._loaded)
      return;
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad00039-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) this.EventID, LogicalOperators.AND, 0);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.EventLog.Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        conditionStructure
      }), true);
      if (dataTable.Rows.Count > 0)
        this.EditorForm.LoadObjectData(dataTable.Rows[0]);
    }
    this._loaded = true;
  }

  public void Deactivate(IView nextView) => this._loaded = true;
}
