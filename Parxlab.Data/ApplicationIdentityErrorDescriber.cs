using Microsoft.AspNetCore.Identity;

namespace Parxlab.Data
{
    public class ApplicationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) => new() { Code = nameof(DuplicateUserName), Description = $"'{userName}' kullanıcı adı zaten başka biri tarafından seçilmiş." };

        public override IdentityError PasswordRequiresDigit() => new() { Code = nameof(PasswordRequiresDigit), Description = "Şifre en az bir sayı (0-9) içermelidir." };

        public override IdentityError PasswordRequiresLower() => new() { Code = nameof(PasswordRequiresLower), Description = "Parola en az bir küçük harf (a-z) içermelidir." };

        public override IdentityError PasswordRequiresUpper() => new() { Code = nameof(PasswordRequiresUpper), Description = "Şifre en az bir büyük harf (A-Z) içermelidir." };

        public override IdentityError PasswordTooShort(int length) => new() { Code = nameof(PasswordTooShort), Description = $"Şifre en az {length} karakter içermelidir." };

        public override IdentityError InvalidUserName(string userName) => new() { Code = nameof(InvalidUserName), Description = "Kullanıcı adı (0-9) ve (a-z) karakterlerini içermelidir." };

        public override IdentityError InvalidEmail(string email)=> new() { Code = nameof(InvalidEmail), Description = "Girilen e-posta yanlış" };

        public override IdentityError DuplicateEmail(string email) => new() { Code = nameof(DuplicateEmail), Description = $"'{email}' ile zaten kayıtlısınız." };

        public override IdentityError DuplicateRoleName(string role) => new() { Code = nameof(DuplicateRoleName), Description = $"'{role}' yineleniyor." };

        public override IdentityError PasswordMismatch() => new() { Code = nameof(PasswordMismatch), Description = "Şifreniz yanlış" };

    }
}
