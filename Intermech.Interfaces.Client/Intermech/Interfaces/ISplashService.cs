// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ISplashService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces;

/// <summary>Сервис для работы с SplashScreen</summary>
public interface ISplashService
{
  /// <summary>Количество шагов ProgressBar</summary>
  int Steps { get; set; }

  /// <summary>Текущее положение ProgressBar</summary>
  int Position { get; set; }

  /// <summary>Имя текущего шага</summary>
  string StepName { get; set; }

  /// <summary>Описание текущего шага</summary>
  string StepDescription { get; set; }

  /// <summary>Увеличение текущей позиции в ProgressBar</summary>
  void StepIt();

  /// <summary>Закрывает окно SplashScreen</summary>
  void CloseSplash();

  void ShowSplash();

  void HideSplash();
}
