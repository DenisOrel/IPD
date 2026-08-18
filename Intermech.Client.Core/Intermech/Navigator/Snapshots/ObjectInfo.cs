
// Type: Intermech.Navigator.Snapshots.ObjectInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;


namespace Intermech.Navigator.Snapshots;

/// <summary>
///  Класс, содержащий информацию об объекте, необходимую для заполнения узла дерева
/// </summary>
internal class ObjectInfo
{
  /// <summary>ID объекта.</summary>
  public long ID { get; private set; }

  /// <summary>Наименование объекта</summary>
  public string Caption { get; private set; }

  /// <summary>Gets or sets the index of the image.</summary>
  public int ImageIndex { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="objectID">ID объекта</param>
  public ObjectInfo(long objectID)
  {
    this.ID = objectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      this.Caption = dbObject.Caption;
      this.ImageIndex = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
    }
  }
}
