namespace Contact.Data.Models;
public class Contacts
{
    public int      Id {get; set;}
    public string?  FirstName {get; set;}
    public string?  LastName {get; set;}
    public string?  FullName {get; set;}
    public string?  Address {get; set;}
    public string?  Email {get; set;}
    public string?  MobilePhone {get;set;}
    public List<Contacts_Skills> Contacts_Skills { get; set; }
}