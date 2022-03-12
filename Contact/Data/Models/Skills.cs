namespace Contact.Data.Models;

public class Skills
{
    public int Id {get; set;}
    public string? Name { get; set; }
    public short Level { get; set; }
    public List<Contacts_Skills> Contacts_Skills { get; set; }
}