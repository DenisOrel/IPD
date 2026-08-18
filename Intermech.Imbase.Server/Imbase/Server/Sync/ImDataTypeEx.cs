// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImDataTypeEx
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal enum ImDataTypeEx
{
  [Description("Не поддерживается")] IEX_UNKNOWN,
  [Description("Строковое")] IEX_STRING,
  [Description("Целое")] IEX_INTEGER,
  [Description("Вещественное")] IEX_FLOAT,
  [Description("Логическое")] IEX_BOOL,
  [Description("Ссылка")] IEX_REF,
  [Description("Набор")] IEX_SET,
  [Description("")] IEX_ADT,
}
