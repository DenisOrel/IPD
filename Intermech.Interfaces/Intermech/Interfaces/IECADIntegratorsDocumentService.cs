
// Type: Intermech.Interfaces.IECADIntegratorsDocumentService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public interface IECADIntegratorsDocumentService
    {
      /// <summary>Открыть окно со специцикацией для сборки</summary>
      /// <param name="assemblyID">Идентификатор версии сборки</param>
      void CreateSpecificationWindow(long assemblyID);
    }
}
