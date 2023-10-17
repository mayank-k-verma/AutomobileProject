using AutoMapper;
using Automobile.DataService.Repositories.Interfaces;
using Automobile.Entities.DbSet;
using Automobile.Entities.Dtos;
using Automobile.Entities.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Automobile.Api.Controller;

public class AchievementController : BaseController
{
    public AchievementController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper){  }
    
    [HttpGet("GetDriverAchievements")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId){
        var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);

        if(driverAchievements == null)
            return NotFound("Achievements not found");
        
        var result = _mapper.Map<DriverAchievementResponse>(driverAchievements);

        return Ok(result);
    }

    [HttpPost("AddAchievement")]
    public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement){
        if(!ModelState.IsValid)
            return BadRequest();

        var result = _mapper.Map<Achievement>(achievement);
        
        await _unitOfWork.Achievements.Add(result);

        await _unitOfWork.CompleteAsync();
        
        return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = result.DriverId }, result);
    }

    [HttpPut("UpdateAchievement")]
    public async Task<IActionResult> UpdateAchievement([FromBody] UpdateDriverAchievementRequest achievement){
        if(!ModelState.IsValid)
            return BadRequest();

        var result = _mapper.Map<Achievement>(achievement);
        
        await _unitOfWork.Achievements.Update(result);

        await _unitOfWork.CompleteAsync();
        
        return NoContent();
    }
}