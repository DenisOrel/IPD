
// Type: Intermech.Client.Core.ObjectCreator.Controls.SetFileAttrPrototype
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>Класс для установки прототипа файлового атрибута</summary>
public static class SetFileAttrPrototype
{
  /// <summary>
  /// Статическая функция запуска процесса установки прототипа для файлового атрибута
  /// (и если потребуется вызова диалога выбора прототипа)
  /// </summary>
  /// <param name="attr">Файловый атрибут для которого надо установить прототип</param>
  /// <returns>Если файл был добавлен True</returns>
  public static bool Execute(IDBAttribute attr, IUserSession session, IDBObject dBObject)
  {
    if (attr == null || !(attr is IDBFileAttribute))
      return false;
    long[] prototypes = (attr as IDBFileAttribute).SetPrototype(0L);
    if (prototypes == null)
      return false;
    if (prototypes.Length > 1)
    {
      using (SelectFileAttrPrototypeDialog attrPrototypeDialog = new SelectFileAttrPrototypeDialog(prototypes))
      {
        int attributeId = attr.AttributeID;
        long objectId = dBObject.ObjectID;
        if (attrPrototypeDialog.ShowDialog() != DialogResult.OK)
          return false;
        attr = session.GetObjectAttributeByID(objectId, attributeId);
        (attr as IDBFileAttribute).SetPrototype(attrPrototypeDialog.SelectedPrototypeId);
      }
    }
    return true;
  }
}
