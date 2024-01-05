using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdressbookApp.Infrastructure;
using AdressbookApp.Models;

namespace AdressbookApp.Controllers
{
    public class AddressesController : Controller
    {
        private readonly AddressService _addressService;

        public AddressesController()
        {
            _addressService = new AddressService();
        }

        public async Task<IActionResult> Index()
        {
            var addressList = await _addressService.GetAddressesAsync();
              return addressList != null ? 
                          View(addressList) :
                          Problem("Entity set 'AddressContext.Addresses'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressService.GetAddressByIdAsync((int)id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,FullName,Email,PhoneNumber,Country,City,Region,Street,ApartmentNumber")] Address address)
        {
            if (ModelState.IsValid)
            {
                await _addressService.SendAddressAsync(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressService.GetAddressByIdAsync((int)id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,FullName,Email,PhoneNumber,Country,City,Region,Street,ApartmentNumber")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _addressService.UpdateAddressAsync(id, address);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressService.GetAddressByIdAsync((int)id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address != null)
            {
                await _addressService.DeleteAddressAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
