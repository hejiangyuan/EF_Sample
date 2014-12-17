using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication9.Model
{
    public partial class Shop : IShop
    {
        public Guid? ShId { get; set; }

        public Guid? ShDomainId { get; set; }

        public string ShIdentityCode { get; set; }

        public string ShCode { get; set; }

        public string ShName { get; set; }

        public string ShDefaultWarehouse { get; set; }

        public string ShWebShopPlatformType { get; set; }

        public string ShShortcutCode { get; set; }

        public string ShOuterCode { get; set; }

        public string ShWebShopType { get; set; }

        public string ShDesc { get; set; }
        public string ShType { get; set; }
        public string ShLevel { get; set; }
        public int ShIsWebShop { get; set; }
        public string ShWebShopId { get; set; }
        public string ShWebShopUrl { get; set; }
        public string ShWebShopUserIdentity { get; set; }
        public string ShWebShopAppKey { get; set; }
        public string ShWebShopAppSecret { get; set; }
        public string ShWebShopSessionKey { get; set; }
        public string ShWebShopSessionTag { get; set; }
        public int ShEnabled { get; set; }
        public string ShOwnerNick { get; set; }
        public string ShOwnerName { get; set; }
        public string ShCountry { get; set; }
        public string ShProvince { get; set; }
        public string ShCity { get; set; }
        public string ShDistrict { get; set; }
        public string ShAddress { get; set; }
        public string ShPostCode { get; set; }
        public string ShPhone { get; set; }
        public string ShFax { get; set; }
        public decimal? ShArea { get; set; }
        public string ShText1 { get; set; }
        public string ShText2 { get; set; }
        public string ShText3 { get; set; }
        public string ShText4 { get; set; }
        public string ShText5 { get; set; }
        public string ShText6 { get; set; }
        public string ShText7 { get; set; }
        public string ShText8 { get; set; }
        public string ShText9 { get; set; }
        public string ShText10 { get; set; }
        public string ShLongText1 { get; set; }
        public string ShLongText2 { get; set; }
        public string ShLongText3 { get; set; }
        public int ShCheckBox1 { get; set; }
        public int ShCheckBox2 { get; set; }
        public int ShCheckBox3 { get; set; }
        public int ShCheckBox4 { get; set; }
        public int ShCheckBox5 { get; set; }
        public DateTime? ShDate1 { get; set; }
        public DateTime? ShDate2 { get; set; }
        public DateTime? ShDate3 { get; set; }
        public int? ShInt1 { get; set; }
        public int? ShInt2 { get; set; }
        public int? ShInt3 { get; set; }
        public decimal? ShDecimal1 { get; set; }
        public decimal? ShDecimal2 { get; set; }
        public decimal? ShDecimal3 { get; set; }
        public string ShCreateUser { get; set; }
        public DateTime? ShCreateTime { get; set; }
        public string ShLastUpdateUser { get; set; }
        public DateTime? ShLastUpdateTime { get; set; }
        public string ShInvExpSetting { get; set; }
        public string ShWebShopOtherParam { get; set; }
        public Guid? ShRelateCustomer { get; set; }

    }
}
