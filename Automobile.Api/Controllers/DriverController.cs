using AutoMapper;
using Automobile.DataService.Repositories.Interfaces;
using Automobile.Entities.DbSet;
using Automobile.Entities.Dtos.Requests;
using Automobile.Entities.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace Automobile.Api.Controller;

public class DriverController : BaseController
{
    public DriverController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper){  }
    
    [HttpGet("GetDriver")]
    public async Task<IActionResult> GetDriver(Guid driverId){
        var driver = await _unitOfWork.Drivers.GetById(driverId);

        if(driver == null)
            return NotFound();
        
        var result = _mapper.Map<GetDriverResponse>(driver);
        return Ok(result);
    }

    [HttpPost("AddDriver")]
    public async Task<IActionResult> AddDriver(CreateDriverRequest driver){
        if(!ModelState.IsValid)
            return BadRequest();
        
        var result = _mapper.Map<Driver>(driver); 

        await _unitOfWork.Drivers.Add(result);

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDriver), new { driverId = result.Id }, result);
    }

    [HttpPut("UpdateDriver")]
    public async Task<IActionResult> UpdateDriver(UpdateDriverRequest driver){
        if(!ModelState.IsValid)
            return BadRequest();
        
        var result = _mapper.Map<Driver>(driver); 

        await _unitOfWork.Drivers.Update(result);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
    
    [HttpGet("GetAllDrivers")]
    public async Task<IActionResult> GetAllDrivers(){

        var driver = await _unitOfWork.Drivers.All();

        if(driver == null)
            return NotFound();
        
        var result = _mapper.Map<IEnumerable<GetDriverResponse>>(driver);

        return Ok(result);
    }

    [HttpDelete("DeleteDriver")]
    public async Task<IActionResult> DeleteDriver(Guid driverId){
        
        var driver = await  _unitOfWork.Drivers.GetById(driverId);

        if(driver == null)
            return NotFound();

        await _unitOfWork.Drivers.Delete(driverId);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}