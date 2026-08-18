// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImClientFilesEditor
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Files;
using System;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>
/// Класс отвечает за редактирование файлов (пока containerelement) выгружая их на диск
/// </summary>
internal class ImClientFilesEditor : FilesEditor
{
  private string tempPath;

  public ImClientFilesEditor(IServiceProvider provider)
  {
    IFileVault service = provider.GetService<IFileVault>(false);
    if (service == null || service.TempArea == null)
      return;
    this.tempPath = service.TempArea.AreaPath;
  }

  public override string TempPath
  {
    get => this.tempPath != null ? this.tempPath + "\\ImDocEditor" : base.TempPath;
  }
}
