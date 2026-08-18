// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CustomImportService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;


namespace Intermech.Kernel.Services;

public class CustomImportService : LongLifeObject, ICustomImport
{
  public event CustomImported CustomImportedEvent;

  public void FireCustomImported(object sender, CustomImportedEventArgs e)
  {
    if (this.CustomImportedEvent == null)
      return;
    this.CustomImportedEvent(sender, e);
  }

  public event AfterCustomImport AfterImportObjects;

  public void FireAfterImportObjects(object sender, AfterCustomImportEventArgs e)
  {
    if (this.AfterImportObjects == null)
      return;
    this.AfterImportObjects(sender, e);
  }
}
