using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckAdministratorsAsync();
            await CheckCommitteesAsync();
            await CheckRequestsAsync();
            await CheckStatusTypesAsync();
            await CheckTenantsAsync();
            await CheckWorkersAsync();
        }

        private async Task CheckAdministratorsAsync()
        {
            if (!_context.Administrators.Any())
            {
                AddTenant("27010101", "Facundo", "Colino", "45450101", "1130767787", "Aguilar 2497");
                AddTenant("77587357", "Martín", "Herrera", "47812901", "5128224276", "Juana Azurduy 100");
                AddTenant("35010987", "Ludmila", "Lopez", "47509856", "350 311 4425", "Manuela Pedraza 200");
                await _context.SaveChangesAsync();
            }
        }
        private void AddAdministrator(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _context.Administrators.Add(new Administrator
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }
        private async Task CheckCommitteesAsync()
        {
            if (!_context.Committees.Any())
            {
                _context.Committees.Add(new Entities.Committee { Description = "Consorcio Inicial", Address = "Aguilar 2497", Neighborhood = "Colegiales", Price = 200M });

                await _context.SaveChangesAsync();
            }
        }
        private Task CheckRequestsAsync()
        {
            throw new NotImplementedException();
        }
        private async Task CheckStatusTypesAsync()
        {
            if (!_context.StatusTypes.Any())
            {
                _context.StatusTypes.Add(new Entities.StatusType { Description = "Generada" });
                _context.StatusTypes.Add(new Entities.StatusType { Description = "Asignada" });
                _context.StatusTypes.Add(new Entities.StatusType { Description = "En Proceso" });
                _context.StatusTypes.Add(new Entities.StatusType { Description = "Finalizada" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckTenantsAsync()
        {
            if (!_context.Tenants.Any())
            {
                AddTenant("27845890", "Facundo", "Delgado", "45450202", "1136557821", "Balbin 300");
                AddTenant("77587357", "Barby", "Marsland", "47811020", "3128227398", "Lavalle 1461");
                AddTenant("214231", "Carmen", "Ruiz", "44504332", "350 322 3221", "Callao 12");
                await _context.SaveChangesAsync();
            }
        }
        private void AddTenant(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _context.Tenants.Add(new Tenant
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }
        private async Task CheckWorkersAsync()
        {
            AddWorker("30987172", "Isidro", "Banega", "44437891", "1120984530", "Bolivar 3300");
            AddWorker("29010124", "Daniela", "Gomez", "47811025", "3128227390", "Viamonte 100");
            AddWorker("30987654", "Daniel", "Lopez", "902021020", "350 322 3245", "Prometeo 20");
            await _context.SaveChangesAsync();
        }
        private void AddWorker(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _context.Workers.Add(new Worker
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }
    }
}
