using Microsoft.AspNetCore.Mvc;
using ContactListMvc.Models;
using ContactListMvc.Business.Abstractions.Services;
using ContactListMvc.Business.Models;
using ContactListMvc.Business.Exceptions;
using ContactListMvc.Web.Filters;

namespace ContactListMvc.Controllers
{
    public class ContactListEntriesController : Controller
    {
        private readonly IContactListService _service;

        public ContactListEntriesController(
            IContactListService service)
        {
            _service = service;
        }

        // GET: ContactListEntries
        [MeasureExecutionActionFilter]
        public async Task<IActionResult> Index()
        {
            IReadOnlyList<ContactListEntry> contactList = await _service.GetAllContactsAsync();

            IReadOnlyList<ContactListEntryViewModel> viewModels = contactList.Select(c => MapToViewModel(c)).ToList();

            return View(viewModels);
        }

        // GET: ContactListEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactListEntry? model = await _service.GetByIdAsync(id.Value);
            if (model is null)
            {
                return NotFound();
            }

            ContactListEntryViewModel viewModel = MapToViewModel(model);

            return View(viewModel);
        }

        // GET: ContactListEntries/Create
        public IActionResult Create()
        {
            // ViewData["CompanyName"] = "SC Test ABC SRL";
            ViewBag.CompanyName = "SC Test ABC123 SRL";

            return View();
        }

        // POST: ContactListEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactType,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ContactListEntry model = MapToBusinessModel(viewModel);
                await _service.CreateEntryAsync(model);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: ContactListEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactListEntry? model = await _service.GetByIdAsync(id.Value);
            if (model is null)
            {
                return NotFound();
            }

            ContactListEntryViewModel viewModel = MapToViewModel(model);

            return View(viewModel);
        }

        // POST: ContactListEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContactType,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContactListEntry model = MapToBusinessModel(viewModel);

                    await _service.UpdateEntryAsync(id, model);
                }
                catch (ContactListUpdateConflictException e)
                {
                    if (e.EntryDeletedInTheMeanwhile)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: ContactListEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactListEntry? model = await _service.GetByIdAsync(id.Value);
            if (model is null)
            {
                return NotFound();
            }

            ContactListEntryViewModel viewModel = MapToViewModel(model);

            return View(viewModel);
        }

        // POST: ContactListEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntryAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private static ContactListEntry MapToBusinessModel(ContactListEntryViewModel viewModel)
        {
            return new ContactListEntry
            {
                Id = viewModel.Id,
                ContactType = viewModel.ContactType,
                Name = viewModel.Name,
                DateOfBirth = viewModel.DateOfBirth,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email
            };
        }

        private static ContactListEntryViewModel MapToViewModel(ContactListEntry businessModel)
        {
            return new ContactListEntryViewModel
            {
                Id = businessModel.Id,
                ContactType = businessModel.ContactType,
                Name = businessModel.Name,
                DateOfBirth = businessModel.DateOfBirth,
                Address = businessModel.Address,
                PhoneNumber = businessModel.PhoneNumber,
                Email = businessModel.Email
            };
        }
    }
}
