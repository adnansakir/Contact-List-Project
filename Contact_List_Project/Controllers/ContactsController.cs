using Contact_List_Project.Data;
using Contact_List_Project.Models;
using Contact_List_Project.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Contact_List_Project.Controllers
{
    public class ContactsController : Controller
    {

        public ContactsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contacts = await mvcDemoDbContext.Contacts.ToListAsync();
            return View(contacts);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContactViewModel addContactRequest)
        {
            var contact = new Contact() {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Number = addContactRequest.Number,
                Email = addContactRequest.Email,
                Team = addContactRequest.Team,
                DateOfBirth = addContactRequest.DateOfBirth 
            };

            await mvcDemoDbContext.Contacts.AddAsync(contact);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private readonly MVCDemoDbContext mvcDemoDbContext;

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var contact = await mvcDemoDbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if(contact != null)
            {
                var viewModel = new UpdateContactViewModel()
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Number = contact.Number,
                    Email = contact.Email,
                    Team = contact.Team,
                    DateOfBirth = contact.DateOfBirth
                };
                return await Task.Run(() =>View("View", viewModel));
            }
            
            return RedirectToAction("Index");
        }




       
        [HttpPost]
        public async Task<IActionResult> View(UpdateContactViewModel model)
        {
            var contact = await mvcDemoDbContext.Contacts.FindAsync(model.Id);

            if(contact != null)
            {
                contact.Name = model.Name;
                contact.Number = model.Number;
                contact.Email = model.Email;
                contact.Team = model.Team;
                contact.DateOfBirth = model.DateOfBirth;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateContactViewModel model)
        {
            var contact = await mvcDemoDbContext.Contacts.FindAsync(model.Id);

            if (contact !=null)
            {
                mvcDemoDbContext.Contacts.Remove(contact);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
