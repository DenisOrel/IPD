
// Type: Intermech.Redline.Redliner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Redline;
using Intermech.Client.Core.Visualizers;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Redline;

public class Redliner : IRedProperty, IDisposable
{
  internal static readonly string Developed = LocalizationHolder.rm.GetString("Client.Core.Redliner.Developed");
  internal static readonly string Made = LocalizationHolder.rm.GetString("Client.Core.Redliner.Made");
  public static readonly int RedlineAttId;
  private static readonly int RemarksStateAttrId;
  private IMapTool _currentTool;
  /// <summary>поле ФИО | Guid активного пользователя</summary>
  public static readonly string UserNameID;
  /// <summary>надо ли сохранять</summary>
  private bool _dirty;
  /// <summary>
  /// 
  /// </summary>
  private MapView _view;
  /// <summary>
  /// 
  /// </summary>
  private IMapRelative _relative;
  /// <summary>
  /// 
  /// </summary>
  private RedProperty _redProperty;
  /// <summary>
  /// 
  /// </summary>
  private MapLayerCollection _layers;
  /// <summary>Версия данных</summary>
  private int _dataVersion = 1;
  /// <summary>Имя модуля для ЭЦП</summary>
  private const string ModuleSigns = "Intermech.Signs";
  /// <summary>Заголовок плагина</summary>
  private readonly string SignsName = LocalizationHolder.rm.GetString("Signs_92");
  private Color _color;
  private MapLayer _currentRedLayer;

  public List<RedlineLayer> ListRedlineLayer()
  {
    List<RedlineLayer> redlineLayerList = new List<RedlineLayer>();
    if (this.Layers == null)
      return redlineLayerList;
    foreach (MapLayer layer in this.Layers)
    {
      if (layer.Identifier is RedlineLayer identifier)
        redlineLayerList.Add(identifier);
    }
    return redlineLayerList;
  }

  public List<RedlineLayer> ListChainRedlineLayer(ulong rlayerId)
  {
    List<RedlineLayer> redlineLayerList1 = this.ListRedlineLayer();
    List<RedlineLayer> redlineLayerList2 = new List<RedlineLayer>();
    for (int index = 0; index < redlineLayerList1.Count; ++index)
    {
      RedlineLayer redlineLayer = redlineLayerList1[index];
      if ((long) redlineLayer.RedObjectID == (long) rlayerId)
      {
        redlineLayerList2.Add(redlineLayer);
        redlineLayerList1.RemoveAt(index);
        if ((rlayerId = redlineLayer.ParentID) != 0UL)
          index = -1;
        else
          break;
      }
    }
    return redlineLayerList2;
  }

  /// <summary>получить список разрешённых граф подписей</summary>
  /// <param name="status">статус замечания</param>
  /// <returns>графы подписей ,Count == 0 - нет подписей</returns>
  public List<string> GenerateSignatures(EStatusRemark status)
  {
    List<string> signatures = new List<string>();
    foreach (RankGraphsInfo rankGraphsInfo in this.EditRedRole)
    {
      foreach (string graph in rankGraphsInfo.Graphs)
      {
        if (status != EStatusRemark.eAgreed && status != EStatusRemark.eInconsistent || !(graph == Redliner.Developed) && !(graph == Redliner.Made))
          signatures.Add($"{rankGraphsInfo.RankCaption} / {graph}");
      }
    }
    return signatures;
  }

  /// <summary>получить Графу подписи</summary>
  /// <param name="status">статус замечания</param>
  /// <returns>Имя графы подписи или пустая строка, если пользователь отказался от выбора,null - нет ролей</returns>
  public string NewSignature(EStatusRemark status)
  {
    List<string> signatures = this.GenerateSignatures(status);
    if (signatures.Count == 0)
      return (string) null;
    if (signatures.Count == 1)
      return signatures[0];
    using (UserRankGraphsForm userRankGraphsForm = new UserRankGraphsForm(signatures))
      return userRankGraphsForm.ShowDialog().Equals((object) DialogResult.OK) ? userRankGraphsForm.SelectedItem : string.Empty;
  }

  public ulong GenerateRandomNumber()
  {
    Random rnd = new Random((int) DateTime.Now.Ticks);
    List<RedlineLayer> source = this.ListRedlineLayer();
    ulong val;
    do
    {
      val = rnd.NextULong(1000UL, 9223372036854774807UL);
    }
    while (source.Any<RedlineLayer>((Func<RedlineLayer, bool>) (p => (long) p.RedObjectID == (long) val)));
    return val;
  }

  /// <summary>создание нового замечания</summary>
  /// <param name="objectId">ID объекта</param>
  /// <param name="status">тип замечания</param>
  /// <param name="setNewName">присвоить имя новому замечанию - Замечание №</param>
  /// <returns>новое замечание</returns>
  public RedlineLayer CreateRedlineLayer(long objectId, EStatusRemark status, bool setNewName = false)
  {
    RedlineLayer redlineLayer = new RedlineLayer()
    {
      StatusRemark = status,
      UserID = Redliner.UserNameID,
      RedObjectID = this.GenerateRandomNumber()
    };
    Tuple<long, int, string, string> anyActiveProcess = new RedliningWorkflowHelper().FindAnyActiveProcess(objectId);
    if (anyActiveProcess != null)
    {
      redlineLayer.NameBusiness = anyActiveProcess.Item4;
      redlineLayer.StepBusiness = anyActiveProcess.Item3;
    }
    ReportAttribute attribute = status.GetAttribute<ReportAttribute>();
    if (attribute != null)
      redlineLayer.NameRemark = attribute.Name;
    if (setNewName)
      redlineLayer.NameRemark = this.GetNewRemarkName();
    return redlineLayer;
  }

  /// <summary>Получить имя для нового замечания</summary>
  /// <returns></returns>
  private string GetNewRemarkName()
  {
    int[] array = this.ListRedlineLayer().Where<RedlineLayer>((Func<RedlineLayer, bool>) (x => x.NameRemark.StartsWith("Замечание"))).Select<RedlineLayer, int>((Func<RedlineLayer, int>) (x =>
    {
      int result;
      int.TryParse(x.NameRemark.Replace("Замечание", string.Empty), out result);
      return result;
    })).ToArray<int>();
    int num1 = ((IEnumerable<int>) array).Any<int>() ? ((IEnumerable<int>) array).Max() : 0;
    int num2;
    return $"Замечание {num2 = num1 + 1,2}";
  }

  public void AddNewObject(MapObject vobj)
  {
    vobj.Remove();
    this.CurrentRedLayer.Add(vobj);
    if (vobj is IMapToolTipText mapToolTipText)
      mapToolTipText.GenerateToolTipText();
    this.OnChanged();
  }

  /// <summary>тип работающего MapTool</summary>
  public System.Type TypeTool
  {
    get => this._redProperty.TypeTool;
    set => this._redProperty.TypeTool = value;
  }

  /// <summary>цвет заливки</summary>
  public Color BrushColor
  {
    get => (Color) this._redProperty.BrushColor;
    set => this._redProperty.BrushColor.Value = value;
  }

  /// <summary>прозрачность заливки= 0-255(0 - нет заливки)</summary>
  public int BrushAlpha
  {
    get => (int) this._redProperty.BrushAlpha;
    set => this._redProperty.BrushAlpha.Value = value;
  }

  /// <summary>цвет заливки с прозрачностью</summary>
  public Color BrushColorAlpha => this._redProperty.BrushColorAlpha;

  /// <summary>цвет кривой</summary>
  public Color PenColor
  {
    get => (Color) this._redProperty.PenColor;
    set => this._redProperty.PenColor.Value = value;
  }

  /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
  public int PenAlpha
  {
    get => (int) this._redProperty.PenAlpha;
    set => this._redProperty.PenAlpha.Value = value;
  }

  /// <summary>цвет кривой с прозрачностью</summary>
  public Color PenColorAlpha => this._redProperty.PenColorAlpha;

  /// <summary>толщина линий(мм)</summary>
  public float PenThickness
  {
    get => (float) this._redProperty.PenThickness;
    set => this._redProperty.PenThickness.Value = value;
  }

  /// <summary>толщина линий в единицах отрисовки(мм или пиксели)</summary>
  public float PenWidthInDrawingUnits
  {
    get
    {
      float widthInDrawingUnits = this._redProperty.PenThickness.Value;
      if (!this.UseUnitsConversion && (double) widthInDrawingUnits > 0.0)
        widthInDrawingUnits /= this.View.PixelsPerMM;
      return widthInDrawingUnits;
    }
  }

  /// <summary>имя фонта</summary>
  public string FontName
  {
    get => (string) this._redProperty.FontName;
    set => this._redProperty.FontName.Value = value;
  }

  /// <summary>высота текста</summary>
  public float FontSize
  {
    get => (float) this._redProperty.FontSize;
    set => this._redProperty.FontSize.Value = value;
  }

  /// <summary>цвет текста</summary>
  public Color TextColor
  {
    get => (Color) this._redProperty.TextColor;
    set => this._redProperty.TextColor.Value = value;
  }

  /// <summary>прозрачность заливки= 0-255(0 - нет заливки)</summary>
  public int TextAlpha
  {
    get => (int) this._redProperty.TextAlpha;
    set => this._redProperty.TextAlpha.Value = value;
  }

  /// <summary>цвет текста с прозрачностью</summary>
  public Color TextColorAlpha => this._redProperty.TextColorAlpha;

  /// <summary>стиль фаски</summary>
  public Intermech.Interfaces.IRedNoteStyle NoteStyle
  {
    get => (Intermech.Interfaces.IRedNoteStyle) this._redProperty.NoteStyle;
    set => this._redProperty.NoteStyle.Value = value;
  }

  /// <summary>размер фаски</summary>
  public float Facet
  {
    get => (float) this._redProperty.Facet;
    set => this._redProperty.Facet.Value = value;
  }

  /// <summary>стиль стрелки</summary>
  public Intermech.Interfaces.IRedArrowStyle NoteArrow
  {
    get => (Intermech.Interfaces.IRedArrowStyle) this._redProperty.NoteArrow;
    set => this._redProperty.NoteArrow.Value = value;
  }

  /// <summary>размер стрелки</summary>
  public float ArrowSize
  {
    get => (float) this._redProperty.ArrowSize;
    set => this._redProperty.ArrowSize.Value = value;
  }

  /// <summary>Хранит значение "Загружен ли плагин "ЭЦП"</summary>
  internal bool IsModuleSignsLoaded { get; private set; }

  /// <summary> Преобразовывать ли единицы измерения из/в миллиметры при загрузке/выгрузке данных </summary>
  public bool UseUnitsConversion { get; set; }

  /// <summary>проверка "Загружен ли плагин "ЭЦП"</summary>
  private void ChangeSigns()
  {
    this.IsModuleSignsLoaded = false;
    if (!(ServicesManager.GetService(typeof (IPluginManager)) is IPluginManager service))
      return;
    foreach (IPlugin plugin in (IEnumerable<IPlugin>) service.Plugins)
    {
      if (plugin.Name == "Intermech.Signs")
      {
        foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
        {
          if (package.Name == this.SignsName)
          {
            this.IsModuleSignsLoaded = true;
            return;
          }
        }
      }
    }
  }

  static Redliner()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID);
      Redliner.UserNameID = $"{objectInfo.Caption}|{(object) objectInfo.VersionGuid}";
    }
    MapRedNote.OnCreateControl += new MapControl.CreateControlEdit(Redliner.CreateControlEdit);
    MapRedNoteText.OnCreateFormulaImages += new MapRedNoteText.CreateFormulaImages(Redliner.CreateFormulaImages);
    Redliner.RedlineAttId = MetaDataHelper.GetAttributeTypeID("cad0036f-306c-11d8-b4e9-00304f19f545");
    Redliner.RemarksStateAttrId = MetaDataHelper.GetAttributeTypeID("cadd9abe-306c-11d8-b4e9-00304f19f545");
  }

  public static Control CreateControlEdit() => (Control) new MapAttrMemoEdit();

  public static bool CreateFormulaImages(
    Font font,
    Color textColor,
    Color backgroundColor,
    ref Dictionary<string, Image> dictImage,
    ref Dictionary<string, SizeF> dictImageSize)
  {
    return ServicesManager.GetService(typeof (IImRtfViewService)) is IImRtfViewService service && service.CreateFormulaImages(font, textColor, backgroundColor, ref dictImage, ref dictImageSize);
  }

  /// <summary>Конструктор</summary>
  /// <param name="layers"></param>
  internal Redliner(MapLayerCollection layers)
  {
    this.EditRedRole = new RankGraphsInfo[0];
    this._layers = layers ?? new MapLayerCollection();
    this.ChangeSigns();
  }

  internal MapLayerCollection Layers => this._layers;

  /// <summary>Конструктор</summary>
  /// <param name="valproperty"></param>
  private Redliner(MapLayerCollection layers, RedProperty valproperty)
    : this(layers)
  {
    this._redProperty = valproperty;
  }

  public Redliner(MapView view, IMapRelative relative, RedProperty valproperty)
    : this(view.Document.Layers, valproperty)
  {
    this._relative = relative;
    this._view = view;
  }

  public virtual void Dispose()
  {
    this._redProperty = (RedProperty) null;
    this._currentTool = (IMapTool) null;
    this.DeleteRedLayers();
    if (this._view != null)
    {
      this._view.ViewChanged -= new EventHandler(this.OnViewChanged);
      this._view.SelectionDeleted -= new EventHandler(this.OnViewChanged);
      this._view.SelectionMoved -= new EventHandler(this.OnViewChanged);
      this._view = (MapView) null;
    }
    this._layers = (MapLayerCollection) null;
    this._relative = (IMapRelative) null;
  }

  /// <summary>цвет примитивов  Redline</summary>
  public Color Color
  {
    get => this._color;
    set
    {
      bool flag = false;
      if (this._view?.Selection != null)
      {
        this._view.StartTransaction();
        foreach (MapObject mapObject in (MapCollection) this._view.Selection)
        {
          if (mapObject is MapShape mapShape)
          {
            flag = true;
            Pen pen1 = mapShape.Pen;
            Pen pen2 = new Pen(value, pen1.Width)
            {
              DashStyle = pen1.DashStyle,
              DashCap = pen1.DashCap,
              DashOffset = pen1.DashOffset
            };
            if (pen1.DashStyle == DashStyle.Custom)
              pen2.DashPattern = pen1.DashPattern;
            pen2.Alignment = pen1.Alignment;
            pen2.EndCap = pen1.EndCap;
            pen2.StartCap = pen1.StartCap;
            pen2.LineJoin = pen1.LineJoin;
            pen2.MiterLimit = pen1.MiterLimit;
            mapShape.Pen = pen2;
          }
        }
        this._view.FinishTransaction("SetColorSelection");
      }
      if (flag)
        return;
      this._color = value;
    }
  }

  public static Redliner CreateRedLiner(MapObject obj, MapView view, ref RedProperty property)
  {
    if (view == null)
      throw new ArgumentNullException(LocalizationHolder.rm.GetString("Client.Core_1005"));
    if (obj == null)
      return (Redliner) null;
    Redliner redLiner = new Redliner(view.Document.Layers, property)
    {
      _relative = obj as IMapRelative,
      _view = view
    };
    redLiner._view.ViewChanged += new EventHandler(redLiner.OnViewChanged);
    redLiner._view.SelectionDeleted += new EventHandler(redLiner.OnViewChanged);
    redLiner._view.SelectionMoved += new EventHandler(redLiner.OnViewChanged);
    redLiner.RestoreTools();
    return redLiner;
  }

  public IMapRelative Relative
  {
    [DebuggerStepThrough] get => this._relative;
  }

  /// <summary>Данные были изменены</summary>
  public event EventHandler Changed;

  public void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  /// <summary> управление кнопками Undo и Redo</summary>
  /// <param name="sender">не используется</param>
  /// <param name="e">не используется</param>
  public void OnViewChanged(object sender, EventArgs e)
  {
    this.SetDirty(true);
    this.OnChanged();
  }

  /// <summary> настроить на рисование</summary>
  public void RestoreTools()
  {
    System.Type typeTool = this.TypeTool;
    if (typeTool == (System.Type) null)
      return;
    if (typeTool == typeof (RedLineStrokeTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineStrokeTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineEllipseTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineEllipseTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineEllipseFillTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineEllipseFillTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineCircleTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineCircleTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineCircleFillTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineCircleFillTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineRectangleTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineRectangleTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineRectangleFillTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineRectangleFillTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLinePencilTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLinePencilTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (RedLineNoteTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new RedLineNoteTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else if (typeTool == typeof (DistanceTool))
    {
      this.View.Tool = this._currentTool = (IMapTool) new DistanceTool(this);
      this.TypeTool = this.View.Tool.GetType();
    }
    else
      this.TypeTool = (System.Type) null;
  }

  /// <summary>настроить на измерение отрезков</summary>
  public void Distance()
  {
    this.View.Tool = this._currentTool = (IMapTool) new DistanceTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary>настроить на рисование отрезков</summary>
  public void DrawLine()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineStrokeTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование прямоугольника</summary>
  public void DrawRectangle()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineRectangleTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование прямоугольника с заливкой</summary>
  public void DrawRectangleFill()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineRectangleFillTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование круга</summary>
  public void DrawCircle()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineCircleTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование круга с заливкой</summary>
  public void DrawCircleFill()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineCircleFillTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование эллипса</summary>
  public void DrawEllipse()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineEllipseTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование эллипса с заливкой</summary>
  public void DrawEllipseFill()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineEllipseFillTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary> настроить на рисование линии движением мыши</summary>
  public void DrawPencil()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLinePencilTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary>настроить на рисование заметки</summary>
  public void DrawNote()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLineNoteTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary>настроить на режим указателя</summary>
  public void SetPointing()
  {
    this.View.Tool = this._currentTool = (IMapTool) new RedLinePointerTool(this);
    this.TypeTool = this.View.Tool.GetType();
  }

  /// <summary>прекратить рисовать пометки</summary>
  public void CancelDraw()
  {
    if (this._currentTool == null)
      return;
    if (this._currentTool is MapTool currentTool)
      currentTool.StopTool();
    this._currentTool = (IMapTool) null;
    this.TypeTool = (System.Type) null;
  }

  /// <summary>отменить, если возможно</summary>
  public void Undo() => this._view?.Document.Undo();

  /// <summary>отменить полностью, если возможно</summary>
  public void UndoAll()
  {
    if (this._view == null)
      return;
    while (this._view.Document.CanUndo())
      this._view.Document.Undo();
    this.SetDirty(false);
    this.OnChanged();
  }

  /// <summary>вернуть, если возможно</summary>
  public void Redo()
  {
    if (this._view == null)
      return;
    this._view.Document.Redo();
    if (this._currentRedLayer == null)
      return;
    this.ChangeVisibleLayer(this._currentRedLayer.Identifier as RedlineLayer, true);
    this.OnChanged();
  }

  /// <summary>можно ли отменить</summary>
  public bool CanUndo => this._view != null && this._view.Document.CanUndo();

  /// <summary>можно ли вернуть</summary>
  public bool CanRedo => this._view != null && this._view.Document.CanRedo();

  /// <summary> Изменились ли данные в Redline?</summary>
  public bool Dirty => this._dirty || this.CanUndo;

  public void SetDirty(bool dirty) => this._dirty = dirty;

  /// <summary>видовое окно связанное с документом</summary>
  public MapView View
  {
    [DebuggerStepThrough] get => this._view;
  }

  /// <summary>слой содержащий графику замечания</summary>
  public MapLayer CurrentRedLayer
  {
    get => this._currentRedLayer;
    set
    {
      this._currentRedLayer = value;
      if (this._currentRedLayer == null)
      {
        if (this._view != null)
          this._view.Document.UndoManager = (MapUndoManager) null;
        this.CancelDraw();
      }
      else
      {
        if (!(this._currentRedLayer.Identifier is RedlineLayer identifier) || this._view == null)
          return;
        this._view.Document.UndoManager = identifier.UndoManager;
      }
    }
  }

  /// <summary> проверить сущетвование элемента в документе</summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент существует</returns>
  private bool CheckElementId(string id)
  {
    return this.Relative != null && this.Relative.CheckElementId(id);
  }

  /// <summary>найти страницу с которой начинается замечание</summary>
  /// <param name="redLayer">слой содержащий замечание</param>
  /// <returns>страница с которой начинается замечание, иначе null</returns>
  public object GetRedPage(MapLayer mapLayer)
  {
    if (mapLayer == null)
      return (object) null;
    if (!(this.Relative is IPager relative))
      return (object) null;
    try
    {
      object[] pages = relative.Pages;
      List<\u003C\u003Ef__AnonymousType2<object, int>> list = mapLayer.OfType<IMapRelativePosition>().Where<IMapRelativePosition>((Func<IMapRelativePosition, bool>) (o => o.Relative != null)).Select<IMapRelativePosition, object>((Func<IMapRelativePosition, object>) (o => o.Relative.GetPage(o.RelativeId))).Distinct<object>().Where<object>((Func<object, bool>) (x => x != null)).Select(p => new
      {
        page = p,
        index = Array.IndexOf<object>(pages, p)
      }).Where(p => p.index != -1).ToList();
      return list.Count == 0 ? (object) null : list.Aggregate((a1, a2) => a1.index >= a2.index ? a2 : a1).page;
    }
    catch (Exception ex)
    {
    }
    return (object) null;
  }

  /// <summary>найти все страницы, на которых размещены объекты замечания</summary>
  /// <param name="redLayer">слой содержащий замечание</param>
  /// <returns>список страниц с объектами замечания, иначе null</returns>
  public List<object> GetRedPagesForLayer(MapLayer mapLayer)
  {
    if (mapLayer == null)
      return (List<object>) null;
    if (!(this.Relative is IPager relative))
      return (List<object>) null;
    try
    {
      object[] pages = relative.Pages;
      return mapLayer.OfType<IMapRelativePosition>().Where<IMapRelativePosition>((Func<IMapRelativePosition, bool>) (o => o.Relative != null)).Select<IMapRelativePosition, object>((Func<IMapRelativePosition, object>) (o => o.Relative.GetPage(o.RelativeId))).Distinct<object>().Where<object>((Func<object, bool>) (x => x != null)).Select(p => new
      {
        page = p,
        index = Array.IndexOf<object>(pages, p)
      }).Where(p => p.index != -1).OrderBy(pi => pi.index).Select(a => a.page).ToList<object>();
    }
    catch (Exception ex)
    {
      return (List<object>) null;
    }
  }

  public void WriteData(long objectId, long blobId, string blobFileName, bool convertUnits = false)
  {
    string data = this.Save(convertUnits);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      IDBAttribute redAtt = dbObject.GetAttributeByID(Redliner.RedlineAttId);
      if (redAtt == null && string.IsNullOrEmpty(data))
        return;
      string str = blobId.ToString();
      if (redAtt == null)
      {
        redAtt = dbObject.Attributes.AddAttribute(Redliner.RedlineAttId, false);
        if (redAtt == null)
          return;
        if (redAtt.ValuesCount == 1)
        {
          if (redAtt.AsString == str)
            redAtt.AsString = blobFileName;
          redAtt.Index = 0;
          this.WriteString(redAtt, data, blobFileName);
          this.WriteRemarkStateAttribute(dbObject);
          return;
        }
      }
      bool flag = false;
      int valuesCount = redAtt.ValuesCount;
      for (int index = 0; index < valuesCount; ++index)
      {
        redAtt.Index = index;
        string asString = redAtt.AsString;
        if (asString == blobFileName)
        {
          flag = true;
          break;
        }
        if (asString == str)
        {
          redAtt.AsString = blobFileName;
          flag = true;
          break;
        }
      }
      if (data.Length == 0)
      {
        if (!flag)
          return;
        if (redAtt.ValuesCount > 1)
        {
          redAtt.DeleteValue();
        }
        else
        {
          redAtt.Delete(0L);
          dbObject.GetAttributeByID(Redliner.RemarksStateAttrId)?.Delete(0L);
        }
      }
      else
      {
        if (!flag)
          redAtt.AddValue((object) null);
        this.WriteString(redAtt, data, blobFileName);
        this.WriteRemarkStateAttribute(dbObject);
      }
    }
  }

  /// <summary>Добавление атрибута Статус замечений</summary>
  /// <param name="dbObject"></param>
  private void WriteRemarkStateAttribute(IDBObject dbObject)
  {
    List<RedlineLayer> list = this.Layers.OfType<MapLayer>().Select<MapLayer, object>((Func<MapLayer, object>) (x => x.Identifier)).OfType<RedlineLayer>().Where<RedlineLayer>((Func<RedlineLayer, bool>) (x => x.ParentID == 0UL)).ToList<RedlineLayer>();
    if (list.Count == 0)
      return;
    IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(Redliner.RemarksStateAttrId, false);
    if (dbAttribute == null)
      return;
    if (list.Any<RedlineLayer>((Func<RedlineLayer, bool>) (x => x.StatusRemark == EStatusRemark.eInconsistent)))
      dbAttribute.AsInteger = 0L;
    else if (list.All<RedlineLayer>((Func<RedlineLayer, bool>) (x => x.StatusRemark != EStatusRemark.eInconsistent)) && list.Any<RedlineLayer>((Func<RedlineLayer, bool>) (x => x.StatusRemark == EStatusRemark.eCorrected || x.StatusRemark == EStatusRemark.eRejected)))
    {
      dbAttribute.AsInteger = 1L;
    }
    else
    {
      if (!list.All<RedlineLayer>((Func<RedlineLayer, bool>) (x => x.StatusRemark == EStatusRemark.eAgreed)))
        return;
      dbAttribute.AsInteger = 2L;
    }
  }

  /// <summary>Сохранить данные</summary>
  /// <returns>строка данных</returns>
  public string Save(bool convertToPixels = false)
  {
    StringWriter w = new StringWriter();
    bool flag = true;
    RedlineXmlWrite redlineXmlWrite = new RedlineXmlWrite();
    if (this._view != null)
      redlineXmlWrite.PixelsPerMM = this._view.PixelsPerMM;
    redlineXmlWrite.ConvertMMtoPixels = convertToPixels;
    using (XmlTextWriter writer = new XmlTextWriter((TextWriter) w))
    {
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("ReadLines");
      writer.WriteAttributeString("name", "test");
      writer.WriteAttributeString("version", "7");
      MapLayerCollectionEnumerator enumerator = this.Layers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        MapLayer current = enumerator.Current;
        if (current.Identifier is RedlineLayer identifier)
        {
          flag = false;
          redlineXmlWrite.WriteMapLayer(current, writer);
          identifier.UndoManager.Clear();
        }
      }
      writer.WriteEndElement();
      writer.WriteEndDocument();
    }
    this.SetDirty(false);
    this.OnChanged();
    return flag ? string.Empty : w.ToString();
  }

  /// <summary>запись замечаний</summary>
  /// <param name="redAtt">атрибут замечаний</param>
  /// <param name="data">строка Xml даннных замечаний</param>
  /// <param name="note">индификатор блоба файла с пометками(вместо номера блоба имя файла)</param>
  private void WriteString(IDBAttribute redAtt, string data, string note)
  {
    using (MemoryStream inStream = new MemoryStream(data.Length * 2))
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) inStream))
        {
          streamWriter.Write(data);
          streamWriter.Flush();
          inStream.Position = 0L;
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true)?.PackStream((Stream) outStream, (Stream) inStream, 5);
          byte[] array = outStream.ToArray();
          if (!(redAtt is IBlobReader blobReader))
            return;
          BlobInformation blobInfo = blobReader.OpenBlob(-1) with
          {
            Note = note
          };
          if (redAtt is IBlobWriter blobWriter1)
            blobWriter1.OpenBlob(blobInfo, true);
          blobInfo.PackedFileSize = (long) array.Length;
          blobInfo.RealFileSize = inStream.Length;
          blobInfo.ModifyDate = DateTime.Now;
          blobInfo.ArcMethod = ArcMethods.ZLibPacked;
          if (!(redAtt is IBlobWriter blobWriter2))
            return;
          blobWriter2.OpenBlob(blobInfo, false);
          blobWriter2.WriteDataBlock(array);
        }
      }
    }
  }

  public void LoadData(long objectId, long blobId, string blobFileName)
  {
    string data = string.Empty;
    string str = blobId.ToString();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(objectId).GetAttributeByID(Redliner.RedlineAttId);
      if (attributeById != null)
      {
        int valuesCount = attributeById.ValuesCount;
        for (int index = 0; index < valuesCount; ++index)
        {
          attributeById.Index = index;
          string asString = attributeById.AsString;
          if (asString == str)
          {
            attributeById.AsString = blobFileName;
            data = this.ReadString(attributeById);
            break;
          }
          if (asString == blobFileName)
          {
            data = this.ReadString(attributeById);
            break;
          }
        }
      }
    }
    this.Load(data, this.UseUnitsConversion);
  }

  private string ReadString(IDBAttribute redAtt)
  {
    using (MemoryStream memoryStream = new MemoryStream(this.ExtractBlob(redAtt as IBlobReader)))
    {
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
        return streamReader.ReadToEnd();
    }
  }

  private byte[] ExtractBlob(IBlobReader br)
  {
    if (br == null)
      return (byte[]) null;
    BlobInformation blobInformation = br.OpenBlob(0);
    if (blobInformation.RealFileSize == 0L)
    {
      br.CloseBlob();
      return (byte[]) null;
    }
    byte[] buffer = br.ReadDataBlock();
    br.CloseBlob();
    if (buffer.Length == 0)
      return (byte[]) null;
    switch (blobInformation.ArcMethod)
    {
      case ArcMethods.NotPacked:
        return buffer;
      case ArcMethods.ZLibPacked:
        IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
        using (MemoryStream inStream = new MemoryStream(buffer))
        {
          inStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) blobInformation.RealFileSize))
          {
            service?.UnpackStream((Stream) outStream, (Stream) inStream);
            outStream.Position = 0L;
            return outStream.GetBuffer();
          }
        }
      default:
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_379"));
    }
  }

  /// <summary> Восстановить данные</summary>
  /// <param name="data">строка данных</param>
  private void Load(string data, bool convertToPixels = false)
  {
    this.DeleteRedLayers();
    if (string.IsNullOrEmpty(data))
      return;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(data);
    RedlineXmlRead redlineXmlRead = new RedlineXmlRead()
    {
      Relative = this.Relative
    };
    if (this._view != null)
      redlineXmlRead.PixelsPerMM = this._view.PixelsPerMM;
    redlineXmlRead.ConvertMMtoPixels = convertToPixels;
    XmlNode documentElement = (XmlNode) xmlDocument.DocumentElement;
    if (documentElement != null)
    {
      XmlAttribute attribute = documentElement.Attributes?["version"];
      this._dataVersion = attribute != null ? XmlConvert.ToInt32(attribute.Value) : 0;
      redlineXmlRead.Version = this._dataVersion == 1 || this._dataVersion == 7 ? this._dataVersion : throw new NotSupportedException("Неподдерживаемая версия формата данных Красного карандаша в документе. Поддерживаются версии 7 и 1 (все ранние версии).");
      foreach (XmlNode root in documentElement)
      {
        if (root.NodeType == XmlNodeType.Element)
          redlineXmlRead.LoadXmlMapLayer(root, this.Layers);
      }
    }
    List<RedlineLayer> source = this.ListRedlineLayer();
    foreach (RedlineLayer redlineLayer1 in source.Where<RedlineLayer>((Func<RedlineLayer, bool>) (p => p.RedObjectID < 1000UL)))
    {
      ulong redObjectId = redlineLayer1.RedObjectID;
      ulong randomNumber = this.GenerateRandomNumber();
      foreach (RedlineLayer redlineLayer2 in source)
      {
        if ((long) redlineLayer2.RedObjectID == (long) redObjectId)
          redlineLayer2.RedObjectID = randomNumber;
        if ((long) redlineLayer2.ParentID == (long) redObjectId)
          redlineLayer2.ParentID = randomNumber;
      }
    }
    foreach (MapLayer layer in this.Layers)
    {
      if (layer.Identifier is RedlineLayer)
      {
        foreach (MapObject mapObject in layer.GetEnumerator())
        {
          if (mapObject is IMapRelativePosition)
            this.CheckElementId((mapObject as IMapRelativePosition).RelativeId);
        }
      }
    }
  }

  /// <summary>включить видимость указанного слоя</summary>
  /// <param name="layer">слой документа</param>
  /// <param name="isView">разрешение просмотра</param>
  /// <param name="isEdit">разрешение редактирования</param>
  public void SetVisibleLayer(MapLayer layer, bool isView, bool isEdit)
  {
    layer.AllowView = isView;
    layer.AllowPrint = isView;
    layer.AllowEdit = isEdit;
    layer.AllowSelect = isEdit;
    layer.AllowMove = isEdit;
    layer.AllowCopy = isEdit;
    layer.AllowResize = isEdit;
    layer.AllowReshape = isEdit;
    layer.AllowDelete = isEdit;
    layer.AllowInsert = isEdit;
    layer.AllowLink = isEdit;
  }

  internal static void SetOnlyViewObject(MapObject obj, bool viewOlny)
  {
    obj.Editable = !viewOlny;
    obj.Selectable = !viewOlny;
  }

  public void CopyLayerDark(MapLayer layer, MapLayer newlayer)
  {
    Color baseColor = Color.FromArgb(64 /*0x40*/, 64 /*0x40*/, 64 /*0x40*/);
    Color darkGray = Color.DarkGray;
    Color color1;
    foreach (MapObject copy in layer.CopyArray())
    {
      switch (copy)
      {
        case MapRedCircle mapRedCircle3:
          MapRedCircle mapRedCircle1 = new MapRedCircle();
          color1 = mapRedCircle3.Pen.Color;
          mapRedCircle1.Pen = new Pen(Color.FromArgb((int) color1.A, baseColor), mapRedCircle3.Pen.Width)
          {
            DashStyle = mapRedCircle3.Pen.DashStyle
          };
          MapRedCircle mapRedCircle2 = mapRedCircle1;
          if (mapRedCircle3.Brush != null)
          {
            int a;
            if (!(mapRedCircle3.Brush is SolidBrush brush))
            {
              a = (int) darkGray.A;
            }
            else
            {
              color1 = brush.Color;
              a = (int) color1.A;
            }
            int alpha = a;
            mapRedCircle2.Brush = (Brush) new SolidBrush(Color.FromArgb(alpha, darkGray));
          }
          mapRedCircle2.Bounds = mapRedCircle3.Bounds;
          mapRedCircle2.Relative = mapRedCircle3.Relative;
          mapRedCircle2.RelativeId = mapRedCircle3.RelativeId;
          Redliner.SetOnlyViewObject((MapObject) mapRedCircle2, true);
          newlayer.Add((MapObject) mapRedCircle2);
          break;
        case MapRedEllipse mapRedEllipse3:
          MapRedEllipse mapRedEllipse1 = new MapRedEllipse();
          color1 = mapRedEllipse3.Pen.Color;
          mapRedEllipse1.Pen = new Pen(Color.FromArgb((int) color1.A, baseColor), mapRedEllipse3.Pen.Width)
          {
            DashStyle = mapRedEllipse3.Pen.DashStyle
          };
          MapRedEllipse mapRedEllipse2 = mapRedEllipse1;
          if (mapRedEllipse3.Brush != null)
          {
            int a;
            if (!(mapRedEllipse3.Brush is SolidBrush brush))
            {
              a = (int) darkGray.A;
            }
            else
            {
              color1 = brush.Color;
              a = (int) color1.A;
            }
            int alpha = a;
            mapRedEllipse2.Brush = (Brush) new SolidBrush(Color.FromArgb(alpha, darkGray));
          }
          mapRedEllipse2.Bounds = mapRedEllipse3.Bounds;
          mapRedEllipse2.Relative = mapRedEllipse3.Relative;
          mapRedEllipse2.RelativeId = mapRedEllipse3.RelativeId;
          Redliner.SetOnlyViewObject((MapObject) mapRedEllipse2, true);
          newlayer.Add((MapObject) mapRedEllipse2);
          break;
        case MapRedNote mapRedNote4:
          MapRedNote mapRedNote1 = new MapRedNote();
          mapRedNote1.UseMillimeters = !this.UseUnitsConversion;
          color1 = mapRedNote4.Pen.Color;
          mapRedNote1.Pen = new Pen(Color.FromArgb((int) color1.A, baseColor), mapRedNote4.Pen.Width)
          {
            DashStyle = mapRedNote4.Pen.DashStyle
          };
          MapRedNote mapRedNote2 = mapRedNote1;
          if (mapRedNote4.Brush != null)
          {
            int a;
            if (!(mapRedNote4.Brush is SolidBrush brush))
            {
              a = (int) darkGray.A;
            }
            else
            {
              color1 = brush.Color;
              a = (int) color1.A;
            }
            int alpha = a;
            mapRedNote2.Brush = (Brush) new SolidBrush(Color.FromArgb(alpha, darkGray));
          }
          MapRedNote mapRedNote3 = mapRedNote2;
          color1 = mapRedNote4.TextColor;
          Color color2 = Color.FromArgb((int) color1.A, baseColor);
          mapRedNote3.TextColor = color2;
          mapRedNote2.FontName = mapRedNote4.FontName;
          mapRedNote2.FontSize = mapRedNote4.FontSize;
          mapRedNote2.NoteStyle = mapRedNote4.NoteStyle;
          mapRedNote2.Facet = mapRedNote4.Facet;
          mapRedNote2.NoteArrow = mapRedNote4.NoteArrow;
          mapRedNote2.ArrowSize = mapRedNote4.ArrowSize;
          mapRedNote2.Text = mapRedNote4.Text;
          mapRedNote2.PlaceNote = mapRedNote4.PlaceNote;
          mapRedNote2.NoteLocation = mapRedNote4.NoteLocation;
          mapRedNote2.Relative = mapRedNote4.Relative;
          mapRedNote2.RelativeId = mapRedNote4.RelativeId;
          Redliner.SetOnlyViewObject((MapObject) mapRedNote2, true);
          newlayer.Add((MapObject) mapRedNote2);
          break;
        case MapRedPencil mapRedPencil3:
          MapRedPencil mapRedPencil1 = new MapRedPencil();
          color1 = mapRedPencil3.Pen.Color;
          mapRedPencil1.Pen = new Pen(Color.FromArgb((int) color1.A, baseColor), mapRedPencil3.Pen.Width)
          {
            DashStyle = mapRedPencil3.Pen.DashStyle
          };
          MapRedPencil mapRedPencil2 = mapRedPencil1;
          for (int i = 0; i < mapRedPencil3.PointsCount; ++i)
            mapRedPencil2.AddPoint(mapRedPencil3.GetPoint(i));
          mapRedPencil2.Relative = mapRedPencil3.Relative;
          mapRedPencil2.RelativeId = mapRedPencil3.RelativeId;
          Redliner.SetOnlyViewObject((MapObject) mapRedPencil2, true);
          newlayer.Add((MapObject) mapRedPencil2);
          break;
        case MapRedRectangle mapRedRectangle3:
          MapRedRectangle mapRedRectangle1 = new MapRedRectangle();
          color1 = mapRedRectangle3.Pen.Color;
          mapRedRectangle1.Pen = new Pen(Color.FromArgb((int) color1.A, baseColor), mapRedRectangle3.Pen.Width)
          {
            DashStyle = mapRedRectangle3.Pen.DashStyle
          };
          MapRedRectangle mapRedRectangle2 = mapRedRectangle1;
          if (mapRedRectangle3.Brush != null)
          {
            int a;
            if (!(mapRedRectangle3.Brush is SolidBrush brush))
            {
              a = (int) darkGray.A;
            }
            else
            {
              color1 = brush.Color;
              a = (int) color1.A;
            }
            int alpha = a;
            mapRedRectangle2.Brush = (Brush) new SolidBrush(Color.FromArgb(alpha, darkGray));
          }
          mapRedRectangle2.Bounds = mapRedRectangle3.Bounds;
          mapRedRectangle2.Relative = mapRedRectangle3.Relative;
          mapRedRectangle2.RelativeId = mapRedRectangle3.RelativeId;
          Redliner.SetOnlyViewObject((MapObject) mapRedRectangle2, true);
          newlayer.Add((MapObject) mapRedRectangle2);
          break;
        case MapRedStroke mapRedStroke3:
          MapRedStroke mapRedStroke1 = new MapRedStroke();
          color1 = mapRedStroke3.Pen.Color;
          mapRedStroke1.Pen = new Pen(Color.FromArgb((int) color1.A, baseColor), mapRedStroke3.Pen.Width)
          {
            DashStyle = mapRedStroke3.Pen.DashStyle
          };
          MapRedStroke mapRedStroke2 = mapRedStroke1;
          for (int i = 0; i < mapRedStroke3.PointsCount; ++i)
            mapRedStroke2.AddPoint(mapRedStroke3.GetPoint(i));
          mapRedStroke2.Relative = mapRedStroke3.Relative;
          mapRedStroke2.RelativeId = mapRedStroke3.RelativeId;
          Redliner.SetOnlyViewObject((MapObject) mapRedStroke2, true);
          newlayer.Add((MapObject) mapRedStroke2);
          break;
      }
    }
  }

  /// <summary>включить указанные RedlineLayer для просмотра</summary>
  /// <param name="redLayers">список тегов среди которых могут быть RedlineLayer</param>
  public void ChangeVisibleLayers(List<object> redLayers)
  {
    foreach (MapLayer layer in this.Layers)
    {
      if (layer.Identifier is RedlineLayer identifier)
      {
        bool isView = false;
        if (redLayers != null)
        {
          foreach (object redLayer in redLayers)
          {
            if (redLayer is RedlineLayer redlineLayer && identifier.Equals((object) redlineLayer))
              isView = true;
          }
        }
        this.SetVisibleLayer(layer, isView, false);
      }
    }
    this.CurrentRedLayer = (MapLayer) null;
  }

  /// <summary>включить указанный RedlineLayer для просмотра или редактирования</summary>
  /// <param name="redLayer">указанный RedlineLayer</param>
  /// <param name="isRedlineEdit"></param>
  public void ChangeVisibleLayer(RedlineLayer redLayer, bool isRedlineEdit)
  {
    MapLayer mapLayer = (MapLayer) null;
    foreach (MapLayer layer in this.Layers)
    {
      if (layer.Identifier is RedlineLayer identifier)
      {
        bool isView = identifier.Equals((object) redLayer);
        bool flag = identifier.UserID == Redliner.UserNameID && (redLayer.ParentID == 0UL || this.isEditRedRole);
        if (isView)
          mapLayer = layer;
        this.SetVisibleLayer(layer, isView, isView & flag & isRedlineEdit & !identifier.LockRemark);
      }
    }
    this.CurrentRedLayer = mapLayer;
  }

  /// <summary>графы подписи объекта, иначе RankGraphsInfo[0]</summary>
  public RankGraphsInfo[] EditRedRole { get; set; }

  /// <summary>Разрешено ли Пользователю редактировать пометки</summary>
  public bool isEditRedRole => this.EditRedRole.Length != 0;

  public void DeleteRedLayer(RedlineLayer redLayer)
  {
    MapLayer layer = this.Layers.Find((object) redLayer);
    if (layer == null)
      return;
    redLayer.ClearObject();
    redLayer.UndoManager.Clear();
    layer.Clear();
    layer.Identifier = (object) null;
    this.Layers.Remove(layer);
  }

  /// <summary>Удалить из документа и окна просмотра слои Ред</summary>
  public void DeleteRedLayers()
  {
    this.CurrentRedLayer = (MapLayer) null;
    foreach (RedlineLayer redLayer in this.ListRedlineLayer())
      this.DeleteRedLayer(redLayer);
  }
}
