using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DataContract.CustomerDC;
using ML.Common;
using Data.DataContract;
using Db.Interfaces;
using ML.Common.Extensions.Linq;

namespace Db.ImplementSQL.Repository
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public BaseResponse UpdateInfo(Customer user, UpdateInfo info)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = new Entity.Customer { Id = user.Id };
                db.Customers.Attach(entityDb);

                var changed = false;

                if (info.CustomerType.HasValue)
                {
                    changed = true;
                    entityDb.CustomerType = info.CustomerType.Value;
                    db.Entry(entityDb).Property(o => o.CustomerType).IsModified = true;
                }

                if (changed)
                {
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public FindResponse FindCustomers(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.Customers
                    .Include("Images")
                    .Include("Visits")
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.ClinicId))
                {
                    query = query.Where(q => q.ClinicId.ToLower().Contains(request.ClinicId.Trim().ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.ClinicName))
                {
                    query = query.Where(q => q.ClinicName.ToLower().Contains(request.ClinicName.Trim().ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.DentistName))
                {
                    query = query.Where(q => q.DentistName.ToLower().Contains(request.DentistName.Trim().ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.ClinicPhone))
                {
                    query = query.Where(q => q.ClinicPhone.ToLower().Contains(request.ClinicPhone.Trim().ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.Address))
                {
                    query = query.Where(q => q.Address.ToLower().Contains(request.Address.Trim().ToLower()));
                }

                if (request.City.HasValue)
                {
                    query = query.Where(q => q.City == request.City);
                }

                if (request.District.HasValue)
                {
                    query = query.Where(q => q.District == request.District);
                }

                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    request.Email = request.Email.Trim().ToLower();
                    query = query.Where(q => q.ClinicEmail.ToLower().Contains(request.Email)
                        || q.DentistEmail.ToLower().Contains(request.Email));
                }

                if (request.InterestingDevice.HasValue)
                {
                    query = query.Where(q => q.InterestingDevice.ToLower().Contains(request.InterestingDevice.ToString().ToLower().ToLower()));
                }

                if (request.UsingRC.HasValue)
                {
                    query = query.Where(q => q.UsingRC == request.UsingRC);
                }

                if (request.AssignTo.HasValue)
                {
                    query = query.Where(q => q.AssignTo.ToLower().Contains(request.AssignTo.Value.ToString().ToLower()));
                }

                if (request.CustomerType.HasValue)
                {
                    query = query.Where(q => q.CustomerType == request.CustomerType);
                }

                if (request.VisitFrom.HasValue)
                {
                    query = query.Where(q => q.Visits.Any(i => i.DateVisit >= request.VisitFrom.Value));
                }

                if (request.VisitTo.HasValue)
                {
                    query = query.Where(q => q.Visits.Any(i => i.DateVisit <= request.VisitTo.Value));
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.ClinicId));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<Customer>();
            }

            return response;
        }

        public Customer GetCustomer(Guid id)
        {
            using (var db = DbContext)
            {
                return db.Customers
                    .Include("Images")
                    .Include("Visits")
                    .FirstOrDefault(u => u.Id == id).Map<Entity.Customer, Customer>();
            }
        }

        public Customer GetCustomer(string clinicId)
        {
            using (var db = DbContext)
            {
                return db.Customers.FirstOrDefault(i => i.ClinicId.ToLower().Trim() == clinicId.ToLower().Trim()).Map<Entity.Customer, Customer>();
            }
        }

        public BaseResponse SaveCustomer(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<Customer, Entity.Customer>();

                var isNew = false;
                if (!db.Customers.Any(e => e.Id == entityDb.Id))
                {
                    db.Customers.Add(entityDb);
                    isNew = true;
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                if (!isNew)
                {
                    var oldCustomer = GetCustomer(entityDb.Id);

                    //delete
                    var deletedVisits = oldCustomer.Visits.Where(i => entityDb.Visits.All(v => v.Id != i.Id));

                    foreach (var customerVisit in deletedVisits)
                    {
                        db.Entry(customerVisit.Map<CustomerVisit, Entity.CustomerVisit>()).State = EntityState.Deleted;
                    }

                    //add
                    foreach (var customerVisit in entityDb.Visits.Where(i => oldCustomer.Visits.All(v => v.Id != i.Id)))
                    {
                        db.CustomerVisits.Add(customerVisit);
                    }

                    //update
                    foreach (var customerVisit in entityDb.Visits.Where(i => oldCustomer.Visits.Any(v => v.Id == i.Id)))
                    {
                        db.Entry(customerVisit).State = EntityState.Modified;
                    }
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse SaveCustomers(List<Customer> customers)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                if (customers == null || !customers.Any()) return response;

                customers.ForEach(customer => db.Customers.Add(customer.Map<Customer, Entity.Customer>()));
                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public Customer GetCustomerByEmail(string email)
        {
            using (var db = DbContext)
            {
                return db.Customers.FirstOrDefault(u => u.ClinicEmail == email).Map<Entity.Customer, Customer>();
            }
        }

        public bool ClinicIdIsExisted(string clinicId)
        {
            clinicId = clinicId.ToStr().Trim().ToLower();
            using (var db = DbContext)
            {
                return db.Customers.Any(u => u.ClinicId.Trim().ToLower() == clinicId);
            }
        }

        public bool ClinicIdIsExisted(string clinicId, Guid id)
        {
            clinicId = clinicId.ToStr().Trim().ToLower();
            using (var db = DbContext)
            {
                return db.Customers.Any(u => u.ClinicId.ToLower().Trim() == clinicId & u.Id != id);
            }
        }

        public List<string> GetAllEmails()
        {
            using (var db = DbContext)
            {
                return db.Customers.Select(i => i.ClinicEmail).ToList();
            }
        }

        public BaseResponse DeleteCustomer(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.Customers.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;

                    if (response.Success)
                    {
                        db.Database.ExecuteSqlCommand("DELETE CustomerImages WHERE CustomerId = {0}", id);
                        db.Database.ExecuteSqlCommand("DELETE CustomerVisits WHERE CustomerId = {0}", id);
                    }
                }
            }

            return response;
        }

        #region image

        public BaseResponse SaveImage(CustomerImage customerImage)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = customerImage.Map<CustomerImage, Entity.CustomerImage>();

                if (!db.CustomerImages.Any(e => e.Id == entityDb.Id))
                {
                    db.CustomerImages.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse SaveListImage(List<CustomerImage> listImage)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                if (listImage.Any())
                {
                    listImage.ForEach(productImage =>
                    {
                        var entityDb = productImage.Map<CustomerImage, Entity.CustomerImage>();
                        db.CustomerImages.Add(entityDb);

                    });

                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public BaseResponse DeleteImage(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.CustomerImages.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public BaseResponse DeleteImages(List<Guid> ids)
        {
            var response = new BaseResponse();

            if (ids == null || !ids.Any()) return response;

            using (var db = DbContext)
            {
                ids.ForEach(id =>
                {
                    var entity = db.CustomerImages.FirstOrDefault(e => e.Id == id);

                    if (entity != null)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                    }
                });

                response.Success = db.SaveChanges() > 0;
                return response;
            }
        }

        public List<CustomerImage> GetImages(Guid customerId)
        {
            using (var db = DbContext)
            {
                return
                    db.CustomerImages.Where(i => i.CustomerId == customerId)
                        .ToList()
                        .MapList<CustomerImage>();
            }
        }

        public List<CustomerImage> GetImages(List<Guid> ids)
        {
            using (var db = DbContext)
            {
                if (ids == null || !ids.Any()) return new List<CustomerImage>();

                return db.CustomerImages.Where(i => ids.Contains(i.Id)).ToList().MapList<CustomerImage>();
            }
        }

        public CustomerImage GetImage(Guid id)
        {
            using (var db = DbContext)
            {
                return db.CustomerImages.FirstOrDefault(i => i.Id == id).Map<Entity.CustomerImage, CustomerImage>();
            }
        }

        #endregion
    }
}
