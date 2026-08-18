
// Type: Intermech.Client.Core.DockBarsSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Client.Core;

public class DockBarsSettings : IDisposable
{
  private IServiceProvider _provider;
  private IConfigurationManager _manager;
  private Intermech.Bars.Office2003ColorScheme _barColorScheme;
  private Intermech.Docking.Rendering.Office2003Renderer.Office2003ColorScheme _dockColorScheme;
  private BarRendererType _barRenderer;
  private DockRendererType _dockRenderer;
  private bool _fullMenus;
  private bool _showImageInDocumentTab = true;

  public DockBarsSettings(IServiceProvider provider)
  {
    this._provider = provider;
    this._fullMenus = true;
    this._manager = (IConfigurationManager) provider.GetService(typeof (IConfigurationManager));
    this._manager.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.manager_ConfigurationBeforeSave);
    this.LoadConfiguration();
  }

  public void ApplyChanges()
  {
    BarManager service1 = (BarManager) this._provider.GetService(typeof (BarManager));
    if (service1 != null)
    {
      switch (this._barRenderer)
      {
        case BarRendererType.Office2002:
          service1.Renderer = (IToolBarRenderer) new Office2002Renderer();
          break;
        case BarRendererType.Office2003:
          service1.Renderer = (IToolBarRenderer) new Intermech.Bars.Office2003Renderer()
          {
            ColorScheme = this._barColorScheme
          };
          break;
        case BarRendererType.Whidbey:
          Intermech.Bars.WhidbeyRenderer whidbeyRenderer = new Intermech.Bars.WhidbeyRenderer();
          whidbeyRenderer.ColorScheme = this._barColorScheme;
          service1.Renderer = (IToolBarRenderer) whidbeyRenderer;
          break;
      }
      MenuBar menuBar = service1.MenuBar;
      if (menuBar != null)
        menuBar.FullMenus = this._fullMenus;
    }
    DockManager service2 = (DockManager) this._provider.GetService(typeof (DockManager));
    if (service2 == null)
      return;
    DocumentContainer documentContainer = service2.DocumentContainer;
    switch (this._dockRenderer)
    {
      case DockRendererType.Everett:
        service2.Renderer = (RendererBase) new EverettRenderer();
        break;
      case DockRendererType.Office2003:
        service2.Renderer = (RendererBase) new Intermech.Docking.Rendering.Office2003Renderer()
        {
          ColorScheme = this._dockColorScheme
        };
        break;
      case DockRendererType.Whidbey:
        Intermech.Docking.Rendering.WhidbeyRenderer whidbeyRenderer1 = new Intermech.Docking.Rendering.WhidbeyRenderer();
        service2.Renderer = (RendererBase) whidbeyRenderer1;
        break;
    }
    if (documentContainer == null)
      return;
    documentContainer.Renderer = service2.Renderer;
    documentContainer.ShowImageInDocumentTab = this._showImageInDocumentTab;
  }

  public void LoadConfiguration()
  {
    IConfiguration configuration = this._manager.Open(nameof (DockBarsSettings));
    if (configuration == null)
      return;
    this._barColorScheme = (Intermech.Bars.Office2003ColorScheme) Enum.Parse(typeof (Intermech.Bars.Office2003ColorScheme), configuration.GetProperty("BarColorScheme"), true);
    this._barRenderer = (BarRendererType) Enum.Parse(typeof (BarRendererType), configuration.GetProperty("BarRenderer"), true);
    if (configuration.HasProperty("FullMenus"))
      this._fullMenus = bool.Parse(configuration.GetProperty("FullMenus"));
    if (configuration.HasProperty("DockRenderer"))
      this._dockRenderer = (DockRendererType) Enum.Parse(typeof (DockRendererType), configuration.GetProperty("DockRenderer"), true);
    if (configuration.HasProperty("DockColorScheme"))
      this._dockColorScheme = (Intermech.Docking.Rendering.Office2003Renderer.Office2003ColorScheme) Enum.Parse(typeof (Intermech.Docking.Rendering.Office2003Renderer.Office2003ColorScheme), configuration.GetProperty("DockColorScheme"), true);
    if (!configuration.HasProperty("ShowImageInDocumentTab"))
      return;
    this._showImageInDocumentTab = bool.Parse(configuration.GetProperty("ShowImageInDocumentTab"));
  }

  [CustomDescription("Attribute.Client.Core_171")]
  [CustomDisplayName("Attribute.Client.Core_172")]
  public BarRendererType BarRenderer
  {
    get => this._barRenderer;
    set => this._barRenderer = value;
  }

  [CustomDescription("Attribute.Client.Core_173")]
  [CustomDisplayName("Attribute.Client.Core_174")]
  public DockRendererType DockRenderer
  {
    get => this._dockRenderer;
    set => this._dockRenderer = value;
  }

  [CustomDescription("Attribute.Client.Core_175")]
  [CustomDisplayName("Attribute.Client.Core_176")]
  public Intermech.Bars.Office2003ColorScheme BarColorScheme
  {
    get => this._barColorScheme;
    set => this._barColorScheme = value;
  }

  [CustomDescription("Attribute.Client.Core_177")]
  [CustomDisplayName("Attribute.Client.Core_178")]
  public Intermech.Docking.Rendering.Office2003Renderer.Office2003ColorScheme DockColorScheme
  {
    get => this._dockColorScheme;
    set => this._dockColorScheme = value;
  }

  [CustomDescription("Attribute.Client.Core_179")]
  [CustomDisplayName("Attribute.Client.Core_180")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool FullMenus
  {
    get => this._fullMenus;
    set => this._fullMenus = value;
  }

  [CustomDescription("Attribute.Client.Core_181")]
  [CustomDisplayName("Attribute.Client.Core_182")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [DefaultValue(true)]
  public bool ShowImageInDocumentTab
  {
    get => this._showImageInDocumentTab;
    set => this._showImageInDocumentTab = value;
  }

  private void manager_ConfigurationBeforeSave(IConfigurationManager configurationManager)
  {
    IConfiguration configuration = this._manager.Create(nameof (DockBarsSettings));
    configuration.SetProperty("BarColorScheme", this._barColorScheme.ToString());
    configuration.SetProperty("DockColorScheme", this._dockColorScheme.ToString());
    configuration.SetProperty("BarRenderer", this._barRenderer.ToString());
    configuration.SetProperty("FullMenus", this._fullMenus.ToString());
    configuration.SetProperty("DockRenderer", this._dockRenderer.ToString());
    configuration.SetProperty("ShowImageInDocumentTab", this._showImageInDocumentTab.ToString());
  }

  public void Dispose()
  {
    this._manager.ConfigurationBeforeSave -= new ConfigurationBeforeSaveEventHandler(this.manager_ConfigurationBeforeSave);
  }
}
