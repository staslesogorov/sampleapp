using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using SampleApp.API.Data;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;

namespace SampleApp.API.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly SampleAppContext _db;

        public UsersRepository(SampleAppContext db)
        {
            _db = db;
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        public User CreateUser(User user)
        {
            try
            {
                _db.Add(user);
                _db.SaveChanges();
                return user;
            }
            catch (NpgsqlException ex)
            {
                throw new NpgsqlException($"Ошибка SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }

        public User EditUser(User user, int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User FindUserById(int id)
        {
            try
            {
                var result = _db.Users.Where(u => u.Id == id).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception($"Нет пользователя с id = {id}");
                }

                return result;
            }
            catch (NpgsqlException ex)
            {
                throw new NpgsqlException($"Ошибка SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }

        }

        public User FindUserByLogin(string login)
        {
            try
            {
                var result = _db.Users.Where(u => u.Login == login).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception($"Нет пользователя с id = {login}");
                }

                return result;
            }
            catch (NpgsqlException ex)
            {
                throw new NpgsqlException($"Ошибка SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }

    }
}