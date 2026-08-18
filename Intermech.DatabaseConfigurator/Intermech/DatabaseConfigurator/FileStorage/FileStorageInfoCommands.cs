// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileStorageInfoCommands
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Objects;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileStorageInfoCommands
{
  public static void Show(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (FileStorageInfoForm fileStorageInfoForm = new FileStorageInfoForm((items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value))
    {
      int num = (int) fileStorageInfoForm.ShowDialog();
    }
  }

  public static void CutCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.IsCollage)
    {
      int num = (int) IMMessageBox.Show(MessageDialogs.msgError, LocalizationHolder.rm.GetString("DatabaseConfigurator_40"), MessageBoxButtons.OK, IMMessageBoxImage.Error);
    }
    else
    {
      ArrayList arrayList = new ArrayList(items.Count);
      IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
      string str = string.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectID parentData = (IDBObjectID) items.GetParentData(0, typeof (IDBObjectID));
        if (parentData == null)
          return;
        IDBObject dbObject = sessionKeeper.Session.GetObject(parentData.Value, false);
        if (dbObject != null)
          str = dbObject.Caption;
        ClipboardFiles data = new ClipboardFiles(parentData.Value);
        for (int index = 0; index < items.Count; ++index)
        {
          IFileID itemData = (IFileID) items.GetItemData(index, typeof (IFileID));
          if (itemData != null)
            data.Add(itemData.Value);
        }
        service.SetDataObject((object) new DataObject((object) data), string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_41"), (object) items.Count, (object) str));
      }
    }
  }

  public static void PasteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count != 1 || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service) || !(service.GetDataObject() is DataObject dataObject) || !(dataObject.GetData(typeof (ClipboardFiles)) is ClipboardFiles data))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long num1 = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
      if (num1 == data.StorageID)
      {
        int num2 = (int) IMMessageBox.Show(MessageDialogs.msgError, LocalizationHolder.rm.GetString("DatabaseConfigurator_42"), MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
      else
      {
        if (!(sessionKeeper.Session.GetObject(data.StorageID, false) is IBlobStorageObject blobStorageObject) || !blobStorageObject.RemoveFiles((long[]) data.FileIDs.ToArray(typeof (long)), num1))
          return;
        ((INotificationService) ServicesManager.GetService(typeof (INotificationService))).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", num1));
        service.RemoveCurrentDataObject();
      }
    }
  }
}
