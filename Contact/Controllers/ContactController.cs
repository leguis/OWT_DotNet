using Microsoft.AspNetCore.Mvc;

namespace Contact.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly DataContext _context;

    public ContactController(DataContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetContacts() {
        var contacts = await _context.Contacts.ToListAsync();
        foreach (var contact in contacts)
        {
            contact.Decrypt();
        }
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContact(int id) {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
            return NotFound("No contact with this id");
        contact.Decrypt();
        return Ok(contact);
    }

    [HttpPost]
    public async Task<ActionResult<Contact>> PostContact(Contact request) {
        if (!request.IsValid())
            return BadRequest("Body not valid");

        request.Encrypt();
        _context.Contacts.Add(request);
        await _context.SaveChangesAsync();
        request.Decrypt();
        
        return Ok(request);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Contact>> UpdateContact(int id, Contact request) {
        if (!request.IsValid())
            return BadRequest("Body not valid");

        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
            return NotFound("No contact with this id");

        contact.FirstName = request.FirstName;
        contact.LastName = request.LastName;
        contact.Address = request.Address;
        contact.MobilePhone = request.MobilePhone;
        contact.Email = request.Email;
        contact.FullName = request.FullName;
        contact.Encrypt();

        await _context.SaveChangesAsync();
        contact.Decrypt();
        return Ok(contact);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Contact>> RemoveContact(int id) {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
            return NotFound("No contact with this id");
        
        _context.Contacts.Remove(contact);
        contact.Decrypt();
        return Ok(contact);
    }
}