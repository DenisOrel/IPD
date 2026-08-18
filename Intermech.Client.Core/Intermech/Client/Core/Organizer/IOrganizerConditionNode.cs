
// Type: Intermech.Client.Core.Organizer.IOrganizerConditionNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Интерфейс необходимый для узлов, являющихся подузлами узла "Органайзер".
/// Создавался для того, чтобы можно было передать условия выбора данных.
/// </summary>
public interface IOrganizerConditionNode
{
  /// <summary>Установить условие выборки данных.</summary>
  /// <param name="conditions">Условия</param>
  void SetCondition(ConditionStructure[] conditions);
}
