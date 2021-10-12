using LSB.Models;
using LSB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSB.API.Controllers
{
    [Route("api/Items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ItemRepository _itemRepository;

        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }



        // GET: api/<ItemController>

        [HttpPost, Route("api/items")]
        public async Task<IActionResult> GetSearchItemsAsync(string searchCriteria, [FromBody] FilterCriteria filterCriteria)
        {
            var matchedItems = await _itemRepository.GetItemsAsync(searchCriteria, filterCriteria);
            return Ok(matchedItems);
        }

        // GET api/<ItemController>/5
        [HttpGet, Route("api/items/{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/<ItemController>
        [HttpPost, Route("api/items")]
        public async Task<IActionResult> CreateItemAsync([FromBody] Item item)
        {
            var itemDetail = await _itemRepository.CreateItemAync(item);
            if (itemDetail == null)
            {
                return NotFound();
            }
            return Ok(itemDetail);
        }

        // PUT api/<ItemController>/5
        [HttpPut, Route("api/items/{id}")]
        public async Task<IActionResult> UpdateItemAync(string id, [FromBody] Item item)
        {
            var itemDetail = await _itemRepository.UpdateItemAync(id, item);
            if (itemDetail == null)
            {
                return NotFound();
            }
            return Ok(itemDetail);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete, Route("api/items/{id}")]
        public async Task<IActionResult> DeleteItemAsync(string id)
        {
            await _itemRepository.DeleteItemAync(id);
            return Ok();
        }
    }
}
