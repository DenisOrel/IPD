// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.FuncHandler
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Collections;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Generic delegate for user functions (takes some parms, returns some value)
/// </summary>
/// <param name="parms">Function parameters</param>
/// <returns>Return value</returns>
public delegate object FuncHandler(ArrayList parms);
