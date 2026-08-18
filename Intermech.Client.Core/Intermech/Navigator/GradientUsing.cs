
// Type: Intermech.Navigator.GradientUsing
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator;

/// <summary>для указания, используется ли градиент в кистях</summary>
public enum GradientUsing
{
  /// <summary>всё без  градиента</summary>
  None,
  /// <summary>для взятых пользователем на изменение</summary>
  CheckOut,
  /// <summary>для взятых другими пользователями на изменение</summary>
  CheckedOutOther,
}
