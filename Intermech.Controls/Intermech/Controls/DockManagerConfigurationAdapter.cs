
// Type: Intermech.Controls.DockManagerConfigurationAdapter
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Docking;
using Intermech.Interfaces.Configuration;
using System;


namespace Intermech.Controls;

/// <summary>
/// Реализует хранение конфигурации DockManager в контейнере настроек клиента IPS.
/// </summary>
public sealed class DockManagerConfigurationAdapter : DockManagerConfigurationStorage
{
  private const string LayoutPropertyName = "Data";
  private readonly IConfigurationManager configManager;
  private readonly string fileName;

  public DockManagerConfigurationAdapter(IConfigurationManager configManager, string fileName)
  {
    if (configManager == null)
      throw new ArgumentNullException(nameof (configManager));
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.configManager = configManager;
    this.fileName = fileName;
  }

  public DockManagerConfigurationAdapter(IConfigurationManager configManager)
    : this(configManager, "DockManagerLayout")
  {
  }

  public override string TryLoadLayout()
  {
    IConfiguration configuration = this.configManager.Open(this.fileName);
    if (configuration == null)
      return (string) null;
    string property = configuration.GetProperty("Data");
    return string.IsNullOrEmpty(property) ? (string) null : property;
  }

  public override void SaveLayout(string layout)
  {
    if (layout == null)
      layout = string.Empty;
    this.configManager.Create(this.fileName).SetProperty("Data", layout);
  }
}
