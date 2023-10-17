using AutoMapper;
using Automobile.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Automobile.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase{

    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}