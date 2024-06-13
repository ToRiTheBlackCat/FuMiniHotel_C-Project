using HotelManagementSystemMain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemClassLibrary.Repository
{
    public class UserRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public UserRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task DeleteCustInfo(int custID)
        {
            var cust = await FindCustInfoById(custID);
            cust.CustomerStatus = 0;
            _context.SaveChanges();
        }

        public async Task<Customer?> FindCustInfoById(int? custID)
        {
            return await _context.Customers.FindAsync(custID);
        }
        public async Task AddOrUpdateCustInfo(Customer customer)
        {
            var custInfo = await FindCustInfoById(customer.CustomerId);
            if (custInfo == null)
                _context.Customers.Add(customer);

            else
            {
                //custInfo.CustomerId = customer.CustomerId;
                custInfo.CustomerFullName = customer.CustomerFullName;
                custInfo.EmailAddress = customer.EmailAddress;
                custInfo.Telephone = customer.Telephone;
                custInfo.CustomerBirthday = customer.CustomerBirthday;
                custInfo.Password = customer.Password;
               
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> LoadAllCustInfoBySearch(string search)
        {
            return await  _context.Customers.Where(r => r.CustomerFullName.Contains(search) && r.CustomerStatus == 1 ).ToListAsync();
        }

        public async Task<List<Customer>> LoadAllCustInfo()
        {
            return await _context.Customers.Where(c => c.CustomerStatus == 1).Select(c => new Customer
            {
                CustomerId = c.CustomerId,
                CustomerFullName = c.CustomerFullName,
                Telephone = c.Telephone,
                EmailAddress = c.EmailAddress,
                CustomerBirthday = c.CustomerBirthday,
                Password = c.Password,
            }).ToListAsync();
        }

        public async Task<Customer?> LoginByEmailAndPassword(string email, string password)
        {
            var customer =  _context.Customers.FirstOrDefault(c => c.EmailAddress.Equals(email) && c.Password.Equals(password));
            return customer;
            
        }
        public async Task<Customer?> GetCustomerByEmail(string email)
        {
            var customer = await  _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress.Equals(email));
            return customer;
        }
        public async Task RegisterAccount(Customer customer )
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCustomerInfo( Customer customer)
        {
            var originCust = await GetCustomerByEmail(customer.EmailAddress);

            originCust.CustomerFullName = customer.CustomerFullName;
            originCust.Password = customer.Password;
            originCust.Telephone = customer.Telephone;
            originCust.CustomerBirthday = customer.CustomerBirthday;
            await _context.SaveChangesAsync();
        }



    }
}
