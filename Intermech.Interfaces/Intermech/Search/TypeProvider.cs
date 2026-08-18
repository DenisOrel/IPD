
// Type: Intermech.Search.TypeProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search
{
    public sealed class TypeProvider : ITypeProvider
    {
      private Dictionary<int, Type> _objectTypeDictionary = new Dictionary<int, Type>();
      private Dictionary<int, Type> _relationTypeDictionary = new Dictionary<int, Type>();

      public Type GetObjectType(int objectTypeID)
      {
        if (objectTypeID == -1)
          throw new ArgumentException();
        Type objectType = (Type) null;
        this._objectTypeDictionary.TryGetValue(objectTypeID, out objectType);
        return objectType;
      }

      public Type GetRelationType(int relationTypeID)
      {
        if (relationTypeID == -1)
          throw new ArgumentException();
        Type relationType = (Type) null;
        this._relationTypeDictionary.TryGetValue(relationTypeID, out relationType);
        return relationType;
      }

      public void RegisterObjectType(int objectTypeID, Type objectType)
      {
        if (objectTypeID == -1)
          throw new ArgumentException();
        if (objectType == (Type) null)
          throw new ArgumentNullException(nameof (objectType));
        this._objectTypeDictionary.Add(objectTypeID, objectType);
      }

      public void RegisterRelationType(int relationTypeID, Type relationType)
      {
        if (relationTypeID == -1)
          throw new ArgumentException();
        if (relationType == (Type) null)
          throw new ArgumentNullException(nameof (relationType));
        this._relationTypeDictionary.Add(relationTypeID, relationType);
      }
    }
}
