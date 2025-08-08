using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.TicketCategories;
public record GetTicketCategoriesQuery() : IRequest<List<TicketCategory>>;
