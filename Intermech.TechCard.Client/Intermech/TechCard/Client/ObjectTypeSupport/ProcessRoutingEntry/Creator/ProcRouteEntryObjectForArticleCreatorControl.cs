// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.Creator.ProcRouteEntryObjectForArticleCreatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.Creator;

/// <summary>
/// Контрол отображения привязок входимости в Сборку/Заказ при создании объекта
/// </summary>
public class ProcRouteEntryObjectForArticleCreatorControl : TechObjectCreatorBaseControl
{
  /// <summary>Объект "Входимость маршрута обработки"</summary>
  private readonly ProcRouteEntryObject _procRouteEntryObject = new ProcRouteEntryObject(-1L);
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ProcRouteEntryForArticleControl procRouteEntryForArticleControl;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.LoadContextObjectData();
    this.UpdateCustomControls();
  }

  /// <summary>
  /// Загрузка информации о контексте объекта (Изделие / МО)
  /// </summary>
  private void LoadContextObjectData()
  {
  }

  /// <summary>Обновление состояний контролов</summary>
  private void UpdateCustomControls()
  {
  }

  /// <summary>Загрузка данных объекта</summary>
  protected override void DoLoadObjectData(IDBObject dbObject)
  {
    this._procRouteEntryObject.ObjectId = this.CreatedObject.ObjectID;
    this._procRouteEntryObject.LoadData(dbObject.Session);
    this.procRouteEntryForArticleControl.ProcRouteEntryObject = this._procRouteEntryObject;
    this.procRouteEntryForArticleControl.StartLoadData(dbObject.Session);
  }

  /// <summary>Сохранение данных объекта</summary>
  protected override void DoSaveObjectData(IDBObject dbObject)
  {
    this._procRouteEntryObject.SaveData(dbObject.Session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObject"></param>
  /// <returns></returns>
  protected override bool CreatedObject_DoBeforeCommitCreation(
    IUserSession session,
    IDBObject newObject)
  {
    if (!base.CreatedObject_DoBeforeCommitCreation(session, newObject))
      return false;
    ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, false);
    service?.CreateSession((object) session.SessionGUID);
    try
    {
      this.CreateObject_CopyPrototypeComposition(session);
    }
    finally
    {
      service?.DisposeSession((object) session.SessionGUID);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public ProcRouteEntryObjectForArticleCreatorControl() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public ProcRouteEntryObjectForArticleCreatorControl(
    CreatedObjectItem createdObject,
    IObjectCreatorParams creatorExtraParams)
    : base(createdObject, creatorExtraParams)
  {
    this.InitializeComponent();
    this.InitializeControlData();
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
    this.procRouteEntryForArticleControl = new ProcRouteEntryForArticleControl();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    this.procRouteEntryForArticleControl.ArticleObjectItems = (List<ObjInfoIDItem>) null;
    this.procRouteEntryForArticleControl.Dock = DockStyle.Fill;
    this.procRouteEntryForArticleControl.Location = new Point(0, 0);
    this.procRouteEntryForArticleControl.Name = "procRouteEntryForArticleControl";
    this.procRouteEntryForArticleControl.Size = new Size(676, 352);
    this.procRouteEntryForArticleControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.procRouteEntryForArticleControl);
    this.Name = nameof (ProcRouteEntryObjectForArticleCreatorControl);
    this.Size = new Size(676, 352);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }
}
