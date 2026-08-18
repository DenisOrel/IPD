// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SetupOutputView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Output;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

[ViewDescriptionProvider(typeof (SetupOutputView.CustomViewDescriptionProvider))]
internal class SetupOutputView : UserControl, IView
{
  private FormSetupOutput _form;
  /// <summary>Провайдер сервисов</summary>
  private System.IServiceProvider _provider;
  /// <summary>Идентификатор выделенного объекта</summary>
  private long _selectedID;
  private int _imageIndex = -1;
  /// <summary>Порядковый номер вьюшки</summary>
  private int _viewIndex = 101;
  /// <summary>Название вьюшки</summary>
  private string _viewName = "Настройка вывода";

  public SetupOutputView()
  {
    this.InitializeComponent();
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service == null)
      return;
    Image image = DocumentMenuHelper.LoadImageFromResurces("Intermech.Document.Model.Resources.OutputSetup.png");
    if (image != null)
      this._imageIndex = service.Add(image, "imgAVSSetupOutput");
    else
      this._imageIndex = service.ImageIndex("imgSpecRow");
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._selectedID = (items.GetItemID(0) as NodeID).ObjectID;
    this._provider = provider;
  }

  public void Activate(IView previousView)
  {
    if (this._form != null)
      this._form.Parent = (Control) null;
    bool showCommonTemplate = (this._provider.GetService(typeof (IAVSTemplatesViewsService)) as IAVSTemplatesViewsService).ShowCommonTemplate;
    this.Cursor = Cursors.WaitCursor;
    Application.UseWaitCursor = true;
    Application.DoEvents();
    try
    {
      this._form = new FormSetupOutput(this._selectedID)
      {
        SettingsLevel = showCommonTemplate ? InheritanceSettingsLevel.CommonTemplate : InheritanceSettingsLevel.Template
      };
      Application.DoEvents();
      this._form.InitOutputMapping();
      this._form.SetInView();
      this._form.SetParent((Control) this);
      Application.DoEvents();
      this._form.LoadControlData();
    }
    finally
    {
      Application.UseWaitCursor = false;
      this.Cursor = Cursors.Default;
    }
  }

  private void ResizeAndShiftWindow(int newWidth)
  {
    int width = this.ParentForm.Width;
    this.ParentForm.Width = newWidth;
    this.ParentForm.Left += (width - newWidth) / 2;
  }

  public void Deactivate(IView nextView)
  {
    if (this._form != null && this._form.Changed && MessageBox.Show($"В закладке \"{this.Caption}\" остались не сохраненные данные. Сохранить?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      this._form.SaveChanges();
    this._form?.AutoCheckInAll();
  }

  public string Caption => this._viewName;

  public int ImageIndex => this._imageIndex;

  public int OrderID => this._viewIndex;

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.Name = nameof (SetupOutputView);
    this.Size = new Size(185, 218);
    this.ResumeLayout(false);
  }

  private sealed class CustomViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Настройка вывода",
        ImageIndex = -1,
        OrderID = 101
      };
    }
  }
}
