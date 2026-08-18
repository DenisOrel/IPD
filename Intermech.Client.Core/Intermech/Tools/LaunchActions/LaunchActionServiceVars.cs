
// Type: Intermech.Tools.LaunchActions.LaunchActionServiceVars
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;


namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Содержит динамические переменные для тонкой настройки поведения сервиса <see cref="T:Intermech.Tools.LaunchActions.LaunchActionService" />.
/// </summary>
internal static class LaunchActionServiceVars
{
  public static readonly DynamicVariable<bool> RootObjectMode = new DynamicVariable<bool>("LaunchActionServiceVars.RootObjectMode", false);
}
