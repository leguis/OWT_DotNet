using Contact.Data.Models;
using Contact.Data.Services;
using Contact.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Controllers;

[ApiController]
[Route("[controller]")]
public class SkillsController : ControllerBase
{
    private SkillServices _skillServices;

    public SkillsController(DataContext context) {
        _skillServices = new SkillServices(context);
    }

    [HttpGet]
    public async Task<ActionResult<List<SkillVM>>> GetSkills() {
        var skills = await _skillServices.GetSkills();
        return Ok(skills);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SkillVM>> GetSkill(int id) {
        var skill = await _skillServices.GetSkill(id);

        if (skill == null)
            return NotFound("No skill with this id");
        return Ok(skill);
    }

    [HttpPost]
    public async Task<ActionResult<SkillVM>> PostSkill(SkillVM request) {
        SkillVM newSkill = await _skillServices.AddSkill(request);
        return Ok(newSkill);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SkillVM>> UpdateSkill(int id, SkillVM request) {


        var skill = await _skillServices.PutSkill(id, request);

        if (skill == null)
            return NotFound("No skill with this id");

        return Ok(skill);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<SkillVM>> RemoveSkill(int id) {
        var skill = await _skillServices.DeleteSkill(id);

        if (skill == null)
            return NotFound("No skill with this id");
        
        return Ok(skill);
    }

    [HttpPost("addSkillToContact")]
    public async Task<ActionResult<Contacts_Skills>> AddSkillToContact(int skillId, int contactId) {
        var link = await _skillServices.AddSkillToContact(skillId, contactId);
        return Ok(link);
    }
}