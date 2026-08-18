
// Type: Intermech.Interfaces.Plugins.IAssemblyResolveFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Plugins
{
    /// <summary>
    /// Интерфейс фильтра для загрузчика сборок. Фильтр применяется к сборкам, которые не были найдены по обычным правилам поиска и загрузки сборок на платформе .NET.
    /// Реализация должна быть thread safe.
    /// </summary>
    public interface IAssemblyResolveFilter
    {
      /// <summary>
      /// Позволяет определить, следует ли искать и загружать сборку с указанным именем.
      /// </summary>
      /// <param name="name">Имя сборки</param>
      /// <returns>true - следует искать и загружать сборку; false - не следует искать сборку, так как она не должна быть загружена</returns>
      bool CanResolve(string name);
    }
}
