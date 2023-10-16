using SFirstApplicationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SFirstApplicationApi.Services
{


    // first we created model i.e user.cs
    //we create DTO (Data Transfer object) file 
    //context page,for creating Database i.e (CompanyContext) Download .sql,.Tools package
    //add database name in appsetting and startup
    //UserController for only Testing in postman(not for view)
    // for UserController we need UserService
    // add the services in startup

    //Next is token part
    // Download package,"System.IdentityModel.Tokens.Jwt" , "Microsoft.AspNetCore.Authentication.JwtBearer" 
    // for that we need interface (ItokenService)
    // then to implement interface we created (TokenService)

    public class UserService
    {
        private readonly CompanyContext _context;
        private readonly ITokenService _tokenservice;

        public UserService(CompanyContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenservice = tokenService;

        }
        public UserDTO Register(UserDTO userDto)
        {

            try
            {
                using var hmac = new HMACSHA512();
                var user = new User()
                {
                    UserId = userDto.UserId,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                    PasswordSalt = hmac.Key

                };
                _context.users.Add(user);
                _context.SaveChanges();
                userDto.jwtToken = _tokenservice.CreateToken(userDto);
                userDto.Password = "";

                return userDto;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;

        }
        public UserDTO Login(UserDTO userDto)
        {
            try
            {
                //Single() expects one and only one element in the collection.
                //   Single() throws an exception when it gets no element or more than one elements in the collection.
                //
                var myUser = _context.users.SingleOrDefault(u => u.UserId == userDto.UserId);
                if (myUser != null)
                {
                    using var hmac = new HMACSHA512(myUser.PasswordSalt);
                    var userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

                    for (int i = 0; i < userPassword.Length; i++)
                    {
                        if (userPassword[i] != myUser.PasswordHash[i])
                            return null;
                    }
                    userDto.jwtToken = _tokenservice.CreateToken(userDto);
                    userDto.Password = "";
                    return userDto;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }


        //public bool DeleteUser(UserDTO userDto)
        //{
        //    try
        //    {
        //        // Find the user by their unique identifier (e.g., UserId) in the database
        //        var userToDelete = _context.users.SingleOrDefault(u => u.UserId == userDto.Id);

        //        if (userToDelete != null)
        //        {
        //            // Remove the user from the database
        //            _context.users.Remove(userToDelete);
        //            _context.SaveChanges();
        //            return true; // Return true if the user is successfully deleted.
        //        }

        //        return false; // Return false if the user with the given ID is not found.
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false; // Return false in case of an exception or error.
        //    }
        //}

    }
}