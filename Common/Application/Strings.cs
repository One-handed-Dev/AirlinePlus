namespace Common.Application
{
    public static class Strings
    {
        public static class Symbol
        {
            public const string IdSeparator = ";";
            public const string TagSeparator = "^";
            public const string OrderSymbol = "Ord-";
        }

        public static class Persian
        {
            public static class Payment
            {
                public const string SucceededPurchase = "پرداخت با موفقیت صورت پذیرفت. از خرید شما متشکریم.";
                public const string FailedPurchase = "پرداخت شما موفقیت آمیز نبود. شما می توانید مجددا اقدام به پرداخت کنید.";
            }

            public static class Discount
            {
                public const string Started = "شروع شده";
                public const string Expired = "منقضی شده";
                public const string InUse = "درحال استفاده";
                public const string NotStarted = "شروع نشده";
            }

            public sealed class ValidationMessages
            {
                public const string IsRequired = "این بخش نمی تواند خالی باشد";
                public const string InvalidFileFormat = "قالب فایل اشتباه است";
                public const string BadFileSize = "حجم فایل بیشتر از حدمجاز است";
                public const string InvalidInputFormat = "قالب اطلاعات اشتباه است";
                public const string InvalidLength = "ورودی با طول مناسب برگزینید";
            }

            public sealed class PermissionDisplayNames
            {
                public const string List = "دسترسی فهرست";
                public const string Create = "دسترسی ایجاد";
                public const string Edit = "دسترسی ویرایش";
                public const string Make = "دسترسی ساخت";
                public const string Search = "دسترسی جستجو";
                public const string Remove = "دسترسی حذف";
                public const string Restore = "دسترسی فعال سازی";
                public const string ChangePassword = "دسترسی تغییررمز";
                public const string ChangeRole = "دسترسی تغییر نقش";
                public const string Cancel = "دسترسی رد";
                public const string Confirm = "دسترسی تایید";
                public const string Increase = "دسترسی افزودن";
                public const string Decrease = "دسترسی کاهیدن";
                public const string Log = "دسترسی سوابق";
                public const string Answer = "دسترسی جوابدهی";
                public const string Send = "دسترسی ارسال";
                public const string Reset = "دسترسی بازنشانی";
                public const string Step = "دسترسی پیشروی";
                public const string Ban = "دسترسی مسدودسازی";
                public const string Unban = "دسترسی نامسدودسازی";

                public const string Account = "حساب کاربری (Account)";
                public const string Role = "نقش (Role)";
                public const string ForgetPasswordLog = "فراموشی رمز عبور (ForgetPasswordLog)";
                public const string AccountPolicy = "سیاست‌های کاربران (AccountPolicy)";

                public const string Shop = "هتل (Shop)";
                public const string Room = "اتاق (Room)";
                public const string ShopPicture = "عکس هتل (ShopPicture)";

                public const string Email = "ایمیل (Email)";
                public const string Comment = "نظرات (Comment)";
                public const string Order = "سفارش (Order)";
            }
        }

        public static class English
        {
            public static class Host
            {
                public const string CartCookieName = "cart-items";
                public const string DefaultSession = "default-session";
            }
            
            public static class Query
            {
                public const string NoPictureProfileFilePath = "no-picture";
                public const string NoPictureProductFilePath = "no-picture.png";
            }
        }
    }
}
