
// Type: Intermech.Expressions.OperatorType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions
{
    internal enum OperatorType
    {
      noOperator = -1, // 0xFFFFFFFF
      plusModifier = 0,
      minusModifier = 1,
      plusOperator = 2,
      minusOperator = 3,
      multiplyOperator = 4,
      divideOperator = 5,
      modulusOperator = 6,
      powerOperator = 7,
      isLessThanOperator = 8,
      isGreaterThanOperator = 9,
      isEqualToOperator = 10, // 0x0000000A
      isBasicEqualToOperator = 11, // 0x0000000B
      isNotEqualToOperator = 12, // 0x0000000C
      isBasicNotEqualToOperator = 13, // 0x0000000D
      isLessThanOrEqualToOperator = 14, // 0x0000000E
      isGreaterThanOrEqualToOperator = 15, // 0x0000000F
      andOperator = 16, // 0x00000010
      orOperator = 17, // 0x00000011
      notOperator = 18, // 0x00000012
      andBasicOperator = 19, // 0x00000013
      orBasicOperator = 20, // 0x00000014
      notBasicOperator = 21, // 0x00000015
      bitwiseAndOperator = 22, // 0x00000016
      bitwiseInclusiveOrOperator = 23, // 0x00000017
      bitwiseExclusiveOrOperator = 24, // 0x00000018
      bitwiseCompliment = 25, // 0x00000019
      shiftLeftOperator = 26, // 0x0000001A
      shiftRightOperator = 27, // 0x0000001B
      lastOperator = 28, // 0x0000001C
    }
}
