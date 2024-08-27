using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.Constant.Messages
{
    /// <summary>
    /// Common Class
    /// </summary>
    public class Common
    {
        /// <summary>
        /// SDCOM001: Something went wrong. Please try again.
        /// </summary>
        public const string ServerError = "SDCOM001: Something went wrong. Please try again.";

        /// <summary>
        /// SDCOM002: Failed to add {0} detail. Please try again.
        /// </summary>
        public const string AddFailed = "SDCOM002: Failed to add {0} detail. Please try again.";

        /// <summary>
        /// SDCOM003: Failed to update {0} detail. Please try again.
        /// </summary>
        public const string UpdateFailed = "SDCOM003: Failed to update {0} detail. Please try again.";

        /// <summary>
        /// SDCOM004: Failed to delete {0} detail. Please try again.
        /// </summary>
        public const string DeleteFailed = "SDCOM004: Failed to delete {0} detail. Please try again.";

        /// <summary>
        /// SDCOM005: Failed to retrieve {0} detail. Please try again.
        /// </summary>
        public const string RetrievalFailed = "SDCOM005: Failed to retrieve {0} detail. Please try again.";

        /// <summary>
        /// {0} added successfully.
        /// </summary>
        public const string Added = "{0} added successfully.";

        /// <summary>
        /// {0} added successfully.
        /// </summary>
        public const string Updated = "{0} updated successfully.";

        /// <summary>
        /// {0} deleted successfully.
        /// </summary>
        public const string Deleted = "{0} deleted successfully.";
    }
}
