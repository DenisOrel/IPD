
// Type: Intermech.Protection.CodesScaner
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Protection
{
    public static class CodesScaner
    {
      public static int Count => KeyCodes.Count;

      public static int RandomQuery(ref byte[] query, ref byte[] reply)
      {
        return KeyCodes.RandomQuery(ref query, ref reply);
      }

      public static int DateQuery(ref byte[] query, ref byte[] reply)
      {
        return KeyCodes.DateQuery(ref query, ref reply);
      }

      public static int GetCodes(int pos, ref byte[] query, ref byte[] reply)
      {
        return KeyCodes.GetCodes(pos, ref query, ref reply);
      }
    }
}
