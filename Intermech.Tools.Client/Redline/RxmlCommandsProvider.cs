// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RxmlCommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Redline;

internal sealed class RxmlCommandsProvider : ICommandsProvider
{
  internal static readonly string ViewRxmlCommandName = "ViewRxml";
  private IFileVault fileVaultService;
  private Func<RedliningComObject> redliningApiFactory;
  private string exePath;
  private string rxmlFolderPath;
  private readonly bool exeFileExist;

  public RxmlCommandsProvider(
    IFileVault fileVaultService,
    Func<RedliningComObject> redliningApiFactory)
  {
    this.fileVaultService = fileVaultService;
    this.redliningApiFactory = redliningApiFactory;
    this.exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scrviewer.exe");
    this.rxmlFolderPath = Path.Combine(this.fileVaultService.TempArea.AreaPath, $"RXML_{this.fileVaultService.TempArea.GetRandomFileName()}");
    this.exeFileExist = File.Exists(this.exePath);
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if ((items.Count != 1 ? 0 : (this.exeFileExist ? 1 : 0)) == 0)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add(RxmlCommandsProvider.ViewRxmlCommandName, new CommandInfo(0, new ClickEventHandler(this.OnViewRxml)));
    return groupCommands;
  }

  private void OnViewRxml(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (!File.Exists(this.exePath))
      return;
    IDBObjectID itemData = (IDBObjectID) items.GetItemData(0, typeof (IDBObjectID));
    RedliningComObject redliningComObject = this.redliningApiFactory();
    redliningComObject.OpenDocument(itemData.Value);
    string redliningFile = redliningComObject.GetRedliningFile(this.rxmlFolderPath);
    if (string.IsNullOrEmpty(redliningFile))
    {
      int num = (int) MessageBox.Show("Файл замечаний у объекта отсутствует.");
    }
    else
      Process.Start(this.exePath, $"\"{redliningFile}\" {itemData.Value} 0")?.Dispose();
  }
}
