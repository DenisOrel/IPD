// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Common.XmlExportExtAction
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Common;

/// <summary>Действия, которые может выполнять расширение экспорта</summary>
[Flags]
[Serializable]
public enum XmlExportExtAction : long
{
  ImBeforeExportObject = 256, // 0x0000000000000100
  ImAfterExportObject = 512, // 0x0000000000000200
  ImBeforeExportRelation = 1024, // 0x0000000000000400
  ImAfterExportRelation = 2048, // 0x0000000000000800
  ImBeforeExportAttribute = 4096, // 0x0000000000001000
  /// <summary>Произвольная обработка после завершения задачи</summary>
  TaskPostProcess = 72057594037927936, // 0x0100000000000000
}
