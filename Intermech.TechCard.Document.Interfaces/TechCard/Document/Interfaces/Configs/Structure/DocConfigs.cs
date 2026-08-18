// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.DocConfigs
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public class DocConfigs
{
  private List<int> movingOpers = new List<int>();
  private List<int> nomovingOpers = new List<int>();
  private List<int> nextOperMoving = new List<int>();
  private List<int> nextOperNoMoving = new List<int>();

  public List<int> MovingOpers
  {
    get => this.movingOpers;
    set
    {
      this.movingOpers.Clear();
      this.movingOpers.AddRange((IEnumerable<int>) value);
    }
  }

  public List<int> NoMovingOpers
  {
    get => this.nomovingOpers;
    set
    {
      this.nomovingOpers.Clear();
      this.nomovingOpers.AddRange((IEnumerable<int>) value);
    }
  }

  public List<int> NextOperMoving
  {
    get => this.nextOperMoving;
    set
    {
      this.nextOperMoving.Clear();
      this.nextOperMoving.AddRange((IEnumerable<int>) value);
    }
  }

  public List<int> NextOperNoMoving
  {
    get => this.nextOperNoMoving;
    set
    {
      this.nextOperNoMoving.Clear();
      this.nextOperNoMoving.AddRange((IEnumerable<int>) value);
    }
  }
}
