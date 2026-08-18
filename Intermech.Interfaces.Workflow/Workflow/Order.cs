// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Order
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[AttributeUsage(AttributeTargets.All)]
public class Order : Attribute
{
  private int _pos;

  public Order(int pos) => this._pos = pos;

  public int Pos => this._pos;
}
