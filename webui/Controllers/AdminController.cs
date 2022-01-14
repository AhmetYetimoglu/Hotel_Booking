using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using business.Abstract;
using entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webui.Identity;
using webui.Models;

namespace webui.Controllers
{
    //ahmetyetimoglu =>  admin
    [Authorize(Roles ="admin")]
    public class AdminController: Controller
    {
        private IProductService _productService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public AdminController(IProductService productService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _productService = productService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i=>i.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/admin/user/list");
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model, string[] selectedRoles)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user!=null)
                {
                    user.FirstName = model.FirstName;
                    user.FirstName = model.UserName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles?? new string[]{};
                        //ilgili rollerin veritabanına eklenmesi
                        await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles).ToArray<string>());
                        //ilgili rollerin veritabanından çıkartılması
                        await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles).ToArray<string>());
                        return Redirect("/admin/user/list");
                    }
                }
                return Redirect("/admin/user/list");
            }
            return View(model);
        }
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                //Kullanıcıların role sahip olup olmadığını kontrol eder
                var list = await _userManager.IsInRoleAsync(user,role.Name)
                                ?members:nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[]{})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user!=null)
                    {
                        var result = await _userManager.AddToRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[]{})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user!=null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }
                }


            }
            return Redirect("/admin/role/"+model.RoleId);
        }
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if(result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult ProductList()
        {
            return View(new ProductViewModel{
                Products = _productService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model,IFormFile file)
        {
            var entity = new Product()
            {
                Name = model.Name,
                City = model.City,
                ArrivalDate = model.ArrivalDate,
                DepartureDate = model.DepartureDate,
                Url = model.Url,
                AdultPrice = model.AdultPrice,
                ChildPrice = model.ChildPrice,
                ImageUrl = model.ImageUrl
            };
            if(file!=null)
            {
                var exntention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}.{exntention}");
                entity.ImageUrl = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            if(_productService.Create(entity))
            {
                List<String> mesaj = new List<string>();
                mesaj.Add("kayit eklendi");
                CreateMessage(mesaj,"success");

                return RedirectToAction("ProductList");
            }
            CreateMessage(_productService.ErrorMessage,"danger");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if(entity==null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                City = entity.City,
                ArrivalDate = entity.ArrivalDate,
                DepartureDate = entity.DepartureDate,
                Url = entity.Url,
                AdultPrice = entity.AdultPrice,
                ChildPrice = entity.ChildPrice,
                ImageUrl = entity.ImageUrl,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel model,IFormFile file)
        {
            var entity = _productService.GetById(model.ProductId);
            if (entity ==null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.City = model.City;
            entity.ArrivalDate = model.ArrivalDate;
            entity.DepartureDate = model.DepartureDate;
            entity.Url = model.Url;
            entity.AdultPrice = model.AdultPrice;
            entity.ChildPrice = model.ChildPrice;
            entity.IsApproved = model.IsApproved;
            //resim upload
            if(file!=null)
            {
                var exntention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}.{exntention}");
                entity.ImageUrl = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            
            _productService.Update(entity);
            List<String> mesaj = new List<string>();
            mesaj.Add(entity.Name+" isimli ürün güncellendi.");
            CreateMessage(mesaj,"success");
            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteProduct(int productId)
        {
            Console.WriteLine(productId);
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            List<String> mesaj = new List<string>();
            mesaj.Add(entity.Name+" isimli ürün silindi.");
            CreateMessage(mesaj,"danger");
            return RedirectToAction("ProductList");
        }
        private void CreateMessage(List<string> message,string alerttype)
        {
            var msg = new AlertMessage()
            {
            Message = message,
            AlertType = alerttype
            };
            
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }
    }
}