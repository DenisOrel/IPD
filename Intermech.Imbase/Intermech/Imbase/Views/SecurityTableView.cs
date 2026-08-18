// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.SecurityTableView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class SecurityTableView : SecurityView
{
  private IContainer components;

  public SecurityTableView() => this.InitializeComponent();

  public override void InitData(ISelectedItems items)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          this._objIDlist.Add((object) TableLoadHelper.GetTableReference(sessionKeeper.Session, itemData.ObjectID));
          this._objTypeIDlist.Add(Intermech.Imbase.Consts.ImbaseTableTypeID);
        }
      }
    }
  }

  public override string Caption => LocalizationHolder.rm.GetString("SecurityTable");

  public override int OrderID => 61;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
