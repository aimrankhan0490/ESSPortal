using ESSPortal.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ESSPortal.ESSWebService;
using System.Data;
using FastMember;
using Helper;

namespace ESSPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
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
        public ActionResult MenuLayout()
        {
            DTOModels.DTOPersonalInfo _perinfo = new DTOModels.DTOPersonalInfo();
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                DateTime _nulldate = new DateTime(1990, 1, 1);
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.SDSPersonalInfoServicesClient sd = new ESSWebService.SDSPersonalInfoServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSPortal.ESSWebService.PersonalInfo perinfoser = sd.getOnePersonalInfo(callcont, username);


                _perinfo.ArabicName = perinfoser.ArabicName;
                _perinfo.EnglishName = perinfoser.EnglishName;
                _perinfo.FirstName = perinfoser.EnglishName.Split(' ')[0];
                _perinfo.NewID = perinfoser.OldId;
                _perinfo.OldID = perinfoser.OldId;
                _perinfo.JoiningDate = perinfoser.Joiningdate == _nulldate ? "-" : perinfoser.Joiningdate.ToShortDateString();
                _perinfo.BirthDate = perinfoser.Birthdate == _nulldate ?null :  perinfoser.Birthdate.ToShortDateString(); 
                _perinfo.JobType = perinfoser.JobId;
                _perinfo.JobDesc = perinfoser.JobTypeId;
                _perinfo.PositionDesc = perinfoser.Positiondescription;
                _perinfo.PositionId = perinfoser.PositionId;
                _perinfo.Companyname = perinfoser.Companyname;
                _perinfo.Gender = perinfoser.Gender;
                _perinfo.MaritalStatus = perinfoser.Maritalstatus;
                ESSPortal.ESSWebService.SDSAddressListDC lstadd = perinfoser.AddressList;
                _perinfo.ImageArray = (sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto;
                // lstadd.parmAddressList
                _perinfo.Departmentname = perinfoser.DepartmentName;
                _perinfo.ProbationEndDate = perinfoser.ProbationEndDate == _nulldate ? "-" : perinfoser.ProbationEndDate.ToShortDateString();
                //if ((sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto != null)
                //    _perinfo.ImageArray = Encoding.ASCII.GetBytes((sd.getPersonPhoto(callcont, Convert.ToInt64(username))).parmPersonPhoto);

                //   C:\Sabri\SoltechESSPortal\ESSPortal\ESSPortal\Views\.cshtml
                return PartialView("~/Views/LeftProfile.cshtml", _perinfo);
            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }

        }

        public ActionResult Details(string Detail, string Group)
        {
            List<DTOCommonDetailsList> _commondetails = new List<DTOCommonDetailsList>();
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            DateTime _nulldate = new DateTime(1990, 1, 1);
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.SDSPersonalInfoServicesClient sd = new ESSWebService.SDSPersonalInfoServicesClient();
                ESSWebService.SDSBankInfoServicesClient sdbank = new ESSWebService.SDSBankInfoServicesClient();
                ESSWebService.SDSBenefitsInfoServiceClient sdbenfits = new ESSWebService.SDSBenefitsInfoServiceClient();
                ESSWebService.SDSDependentInfoServicesClient sddepend = new ESSWebService.SDSDependentInfoServicesClient();
                ESSWebService.SDSExcuseRequestServicesClient sdexcreq = new ESSWebService.SDSExcuseRequestServicesClient();
                ESSWebService.SDSIdentificationInfoServicesClient sdidenreq = new ESSWebService.SDSIdentificationInfoServicesClient();
                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                //  ESSWebService.sds sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                sdbank.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbank.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                sddepend.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sddepend.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                sdexcreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdexcreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                sdidenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdidenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                sdbenfits.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbenfits.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");

                ESSPortal.ESSWebService.PersonalInfo perinfoser = sd.getOnePersonalInfo(callcont, username);
                var competencies = sd.getCompetencies(callcont, username, compid);
                var jobs = sd.getJobdetails(callcont, username);
                var medinsurance = sd.getMedicalInsuranceList(callcont, username, compid);
                DTOCommonDetailsList itemaddr = new DTOCommonDetailsList();
                DTOCommonDetailsList itemContact = new DTOCommonDetailsList();
                DTOCommonDetailsList itemBanks = new DTOCommonDetailsList();
                DTOCommonDetailsList itemDepend = new DTOCommonDetailsList();
                DTOCommonDetailsList itemexecreq = new DTOCommonDetailsList();
                DTOCommonDetailsList itemidenti = new DTOCommonDetailsList();
                DTOCommonDetailsList itemleavereq = new DTOCommonDetailsList();
                DTOCommonDetailsList itempersonalcontact = new DTOCommonDetailsList();
                DTOCommonDetailsList itembenifits = new DTOCommonDetailsList();
                DTOCommonDetailsList itembenifitLines = new DTOCommonDetailsList();
                DTOCommonDetailsList itemCompetencies = new DTOCommonDetailsList();
                DTOCommonDetailsList itemJobs = new DTOCommonDetailsList();
                DTOCommonDetailsList itemMedicalInsurance = new DTOCommonDetailsList();
                DTOCommonDetailsList itempersonalinfo = new DTOCommonDetailsList();
                DTOCommonDetailsList itempersocontact = new DTOCommonDetailsList();
                List<DTOPersonalContact> lstpersocontact = new List<DTOPersonalContact>();

                DTOCommonDetailsList itemCertificationList = new DTOCommonDetailsList();
                DTOCommonDetailsList itemCourseList = new DTOCommonDetailsList();
                DTOCommonDetailsList itemEducaitonList = new DTOCommonDetailsList();
                DTOCommonDetailsList itemPositionofTrustList = new DTOCommonDetailsList();
                DTOCommonDetailsList itemProjectexperiencelist = new DTOCommonDetailsList();
                DTOCommonDetailsList itemProjectrolelist = new DTOCommonDetailsList();
                DTOCommonDetailsList itemSkilllistComp = new DTOCommonDetailsList();
                DTOCommonDetailsList itemAreaofRespobsibiltyList = new DTOCommonDetailsList();
                DTOCommonDetailsList itemSkilllistJob = new DTOCommonDetailsList();
                DTOCommonDetailsList itemWorkTaskList = new DTOCommonDetailsList();

                DTOCommonDetailsList itemmedicalinshdr = new DTOCommonDetailsList();


                DTOPersonalContact additem = new DTOPersonalContact();
                additem.EmergencyContact = perinfoser.Perosnalcontactemergency;
                additem.Name = perinfoser.Perosnalcontactname;
                additem.RelationShip = perinfoser.Perosnalcontactrelationship;
                lstpersocontact.Add(additem);

                //perinfoser.Perosnalconta

                // perinfoser.ContactList.
                itempersonalinfo.Key = "PersonalInfo";
                itempersonalinfo.GroupKey = "PersonalInfo";
                itempersonalinfo.DisplayName = Helper.Resources.getDescriptionBykey("Personal Informations");
                itempersonalinfo.tabledata = new List<DataTable>();
                itempersonalinfo.PersonalNumber = perinfoser.Personnelnumber;
                itempersonalinfo.EnglishName = perinfoser.EnglishName;
                itempersonalinfo.JoiningDate = perinfoser.Joiningdate == _nulldate ? "-" : perinfoser.Joiningdate.ToShortDateString();
                itempersonalinfo.Gender = perinfoser.Gender == 1 ? "Male" : "Female";
                itempersonalinfo.OldID = perinfoser.OldId;
                itempersonalinfo.ArabicName = perinfoser.ArabicName;
                itempersonalinfo.MaritalStatus = perinfoser.Maritalstatus == 1 ? "Single" : "Married";
                itempersonalinfo.ProbhotionEndDate = perinfoser.ProbationEndDate == _nulldate ? "-" : perinfoser.ProbationEndDate.ToShortDateString();
                _commondetails.Add(itempersonalinfo);

                itemaddr.Key = "Address";
                itemaddr.GroupKey = "PersonalInfo";
                itemaddr.DisplayName = Helper.Resources.getDescriptionBykey("Address");
                itemaddr.tabledata = new List<DataTable>();
                itemaddr.tabledata.Add(FormDynamicDetailsView(perinfoser.AddressList.parmAddressList, "Address"));
                _commondetails.Add(itemaddr);


                itemContact.tabledata = new List<DataTable>();
                itemContact.Key = "Contacts";
                itemContact.GroupKey = "PersonalInfo";
                itemContact.DisplayName = Helper.Resources.getDescriptionBykey("Contacts");
                var contdisp = perinfoser.ContactList.parmContactList;
                var displist = (from a in contdisp
                                select new DTOContacts
                                {
                                    ContactNumber = a.ContactNumber,
                                    Description = a.Description,
                                    Extension = a.Extension,
                                    IsPrimary = a.IsPrimary.ToString(),
                                    Type = a.Type.ToString()
                                }).ToList();
                itemContact.tabledata.Add(FormDynamicDetailsView(displist, "Contacts"));
                _commondetails.Add(itemContact);

                itempersocontact.Key = "PersonalContact";
                itempersocontact.DisplayName = Helper.Resources.getDescriptionBykey("Personal Contact");
                itempersocontact.GroupKey = "PersonalInfo";
                itempersocontact.tabledata = new List<DataTable>();
                itempersocontact.tabledata.Add(FormDynamicDetailsView(lstpersocontact, "PersonalContact"));
                _commondetails.Add(itempersocontact);


                itembenifits.tabledata = new List<DataTable>();
                var beninfo = sdbenfits.getBenefitsInfo(callcont, username);

                itembenifits.AirportFrom = beninfo.Airportfrom;
                itembenifits.AirportTo = beninfo.Airportto;
                itembenifits.ContractEndDate = beninfo.ContractEndDate == _nulldate ? "-" : beninfo.ContractEndDate.ToShortDateString();
                itembenifits.ContractStartDate = beninfo.ContractStartDate == _nulldate ? "-" : beninfo.ContractStartDate.ToShortDateString();
                itembenifits.Garde = beninfo.Grade;
                itembenifits.SalaryScale = beninfo.SalaryScale;
                itembenifits.Paygroup = beninfo.PayGroup;
                itembenifits.BasicSalary = beninfo.BasicSalary.ToString();
                itembenifits.AnualLeave = beninfo.Annualleave.ToString();
                itembenifits.NoofTickets = beninfo.Nooftickets.ToString();
                itembenifits.TicketClass = beninfo.Ticketclass.ToString();
                itembenifits.TicketRate = beninfo.Ticketrates.ToString();
                itembenifits.Key = "ContractInfo";
                itembenifits.DisplayName = Helper.Resources.getDescriptionBykey("Contract Details");
                itembenifits.GroupKey = "Benifits";
                _commondetails.Add(itembenifits);

                itembenifitLines.tabledata = new List<DataTable>();
                itembenifitLines.Key = "Benifits";
                itembenifitLines.GroupKey = "Benifits";
                itembenifitLines.DisplayName = Helper.Resources.getDescriptionBykey("Benefit Details");
                var benfillist = sdbenfits.getBenefitsAndAllowDeduList(callcont, username).Benefitsallowancesdeductionslist.parmbenefitsAllowDedList;
                var todisp = (from a in benfillist
                              select new DTOBenifits
                              {
                                  BenifitID = a.BenefitsId,
                                  BenifitType = a.BenefitsType.ToString(),
                                  Percent = a.Percent.ToString(),
                                  xAmount = a.Amount.ToString()


                              }).ToList();
                itembenifitLines.tabledata.Add(FormDynamicDetailsView(todisp, "Benifits"));
                _commondetails.Add(itembenifitLines);


                itemBanks.tabledata = new List<DataTable>();
                itemBanks.tabledata.Add(FormDynamicDetailsView(sdbank.getAllBankInfoList(callcont, username).parmBankInfoList, "Banks"));
                itemBanks.Key = "Banks";
                itemBanks.GroupKey = "Banks";
                itemBanks.DisplayName = Helper.Resources.getDescriptionBykey("Banks");
                _commondetails.Add(itemBanks);

                itemSkilllistComp.tabledata = new List<DataTable>();
                itemSkilllistComp.tabledata.Add(FormDynamicDetailsView(competencies.Skilllist.parmList, "SkilllistComp"));
                itemSkilllistComp.Key = "SkilllistComp";
                itemSkilllistComp.GroupKey = "Skills";
                itemSkilllistComp.DisplayName = Helper.Resources.getDescriptionBykey("Skills");
                _commondetails.Add(itemSkilllistComp);

                itemCertificationList.tabledata = new List<DataTable>();
                itemCertificationList.tabledata.Add(FormDynamicDetailsView(competencies.CertificationList.parmList, "CertificationList"));
                itemCertificationList.Key = "CertificationList";
                itemCertificationList.GroupKey = "Skills";
                itemCertificationList.DisplayName = Helper.Resources.getDescriptionBykey("Certificates");
                _commondetails.Add(itemCertificationList);

                itemCourseList.tabledata = new List<DataTable>();
                itemCourseList.tabledata.Add(FormDynamicDetailsView(competencies.CourseList.parmList, "CourseList"));
                itemCourseList.Key = "CourseList";
                itemCourseList.GroupKey = "Skills";
                itemCourseList.DisplayName = Helper.Resources.getDescriptionBykey("Courses");
                _commondetails.Add(itemCourseList);

                itemEducaitonList.tabledata = new List<DataTable>();
                itemEducaitonList.tabledata.Add(FormDynamicDetailsView(competencies.EducaitonList.parmList, "EducaitonList"));
                itemEducaitonList.Key = "EducaitonList";
                itemEducaitonList.GroupKey = "Skills";
                itemEducaitonList.DisplayName = Helper.Resources.getDescriptionBykey("Educaiton");
                _commondetails.Add(itemEducaitonList);

                itemPositionofTrustList.tabledata = new List<DataTable>();
                itemPositionofTrustList.tabledata.Add(FormDynamicDetailsView(competencies.PositionofTrustList.parmList, "PositionofTrustList"));
                itemPositionofTrustList.Key = "PositionofTrustList";
                itemPositionofTrustList.GroupKey = "Skills";
                itemPositionofTrustList.DisplayName = Helper.Resources.getDescriptionBykey("Position Trust");
                _commondetails.Add(itemPositionofTrustList);

                itemProjectexperiencelist.tabledata = new List<DataTable>();
                itemProjectexperiencelist.tabledata.Add(FormDynamicDetailsView(competencies.Projectexperiencelist.parmList, "Projectexperiencelist"));
                itemProjectexperiencelist.Key = "Projectexperiencelist";
                itemProjectexperiencelist.GroupKey = "Skills";
                itemProjectexperiencelist.DisplayName = Helper.Resources.getDescriptionBykey("Project Experience");
                _commondetails.Add(itemProjectexperiencelist);

                itemProjectrolelist.tabledata = new List<DataTable>();
                itemProjectrolelist.tabledata.Add(FormDynamicDetailsView(competencies.Projectrolelist.parmList, "Projectrolelist"));
                itemProjectrolelist.Key = "Projectrolelist";
                itemProjectrolelist.GroupKey = "Skills";
                itemProjectrolelist.DisplayName = Helper.Resources.getDescriptionBykey("Project Role");
                _commondetails.Add(itemProjectrolelist);




                itemJobs.tabledata = new List<DataTable>();
                itemJobs.PositionId = jobs.PositionId;
                itemJobs.PositionDescription = jobs.Positiondescription;
                itemJobs.Department = perinfoser.DepartmentName;
                itemJobs.ReportToLineManagerName = perinfoser.LineManagerName;
                itemJobs.PositionType = perinfoser.PositionType;
                itemJobs.PositionCompensation = perinfoser.Positiondescription;
                itemJobs.JobType = perinfoser.JobTypeId;
                itemJobs.JobFunction = perinfoser.JobFunctionId;
                itemJobs.JobId = perinfoser.JobId;
                itemJobs.ReporttoPositionManagerId = perinfoser.PositionLineManagerposition;
                itemJobs.PositioinCompensationName = perinfoser.Compenstationregion;
                itemJobs.Key = "Jobs";
                itemJobs.GroupKey = "Jobs";
                itemJobs.DisplayName = Helper.Resources.getDescriptionBykey("Jobs");
                _commondetails.Add(itemJobs);

                itemAreaofRespobsibiltyList.tabledata = new List<DataTable>();
                itemAreaofRespobsibiltyList.Key = "AreaofRespobsibiltyList";
                itemAreaofRespobsibiltyList.GroupKey = "Jobs";
                itemAreaofRespobsibiltyList.DisplayName = Helper.Resources.getDescriptionBykey("Area of Responsibility");
                itemAreaofRespobsibiltyList.tabledata.Add(FormDynamicDetailsView(jobs.AreaofRespobsibiltyList.parmList, "AreaofRespobsibiltyList"));
                _commondetails.Add(itemAreaofRespobsibiltyList);

                itemSkilllistJob.tabledata = new List<DataTable>();
                itemSkilllistJob.tabledata.Add(FormDynamicDetailsView(jobs.SkillList.parmList, "SkilllistJob"));
                itemSkilllistJob.Key = "SkilllistJob";
                itemSkilllistJob.GroupKey = "Jobs";
                itemSkilllistJob.DisplayName = Helper.Resources.getDescriptionBykey("Skills");
                _commondetails.Add(itemSkilllistJob);


                itemWorkTaskList.tabledata = new List<DataTable>();
                itemWorkTaskList.tabledata.Add(FormDynamicDetailsView(jobs.WorkTaskList.parmList, "WorkTaskList"));
                itemWorkTaskList.Key = "WorkTaskList";
                itemWorkTaskList.GroupKey = "Jobs";
                itemWorkTaskList.DisplayName = Helper.Resources.getDescriptionBykey("Job Task");
                _commondetails.Add(itemWorkTaskList);

                itemDepend.tabledata = new List<DataTable>();
                var dependencies = sddepend.getAllDependentInfoList(callcont, username, compid).parmIdentificationInfoList;
                var disp = (from a in dependencies
                            select new
                            {
                                ArabicName = a.ArabicName,
                                BirthDate = a.BirthDate == _nulldate ? "-" : a.BirthDate.ToShortDateString(),
                                DependentId = a.DependentId,
                                EnglishName = a.EnglishName,
                                MedicalInsuranceCover = a.Medicalinsurancecover.ToString(),
                                Relationship = a.Relationship,
                                TicketCover = a.TicketCover.ToString()
                            }).ToList();
                itemDepend.tabledata.Add(FormDynamicDetailsView(disp, "Dependencies"));
                itemDepend.Key = "Dependencies";
                itemDepend.GroupKey = "Dependencies";
                itemDepend.DisplayName = Helper.Resources.getDescriptionBykey("Dependents");


                _commondetails.Add(itemDepend);

                itemexecreq.tabledata = new List<DataTable>();
                itemexecreq.tabledata.Add(FormDynamicDetailsView(sdexcreq.getAllExcuseReqeust(callcont, username, compid).parmExcuseRequestList, "ExecuseRequests"));
                itemexecreq.Key = "ExecuseRequests";
                itemexecreq.GroupKey = "ExecuseRequests";
                itemexecreq.DisplayName = Helper.Resources.getDescriptionBykey("Excuse Requests");
                _commondetails.Add(itemexecreq);

                itemmedicalinshdr.tabledata = new List<DataTable>();
                itemmedicalinshdr.Key = "itemmedicalinshdr";
                itemmedicalinshdr.GroupKey = "MedicalInsurance";
                itemmedicalinshdr.DisplayName = Helper.Resources.getDescriptionBykey("Medical Insurance");
                itemMedicalInsurance.tabledata = new List<DataTable>();
                var insurance = medinsurance.parmMedicalInsuranceList;
                var inslist = (from a in insurance
                               where a.PolicyFor != "Worker"
                               select new DTOMedicalInsurancelst
                               {

                                   DependentName = a.DependentNameAr,
                                   // Endurance = a.Endurance.ToString(),
                                   MembershipId = a.MembershipId,
                                   PolicyLevel = a.Policylevel,
                                   //  PolicyFor = a.PolicyFor,'
                                   DependentNameEnglish = a.DependentNameEn,
                                   PolicyNumber = a.PolicyNumber,
                                   PregnencyCover = a.PregnancyCovered.ToString(),
                                   Room = a.Room

                               }).ToList();
                var inshdr = (from a in insurance
                              where a.PolicyFor == "Worker"
                              select new DTOMedicalInsurance
                              {
                                  WorkerName = a.WorkerName,
                                  WorkerRecId = a.WorkerRecid.ToString(),
                                  DentalLimit = a.DentalLimit.ToString(),
                                  DependentNameArabic = a.DependentNameAr,
                                  DependentNameEnglish = a.DependentNameEn,
                                  Endurance = a.Endurance.ToString(),
                                  OccuralLimit = a.OcularLimir.ToString(),
                                  PolicyFor = a.PolicyFor,
                                  PolicyLevel = a.Policylevel,
                                  PolicyNumber = a.PolicyNumber,
                                  Room = a.Room,
                                  FromDate = a.FromDateTime == _nulldate ? "-" : a.FromDateTime.ToShortDateString(),
                                  ToDate = a.ToDateTime == _nulldate ? "-" :  a.ToDateTime.ToShortDateString(),
                                  TotalLimit = a.TotalLimit.ToString(),

                              }).FirstOrDefault();
                if (inshdr != null)
                {
                    itemmedicalinshdr.DentalLimit = inshdr.DentalLimit;
                    itemmedicalinshdr.Endurance = inshdr.Endurance;
                    itemmedicalinshdr.OccuralLimit = inshdr.OccuralLimit;
                    itemmedicalinshdr.PolicyLevel = inshdr.PolicyLevel;
                    itemmedicalinshdr.FromDate =   inshdr.FromDate;
                    itemmedicalinshdr.ToDate =  inshdr.ToDate;
                    itemmedicalinshdr.WorkerName = inshdr.WorkerName;
                    itemmedicalinshdr.TotalLimit = inshdr.TotalLimit;
                    itemMedicalInsurance.tabledata.Add(FormDynamicDetailsView(inslist, "MedicalInsurancelst"));
                    itemMedicalInsurance.Key = "MedicalInsurancelst";
                    itemMedicalInsurance.GroupKey = "MedicalInsurance";
                    itemMedicalInsurance.DisplayName = Helper.Resources.getDescriptionBykey("Medical Insurance Dependents");
                    _commondetails.Add(itemmedicalinshdr);
                    _commondetails.Add(itemMedicalInsurance);
                }

                itemidenti.tabledata = new List<DataTable>();
                var identilist = sdidenreq.getAllIdentificationList(callcont, username, compid).parmIdentificationInfoList;
                var dispiden = (from a in identilist
                                select new
                                {
                                    IdentificationNumber = a.IdentificationNumber,
                                    IdentificationType = a.IdentificationType,
                                    IssueAgency = a.IssueAgency,
                                    IssueDate = a.IssueDate == _nulldate ? "-" : a.IssueDate.ToShortDateString(),
                                    EndDate = a.EndDate == _nulldate ? "-" : a.EndDate.ToShortDateString(),
                                    IssuePlace = a.Issueplace,
                                }).ToList();
                itemidenti.tabledata.Add(FormDynamicDetailsView(dispiden, "Identifications"));

                itemidenti.Key = "Identifications";
                itemidenti.GroupKey = "Identifications";
                itemidenti.DisplayName = Helper.Resources.getDescriptionBykey("Identifications");
                _commondetails.Add(itemidenti);

                itemleavereq.tabledata = new List<DataTable>();
                itemleavereq.tabledata.Add(FormDynamicDetailsView(sdleavereq.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList, "LeaveRequests"));
                itemleavereq.Key = "LeaveRequests";

                _commondetails.Add(itemleavereq);

                //itempayslip.ListbyKey = FormDynamicDetailsView(sdspayslip.getPaySlipInfo(callcont, username, 2017, 10, compid), "Payslip");
                //itempayslip.Key = "Payslip";
                //itempayslip.Headers = itempayslip.ListbyKey.FirstOrDefault().Headers;
                //_commondetails.Add(itempayslip);


                //itempayslipdetail.tabledata = FormDynamicDetailsView(sdspayslip.getPaySlipLineInfoList(callcont, username, 2017, 10, compid).parmpaySlipLineInfoList, "PayslipDetails");
                //  itempayslipdetail.Key = "PayslipDetails";

                // _commondetails.Add(itempayslipdetail);
                if (Group != null && Group != "")
                    _commondetails = _commondetails.Where(x => x.GroupKey == Group).ToList();
                if (Detail != null && Detail != "")
                    _commondetails = _commondetails.Where(x => x.Key == Detail).ToList();

                //   C:\Sabri\SoltechESSPortal\ESSPortal\ESSPortal\Views\.cshtml
                return View(_commondetails);
            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }
        }

        public FileContentResult getImg(byte[] id)
        {
            byte[] byteArray = id;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        private DataTable FormDynamicDetailsView(dynamic parmAddressList, string Key)
        {

            DataTable table = new DataTable();
            if (Key != "Payslip")
            {
                using (var reader = ObjectReader.Create(parmAddressList))
                {
                    table.Load(reader);
                }
            }
            if (table.Columns.Contains("ExtensionData"))
            {
                table.Columns.Remove("ExtensionData");
            }

            return table;
        }

        [HttpGet]
        public JsonResult GetAllLanguage() //It will be fired from Jquery ajax call  
        {
            var jsonData = Helper.Resources.getAllLanguages();
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public void ChangeLanguage(string languageID)
        {
            try
            {
                var language = new HttpCookie("Language", languageID);
                HttpContext.Response.Cookies.Add(language);
            }
            catch (Exception ex)
            {

            }

        }

    }
}