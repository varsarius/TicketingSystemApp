using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Api.Models;
using TicketingSystemBackend.Application.DTOs;

namespace TicketingSystemBackend.Api.Controllers;


[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] TicketCreateRequest request)
    {
        Console.WriteLine("New ticket received:");
        Console.WriteLine($"Title: {request.Title}");
        Console.WriteLine($"Description: {request.Description}");
        Console.WriteLine($"Priority: {request.Priority}");
        Console.WriteLine($"AssignedTo: {request.AssignedTo}");

        return Ok();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var fakeTickets = new List<TicketDto>
        {
            new(1, "Login Issue", "User cannot login with correct credentials.", "High", "Authentication", DateTime.Now.AddHours(-5)),
            new(2, "Page crash on /tickets", "Page crashes when clicking on 'Create New Ticket'.", "Medium", "Frontend", DateTime.Now.AddDays(-1)),
            new(3, "Pagessssssssssssssssssssssssssssssssssssssssssssssssss crash on /tickets", "Page crashes when clicking on 'Create New Ticket'.", "Mediuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuum", "Frrrrrrrrrrrrrrrrrrrrrrrrrrontend", DateTime.Now.AddDays(10))

        };

        return Ok(fakeTickets);
    }
}
