using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// ClientServicingTeam
    /// </summary>
    public class ClientServicingTeam
    {
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public bool IsPOC { get; set; }

    }
}
