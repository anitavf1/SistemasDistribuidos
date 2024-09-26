using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Mappers;
using RestApi.Services;
using RestApi.Exceptions;
using System.Net;
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


    [HttpGet("like-name")]
    public async Task<ActionResult<IList<GroupResponse>>> GetGroupByName([FromQuery] string name, 
    [FromQuery] int page, [FromQuery] int pageS, [FromQuery] string orderBy, CancellationToken cancellationToken){
        
        var groups = await _groupService.GetGroupByNameAsync(name, page, pageS, orderBy, cancellationToken);

        return Ok(groups.Select(group => group.ToDto()).ToList());
    }

    


    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteGroup(string id, CancellationToken cancellationToken){
        try
        {
            await _groupService.DeleteGroupByIdAsync(id, cancellationToken);
            return NoContent();
        } catch(GroupNotFoundException){
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<GroupResponse>> CreatedGroup([FromBody] CreateGroupRequest groupRequest, CancellationToken cancellationToken){
        try{

            var group = await _groupService.CreateGroupAsync(groupRequest.Name, groupRequest.Users, cancellationToken);
            return CreatedAtAction(nameof(GetGroupById), new {id = group.Id}, group.ToDto());

        }catch(InvalidGroupRequestFormatException){
            
            return BadRequest(NewValidationProblemDetails("One or more validation errors occurred", HttpStatusCode.BadRequest, new Dictionary<string, string[]>{
                {"Groups",["Users array is empty"]}
            }));

        }catch(GroupAlreadyExistsException){
            return Conflict(NewValidationProblemDetails("One or more validation errors occurred", HttpStatusCode.Conflict, new Dictionary<string, string[]>{
                {"Groups",["Group with same name already exists"]}
            }));
        }
    }

    private static ValidationProblemDetails NewValidationProblemDetails(string title, HttpStatusCode statusCode, Dictionary<string, string[]>errors){
        return new ValidationProblemDetails{
            Title = title,
            Status = (int)statusCode,
            Errors = errors
        };
    }


}