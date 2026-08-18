
// Type: Intermech.Navigator.GlobalNode.Descriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.GlobalNode;

public class Descriptor : HiveDescriptor
{
  /// <summary>
  /// Создает дескриптор элемента навигации, реализующего корень всего
  /// дерева навигации.
  /// </summary>
  public Descriptor()
    : base(Intermech.Navigator.Consts.CategoryGlobalNode, 0, LocalizationHolder.rm.GetString("Client.Core_1120"))
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализация дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected Descriptor(PersistentState state)
    : this()
  {
  }

  /// <summary>Выполняет сериализацию дескриптора.</summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
  }
}
