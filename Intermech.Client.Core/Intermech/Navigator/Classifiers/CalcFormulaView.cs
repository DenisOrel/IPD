
// Type: Intermech.Navigator.Classifiers.CalcFormulaView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.Classifiers;

/// <summary>
/// Вьюшка для редактирования правила отбора версий объектов
/// </summary>
[ViewDescriptionProvider(typeof (CalcFormulaView.CalcFormulaViewDescriptionProvider))]
public class CalcFormulaView : UserControl, IView
{
  public long ObjectID = -1;
  public long ObjectType = -1;
  public string ObjectName = "";
  private CalcFormulaForm EditorForm;
  private int _imageIndex = -1;
  private bool _initmode;
  private bool _loaded;
  private long _parentID = -1;
  private System.ComponentModel.Container components;

  /// <summary>Создать экземпляр панели</summary>
  public CalcFormulaView()
  {
    this.InitializeComponent();
    this._initmode = false;
  }

  /// <summary>Убрать свой хлам из системы</summary>
  protected override void Dispose(bool disposing)
  {
    this.ObjectID = -1L;
    this.ObjectType = -1L;
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void Initialize(long objID, int objType, long relID, System.IServiceProvider services)
  {
    this.ObjectID = objID;
    this.ObjectType = (long) objType;
    this.ObjectName = "";
    this._initmode = true;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CalcFormulaView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (CalcFormulaView);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items.Count < 1)
    {
      this.ObjectID = -1L;
    }
    else
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
      this.ObjectID = itemData.ObjectID;
      this.ObjectType = (long) itemData.ObjectType;
      IDBTypedObjectID parentData = (IDBTypedObjectID) items.GetParentData(0, typeof (IDBTypedObjectID));
      this._parentID = parentData == null || parentData.ObjectID == 0L ? -1L : parentData.ObjectID;
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
  public int OrderID => 11;

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption => CalcFormulaView.CalcFormulaViewConsts.TabCaption;

  /// <summary>
  /// Выполнить действия при активации объекта подходящего типа
  /// </summary>
  /// <param name="previousView">Предыдущая вьюшка</param>
  public void Activate(IView previousView)
  {
    if (this._initmode)
    {
      this.ObjectName = "";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.ObjectName = sessionKeeper.Session.GetObject(this.ObjectID).Caption;
      if (this.EditorForm == null)
      {
        this.EditorForm = new CalcFormulaForm();
        this.EditorForm.SetParent((Control) this);
        this.EditorForm.ParentMode = 2;
      }
      this._initmode = false;
    }
    if (this._loaded)
      return;
    this.EditorForm._dataSource = (DataTable) null;
    this.EditorForm.parentClassifier = this._parentID != -1L ? new ClassifierCalcFormula(this._parentID) : (ClassifierCalcFormula) null;
    this.EditorForm.CurrentClassifier = new ClassifierCalcFormula(this.ObjectID);
    this.EditorForm.LoadObjectData();
    this._loaded = true;
  }

  public void Deactivate(IView nextView)
  {
    if (this.EditorForm == null || !this.EditorForm.IsChanged)
      return;
    if (MessageBox.Show($"{CalcFormulaView.CalcFormulaViewConsts.ViewDialog1} \"{this.ObjectName}\"?", CalcFormulaView.CalcFormulaViewConsts.ViewDialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
      this.EditorForm.SaveObjectData();
    else
      this._loaded = false;
  }

  /// <summary>
  /// Свалка констант для вьюшки-редактора правил отбора версий
  /// </summary>
  public abstract class CalcFormulaViewConsts
  {
    public static readonly string TabCaption = LocalizationHolder.rm.GetString("Client.Core_264");
    public static readonly string ViewDialog1 = LocalizationHolder.rm.GetString("Client.Core_1209");
    public static readonly string ViewDialog2 = LocalizationHolder.rm.GetString("Client.Core_1210");
  }

  private sealed class CalcFormulaViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = CalcFormulaView.CalcFormulaViewConsts.TabCaption,
        ImageIndex = Holder.NamedImageList.ImageIndex("imgVersionRuleEditor"),
        OrderID = 11
      };
    }
  }
}
