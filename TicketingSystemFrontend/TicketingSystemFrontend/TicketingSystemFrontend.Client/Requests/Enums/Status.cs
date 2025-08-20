using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemFrontend.Client.Requests.Enums;

public enum Status
{
    Open,
    [Display(Name = "In Progress")]
    InProgress,
    Resolved,
    Closed,
}