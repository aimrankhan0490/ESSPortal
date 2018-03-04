using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ESSPortal.Models;
using ESSPortal.DTOModels;
using System.ServiceModel;
using System.Net;
using System.Text;
using ESSPortal.ESSWebService;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace ESSPortal.Controllers
{
    // [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        public ActionResult RedirectLogin(string username, string password)
        {
            //C:\Sabri\SoltechESSPortal\ESSPortal\ESSPortal\Views\\.cshtml


            return View("/Account/Home");
        }
        public string Logout()
        {
            //C:\Sabri\SoltechESSPortal\ESSPortal\ESSPortal\Views\\.cshtml
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
                Response.Cookies.Add(aCookie); // overwrite it
            }

            //return RedirectToAction("Login?returnUrl=1");
            return "/Account/Login?returnUrl=1";
        }
        public ActionResult ChangePassword()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public string UpdatePassword(string currpassword, string newpassword)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                var userlogid = Request.Cookies["userlogid"].Value;
                ESSWebService.SDSLoginServicesClient sd = new ESSWebService.SDSLoginServicesClient();

                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.Permission _permissionlInfo = sd.ValidateUserEncrypted(callcont, userlogid, currpassword);
                if (_permissionlInfo != null)
                {
                    sd.changePassword(callcont, username, currpassword, newpassword, newpassword);
                    HttpCookie aCookie;
                    string cookieName;
                    int limit = Request.Cookies.Count;
                    for (int i = 0; i < limit; i++)
                    {
                        cookieName = Request.Cookies[i].Name;
                        aCookie = new HttpCookie(cookieName);
                        aCookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
                        Response.Cookies.Add(aCookie); // overwrite it
                    }
                    return "Password has changed";
                }
                else
                    return "Current Password is Incorrect";
            }
            else
                return "";

        }

        public ActionResult Home()
        {
            DTOModels.DTOPersonalInfo _perinfo = new DTOModels.DTOPersonalInfo();
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.SDSPersonalInfoServicesClient sd = new ESSWebService.SDSPersonalInfoServicesClient();
                ESSWebService.SDSBankInfoServicesClient sdbank = new ESSWebService.SDSBankInfoServicesClient();
                ESSWebService.SDSDashboardServicesClient dashclient = new ESSWebService.SDSDashboardServicesClient();


                ESSWebService.SDSBenefitsInfoServiceClient sdbenfits = new ESSWebService.SDSBenefitsInfoServiceClient();
                sdbenfits.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbenfits.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var beninfocount = sdbenfits.getAllowanceDeductionCostList(callcont, username).parmbenefitsAllowDedList.Count();


                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ticketcount = sdleavereq.getAllleaveRequestByNatureList(callcont, username, (int)SDSLeaveReqNature.TicketRequest, compid).parmGeneralRequestList.Count();
                var leavecount = sdleavereq.getAllleaveRequestByNatureList(callcont, username, (int)SDSLeaveReqNature.LeaveRequest, compid).parmGeneralRequestList.Count();
                var tcktencahscount = sdleavereq.getAllleaveRequestByNatureList(callcont, username, (int)SDSLeaveReqNature.TicketEncashment, compid).parmGeneralRequestList.Count();

                ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var absencecount = sdexcusereq.getAllExcuseReqeustByNature(callcont, username, ESSWebService.SDSExcuseNature.Absence, compid).parmExcuseRequestList.Count();
                var overtimecount = sdexcusereq.getAllExcuseReqeustByNature(callcont, username, ESSWebService.SDSExcuseNature.Overtime, compid).parmExcuseRequestList.Count();

                ESSWebService.SDSGeneralRequestServiceClient sdgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var lettercount = sdgenreq.getAllGeneralRequestListByNature(callcont, username, ESSWebService.SDSGeneralNature.Letter, compid).parmGeneralRequestList.Count();
                var generalcount = sdgenreq.getAllGeneralRequestListByNature(callcont, username, ESSWebService.SDSGeneralNature.General, compid).parmGeneralRequestList.Count();
 

                dashclient.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");

                ESSPortal.ESSWebService.Dashboard retdash = dashclient.getDashboard(callcont, username, compid);

                ESSWebService.SDSIdentificationInfoServicesClient sdidentify = new ESSWebService.SDSIdentificationInfoServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");

                ESSPortal.ESSWebService.PersonalInfo perinfoser = sd.getOnePersonalInfo(callcont, username);
                _perinfo.ArabicName = perinfoser.ArabicName;
                _perinfo.EnglishName = perinfoser.EnglishName;
                _perinfo.FirstName = perinfoser.EnglishName.Split(' ')[0];
                _perinfo.NewID = perinfoser.OldId;
                _perinfo.OldID = perinfoser.OldId;
                _perinfo.JoiningDate = perinfoser.Joiningdate.ToShortDateString();
                _perinfo.BirthDate = perinfoser.Birthdate.ToShortDateString();
                _perinfo.JobDesc = perinfoser.JobId;
                _perinfo.PositionDesc = perinfoser.Positiondescription;
                _perinfo.PersonalNumber = perinfoser.Personnelnumber;
                _perinfo.Companyname = perinfoser.Companyname;
                _perinfo.Gender = perinfoser.Gender;
                _perinfo.MaritalStatus = perinfoser.Maritalstatus;
                _perinfo.dashboardlist = FormDynamicDashBoard(retdash);
                _perinfo.Departmentname = perinfoser.DepartmentName;
                _perinfo.ProbationEndDate = perinfoser.ProbationEndDate.ToShortDateString();
                _perinfo.CountAddress = retdash.EmployeeDashboard.CountAddress;
                _perinfo.PositionId = perinfoser.PositionId;
                _perinfo.JobType = perinfoser.JobTypeId;
                _perinfo.CountBank = retdash.EmployeeDashboard.CountBank;
                _perinfo.CountContact = retdash.EmployeeDashboard.CountContact;
                _perinfo.CountDependents = retdash.EmployeeDashboard.CountDependents;
                _perinfo.CountExcuseRequest = retdash.EmployeeDashboard.CountExcuseRequest;
                _perinfo.CountIdentification = retdash.EmployeeDashboard.CountIdentification;
                _perinfo.CountLeaveRequest = retdash.EmployeeDashboard.CountLeaveRequest;
                _perinfo.CountSkills = retdash.EmployeeDashboard.CountSkills;
                _perinfo.DatePayslip = retdash.EmployeeDashboard.DatePayslip.ToShortDateString();
                _perinfo.DatePayslipPrevious = retdash.EmployeeDashboard.DatePayslip.ToShortDateString();

                _perinfo.LoanedItemCount = retdash.EmployeeDashboard.CountLoanedItems;
                _perinfo.AttendanceCount = retdash.EmployeeDashboard.CountAttendance;
                _perinfo.MyBusinessTripCount = retdash.EmployeeDashboard.CountBusinessTripRequest;
                _perinfo.CountMedicalInsuarance = retdash.EmployeeDashboard.CountMedicalInsurance;
                _perinfo.MyGeneralCount = retdash.EmployeeDashboard.CountGeneralRequest;
                _perinfo.MyLoanCount = retdash.EmployeeDashboard.CountLoanRequest;
                _perinfo.MyGeneralCount = retdash.EmployeeDashboard.CountGeneralRequest;
                //_perinfo.CountLeaveRequest = retdash.EmployeeDashboard.CountLeaveRequest;
                _perinfo.MyEOSCount = retdash.EmployeeDashboard.CountEOSRequest;
                _perinfo.MyExcuseCount = retdash.EmployeeDashboard.CountExcuseRequest;
                var medinsurance = sd.getMedicalInsuranceList(callcont, username, compid);
                // _perinfo.CountMedicalInsuarance = medinsurance.parmMedicalInsuranceList.Count();
                _perinfo.ImageArray = (sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto;
                _perinfo.Datepay = retdash.EmployeeDashboard.DatePayslip == new DateTime( 1900,1,1,12,0,0) ? "-" : retdash.EmployeeDashboard.DatePayslip.Day.ToString();
                _perinfo.DayPaid = retdash.EmployeeDashboard.DatePayslip == new DateTime(1900, 1, 1, 12, 0, 0) ? "-" : retdash.EmployeeDashboard.DatePayslip.ToString("dddd");
                _perinfo.YearMonth = retdash.EmployeeDashboard.DatePayslip == new DateTime(1900, 1, 1, 12, 0, 0) ? "-" : retdash.EmployeeDashboard.DatePayslip.ToString("MMMM") + " " + retdash.EmployeeDashboard.DatePayslip.Year.ToString();

                _perinfo.DatepayPrev = retdash.EmployeeDashboard.DatePayslipPrevious == new DateTime(1900, 1, 1, 12, 0, 0) ? "-" : retdash.EmployeeDashboard.DatePayslipPrevious.Day.ToString();
                _perinfo.DayPaidPrev = retdash.EmployeeDashboard.DatePayslipPrevious == new DateTime(1900, 1, 1, 12, 0, 0) ? "-" : retdash.EmployeeDashboard.DatePayslipPrevious.ToString("dddd");
                _perinfo.YearMonthPrev = retdash.EmployeeDashboard.DatePayslipPrevious == new DateTime(1900, 1, 1, 12, 0, 0) ? "-" : retdash.EmployeeDashboard.DatePayslipPrevious.ToString("MMMM") + " " + retdash.EmployeeDashboard.DatePayslipPrevious.Year.ToString();
                _perinfo.MyOverTimeCount = overtimecount;
                _perinfo.CountBenifits = beninfocount;
                _perinfo.MyGeneralCount = generalcount;
                _perinfo.CountLeaveRequest = leavecount;
                _perinfo.CountTicketRequest = ticketcount;
                _perinfo.CountLetter = lettercount;
                _perinfo.tcktencahscount = tcktencahscount;
                _perinfo.absencecount = absencecount;
                List<DTOPersonalContact> lstpersocontact = new List<DTOPersonalContact>();
                DTOPersonalContact additem = new DTOPersonalContact();
                additem.EmergencyContact = perinfoser.Perosnalcontactemergency;
                additem.Name = perinfoser.Perosnalcontactname;
                additem.RelationShip = perinfoser.Perosnalcontactrelationship;
                lstpersocontact.Add(additem);

                //if ((sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto != null)
                //{



                //    //Save the Byte Array as File.
                //    string filePath = "~/Prflpics/" + Path.GetFileName(username.ToString()) + ".jpg";
                //    string x = (sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto;
                //    byte[] imageBytes = Convert.FromBase64String(x);
                //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                //    // Convert byte[] to Image
                //    ms.Write(imageBytes, 0, imageBytes.Length);
                //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                //    Bitmap bmp = new Bitmap(image);
                //    // bm.Save(filePath, ImageFormat.Bmp);
                //    try
                //    {
                //        // bmp.Save(filePath);
                //    }
                //    catch
                //    {
                //        Bitmap bitmap = new Bitmap(bmp.Width, bmp.Height, bmp.PixelFormat);
                //        Graphics g = Graphics.FromImage(bitmap);
                //        g.DrawImage(bmp, new Point(0, 0));
                //        g.Dispose();
                //        bmp.Dispose();
                //        bmp = null;
                //        //  bitmap.Save(filePath);

                //        // bmp = bitmap; // preserve clone        
                //    }
                //    // image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);






                //    //   System.IO.File.WriteAllBytes(Server.MapPath(filePath), Encoding.UTF8.GetBytes((sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto));

                //}
                // _perinfo.ImageArray = Encoding.ASCII.GetBytes((sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto);

                // sd.get
                return View(_perinfo);
            }
            else
                return RedirectToAction("Login");
        }

        private List<DTODashBoard> FormDynamicDashBoard(Dashboard retdash)
        {
            List<DTODashBoard> dashlist = new List<DTOModels.DTODashBoard>();
            if (retdash.EmployeeDashboard != null)
            {

                foreach (var prop in retdash.EmployeeDashboard.GetType().GetProperties())
                {
                    ESSPortal.DTOModels.DTODashBoard _DTODashBoard = new DTOModels.DTODashBoard();
                    //Console.WriteLine("{0}={1}", prop.Name, );
                    if (prop.Name.StartsWith("Count"))
                    {
                        _DTODashBoard.NameDashboard = prop.Name.Substring(5, prop.Name.Length - 5);
                        _DTODashBoard.StyleTheme = "#5cb85c";
                        _DTODashBoard.Url = "/Home/Details?Detail=" + _DTODashBoard.NameDashboard;
                        _DTODashBoard.Count = prop.GetValue((Object)retdash.EmployeeDashboard, null).ToString();
                    }
                    else
                    {
                        _DTODashBoard.NameDashboard = prop.Name;
                        _DTODashBoard.StyleTheme = "#5cb85c";
                        _DTODashBoard.Count = prop.GetValue((Object)retdash.EmployeeDashboard, null).ToString();
                    }


                    if (!prop.Name.StartsWith("ExtensionData"))
                        dashlist.Add(_DTODashBoard);

                }
            }

            return dashlist;
        }

        [HttpPost]
        public string ValidateLogin(string username, string password)
        {
            try
            {
                //http://172.22.226.148:31564/MicrosoftDynamicsAXAif60/AxTest/xppservice.svc
                //http://dc01:3340/MicrosoftDynamicsAXAif60/AxMobServices/xppservice.svc
                //BasicHttpBinding binding = new BasicHttpBinding();
                //EndpointAddress epAddr = new EndpointAddress("http://172.22.226.148:31564/MicrosoftDynamicsAXAif60/AxTest/xppservice.svc");
                //ESSWebService.SDSPersonalInfoServices servref = ChannelFactory<ESSWebService.SDSPersonalInfoServices>.CreateChannel(binding, epAddr);
                //ESSWebService.SDSPersonalInfoServicesClient sd = new ESSWebService.SDSPersonalInfoServicesClient(binding, epAddr);
                ESSWebService.SDSLoginServicesClient sd = new ESSWebService.SDSLoginServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.ClientCredentials.Windows.ClientCredential.UserName = "webapp";
                //sd.ClientCredentials.Windows.ClientCredential.Password = "pass" + '"' + "word123";
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.Permission _permissionlInfo = sd.ValidateUserEncrypted(callcont, username, password);
                callcont.LogonAsUser = "";
                var userCookie = new HttpCookie("user", _permissionlInfo.WorkerRecId.ToString());
                var compCookie = new HttpCookie("compid", _permissionlInfo.CompanyId);
                var useridCookie = new HttpCookie("userlogid", username);
                var language = new HttpCookie("Language", "En");
                //add Language Cookie 
                //language cookie by default English

                //language = new HttpCookie("Language", "En");

                userCookie.Expires.AddDays(365);
                compCookie.Expires.AddDays(365);
                useridCookie.Expires.AddDays(365);
                language.Expires.AddDays(365);
                HttpContext.Response.Cookies.Add(userCookie);
                HttpContext.Response.Cookies.Add(compCookie);
                HttpContext.Response.Cookies.Add(useridCookie);
                //if (Request.Cookies["Language"] == null)
                //{
                HttpContext.Response.Cookies.Add(language);
                //}
                // ESSWebService.SDSPersonPhotoData photo = sd.getPersonPhoto(callcont, Convert.ToInt64("52565425785"));
                return "";
            }
            catch (Exception ex)
            {

                //throw new Exception("Username or password is Incorrect");
                return ex.Message;
            }


        }
        Bitmap GetBitmap(byte[] buf)
        {
            Int16 width = BitConverter.ToInt16(buf, 18);
            Int16 height = BitConverter.ToInt16(buf, 22);

            Bitmap bitmap = new Bitmap(width, height);

            int imageSize = width * height * 4;
            int headerSize = BitConverter.ToInt16(buf, 10);

            System.Diagnostics.Debug.Assert(imageSize == buf.Length - headerSize);

            int offset = headerSize;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, height - y - 1, Color.FromArgb(buf[offset + 3], buf[offset], buf[offset + 1], buf[offset + 2]));
                    offset += 4;
                }
            }
            return bitmap;
        }

        //
        // POST: /Account/VerifyCode

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}