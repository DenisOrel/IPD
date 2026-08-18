// Decompiled with JetBrains decompiler
// Type: Intermech.Search.RoleConfigurationManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Search;

public sealed class RoleConfigurationManager : IRoleConfigurationManager
{
  public static readonly Guid DefaultColumnsSettingsAttributeTypeGuid = new Guid("cadd955c-306c-11d8-b4e9-00304f19f545");

  public ColumnPack LoadNavigatorDefaultColumnPack(long roleConfigurationVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID))
      throw new ArgumentException();
    using (MemoryStream serializationStream = new MemoryStream())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        this.ReadFromAttributeToStream(this.GetDefaultColumnsSettingsAttribute(roleConfigurationVersionID, session), (Stream) serializationStream);
      }
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      serializationStream.Seek(0L, SeekOrigin.Begin);
      if (serializationStream.Length <= 0L)
        columnPack1 = new ColumnPack();
      else if (!(binaryFormatter.Deserialize((Stream) serializationStream) is ColumnPack columnPack1))
        columnPack1 = new ColumnPack();
      ColumnPack columnPack2 = columnPack1;
      return this.CheckColumnPackAfterDeserilization(columnPack2) ? columnPack2 : new ColumnPack();
    }
  }

  public void SaveNavigatorDefaultColumnPack(long roleConfigurationVersionID, ColumnPack columnPack)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID))
      throw new ArgumentException();
    if (columnPack == null)
      throw new ArgumentNullException(nameof (columnPack));
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) columnPack);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IDBAttribute settingsAttribute = this.GetDefaultColumnsSettingsAttribute(roleConfigurationVersionID, session);
        BlobInformation columnsBlobInformation = RoleConfigurationManager.GetNavigatorDefaultColumnsBlobInformation(session);
        this.WriteFromStreamToAttribute((Stream) serializationStream, settingsAttribute, columnsBlobInformation);
      }
    }
  }

  private static BlobInformation GetNavigatorDefaultColumnsBlobInformation(IUserSession userSession)
  {
    BlobInformation columnsBlobInformation = new BlobInformation();
    columnsBlobInformation.ArcMethod = ArcMethods.ZLibPacked;
    if (columnsBlobInformation.ModifyDate == DateTime.MinValue)
      columnsBlobInformation.ModifyDate = userSession.UTCTime;
    return columnsBlobInformation;
  }

  private IDBAttribute GetDefaultColumnsSettingsAttribute(
    long roleConfigurationVersionID,
    IUserSession userSession)
  {
    return userSession.GetObject(roleConfigurationVersionID).GetAttributeByGuid(RoleConfigurationManager.DefaultColumnsSettingsAttributeTypeGuid);
  }

  private void ReadFromAttributeToStream(IDBAttribute attribute, Stream stream)
  {
    new BlobProcReader(attribute, 0, stream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
  }

  private void WriteFromStreamToAttribute(
    Stream stream,
    IDBAttribute attribute,
    BlobInformation blobInformation)
  {
    new BlobProcWriter(attribute, (int) stream.Length, blobInformation, stream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
  }

  private bool CheckColumnPackAfterDeserilization(ColumnPack columnPack)
  {
    foreach (KeyValuePair<NavigatorColumnsKey, NodeColumnCollection> keyValuePair in columnPack)
    {
      NodeColumnCollection source = keyValuePair.Value;
      if (source == null || source.Where<NodeColumn>((Func<NodeColumn, bool>) (o => !o.IsValid)).Count<NodeColumn>() > 0)
        return false;
    }
    return true;
  }
}
