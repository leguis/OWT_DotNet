using Contact.Data.Models;
using Contact.Data.ViewModels;
using Contact.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private ContactService _contactService;

    public ContactController(DataContext context) {
        _contactService = new ContactService(context);
    }

    [HttpGet]
    public async Task<ActionResult<List<ContactVM>>> GetContacts() {
        var contacts = await _contactService.GetContacts();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactVM>> GetContact(int id) {
        var contact = await _contactService.GetContact(id);

        if (contact == null)
            return NotFound("No contact with this id");
        contact.Decrypt();
        return Ok(contact);
    }

    [HttpPost]
    public async Task<ActionResult<ContactVM>> PostContact(ContactVM request) {
        if (!request.IsValid())
            return BadRequest("Body not valid");

        request.Encrypt();
        request = await _contactService.AddContact(request);
        request.Decrypt();

        return Ok(request);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ContactVM>> UpdateContact(int id, ContactVM request) {
        if (!request.IsValid())
            return BadRequest("Body not valid");

        var contact = await _contactService.PutContact(id, request);

        if (contact == null)
            return NotFound("No contact with this id");

        return Ok(contact);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ContactVM>> RemoveContact(int id) {
        var contact = await _contactService.DeleteContact(id);

        if (contact == null)
            return NotFound("No contact with this id");

        return Ok(contact);
    }
}