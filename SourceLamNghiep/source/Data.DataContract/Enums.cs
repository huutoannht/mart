using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public enum TableLog : short
    {
        [EnumMember]
        Customer = 5
    }

    [Serializable]
    [DataContract]
    public enum ActionLog : short
    {
        [Description("ActionLog_Insert")]
        [EnumMember]
        Insert = 5,

        [Description("ActionLog_Update")]
        [EnumMember]
        Update = 10,

        [Description("ActionLog_Delete")]
        [EnumMember]
        Delete = 20,

        [Description("ActionLog_ChangeType")]
        [EnumMember]
        ChangeType = 30,

        [Description("ActionLog_InsertImage")]
        [EnumMember]
        InsertImage = 40,

        [Description("ActionLog_DeleteImage")]
        [EnumMember]
        DeleteImage = 50,
    }

    [Serializable]
    [DataContract]
    public enum WeekDay : short
    {
        [Description("WeekDay_Sunday")]
        [EnumMember]
        Sunday = 5,

        [Description("WeekDay_Monday")]
        [EnumMember]
        Monday = 10,

        [Description("WeekDay_Tuesday")]
        [EnumMember]
        Tuesday = 15,

        [Description("WeekDay_Wednesday")]
        [EnumMember]
        Wednesday = 20,

        [Description("WeekDay_Thursday")]
        [EnumMember]
        Thursday = 30,

        [Description("WeekDay_Friday")]
        [EnumMember]
        Friday = 40,

        [Description("WeekDay_Saturday")]
        [EnumMember]
        Saturday = 50
    }

    [Serializable]
    [DataContract]
    public enum BeUserStatus : short
    {
        [Description("BeUserStatus_Active")]
        [EnumMember]
        Active = 5,

        [Description("BeUserStatus_Locked")]
        [EnumMember]
        Locked = 10
    }

    [Serializable]
    [DataContract]
    public enum CustomerType : short
    {
        [Description("CustomerType_Potential")]
        [EnumMember]
        Potential = 10,

        [Description("CustomerType_Prospects")]
        [EnumMember]
        Prospects = 20,

        [Description("CustomerType_Current")]
        [EnumMember]
        Current = 30
    }

    public enum FrontEndHomeViewType
    {
        Default,
        TinDang
    }

    [Serializable]
    [DataContract]
    public enum ProductStatus : short
    {
        [Description("ProductStatus_Active")]
        [EnumMember]
        Active = 10,

        [Description("ProductStatus_Inactive")]
        [EnumMember]
        Inactive = 20
    }

    [Serializable]
    [DataContract]
    public enum CategoryType : short
    {
        [EnumMember]
        Product = 5,

        [EnumMember]
        UOM = 10,

        [EnumMember]
        NumberOfStaff = 15,

        [EnumMember]
        NumberOfDentist = 20,

        [EnumMember]
        Specialization = 30,

        [EnumMember]
        NumberOfChair = 40,

        [EnumMember]
        Age = 50,

        [EnumMember]
        LeadTime = 60,

        [EnumMember]
        UsingRC = 70,

        [EnumMember]
        Position = 80,
    }

    [Serializable]
    public enum ChatCommandType : short
    {
        AdminJoinGroup = 10,
        ClientJoinGroup = 20
    }

    [Serializable]
    [DataContract]
    public enum PaymentMethod : short
    {
        [Description("PaymentMethod_Cash")]
        [EnumMember]
        Cash = 10,

        [Description("PaymentMethod_CreditCard")]
        [EnumMember]
        CreditCard = 20
    }

    public enum ExternalType : short
    {
        Facebook = 10,
        Google = 20
    }

    [Serializable]
    [DataContract]
    public enum PaymentStatus : short
    {
        [Description("PaymentStatus_ChuaThanhToan")]
        [EnumMember]
        ChuaThanhToan = 10,

        [Description("PaymentStatus_DaThanhToan")]
        [EnumMember]
        DaThanhToan = 20,

        [Description("PaymentStatus_ThanhToanKhongThanhCong")]
        [EnumMember]
        ThanhToanKhongThanhCong = 30
    }

    public enum SessionKey : short
    {
        CurrentUserId,

        SESSION_PATH_KEY,

        FilterHistory,

        CurrentTimeZone
    }

    public enum SearchType : short
    {
        [Description("SearchType_Products")]
        Products = 10
    }

    [Serializable]
    [DataContract]
    public enum Gender : short
    {
        [Description("Gender_Nam")]
        [EnumMember]
        Nam = 5,

        [Description("Gender_Nu")]
        [EnumMember]
        Nu = 10
    }
  
    public enum VietNamProvine : short
    {
        [Description("An Giang")]
        [EnumMember]
        AnGiang = 5,

        [Description("Bà Rịa - Vũng Tàu")]
        [EnumMember]
        BaRiaVungTau = 10,

        [Description("Bắc Giang")]
        [EnumMember]
        BcGiang = 15,

        [Description("Bắc Kạn")]
        [EnumMember]
        BacKan = 20,

        [Description("Bạc Liêu")]
        [EnumMember]
        BacLieu = 25,

        [Description("Bắc Ninh")]
        [EnumMember]
        BacNinh = 30,

        [Description("Bến Tre")]
        [EnumMember]
        BenTre = 35,

        [Description("Bình Định")]
        [EnumMember]
        BinhDinh = 40,

        [Description("Bình Dương")]
        [EnumMember]
        BinhDuong = 45,

        [Description("Bình Phước")]
        [EnumMember]
        BinhPhuoc = 50,

        [Description("Bình Thuận")]
        [EnumMember]
        BinhThuan = 55,

        [Description("Cà Mau")]
        [EnumMember]
        CaMau = 60,

        [Description("Cao Bằng")]
        [EnumMember]
        CaoBang = 65,

        [Description("Đắk Lắk")]
        [EnumMember]
        DakLak = 70,

        [Description("Đắk Nông")]
        [EnumMember]
        DakNong = 75,

        [Description("Điện Biên")]
        [EnumMember]
        DienBien = 80,

        [Description("Đồng Nai")]
        [EnumMember]
        DongNai = 85,

        [Description("Đồng Tháp")]
        [EnumMember]
        DongThap = 90,

        [Description("Gia Lai")]
        [EnumMember]
        GiaLai = 95,

        [Description("Hà Giang")]
        [EnumMember]
        HaGiang = 100,

        [Description("Hà Nam")]
        [EnumMember]
        HaNam = 105,

        [Description("Hà Tĩnh")]
        [EnumMember]
        HaTinh = 110,

        [Description("Hải Dương")]
        [EnumMember]
        HaiDuong = 115,

        [Description("Hậu Giang")]
        [EnumMember]
        HauGiang = 120,

        [Description("Hòa Bình")]
        [EnumMember]
        HoaBinh = 125,

        [Description("Hưng Yên")]
        [EnumMember]
        HungYen = 130,

        [Description("Khánh Hòa")]
        [EnumMember]
        KhanhHoa = 135,

        [Description("Kiên Giang")]
        [EnumMember]
        KienGiang = 140,

        [Description("Kon Tum")]
        [EnumMember]
        KonTum = 145,

        [Description("Lai Châu")]
        [EnumMember]
        LaiChau = 150,

        [Description("Lâm Đồng")]
        [EnumMember]
        LamDong = 155,

        [Description("Lạng Sơn")]
        [EnumMember]
        LangSon = 160,

        [Description("Lào Cai")]
        [EnumMember]
        LaoCai = 165,

        [Description("Long An")]
        [EnumMember]
        LongAn = 170,

        [Description("Nam Định")]
        [EnumMember]
        NamDinh = 175,

        [Description("Nghệ An")]
        [EnumMember]
        NgheAn = 180,

        [Description("Ninh Bình")]
        [EnumMember]
        NinhBinh = 185,

        [Description("Ninh Thuận")]
        [EnumMember]
        NinhThuan = 190,

        [Description("Phú Thọ")]
        [EnumMember]
        PhuTho = 195,

        [Description("Quảng Bình")]
        [EnumMember]
        QuangBinh = 200,

        [Description("Quảng Nam")]
        [EnumMember]
        QuangNam = 205,

        [Description("Quảng Ngãi")]
        [EnumMember]
        QuangNgai = 210,

        [Description("Quảng Ninh")]
        [EnumMember]
        QuangNinh = 215,

        [Description("Quảng Trị")]
        [EnumMember]
        QuangTri = 220,

        [Description("Sóc Trăng")]
        [EnumMember]
        SocTrang = 225,

        [Description("Sơn La")]
        [EnumMember]
        SonLa = 230,

        [Description("Tây Ninh")]
        [EnumMember]
        TayNinh = 235,

        [Description("Thái Bình")]
        [EnumMember]
        ThaiBinh = 240,

        [Description("Thái Nguyên")]
        [EnumMember]
        ThaiNguyen = 245,

        [Description("Thanh Hóa")]
        [EnumMember]
        ThanhHoa = 250,

        [Description("Thừa Thiên Huế")]
        [EnumMember]
        ThuaThienHue = 255,

        [Description("Tiền Giang")]
        [EnumMember]
        TienGiang = 260,

        [Description("Trà Vinh")]
        [EnumMember]
        TraVinh = 270,

        [Description("Tuyên Quang")]
        [EnumMember]
        TuyenQuang = 275,

        [Description("Vĩnh Long")]
        [EnumMember]
        VinhLong = 280,

        [Description("Vĩnh Phúc")]
        [EnumMember]
        VinhPhuc = 285,

        [Description("Yên Bái")]
        [EnumMember]
        YenBai = 290,

        [Description("Phú Yên")]
        [EnumMember]
        PhuYen = 295,

        [Description("Cần Thơ")]
        [EnumMember]
        CanTho = 300,

        [Description("Đà Nẵng")]
        [EnumMember]
        DaNang = 305,

        [Description("Hải Phòng")]
        [EnumMember]
        HaiPhong = 310,

        [Description("Hà Nội")]
        [EnumMember]
        HaNoi = 315,

        [Description("TP HCM")]
        [EnumMember]
        TPHCM = 320,

        [Description("Toàn Quốc")]
        [EnumMember]
        ToanQuoc = 325
    }

    public enum DataChildFolder : short
    {
        Images,
        Files
    }

    [Serializable]
    [DataContract]
    public enum StoreFrontPageSection : short
    {
        
    }

    [Serializable]
    [DataContract]
    public enum CreditCardType
    {
        [EnumMember]
        [Description("Invalid")]
        Invalid,

        [EnumMember]
        [Description("")]
        Unknown,

        [EnumMember]
        [Description("Visa")]
        Visa,

        [EnumMember]
        [Description("Master Card")]
        MasterCard,

        [EnumMember]
        [Description("Discover")]
        Discover,

        [EnumMember]
        [Description("American Express")]
        AmericanExpress,

        [EnumMember]
        [Description("JCB")]
        JCB,

        [EnumMember]
        [Description("Diners Club")]
        DinersClub,

        [EnumMember]
        [Description("Dankort")]
        Dankort,

        [EnumMember]
        [Description("Electron")]
        Electron,

        [EnumMember]
        [Description("Maestro")]
        Maestro,

        [EnumMember]
        [Description("Interpayment")]
        Interpayment,

        [EnumMember]
        [Description("Unionpay")]
        Unionpay,

        [EnumMember]
        [Description("Paymentech")]
        Paymentech,

        [EnumMember]
        [Description("Australian BankCard")]
        AustralianBankCard
    }

    [Serializable]
    [DataContract]
    public enum HtmlContentDisplayType : short
    {
        [Description("HtmlContentDisplayType_WebSite")]//res key
        [EnumMember]
        WebSite = 5,

        [Description("HtmlContentDisplayType_SMS")]
        [EnumMember]
        SMS = 10,

        [Description("HtmlContentDisplayType_Email")]
        [EnumMember]
        Email = 15
    }

    public enum SiteType : short
    {
        SuperSite = 5,
        StoreSite = 10
    }

    [Serializable]
    [DataContract]
    public enum MessageCase : short
    {
        [Description("MessageCase_ForgotPasswordEmailSent")]//Resouce key, must be localization
        [EnumMember]
        ForgotPasswordEmailSent = 5,

        [Description("MessageCase_ExternalLoginIdConfirm")]
        [EnumMember]
        ExternalLoginIdConfirm = 10,

        [Description("MessageCase_DatHangThanhCong")]
        [EnumMember]
        DatHangThanhCong = 20,

        [Description("MessageCase_HuyBoThanhToan")]
        [EnumMember]
        HuyBoThanhToan = 30
    }

    [Serializable]
    [DataContract]
    public enum MaritalStatus : short
    {
        [Description("MaritalStatus_Single")]
        [EnumMember]
        Single = 5,

        [Description("MaritalStatus_Married")]
        [EnumMember]
        Married = 10,

        [Description("MaritalStatus_Divorced")]
        [EnumMember]
        Divorced = 15
    }

    [Serializable]
    [DataContract]
    public enum NotifyType : short
    {
        NotSet = 10,
        Error = 20,
        Success = 30,
        Warning = 40,
        Information = 50
    }

    [Serializable]
    [DataContract]
    public enum YesNo : short
    {
        [Description("Yes")]
        [EnumMember]
        Yes = 5,

        [Description("No")]
        [EnumMember]
        No = 10
    }

    [Serializable]
    [DataContract]
    public enum SortDirection : short
    {
        [EnumMember]
        None = 5,

        [EnumMember]
        Asc = 10,

        [EnumMember]
        Desc = 15
    }

    [Serializable]
    [DataContract]
    public enum BeUserType : short
    {
        [Description("SuperAdmin")]//Resouce key, must be localization
        [EnumMember]
        Super = 5,

        [Description("Admin")]
        [EnumMember]
        Admin = 10
    }

    [DataContract]
    [Serializable]
    public enum BePage : short
    {
        [Description("BackendUsers")]//Resouce key, must be localization
        [EnumMember]
        BackendUsers = 5,

        [Description("Appsettings")]
        [EnumMember]
        Appsettings = 25,

        [Description("SystemEmailTemplates")]
        [EnumMember]
        SystemEmailTemplates = 30,

        [Description("Roles")]
        [EnumMember]
        Roles = 35,

        [Description("Home")]
        [EnumMember]
        Home = 40,

        [Description("SiteSettings")]
        [EnumMember]
        SiteSettings = 45,

        [Description("Profile")]
        [EnumMember]
        Profile = 55,

        [Description("BePage_MessageSetup")]
        [EnumMember]
        MessageSetup = 65,

        [Description("BePage_Customer")]
        Customer = 75,

        [Description("BePage_SanPham")]
        SanPham = 90,

        [Description("BePage_NhomSanPham")]
        NhomSanPham = 110,

        [Description("BePage_GuiMail")]
        GuiMail = 230,

        [Description("BePage_Logo")]
        Logo = 240,

        [Description("BePage_Files")]
        Files = 250,

        [Description("BePage_News")]
        News = 260,

        [Description("BePage_UOM")]
        UOM = 270,

        [Description("BePage_NumberOfStaffSetting")]
        NumberOfStaffSetting = 280,

        [Description("BePage_NumberOfDoctorSetting")]
        NumberOfDoctorSetting = 290,

        [Description("BePage_SpecializationSetting")]
        SpecializationSetting = 300,

        [Description("BePage_NumberOfChairSetting")]
        NumberOfChairSetting = 310,

        [Description("BePage_AgeSetting")]
        AgeSetting = 320,

        [Description("BePage_LeadTimeSetting")]
        LeadTimeSetting = 330,

        [Description("BePage_UsingRCSetting")]
        UsingRCSetting = 340,

        [Description("BePage_PositionSetting")]
        PositionSetting = 350,

        [Description("BePage_PotentialCustomer")]
        PotentialCustomer = 360,

        [Description("BePage_ProspectsCustomer")]
        ProspectsCustomer = 370,

        [Description("BePage_CustomerLog")]
        CustomerLog = 380,
    }

    [Serializable]
    [DataContract]
    public enum ArticeStatus : short
    {
        [Description("ArticeStatus_Visible")]
        [EnumMember]
        Visible = 5,

        [Description("ArticeStatus_Hidden")]
        [EnumMember]
        Hidden = 10
    }

    [DataContract]
    [Serializable]
    public enum RoleFeatureAction : short
    {
        [EnumMember]
        View = 5,

        [EnumMember]
        Modify = 10,

        [EnumMember]
        Delete = 15,
    }

    [Serializable]
    [DataContract]
    public enum AppSettingType : short
    {
        [EnumMember]
        [Description("AppSettingType_Smtp")]//Resouce key, must be localization
        Smtp = 5,

        [EnumMember]
        [Description("AppSettingType_State")]
        State = 15,

        [EnumMember]
        [Description("AppSettingType_EmailSettings")]
        EmailSettings = 20,

        [EnumMember]
        [Description("AppSettingType_SiteStyleSettings")]
        SiteStyleSettings = 35,

        [EnumMember]
        [Description("AppSettingType_SiteData")]
        SiteData = 50,

        [EnumMember]
        [Description("AppSettingType_SmsVendorSetting")]
        SmsVendorSetting = 55,

        [EnumMember]
        [Description("AppSettingType_CompanyInfo")]
        CompanyInfo = 60,
    }

    [Serializable]
    [DataContract]
    public enum SystemEmailTemplateType : short
    {
        [EnumMember]
        [Description("SystemEmailTemplateType_BackEndForgotPassword")]//Resouce key, must be localization
        BeForgotPassword = 5,

        [EnumMember]
        [Description("SystemEmailTemplateType_StoreFrontContactUs")]
        StoreFrontContactUs = 10,

        [EnumMember]
        [Description("SystemEmailTemplateType_ExternalLoginIdConfirm")]
        ExternalLoginIdConfirm = 20,

        [EnumMember]
        [Description("SystemEmailTemplateType_HasNewOrder")]
        HasNewOrder = 30,

        [EnumMember]
        [Description("SystemEmailTemplateType_ConfirmNewAccountToUser")]
        ConfirmNewAccountToUser = 40
    }
}
