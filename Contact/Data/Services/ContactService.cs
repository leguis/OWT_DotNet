using Contact.Data.Models;
using Contact.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Data.Services;

public class ContactService
{
    private readonly DataContext _context;

    public ContactService(DataContext context) {
        _context = context;
    }

    public async Task<ContactVM> AddContact(ContactVM contactVM) {
        var contact = new Contacts {
            FirstName = contactVM.FirstName,
            LastName = contactVM.LastName,
            FullName = contactVM.FullName,
            Address = contactVM.Address,
            Email = contactVM.Email,
            MobilePhone = contactVM.MobilePhone
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contactVM;
    }

    public async Task<List<ContactVM>> GetContacts() {
        var list_contacts = new List<ContactVM>();
        var contacts = await _context.Contacts.Include(cs => cs.Contacts_Skills).ThenInclude(s => s.Skill).ToListAsync();
        foreach (var contact in contacts)
        {
            var contactVM = new ContactVM(contact);
            contactVM.Decrypt();
            list_contacts.Add(contactVM);
        }
        return list_contacts;
    }

    public async Task<ContactVM> GetContact(int id) {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null)
            return null;
        var contactVM = new ContactVM(contact);
        return contactVM;
    }

    public async Task<ContactVM>  PutContact(int id, ContactVM request) {
        var contact = await _context.Contacts.FindAsync(id);
        
        if (contact == null)
            return null;
        request.Encrypt();
        contact.FirstName = request.FirstName;
        contact.LastName = request.LastName;
        contact.Address = request.Address;
        contact.MobilePhone = request.MobilePhone;
        contact.Email = request.Email;
        contact.FullName = request.FullName;


        await _context.SaveChangesAsync();
        request.Decrypt();
        return request;
    }

    public async Task<ContactVM> DeleteContact(int id) {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
            return null;
        
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        var contactVM = new ContactVM(contact);
        contactVM.Decrypt();
        return contactVM;
    }
}