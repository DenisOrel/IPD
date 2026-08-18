// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSObjectInfo
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Класс обертка для ссылочных атрибутов</summary>
public class AVSObjectInfo
{
  /// <summary>Заголовок</summary>
  public string Text;
  /// <summary>Идентификатор объекта в ImBase</summary>
  public long Id;

  public AVSObjectInfo()
  {
  }

  public AVSObjectInfo(long id, string text)
  {
    this.Id = id;
    this.Text = text;
  }

  public override string ToString() => this.Text;
}
