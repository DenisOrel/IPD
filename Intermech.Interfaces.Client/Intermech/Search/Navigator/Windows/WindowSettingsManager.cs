// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.WindowSettingsManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Search.Navigator.Windows;

internal sealed class WindowSettingsManager : IWindowSettingsManager
{
  public const string NavigatorWindowsSettingsFileName = "Navigator.WindowsSettings";
  private WindowSettingsCollection _settingsCollection = new WindowSettingsCollection();
  private INotificationService _notificationService;
  private IWindowSettingsProviderFactory _windowSettingsProviderFactory;
  private bool _isLoaded;

  public WindowSettingsManager(IServiceProvider serviceProvider)
  {
    this._notificationService = serviceProvider != null ? serviceProvider.GetService<INotificationService>() : throw new ArgumentNullException(nameof (serviceProvider));
    this._windowSettingsProviderFactory = serviceProvider.GetService<IWindowSettingsProviderFactory>();
    this._notificationService.Subscribe("ApplicationClosing", new NotificationEventHandler(this.Application_ClientClosing));
  }

  public WindowSettingsBase Get(int categoryID, int typeID)
  {
    IWindowSettingsProvider settingsProvider = this._windowSettingsProviderFactory.Get(categoryID);
    return settingsProvider != null ? settingsProvider.Get(typeID, this._settingsCollection) : this._settingsCollection.Get(categoryID, typeID);
  }

  public void Set(int categoryID, int typeID, WindowSettingsBase settings)
  {
    IWindowSettingsProvider settingsProvider = this._windowSettingsProviderFactory.Get(categoryID);
    if (settingsProvider != null)
      settingsProvider.Set(typeID, settings, this._settingsCollection);
    else
      this._settingsCollection.AddOrSet(categoryID, typeID, settings);
  }

  public void Save()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._settingsCollection);
      BlobInformation config_info = new BlobInformation();
      config_info.ArcMethod = ArcMethods.ZLibPacked;
      config_info.FileName = "Navigator.WindowsSettings";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        session.Configurations.WriteConfigData(config_info, serializationStream.GetBuffer(), session.UserID);
      }
    }
  }

  public void Load()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      byte[] buffer;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IDBConfigurations configurations = session.Configurations;
        BlobInformation blobInformation = new BlobInformation();
        ref BlobInformation local1 = ref blobInformation;
        ref byte[] local2 = ref buffer;
        long userId = session.UserID;
        configurations.LoadConfigData("Navigator.WindowsSettings", out local1, out local2, userId);
      }
      serializationStream.Write(buffer, 0, buffer.Length);
      this._settingsCollection = new BinaryFormatter().Deserialize((Stream) serializationStream) as WindowSettingsCollection;
    }
  }

  private void Application_ClientClosing(object sender, NotificationEventArgs e)
  {
  }

  private void LoadIfNotLoaded()
  {
    if (this._isLoaded)
      return;
    this._isLoaded = true;
  }
}
