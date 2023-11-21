﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProfileCaio.Token
{
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey Creater(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
