// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Navigator.Commands.XmlImportCommand
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.XmlExchange;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.XmlExchange.Client.Kernel.Tasks;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.Client.Navigator.Commands;

/// <summary>
/// 
/// </summary>
internal static class XmlImportCommand
{
  /// <summary>Выбрать конфигурацию импорта</summary>
  /// <returns>Идентификатор конфигурации импорта или Intermech.Consts.UnknownObjectId</returns>
  private static long SelectImportCfg()
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("XmlExchange.Client_16"), LocalizationHolder.rm.GetString("XmlExchange.Client_20"), MetaDataHelper.GetObjectTypeID(XmlExchangeConsts.Common.ImportSettObjTypeGuid), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    return numArray == null || numArray.Length == 0 ? 0L : numArray[0];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  internal static void Execute(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = XmlExchangeProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = XmlExchangeProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(XmlExchangeProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_2"), (object) LocalizationHolder.rm.GetString("XmlExchange.Client_1"), (object) num1));
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = LocalizationHolder.rm.GetString("XmlExchange.Client_23");
    openFileDialog.CheckFileExists = true;
    openFileDialog.CheckPathExists = true;
    openFileDialog.DefaultExt = "zip";
    openFileDialog.DereferenceLinks = true;
    openFileDialog.Multiselect = true;
    openFileDialog.RestoreDirectory = true;
    openFileDialog.SupportMultiDottedExtensions = true;
    openFileDialog.Title = LocalizationHolder.rm.GetString("XmlExchange.Client_17");
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    long configurationId = XmlImportCommand.SelectImportCfg();
    if (configurationId == 0L)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("XmlExchange.Client_22"), LocalizationHolder.rm.GetString("XmlExchange.Client_21"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ServiceUtils.GetService<IXmlExchangeService>((object) sessionKeeper.Session, true);
      foreach (string fileName in openFileDialog.FileNames)
      {
        XmlExchangeImportTask task = new XmlExchangeImportTask(fileName, true, configurationId);
        XmlExchangeClientCache.Services.BackgroundTaskView.AddTask((IBackgroundTask) task);
      }
    }
  }
}
