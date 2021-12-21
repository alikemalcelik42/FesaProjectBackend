using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kullanıcı kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Kullanıcı mevcud";
        public static string AccessTokenCreated = "Bağlantı jetonu oluşturuldu";
        public static string UserUpdated = "Kullanıcı güncellendi";

        public static string WriteAdded = "Yazı eklendi";
        public static string WritesListed = "Yazılar listelendi";
        public static string WriteUpdated = "Yazı güncellendi";
        public static string WriteDeleted = "Yazı silindi";

        public static string FileCreated = "Dosya oluşturuldu";
        public static string FileDeleted = "Dosya silindi";
    }
}
