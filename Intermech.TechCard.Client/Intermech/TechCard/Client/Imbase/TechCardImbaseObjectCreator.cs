// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.TechCardImbaseObjectCreator
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// 
/// </summary>
internal class TechCardImbaseObjectCreator : IDisposable
{
  /// <summary>Ид. типа объекта</summary>
  private readonly int _objectTypeId;
  /// <summary>Модальный режим</summary>
  private bool _modalMode;
  /// <summary>
  /// 
  /// </summary>
  private bool _enabled;
  /// <summary>
  /// 
  /// </summary>
  private ISelectedItems _items;
  /// <summary>
  /// 
  /// </summary>
  private System.IServiceProvider _contextServices;
  /// <summary>
  /// 
  /// </summary>
  private ImbaseObjectCreatorForm _creatorForm;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
    this._creatorForm = new ImbaseObjectCreatorForm(new ImbaseSelectionParam(0L, (IEnumerable<int>) new int[1]
    {
      this._objectTypeId
    }));
    this._creatorForm.Services.AddService(typeof (TechCardImbaseObjectCreator), (object) this);
    this._creatorForm.GotFocus += new EventHandler(this.CreatorFormOnGotFocus);
    this._creatorForm.LostFocus += new EventHandler(this.CreatorFormOnLostFocus);
    this._creatorForm.Closed += new EventHandler(this.CreatorFormOnClosed);
    this._creatorForm._btnCancel.Click += new EventHandler(this.CreatorFormOnBtnCancelClick);
    this._creatorForm._btnApply.Click += new EventHandler(this.CreatorFormOnBtnApplyClick);
    this._creatorForm._btnApply.EnabledChanged += new EventHandler(this.CreatorFormOnBtnApplyEnabled);
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls(bool forceMode = false)
  {
    if (forceMode)
      this._creatorForm.UpdateButtons();
    this._creatorForm._btnApply.Enabled &= this._enabled;
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateOwnerObjectInfo()
  {
    IDBTypedObjectID dbTypedObjectId = this._items != null ? this._items.GetItemData<IDBTypedObjectID>(0, false) : (IDBTypedObjectID) null;
    if (dbTypedObjectId == null)
      return;
    if (dbTypedObjectId.ObjectType == this._objectTypeId)
      dbTypedObjectId = this._items.GetParentData<IDBTypedObjectID>(0, false);
    if (dbTypedObjectId == null)
      return;
    this._creatorForm.OwnerObjectId = dbTypedObjectId.ObjectID;
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeId"> Ид. типа объекта</param>
  public TechCardImbaseObjectCreator(int objectTypeId)
  {
    this._objectTypeId = objectTypeId;
    this.InitializeData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public DialogResult ShowDialog()
  {
    this._modalMode = true;
    this._creatorForm.TopMost = false;
    return this._creatorForm.ShowDialog();
  }

  /// <summary>
  /// 
  /// </summary>
  public void Show()
  {
    this._modalMode = false;
    if (this._creatorForm.WindowState == FormWindowState.Minimized)
      this._creatorForm.WindowState = FormWindowState.Normal;
    if (!this._creatorForm.Visible)
      this._creatorForm.Show((IWin32Window) ((IMainFormUpdate) ApplicationServices.Container.GetService(typeof (IMainFormUpdate)))?.MainForm);
    this._creatorForm.BringToFront();
    this._creatorForm.Focus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  public void UpdateContext(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (ServiceUtils.GetService<TechCardImbaseObjectCreator>((object) viewServices, false) != null)
      return;
    List<int> allowedObjectTypes = AddTechCardObjectCommand.GetAllowedObjectTypes(items, viewServices);
    this._items = items;
    this._contextServices = viewServices;
    this.UpdateOwnerObjectInfo();
    this.Enabled = allowedObjectTypes.Contains(this._objectTypeId);
  }

  /// <summary>
  /// 
  /// </summary>
  public bool Enabled
  {
    get => this._enabled;
    set
    {
      if (this._enabled == value)
        return;
      this._enabled = value;
      this.UpdateControls(true);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public IEnumerable<ImbaseObjectInfoItem> SelectedObjItems => this._creatorForm.SelectedObjItems;

  /// <summary>
  /// 
  /// </summary>
  public void Dispose()
  {
    if (this._creatorForm != null)
    {
      this._creatorForm.Services.RemoveService(typeof (TechCardImbaseObjectCreator));
      this._creatorForm.GotFocus -= new EventHandler(this.CreatorFormOnGotFocus);
      this._creatorForm.LostFocus -= new EventHandler(this.CreatorFormOnLostFocus);
      this._creatorForm.Closed -= new EventHandler(this.CreatorFormOnClosed);
      this._creatorForm._btnCancel.Click -= new EventHandler(this.CreatorFormOnBtnCancelClick);
      this._creatorForm._btnApply.Click -= new EventHandler(this.CreatorFormOnBtnApplyClick);
      this._creatorForm._btnApply.EnabledChanged -= new EventHandler(this.CreatorFormOnBtnApplyEnabled);
      this._creatorForm.Dispose();
      this._creatorForm = (ImbaseObjectCreatorForm) null;
    }
    if (ServiceUtils.GetService<ITechCardImbaseObjectCreatorService>((object) ApplicationServices.Container, false) is TechCardImbaseObjectCreatorService service)
      service.UnRegisterCreator(this);
    this._items = (ISelectedItems) null;
    this._contextServices = (System.IServiceProvider) null;
  }

  /// <summary>
  /// 
  /// </summary>
  public int ObjectTypeId => this._objectTypeId;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="eventArgs"></param>
  private void CreatorFormOnClosed(object sender, EventArgs eventArgs)
  {
    if (this._modalMode)
      return;
    this.Dispose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreatorFormOnLostFocus(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreatorFormOnGotFocus(object sender, EventArgs e)
  {
    if (!this._enabled)
      return;
    this.UpdateOwnerObjectInfo();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreatorFormOnBtnApplyEnabled(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreatorFormOnBtnApplyClick(object sender, EventArgs e)
  {
    if (this._modalMode)
      return;
    IEnumerable<ImbaseObjectInfoItem> selectedObjItems = this.SelectedObjItems;
    if (!selectedObjItems.Any<ImbaseObjectInfoItem>())
      return;
    bool topMost = this._creatorForm.TopMost;
    try
    {
      this._creatorForm.TopMost = false;
      this._creatorForm.Update();
      ApplicationServices.Container.RemoveService(typeof (IEnumerable<ImbaseObjectInfoItem>));
      ApplicationServices.Container.AddService(typeof (IEnumerable<ImbaseObjectInfoItem>), (object) selectedObjItems);
      AddTechCardObjectCommand cardObjectCommand = new AddTechCardObjectCommand(this.ObjectTypeId);
      cardObjectCommand.Init(this._items, this._contextServices ?? (System.IServiceProvider) ApplicationServices.Container, (object) this.ObjectTypeId);
      cardObjectCommand.Execute();
      this._creatorForm.SelectedObjItems = (IEnumerable<ImbaseObjectInfoItem>) null;
    }
    finally
    {
      this._creatorForm.TopMost = topMost;
      this._creatorForm.Update();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreatorFormOnBtnCancelClick(object sender, EventArgs e)
  {
    if (this._modalMode)
      return;
    this._creatorForm.Close();
  }
}
