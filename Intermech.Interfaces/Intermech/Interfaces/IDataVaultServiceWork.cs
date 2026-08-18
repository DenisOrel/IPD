
// Type: Intermech.Interfaces.IDataVaultServiceWork
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>служба, для определения наличия шкафов нужного типа</summary>
    public interface IDataVaultServiceWork
    {
      /// <summary>существуют ли в системе шкафы нужного типа</summary>
      bool IsDataVaultStorageExists { get; }

      /// <summary>вызывать при удалении шкафа нужного типа</summary>
      void ResetStoragesExistState();

      /// <summary>вызывать при создании шкафа нужного типа</summary>
      void SetStorageExistsState();
    }
}
