
// Type: Intermech.Search.ITypeProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search
{
    public interface ITypeProvider
    {
      Type GetObjectType(int objectTypeID);

      Type GetRelationType(int relationTypeID);

      void RegisterObjectType(int objectTypeID, Type objectType);

      void RegisterRelationType(int relationTypeID, Type relationType);
    }
}
