// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptCheckerConsts
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal static class ScriptCheckerConsts
{
  public static readonly string BreakingChangesWarning = "В IPS, начиная с версии 5 SP2, доступна улучшенная система компиляции и выполнения сценариев C#. Она призвана решить проблемы текущей системы сценариев, связанные с многопоточностью и расходом системных ресурсов. До выхода IPS 6 обе системы будут действовать одновременно, а после выхода IPS 6 останется только новая система, а текущая будет полностью отключена.";
  public static readonly string ConversionWarning = "Все существующие сценарии C# должны быть преобразованы, чтобы они поддерживались новой системой выполнения. Подробнее об этом можно прочесть в 'Руководстве программиста' в разделе 'Приложение 3. Сценарии C#'.";
}
