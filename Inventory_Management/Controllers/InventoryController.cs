﻿using DigitalBookstoreManagement.Models;
using DigitalBookstoreManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookstoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly I_InventoryService _inventoryService;

        public InventoryController(I_InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // ✅ 1. Get all inventory items
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Inventory>>> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        // ✅ 2. Get inventory by InventoryID
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryById(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null)
                return NotFound($"Inventory with ID {id} not found.");

            return Ok(inventory);
        }

        // ✅ 3. Get inventory by BookID
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<Inventory>> GetInventoryByBookId(int bookId)
        {
            var inventory = await _inventoryService.GetInventoryByBookIdAsync(bookId);
            if (inventory == null)
                return NotFound($"Inventory for BookID {bookId} not found.");

            return Ok(inventory);
        }

        // ✅ 4. Add new inventory
        [HttpPost]
        public async Task<ActionResult<Inventory>> AddInventory([FromBody] InventoryDTO inventoryDto)
        {
            if (inventoryDto == null)
                return BadRequest("Invalid inventory data.");

            var inventory = new Inventory
            {
                BookID = inventoryDto.BookID,
                Quantity = inventoryDto.Quantity,
                NotifyLimit = inventoryDto.NotifyLimit,
            };

            //  inventory.Book = null;

            await _inventoryService.AddInventoryAsync(inventory);
            //return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.InventoryID }, inventory);
            return Ok($"Inventory added successfully.");
        }

        // ✅ 5. Update inventory
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] Inventory inventory)
        {
            if (inventory == null || id != inventory.InventoryID)
                return BadRequest("Inventory ID mismatch or invalid data.");

            //  inventory.Book = null;

            await _inventoryService.UpdateInventoryAsync(inventory);
            return Ok("Inventory Updated Successfully.");
        }

        //Add Stock in Inventory
        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock(int bookId, int quantity)
        {
            bool success = await _inventoryService.AddStockAsync(bookId, quantity);

            if (!success)
            {
                return BadRequest("Stock update failed. Inventory item not found.");
            }

            return Ok("Stock added successfully.");
        }

        // ✅ 6. Delete inventory
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var existingInventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (existingInventory == null)
                return NotFound("Inventory not found!");

            await _inventoryService.DeleteInventoryAsync(id);
            return NoContent();
        }

        [HttpPut("update-stock")]
        public async Task<IActionResult> UpdateStockOnOrder(int bookId, int orderedQuantity)
        {
            bool isAvailable = await _inventoryService.IsStockAvailableAsync(bookId, orderedQuantity);
            if (!isAvailable)
                return BadRequest("Insufficient stock!");

            await _inventoryService.UpdateStockOnOrderAsync(bookId, orderedQuantity);
            return Ok("Stock updated successfully!");
        }

        // ✅ 7. Check Stock and Notify Admin
        [HttpPost("check-stock/{bookId}")]
        public async Task<IActionResult> CheckStockAndNotifyAdminAsync(int bookId)
        {
            await _inventoryService.CheckStockAndNotifyAdminAsync(bookId);
            return Ok($"Stock check completed for BookID {bookId}.");
        }
    }
}
