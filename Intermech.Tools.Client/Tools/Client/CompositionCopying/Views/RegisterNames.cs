// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.RegisterNames
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

public enum RegisterNames
{
  [Display(Name = "Без изменения")] Default,
  [Display(Name = "Все строчные")] Lowercase,
  [Display(Name = "Все прописные")] Uppercase,
  [Display(Name = "Начать с прописных")] FirstUpper,
}
