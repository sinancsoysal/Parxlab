using System;

namespace Parxlab.Data.Dtos.Role
{
   public class RoleClaimDto
    {
        public Guid Id { get; set; }
        public string Name { get; init; }
        public bool Company { get; init; }
        public bool Invoice { get; init; }
        public bool ExpenseItem { get; init; }
        public bool Report { get; init; }
        public bool Query { get; init; }
        public bool Settings { get; init; }
        public bool ExcelImport { get; init; }
    }
}
