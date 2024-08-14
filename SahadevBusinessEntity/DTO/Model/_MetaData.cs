using System;

namespace SahadevBusinessEntity.DTO.Model
{

    public class _MetaData
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class _MetaDataActive : _MetaData
    {
        public bool IsActive { get; set; }
    }
}
