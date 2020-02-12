using AS.Oil.BLL.Enums;
using AS.Oil.BLL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.Oil.BLL.Models
{
    public class QueueItem
    {
        public OperationType Type { get; set; }

        public StorageDto Storage { get; set; }
    }
}
