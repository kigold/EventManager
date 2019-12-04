using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EventManagement.Core.ViewModel
{

    public class ResponseViewModel
    {
        public ResponseViewModel(IEnumerable<ValidationResult> validations)
        {
            Validations = validations.ToArray();
        }
        public ValidationResult[] Validations { get; set; }
    }

    public class ResponseViewModel<T> : ResponseViewModel
    {
        public ResponseViewModel(IEnumerable<ValidationResult> validations, T payload, int totalCount = 0) : base(validations)
        {
            Validations = validations.ToArray();
            Payload = payload;
            TotalCount = totalCount;
            
        }
        public T Payload { get; set; }
        public int TotalCount { get; set; }
    }
}
