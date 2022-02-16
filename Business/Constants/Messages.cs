

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace Business.Constants
{
    public static class Messages
    {//static yapmamın sebebi her seferinde new'lememek için
        //Message lar sabit olduğu için buradan static kullanıyorum.

        public static string ProductAdded = "Ürün başarıyla eklendi.";
        public static string ProductDeleted = "Ürün başarıyla silindi.";
        public static string ProductUpdated = "Ürün başarıyla güncellendi.";


        public static string CategoryAdded = "Kategori başarıyla eklendi.";
        public static string CategoryDeleted = "Kategori başarıyla silindi.";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi.";

        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre Hatalı.";
        public static string SuccessfulLogin="Sisteme Giriş başarılı.";
        public static string UserAlreadyExits = "Bu kullanıcı zaten mevcut.";
        public static string UserRegisterd = "Kullanıcı başarıyla kaydedildi.";
        public static string AccessTokenCreated ="Access token başarıyla oluşturuldu." ;


        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut.";
    }
}
