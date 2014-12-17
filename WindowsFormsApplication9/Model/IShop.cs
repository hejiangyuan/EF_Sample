using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication9.Model
{
    public partial interface IShop
    {
        /// <summary>
        /// 
        /// </summary>
        Guid? ShId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Guid? ShDomainId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShIdentityCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShShortcutCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShOuterCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShDefaultWarehouse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShIsWebShop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopPlatformType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopUserIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopAppKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopAppSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopSessionKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopSessionTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShOwnerNick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShOwnerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShCountry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShProvince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShCity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShDistrict { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShPostCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShFax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        decimal? ShArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShText10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShLongText1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShLongText2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShLongText3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShCheckBox1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShCheckBox2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShCheckBox3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShCheckBox4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ShCheckBox5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? ShDate1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? ShDate2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? ShDate3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int? ShInt1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int? ShInt2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int? ShInt3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        decimal? ShDecimal1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        decimal? ShDecimal2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        decimal? ShDecimal3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShCreateUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? ShCreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShLastUpdateUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? ShLastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShInvExpSetting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ShWebShopOtherParam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Guid? ShRelateCustomer { get; set; }



    }
}
