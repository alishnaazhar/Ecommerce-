using BL.DTOs.CustomerDTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL;
public interface IStateHelper
{
    GetCustomerDTO User();
}

public class StateHelper:IStateHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StateHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public GetCustomerDTO User()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)
            result = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.UserData).Value;

        return JsonConvert.DeserializeObject<GetCustomerDTO>(result!)!;
    }


}
