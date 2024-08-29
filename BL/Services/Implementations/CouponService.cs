using BL.DTOs.CouponDTOs;
using BL.Services.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementations;

public class CouponService : ICouponService
{
    private ApplicationDBContext _context;

    public CouponService(ApplicationDBContext context)
    {
        _context = context;
    }
    public void Delete(int id)
    {
        var coupon = _context.Coupons.FirstOrDefault(_ => _.Id == id);
        if (coupon == null)
            throw new Exception("There is no such coupon found.");
        _context.Coupons.Remove(coupon);
        _context.SaveChanges();

    }
    public GetCouponDTO Get(int id)
    {
        var coupon = _context.Coupons.FirstOrDefault(_ => _.Id == id)
            ?? throw new Exception("There is no such coupon found.");

        return new GetCouponDTO
        {
            Id = id,
            CreatedDate = coupon.CreatedDate,
            UpdatedDate = coupon.UpdatedDate,
            Name = coupon.Name,
            Code = coupon.Code,
            Discount = coupon.Discount,
            Expiration_Date = coupon.Expiration_Date,
        };

    }
    public List<GetCouponDTO> GetAll()
    {
        var coupons = _context.Coupons.ToList();
        List<GetCouponDTO> couponList = new List<GetCouponDTO>();
        foreach (var coupon in coupons)
        {
            var newCoupon = new GetCouponDTO
            {
                Id = coupon.Id,
                CreatedDate=coupon.CreatedDate,
                UpdatedDate = coupon.UpdatedDate,
                Name = coupon.Name,
                Code = coupon.Code,
                Discount = coupon.Discount,
                Expiration_Date = coupon.Expiration_Date,
            };
            couponList.Add(newCoupon);
        }
        return couponList;
    }
    public GetCouponDTO Post(UpsertCouponDTO couponDTO)
    {
        var coupon = new Coupon
        {
            CreatedDate=DateTime.Now,
            Name = couponDTO.Name,
            Code = couponDTO.Code,
            Discount = couponDTO.Discount,
            Expiration_Date = couponDTO.Expiration_Date,
        };

        _context.Coupons.Add(coupon);
        _context.SaveChanges();

        return new GetCouponDTO
        {
            Id=coupon.Id,
            CreatedDate= coupon.CreatedDate,
            Name = coupon.Name,
            Code = coupon.Code,
            Discount = coupon.Discount,
            Expiration_Date = coupon.Expiration_Date,
        };

    }
    public GetCouponDTO Put(int id, UpsertCouponDTO couponDTO)
    {
        var coupon = _context.Coupons.FirstOrDefault(_ => _.Id == id);
        if (coupon == null)
            throw new Exception("There is no such coupon found.");
        coupon.Name = couponDTO.Name;
        coupon.Code = couponDTO.Code;
        coupon.Discount = couponDTO.Discount;
        coupon.Expiration_Date = couponDTO.Expiration_Date;
        coupon.UpdatedDate= DateTime.Now;
        _context.SaveChanges();

        return new GetCouponDTO
        {
            Id=coupon.Id,
            Name = coupon.Name,
            Code = coupon.Code,
            Discount = coupon.Discount,
            Expiration_Date = coupon.Expiration_Date,
            CreatedDate = coupon.CreatedDate,
            UpdatedDate = coupon.UpdatedDate,
        };
    }
}
