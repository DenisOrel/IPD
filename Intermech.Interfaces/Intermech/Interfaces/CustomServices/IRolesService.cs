
// Type: Intermech.Interfaces.CustomServices.IRolesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.CustomServices
{
    /// <summary>Служба для получения информации о ролях пользователей</summary>
    public interface IRolesService
    {
      /// <summary>
      /// Возвращает массив ролей, назначенных пользователю userID
      /// </summary>
      /// <param name="userID">ObjectID пользователя (если -1, то возвращает полный список ролей в системе)</param>
      /// <returns>Массив ролей или пустой массив</returns>
      RoleProperties[] GetRolesList(long userID);
    }
}
