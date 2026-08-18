// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DopZamenyGroups
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Дескриптор списка групп допустимых замен</summary>
internal class DopZamenyGroups
{
  /// <summary>Список номеров групп</summary>
  public List<long> SubGroupNumsList = new List<long>();
  /// <summary>Словарь, ключ в котором - номер группы, значение - дескриптор группы допустимых замен</summary>
  public Dictionary<long, DopZamenyGroup> GroupNumToGroupDict = new Dictionary<long, DopZamenyGroup>();
}
