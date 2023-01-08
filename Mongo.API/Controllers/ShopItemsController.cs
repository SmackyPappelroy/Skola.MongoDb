using Microsoft.AspNetCore.Mvc;
using Mongo.Common;
using Mongo.Data.Services;
using MongoDB.Bson;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mongo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopItemsController : ControllerBase
    {
        private readonly IMongoService _service;

        public ShopItemsController(IMongoService service)
        {
            _service = service;
        }

        // GET: api/<WebShopController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                return Results.Ok(await _service.GetAsync<ShopItem>("ShopItem"));
            }
            catch (Exception)
            {
            }
            return Results.NotFound();
        }

        // GET api/<WebShopController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(string id)
        {
            try
            {
                return Results.Ok(await _service.GetSingleAsync<ShopItem>(id, "ShopItem"));
            }
            catch (Exception)
            {
            }
            return Results.NotFound();
        }

        // POST api/<WebShopController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] ShopItem shop)
        {
            if (shop == null)
                return Results.BadRequest();

            try
            {
                var uri = $"{typeof(ShopItem).Name.ToLower()}s/{shop.Id}";
                await _service.InsertAsync(uri, shop, "ShopItem");
                return Results.Created(uri, shop);
            }
            catch { }
            return Results.BadRequest();
        }

        // PUT api/<WebShopController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(string id, [FromBody] ShopItem item)
        {
            if (item == null || string.IsNullOrEmpty(id)) return Results.BadRequest();
            try
            {
                item.Id = id;
                if (await _service.UpdateAsync(id, item, "ShopItem"))
                    return Results.NoContent();
            }
            catch { }
            return Results.BadRequest();
        }

        // DELETE api/<WebShopController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(string id)
        {
            try
            {
                if (await _service.DeleteAsync<ShopItem>(id, "ShopItem"))
                {
                    return Results.NoContent();
                }
            }
            catch { }
            return Results.BadRequest();

        }
    }
}
