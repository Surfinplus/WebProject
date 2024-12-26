using BerberOtomasyonASP.NET.Models;
using BerberOtomasyonASP.NET.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerberOtomasyonASP.NET.Controllers
{
	public class SalonController : Controller
	{
		private readonly ISalonRepository _SalonRepository;//nesnemiz 

		public SalonController(ISalonRepository SalonRepository)//
		{
			SalonRepository = SalonRepository;
		}

		
		public IActionResult Index()//listeleme
		{
			List<Salon>? objSalonList = SalonRepository.GetAll().ToList();
			return View(objSalonList);
		}

		[Authorize(Roles =UserRoles.Role_Admin)]
		public IActionResult Ekle()
		{
			return View();
		}
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
		public IActionResult Ekle(Salon Salon)
		{
			if (ModelState.IsValid)
			{
                SalonRepository.Ekle(Salon);
                SalonRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
				TempData["basarili"] = "Yeni Salon başarıyla oluşturuldu.";
				return RedirectToAction("Index", "Salon");// controller'ın Index metodunu cagirir
			}
			return View();


		}


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Guncelle(int? id)
		{
			if (id == null || id == 0) return NotFound();//id 0 null kontrolü
            Salon? Salon = SalonRepository.Get(u => u.Id == id);//parametre id eşit olan id'yi getir
			if (Salon == null)
			{
				return NotFound();
			}
			return View(Salon);
		}

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
		public IActionResult Guncelle(SalonSalon)
		{

			if (ModelState.IsValid)
			{
                Salonepository.Guncelle(Salon);
                SalonRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
				TempData["basarili"] = " Salon başarıyla güncellendi.";
				return RedirectToAction("Index", "Salon");//KiitapTuru controller'ın Index metodunu cagirir
			}
			return View();

		}

        //SİL
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
		{
			if (id == null || id == 0) return NotFound();//id 0 veya null kontrolü (asp-route-id index.html )
            Salon? Salon = _SalonRepository.Get(u => u.Id == id);
			if (Salon == null)
			{
				return NotFound();
			}
			return View(Salon;
		}

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
            Salon? Salon = SalonRepository.Get(u => u.Id == id);
			if (Salon == null) { return NotFound(); }
            SalonkRepository.Sil(Salon);
            //_uygulamaDbContext.KitapTurleri.Remove(kitapTuru);
            SalonRepository.Kaydet();
			// _uygulamaDbContext.SaveChanges();
			TempData["basarili"] = "Silme işlemi başarılı.";
			return RedirectToAction("Index", "Salon");

		}


		//detay


		