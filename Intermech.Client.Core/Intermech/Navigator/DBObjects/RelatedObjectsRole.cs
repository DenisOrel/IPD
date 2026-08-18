
// Type: Intermech.Navigator.DBObjects.RelatedObjectsRole
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Указывает роль объектов, связанных с каким-либо объектом. Используется в
/// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
/// состав или применяемость объекта.
/// </summary>
public enum RelatedObjectsRole
{
  /// <summary>
  /// Связанные объекты должны входить в состав указанного объекта
  /// </summary>
  Composition,
  /// <summary>
  /// Указанный объект должен применяться в связанных объектах
  /// </summary>
  Applicability,
}
