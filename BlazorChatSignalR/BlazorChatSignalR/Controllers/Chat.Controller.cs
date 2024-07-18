using BlazorChatSignalR.Repos;
using ChatModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorChatSignalR.Controllers
{
    // The [Route] attribute is used to specify the URL pattern on which the controller should listen for requests.
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepo chatRepo) : ControllerBase
    {
        [HttpGet]
        // The ActionResult type is a flexible return type that can represent any type of result.
        public async Task<ActionResult<List<Chat>>> GetChatsAsync()
        {
            return Ok(await chatRepo.GetChatsAsync());
        }
    }
}
