using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webui.EmailSevices;
using webui.Identity;
using webui.Models;

namespace shopapp.webui.Controllers
{
    //Account içindeki bütün post metotlarına token kontrol işlemini getir.
    [AutoValidateAntiforgeryToken]
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,IEmailSender emailSender)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl=null)
        {
            return View(new LoginModel(){
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            // var user = await _userManager.FindByNameAsync(model.Username);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user==null)
            {
                ModelState.AddModelError("","Bu kullanici adi ile daha önce hesap oluşturulmamiştir");
                return View(model);
            }
            //Giriş yaparken emailini onaylanıp onaylanmadığını kontrol
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("","Lütfen email hesabiniza gelen link ile hesabinizi doğrulayin.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user,model.Password,true,false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("","Girilen kullanici adi veya parola yanlis");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName  = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email    
            };           

            var result = await _userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"customer");
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail","Account",new {
                    userId = user.Id,
                    token = code
                });
                // email
                await _emailSender.SendEmailAsync(model.Email,"Hesabinizi onaylayiniz.",$"Lütfen email hesabinizi onaylamak için linke <a href='https://localhost:5001{url}'>tiklayiniz.</a>");
                return RedirectToAction("Login","Account");
            }

            ModelState.AddModelError("","Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId==null || token ==null)
            {
                List<String> mesaj = new List<string>();
                mesaj.Add("Geçersiz token");
                CreateMessage(mesaj,"danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user,token);
                if(result.Succeeded)
                {
                    List<String> mesaj = new List<string>();
                    mesaj.Add("Hesabiniz onaylandi.");
                    CreateMessage(mesaj,"success");
                    return View();
                }
            }
            List<String> mesaj1 = new List<string>();
            mesaj1.Add("Hesabiniz onaylanmadi");
            CreateMessage(mesaj1,"warning");
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);

            if(user==null)
            {
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword","Account",new {
                userId = user.Id,
                token = code
            });
            // email
            await _emailSender.SendEmailAsync(Email,"Reset Password.",$"Parolanizi yenilemek için linke <a href='https://localhost:5001{url}'>tiklayiniz.</a>");

            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId==null || token==null)
            {
                return RedirectToAction("Home","Index");
            }
            var model = new ResetPasswordModel {Token=token};
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                return RedirectToAction("Home","Index");
            }
            //resetleme işlemi
            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
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