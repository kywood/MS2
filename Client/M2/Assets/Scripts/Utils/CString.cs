using UnityEngine;
using System.Collections;
using System.Text;


public enum eContryType
{
    None = -1,
    Kor,
    Usa,

    MAX
}

public enum eMoneyType
{
    None = -1,
    Won,
    Dol,

    MAX
}

public partial class Util
{
    public static string ConvertIntToMoney(eMoneyType moneyType, int Money)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("￦ ");
        sb.Append(Money.ToString() );
        return sb.ToString();
    }

    public static string ConvertIntToCnt( int Count)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("X ");
        sb.Append(Count.ToString());
        return sb.ToString();
    }
}
