// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechProduction
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.TechCard.Client.Common;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>Класс, содержащий информацию о "виде производства"</summary>
/// <summary>Конструктор</summary>
/// <param name="id">Ид. вида производства</param>
/// <param name="name">Наименование</param>
public class TechProduction(long id, string name) : IntBaseInfo(id, name)
{
  /// <summary>Ид. вида производства</summary>
  /// <remarks> For compatibility only </remarks>
  public long ID => this.Value;

  /// <summary>Наименование вида производства</summary>
  /// <remarks> For compatibility only </remarks>
  public string Name => this.Caption;

  /// <summary>Строковое представление объекта</summary>
  /// <returns></returns>
  public override string ToString() => this.Name;
}
