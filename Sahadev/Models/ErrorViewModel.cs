using System;

namespace SampleMVCCoreProject.Models
{
    /// <summary>
    /// ErrorViewModel
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// RequestId
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// ShowRequestId
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
