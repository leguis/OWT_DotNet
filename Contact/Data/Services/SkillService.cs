using Contact.Data.Models;
using Contact.Data.ViewModels;

namespace Contact.Data.Services;

public class SkillServices
{
    private readonly DataContext _context;

    public SkillServices(DataContext context) {
        _context = context;
    }

    public async Task<SkillVM> AddSkill(SkillVM skillVM) {
        var skill = new Skills {
            Name = skillVM.Name,
            Level = skillVM.Level
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skillVM;
    }

    public async Task<List<SkillVM>> GetSkills() {
        var list_skills = new List<SkillVM>();
        var skills = await _context.Skills.ToListAsync();
        foreach (var skill in skills)
        {
            var skillVM = new SkillVM(skill);
            list_skills.Add(skillVM);
        }
        return list_skills;
    }

    public async Task<SkillVM> GetSkill(int id) {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
            return null;
        var skillVM = new SkillVM(skill);
        return skillVM;
    }

    public async Task<SkillVM>  PutSkill(int id, SkillVM request) {
        var skill = await _context.Skills.FindAsync(id);
        
        if (skill == null)
            return null;
        skill.Name = request.Name;
        skill.Level = request.Level;

        await _context.SaveChangesAsync();
        return request;
    }

    public async Task<SkillVM> DeleteSkill(int id) {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
            return null;
        
        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        var skillVM = new SkillVM(skill);
        return skillVM;
    }

    public async Task<Contacts_Skills> AddSkillToContact(int skillId, int contactId) {
        var link = new Contacts_Skills {
            ContactId = contactId,
            SkillsId = skillId
        };
        _context.Contacts_SKills.Add(link);
        await _context.SaveChangesAsync();
        return link;
    }
}