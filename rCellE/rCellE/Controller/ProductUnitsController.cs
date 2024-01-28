using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DataAccessLayer;
using ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductUnitsController : ControllerBase
{
    private readonly ProductUnitRepository _productUnitRepository;

    public ProductUnitsController(ProductUnitRepository productUnitRepository)
    {
        _productUnitRepository = productUnitRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductUnit>>> GetProductUnits()
    {
        try
        {
            var productUnits = await _productUnitRepository.GetAllProductUnits();
            return Ok(productUnits);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving product units: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductUnit>> GetProductUnit(int id)
    {
        try
        {
            var productUnit = await _productUnitRepository.GetProductUnitById(id);

            if (productUnit == null)
                return NotFound();

            return Ok(productUnit);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving product unit: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProductUnit>> PostProductUnit(ProductUnit productUnit)
    {
        try
        {
            await _productUnitRepository.AddProductUnit(productUnit);
            return CreatedAtAction(nameof(GetProductUnit), new { id = productUnit.ProductUnitId }, productUnit);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error adding product unit: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductUnit(int id, ProductUnit productUnit)
    {
        try
        {
            var existingProductUnit = await _productUnitRepository.GetProductUnitById(id);

            if (existingProductUnit == null)
                return NotFound();

            productUnit.ProductUnitId = id; // Ensure the correct ID is set
            await _productUnitRepository.UpdateProductUnit(productUnit);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating product unit: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductUnit(int id)
    {
        try
        {
            var existingProductUnit = await _productUnitRepository.GetProductUnitById(id);

            if (existingProductUnit == null)
                return NotFound();

            await _productUnitRepository.DeleteProductUnit(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting product unit: {ex.Message}");
        }
    }
}
