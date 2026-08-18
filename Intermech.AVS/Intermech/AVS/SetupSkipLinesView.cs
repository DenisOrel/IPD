// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SetupSkipLinesView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
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

[ViewDescriptionProvider(typeof (SetupSkipLinesView.CustomViewDescriptionProvider))]
internal class SetupSkipLinesView : UserControl, IView
{
  private FormSetupSkipLines _form;
  /// <summary>Провайдер сервисов</summary>
  private System.IServiceProvider _provider;
  /// <summary>Идентификатор выделенного объекта</summary>
  private long _selectedID;
  private int _selectedObjectTypeID = -1;
  /// <summary>Идентификатор шаблона документа, если есть документ</summary>
  private long _documentTemplateId;
  private int _imageIndex = -1;
  /// <summary>Порядковый номер вьюшки</summary>
  private int _viewIndex = 10;
  /// <summary>Название вьюшки</summary>
  private string _viewName = "Пропуск строк";

  public SetupSkipLinesView()
  {
    this.InitializeComponent();
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service == null)
      return;
    Image image = DocumentMenuHelper.LoadImageFromResurces("Intermech.Document.Model.Resources.SkipLines.bmp");
    if (image != null)
      this._imageIndex = service.Add(image, "imgAVSSkipLinesSetup");
    else
      this._imageIndex = service.ImageIndex("imgSpecRow");
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    NodeID itemId = (NodeID) items.GetItemID(0);
    this._selectedID = itemId.ObjectID;
    this._selectedObjectTypeID = itemId.ObjectTypeID;
    this._provider = provider;
    this._documentTemplateId = -1L;
    if (provider == null || !this._selectedObjectTypeID.IsDefinedTypeId() || MetaDataHelper.IsObjectTypeChildOf(this._selectedObjectTypeID, AvsIDCache.ObjType_ConstructorDocumentTemplate))
      return;
    IAVSTemplatesViewsService service = (IAVSTemplatesViewsService) provider.GetService(typeof (IAVSTemplatesViewsService));
    if (service == null)
      return;
    this._documentTemplateId = service.DocumetnTemplateId;
  }

  public void Activate(IView previousView)
  {
    if (this._form != null)
      this._form.Parent = (Control) null;
    this._form = new FormSetupSkipLines((SettingsStructure) null, this._selectedID, this._selectedObjectTypeID, this._documentTemplateId);
    this._form.SetInView();
    this._form.SetParent((Control) this);
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
    this.Name = nameof (SetupSkipLinesView);
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
        Caption = "Пропуск строк",
        ImageIndex = -1,
        OrderID = 10
      };
    }
  }
}
