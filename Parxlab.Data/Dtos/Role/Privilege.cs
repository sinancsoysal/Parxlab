
namespace Parxlab.Data.Dtos.Role
{
    /// <summary>
    /// kullanıcı erişim seviyeleri
    /// </summary>
    public record Privilege
    {
        /// <summary>
        /// şirket erişim seviyeleri
        /// </summary>
        public bool Company { get; init; }
        /// <summary>
        /// fatura erişim seviyeleri
        /// </summary>
        public bool Invoice { get; init; }
        /// <summary>
        /// gider kalemi erişim seviyeleri
        /// </summary>
        public bool ExpenseItem { get; init; }
        /// <summary>
        /// rapor erişim seviyeleri
        /// </summary>
        public bool Report { get; init; }
        /// <summary>
        /// Arama erişim seviyeleri
        /// </summary>
        public bool Query { get; init; }
        /// <summary>
        /// ayarlar erişim seviyeleri
        /// </summary>
        public bool Settings { get; init; }
        /// <summary>
        /// excel dosya erişim seviyeleri
        /// </summary>
        public bool ExcelImport { get; init; }
    }
}
