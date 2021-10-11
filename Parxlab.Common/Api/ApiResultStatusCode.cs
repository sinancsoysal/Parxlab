using System.ComponentModel.DataAnnotations;

namespace Parxlab.Common.Api
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "görev tamamlandı")]
        Success = 0,

        [Display(Name = "sunucuda bir hata oluştu")]
        ServerError = 1,

        [Display(Name = "Gönderilen parametreler geçerli değil")]
        BadRequest = 2,

        [Display(Name = "bulunamadı")]
        NotFound = 3,

        [Display(Name = "liste boş")]
        ListEmpty = 4,

        [Display(Name = "işlenirken bir hata oluştu")]
        LogicError = 5,

        [Display(Name = "Doğrulama hatası")]
        UnAuthorized = 6,

        [Display(Name = "Erişim izni verilmedi")]
        Forbidden=7,

        [Display(Name = "Veriler diğer verilerle çelişiyor")]
        Conflict =8,

        Gone=9
    }
}
