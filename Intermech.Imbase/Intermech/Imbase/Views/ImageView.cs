// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImageView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImageView : UserControl, IView
{
  public int ImageIndex => -1;

  public int OrderID => 36;

  public string Caption => LocalizationHolder.rm.GetString("Imbase.Client_95");

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
  }

  public void Deactivate(IView nextView)
  {
  }

  public void Activate(IView previousView)
  {
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImageView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ImageView);
    this.ResumeLayout(false);
  }
}
