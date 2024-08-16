using APIWeb.EF;
using APIWeb.Entities;
using APIWeb.Infrastructure;
using APIWeb.Interface.Repository;
using APIWeb.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace APIWeb.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {

        }
       
    }

}

