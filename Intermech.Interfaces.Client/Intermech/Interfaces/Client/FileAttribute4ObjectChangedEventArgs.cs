// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FileAttribute4ObjectChangedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы нотификации изменения файлового атрибута объекта (ftFile ftBlob ftShortBlob)
/// </summary>
public class FileAttribute4ObjectChangedEventArgs : Attribute4ObjectEventArgs
{
  public FileAttribute4ObjectChangedEventArgs(int attributeID, int attributeType, long objectID)
    : base(ClientConsts.NotificationFileAttribute4ObjectChanged, attributeID, attributeType, objectID)
  {
  }

  public FileAttribute4ObjectChangedEventArgs(int attributeID, long objectID)
    : base(ClientConsts.NotificationFileAttribute4ObjectChanged, attributeID, objectID)
  {
  }
}
