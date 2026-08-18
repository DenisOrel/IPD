
// Type: Intermech.Navigator.Controls.DialogChildrenView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

public class DialogChildrenView : ChildrenView
{
  /// <summary>Контейнер сервисов контекстного меню закладки</summary>
  protected override IServiceContainer GetMenuServiceContainer()
  {
    return DialogChildrenView.DisableGlobalCommandProviders((object) this, base.GetMenuServiceContainer());
  }

  /// <summary>Disables the global command providers</summary>
  /// <param name="sender"></param>
  /// <param name="originalMenuServiceContainer"></param>
  /// <returns>An IServiceContainer</returns>
  public static IServiceContainer DisableGlobalCommandProviders(
    object sender,
    IServiceContainer originalMenuServiceContainer)
  {
    IServiceContainer serviceContainer1 = originalMenuServiceContainer;
    if (!(serviceContainer1 is ServiceContainer serviceContainer2))
      return serviceContainer1;
    IViewState service1 = serviceContainer2.GetService<IViewState>(false);
    IViewState service2 = service1 != null ? (IViewState) new ViewStateService(service1.ViewState | ViewStateFlags.DisableGlobalCommandProviders) : (IViewState) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.NodeInViews | ViewStateFlags.InSelectionWindow | ViewStateFlags.DisableGlobalCommandProviders);
    serviceContainer2.AddService<IViewState>(service2);
    return serviceContainer1;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DialogChildrenView));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toggleManualSortingButtonItem.Image = (Image) componentResourceManager.GetObject("_toggleManualSortingButtonItem.Image");
    this._grid.DefaultAutoGroupRow.Height = 25;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 20;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Size = new Size(1151, 160 /*0xA0*/);
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this._manualSortingSetupButtonItem.Image = (Image) componentResourceManager.GetObject("_manualSortingSetupButtonItem.Image");
    this.Name = nameof (DialogChildrenView);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._gridHeaderMenuBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._pictureBox, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._toolBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._grid, 0);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
