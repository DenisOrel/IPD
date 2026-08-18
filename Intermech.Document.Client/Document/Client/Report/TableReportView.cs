// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.TableReportView
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Вьюшка редактора табличных отчетов</summary>
[ViewDescriptionProvider(typeof (TableReportView.TableReportViewDescriptionProvider))]
internal class TableReportView : UserControl, IView
{
  internal long ObjectID;
  /// <summary>Режим инициализации формы на вьюшке</summary>
  private bool _initmode;
  /// <summary>Номер значка в общей коллекции именованных рисунков</summary>
  private int _imageIndex;
  /// <summary>Загружен ли объект</summary>
  private bool _loaded;
  /// <summary>Форма для панели "Редактор опций"</summary>
  private TableReportEditor form;

  public TableReportView()
  {
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    this._imageIndex = service != null ? service.ImageIndex("imgTableReportEdit") : -1;
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.ObjectID = ((IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID))).ObjectID;
    this._initmode = true;
    this._loaded = false;
  }

  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    if (this._initmode)
    {
      if (this.form == null)
      {
        this.form = new TableReportEditor();
        this.form.SetParent((Control) this);
        this.form.ParentMode = 2;
      }
      this._initmode = false;
    }
    if (this._loaded)
      return;
    this.form.LoadObjectData(this.ObjectID);
    this._loaded = true;
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("Document.Client_44");

  public int ImageIndex => this._imageIndex;

  public int OrderID => 5;

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.Name = nameof (TableReportView);
    this.ResumeLayout(false);
  }

  private sealed class TableReportViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Document.Client_44"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgTableReportEdit") : -1,
        OrderID = 5
      };
    }
  }
}
