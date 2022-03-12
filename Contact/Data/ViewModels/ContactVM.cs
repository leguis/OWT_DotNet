using System.Text.RegularExpressions;
using Contact.Data.Models;

namespace Contact.Data.ViewModels;

public class ContactVM
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
    public List<string?> SkillRelated { get; set; }
    public ContactVM() {}
    public ContactVM(Contacts contact) : base() {
        FirstName = contact.FirstName;
        LastName = contact.LastName;
        FullName = contact.FullName;
        Address = contact.Address;
        Email = contact.Email;
        MobilePhone = contact.MobilePhone;
        SkillRelated = new List<string?>();
        if (contact.Contacts_Skills != null) {
            foreach (var skill in contact.Contacts_Skills)
            {
                SkillRelated.Add(String.Format("{0}: {1}", skill.Skill.Name, skill.Skill.Level));
            }
        }
    }
    public void Encrypt() {
        FirstName = Encryption.Encrypt(FirstName);
        LastName = Encryption.Encrypt(LastName);
        FullName = Encryption.Encrypt(FullName);
        Address = Encryption.Encrypt(Address);
        Email = Encryption.Encrypt(Email);
        MobilePhone = Encryption.Encrypt(MobilePhone);
    }

    public void Decrypt() {
        FirstName = Encryption.Decrypt(FirstName);
        LastName = Encryption.Decrypt(LastName);
        FullName = Encryption.Decrypt(FullName);
        Address = Encryption.Decrypt(Address);
        Email = Encryption.Decrypt(Email);
        MobilePhone = Encryption.Decrypt(MobilePhone);
    }

    // TODO Validation for Address with Google maps or other
    public bool IsValid() {
        if (FirstName == null || LastName == null || FullName == null || Address == null || Email == null || MobilePhone == null)
            return false;

        Regex regexEmail = new Regex(@".+\@.+\..+");
        Match matchEmail = regexEmail.Match(Email);
        Regex regexPhone = new Regex(@"^([+]?\d{1,2}[-\s]?|)\d{3}[-\s]?\d{3}[-\s]?\d{4}$");
        Match matchPhone = regexPhone.Match(MobilePhone);

        if (matchEmail.Success && matchPhone.Success )
            return true;
        else
            return false;
    }
}