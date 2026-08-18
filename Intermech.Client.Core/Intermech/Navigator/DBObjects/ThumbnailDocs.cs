
// Type: Intermech.Navigator.DBObjects.ThumbnailDocs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.Thumbnail;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.ObjectListFilters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

[ViewDescriptionProvider(typeof (ThumbnailDocs.ThumbnailDocsViewDescriptionProvider))]
public class ThumbnailDocs : Intermech.Client.Core.Thumbnail.ThumbnailView, IViewData, IObjectListFiltration
{
  private INotificationService _notificationService;
  private NotificationEventHandler _notifyHandler;
  private INavGraphicsCache _navGraphicsCache;
  /// <summary>
  /// Кэш со списком фильтров - чтобы не получать его каждый раз, когда юзер кликает по дереву навигатора на что-то, что содержит закладку Превью
  /// (когда приделают кнопку Обновить, тогда надо будет сбрасывать этот список)
  /// </summary>
  internal static List<MyElement> _Filters;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public bool needRefresh { get; set; }

  public ThumbnailDocs()
  {
    this._services.AddService(typeof (IObjectListFiltration), (object) this);
  }

  protected override ContentType ContentType => ContentType.NonFolders;

  public override string Caption => "Превью";

  public override int OrderID => 36;

  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
    this._tsSeparator1.Visible = true;
    this._tsFltLabel.Visible = true;
    this._tsFltComboBox.Visible = true;
    this._tsSeparator2.Visible = true;
    List<MyElement> filters = ThumbnailDocs.GetFilters();
    this._tsFltComboBox.BeginUpdate();
    try
    {
      this._tsFltComboBox.Items.Clear();
      this._tsFltComboBox.Items.AddRange((object[]) filters.ToArray());
    }
    finally
    {
      this._tsFltComboBox.EndUpdate();
    }
    this._tsFltComboBox.SelectedIndexChanged -= new EventHandler(this.FiltersComboBox_SelectedIndexChanged);
    this._tsFltComboBox.SelectedItem = (object) this._tsFltComboBox.Items.Cast<MyElement>().FirstOrDefault<MyElement>((System.Func<MyElement, bool>) (o => object.Equals(o.Tag, UISettings.SelectedChildrenViewObjectFilter.HasValue ? (object) UISettings.SelectedChildrenViewObjectFilter.Value : (object) ObjectListFilter.DefaultFilter.Guid)));
    this._tsFltComboBox.SelectedIndexChanged += new EventHandler(this.FiltersComboBox_SelectedIndexChanged);
    this._tsFltComboBox.ComboBox.DrawItem += new DrawItemEventHandler(this.FiltersComboBox_DrawItem);
    this._tsFltComboBox.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.GlobalIndexSearchValue = new GlobalIndexSearchValue(string.Empty, GlobalIndexSearchOptions.SubstringSearch, new List<string>());
  }

  internal static List<MyElement> GetFilters()
  {
    if (ThumbnailDocs._Filters != null)
      return new List<MyElement>((IEnumerable<MyElement>) ThumbnailDocs._Filters);
    List<MyElement> source = new List<MyElement>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams1 = new DBRecordSetParams();
      dbRecordSetParams1.Columns = new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION,
        (object) ObligatoryObjectAttributes.F_GUID
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams1).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) Intermech.Navigator.Selections.Consts.KindSelectionAttrID,
          RelationalOperator = RelationalOperators.Equal,
          Value = (object) 6,
          SQL = string.Empty
        }
      };
      dbRecordSetParams1.RecordCount = -1;
      DBRecordSetParams dbRecordSetParams2 = dbRecordSetParams1;
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(Intermech.Navigator.Selections.Consts.SelectionTypeID, dbRecordSetParams2);
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          MyElement myElement = new MyElement((object) DataSetProcessor.GetInt64Value(row, 0, 0L), DataSetProcessor.GetStringValue(row, 1, string.Empty), (object) DataSetProcessor.GetGuidValue(row, 2, Guid.Empty));
          source.Add(myElement);
        }
      }
      source = source.OrderBy<MyElement, string>((System.Func<MyElement, string>) (o => o.Caption)).ToList<MyElement>();
    }
    MyElement myElement1 = new MyElement((object) 0L, "Все объекты", (object) Guid.Empty);
    source.Insert(0, myElement1);
    ThumbnailDocs._Filters = source;
    return source;
  }

  private void FiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._tsFltComboBox.SelectedItem is MyElement selectedItem && selectedItem.Tag is Guid)
      UISettings.SelectedChildrenViewObjectFilter = new Guid?((Guid) selectedItem.Tag);
    ((IViewData) this).Refresh();
  }

  private void FiltersComboBox_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (this._navGraphicsCache == null)
      this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    ThumbnailDocs.FilterDrawItem(this._tsFltComboBox.ComboBox, e, this._navGraphicsCache);
  }

  internal static void FilterDrawItem(
    ComboBox ComboBox,
    DrawItemEventArgs e,
    INavGraphicsCache _navGraphicsCache)
  {
    MyElement myElement = e.Index >= 0 ? ComboBox.Items[e.Index] as MyElement : (MyElement) null;
    if (myElement != null)
      Convert.ToInt64(myElement.Value);
    Guid aGUID = myElement != null ? (Guid) myElement.Tag : Guid.Empty;
    Brush brush1 = (Brush) null;
    Brush brush2 = (Brush) null;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
    {
      brush1 = _navGraphicsCache.GetNavGradientBrush(_navGraphicsCache.CurrentColorsScheme.ComboBoxBkStartColor, _navGraphicsCache.CurrentColorsScheme.ComboBoxBkEndColor, _navGraphicsCache.CurrentColorsScheme.ComboBoxGradientMode, e.Bounds).Brush;
      brush2 = SystemBrushes.HighlightText;
    }
    if (brush1 == null)
    {
      brush1 = SystemBrushes.Window;
      brush2 = aGUID == Guid.Empty || SystemGUIDs.IsSystemGUID(aGUID) ? Brushes.DarkBlue : SystemBrushes.WindowText;
    }
    e.Graphics.FillRectangle(brush1, e.Bounds);
    if (myElement != null)
      e.Graphics.DrawString(myElement.ToString(), e.Font, brush2, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    if (!ComboBox.Focused)
      return;
    e.DrawFocusRectangle();
  }

  public override void Activate(IView previousView)
  {
    base.Activate(previousView);
    if (!this.needRefresh)
      return;
    ((IViewData) this).Refresh();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imageIndex"></param>
  /// <returns>Image object or null</returns>
  protected override object OnGetImage(int imageIndex)
  {
    ThumbnailItem thumbnailItem = this._items[imageIndex];
    object image1 = thumbnailItem.Image;
    if (image1 == null)
    {
      ThumbnailDocItem thumbnailDocItem = thumbnailItem as ThumbnailDocItem;
      if (thumbnailDocItem.Preview == DBNull.Value)
      {
        image1 = (object) "Нет рисунка";
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long objectId = thumbnailDocItem.ObjectId;
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
          if (dbObject != null)
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(SystemGUIDs.attributePreview, false);
            if (attributeByGuid != null)
            {
              IBlobReader blobReader = attributeByGuid as IBlobReader;
              BlobInformation blobInformation = blobReader.OpenBlob(8192 /*0x2000*/);
              using (MemoryStream memoryStream = new MemoryStream())
              {
                while (memoryStream.Length < blobInformation.PackedFileSize)
                {
                  byte[] buffer = blobReader.ReadDataBlock();
                  memoryStream.Write(buffer, 0, buffer.Length);
                }
                memoryStream.Position = 0L;
                try
                {
                  Image image2 = Image.FromStream((Stream) memoryStream);
                  thumbnailDocItem.Image = (object) image2;
                  image1 = (object) image2;
                }
                catch
                {
                  image1 = (object) "Нет рисунка";
                }
              }
            }
          }
        }
      }
    }
    return image1;
  }

  private object ExtractPreviewImage(long objID) => (object) "загрузка";

  protected override ContextMenuBarItem GetContextMenu(ThumbnailItem item)
  {
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInViews));
    return Intermech.Navigator.ContextMenu.Services.GetMenu((ISelectedItems) new NodeItems(this._path, this._node, new NodeIDCollection()
    {
      item.NodeID
    }, (System.IServiceProvider) serviceContainer), (System.IServiceProvider) serviceContainer) as ContextMenuBarItem;
  }

  protected override void ApplyColumns(INodeQuery query)
  {
    if (Intermech.Client.Core.Thumbnail.ThumbnailView._columns.FindByAttrID(SystemGUIDs.attributePreview).Length == 0)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(SystemGUIDs.attributePreview);
      NodeColumn nodeColumn = new NodeColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attributeType.AttributeID, typeof (long), attributeType.RealFieldType, attributeType.Name, attributeType.ShortName, attributeType.Name);
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns.Add(nodeColumn);
    }
    if (this._tsBtnAlphabet.Checked)
    {
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortIndex = 0;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortIndex = -1;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortOrder = NodeColumnSortOrder.None;
      if ((Intermech.Client.Core.Thumbnail.ThumbnailView.SortMethods) this._tsBtnAlphabet.Tag == Intermech.Client.Core.Thumbnail.ThumbnailView.SortMethods.NameAsc)
        Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortOrder = NodeColumnSortOrder.Ascending;
      else
        Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortOrder = NodeColumnSortOrder.Descending;
    }
    else if (this._tsBtnNumber.Checked)
    {
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortIndex = -1;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortOrder = NodeColumnSortOrder.None;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortIndex = 0;
      if ((Intermech.Client.Core.Thumbnail.ThumbnailView.SortMethods) this._tsBtnNumber.Tag == Intermech.Client.Core.Thumbnail.ThumbnailView.SortMethods.NumAsc)
        Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortOrder = NodeColumnSortOrder.Ascending;
      else
        Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortOrder = NodeColumnSortOrder.Descending;
    }
    else
    {
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortIndex = -1;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[0].SortOrder = NodeColumnSortOrder.None;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortIndex = -1;
      Intermech.Client.Core.Thumbnail.ThumbnailView._columns[1].SortOrder = NodeColumnSortOrder.None;
    }
    base.ApplyColumns(query);
  }

  protected override ThumbnailItem CreateThumbnailItem(INodeID nodeID, object[] record)
  {
    return (ThumbnailItem) new ThumbnailDocItem(nodeID, Convert.ToString(record[0]), Convert.ToInt64(record[1]), Convert.ToInt32(record[2]), record[3]);
  }

  protected override int SearchItem(int startIndex, int endIndex, string text)
  {
    if (this._items.Count == 0 || startIndex < 0 || endIndex < 0 || endIndex < startIndex)
      return -1;
    for (int index = startIndex; index < endIndex; ++index)
    {
      if (this._items[index].Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)
        return index;
    }
    return -1;
  }

  void IViewData.Refresh()
  {
    this._items = (List<ThumbnailItem>) null;
    this._readedCount = 0;
    this._bookmark = (object) null;
    this.GetDataPacket();
    this.needRefresh = false;
  }

  public bool FilterByCurrentVersionsRule => false;

  public bool IsGlobalIndexSearchActived => false;

  public Guid SelectedFilterGuid
  {
    get
    {
      return !(this._tsFltComboBox.SelectedItem is MyElement selectedItem) ? Guid.Empty : (Guid) selectedItem.Tag;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public GlobalIndexSearchValue GlobalIndexSearchValue { get; private set; }

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
    this._thumbnails.Size = new Size(631, 409);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ThumbnailDocs);
    this.Size = new Size(631, 476);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class ThumbnailDocsViewDescriptionProvider : 
    Intermech.Client.Core.Thumbnail.ThumbnailView.ThumbnailViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = base.DoGetViewDescription(selectedItems, serviceProvider);
      viewDescription.Caption = "Превью";
      viewDescription.OrderID = 36;
      return viewDescription;
    }
  }
}
