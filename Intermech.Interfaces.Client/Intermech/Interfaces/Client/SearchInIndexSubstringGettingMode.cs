// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SearchInIndexSubstringGettingMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

[TypeConverter(typeof (EnumDescConverter))]
public enum SearchInIndexSubstringGettingMode : long
{
  [Description("Да")] Yes,
  [Description("Нет")] No,
  [Description("Сохранять выбор пользователя")] KeepUsersChoice,
}
