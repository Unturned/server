//------------------------------------------------------------------------------
// <auto-generated>
//     Ezt a kódot eszköz generálta.
//     Futásidejű verzió:4.0.30319.269
//
//     Ennek a fájlnak a módosítása helytelen viselkedést eredményezhet, és elvész, ha
//     a kódot újragenerálják.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace Unturned
{
    public interface IBanEntry
    {
        string Name { get;}
        string SteamID { get;}
        string Reason  { get; }
        string BannedBy  { get;  }
        DateTime BanTime  { get; }

        // Expiry time. -1 if never
        int Expires { get; set; }
    }
}

