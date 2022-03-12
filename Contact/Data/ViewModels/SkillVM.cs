using Contact.Data.Models;

namespace Contact.Data.ViewModels;

public class SkillVM
{
    public string? Name { get; set; }
    public short Level { get; set; }
    public List<string?> ContactRelatedName { get; set; }
    public SkillVM() {}
    public SkillVM(Skills skill) : base()
    {
        Name = skill.Name;
        Level = skill.Level;
        ContactRelatedName = new List<string?>();
        if (skill.Contacts_Skills != null) {
            foreach (var contact in skill.Contacts_Skills)
            {
                ContactRelatedName.Add(contact.Contact.FullName);
            }
        }
    }
}