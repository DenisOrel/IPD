
// Type: Intermech.Client.Core.FormSelectAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> Описание класса FormSelectAttribute </summary>
public class FormSelectAttribute : Form
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private TreeList _treeListAttributes;
  private TreeListColumn treeListColumn1;
  private Button _buttonAllAttributes;
  private ImageList imageList1;
  private ToolTipController _ReadModeToolTip;
  private int _coreObjType = -1;
  private IntList _scanRelationIDs = new IntList();
  private IntList _scanObjTypeIDs = new IntList();
  private bool _onlyGridableParams = true;
  private int _selectedAttributeID = -1;
  private IntList _selectedAttributeIDs;
  private IntList _loadedAttributeIDs = new IntList();

  public FormSelectAttribute() => this.InitializeComponent();

  /// <summary> Очистка использованых ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormSelectAttribute));
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._buttonAllAttributes = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this._treeListAttributes = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.imageList1 = new ImageList(this.components);
    this._treeListAttributes.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    componentResourceManager.ApplyResources((object) this._BtnOK, "_BtnOK");
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Name = "_BtnOK";
    componentResourceManager.ApplyResources((object) this._BtnCancel, "_BtnCancel");
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.Name = "_BtnCancel";
    componentResourceManager.ApplyResources((object) this._buttonAllAttributes, "_buttonAllAttributes");
    this._buttonAllAttributes.Name = "_buttonAllAttributes";
    this._buttonAllAttributes.Click += new EventHandler(this._buttonAllAttributes_Click);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    componentResourceManager.ApplyResources((object) this._treeListAttributes, "_treeListAttributes");
    this._treeListAttributes.BackColor = SystemColors.Window;
    this._treeListAttributes.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this._treeListAttributes.Name = "_treeListAttributes";
    this._treeListAttributes.BeginUnboundLoad();
    this._treeListAttributes.AppendNode((object) new object[1], -1, 0, 0, 4610);
    this._treeListAttributes.AppendNode((object) new object[1]
    {
      (object) "dfsgsdfg"
    }, -1, 0, 0, 4610);
    this._treeListAttributes.EndUnboundLoad();
    this._treeListAttributes.SelectImageList = this.imageList1;
    this._treeListAttributes.Styles.AddReplace("Style1", (object) new ViewStyle("Style1", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, true, false, HorzAlignment.Center, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText));
    this._treeListAttributes.Styles.AddReplace("Preview", (object) new ViewStyle("Preview", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, true, false, HorzAlignment.Near, VertAlignment.Center, (Image) null, SystemColors.GrayText, Color.Blue));
    this._treeListAttributes.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeListAttributes.Styles.AddReplace("Row", (object) new ViewStyle("Row", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Near, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this._treeListAttributes.Styles.AddReplace("SortDescend", (object) new ViewStyle("SortDescend", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "Style2", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) componentResourceManager.GetObject("_treeListAttributes.Styles"), SystemColors.Window, SystemColors.WindowText));
    this._treeListAttributes.Styles.AddReplace("SectionStyle", (object) new ViewStyle("SectionStyle", "", new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Bottom, (Image) null, Color.Gainsboro, SystemColors.WindowText));
    this._treeListAttributes.Styles.AddReplace("TreeLine", (object) new ViewStyle("TreeLine", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.Empty, SystemColors.ControlDark));
    this._treeListAttributes.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeListAttributes.Styles.AddReplace("SortAscend", (object) new ViewStyle("SortAscend", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "Style2", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) componentResourceManager.GetObject("_treeListAttributes.Styles1"), SystemColors.Window, SystemColors.WindowText));
    this._treeListAttributes.Styles.AddReplace("Style2", (object) new ViewStyle("Style2", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, true, false, HorzAlignment.Near, VertAlignment.Center, (Image) null, SystemColors.InactiveCaption, SystemColors.WindowText));
    this._treeListAttributes.Styles.AddReplace("HeaderPanel", (object) new ViewStyle("HeaderPanel", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Center, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText));
    this._treeListAttributes.TreeLineStyle = LineStyle.None;
    this._treeListAttributes.UncheckedStateIndex = 4610;
    this._treeListAttributes.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this._treeListAttributes_FocusedNodeChanged);
    this._treeListAttributes.DoubleClick += new EventHandler(this._treeListAttributes_DoubleClick);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.imageList1, "imageList1");
    this.imageList1.TransparentColor = Color.Transparent;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.Controls.Add((Control) this._buttonAllAttributes);
    this.Controls.Add((Control) this._treeListAttributes);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormSelectAttribute);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Closed += new EventHandler(this.FormSelectAttribute_Closed);
    this.Load += new EventHandler(this.FormSelectAttribute_Load);
    this._treeListAttributes.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Давать выбирать только поля, значения которых могут быть видны в Property Grid-е </summary>
  public bool OnlyGridableParams
  {
    get => this._onlyGridableParams;
    set => this._onlyGridableParams = value;
  }

  /// <summary> Давать ли возможность выбирать сразу несколько атрибутов </summary>
  public bool MultiSelect
  {
    get => (this._treeListAttributes.BehaviorOptions & BehaviorOptionsFlags.MultiSelect) != 0;
    set => this._treeListAttributes.BehaviorOptions &= BehaviorOptionsFlags.MultiSelect;
  }

  /// <summary> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </summary>
  public bool ShowAllAttributesButton
  {
    get => this._buttonAllAttributes.Visible;
    set
    {
      this._buttonAllAttributes.Visible = value;
      this._buttonAllAttributes.Enabled = value;
    }
  }

  /// <summary> Идентификатор типа объекта, от которого накладываются связи с идентификаторами, перечисленными в ScanRelationIDs </summary>
  public int CoreObjType
  {
    get => this._coreObjType;
    set => this._coreObjType = value;
  }

  /// <summary> Список идентификаторов связей, у которых необходимо зачитывать список допустимых атрибутов </summary>
  public IntList ScanRelationIDs
  {
    get => this._scanRelationIDs;
    set => this._scanRelationIDs = value;
  }

  /// <summary> Список идентификаторов типов объектов, у которых необходимо зачитывать список допустимых атрибутов </summary>
  public IntList ScanObjTypeIDs
  {
    get => this._scanObjTypeIDs;
    set => this._scanObjTypeIDs = value;
  }

  /// <summary> Идентификатор выбраного атрибута </summary>
  public int FocusedAttributeID
  {
    get
    {
      int focusedAttributeId = this._selectedAttributeID;
      if (focusedAttributeId != -1)
        return focusedAttributeId;
      try
      {
        focusedAttributeId = this._treeListAttributes.FocusedNode == null || this._treeListAttributes.FocusedNode.Tag == null ? -1 : (int) this._treeListAttributes.FocusedNode.Tag;
      }
      catch
      {
      }
      return focusedAttributeId;
    }
  }

  /// <summary> Список идентификаторов выбранных атрибутов </summary>
  public IntList SelectedAttributeIDs
  {
    get
    {
      IntList selectedAttributeIds = this._selectedAttributeIDs;
      if (this._selectedAttributeIDs != null)
        return selectedAttributeIds;
      try
      {
        if (this._treeListAttributes.Selection.Count != 0 && this.MultiSelect)
        {
          selectedAttributeIds = new IntList(this._treeListAttributes.Selection.Count);
          foreach (TreeListNode treeListNode in (CollectionBase) this._treeListAttributes.Selection)
            selectedAttributeIds.Add((object) (int) treeListNode.Tag);
        }
        else if (this.FocusedAttributeID != -1)
        {
          selectedAttributeIds = new IntList(1);
          selectedAttributeIds.Add((object) this.FocusedAttributeID);
        }
      }
      catch
      {
      }
      return selectedAttributeIds;
    }
  }

  /// <summary> Список идентификатор уже загруженных атрибутов </summary>
  protected IntList LoadedAttributeIDs => this._loadedAttributeIDs;

  /// <summary> Добавить в визульный список атрибутов атрибут </summary>
  /// <param name="attributeShortDescription"> Краткое описание атрибута (идентификатор и заголовок) </param>
  private void AddAttribute(ShortAttributeDecription attributeShortDescription)
  {
    if (attributeShortDescription == null)
      return;
    this._treeListAttributes.AppendNode((object) new object[1]
    {
      (object) attributeShortDescription.AttributeCaption
    }, (TreeListNode) null).Tag = (object) attributeShortDescription.AttributeID;
    this._loadedAttributeIDs.Add((object) attributeShortDescription.AttributeID);
  }

  /// <summary> Перечитывает список возможных атрибутов </summary>
  public void RefreshAttributesList()
  {
    this._treeListAttributes.Nodes.Clear();
    this._loadedAttributeIDs.Clear();
    if (this._coreObjType != -1 && this._scanObjTypeIDs.Count > 0 && this._scanRelationIDs.Count > 0)
    {
      for (int index = this._scanRelationIDs.Count - 1; index >= 0; --index)
      {
        if (!DBHelper.CanCreateRelationBetween(this._scanRelationIDs[index], this._coreObjType, this._scanObjTypeIDs))
          this._scanRelationIDs.RemoveAt(index);
      }
    }
    foreach (int scanRelationId in (ArrayList) this._scanRelationIDs)
    {
      foreach (ShortAttributeDecription shortDescription in (ArrayList) DBHelper.GetRelationTypeAttributeShortDescriptions(scanRelationId))
      {
        if ((!this._onlyGridableParams || DBHelper.IsAttributeGridable(shortDescription.AttributeID)) && !this._loadedAttributeIDs.Contains((object) shortDescription.AttributeID))
          this.AddAttribute(shortDescription);
      }
    }
    foreach (int scanObjTypeId in (ArrayList) this._scanObjTypeIDs)
    {
      foreach (ShortAttributeDecription shortDescription in (ArrayList) DBHelper.GetObjTypeAttributeShortDescriptions(scanObjTypeId))
      {
        if ((!this._onlyGridableParams || DBHelper.IsAttributeGridable(shortDescription.AttributeID)) && !this._loadedAttributeIDs.Contains((object) shortDescription.AttributeID))
          this.AddAttribute(shortDescription);
      }
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected void UpdateControls()
  {
    this._BtnCancel.Text = this.GetIsReadOnly() ? LocalizationHolder.rm.GetString("Client.Core_217") : LocalizationHolder.rm.GetString("Client.Core_166");
    this._BtnOK.Enabled = !this.GetIsReadOnly() && this.FocusedAttributeID != -1;
    if (this._EditModeToolTip == null)
      return;
    if (this.GetIsReadOnly())
    {
      if (!this._EditModeToolTip.Active)
        return;
      this._EditModeToolTip.Active = false;
      this._ReadModeToolTip.Active = true;
    }
    else
    {
      if (!this._ReadModeToolTip.Active)
        return;
      this._ReadModeToolTip.Active = false;
      this._EditModeToolTip.Active = true;
    }
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected bool GetIsReadOnly() => false;

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormSelectAttribute_Load(object sender, EventArgs e)
  {
    this._selectedAttributeID = -1;
    this._selectedAttributeIDs = (IntList) null;
    Application.DoEvents();
    if (this._treeListAttributes.Nodes.Count > 0)
      this._treeListAttributes.FocusedNode = this._treeListAttributes.GetNodeByVisibleIndex(0);
    this.UpdateControls();
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormSelectAttribute_Closed(object sender, EventArgs e)
  {
  }

  /// <summary> Атрибут был выбран </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _treeListAttributes_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary> Была нажата кнопка "все атрибуты" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _buttonAllAttributes_Click(object sender, EventArgs e)
  {
    bool flag;
    if (this.MultiSelect)
    {
      this._selectedAttributeID = UIHelper.SelectAttributeInTotalList();
      flag = this._selectedAttributeID != -1;
    }
    else
    {
      this._selectedAttributeIDs = UIHelper.SelectAttributesInTotalList();
      flag = this._selectedAttributeIDs != null;
    }
    if (!flag)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
  }

  /// <summary> Был двойной щелчок по дереву </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _treeListAttributes_DoubleClick(object sender, EventArgs e)
  {
    Point mousePosition = Control.MousePosition;
    if (this._treeListAttributes.GetHitInfo(this._treeListAttributes.PointToClient(Control.MousePosition)).Node == null)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
  }
}
