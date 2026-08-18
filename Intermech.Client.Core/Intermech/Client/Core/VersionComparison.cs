
// Type: Intermech.Client.Core.VersionComparison
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Класс, использующийся для сравнения файлов</summary>
public static class VersionComparison
{
  /// <summary>Получить версию для сравнения</summary>
  /// <param name="viewServices"></param>
  /// <param name="itemObj">Для кого ищем версию</param>
  /// <returns>ИД версии для сравнения, возможно ИД архивной копии или UnknownObjectId </returns>
  public static long GetVersionForCompareId(System.IServiceProvider viewServices, IDBObjectID itemObj)
  {
    bool flag = false;
    List<long> objectIdVersions;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      objectIdVersions = sessionKeeper.Session.GetObjectIDVersions(itemObj.Value, false);
      objectIdVersions.Remove(itemObj.Value);
      IDBObject dbObject = sessionKeeper.Session.GetObject(itemObj.Value, false);
      flag = dbObject.CheckoutBy != 0L && dbObject.CheckoutBy == sessionKeeper.Session.UserID;
    }
    long versionForCompareId;
    if (objectIdVersions.Count > 0)
    {
      VersionChoosingForm versionChoosingForm = new VersionChoosingForm();
      versionChoosingForm.Init(itemObj.Value, objectIdVersions, viewServices);
      int num = (int) versionChoosingForm.ShowDialog();
      versionForCompareId = versionChoosingForm.VersionForCompareId;
    }
    else if (flag)
    {
      versionForCompareId = Math.Abs(itemObj.Value);
    }
    else
    {
      int num = (int) MessageBox.Show("Не найдено версий для выполнения команды сравнения.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      versionForCompareId = 0L;
    }
    return versionForCompareId;
  }
}
