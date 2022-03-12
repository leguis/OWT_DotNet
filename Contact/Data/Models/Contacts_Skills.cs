namespace Contact.Data.Models;

public class Contacts_Skills
{
    public int Id { get; set; }
    public int ContactId { get; set; }
    public Contacts Contact { get; set; }
    public int SkillsId { get; set; }
    public Skills Skill { get; set; }
}