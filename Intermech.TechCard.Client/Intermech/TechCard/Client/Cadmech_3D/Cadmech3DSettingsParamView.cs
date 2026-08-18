// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DSettingsParamView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>Закладка для настроек атрибутов параметров Cadmech</summary>
internal class Cadmech3DSettingsParamView : TechCardBaseView
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IgTexWithButtonCellManager _cellManager = new IgTexWithButtonCellManager();
  /// <summary>
  /// 
  /// </summary>
  private int _panel2DefHeight;
  /// <summary>Возможность редактирования настроек параметра</summary>
  private bool _canEdit;
  /// <summary>Текущие настройки</summary>
  private IIMCadSettings _cadSettings;
  /// <summary>Сервис настроек</summary>
  private IIMCadSettingsService _cadSettingsService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer;
  private iGrid gridParams;
  private iGrid gridParamProp;
  private ContextMenuStrip cmsAttrParams;
  private ToolStripMenuItem tsmiParamEdit;
  private ToolStripMenuItem tsmiParamClear;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private IIMCadAttrTypeParamSettings GetSelectedAttrTypeSett()
  {
    return this.gridParams.CurRow?.Tag as IIMCadAttrTypeParamSettings;
  }

  /// <summary>Загрузка настроек для тек. типа</summary>
  /// <param name="cadAttrType"></param>
  private void LoadCadAttrParam(IMTextFaceAttributeType cadAttrType)
  {
    this._cadSettings = (IIMCadSettings) null;
    this._cadSettingsService?.LoadSettings(out this._cadSettings);
    IIMCadSettings cadSettings = this._cadSettings;
    this.FillGridParam(cadSettings != null ? ((IEnumerable<IIMCadAttrTypeParamSettings>) cadSettings.AttrTypeSettings.Params).Where<IIMCadAttrTypeParamSettings>((Func<IIMCadAttrTypeParamSettings, bool>) (item => item.AttrType == cadAttrType)).ToArray<IIMCadAttrTypeParamSettings>() : (IIMCadAttrTypeParamSettings[]) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrTypeParams"></param>
  private void FillGridParam(IIMCadAttrTypeParamSettings[] attrTypeParams)
  {
    this.gridParams.BeginUpdate();
    try
    {
      this.gridParams.Rows.Clear();
      if (attrTypeParams == null)
        return;
      foreach (IIMCadAttrTypeParamSettings attrTypeParam in attrTypeParams)
        this.FillGridParamRow(this.gridParams.Rows.Add(), attrTypeParam);
    }
    finally
    {
      this.gridParams.EndUpdate();
      this.FillGridParamProps(this.GetSelectedAttrTypeSett());
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="row"></param>
  /// <param name="attrTypeParam"></param>
  private void FillGridParamRow(iGRow row, IIMCadAttrTypeParamSettings attrTypeParam)
  {
    row.Cells[0].Value = (object) attrTypeParam.Name;
    row.Cells[1].Value = attrTypeParam.IpsAttrType != Guid.Empty ? (object) MetaDataHelper.GetAttributeTypeName(attrTypeParam.IpsAttrType) : (object) string.Empty;
    row.Tag = (object) attrTypeParam;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrTypeParam"></param>
  private void FillGridParamProps(IIMCadAttrTypeParamSettings attrTypeParam)
  {
    this.gridParamProp.BeginUpdate();
    try
    {
      if (attrTypeParam == null)
      {
        foreach (iGRow row in (IEnumerable) this.gridParamProp.Rows)
          row.Cells[1].Value = (object) string.Empty;
      }
      else
      {
        this.gridParamProp.Cells[0, 1].Value = (object) attrTypeParam.Name;
        this.gridParamProp.Cells[1, 1].Value = (object) attrTypeParam.Code;
        this.gridParamProp.Cells[2, 1].Value = (object) attrTypeParam.ParamType.ToString();
        this.gridParamProp.Cells[3, 1].Value = attrTypeParam.IsSystem ? (object) Intermech.Consts.YesValue : (object) Intermech.Consts.NoValue;
      }
    }
    finally
    {
      this.gridParamProp.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrTypeSett"></param>
  private void SelectIpsAttrType(IIMCadAttrTypeParamSettings attrTypeSett)
  {
    if (!this.CanModify)
      return;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      FieldTypes[] collection;
      switch (attrTypeSett.ParamType)
      {
        case IMCadFaceAttrPropType.String:
          collection = new FieldTypes[1]
          {
            FieldTypes.ftString
          };
          break;
        case IMCadFaceAttrPropType.Strings:
          collection = new FieldTypes[2]
          {
            FieldTypes.ftString,
            FieldTypes.ftShortBlob
          };
          break;
        case IMCadFaceAttrPropType.Integer:
          collection = new FieldTypes[2]
          {
            FieldTypes.ftInteger,
            FieldTypes.ftString
          };
          break;
        case IMCadFaceAttrPropType.Float:
          collection = new FieldTypes[2]
          {
            FieldTypes.ftMeasured,
            FieldTypes.ftDouble
          };
          break;
        case IMCadFaceAttrPropType.Boolean:
          collection = new FieldTypes[2]
          {
            FieldTypes.ftBoolean,
            FieldTypes.ftInteger
          };
          break;
        case IMCadFaceAttrPropType.GUID:
          collection = new FieldTypes[2]
          {
            FieldTypes.ftGuid,
            FieldTypes.ftString
          };
          break;
        default:
          collection = new FieldTypes[0];
          break;
      }
      attributesSelectDlg.AllowedAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) collection);
      attributesSelectDlg.SelectedAttributeIDOnStartup(MetaDataHelper.GetAttributeID((object) attrTypeSett.IpsAttrType));
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(TechCardConsts.ObjectTypes.SurfaceParamGUID);
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0)
        return;
      Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attributesSelectDlg.SelectedAttributesID[0]);
      if (attrTypeSett.IpsAttrType == attributeTypeGuid)
        return;
      attrTypeSett.IpsAttrType = attributeTypeGuid;
      this.Modified = true;
      this.FillGridParamRow(this.gridParams.CurRow, attrTypeSett);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrTypeSett"></param>
  private void ClearIpsAttrType(IIMCadAttrTypeParamSettings attrTypeSett)
  {
    if (!this.CanModify || attrTypeSett == null || !(attrTypeSett.IpsAttrType != Guid.Empty))
      return;
    attrTypeSett.IpsAttrType = Guid.Empty;
    this.Modified = true;
    this.FillGridParamRow(this.gridParams.CurRow, attrTypeSett);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  protected override void InitServices(System.IServiceProvider services)
  {
    base.InitServices(services);
    this._cadSettingsService = ServiceUtils.GetService<IIMCadSettingsService>((object) services, false);
  }

  /// <summary>Инициализация контролов</summary>
  protected override void InitializeCustomControls()
  {
    base.InitializeCustomControls();
    this.InitializeComponent();
    this.gridParams.BeginUpdate();
    try
    {
      this.gridParams.Cols[1].Tag = (object) "TextWithButton";
      this._cellManager.AttachTo(this.gridParams);
      this._cellManager.CellButtonClicked += new IgTexWithButtonCellManager.CellButtonClickedDelegate(this.gridParams_CellButtonClickedDelegate);
    }
    finally
    {
      this.gridParams.EndUpdate();
    }
    this.gridParamProp.BeginUpdate();
    try
    {
      this.gridParamProp.Rows.AddRange(4);
      this.gridParamProp.Cells[0, 0].Value = (object) LocalizationHolder.rm.GetString("TechCard.Client_491");
      this.gridParamProp.Cells[1, 0].Value = (object) LocalizationHolder.rm.GetString("TechCard.Client_492");
      this.gridParamProp.Cells[2, 0].Value = (object) LocalizationHolder.rm.GetString("TechCard.Client_493");
      this.gridParamProp.Cells[3, 0].Value = (object) LocalizationHolder.rm.GetString("TechCard.Client_494");
    }
    finally
    {
      this.gridParamProp.EndUpdate();
    }
  }

  /// <summary>Инициализация сообщений</summary>
  protected override void InitializeCustomMessages()
  {
    base.InitializeCustomMessages();
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_489");
    this._locMessageTxt = LocalizationHolder.rm.GetString("TechCard.Client_490");
  }

  /// <summary>Загрузить информацию в контрол</summary>
  protected override void LoadData()
  {
    this._canEdit = false;
    IMTextFaceAttributeType cadAttrType = IMTextFaceAttributeType.None;
    if (this._objID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._canEdit = sessionKeeper.Session.IsAdmin;
        IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this._objID, TechCardConsts.AttributeTypes.CadmechAttrTypeAttrGuid);
        if (objectAttributeByGuid != null)
        {
          if (objectAttributeByGuid.Value != DBNull.Value)
            cadAttrType = (IMTextFaceAttributeType) objectAttributeByGuid.AsInteger;
        }
      }
    }
    this.LoadCadAttrParam(cadAttrType);
    base.LoadData();
  }

  /// <summary>Сохранить информацию из контрола</summary>
  /// <param name="sendNotifications">Необходимость отправки уведомлений</param>
  protected override void SaveData(bool sendNotifications = true)
  {
    if (!this.Modified)
      return;
    this._cadSettingsService.SaveSettings(this._cadSettings);
    base.SaveData(sendNotifications);
  }

  /// <summary>Загрузка настроек</summary>
  protected override void LoadSettings()
  {
    base.LoadSettings();
    HybridDictionary config = new HybridDictionary(1);
    TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) config);
    if (this.gridParams != null && config.Contains((object) "gripParams_Col1_Width"))
      this.gridParams.Cols[0].Width = (int) config[(object) "gripParams_Col1_Width"];
    if (this.gridParamProp != null && config.Contains((object) "gripParamProp_Col1_Width"))
      this.gridParamProp.Cols[0].Width = (int) config[(object) "gripParamProp_Col1_Width"];
    if (!config.Contains((object) "splitContainer_Panel2_Height"))
      return;
    this._panel2DefHeight = (int) config[(object) "splitContainer_Panel2_Height"];
  }

  /// <summary>Сохранение настроек</summary>
  protected override void SaveSettings()
  {
    base.SaveSettings();
    HybridDictionary config = new HybridDictionary(1);
    if (this.gridParams != null)
      config.Add((object) "gripParams_Col1_Width", (object) this.gridParams.Cols[0].Width);
    if (this.gridParamProp != null)
      config.Add((object) "gripParamProp_Col1_Width", (object) this.gridParamProp.Cols[0].Width);
    config.Add((object) "splitContainer_Panel2_Height", (object) this.splitContainer.Panel2.Height);
    TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) config);
  }

  /// <summary>OrderID</summary>
  public override int OrderID => 0;

  /// <summary>Can modifying flag</summary>
  public override bool CanModify => base.CanModify && this._canEdit;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void gridParams_RequestEdit(object sender, iGRequestEditEventArgs e)
  {
    if (e == null || e.ColIndex != 0)
      return;
    e.DoDefault = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void gridParams_CurRowChanged(object sender, EventArgs e)
  {
    if (e == null)
      return;
    this.FillGridParamProps(this.GetSelectedAttrTypeSett());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rowIndex"></param>
  /// <param name="colIndex"></param>
  private void gridParams_CellButtonClickedDelegate(int rowIndex, int colIndex)
  {
    this.SelectIpsAttrType(this.GetSelectedAttrTypeSett());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiParamEdit_Click(object sender, EventArgs e)
  {
    this.SelectIpsAttrType(this.GetSelectedAttrTypeSett());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiParamClear_Click(object sender, EventArgs e)
  {
    this.ClearIpsAttrType(this.GetSelectedAttrTypeSett());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void splitContainer_VisibleChanged(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void splitContainer_SizeChanged(object sender, EventArgs e)
  {
    if (this._panel2DefHeight == 0 || this.splitContainer.Height <= 0)
      return;
    this.splitContainer.SplitterDistance = this.splitContainer.Height - this.splitContainer.SplitterWidth - this._panel2DefHeight;
    this.splitContainer.SizeChanged -= new EventHandler(this.splitContainer_SizeChanged);
  }

  private void cmsAttrParams_Opening(object sender, CancelEventArgs e)
  {
    this.cmsAttrParams.Enabled = this.CanModify && this.GetSelectedAttrTypeSett() != null;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    iGColPattern iGcolPattern4 = new iGColPattern();
    this.splitContainer = new SplitContainer();
    this.gridParams = new iGrid();
    this.cmsAttrParams = new ContextMenuStrip(this.components);
    this.tsmiParamEdit = new ToolStripMenuItem();
    this.tsmiParamClear = new ToolStripMenuItem();
    this.gridParamProp = new iGrid();
    this.pnButtons.SuspendLayout();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    ((ISupportInitialize) this.gridParams).BeginInit();
    this.cmsAttrParams.SuspendLayout();
    ((ISupportInitialize) this.gridParamProp).BeginInit();
    this.SuspendLayout();
    this.pnButtons.Location = new Point(2, 409);
    this.pnButtons.Padding = new Padding(0, 0, 5, 0);
    this.pnButtons.Size = new Size(459, 40);
    this.btApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btApply.Location = new Point(203, 6);
    this.btCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btCancel.Location = new Point(330, 6);
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.FixedPanel = FixedPanel.Panel2;
    this.splitContainer.Location = new Point(2, 2);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Orientation = Orientation.Horizontal;
    this.splitContainer.Panel1.Controls.Add((Control) this.gridParams);
    this.splitContainer.Panel2.Controls.Add((Control) this.gridParamProp);
    this.splitContainer.Panel2MinSize = 50;
    this.splitContainer.Size = new Size(459, 407);
    this.splitContainer.SplitterDistance = 271;
    this.splitContainer.TabIndex = 0;
    this.splitContainer.SizeChanged += new EventHandler(this.splitContainer_SizeChanged);
    this.splitContainer.VisibleChanged += new EventHandler(this.splitContainer_VisibleChanged);
    this.gridParams.AutoResizeCols = true;
    iGcolPattern1.AllowGrouping = false;
    iGcolPattern1.AllowMoving = false;
    iGcolPattern1.MinWidth = 100;
    iGcolPattern1.Text = (object) "Параметр Cadmech";
    iGcolPattern1.Width = 218;
    iGcolPattern2.AllowGrouping = false;
    iGcolPattern2.AllowMoving = false;
    iGcolPattern2.Text = (object) "Атрибут IPS";
    iGcolPattern2.Width = 237;
    this.gridParams.Cols.AddRange(new iGColPattern[2]
    {
      iGcolPattern1,
      iGcolPattern2
    });
    this.gridParams.ContextMenuStrip = this.cmsAttrParams;
    this.gridParams.DefaultRow.Height = 21;
    this.gridParams.DefaultRow.NormalCellHeight = 21;
    this.gridParams.DefaultRow.Sortable = false;
    this.gridParams.Dock = DockStyle.Fill;
    this.gridParams.Header.AllowPress = false;
    this.gridParams.Header.Height = 19;
    this.gridParams.Header.HotTrackFlags = iGHdrHotTrackFlags.None;
    this.gridParams.Location = new Point(0, 0);
    this.gridParams.Name = "gridParams";
    this.gridParams.ReadOnly = true;
    this.gridParams.RowMode = true;
    this.gridParams.RowModeHasCurCell = true;
    this.gridParams.SingleClickEdit = true;
    this.gridParams.Size = new Size(459, 271);
    this.gridParams.TabIndex = 8;
    this.gridParams.CurRowChanged += new EventHandler(this.gridParams_CurRowChanged);
    this.gridParams.RequestEdit += new iGRequestEditEventHandler(this.gridParams_RequestEdit);
    this.cmsAttrParams.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiParamEdit,
      (ToolStripItem) this.tsmiParamClear
    });
    this.cmsAttrParams.Name = "cmsAttrParams";
    this.cmsAttrParams.Size = new Size(153, 70);
    this.cmsAttrParams.Opening += new CancelEventHandler(this.cmsAttrParams_Opening);
    this.tsmiParamEdit.Name = "tsmiParamEdit";
    this.tsmiParamEdit.Size = new Size(152, 22);
    this.tsmiParamEdit.Text = "Изменить";
    this.tsmiParamEdit.Click += new EventHandler(this.tsmiParamEdit_Click);
    this.tsmiParamClear.Name = "tsmiParamClear";
    this.tsmiParamClear.Size = new Size(152, 22);
    this.tsmiParamClear.Text = "Очистить";
    this.tsmiParamClear.Click += new EventHandler(this.tsmiParamClear_Click);
    this.gridParamProp.AutoResizeCols = true;
    iGcolPattern3.AllowGrouping = false;
    iGcolPattern3.AllowMoving = false;
    iGcolPattern3.MinWidth = 100;
    iGcolPattern3.Text = (object) "Свойство";
    iGcolPattern3.Width = 137;
    iGcolPattern4.AllowGrouping = false;
    iGcolPattern4.AllowMoving = false;
    iGcolPattern4.Text = (object) "Описание";
    iGcolPattern4.Width = 318;
    this.gridParamProp.Cols.AddRange(new iGColPattern[2]
    {
      iGcolPattern3,
      iGcolPattern4
    });
    this.gridParamProp.DefaultRow.Height = 21;
    this.gridParamProp.DefaultRow.NormalCellHeight = 21;
    this.gridParamProp.DefaultRow.Sortable = false;
    this.gridParamProp.Dock = DockStyle.Fill;
    this.gridParamProp.Header.AllowPress = false;
    this.gridParamProp.Header.Height = 19;
    this.gridParamProp.Header.HotTrackFlags = iGHdrHotTrackFlags.None;
    this.gridParamProp.HScrollBar.Visibility = iGScrollBarVisibility.Hide;
    this.gridParamProp.Location = new Point(0, 0);
    this.gridParamProp.Name = "gridParamProp";
    this.gridParamProp.ReadOnly = true;
    this.gridParamProp.RowMode = true;
    this.gridParamProp.SelectionMode = iGSelectionMode.None;
    this.gridParamProp.SingleClickEdit = true;
    this.gridParamProp.Size = new Size(459, 132);
    this.gridParamProp.TabIndex = 8;
    this.gridParamProp.VScrollBar.Visibility = iGScrollBarVisibility.Hide;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer);
    this.Name = nameof (Cadmech3DSettingsParamView);
    this.Size = new Size(463, 451);
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.Controls.SetChildIndex((Control) this.splitContainer, 0);
    this.pnButtons.ResumeLayout(false);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    ((ISupportInitialize) this.gridParams).EndInit();
    this.cmsAttrParams.ResumeLayout(false);
    ((ISupportInitialize) this.gridParamProp).EndInit();
    this.ResumeLayout(false);
  }
}
