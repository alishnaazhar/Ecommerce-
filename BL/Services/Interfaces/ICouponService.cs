using BL.DTOs.CouponDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces;

public interface ICouponService
{
    List<GetCouponDTO> GetAll();
    GetCouponDTO Get(int id);

    GetCouponDTO Post(UpsertCouponDTO couponDTO);
    GetCouponDTO Put(int id, UpsertCouponDTO couponDTO);

    void Delete(int id);

}
