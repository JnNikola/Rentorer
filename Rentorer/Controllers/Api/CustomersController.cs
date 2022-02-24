using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using Rentor.Models;
using Rentorer.Dto;
using Rentorer.Models;

namespace Rentorer.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET: /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers
                .Include(c=> c.MembershipTypeType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET: /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer =_context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }


        //POST: /api/customers
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer= Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT: /api/customers/id
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return BadRequest();

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri +"/"+ customerInDb.Id), customerDto);
        }

        //DELETE: /api/customers/id
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
