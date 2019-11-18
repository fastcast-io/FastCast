using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastCast.Pages
{
    public class AnswerModel : PageModel
    {
        private readonly IFastCastService _fastCastService;
        public string SessionFormId { get; set; }
        public bool Authenticated { get; set; }
        public AnswerModel(IFastCastService fastCastService)
        {
            _fastCastService = fastCastService;
        }

        public void OnGet()
        {
            String sessionCode = _fastCastService.GetData("SessionCode");
            if (sessionCode == "-1")
            {
                ViewData["SessionAnswerError"] = "Could not authentify your request. Please re-enter your session code in the home page";
                Authenticated = false;
            }
            else
            {
                Authenticated = true;
                ViewData["SessionCode"] = sessionCode;
                ViewData["SessionName"] = _fastCastService.GetData("SessionName");
                SessionFormId = _fastCastService.GetData("SessionFormId");
                ViewData["SessionFormId"] = _fastCastService.GetData("SessionFormId");
                ViewData["SessionDuration"] = _fastCastService.GetData("SessionDuration");
            }
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            // Todo: add some more checks and different checks
            return RedirectToPage("./AnswerSuccess");

        }
    }
}
