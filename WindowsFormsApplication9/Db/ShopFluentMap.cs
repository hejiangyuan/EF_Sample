using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication9.Model;

namespace WindowsFormsApplication9.Db
{
    public class ShopFluentMap : EntityTypeConfiguration<Shop>
    {
        public ShopFluentMap()
        {
            HasKey(m => m.ShId).ToTable("SHOP");

            Property(m => m.ShDomainId).HasColumnName("SHDOMAINID").IsRequired();
            Property(m => m.ShIdentityCode).HasColumnName("SHIDENTITYCODE").IsRequired();
            Property(m => m.ShName).HasColumnName("SHNAME").IsRequired();

            Property(m => m.ShAddress).HasColumnName("SHADDRESS");
            Property(m => m.ShArea).HasColumnName("SHAREA");
            Property(m => m.ShCheckBox1).HasColumnName("SHCHECKBOX1");
            Property(m => m.ShCheckBox2).HasColumnName("SHCHECKBOX2");
            Property(m => m.ShCheckBox3).HasColumnName("SHCHECKBOX3");
            Property(m => m.ShCheckBox4).HasColumnName("SHCHECKBOX4");
            Property(m => m.ShCheckBox5).HasColumnName("SHCHECKBOX5");
            Property(m => m.ShCity).HasColumnName("SHCITY");
            Property(m => m.ShCode).HasColumnName("SHCODE");
            Property(m => m.ShCountry).HasColumnName("SHCOUNTRY");
            Property(m => m.ShCreateTime).HasColumnName("SHCREATETIME");
            Property(m => m.ShCreateUser).HasColumnName("SHCREATEUSER");
            Property(m => m.ShDate1).HasColumnName("SHDATE1");
            Property(m => m.ShDate2).HasColumnName("SHDATE2");
            Property(m => m.ShDate3).HasColumnName("SHDATE3");
            Property(m => m.ShDecimal1).HasColumnName("SHDECIMAL1");
            Property(m => m.ShDecimal2).HasColumnName("SHDECIMAL2");
            Property(m => m.ShDecimal3).HasColumnName("SHDECIMAL3");
            Property(m => m.ShDefaultWarehouse).HasColumnName("SHDEFAULTWAREHOUSE");
            Property(m => m.ShDesc).HasColumnName("SHDESC");
            Property(m => m.ShDistrict).HasColumnName("SHDISTRICT");
            Property(m => m.ShEnabled).HasColumnName("SHENABLED");
            Property(m => m.ShFax).HasColumnName("SHFAX");
            Property(m => m.ShId).HasColumnName("SHID");
            Property(m => m.ShInt1).HasColumnName("SHINT1");
            Property(m => m.ShInt2).HasColumnName("SHINT2");
            Property(m => m.ShInt3).HasColumnName("SHINT3");
            Property(m => m.ShInvExpSetting).HasColumnName("SHINVEXPSETTING");
            Property(m => m.ShIsWebShop).HasColumnName("SHISWEBSHOP");
            Property(m => m.ShLastUpdateTime).HasColumnName("SHLASTUPDATETIME");
            Property(m => m.ShLastUpdateUser).HasColumnName("SHLASTUPDATEUSER");
            Property(m => m.ShLevel).HasColumnName("SHLEVEL");
            Property(m => m.ShLongText1).HasColumnName("SHLONGTEXT1");
            Property(m => m.ShLongText2).HasColumnName("SHLONGTEXT2");
            Property(m => m.ShLongText3).HasColumnName("SHLONGTEXT3");
            Property(m => m.ShOuterCode).HasColumnName("SHOUTERCODE");
            Property(m => m.ShOwnerName).HasColumnName("SHOWNERNAME");
            Property(m => m.ShOwnerNick).HasColumnName("SHOWNERNICK");
            Property(m => m.ShPhone).HasColumnName("SHPHONE");
            Property(m => m.ShPostCode).HasColumnName("SHPOSTCODE");
            Property(m => m.ShProvince).HasColumnName("SHPROVINCE");
            Property(m => m.ShRelateCustomer).HasColumnName("SHRELATECUSTOMER");
            Property(m => m.ShShortcutCode).HasColumnName("SHSHORTCUTCODE");
            Property(m => m.ShText1).HasColumnName("SHTEXT1");
            Property(m => m.ShText10).HasColumnName("SHTEXT10");
            Property(m => m.ShText2).HasColumnName("SHTEXT2");
            Property(m => m.ShText3).HasColumnName("SHTEXT3");
            Property(m => m.ShText4).HasColumnName("SHTEXT4");
            Property(m => m.ShText5).HasColumnName("SHTEXT5");
            Property(m => m.ShText6).HasColumnName("SHTEXT6");
            Property(m => m.ShText7).HasColumnName("SHTEXT7");
            Property(m => m.ShText8).HasColumnName("SHTEXT8");
            Property(m => m.ShText9).HasColumnName("SHTEXT9");
            Property(m => m.ShType).HasColumnName("SHTYPE");
            Property(m => m.ShWebShopAppKey).HasColumnName("SHWEBSHOPAPPKEY");
            Property(m => m.ShWebShopAppSecret).HasColumnName("SHWEBSHOPAPPSECRET");
            Property(m => m.ShWebShopId).HasColumnName("SHWEBSHOPID");
            Property(m => m.ShWebShopOtherParam).HasColumnName("SHWEBSHOPOTHERPARAM");
            Property(m => m.ShWebShopPlatformType).HasColumnName("SHWEBSHOPPLATFORMTYPE");
            Property(m => m.ShWebShopSessionKey).HasColumnName("SHWEBSHOPSESSIONKEY");
            Property(m => m.ShWebShopSessionTag).HasColumnName("SHWEBSHOPSESSIONTAG");
            Property(m => m.ShWebShopType).HasColumnName("SHWEBSHOPTYPE");
            Property(m => m.ShWebShopUrl).HasColumnName("SHWEBSHOPURL");
            Property(m => m.ShWebShopUserIdentity).HasColumnName("SHWEBSHOPUSERIDENTITY");


            //Property(m => m.ShId)..IsRequired();
        }
    }
}
