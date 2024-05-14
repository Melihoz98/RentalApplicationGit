using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class AspNetUserDtoConvert
    {
        
        public static List<AspNetUserDto?>? FromAspNetUserCollection(List<AspNetUser> inAspNetUsers)
        {
            List<AspNetUserDto?>? aspNetUserDtoList = null;
            if (inAspNetUsers != null)
            {
                aspNetUserDtoList = new List<AspNetUserDto?>();
                foreach (AspNetUser user in inAspNetUsers)
                {
                    if (user != null)
                    {
                        AspNetUserDto? dto = FromAspNetUser(user);
                        aspNetUserDtoList.Add(dto);
                    }
                }
            }
            return aspNetUserDtoList;
        }

       
        public static AspNetUserDto? FromAspNetUser(AspNetUser user)
        {
            AspNetUserDto? userDto = null;
            if (user != null)
            {
                userDto = new AspNetUserDto(user.Id, user.UserName);
            }
            return userDto;
        }

       
        public static AspNetUser? ToAspNetUser(AspNetUserDto dto)
        {
            AspNetUser? user = null;
            if (dto != null)
            {
                user = new AspNetUser(dto.Id, dto.UserName);
            }
            return user;
        }
    }
}
