using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Mappers;
using RestApi.Services;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase {
    private readonly IGroupService _groupService;

    public GroupsController (IGroupService groupService) {
        _groupService = groupService;
    }

    // localhost:port/groups/28728723
    [HttpGet("{id}")]
    public async Task<ActionResult<GroupResponse>> GetGroupById(string id, CancellationToken cancellationToken) {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);

        if (group is null) {
            return NotFound();
        }

        return Ok(group.ToDto());
    }
     [HttpGet]
    public async Task<ActionResult<IList<GroupResponse>>> GetGroupByName([FromQuery] string name, 
    [FromQuery] int page, [FromQuery] int pageS, [FromQuery] string orderBy, CancellationToken cancellationToken){
        
        var groups = await _groupService.GetGroupByNameAsync(name, page, pageS, orderBy, cancellationToken);

        return Ok(groups.Select(group => group.ToDto()).ToList());
    }

}