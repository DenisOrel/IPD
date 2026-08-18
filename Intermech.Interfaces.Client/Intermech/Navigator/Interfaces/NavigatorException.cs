// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NavigatorException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Базовый класс исключений, служащий основой для построения иерархии
/// исключенией навигатора.
/// </summary>
public class NavigatorException : ApplicationException
{
  public NavigatorException()
  {
  }

  public NavigatorException(string message)
    : base(message)
  {
  }
}
