using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactListMvc.Data;
using ContactListMvc.Models;
using ContactListMvc.Services;

namespace ContactListMvc.Controllers
{
    public class ContactListEntriesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IConsolePrinter _printer;

        public ContactListEntriesController(
            DatabaseContext context, IConsolePrinter printer)
        {
            _context = context;
            _printer = printer;
        }

        // GET: ContactListEntries
        public async Task<IActionResult> Index()
        {
            _printer.Print("Ma pregatesc sa generez lista de contacte");

            return _context.ContactListEntry != null ?
                        View(await _context.ContactListEntry.ToListAsync()) :
                        Problem("Entity set 'DatabaseContext.ContactListEntry'  is null.");
        }

        // GET: ContactListEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactListEntry == null)
            {
                return NotFound();
            }

            var contactListEntry = await _context.ContactListEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactListEntry == null)
            {
                return NotFound();
            }

            return View(contactListEntry);
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
        public async Task<IActionResult> Create([Bind("Id,ContactType,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntry contactListEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactListEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactListEntry);
        }

        // GET: ContactListEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactListEntry == null)
            {
                return NotFound();
            }

            var contactListEntry = await _context.ContactListEntry.FindAsync(id);
            if (contactListEntry == null)
            {
                return NotFound();
            }
            return View(contactListEntry);
        }

        // POST: ContactListEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContactType,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntry contactListEntry)
        {
            if (id != contactListEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactListEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactListEntryExists(contactListEntry.Id))
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
            return View(contactListEntry);
        }

        // GET: ContactListEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactListEntry == null)
            {
                return NotFound();
            }

            var contactListEntry = await _context.ContactListEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactListEntry == null)
            {
                return NotFound();
            }

            return View(contactListEntry);
        }

        // POST: ContactListEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactListEntry == null)
            {
                return Problem("Entity set 'DatabaseContext.ContactListEntry'  is null.");
            }
            var contactListEntry = await _context.ContactListEntry.FindAsync(id);
            if (contactListEntry != null)
            {
                _context.ContactListEntry.Remove(contactListEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactListEntryExists(int id)
        {
            return (_context.ContactListEntry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
