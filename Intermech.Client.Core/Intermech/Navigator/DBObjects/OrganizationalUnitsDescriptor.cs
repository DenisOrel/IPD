
// Type: Intermech.Navigator.DBObjects.OrganizationalUnitsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор для узла Организационных подразделений</summary>
public class OrganizationalUnitsDescriptor : HiveDescriptor
{
  /// <summary>Заголовок.</summary>
  public new static string Caption => LocalizationHolder.rm.GetString("OrganizationalUnitsName");

  /// <summary>
  /// Конструктор.
  /// Создает дескриптор для узла Организационных подразделений.
  /// </summary>
  public OrganizationalUnitsDescriptor()
    : base(Intermech.Navigator.Consts.CategoryOrganizationalUnitsNode, -1, OrganizationalUnitsDescriptor.Caption)
  {
  }

  /// <summary>
  /// Конструктор.
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected OrganizationalUnitsDescriptor(PersistentState state)
    : base(state)
  {
  }
}
