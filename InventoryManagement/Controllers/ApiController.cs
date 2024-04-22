using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Data;

namespace InventoryManagement.Controllers
{
    // Create and update
    // If the ID in the request body is equal to 0, the API will add/create the data and the value of ID is auto-increment
    // If the ID in the request body is existing, the API will edit/update the data related to the ID.
    // If the ID in the request body is not existing, the API will respond with a status code: 404.
    [Route("api/CreateItem")]
    [ApiController]
    public class CreateItemController : ControllerBase
    {
        private readonly ApiContext _context;

        public CreateItemController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Inventory inventory)
        {
            if(inventory.Id == 0)
            {
                _context.Items.Add(inventory);
            }
            
            else
            {
                var isItemExist = _context.Items.Find(inventory.Id);

                if (isItemExist == null)
                {
                    return new JsonResult(NotFound());
                }

                isItemExist = inventory;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(inventory));
        }
    }


    // Get
    // If the ID is found in the database, the API will response data in JSON format.
    // If the ID is not found in the database, the API will send a response code 404.
    [Route("api/GetItem")]
    [ApiController]
    public class GetItemController : ControllerBase 
    {
        private readonly ApiContext _context;

        public GetItemController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetItem(int id) 
        { 
            var result = _context.Items.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }
    }


    // Delete
    // If the ID is found in the database, the API will delete the data
    // If the ID is not found in the database, the API will send a response code 404.
    [Route("api/DeleteItem")]
    [ApiController]
    public class DeleteItemController : ControllerBase 
    {
        private readonly ApiContext _context;

        public DeleteItemController(ApiContext context)
        {
            _context = context;
        }

        [HttpDelete]
        public JsonResult DeleteItem(int id)
        {
            var result = _context.Items.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Items.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }
    }
}
