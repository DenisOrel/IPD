// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ColumnCaptionsHelper
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Server;

[Serializable]
public class ColumnCaptionsHelper : LongLifeObject, IColumnCaptionsHelper
{
  Dictionary<string, string> IColumnCaptionsHelper.FillColumnCaptionsCach()
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>(ConstsHolder.ArchiveStructureColumns.Count);
    ICaptionsHelper service = ServerServices.GetService(typeof (ICaptionsHelper)) as ICaptionsHelper;
    foreach (string archiveStructureColumn in ConstsHolder.ArchiveStructureColumns)
    {
      string caption = service.GetCaption(archiveStructureColumn);
      dictionary.Add(archiveStructureColumn, caption);
    }
    return dictionary;
  }
}
