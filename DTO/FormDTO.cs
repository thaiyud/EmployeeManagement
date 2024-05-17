using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.DTO
{
    public class FormDTO
    {
        [BindNever]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string UserId { get; set; }

        [BindNever]
        [SwaggerSchema(ReadOnly = true)]
        public DateTime DateSubmitted { get; set; }
        public DateTime? DayStart { get; set; } 
        public DateTime? DayEnd { get; set; }
        public string Description { get; set; }
        public int FormTypeId { get; set; }
        [BindNever]
        [SwaggerSchema(ReadOnly = true)]
        public ICollection<FileAttachmentDTO> Attachments { get; set; } = new List<FileAttachmentDTO>();
    }
}
