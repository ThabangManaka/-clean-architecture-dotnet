using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.DTOs
{
    public record CouponDto(int Id, string ProductName, string Description, int Amount);
}
