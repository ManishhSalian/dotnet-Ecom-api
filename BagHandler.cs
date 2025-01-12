using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BagAPI.Data
{
    public class BagHandler
    {

        public async static Task<IResult> GetData(BagDbContext context)
        {
            var users = await context.Users.ToListAsync();
            return Results.Ok(users);
        }

        public async static Task<IResult> PostData(BagDbContext context)
        {
            var newUser = new Users
            {
                id = 1,
                name = "Mathew",
                age = 21,
                mobile = 1321253,
                password = "mansa@123"
            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return Results.Created($"/users/{newUser.id}", newUser);
        }

        public async static Task<IResult> UpdateData(int id, Users updatedUser, BagDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user == null)
            {
                return Results.NotFound($"User with ID '{id}' not found.");
            }

            user.name = updatedUser.name;
            user.age = updatedUser.age;
            user.mobile = updatedUser.mobile;
            user.password = updatedUser.password;

            await context.SaveChangesAsync();
            return Results.Ok(user);
        }

        public async static Task<IResult> GetById(BagDbContext context, [FromHeader] int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user == null)
            {
                return Results.NotFound($"User with ID '{id}' not found.");
            }
            return Results.Ok(user);
        }

        public async static Task<IResult> GetByIdAndAge(int id, int age, BagDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id && u.age == age);
            if (user == null)
            {
                return Results.NotFound($"User with ID '{id}' and age '{age}' not found.");
            }
            return Results.Ok(user);
        }

        public async static Task<IResult> PutData(int id, Users updatedUser, BagDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user == null)
            {
                return Results.NotFound($"User with ID '{id}' not found.");
            }
            user.name = updatedUser.name;
            user.age = updatedUser.age;
            user.mobile = updatedUser.mobile;
            user.password = updatedUser.password;
            await context.SaveChangesAsync();
            return Results.Ok(user);
        }

        public async static Task<IResult> PatchData(int id, Users updatedUser, BagDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user == null)
            {
                return Results.NotFound($"User with ID '{id}' not found.");
            }

            if (!string.IsNullOrEmpty(updatedUser.name))
                user.name = updatedUser.name;

            if (updatedUser.age > 0)
                user.age = updatedUser.age;

            if (updatedUser.mobile > 0)
                user.mobile = updatedUser.mobile;

            if (!string.IsNullOrEmpty(updatedUser.password))
                user.password = updatedUser.password;

            await context.SaveChangesAsync();
            return Results.Ok(user); // Return the updated user
        }





    }
}
